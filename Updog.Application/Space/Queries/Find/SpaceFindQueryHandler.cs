using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain.Paging;
using Updog.Domain;

namespace Updog.Application {
    public sealed class SpaceFindQueryHandler : QueryHandler<SpaceFindQuery> {
        #region Fields
        private ISpaceReader spaceReader;
        #endregion

        #region Constructor(s)
        public SpaceFindQueryHandler(ISpaceReader spaceReader) {
            this.spaceReader = spaceReader;
        }
        #endregion

        #region Publics
        protected async override Task ExecuteQuery(ExecutionContext<SpaceFindQuery> context) {
            var spaces = await spaceReader.Find(context.Input.Paging);
            context.Output.Success(spaces);
        }
        #endregion
    }
}