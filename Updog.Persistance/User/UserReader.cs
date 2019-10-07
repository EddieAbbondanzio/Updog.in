using System.Threading.Tasks;
using Dapper;
using Updog.Domain;

namespace Updog.Persistance {
    /// <summary>
    /// Reader to retrieve UserReadViews from the database.
    /// </summary>
    public sealed class UserReader : DatabaseReader<UserReadView>, IUserReader {
        #region Fields
        private IUserReadViewMapper mapper;
        #endregion

        #region Constructor(s)
        public UserReader(IDatabase database, IUserReadViewMapper mapper) : base(database) {
            this.mapper = mapper;
        }
        #endregion

        #region Publics
        public async Task<UserReadView?> FindById(int id) {
            var user = await Connection.QuerySingleOrDefaultAsync<UserRecord>(
                @"SELECT U.* FROM ""User"" U WHERE U.Id = @Id",
                new { Id = id }
            );

            return user != null ? mapper.Map(user) : null;
        }

        public async Task<UserReadView?> FindByComment(int commentId) {
            var user = await Connection.QuerySingleOrDefaultAsync<UserRecord>(
                @"SELECT U.* FROM ""User"" U
                    LEFT JOIN Comment ON Comment.UserId = U.Id
                    WHERE Comment.Id = @Id",
                new { Id = commentId }
            );

            return user != null ? mapper.Map(user) : null;
        }

        public async Task<UserReadView?> FindByPost(int postId) {
            var user = await Connection.QuerySingleOrDefaultAsync<UserRecord>(
                @"SELECT U.* FROM ""User"" U
                    LEFT JOIN Post ON Post.UserId = U.Id
                    WHERE Post.Id = @Id",
                new { Id = postId }
            );

            return user != null ? mapper.Map(user) : null;
        }

        public async Task<UserReadView?> FindByUsername(string username) {
            var user = await Connection.QuerySingleOrDefaultAsync<UserRecord>(
                @"SELECT U.* FROM ""User"" U 
                    WHERE Username = @Username",
                new { Username = username }
            );

            return user != null ? mapper.Map(user) : null;
        }
        #endregion
    }
}