using System;

namespace Updog.Domain {
    /// <summary>
    /// Subforum for collecting similar themed posts.
    /// </summary>
    public partial class Space : Entity<Space>, IUserEntity, IUpdatable<SpaceUpdate> {
        #region Constants
        /// <summary>
        /// The number of spaces per page when searching for them.
        /// </summary>
        public const int PageSize = 100;

        /// <summary>
        /// The maximum number of characters allowed in a space name.
        /// </summary>
        public const int NameMaxLength = 24;

        /// <summary>
        /// The maximum number of characters allowed in a space description.
        /// </summary>
        public const int DescriptionMaxLength = 512;
        #endregion

        #region Properties
        public int UserId { get; }
        public string Name { get; }
        public string Description { get; private set; }
        public DateTime CreationDate { get; }
        public int SuscriberCount { get; set; }
        public bool IsDefault { get; set; }
        #endregion

        #region Constructor(s)
        internal Space(SpaceCreate createData, User user) {
            UserId = user.Id;
            Name = createData.Name;
            Description = createData.Description;
            CreationDate = DateTime.UtcNow;
            IsDefault = createData.IsDefault;
        }

        internal Space(int id, int userId, string name, string description, DateTime creationDate, int subscriberCount, bool isDefault) {
            Id = id;
            UserId = userId;
            Name = name;
            Description = description;
            CreationDate = creationDate;
            SuscriberCount = subscriberCount;
            IsDefault = isDefault;
        }
        #endregion

        #region Publics
        public void Update(SpaceUpdate update) {
            Description = update.Description;
        }
        #endregion
    }
}