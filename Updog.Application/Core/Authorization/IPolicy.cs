using System.Threading.Tasks;

namespace Updog.Application {
    public interface IPolicy { }

    public interface IPolicy<TInput> : IPolicy where TInput : IAction {
        Task<PolicyResult> Authorize(TInput action);
    }
}