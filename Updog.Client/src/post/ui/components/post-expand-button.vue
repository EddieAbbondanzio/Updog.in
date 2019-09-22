<template>
    <v-btn @click.native.stop="onClick" text icon>
        <v-icon size="36">{{expanded ? 'remove' : 'add_box'}}</v-icon>
    </v-btn>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';

/**
 * Component to control expand / collapse of a section.
 */
@Component({
    name: 'post-expand-button'
})
export default class PostExpandButton extends Vue {
    /**
     * If the button is currently expanded.
     */
    public expanded: boolean = false;

    public onClick() {
        this.expanded = !this.expanded;
        this.$emit('toggle', this.expanded);
    }
}
</script>