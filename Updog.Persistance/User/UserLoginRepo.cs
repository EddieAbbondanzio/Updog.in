using System.Threading.Tasks;
using Dapper;
using Updog.Domain;

namespace Updog.Persistance {
    public sealed class UserLoginRepo : DatabaseRepo<UserLogin>, IUserLoginRepo {
        #region Fields
        private IUserLoginFactory factory;
        #endregion

        #region Constructor(s)
        public UserLoginRepo(IDatabase database, IUserLoginFactory factory) : base(database) {
            this.factory = factory;
        }
        #endregion

        #region Publics
        public async override Task<UserLogin?> FindById(int id) {
            var rec = await Connection.QueryFirstOrDefaultAsync(
                @"SELECT * FROM user-login WHERE id = @Id",
                new { Id = id }
            );

            return rec != null ? Map(rec) : null;
        }

        public async override Task Add(UserLogin entity) => entity.Id = await Connection.QueryFirstOrDefaultAsync<int>(
            @"INSERT INTO user-login (user-id) 
            VALUES (@Id) RETURNING Id;", Reverse(entity));

#pragma warning disable 1998
        public async override Task Update(UserLogin entity) {
            throw new System.NotImplementedException();
        }
#pragma warning restore 1998

        public async override Task Delete(UserLogin entity) => await Connection.ExecuteAsync(
            @"DELETE FROM user-login WHERE id = @Id",
            Reverse(entity)
        );
        #endregion

        #region Privates
        private UserLogin Map(UserLoginRecord rec) => factory.Create(rec.Id, rec.UserId);

        private UserLoginRecord Reverse(UserLogin login) => new UserLoginRecord() {
            Id = login.Id,
            UserId = login.UserId,
        };
        #endregion
    }
}