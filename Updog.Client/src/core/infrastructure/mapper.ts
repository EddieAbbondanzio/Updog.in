/**
 * Interface for a class that converts an object from one type to
 * another to implement.
 */
export interface Mapper<TSource, TDestination> {
    /**
     * Map an object from it's source type into it's destination type.
     * @param source The source object.
     */
    map(source: TSource): TDestination;
}
