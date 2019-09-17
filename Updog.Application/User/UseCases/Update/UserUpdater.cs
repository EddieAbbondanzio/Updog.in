
using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to update a user.
    /// </summary>
    public sealed class UserUpdater : Interactor<UpdateUserParams> {
        #region Fields
        private IDatabase database;
        #endregion

        #region Constructor(s)
        public UserUpdater(IDatabase database) {
            this.database = database;
        }
        #endregion

        #region Publics
        [Validate(typeof(UserUpdateValidator))]
        protected override async Task HandleInput(UpdateUserParams input) {
            using (var connection = database.GetConnection()) {
                IUserRepo userRepo = database.GetRepo<IUserRepo>(connection);

                //Is the email already in use?
                User? existing = await userRepo.FindByEmail(input.Email);

                if (!existing?.Equals(input.User) ?? false) {
                    throw new CollisionException("Email is already in use.");
                }

                // Good to go, update and save off the change.
                input.User.Email = input.Email;
                await userRepo.Update(input.User);
            }
        }
        #endregion
    }
}