using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class DefaultSpaceQueryHandler : QueryHandler<DefaultSpaceQuery> {
        #region Fields
        private ISpaceReader spaceReader;
        #endregion

        #region Constructor(s)
        public DefaultSpaceQueryHandler(ISpaceReader spaceReader) {
            this.spaceReader = spaceReader;
        }
        #endregion

        #region Publics
        protected async override Task ExecuteQuery(ExecutionContext<DefaultSpaceQuery> context) {
            var spaces = await spaceReader.FindDefault();
            context.Output.Success(spaces);
        }
        #endregion
    }
}