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
        protected async override Task<Either<IEnumerable<SpaceReadView>, Error>> ExecuteQuery(SubscribedSpaceQuery command) =>
            new Either<IEnumerable<SpaceReadView>, Error>(
                await spaceReader.FindSubscribed(command.User)
            );
        #endregion
    }
}