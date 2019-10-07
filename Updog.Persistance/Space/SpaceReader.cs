using System.Threading.Tasks;
using Dapper;
using Updog.Domain;

namespace Updog.Persistance {
    public sealed class SpaceReader : DatabaseReader<SpaceReadView>, ISpaceReader {
        #region Fields
        private ISpaceReadViewMapper mapper;
        #endregion

        #region Constructor(s)
        public SpaceReader(IDatabase database, ISpaceReadViewMapper mapper) : base(database) {
            this.mapper = mapper;
        }
        #endregion

        #region Publics

        public async Task<SpaceReadView?> FindById(int id) {
            var space = (await Connection.QueryFirstOrDefaultAsync<SpaceRecord>(
                @"SELECT * FROM Space WHERE Space.Id = @ID",
                new { Id = id }
            ));

            if (space == null) {
                return null;
            }

            SpaceReadView view = mapper.Map(space);

            IUserReader userReader = GetReader<IUserReader>();
            view.User = (await userReader.FindById(space.UserId))!;

            return view;
        }

        public async Task<SpaceReadView?> FindByName(string name) {
            var space = (await Connection.QueryFirstOrDefaultAsync<SpaceRecord>(
                @"SELECT * FROM Space WHERE LOWER(Space.Name) = LOWER(@Name)",
                new { Name = name }
            ));

            if (space == null) {
                return null;
            }

            SpaceReadView view = mapper.Map(space);

            IUserReader userReader = GetReader<IUserReader>();
            view.User = (await userReader.FindById(space.UserId))!;

            return view;
        }
        #endregion
    }
}