namespace Updog.Domain {
    public sealed class Permission : IEntity {
        #region Properties
        public int Id { get; set; }
        public PermissionResource Resource { get; }
        public PermissionAction Action { get; }
        #endregion

        #region Constructor(s)
        public Permission(PermissionResource resource, PermissionAction action) {
            Resource = resource;
            Action = action;
        }
        #endregion
    }
}