using FluentValidation;
using Updog.Application.Paging;

namespace Updog.Application.Validation {
    internal class IPagableValidator<TResource> : FluentValidatorAdapter<TResource> where TResource : IPagable {
        public IPagableValidator() {
            When(p => p.Pagination != null, () => {
                RuleFor(p => p.Pagination!.PageNumber).GreaterThanOrEqualTo(0).WithMessage("Page number must be 0 or greater.");
                RuleFor(p => p.Pagination!.PageSize).GreaterThanOrEqualTo(1).WithMessage("Page size must be 1 or larger");
            });

        }
    }
}