using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interface for read actions to implement.
    /// </summary>
    public abstract class QueryHandler<TQuery, TOutput> : IActionHandler<TQuery, TOutput> where TQuery : class, IQuery {
        #region Fields
        private IValidator? validator;
        private IPolicy? policy;
        #endregion

        #region Publics
        public void Init(IServiceProvider provider) {
            ValidateAttribute? validateAttribute = AttributeUtils.GetMethodAttribute<ValidateAttribute>(GetType(), "ExecuteQuery");

            if (validateAttribute != null) {
                validator = provider.GetRequiredService(validateAttribute.Validator) as IValidator;
            }

            PolicyAttribute? policyAttribute = AttributeUtils.GetMethodAttribute<PolicyAttribute>(GetType(), "ExecuteQuery");

            if (policyAttribute != null) {
                policy = provider.GetRequiredService(policyAttribute.Policy) as IPolicy;
            }
        }

        public async Task<Either<TOutput, Error>> Execute(TQuery query) {
            // If the query handler has a validator, check to see if input is valid first.
            if (validator != null) {
                var result = await validator.ValidateAsync(query);

                if (!result.IsValid) {
                    return new ValidationError(result);
                }
            }

            if (policy != null) {
                var result = await policy.Authorize(query);

                if (!result.IsAuthorized) {
                    return new AuthorizationError(result);
                }
            }

            // Finally, execute the query.
            return await ExecuteQuery(query);
        }
        #endregion

        #region Privates
        protected abstract Task<Either<TOutput, Error>> ExecuteQuery(TQuery query);
        #endregion
    }
}
