using System;
using Updog.Application;
using Updog.Domain;

namespace Updog.Persistance {
    /// <summary>
    /// Mapper to convert a comment record and user record into it's entity.
    /// </summary>
    public interface ICommentRecordMapper : IReversableMapper<Tuple<CommentRecord, UserRecord>, Comment> { }
}