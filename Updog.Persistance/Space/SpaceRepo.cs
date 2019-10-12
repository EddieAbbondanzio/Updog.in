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
        private ISpaceFactory factory;
        #endregion

        #region Constructor(s)
        public SpaceRepo(IDatabase database, ISpaceFactory factory) : base(database) {
            this.factory = factory;
        }
        #endregion

        #region Publics
        public override async Task<Space?> FindById(int id) => (await Connection.QueryAsync<SpaceRecord>(
            @"SELECT * FROM Space WHERE Space.Id = @Id",
            new { Id = id }
        )).Select(s => Map(s)).FirstOrDefault();

        public async Task<Space?> FindByName(string name) => (await Connection.QueryAsync<SpaceRecord>(
            @"SELECT * FROM Space LEFT JOIN ""User"" ON Space.UserId = ""User"".Id WHERE LOWER(Space.Name) = LOWER(@Name)",
            new { Name = name }
        )).Select(s => Map(s)).FirstOrDefault();

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

            return new PagedResultSet<Space>(spaces.Select(s => Map(s)), new PaginationInfo(paging.PageNumber, Math.Min(spaces.Count(), paging.PageSize), totalCount));
        }

        public async Task<IEnumerable<Space>> FindDefault() => (await Connection.QueryAsync<SpaceRecord>(
            @"SELECT * FROM Space WHERE IsDefault = TRUE"
        )).Select(s => Map(s));

        public async Task<IEnumerable<Space>> FindSubscribed(User user) => (await Connection.QueryAsync<SpaceRecord>(
                @"SELECT * FROM Space LEFT JOIN ""User"" ON Space.UserId = ""User"".Id LEFT JOIN Subscription ON Space.Id = Subscription.SpaceId WHERE Subscription.UserId = @Id",
                user
            )).Select(s => Map(s));

        public override async Task Add(Space entity) => entity.Id = await Connection.QueryFirstOrDefaultAsync<int>(
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
                        ) RETURNING Id;", Reverse(entity)
            );

        public override async Task Update(Space entity) => await Connection.ExecuteAsync(
                @"UPDATE Space SET 
                        Name = @Name,
                        Description = @Description,
                        CreationDate = @CreationDate,
                        SubscriptionCount = @SubscriptionCount,
                        UserId = @UserId
                    WHERE Id = @Id",
                Reverse(entity)
            );

        public override async Task Delete(Space entity) => await Connection.ExecuteAsync(
                @"DELETE FROM Space WHERE Id = @Id",
                Reverse(entity)
            );
        #endregion

        #region Privates
        private Space Map(SpaceRecord rec) => factory.Create(rec.Id, rec.UserId, rec.Name, rec.Description, rec.CreationDate, rec.SubscriptionCount, rec.IsDefault);

        private SpaceRecord Reverse(Space space) => new SpaceRecord() {
            Id = space.Id,
            UserId = space.UserId,
            Name = space.Name,
            Description = space.Description,
            CreationDate = space.CreationDate,
            SubscriptionCount = space.SuscriberCount,
            IsDefault = space.IsDefault
        };
        #endregion
    }
}