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
/**
 * Size possibilities of the icon.
 */
export type Size = 'sm' | 'md' | 'lg' | 'xl';
/**
 * Materials icon.
 */
@Component({
    name: 'material-icon'
})
export default class MaterialIcon extends Vue {
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
    public variant!: 'primary' | 'secondary' | 'success' | 'danger' | 'warning' | 'info' | 'light' | 'dark';
    /**
     * The CSS class list to use.
     */
    get cssClasses(): string {
        return `material-icons icon-${this.size} text-${this.variant}`;
    }
}
</script>