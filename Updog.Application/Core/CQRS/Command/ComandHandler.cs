using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Updog.Application.Validation;

namespace Updog.Application {
    /// <summary>
    /// Command to create, update, or delete resource(s). Automatically wraps the
    /// action in a transaction.
    /// </summary>
    public abstract class CommandHandler<TCommand> : IActionHandler<TCommand, CommandResult> where TCommand : class, ICommand {
        #region Fields
        /// <summary>
        /// The validator to use on the input before handling it.
        /// </summary>
        private IValidator? validator;
        #endregion

        #region Constructor(s)
        public CommandHandler() {
            Validate? attribute = GetValidateAttribute();
            if (attribute != null) {
                validator = GetValidatorInstance(attribute.Validator);
            }
        }
        #endregion

        #region Publics
        public async Task<CommandResult> Execute(TCommand command) {
            // If the command handler has a validator, check to see if input is valid first.
            if (validator != null) {
                var result = await validator.ValidateAsync(command);

                if (!result.IsValid) {
                    /*
                    * No sense in returning back all the errors.
                    */
                    return CommandResult.Failure(result.Failures.First().ErrorMessage);
                }
            }

            // Finally, execute the command.
            return await ExecuteCommand(command);
        }
        #endregion

        #region Privates
        protected abstract Task<CommandResult> ExecuteCommand(TCommand command);

        protected CommandResult Success(int insertId = 0) => CommandResult.Success(insertId);

        protected CommandResult Failure(string? error = null) => CommandResult.Failure(error);

        /// <summary>
        /// Get the custom validate attrbitute from the handle method
        /// if it exists.
        /// </summary>
        /// <returns>The custom attribute.</returns>
        private Validate? GetValidateAttribute() => GetType().GetMethod("ExecuteCommand", BindingFlags.Instance | BindingFlags.NonPublic).GetCustomAttribute<Validate>();

        /// <summary>
        /// Generate a new instance of a validator from it's type.
        /// </summary>
        /// <param name="validator">The validator type to instantiate.</param>
        /// <returns>The newly created validator.</returns>
        private IValidator GetValidatorInstance(Type validator) => (IValidator)Activator.CreateInstance(validator);
        #endregion
    }
}