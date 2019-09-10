using System;
using Updog.Application;
using Updog.Domain;

namespace Updog.Persistance {
    /// <summary>
    /// Mapper to convert a vote record to it's entity.
    /// </summary>
    public interface IVoteRecordMapper : IReversableMapper<Tuple<VoteRecord, UserRecord>, Vote> {
    }
}