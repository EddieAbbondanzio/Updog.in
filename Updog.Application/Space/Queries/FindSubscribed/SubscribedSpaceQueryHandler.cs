using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class SubscribedSpaceQueryHandler : QueryHandler<SubscribedSpaceQuery, IEnumerable<SpaceReadView>> {
        #region Fields
        private ISpaceReader spaceReader;
        #endregion

        #region Constructor(s)
        public SubscribedSpaceQueryHandler(ISpaceReader spaceReader) {
            this.spaceReader = spaceReader;
        }
        #endregion

        #region Publics
        protected async override Task<IEnumerable<SpaceReadView>> ExecuteQuery(SubscribedSpaceQuery command) => await spaceReader.FindSubscribed(command.User);
        #endregion
    }
}