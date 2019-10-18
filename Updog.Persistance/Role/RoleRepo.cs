using System.Threading.Tasks;
using Dapper;
using Updog.Domain;

namespace Updog.Persistance {
    public sealed class RoleRepo : DatabaseRepo<Role>, IRoleRepo {
        #region Fields
        private IRoleFactory factory;
        #endregion

        #region Constructor(s)
        public RoleRepo(IDatabase database, IRoleFactory factory) : base(database) {
            this.factory = factory;
        }
        #endregion

        #region Publics
        public async override Task<Role?> FindById(int id) {
            var role = await Connection.QueryFirstOrDefaultAsync<RoleRecord>(
                @"SELECT * FROM role r WHERE r.id = @Id",
                new { Id = id }
            );

            return Map(role);
        }

        public async Task<Role?> FindAdminRole(User user) {
            var adminRole = await Connection.QueryFirstOrDefaultAsync<RoleRecord>(
                @"SELECT * FROM role r
                    JOIN ""user"" u on u.id = r.user_id
                    WHERE r.role_type = @RoleType AND u.username = @Username",
                new { RoleType = RoleType.Admin }
            );

            return Map(adminRole);
        }

        public async Task<Role?> FindModeratorRole(User user, string space) {
            var adminRole = await Connection.QueryFirstOrDefaultAsync<RoleRecord>(
                @"SELECT * FROM role 
                    JOIN ""user"" u on u.id = r.user_id
                    WHERE r.role_type = @RoleType AND r.domain = @Domain",
                new {
                    RoleType = RoleType.Admin,
                    Domain = space
                }
            );

            return Map(adminRole);
        }

        public async override Task Add(Role entity) => await Connection.ExecuteAsync(
            @"INSERT INTO role(
                user_id,
                role_type,
                domain
                ) VALUES (
                @UserId,
                @RoleType,
                @Domain
                ) RETURNING Id;",
                Reverse(entity)
            );

        public async override Task Update(Role entity) => await Connection.ExecuteAsync(
            @"Update role 
                SET user_id = @UserId, role_type = @RoleType, domain = @Domain
                WHERE id = @Id",
            Reverse(entity)
        );

        public async override Task Delete(Role entity) => await Connection.ExecuteAsync(
            @"DELETE FROM role WHERE id = @Id",
            entity
        );
        #endregion

        #region Privates
        private Role? Map(RoleRecord? rec) => rec != null ? factory.Create(rec.Id, rec.UserId, rec.RoleType, rec.Domain) : null;

        private RoleRecord Reverse(Role role) => new RoleRecord() { Id = role.Id, UserId = role.UserId, RoleType = role.Type, Domain = role.Domain };

        public async Task<bool> IsUserAdmin(string username) => await Connection.ExecuteScalarAsync<bool>(
            @"SELECT COUNT(*) FROM role r
                JOIN ""user"" u on u.id = r.user_id
                WHERE r.role_type = @RoleType AND u.username = @Username",
            new { RoleType = RoleType.Admin }
        );

        public async Task<bool> IsUserModerator(string username, string space) => await Connection.ExecuteScalarAsync<bool>(
            @"SELECT COUNT(*) FROM role 
                JOIN ""user"" u on u.id = r.user_id
                WHERE r.role_type = @RoleType AND r.domain = @Domain",
            new {
                RoleType = RoleType.Admin,
                Domain = space
            }
        );
        #endregion
    }
}