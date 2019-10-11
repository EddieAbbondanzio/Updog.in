namespace Updog.Domain {
    public sealed class PermissionReadView : IValueObject {
        #region Properties
        public PermissionResource Resource { get; }
        public PermissionAction Action { get; }
        #endregion

        #region Constructor(s)
        public PermissionReadView(PermissionResource resource, PermissionAction action) {
            Resource = resource;
            Action = action;
        }
        #endregion
    }
}