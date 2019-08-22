namespace Updog.Application {
    /// <summary>
    /// Two way mapper that can convert an object into another, and back if desired.
    /// </summary>
    /// <typeparam name="TSource">The starting source.</typeparam>
    /// <typeparam name="TDestination">The destination source.</typeparam>
    public interface IReversableMapper<TSource, TDestination> : IMapper<TSource, TDestination> {
        #region Publics
        /// <summary>
        /// Convert the destination resource back into it's original source.
        /// </summary>
        /// <param name="destination">The object to reverse map.</param>
        /// <returns>The de mapped object.</returns>
        TSource Reverse(TDestination destination);
        #endregion
    }
}