using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Updog.Domain;
using Updog.Domain.Paging;

namespace Updog.Persistance {
    public sealed class SpaceReader : DatabaseReader<SpaceReadView>, ISpaceReader {
        #region Constructor(s)
        public SpaceReader(IDatabase database) : base(database) { }
        #endregion

        #region Publics
        public async Task<PagedResultSet<SpaceReadView>> Find(PaginationInfo paging) {
            var spaces = await Connection.QueryAsync<SpaceRecord>(
                @"SELECT * FROM space LIMIT @Limit OFFSET @Offset",
                new {
                    Limit = paging.PageSize,
                    Offset = paging.Offset
                }
            );

            int totalCount = await Connection.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM space"
            );

            return new PagedResultSet<SpaceReadView>(spaces.Select(s => Map(s)), new PaginationInfo(paging.PageNumber, Math.Min(spaces.Count(), paging.PageSize), totalCount));
        }


        public async Task<SpaceReadView?> FindById(int id) {
            var space = (await Connection.QueryFirstOrDefaultAsync<SpaceRecord>(
                @"SELECT * FROM space WHERE space.id = @Id",
                new { Id = id }
            ));

            if (space == null) {
                return null;
            }

            SpaceReadView view = Map(space);

            IUserReader userReader = GetReader<IUserReader>();
            view.User = (await userReader.FindById(space.UserId))!;

            return view;
        }

        public async Task<SpaceReadView?> FindByName(string name) {
            var space = (await Connection.QueryFirstOrDefaultAsync<SpaceRecord>(
                @"SELECT * FROM space WHERE LOWER(space.name) = LOWER(@Name)",
                new { Name = name }
            ));

            if (space == null) {
                return null;
            }

            SpaceReadView view = Map(space);

            IUserReader userReader = GetReader<IUserReader>();
            view.User = (await userReader.FindById(space.UserId))!;

            return view;
        }

        public async Task<IEnumerable<SpaceReadView>> FindDefault() {
            var defaults = await Connection.QueryAsync<SpaceRecord>(
                @"SELECT * FROM space WHERE is_default = TRUE"
            );

            var views = defaults.Select(s => Map(s)).ToArray();
            IUserReader userReader = GetReader<IUserReader>();

            for (int i = 0; i < views.Length; i++) {
                views[i].User = (await userReader.FindById(defaults.ElementAt(i).UserId))!;
            }

            return views;
        }

        public async Task<IEnumerable<SpaceReadView>> FindSubscribed(User user) {
            var subscribes = await Connection.QueryAsync<SpaceRecord>(
                @"SELECT * FROM space 
                    LEFT JOIN ""user"" ON space.user_id = ""user"".id 
                    LEFT JOIN subscription ON space.id = subscription.space_id 
                    WHERE subscription.user_id = @Id",
                user
            );

            var views = subscribes.Select(s => Map(s)).ToArray();
            IUserReader userReader = GetReader<IUserReader>();

            for (int i = 0; i < views.Length; i++) {
                views[i].User = (await userReader.FindById(subscribes.ElementAt(i).UserId))!;
            }

            return views;
        }

        public async Task<IEnumerable<SpaceReadView>> FindSpacesUserModerates(string username) {
            var spaces = await Connection.QueryAsync<SpaceRecord>(
                @"SELECT space.* FROM space
                JOIN role ON role.domain = space.name
                JOIN ""user"" U ON role.user_id = U.id 
                WHERE U.username = @Username",
                new { Username = username }
            );

            return spaces.Select(s => Map(s));
        }
        #endregion

        #region Privates
        private SpaceReadView Map(SpaceRecord source) => new SpaceReadView() {
            Id = source.Id,
            Name = source.Name,
            Description = source.Description,
            CreationDate = source.CreationDate,
            SubscriberCount = source.SubscriptionCount,
            IsDefault = source.IsDefault
        };
        #endregion
    }
}