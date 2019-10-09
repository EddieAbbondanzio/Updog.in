using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class SubscriptionDeleteCommandHandler : CommandHandler<SubscriptionDeleteCommand> {
        #region Fields
        private ISubscriptionService service;
        #endregion

        #region Constructor(s)
        public SubscriptionDeleteCommandHandler(ISubscriptionService service) {
            this.service = service;
        }
        #endregion

        #region Publics
        [Validate(typeof(SubscriptionDeleteCommandValidator))]
        protected async override Task<CommandResult> ExecuteCommand(SubscriptionDeleteCommand command) {
            Subscription s = await service.DeleteSubscription(command.Data, command.User);
            return new CommandResult(true);
        }
        #endregion
    }
}