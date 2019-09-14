import { ThemeVariant } from './theme-variant';

/**
 * A component that can be themed.
 */
export interface Themeable {
    /**
     * The color variant of the component.
     */
    variant: ThemeVariant;
}
