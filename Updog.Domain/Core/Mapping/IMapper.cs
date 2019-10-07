namespace Updog.Domain {
    /// <summary>
    /// Mapper to handle converting an object from A to B.
    /// </summary>
    /// <typeparam name="TSource">The object it can convert.</typeparam>
    /// <typeparam name="TDestination">The object it produces.</typeparam>
    public interface IMapper<TSource, TDestination> {
        /// <summary>
        /// Map the source object into it's destination object.
        /// </summary>
        /// <param name="source">The object to map.</param>
        /// <returns>The mapped result.</returns>
        TDestination Map(TSource source);
    }
}