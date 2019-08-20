using System;
using Updog.Application;
using Updog.Domain;

namespace Updog.Persistance {
    /// <summary>
    /// Mapper to convert a post record to a post entity and back.
    /// </summary>
    public interface IPostRecordMapper : IReversableMapper<Tuple<PostRecord, UserRecord>, Post> {

    }
}