using System;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Attribute to auto validate an interactor via a validator.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public sealed class Permissions : System.Attribute {
        #region Properties
        public PermissionResource Resource { get; }
        public Domain.PermissionAction Action { get; }
        #endregion

        #region Constructor(s)
        public Permissions(PermissionResource resource, Domain.PermissionAction action) {
            Resource = resource;
            Action = action;
        }
        #endregion
    }
}