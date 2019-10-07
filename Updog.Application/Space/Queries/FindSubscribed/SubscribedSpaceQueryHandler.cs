using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class SubscribedSpaceQueryHandler : QueryHandler<SubscribedSpaceQuery> {
        #region Fields
        private ISpaceReader spaceReader;
        #endregion

        #region Constructor(s)
        public SubscribedSpaceQueryHandler(ISpaceReader spaceReader) {
            this.spaceReader = spaceReader;
        }
        #endregion

        #region Publics
        protected async override Task ExecuteQuery(ExecutionContext<SubscribedSpaceQuery> context) {
            var spaces = await spaceReader.FindSubscribed(context.Input.User);
            context.Output.Success(spaces);
        }
        #endregion
    }
}