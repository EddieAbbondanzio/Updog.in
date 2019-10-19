using System;
using System.Reflection;

namespace Updog.Application {
    /// <summary>
    /// Utility methods for managing custom attributes.
    /// </summary>
    public static class AttributeUtils {
        /// <summary>
        /// Get a custom attribute on an instance method.
        /// </summary>
        /// <param name="type">The type that owns the method.</param>
        /// <param name="method">The method name.</param>
        /// <typeparam name="TAttribute">The attribute type.</typeparam>
        /// <returns>The custom attribute (if any).</returns>
        public static TAttribute? GetMethodAttribute<TAttribute>(Type type, string method) where TAttribute : Attribute =>
            type.GetMethod(
                method, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public
            )?.GetCustomAttribute<TAttribute>();
    }
}