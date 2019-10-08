using System;
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
                    return new CommandResult(false);
                }
            }

            // Finally, execute the command.
            await ExecuteCommand(command);
            return new CommandResult(true);
        }
        #endregion

        #region Privates
        protected abstract Task<CommandResult> ExecuteCommand(TCommand command);

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