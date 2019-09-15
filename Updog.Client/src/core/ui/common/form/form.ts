import Vue from 'vue';

/**
 * Mixin for forms to implement.
 */
export interface Form<TOutput> {
    /**
     * Submit the form.
     */
    submit(): Promise<TOutput>;

    /**
     * Reset the form back to defaults.
     */
    reset(): void;
}
