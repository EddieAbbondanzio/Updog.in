using System.Threading;
using System.Threading.Tasks;

namespace Updog.Application.Validation {
    public interface IValidator {
        ValidationResult Validate(object resource);

        Task<ValidationResult> ValidateAsync(object resource, CancellationToken token = default(CancellationToken));
    }

    /// <summary>
    /// Interface for validators to implement.
    /// </summary>
    /// <typeparam name="TResource">The resource type being validated.</typeparam>
    public interface IValidator<TResource> : IValidator {
        ValidationResult Validate(TResource resource);

        Task<ValidationResult> ValidateAsync(TResource resource, CancellationToken token = default(CancellationToken));
    }
}