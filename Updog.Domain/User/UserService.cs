using System;
using System.Threading.Tasks;

namespace Updog.Domain {
    public sealed class UserService : IUserService {
        #region Fields
        private IEventBus bus;
        private IUserFactory factory;
        private IUserRepo repo;
        private IPasswordHasher passwordHasher;
        private IAuthenticationTokenHandler tokenHandler;
        #endregion

        #region Constructor(s)
        public UserService(IEventBus bus, IUserFactory factory, IUserRepo repo, IPasswordHasher passwordHasher, IAuthenticationTokenHandler tokenHandler) {
            this.bus = bus;
            this.factory = factory;
            this.repo = repo;
            this.passwordHasher = passwordHasher;
            this.tokenHandler = tokenHandler;
        }
        #endregion

        #region Publics
        public async Task<User> AdminRegisterOrUpdate(IAdminConfig config) {
            User? existingAdmin = await repo.FindByUsername(config.Username);

            if (existingAdmin != null) {
                // Check if we need to update the password to match the config one.
                if (!passwordHasher.Verify(config.Password, existingAdmin.PasswordHash)) {
                    existingAdmin.PasswordHash = passwordHasher.Hash(config.Password);
                    await repo.Update(existingAdmin);
                }

                return existingAdmin;
            }

            User user = factory.CreateFromAdminConfig(config);
            await repo.Add(user);

            return user;
        }

        public async Task<UserLogin?> Login(UserCredentials credentials) {
            User? user = await repo.FindByUsername(credentials.Username);

            if (user == null) {
                return null;
            }

            if (passwordHasher.Verify(credentials.Password, user.PasswordHash)) {
                string authToken = tokenHandler.IssueToken(user);
                return new UserLogin(user.Id, authToken);
            } else {
                return null;
            }
        }

        public async Task<UserLogin?> Register(UserRegistration registration) {
            // Check that the email is free first.
            if (!String.IsNullOrWhiteSpace(registration.Email)) {
                User? emailInUse = await repo.FindByEmail(registration.Email!);
                if (emailInUse != null) {
                    return null;
                }
            }

            User? usernameInUse = await repo.FindByUsername(registration.Username);
            if (usernameInUse != null) {
                return null;
            }

            User user = factory.CreateFromRegistration(registration);
            await repo.Add(user);

            UserLogin login = new UserLogin(user.Id, tokenHandler.IssueToken(user));
            await bus.Dispatch(new UserRegisterEvent(user, login));

            return login;
        }

        public async Task<User> Update(UserUpdateData data) {
            User? user = await repo.FindByEmail(data.Email);

            if (!user?.Equals(data.User) ?? false) {
                throw new InvalidOperationException();
            }

            // Good to go, update and save off the change.
            user!.Email = data.Email;

            await repo.Update(user);
            await bus.Dispatch(new UserUpdateEvent(user));

            return user;
        }

        public async Task<User> UpdatePassword(UserPasswordUpdateData data) {
            //Verify the old password is a match first
            bool isMatch = passwordHasher.Verify(data.CurrentPassword, data.User.PasswordHash);

            if (isMatch) {
                data.User.PasswordHash = passwordHasher.Hash(data.NewPassword);
                await repo.Update(data.User);
                return data.User;
            } else {
                throw new InvalidOperationException();
            }
        }
        #endregion
    }
}