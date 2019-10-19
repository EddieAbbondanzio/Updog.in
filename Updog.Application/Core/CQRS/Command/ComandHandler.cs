using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Command to create, update, or delete resource(s). Automatically wraps the
    /// action in a transaction.
    /// </summary>
    public abstract class CommandHandler<TCommand> : IActionHandler<TCommand, CommandResult> where TCommand : class, ICommand {
        #region Fields
        private IValidator? validator;
        private IPolicy? policy;
        #endregion

        #region Publics
        public void Init(IServiceProvider provider) {
            ValidateAttribute? validateAttribute = AttributeUtils.GetMethodAttribute<ValidateAttribute>(GetType(), "ExecuteCommand");

            if (validateAttribute != null) {
                validator = provider.GetRequiredService(validateAttribute.Validator) as IValidator;
            }

            PolicyAttribute? policyAttribute = AttributeUtils.GetMethodAttribute<PolicyAttribute>(GetType(), "ExecuteCommand");

            if (policyAttribute != null) {
                policy = provider.GetRequiredService(policyAttribute.Policy) as IPolicy;
            }
        }

        public async Task<Either<CommandResult, Error>> Execute(TCommand command) {
            // If the command handler has a validator, check to see if input is valid first.
            if (validator != null) {
                var result = await validator.ValidateAsync(command);

                if (!result.IsValid) {
                    return new ValidationError(result);
                }
            }

            if (policy != null) {
                var result = await policy.Authorize(command);

                if (!result.IsAuthorized) {
                    return new AuthorizationError(result);
                }
            }

            // Finally, execute the command.
            return await ExecuteCommand(command);
        }
        #endregion

        #region Privates
        protected abstract Task<Either<CommandResult, Error>> ExecuteCommand(TCommand command);

        protected CommandResult Success() => CommandResult.Success();
        protected CommandResult Insert(int id) => CommandResult.Insert(id);
        #endregion
    }
}