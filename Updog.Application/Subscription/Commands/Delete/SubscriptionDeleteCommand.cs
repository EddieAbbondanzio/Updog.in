using Updog.Domain;

namespace Updog.Application {
    public sealed class SubscriptionDeleteCommand : AuthenticatedCommand {
        #region Properties
        /// <summary>
        /// The name of the space.
        /// </summary>
        /// <value></value>
        public string Space { get; set; } = "";
        #endregion
    }
}