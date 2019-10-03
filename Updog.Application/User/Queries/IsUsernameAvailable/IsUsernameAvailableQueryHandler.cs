using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class IsUsernameAvailableQueryHandler : QueryHandler<IsUsernameAvailableQuery> {
        #region Fields
        private IUserViewMapper userMapper;
        #endregion

        #region Constructor(s)
        public IsUsernameAvailableQueryHandler(IDatabase database, IUserViewMapper userMapper) : base(database) {
            this.userMapper = userMapper;
        }
        #endregion

        #region Publics
        [Validate(typeof(IsUsernameAvailableQueryValidator))]
        protected async override Task ExecuteQuery(ExecutionContext<IsUsernameAvailableQuery> context) {
            if (User.IsUsernameBanned(context.Input.Username)) {
                context.Output.BadInput("Username is unavailable");
                return;
            }

            IUserRepo userRepo = context.Database.GetRepo<IUserRepo>();
            User? user = await userRepo.FindByUsername(context.Input.Username);

            if (user == null) {
                context.Output.NotFound($"No user with username {context.Input.Username} found");
            } else {
                context.Output.Success(user);
            }
        }
        #endregion
    }
}