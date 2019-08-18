using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to find a user by their username.
    /// </summary>
    public sealed class UserFinderByUsername : IInteractor<string, UserView> {
        #region Fields
        private IUserRepo userRepo;

        private UserViewMapper userMapper;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create new user finder.
        /// </summary>
        /// <param name="userRepo">The user repo.</param>
        /// <param name="userMapper">The DTO mapper.</param>
        public UserFinderByUsername(IUserRepo userRepo, UserViewMapper userMapper) {
            this.userRepo = userRepo;
            this.userMapper = userMapper;
        }
        #endregion

        #region Publics
        public async Task<UserView> Handle(string username) {
            User user = await userRepo.FindByUsername(username);
            return userMapper.Map(user);
        }
        #endregion
    }
}