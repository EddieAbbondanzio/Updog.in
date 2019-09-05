using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Mapper to convert a user into it's DTO.
    /// </summary>
    public sealed class UserViewMapper : IUserViewMapper {
        #region Publics
        /// <summary>
        /// Map the user into it's DTO.
        /// </summary>
        /// <param name="user">The user to map.</param>
        /// <returns>The resulting user view.</returns>
        public UserView Map(User user) {
            return new UserView(user.Id, user.Username, user.JoinedDate);
        }
        #endregion
    }
}