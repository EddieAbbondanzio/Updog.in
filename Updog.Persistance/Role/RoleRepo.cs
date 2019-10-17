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
        public async Task<Role?> FindAdminRole(User user) {
            var adminRole = await Connection.QueryFirstOrDefaultAsync<RoleRecord>(@"SELECT * FROM Role WHERE RoleType = @RoleType", new { RoleType = RoleType.Admin });
            return Map(adminRole);
        }

        public async override Task<Role?> FindById(int id) {
            var role = await Connection.QueryFirstOrDefaultAsync<RoleRecord>(@"SELECT * FROM Role WHERE Id = @Id", new { Id = id });
            return Map(role);
        }

        public async Task<Role?> FindModeratorRole(User user, string space) {
            var adminRole = await Connection.QueryFirstOrDefaultAsync<RoleRecord>(
                @"SELECT * FROM Role WHERE RoleType = @RoleType AND Domain = @Domain",
                new {
                    RoleType = RoleType.Admin,
                    Domain = space
                }
            );

            return Map(adminRole);
        }

        public async override Task Add(Role entity) => await Connection.ExecuteAsync(
            @"INSERT INTO Role(
                UserId,
                RoleType,
                Domain
                ) VALUES (
                @UserId,
                @RoleType,
                @Domain
                ) RETURNING Id;",
                Reverse(entity)
            );

        public async override Task Update(Role entity) => await Connection.ExecuteAsync("Update Role SET UserId = @UserId, RoleType = @RoleType, Domain = @Domain", Reverse(entity));

        public async override Task Delete(Role entity) => await Connection.ExecuteAsync("DELETE FROM Role WHERE Id = @Id", entity);
        #endregion

        #region Privates
        private Role? Map(RoleRecord? rec) => rec != null ? factory.Create(rec.Id, rec.UserId, rec.RoleType, rec.Domain) : null;

        private RoleRecord Reverse(Role role) => new RoleRecord() { Id = role.Id, UserId = role.UserId, RoleType = role.Type, Domain = role.Domain };
        #endregion
    }
}