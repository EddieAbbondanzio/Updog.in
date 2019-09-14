<template>
    <i :class="cssClasses">{{ icon }}</i>
</template>

<style scoped>
.icon-sm {
    font-size: 16px;
}

.icon-md {
    font-size: 24px;
}

.icon-lg {
    font-size: 48px;
}

.icon-xl {
    font-size: 64px;
}
</style>


<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { ThemeVariant } from '@/core/ui/common/themeable/theme-variant';
import { Themeable } from '@/core/ui/common/themeable/themeable';
import { Size } from '@/core/ui/common/size';
import { Sizable } from '@/core/ui/common/sizable';

/**
 * Materials icon.
 */
@Component({
    name: 'material-icon'
})
export default class MaterialIcon extends Vue implements Themeable, Sizable {
    /**
     * The size of the icon.
     */
    @Prop({ default: 'md' })
    public size!: Size;
    /**
     * The name of the icon to render.
     */
    @Prop()
    public icon!: string;
    /**
     * The color theme of the component.
     */
    @Prop({ default: 'light' })
    public variant!: ThemeVariant;

    /**
     * The CSS class list to use.
     */
    get cssClasses(): string {
        return `material-icons icon-${this.size} text-${this.variant}`;
    }
}
</script>