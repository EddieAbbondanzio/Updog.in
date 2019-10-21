using System;
using System.Threading.Tasks;

namespace Updog.Domain {
    public sealed class UserService : IUserService {
        #region Fields
        private IEventBus bus;
        private IUserFactory factory;
        private IUserRepo userRepo;
        private IUserLoginRepo loginRepo;
        #endregion

        #region Constructor(s)
        public UserService(IEventBus bus, IUserFactory factory, IUserRepo userRepo, IUserLoginRepo loginRepo) {
            this.bus = bus;
            this.factory = factory;
            this.userRepo = userRepo;
            this.loginRepo = loginRepo;
        }
        #endregion

        #region Publics
        public async Task AdminRegisterOrUpdate(IAdminConfig config) {
            User? existingAdmin = await userRepo.FindByUsername(config.Username);

            if (existingAdmin != null) {
                // Check if we need to update the password to match the config one.
                if (!existingAdmin.Authenticate(config.Password)) {
                    existingAdmin.ResetPassword(config.Password);
                    await userRepo.Update(existingAdmin);
                }
            } else {
                User user = factory.CreateFromAdminConfig(config);
                await userRepo.Add(user);
            }

        }

        public async Task<UserLogin?> Login(UserCredentials credentials) {
            User? user = await userRepo.FindByUsername(credentials.Username);

            //If no use was found, or the password doesn't match the one on file, fail.
            if (user == null || !user.Authenticate(credentials.Password)) {
                return null;
            }

            UserLogin login = new UserLogin(user.Id);
            await loginRepo.Add(login);
            await bus.Dispatch(new UserLoginEvent(user, login));

            return login;
        }

        public async Task<UserLogin> Register(UserRegistration registration) {
            // If registration provided an email, check to see if it's available.
            if (!String.IsNullOrWhiteSpace(registration.Email)) {
                User? emailInUse = await userRepo.FindByEmail(registration.Email!);
                if (emailInUse != null) {
                    throw new EmailAlreadyInUseException();
                }
            }

            // Check to see if the username is unique.
            User? usernameInUse = await userRepo.FindByUsername(registration.Username);
            if (usernameInUse != null) {
                throw new UsernameAlreadyInUseException();
            }

            User user = factory.CreateFromRegistration(registration);
            await userRepo.Add(user);

            UserLogin login = new UserLogin(user.Id);
            await loginRepo.Add(login);
            await bus.Dispatch(new UserRegisterEvent(user, login));

            return login;
        }

        public async Task Update(string username, UserUpdate update) {
            User? user = await userRepo.FindByUsername(username);

            if (user == null) {
                throw new NotFoundException($"User {username} not found.");
            }

            User? existing = await userRepo.FindByEmail(update.Email);

            if (!existing?.Equals(user) ?? false) {
                throw new NotFoundException("User was not found.");
            }

            // Good to go, update and save off the change.
            user.Update(update);
            await userRepo.Update(user);

            await bus.Dispatch(new UserUpdateEvent(user));
        }

        public async Task UpdatePassword(string username, UserUpdatePassword data) {
            User? user = await userRepo.FindByUsername(username);

            if (user == null) {
                throw new NotFoundException($"User {username} not found.");
            }

            user.SetPassword(data.CurrentPassword, data.NewPassword);
            await userRepo.Update(user);
        }

        public async Task<bool> DoesUserExist(string username) => (await userRepo.FindByUsername(username)) != null;

        public async Task<bool> IsEmailAlreadyInUse(string email) => (await userRepo.FindByEmail(email)) == null;

        public async Task<bool> IsUsernameAvailable(string username) => (await userRepo.FindByUsername(username)) == null;

        #endregion
    }
}