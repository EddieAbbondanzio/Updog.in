using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class AdminRegisterOrUpdateCommandHandler : CommandHandler<AdminRegisterOrUpdateCommand> {
        #region Fields
        private IUserService service;
        #endregion

        #region Constructor(s)
        public AdminRegisterOrUpdateCommandHandler(IUserService service) {
            this.service = service;
        }
        #endregion

        protected async override Task<Either<CommandResult, Error>> ExecuteCommand(AdminRegisterOrUpdateCommand command) {
            User admin = await service.AdminRegisterOrUpdate(command.Config);
            return Success();
        }
    }
}