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
    public sealed class SpaceRepo : DapperRepo<Space>, ISpaceRepo {
        #region Fields
        private ISpaceRecordMapper mapper;
        #endregion

        #region Constructor(s)
        public SpaceRepo(DatabaseContext context) : base(context) {
            this.mapper = new SpaceRecordMapper();
        }
        #endregion

        #region Publics
        /// <summary>
        /// Find a space by it's numeric ID.
        /// </summary>
        /// <param name="id">The ID to look for.</param>
        /// <returns>The space found (if any).</returns>
        public async Task<Space?> FindById(int id) {
            var space = (await Connection.QueryAsync<SpaceRecord>(
                @"SELECT * FROM Space WHERE Space.Id = @Id",
                new { Id = id }
            )).FirstOrDefault();

            return space != null ? mapper.Map(space) : null;
        }

        /// <summary>
        /// Find a space by it's unique name..
        /// </summary>
        /// <param name="name">The name of the space to look for.</param>
        /// <returns>The space found (if any).</returns>
        public async Task<Space?> FindByName(string name) {
            var space = (await Connection.QueryAsync<SpaceRecord>(
                @"SELECT * FROM Space LEFT JOIN ""User"" ON Space.UserId = ""User"".Id WHERE LOWER(Space.Name) = LOWER(@Name)",
                new { Name = name }
            )).FirstOrDefault();

            return space != null ? mapper.Map(space) : null;
        }

        /// <summary>
        /// Get a list of spaces.
        /// </summary>
        /// <param name="pageNumber">The 0 based index of the page.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>The pages found.</returns>
        public async Task<PagedResultSet<Space>> Find(int pageNumber, int pageSize) {
            var spaces = await Connection.QueryAsync<SpaceRecord>(
                @"SELECT * FROM Space LIMIT @Limit OFFSET @Offset",
                BuildPaginationParams(pageNumber, pageSize)
            );

            int totalCount = await Connection.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM Space"
            );

            return new PagedResultSet<Space>(spaces.Select(s => mapper.Map(s)), new PaginationInfo(pageNumber, Math.Min(spaces.Count(), pageSize), totalCount));
        }

        /// <summary>
        /// Find all of the default spaces.
        /// </summary>
        /// <returns>The default spaces.</returns>
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

        /// <summary>
        /// Add a new space to the database.
        /// </summary>
        /// <param name="entity">The space to add.</param>
        public async Task Add(Space entity) {
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

        /// <summary>
        /// Update an existing space in the database.
        /// </summary>
        /// <param name="entity">The space to update.</param>
        public async Task Update(Space entity) {
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

        /// <summary>
        /// Delete an existing space from the database.
        /// </summary>
        /// <param name="entity">The space to delete.</param>
        public async Task Delete(Space entity) {
            await Connection.ExecuteAsync(
                @"DELETE FROM Space WHERE Id = @Id",
                mapper.Reverse(entity)
            );
        }
        #endregion
    }
}