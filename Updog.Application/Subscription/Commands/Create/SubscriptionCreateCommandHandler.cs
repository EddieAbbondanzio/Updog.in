using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class SubscriptionCreateCommandHandler : CommandHandler<SubscriptionCreateCommand> {
        #region Constructor(s)
        public SubscriptionCreateCommandHandler(IDatabase database) : base(database) { }
        #endregion

        #region Publics
        [Validate(typeof(SubscriptionCreateCommandValidator))]
        protected async override Task ExecuteCommand(ExecutionContext<SubscriptionCreateCommand> context) {
            IUserRepo userRepo = context.Database.GetRepo<IUserRepo>();
            ISpaceRepo spaceRepo = context.Database.GetRepo<ISpaceRepo>();
            ISubscriptionRepo subRepo = context.Database.GetRepo<ISubscriptionRepo>();

            //Pull in the space first
            Space? space = await spaceRepo.FindByName(context.Input.Space);

            if (space == null) {
                context.Output.BadInput($"No space with name {context.Input.Space} exists.");
                return;
            }

            //Try to pull in the subscription
            Subscription? sub = await subRepo.FindByUserAndSpace(context.Input.User.Username, context.Input.Space);

            if (sub == null) {
                context.Output.BadInput($"Subscription does not exist");
                return;
            }

            await subRepo.Add(sub);

            space.SubscriptionCount++;

            await spaceRepo.Update(space);
        }
        #endregion
    }
}