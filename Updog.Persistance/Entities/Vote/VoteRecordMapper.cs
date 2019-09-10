using System;
using Updog.Domain;

namespace Updog.Persistance {
    /// <summary>
    /// Mapper to convert vote record's to their entity and back.
    /// </summary>
    public sealed class VoteRecordMapper : IVoteRecordMapper {
        #region Fields
        private IUserRecordMapper userRecordMapper;
        #endregion

        #region Constructor(s)
        public VoteRecordMapper(IUserRecordMapper userRecordMapper) {
            this.userRecordMapper = userRecordMapper;
        }
        #endregion

        #region Publics
        public Vote Map(Tuple<VoteRecord, UserRecord> source) {
            return new Vote() {
                Id = source.Item1.Id,
                User = userRecordMapper.Map(source.Item2),
                ResourceId = source.Item1.ResourceId,
                ResourceType = source.Item1.ResourceType,
                Direction = source.Item1.Direction
            };
        }

        public Tuple<VoteRecord, UserRecord> Reverse(Vote destination) {
            VoteRecord voteRec = new VoteRecord() {
                Id = destination.Id,
                UserId = destination.User.Id,
                ResourceId = destination.ResourceId,
                ResourceType = destination.ResourceType,
                Direction = destination.Direction
            };

            UserRecord userRecord = userRecordMapper.Reverse(destination.User);

            return Tuple.Create<VoteRecord, UserRecord>(voteRec, userRecord);
        }
        #endregion
    }
}