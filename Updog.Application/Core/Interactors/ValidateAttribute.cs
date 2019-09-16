using System;
using FluentValidation;

namespace Updog.Application {
    /// <summary>
    /// Attribute to auto validate an interactor via a validator.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public sealed class Validate : System.Attribute {
        #region Properties
        /// <summary>
        /// The type of validator that should be used.
        /// </summary>
        /// <value></value>
        public Type Validator { get; }
        #endregion

        #region Constructor(s)
        public Validate(Type validatorType) {
            Validator = validatorType;

            if (!typeof(IValidator).IsAssignableFrom(validatorType)) {
                throw new ArgumentException("Validator type must implement IValidator");
            }
        }
        #endregion
    }
}