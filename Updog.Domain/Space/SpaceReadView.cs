using System;

namespace Updog.Domain {
    public sealed class SpaceReadView : IValueObject {
        #region Properties
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime CreationDate { get; set; }
        public UserReadView User { get; set; } = null!;
        public int SubscriberCount { get; set; }
        public bool IsDefault { get; set; }
        #endregion
    }
}