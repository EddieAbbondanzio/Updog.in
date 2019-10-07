using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Updog.Application;
using Updog.Domain.Paging;
using Updog.Domain;

namespace Updog.Persistance {
    /// <summary>
    /// CRUD interface for managing spaces in the database.
    /// </summary>
    public sealed class SpaceRepo : DatabaseRepo<Space>, ISpaceRepo {
        #region Fields
        private ISpaceMapper mapper;
        #endregion

        #region Constructor(s)
        public SpaceRepo(IDatabase database, ISpaceMapper mapper) : base(database) {
            this.mapper = mapper;
        }
        #endregion

        #region Publics
        public override async Task<Space?> FindById(int id) {
            var space = (await Connection.QueryAsync<SpaceRecord>(
                @"SELECT * FROM Space WHERE Space.Id = @Id",
                new { Id = id }
            )).FirstOrDefault();

            return space != null ? mapper.Map(space) : null;
        }

        public async Task<Space?> FindByName(string name) {
            var space = (await Connection.QueryAsync<SpaceRecord>(
                @"SELECT * FROM Space LEFT JOIN ""User"" ON Space.UserId = ""User"".Id WHERE LOWER(Space.Name) = LOWER(@Name)",
                new { Name = name }
            )).FirstOrDefault();

            return space != null ? mapper.Map(space) : null;
        }

        public async Task<PagedResultSet<Space>> Find(PaginationInfo paging) {
            var spaces = await Connection.QueryAsync<SpaceRecord>(
                @"SELECT * FROM Space LIMIT @Limit OFFSET @Offset",
                new {
                    Limit = paging.PageSize,
                    Offset = paging.Offset
                }
            );

            int totalCount = await Connection.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM Space"
            );

            return new PagedResultSet<Space>(spaces.Select(s => mapper.Map(s)), new PaginationInfo(paging.PageNumber, Math.Min(spaces.Count(), paging.PageSize), totalCount));
        }

        public async Task<IEnumerable<Space>> FindDefault() {
            var defaults = await Connection.QueryAsync<SpaceRecord>(
                @"SELECT * FROM Space WHERE IsDefault = TRUE"
            );

            return defaults.Select(s => mapper.Map(s));
        }

        public async Task<IEnumerable<Space>> FindSubscribed(User user) {
            var subscribes = await Connection.QueryAsync<SpaceRecord>(
                @"SELECT * FROM Space LEFT JOIN ""User"" ON Space.UserId = ""User"".Id LEFT JOIN Subscription ON Space.Id = Subscription.SpaceId WHERE Subscription.UserId = @Id",
                user
            );

            return subscribes.Select(s => mapper.Map(s));
        }

        public override async Task Add(Space entity) {
            SpaceRecord rec = mapper.Reverse(entity);

            entity.Id = await Connection.QueryFirstOrDefaultAsync<int>(
                @"INSERT INTO Space(
                        Name,
                        Description,
                        CreationDate,
                        SubscriptionCount,
                        UserId,
                        IsDefault
                        ) VALUES (
                        @Name,
                        @Description,
                        @CreationDate,
                        @SubscriptionCount,
                        @UserId,
                        @IsDefault
                        ) RETURNING Id;", rec
            );
        }

        public override async Task Update(Space entity) {
            await Connection.ExecuteAsync(
                @"UPDATE Space SET 
                        Name = @Name,
                        Description = @Description,
                        CreationDate = @CreationDate,
                        SubscriptionCount = @SubscriptionCount,
                        UserId = @UserId
                    WHERE Id = @Id",
                mapper.Reverse(entity)
            );
        }

        public override async Task Delete(Space entity) {
            await Connection.ExecuteAsync(
                @"DELETE FROM Space WHERE Id = @Id",
                mapper.Reverse(entity)
            );
        }
        #endregion
    }
}