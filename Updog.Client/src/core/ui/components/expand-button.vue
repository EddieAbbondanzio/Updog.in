<template>
    <b-button @click="onClick" variant="link" class="px-0 py-0">
        <material-icon
            :icon="expanded ? 'remove' : 'add_box'"
            variant="muted"
            style="font-size: 36px;"
        />
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
export default class ExpandButton extends Vue {
    /**
     * If the button is currently expanded.
     */
    public expanded: boolean = false;

    public onClick() {
        this.$emit('toggle');
        this.expanded = !this.expanded;
    }
}
</script>