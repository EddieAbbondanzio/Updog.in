using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Updog.Application;
using Updog.Application.Paging;
using Updog.Domain;

namespace Updog.Persistance {
    /// <summary>
    /// CRUD interface for managing spaces in the database.
    /// </summary>
    public sealed class SpaceRepo : DatabaseRepo<Space>, ISpaceRepo {
        #region Fields
        private ISpaceRecordMapper mapper;
        #endregion

        #region Constructor(s)
        public SpaceRepo(IDatabase database, ISpaceRecordMapper mapper) : base(database) {
            this.mapper = mapper;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Find a space by it's numeric ID.
        /// </summary>
        /// <param name="id">The ID to look for.</param>
        /// <returns>The space found (if any).</returns>
        public async Task<Space> FindById(int id) {
            using (DbConnection connection = GetConnection()) {
                return (await connection.QueryAsync<SpaceRecord, UserRecord, Space>(
                    @"SELECT * FROM Space LEFT JOIN ""User"" ON Space.UserId = ""User"".Id WHERE Space.Id = @Id",
                    (SpaceRecord s, UserRecord u) => mapper.Map(Tuple.Create(s, u)),
                    new { Id = id }
                )).FirstOrDefault();
            }
        }

        /// <summary>
        /// Find a space by it's unique name..
        /// </summary>
        /// <param name="name">The name of the space to look for.</param>
        /// <returns>The space found (if any).</returns>
        public async Task<Space> FindByName(string name) {
            using (DbConnection connection = GetConnection()) {
                return (await connection.QueryAsync<SpaceRecord, UserRecord, Space>(
                    @"SELECT * FROM Space LEFT JOIN ""User"" ON Space.UserId = ""User"".Id WHERE Space.Name = @Name",
                    (SpaceRecord s, UserRecord u) => mapper.Map(Tuple.Create(s, u)),
                    new { Name = name }
                )).FirstOrDefault();
            }
        }

        /// <summary>
        /// Get a list of spaces.
        /// </summary>
        /// <param name="pageNumber">The 0 based index of the page.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>The pages found.</returns>
        public async Task<PagedResultSet<Space>> Find(int pageNumber, int pageSize) {
            using (DbConnection connection = GetConnection()) {
                IEnumerable<Space> spaces = await connection.QueryAsync<SpaceRecord, UserRecord, Space>(
                    @"SELECT * FROM Space LEFT JOIN ""User"" ON Space.UserId = ""User"".Id LIMIT @Limit OFFSET @Offset",
                    (SpaceRecord s, UserRecord u) => mapper.Map(Tuple.Create(s, u)),
                    BuildPaginationParams(pageNumber, pageSize)
                );

                int totalCount = await connection.ExecuteScalarAsync<int>(
                    "SELECT COUNT(*) FROM Space"
                );

                return new PagedResultSet<Space>(spaces, new PaginationInfo(pageNumber, Math.Min(spaces.Count(), pageSize), totalCount));
            }
        }

        /// <summary>
        /// Add a new space to the database.
        /// </summary>
        /// <param name="entity">The space to add.</param>
        public async Task Add(Space entity) {
            SpaceRecord rec = mapper.Reverse(entity).Item1;

            using (DbConnection connection = GetConnection()) {
                entity.Id = await connection.QueryFirstOrDefaultAsync<int>(
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
        }

        /// <summary>
        /// Update an existing space in the database.
        /// </summary>
        /// <param name="entity">The space to update.</param>
        public async Task Update(Space entity) {
            using (DbConnection connection = GetConnection()) {
                await connection.ExecuteAsync(
                    @"UPDATE Space SET 
                        Name = @Name,
                        Description = @Description,
                        CreationDate = @CreationDate,
                        SubscriptionCount = @SubscriptionCount,
                        UserId = @UserId,
                    WHERE Id = @Id",
                    mapper.Reverse(entity).Item1
                );
            }
        }

        /// <summary>
        /// Delete an existing space from the database.
        /// </summary>
        /// <param name="entity">The space to delete.</param>
        public async Task Delete(Space entity) {
            using (DbConnection connection = GetConnection()) {
                await connection.ExecuteAsync(
                    @"DELETE FROM Space WHERE Id = @Id",
                    mapper.Reverse(entity).Item1
                );
            }
        }
        #endregion
    }
}