namespace Updog.Domain {
    public interface IUpdatable<TUpdate> {
        void Update(TUpdate update);
    }
}