using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class SubscriptionCreateCommandHandler : CommandHandler<SubscriptionCreateCommand> {
        #region Fields
        private ISpaceService spaceService;
        private ISubscriptionService subService;
        #endregion

        #region Constructor(s)
        public SubscriptionCreateCommandHandler(ISpaceService spaceService, ISubscriptionService subService) {
            this.spaceService = spaceService;
            this.subService = subService;
        }
        #endregion

        #region Publics
        [Validate(typeof(SubscriptionCreateCommandValidator))]
        protected async override Task<Either<CommandResult, Error>> ExecuteCommand(SubscriptionCreateCommand command) {
            if (!(await spaceService.DoesSpaceExist(command.Data.Space))) {
                return new NotFoundError();
            }

            Subscription s = await subService.CreateSubscription(command.Data, command.User);
            return Insert(s.Id);
        }
        #endregion
    }
}