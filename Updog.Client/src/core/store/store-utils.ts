export class StoreUtils {
    public static buildGetter(namespace: string, property?: string) {
        return property != null ? `${namespace}/${property}` : namespace;
    }

    public static buildAction(namespace: string, property?: string) {
        return property != null ? `${namespace}/${property}` : namespace;
    }

    public static buildMutation(namespace: string, property?: string): string {
        return property != null ? `${namespace}/${property}` : namespace;
    }
}
