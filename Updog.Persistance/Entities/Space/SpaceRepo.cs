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
            this.mapper = new SpaceRecordMapper(new UserRecordMapper());
        }
        #endregion

        #region Publics
        /// <summary>
        /// Find a space by it's numeric ID.
        /// </summary>
        /// <param name="id">The ID to look for.</param>
        /// <returns>The space found (if any).</returns>
        public async Task<Space?> FindById(int id) {
            return (await Connection.QueryAsync<SpaceRecord, UserRecord, Space>(
                @"SELECT * FROM Space LEFT JOIN ""User"" ON Space.UserId = ""User"".Id WHERE Space.Id = @Id",
                (SpaceRecord s, UserRecord u) => mapper.Map(Tuple.Create(s, u)),
                new { Id = id }
            )).FirstOrDefault();
        }

        /// <summary>
        /// Find a space by it's unique name..
        /// </summary>
        /// <param name="name">The name of the space to look for.</param>
        /// <returns>The space found (if any).</returns>
        public async Task<Space?> FindByName(string name) {
            return (await Connection.QueryAsync<SpaceRecord, UserRecord, Space>(
                @"SELECT * FROM Space LEFT JOIN ""User"" ON Space.UserId = ""User"".Id WHERE LOWER(Space.Name) = LOWER(@Name)",
                (SpaceRecord s, UserRecord u) => mapper.Map(Tuple.Create(s, u)),
                new { Name = name }
            )).FirstOrDefault();
        }

        /// <summary>
        /// Get a list of spaces.
        /// </summary>
        /// <param name="pageNumber">The 0 based index of the page.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>The pages found.</returns>
        public async Task<PagedResultSet<Space>> Find(int pageNumber, int pageSize) {
            IEnumerable<Space> spaces = await Connection.QueryAsync<SpaceRecord, UserRecord, Space>(
                @"SELECT * FROM Space LEFT JOIN ""User"" ON Space.UserId = ""User"".Id LIMIT @Limit OFFSET @Offset",
                (SpaceRecord s, UserRecord u) => mapper.Map(Tuple.Create(s, u)),
                BuildPaginationParams(pageNumber, pageSize)
            );

            int totalCount = await Connection.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM Space"
            );

            return new PagedResultSet<Space>(spaces, new PaginationInfo(pageNumber, Math.Min(spaces.Count(), pageSize), totalCount));
        }

        /// <summary>
        /// Find all of the default spaces.
        /// </summary>
        /// <returns>The default spaces.</returns>
        public async Task<IEnumerable<Space>> FindDefault() {
            return await Connection.QueryAsync<SpaceRecord, UserRecord, Space>(
                @"SELECT * FROM Space LEFT JOIN ""User"" ON Space.UserId = ""User"".Id WHERE IsDefault = TRUE",
                (SpaceRecord s, UserRecord u) => mapper.Map(Tuple.Create(s, u))
            );

        }

        /// <summary>
        /// Add a new space to the database.
        /// </summary>
        /// <param name="entity">The space to add.</param>
        public async Task Add(Space entity) {
            SpaceRecord rec = mapper.Reverse(entity).Item1;

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
                mapper.Reverse(entity).Item1
            );
        }

        /// <summary>
        /// Delete an existing space from the database.
        /// </summary>
        /// <param name="entity">The space to delete.</param>
        public async Task Delete(Space entity) {
            await Connection.ExecuteAsync(
                @"DELETE FROM Space WHERE Id = @Id",
                mapper.Reverse(entity).Item1
            );
        }
        #endregion
    }
}