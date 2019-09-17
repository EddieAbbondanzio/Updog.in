using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class UserPasswordUpdater : Interactor<UserPasswordUpdateParams> {
        #region Fields
        private IDatabase database;
        private IPasswordHasher passwordHasher;
        #endregion

        #region Constructor(s)
        public UserPasswordUpdater(IDatabase database, IPasswordHasher passwordHasher) {
            this.database = database;
            this.passwordHasher = passwordHasher;
        }
        #endregion

        #region Helpers
        [Validate(typeof(UserPasswordUpdateValidator))]
        protected override async Task HandleInput(UserPasswordUpdateParams input) {
            using (var connection = database.GetConnection()) {
                IUserRepo userRepo = database.GetRepo<IUserRepo>(connection);

                //Verify the old password is a match first
                bool isMatch = passwordHasher.Verify(input.CurrentPassword, input.User.PasswordHash);

                if (isMatch) {
                    input.User.PasswordHash = passwordHasher.Hash(input.NewPassword);
                    await userRepo.Update(input.User);
                } else {
                    throw new AuthorizationException();
                }
            }
        }
    }
    #endregion
}