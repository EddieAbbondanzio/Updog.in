using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Updog.Domain;

namespace Updog.Persistance {
    /// <summary>
    /// Reader to retrieve UserReadViews from the database.
    /// </summary>
    public sealed class UserReader : DatabaseReader<UserReadView>, IUserReader {
        #region Constructor(s)
        public UserReader(IDatabase database) : base(database) { }
        #endregion

        #region Publics
        public async Task<UserReadView?> FindById(int id) {
            var user = await Connection.QuerySingleOrDefaultAsync<UserRecord>(
                @"SELECT U.* FROM ""User"" U WHERE U.Id = @Id",
                new { Id = id }
            );

            return user != null ? Map(user) : null;
        }

        public async Task<UserReadView?> FindByComment(int commentId) {
            var user = await Connection.QuerySingleOrDefaultAsync<UserRecord>(
                @"SELECT U.* FROM ""User"" U
                    LEFT JOIN Comment ON Comment.UserId = U.Id
                    WHERE Comment.Id = @Id",
                new { Id = commentId }
            );

            return user != null ? Map(user) : null;
        }

        public async Task<UserReadView?> FindByPost(int postId) {
            var user = await Connection.QuerySingleOrDefaultAsync<UserRecord>(
                @"SELECT U.* FROM ""User"" U
                    LEFT JOIN Post ON Post.UserId = U.Id
                    WHERE Post.Id = @Id",
                new { Id = postId }
            );

            return user != null ? Map(user) : null;
        }

        public async Task<UserReadView?> FindByUsername(string username) {
            var user = await Connection.QuerySingleOrDefaultAsync<UserRecord>(
                @"SELECT U.* FROM ""User"" U 
                    WHERE Username = @Username",
                new { Username = username }
            );

            return user != null ? Map(user) : null;
        }

        public async Task<IEnumerable<UserReadView>> FindAdmins() {
            var admins = await Connection.QueryAsync<UserRecord>(
                @"SELECT U.* FROM ""User"" U
                JOIN Role ON Role.UserId = U.Id WHERE RoleType = @RoleType",
                new { RoleType = RoleType.Admin }
            );

            return admins.Select(a => Map(a));
        }

        public async Task<IEnumerable<UserReadView>> FindModerators(string space) {
            var admins = await Connection.QueryAsync<UserRecord>(
                @"SELECT U.* FROM ""User"" U
                JOIN Role ON Role.UserId = U.Id WHERE RoleType = @RoleType",
                new { RoleType = RoleType.Moderator }
            );

            return admins.Select(a => Map(a));
        }
        #endregion

        #region Privates
        private UserReadView Map(UserRecord source) => new UserReadView() {
            Id = source.Id,
            Username = source.Username,
            JoinedDate = source.JoinedDate,
            PostKarma = source.PostKarma,
            CommentKarma = source.CommentKarma
        };
        #endregion
    }
}