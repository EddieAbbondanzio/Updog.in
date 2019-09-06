using System;
using Updog.Domain;

namespace Updog.Persistance {
    /// <summary>
    /// Mapper to convert the subscription record to it's entity.
    /// </summary>
    public sealed class SubscriptionRecordMapper : ISubscriptionRecordMapper {
        #region Fields
        /// <summary>
        /// Helper mapper to map the space record.
        /// </summary>
        private ISpaceRecordMapper _spaceRecordMapper;

        /// <summary>
        /// Helper mapper to map the user record.
        /// </summary>
        private IUserRecordMapper _userRecordMapper;
        #endregion

        #region Constructor(s)
        public SubscriptionRecordMapper(ISpaceRecordMapper spaceRecordMapper, IUserRecordMapper userRecordMapper) {
            _spaceRecordMapper = spaceRecordMapper;
            _userRecordMapper = userRecordMapper;
        }
        #endregion

        #region Publics
        public Subscription Map(Tuple<SubscriptionRecord, UserRecord, Tuple<SpaceRecord, UserRecord>> source) {
            return new Subscription() {
                Id = source.Item1.Id,
                User = _userRecordMapper.Map(source.Item2),
                Space = _spaceRecordMapper.Map(source.Item3)
            };
        }

        public Tuple<SubscriptionRecord, UserRecord, Tuple<SpaceRecord, UserRecord>> Reverse(Subscription destination) {
            return Tuple.Create(
                new SubscriptionRecord() { Id = destination.Id, SpaceId = destination.Space.Id, UserId = destination.User.Id },
                _userRecordMapper.Reverse(destination.User),
                _spaceRecordMapper.Reverse(destination.Space)
            );
        }
    }
    #endregion
}