using System.Threading.Tasks;

namespace Updog.Application {
    public interface IPolicy {
        Task<PolicyResult> Authorize(object action);
    }

    public interface IPolicy<TInput> : IPolicy where TInput : IAction {
        Task<PolicyResult> Authorize(TInput action);

        Task<PolicyResult> IPolicy.Authorize(object action) => Authorize(action);
    }
}