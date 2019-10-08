namespace Updog.Domain {
    public interface IService { }

    public interface IService<TEntity> where TEntity : class, IEntity { }
}