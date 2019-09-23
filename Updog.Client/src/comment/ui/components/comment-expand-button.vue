<template>
    <a href="#" @click.prevent="onClick" class="px-0 py-0 text-decoration-none">
        <span v-if="expanded" class="grey--text">[&nbsp;-&nbsp;]</span>
        <span v-else class="grey--text">[&nbsp;+&nbsp;]</span>
    </a>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';

/**
 * Component to control expand / collapse of a section.
 */
@Component({
    name: 'expand-button'
})
export default class CommentExpandButton extends Vue {
    /**
     * If the button is currently expanded.
     */
    public expanded: boolean = true;

    public onClick() {
        this.expanded = !this.expanded;
        this.$emit('toggle', this.expanded);
    }
}
</script>