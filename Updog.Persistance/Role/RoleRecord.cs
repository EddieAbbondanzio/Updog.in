using Updog.Domain;

namespace Updog.Persistance {
    public sealed class RoleRecord {
        #region Properties
        public int Id { get; set; }
        public int UserId { get; set; }
        public RoleType RoleType { get; set; }
        public string Domain { get; set; } = "";
        #endregion
    }
}