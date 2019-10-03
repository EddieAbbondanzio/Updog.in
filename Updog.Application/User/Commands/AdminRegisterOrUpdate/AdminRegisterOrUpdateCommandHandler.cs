using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class AdminRegisterOrUpdateCommandHandler : CommandHandler<AdminRegisterOrUpdateCommand> {
        #region Fields
        private IUserFactory userFactory;
        private IPasswordHasher passwordHasher;
        private IUserViewMapper userMapper;

        public AdminRegisterOrUpdateCommandHandler(IDatabase database, IUserFactory userFactory, IPasswordHasher passwordHasher, IUserViewMapper userMapper) : base(database) {
            this.userFactory = userFactory;
            this.passwordHasher = passwordHasher;
            this.userMapper = userMapper;
        }
        #endregion

        protected async override Task ExecuteCommand(ExecutionContext<AdminRegisterOrUpdateCommand> context) {
            IUserRepo userRepo = context.Database.GetRepo<IUserRepo>();
            ISpaceRepo spaceRepo = context.Database.GetRepo<ISpaceRepo>();
            ISubscriptionRepo subRepo = context.Database.GetRepo<ISubscriptionRepo>();

            // See if the admin exists first.
            User? existingAdmin = await userRepo.FindByUsername(context.Input.Config.Username);

            if (existingAdmin != null) {
                // Update password is config file has new value.
                if (!passwordHasher.Verify(context.Input.Config.Password, existingAdmin.PasswordHash)) {
                    existingAdmin.PasswordHash = passwordHasher.Hash(context.Input.Config.Password);
                    await userRepo.Update(existingAdmin);
                }

                context.Output.Success(existingAdmin);
                return;
            }

            User user = userFactory.CreateFromAdminConfig(context.Input.Config);

            await userRepo.Add(user);
            context.Output.Success(user);
        }
    }
}