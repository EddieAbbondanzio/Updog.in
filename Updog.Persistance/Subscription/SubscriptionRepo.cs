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
        private ISubscriptionMapper mapper;
        #endregion

        #region Constructor(s)
        public SubscriptionRepo(IDatabase database, ISubscriptionMapper mapper) : base(database) {
            this.mapper = mapper;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Find a subscription by it's Id.
        /// </summary>
        /// <param name="id">The ID to look for.</param>
        /// <returns>The subscription found.</returns>
        public override async Task<Subscription?> FindById(int id) {
            var sub = (await Connection.QueryAsync<SubscriptionRecord>(
                @"SELECT * FROM Subscription WHERE Subscription.Id = @Id;",
                new { Id = id })).FirstOrDefault();

            return sub != null ? mapper.Map(sub) : null;
        }

        /// <summary>
        /// Find all subscriptions for a user.
        /// </summary>
        /// <param name="username">The user that they belong to..</param>
        /// <returns>The subscriptions found (if any)</returns>
        public async Task<IEnumerable<Subscription>> FindByUser(string username) {
            var subs = await Connection.QueryAsync<SubscriptionRecord>(
                @"SELECT Subscription.* FROM Subscription 
                    LEFT JOIN ""User"" u1 ON u1.Id = Subscription.UserId
                    WHERE u1.Username = @Username;",
                new { Username = username }
            );

            return subs.Select(s => mapper.Map(s));
        }

        /// <summary>
        /// Find a specific subscription by it's user (owner) and space.
        /// </summary>
        /// <param name="username">The username of the user to look for.</param>
        /// <param name="space">The space it's for.</param>
        /// <returns>The subscription found (if any).</returns>
        public async Task<Subscription?> FindByUserAndSpace(string username, string spaceName) {
            var sub = (await Connection.QueryAsync<SubscriptionRecord>(
                @"SELECT Subscription.* FROM Subscription 
                    LEFT JOIN ""User"" u1 ON u1.Id = Subscription.UserId
                    LEFT JOIN Space ON Space.Id = Subscription.SpaceId
                    WHERE u1.Username = @Username AND Space.Name = @Name;",
                new { Username = username, Name = spaceName }
            )).FirstOrDefault();

            return sub != null ? mapper.Map(sub) : null;
        }

        /// <summary>
        /// Add a new subscription to the database.
        /// </summary>
        /// <param name="entity">The subscription to add.</param>
        public override async Task Add(Subscription entity) {
            await Connection.ExecuteAsync(
                @"INSERT INTO Subscription 
                        (SpaceId, UserId) 
                        VALUES(@SpaceId, @UserId)",
                mapper.Reverse(entity)
            );
        }

        /// <summary>
        /// Update an existing subscription in the database.
        /// </summary>
        /// <param name="entity">The subscription to update.</param>
        public override async Task Update(Subscription entity) {
            await Connection.ExecuteAsync(
                @"UPDATE Subscription SET
                        SpaceId = @SpaceId,
                        UserId = @UserId,
                        SubscriptionCount = @SubscriptionCount
                        WHERE Id = @Id",
                mapper.Reverse(entity)
            );
        }

        /// <summary>
        /// Delete an existing subscription from the database.
        /// </summary>
        /// <param name="entity">The subscription to delete.</param>
        public override async Task Delete(Subscription entity) {
            await Connection.ExecuteAsync(
                @"DELETE FROM Subscription WHERE Id = @Id",
                mapper.Reverse(entity)
            );
        }
        #endregion
    }
}