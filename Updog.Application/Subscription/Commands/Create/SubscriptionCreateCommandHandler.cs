using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class SubscriptionCreateCommandHandler : CommandHandler<SubscriptionCreateCommand> {
        #region Fields
        private ISubscriptionService service;
        #endregion

        #region Constructor(s)
        public SubscriptionCreateCommandHandler(ISubscriptionService service) {
            this.service = service;
        }
        #endregion

        #region Publics
        [Validate(typeof(SubscriptionCreateCommandValidator))]
        protected async override Task<CommandResult> ExecuteCommand(SubscriptionCreateCommand command) {
            Subscription s = await service.CreateSubscription(command.Data, command.User);
            return new InsertResult(true, s.Id);
        }
        #endregion
    }
}