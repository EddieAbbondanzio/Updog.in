using System;

namespace Updog.Application {
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class PolicyAttribute : Attribute {
        #region Properties
        public Type Policy { get; }
        #endregion

        #region Constructor(s)
        public PolicyAttribute(Type policy) {
            Policy = policy;

            if (!typeof(IPolicy).IsAssignableFrom(Policy)) {
                throw new ArgumentException("Policy type must implement IPolicy");
            }
        }
        #endregion
    }
}