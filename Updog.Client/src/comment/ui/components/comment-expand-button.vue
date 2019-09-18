<template>
    <b-button @click="onClick" variant="link" class="px-0 py-0 text-decoration-none">
        <span v-if="expanded" class="text-muted">[&nbsp;-&nbsp;]</span>
        <span v-else class="text-muted">[&nbsp;+&nbsp;]</span>
    </b-button>
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