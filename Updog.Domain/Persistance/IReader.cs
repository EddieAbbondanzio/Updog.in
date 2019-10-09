namespace Updog.Domain {
    public interface IReader { }

    /// <summary>
    /// Read only interface for reading value objects from the database.
    /// </summary>
    /// <typeparam name="TValueObject">Value object type it reads.</typeparam>
    public interface IReader<TValueObject> : IReader where TValueObject : class, IValueObject { }
}