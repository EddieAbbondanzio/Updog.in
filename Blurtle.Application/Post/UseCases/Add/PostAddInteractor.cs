using System.Threading.Tasks;

namespace Blurtle.Application {
    /// <summary>
    /// Adds new posts to the system.
    /// </summary>
    public sealed class PostAdder : IInteractor<PostAddParams> {
        public async Task Handle(PostAddParams input) {
            throw new System.NotImplementedException();
        }
    }
}