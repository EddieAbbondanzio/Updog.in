using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class AdminRegistrar : Interactor<IAdminConfig, UserView> {
        #region Fields
        private IDatabase database;
        private IUserViewMapper userMapper;
        private IPasswordHasher passwordHasher;
        #endregion

        #region Constructor(s)
        public AdminRegistrar(IDatabase database, IUserViewMapper userMapper, IPasswordHasher passwordHasher) {
            this.database = database;
            this.userMapper = userMapper;
            this.passwordHasher = passwordHasher;
        }
        #endregion 

        #region Helpers
        protected async override Task<UserView> HandleInput(IAdminConfig input) {
            using (var connection = database.GetConnection()) {
                IUserRepo userRepo = database.GetRepo<IUserRepo>(connection);

                // See if the admin exists first.
                User? existingAdmin = await userRepo.FindByUsername(input.Username);

                if (existingAdmin != null) {
                    return userMapper.Map(existingAdmin);
                }

                // Otherwise create the new user.
                User admin = new User() {
                    Username = input.Username,
                    PasswordHash = passwordHasher.Hash(input.Password),
                    JoinedDate = System.DateTime.UtcNow
                };

                await userRepo.Add(admin);
                return userMapper.Map(admin);
            }

        }
        #endregion
    }
}