using Updog.Domain;

namespace Updog.Persistance {
    public interface IUserReadViewMapper : IMapper<UserRecord, UserReadView> { }

    public sealed class UserReadViewMapper : IUserReadViewMapper {
        #region Publics
        public UserReadView Map(UserRecord source) => new UserReadView() {
            Id = source.Id,
            Username = source.Username,
            JoinedDate = source.JoinedDate,
            PostKarma = source.PostKarma,
            CommentKarma = source.CommentKarma
        };
        #endregion
    }
}