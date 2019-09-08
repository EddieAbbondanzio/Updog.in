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
        private ISubscriptionRecordMapper _mapper;
        #endregion

        #region Constructor(s)
        public SubscriptionRepo(DbConnection connection) : base(connection) {
            _mapper = new SubscriptionRecordMapper(new SpaceRecordMapper(new UserRecordMapper()), new UserRecordMapper());
        }
        #endregion

        #region Publics
        /// <summary>
        /// Find a subscription by it's Id.
        /// </summary>
        /// <param name="id">The ID to look for.</param>
        /// <returns>The subscription found.</returns>
        public async Task<Subscription?> FindById(int id) {
            return (await Connection.QueryAsync<SubscriptionRecord, UserRecord, SpaceRecord, UserRecord, Subscription>(
                @"SELECT * FROM Subscription 
                    LEFT JOIN ""User"" u1 ON u1.Id = Subscription.UserId
                    LEFT JOIN Subscription ON Space.Id = Subscription.SpaceId
                    LEFT JOIN ""User"" u2 ON u2.Id = Space.UserId
                    WHERE Subscription.Id = @Id;",
                (SubscriptionRecord subRec, UserRecord userRec, SpaceRecord spaceRec, UserRecord spaceOwner) => {
                    return _mapper.Map(Tuple.Create(subRec, userRec, Tuple.Create(spaceRec, spaceOwner)));
                },
                new { Id = id }
            )).FirstOrDefault();
        }

        /// <summary>
        /// Find all subscriptions for a user.
        /// </summary>
        /// <param name="username">The user that they belong to..</param>
        /// <returns>The subscriptions found (if any)</returns>
        public async Task<IEnumerable<Subscription>> FindByUser(string username) {
            return await Connection.QueryAsync<SubscriptionRecord, UserRecord, SpaceRecord, UserRecord, Subscription>(
                @"SELECT * FROM Subscription 
                    LEFT JOIN ""User"" u1 ON u1.Id = Subscription.UserId
                    LEFT JOIN Subscription ON Space.Id = Subscription.SpaceId
                    LEFT JOIN ""User"" u2 ON u2.Id = Space.UserId
                    WHERE u1.Username = @Username;",
                (SubscriptionRecord subRec, UserRecord userRec, SpaceRecord spaceRec, UserRecord spaceOwner) => {
                    return _mapper.Map(Tuple.Create(subRec, userRec, Tuple.Create(spaceRec, spaceOwner)));
                },
                new { Username = username }
            );
        }

        /// <summary>
        /// Find a specific subscription by it's user (owner) and space.
        /// </summary>
        /// <param name="username">The username of the user to look for.</param>
        /// <param name="space">The space it's for.</param>
        /// <returns>The subscription found (if any).</returns>
        public async Task<Subscription?> FindByUserAndSpace(string username, string spaceName) {
            return (await Connection.QueryAsync<SubscriptionRecord, UserRecord, SpaceRecord, UserRecord, Subscription>(
                @"SELECT * FROM Subscription 
                    LEFT JOIN ""User"" u1 ON u1.Id = Subscription.UserId
                    LEFT JOIN Subscription ON Space.Id = Subscription.SpaceId
                    LEFT JOIN ""User"" u2 ON u2.Id = Space.UserId
                    WHERE u1.Username = @Username AND Space.Name = @Name;",
                (SubscriptionRecord subRec, UserRecord userRec, SpaceRecord spaceRec, UserRecord spaceOwner) => {
                    return _mapper.Map(Tuple.Create(subRec, userRec, Tuple.Create(spaceRec, spaceOwner)));
                },
                new { Username = username, Name = spaceName }
            )).FirstOrDefault();
        }

        /// <summary>
        /// Add a new subscription to the database.
        /// </summary>
        /// <param name="entity">The subscription to add.</param>
        public async Task Add(Subscription entity) {
            await Connection.ExecuteAsync(
                @"INSERT INTO Subscription 
                        (Id, SpaceId, UserId) 
                        VALUES(@Id, @SpaceId, @UserId)",
                _mapper.Reverse(entity).Item1
            );
        }

        /// <summary>
        /// Update an existing subscription in the database.
        /// </summary>
        /// <param name="entity">The subscription to update.</param>
        public async Task Update(Subscription entity) {
            await Connection.ExecuteAsync(
                @"UPDATE Subscription SET
                        Id = @Id,
                        SpaceId = @SpaceId,
                        UserId = @UserId
                        WHERE Id = @Id",
                _mapper.Reverse(entity).Item1
            );
        }

        /// <summary>
        /// Delete an existing subscription from the database.
        /// </summary>
        /// <param name="entity">The subscription to delete.</param>
        public async Task Delete(Subscription entity) {
            await Connection.ExecuteAsync(
                @"DELETE FROM Subscription WHERE Id = @Id",
                _mapper.Reverse(entity).Item1
            );
        }
        #endregion
    }
}