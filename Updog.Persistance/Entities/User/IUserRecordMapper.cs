using Updog.Application;
using Updog.Domain;

namespace Updog.Persistance {
    /// <summary>
    /// Mapper to convert a user record to a user entity and back.
    /// </summary>
    public interface IUserRecordMapper : IReversableMapper<UserRecord, User> {

    }
}