import Mixin from 'vue-class-component';
import Vue from 'vue';
import { Themeable } from './themeable';
import { ThemeVariant } from './theme-variant';
import { Prop } from 'vue-property-decorator';

/**
 * Mixin to find users in the backend.
 */
@Mixin
export class ThemeableText extends Vue implements Themeable {
    /**
     * The color of text.
     */
    @Prop({ default: 'primary' })
    public variant!: ThemeVariant;

    get cssClass() {
        return `text-${this.variant}`;
    }
}
