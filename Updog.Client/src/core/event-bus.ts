import Vue from 'vue';

/**
 * Event bus for propogating events across the application.
 */
export const EventBus = (function() {
    const vue = new Vue();

    return {
        /**
         * Subscribe to an event on the bus.
         * @param event The event to subscribe.
         * @param handler The handler to add.
         */
        on(event: string, handler: (arg?: any) => Promise<void>) {
            vue.$on(event, handler);
        },

        /**
         * Desubscribe a handler from it's event.
         * @param event The event to desubscribe from.
         * @param handler The handler to remove.
         */
        off(event: string, handler: (arg?: any) => Promise<void>) {
            vue.$off(event, handler);
        },

        /**
         * Emit an event to any listeners.
         * @param event The event to emit.
         * @param arg The argument to pass.
         */
        emit(event: string, arg?: any) {
            vue.$emit(event, arg);
        }
    };
})();
