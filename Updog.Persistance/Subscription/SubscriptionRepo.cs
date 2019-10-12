using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Updog.Application;
using Updog.Domain;

namespace Updog.Persistance {
    /// <summary>
    /// CRUD interface to manage subscriptions in the database.
    /// </summary>
    public sealed class SubscriptionRepo : DatabaseRepo<Subscription>, ISubscriptionRepo {
        #region Fields
        private ISubscriptionFactory factory;
        #endregion

        #region Constructor(s)
        public SubscriptionRepo(IDatabase database, ISubscriptionFactory factory) : base(database) {
            this.factory = factory;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Find a subscription by it's Id.
        /// </summary>
        /// <param name="id">The ID to look for.</param>
        /// <returns>The subscription found.</returns>
        public override async Task<Subscription?> FindById(int id) => (await Connection.QueryAsync<SubscriptionRecord>(
                @"SELECT * FROM Subscription WHERE Subscription.Id = @Id;",
                new { Id = id })).Select(s => Map(s)).FirstOrDefault();

        /// <summary>
        /// Find all subscriptions for a user.
        /// </summary>
        /// <param name="username">The user that they belong to..</param>
        /// <returns>The subscriptions found (if any)</returns>
        public async Task<IEnumerable<Subscription>> FindByUser(string username) => (await Connection.QueryAsync<SubscriptionRecord>(
                @"SELECT Subscription.* FROM Subscription 
                    LEFT JOIN ""User"" u1 ON u1.Id = Subscription.UserId
                    WHERE u1.Username = @Username;",
                new { Username = username }
            )).Select(s => Map(s));

        /// <summary>
        /// Find a specific subscription by it's user (owner) and space.
        /// </summary>
        /// <param name="username">The username of the user to look for.</param>
        /// <param name="space">The space it's for.</param>
        /// <returns>The subscription found (if any).</returns>
        public async Task<Subscription?> FindByUserAndSpace(string username, string spaceName) => (await Connection.QueryAsync<SubscriptionRecord>(
                @"SELECT Subscription.* FROM Subscription 
                    LEFT JOIN ""User"" u1 ON u1.Id = Subscription.UserId
                    LEFT JOIN Space ON Space.Id = Subscription.SpaceId
                    WHERE u1.Username = @Username AND Space.Name = @Name;",
                new { Username = username, Name = spaceName }
            )).Select(s => Map(s)).FirstOrDefault();

        /// <summary>
        /// Add a new subscription to the database.
        /// </summary>
        /// <param name="entity">The subscription to add.</param>
        public override async Task Add(Subscription entity) => await Connection.ExecuteAsync(
                @"INSERT INTO Subscription 
                        (SpaceId, UserId) 
                        VALUES(@SpaceId, @UserId)",
                Reverse(entity)
            );

        /// <summary>
        /// Update an existing subscription in the database.
        /// </summary>
        /// <param name="entity">The subscription to update.</param>
        public override async Task Update(Subscription entity) => await Connection.ExecuteAsync(
                @"UPDATE Subscription SET
                        SpaceId = @SpaceId,
                        UserId = @UserId,
                        SubscriptionCount = @SubscriptionCount
                        WHERE Id = @Id",
                Reverse(entity)
            );

        /// <summary>
        /// Delete an existing subscription from the database.
        /// </summary>
        /// <param name="entity">The subscription to delete.</param>
        public override async Task Delete(Subscription entity) => await Connection.ExecuteAsync(
                @"DELETE FROM Subscription WHERE Id = @Id",
                Reverse(entity)
            );
        #endregion

        #region Privates
        private Subscription Map(SubscriptionRecord rec) => factory.Create(rec.Id, rec.UserId, rec.SpaceId);

        private SubscriptionRecord Reverse(Subscription s) => new SubscriptionRecord() { Id = s.Id, UserId = s.UserId, SpaceId = s.SpaceId };
        #endregion
    }
}