using System;
using System.Threading.Tasks;

namespace Updog.Application {
    /// <summary>
    /// Monad for a potential value that can be returned.
    /// </summary>
    /// <typeparam name="TValue">The value type being returned.</typeparam>
    public class Maybe<TValue> {
        #region Fields
        /// <summary>
        /// Contained value.
        /// </summary>
        private readonly TValue value;

        /// <summary>
        /// If the maybe actually has a value in it.
        /// </summary>
        private readonly bool hasValue;
        #endregion

        #region Constructor(s)
        public Maybe() {
            this.value = default(TValue)!;
            this.hasValue = false;
        }

        public Maybe(TValue value) {
            this.value = value;
            this.hasValue = true;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Match the maybe to a handler depending on if it has a value or not.
        /// </summary>
        /// <param name="someFunc">The handler to invoke if there is a value.</param>
        /// <param name="noneFunc">The handler to invoke if no value.</param>
        /// <typeparam name="T">The return type.</typeparam>
        /// <returns>The value returned.</returns>
        public T Match<T>(Func<TValue, T> someFunc, Func<T> noneFunc) => this.hasValue ? someFunc(this.value) : noneFunc();

        /// <summary>
        /// Match the maybe to an async handler depending on if it has a value or not.
        /// </summary>
        /// <param name="someFunc">The handler to invoke if there is a value.</param>
        /// <param name="noneFunc">The handler to invoke if no value.</param>
        /// <typeparam name="T">The return type.</typeparam>
        /// <returns>The value returned.</returns>
        public async Task<T> MatchAsync<T>(Func<TValue, Task<T>> someFunc, Func<Task<T>> noneFunc) => this.hasValue ? await someFunc(this.value) : await noneFunc();
        #endregion

        /// <summary>
        /// Create a new maybe.
        /// </summary>
        /// <typeparam name="TVal">The type of value it holds.</typeparam>
        /// <returns>The newly created maybe.</returns>
        public static Maybe<TVal> None<TVal>() => new Maybe<TVal>();

        public static implicit operator Maybe<TValue>(TValue value) => new Maybe<TValue>(value);
    }
}
