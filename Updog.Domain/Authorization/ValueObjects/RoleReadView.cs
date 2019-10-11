using System.Collections.Generic;

namespace Updog.Domain {
    public sealed class RoleReadView : IValueObject {
        #region Properties
        public string Scope { get; }
        public IEnumerable<Permission> Permissions { get; }
        #endregion

        #region Constructor(s)
        public RoleReadView(string scope, IEnumerable<Permission> permissions) {
            Scope = scope;
            Permissions = permissions;
        }
        #endregion
    }
}