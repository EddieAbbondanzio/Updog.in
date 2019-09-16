using System;
using System.Reflection;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace Updog.Application {
    /// <summary>
    /// Base class for interactors to implement. 
    /// </summary>
    public abstract class Interactor {
        #region Properties
        /// <summary>
        /// The validator to use on the input before handling it.
        /// </summary>
        protected IValidator? Validator { get; }
        #endregion

        #region Constructor(s)
        public Interactor() {
            Validate? attribute = GetValidateAttribute();

            if (attribute != null) {
                Validator = GetValidatorInstance(attribute.Validator);
            }
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Get the custom validate attrbitute from the handle method
        /// if it exists.
        /// </summary>
        /// <returns>The custom attribute.</returns>
        private Validate? GetValidateAttribute() => GetType().GetMethod("HandleInput", BindingFlags.Instance | BindingFlags.NonPublic).GetCustomAttribute<Validate>();

        /// <summary>
        /// Generate a new instance of a validator from it's type.
        /// </summary>
        /// <param name="validator">The validator type to instantiate.</param>
        /// <returns>The newly created validator.</returns>
        private IValidator GetValidatorInstance(Type validator) => (IValidator)Activator.CreateInstance(validator);
        #endregion
    }

    /// <summary>
    /// Interactor that takes an input but returns nothing.
    /// </summary>
    /// <typeparam name="TInput">The input type.</typeparam>
    public abstract class Interactor<TInput> : Interactor {
        #region Publics
        public async Task<Maybe<ValidationResult>> Handle(TInput input) {
            if (Validator != null) {
                var result = await Validator.ValidateAsync(input);

                if (!result.IsValid) {
                    return result;
                }
            }

            await HandleInput(input);
            return new Maybe<ValidationResult>();
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Handle the input of the interactor.
        /// </summary>
        /// <param name="input">The input to handle.</param>
        /// <returns>The processed output.</returns>
        protected abstract Task HandleInput(TInput input);
        #endregion
    }

    /// <summary>
    /// Interactor that takes an input and produces an output.
    /// </summary>
    /// <typeparam name="TInput">The input type.</typeparam>
    /// <typeparam name="TOutput">The output type.</typeparam>
    public abstract class Interactor<TInput, TOutput> : Interactor {
        #region Publics
        public async Task<Either<TOutput, ValidationResult>> Handle(TInput input) {
            if (Validator != null) {
                var result = await Validator.ValidateAsync(input);

                if (!result.IsValid) {
                    return result;
                }
            }

            return await HandleInput(input);
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Handle the input of the interactor.
        /// </summary>
        /// <param name="input">The input to handle.</param>
        /// <returns>The processed output.</returns>
        protected abstract Task<TOutput> HandleInput(TInput input);
        #endregion
    }
}