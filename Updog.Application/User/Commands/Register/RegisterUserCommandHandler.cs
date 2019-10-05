using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class RegisterUserCommandHandler : CommandHandler<RegisterUserCommand> {
        #region Fields
        IUserFactory userFactory;

        ISubscriptionFactory subscriptionFactory;
        private IAuthenticationTokenHandler tokenHandler;
        private IUserViewMapper userMapper;

        public RegisterUserCommandHandler(IDatabase database, IUserFactory userFactory, ISubscriptionFactory subcriptionFactory, IAuthenticationTokenHandler tokenHandler, IUserViewMapper userMapper) : base(database) {
            this.userFactory = userFactory;
            this.subscriptionFactory = subcriptionFactory;
            this.tokenHandler = tokenHandler;
            this.userMapper = userMapper;
        }
        #endregion

        [Validate(typeof(RegisterUserCommandValidator))]
        protected async override Task ExecuteCommand(ExecutionContext<RegisterUserCommand> context) {
            IUserRepo userRepo = context.Database.GetRepo<IUserRepo>();
            ISpaceRepo spaceRepo = context.Database.GetRepo<ISpaceRepo>();
            ISubscriptionRepo subRepo = context.Database.GetRepo<ISubscriptionRepo>();


            // Check that the email is free first.
            if (!String.IsNullOrWhiteSpace(context.Input.Registration.Email)) {
                User? emailInUse = await userRepo.FindByEmail(context.Input.Registration.Email!);
                if (emailInUse != null) {
                    context.Output.BadInput("Email is already in use");
                    return;
                }
            }

            User? usernameInUse = await userRepo.FindByUsername(context.Input.Registration.Username);
            if (usernameInUse != null) {
                context.Output.BadInput("Username is unavailable");
                return;
            }

            User user = userFactory.CreateFromRegistration(context.Input.Registration);
            await userRepo.Add(user);

            // Subscribe the user to the default spaces.
            IEnumerable<Space> defaultSpaces = await spaceRepo.FindDefault();
            IEnumerable<Subscription> defaultSubscriptions = defaultSpaces.Select(space => subscriptionFactory.CreateFor(user, space));

            UserView userView = userMapper.Map(user);
            string authToken = tokenHandler.IssueToken(user);

            context.Output.Success(new UserLogin(userView, authToken));
        }
    }
}