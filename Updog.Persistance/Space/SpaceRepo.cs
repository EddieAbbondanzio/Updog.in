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
            @"SELECT * FROM space WHERE space.id = @Id",
            new { Id = id }
        )).Select(s => Map(s)).FirstOrDefault();

        public async Task<Space?> FindByName(string name) => (await Connection.QueryAsync<SpaceRecord>(
            @"SELECT * FROM space 
                LEFT JOIN ""user"" ON space.user_id = ""user"".id 
                WHERE LOWER(space.name) = LOWER(@Name)",
            new { Name = name }
        )).Select(s => Map(s)).FirstOrDefault();

        public async Task<PagedResultSet<Space>> Find(PaginationInfo paging) {
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

            return new PagedResultSet<Space>(spaces.Select(s => Map(s)), new PaginationInfo(paging.PageNumber, Math.Min(spaces.Count(), paging.PageSize), totalCount));
        }

        public async Task<IEnumerable<Space>> FindDefault() => (await Connection.QueryAsync<SpaceRecord>(
            @"SELECT * FROM space WHERE is_default = TRUE"
        )).Select(s => Map(s));

        public async Task<IEnumerable<Space>> FindSubscribed(User user) => (await Connection.QueryAsync<SpaceRecord>(
                @"SELECT * FROM space 
                    LEFT JOIN ""user"" ON space.user_id = ""user"".id 
                    LEFT JOIN subscription ON space.id = subscription.space_id 
                    WHERE subscription.user_id = @Id",
                user
            )).Select(s => Map(s));

        public async Task<Space?> FindByComment(int commentId) => (await Connection.QueryAsync<SpaceRecord>(
            @"SELECT s.* FROM space
                JOIN post p ON p.space_id = s.id
                JOIN comment c ON s.post_id = p.id
                WHERE c.id = @Id",
                new { Id = commentId }
        )).Select(s => Map(s)).FirstOrDefault();

        public async Task<Space?> FindByPost(int postId) => (await Connection.QueryAsync<SpaceRecord>(
            @"SELECT s.* FROM space
                JOIN post p ON s.id = p.space_id
                WHERE p.id = @Id",
                new { Id = postId }
        )).Select(s => Map(s)).FirstOrDefault();

        public override async Task Add(Space entity) => entity.Id = await Connection.QueryFirstOrDefaultAsync<int>(
                @"INSERT INTO space(
                        name,
                        description,
                        creation_date,
                        subscription_count,
                        user_id,
                        is_default
                        ) VALUES (
                        @Name,
                        @Description,
                        @CreationDate,
                        @SubscriptionCount,
                        @UserId,
                        @IsDefault
                        ) RETURNING id;", Reverse(entity)
            );

        public override async Task Update(Space entity) => await Connection.ExecuteAsync(
                @"UPDATE Space SET 
                        name = @Name,
                        description = @Description,
                        creation_date = @CreationDate,
                        subscription_count = @SubscriptionCount,
                        user_id = @UserId
                    WHERE id = @Id",
                Reverse(entity)
            );

        public override async Task Delete(Space entity) => await Connection.ExecuteAsync(
                @"DELETE FROM space WHERE id = @Id",
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