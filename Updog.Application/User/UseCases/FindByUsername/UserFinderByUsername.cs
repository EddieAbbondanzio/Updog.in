using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to find a user by their username.
    /// </summary>
    public sealed class UserFinderByUsername : IInteractor<UserFindByUsernameParams, UserView?> {
        #region Fields
        private IDatabase database;
        private IUserViewMapper userMapper;
        #endregion

        #region Constructor(s)
        public UserFinderByUsername(IDatabase database, IUserViewMapper userMapper) {
            this.database = database;
            this.userMapper = userMapper;
        }
        #endregion

        #region Publics
        public async Task<UserView?> Handle(UserFindByUsernameParams input) {
            using (var connection = database.GetConnection()) {
                IUserRepo userRepo = database.GetRepo<IUserRepo>(connection);
                User? user = await userRepo.FindByUsername(input.Username);

                if (user == null) {
                    return null;
                }

                return userMapper.Map(user);
            }
        }
        #endregion
    }
}