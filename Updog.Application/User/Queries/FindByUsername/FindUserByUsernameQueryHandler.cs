using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class FindUserByUsernameQueryHandler : QueryHandler<FindUserByUsernameQuery> {
        #region Fields
        private IUserViewMapper userMapper;
        #endregion

        #region Constructor(s)
        public FindUserByUsernameQueryHandler(IDatabase database, IUserViewMapper userMapper) : base(database) {
            this.userMapper = userMapper;
        }
        #endregion

        #region Publics
        [Validate(typeof(FindUserByUsernameQueryValidator))]
        protected async override Task ExecuteQuery(ExecutionContext<FindUserByUsernameQuery> context) {
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