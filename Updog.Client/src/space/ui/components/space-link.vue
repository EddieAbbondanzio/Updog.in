<template>
    <router-link :to="{ name: 'space', params: { spaceName: space.name}}">
        <slot>{{ formattedName }}</slot>
    </router-link>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { Space } from '@/space/domain/space';

/**
 * Link to a space.
 */
@Component({
    name: 'user-link'
})
export default class SpaceLink extends Vue {
    /**
     * If the space name should be prefixed with: /s
     */
    @Prop({ default: false })
    public showPrefix!: boolean;

    /**
     * The user it links to.
     */
    @Prop()
    public space!: Space;

    /**
     * Get the formatted name of the space to display.
     */
    get formattedName() {
        return this.showPrefix ? `s/${this.space.name}` : this.space.name;
    }
}
</script>