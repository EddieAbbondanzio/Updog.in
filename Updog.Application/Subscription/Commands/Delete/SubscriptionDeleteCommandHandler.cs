using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class SubscriptionDeleteCommandHandler : CommandHandler<SubscriptionDeleteCommand> {
        #region Fields
        private ISpaceService spaceService;
        private ISubscriptionService subService;
        #endregion

        #region Constructor(s)
        public SubscriptionDeleteCommandHandler(ISpaceService spaceService, ISubscriptionService subService) {
            this.spaceService = spaceService;
            this.subService = subService;
        }
        #endregion

        #region Publics
        [Validate(typeof(SubscriptionDeleteCommandValidator))]
        protected async override Task<Either<CommandResult, Error>> ExecuteCommand(SubscriptionDeleteCommand command) {
            if (!(await spaceService.DoesSpaceExist(command.Space))) {
                return new NotFoundError();
            }

            await subService.DeleteSubscription(command.Space, command.User);
            return Success();
        }
        #endregion
    }
}