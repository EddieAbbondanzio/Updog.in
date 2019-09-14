<template>
    <div class="bg-secondary text-white">
        <space-link v-for="space in spaces" v-bind:key="space.id" :space="space" class="px-1" />
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { SpaceFinderMixin } from '@/space';
import SpaceLink from '@/space/ui/components/space-link.vue';

/**
 * Quick access bar to navigate default spaces, or subscribed spaces.
 */
@Component({
    name: 'space-quick-access-bar',
    components: {
        SpaceLink
    }
})
export default class SpaceQuickAccessBar extends SpaceFinderMixin {
    get spaces() {
        if (this.$isLoggedIn()) {
            return this.$subscribedSpaces;
        } else {
            return this.$defaultSpaces;
        }
    }

    public async created() {
        if (this.$isLoggedIn()) {
            await this.$findDefaultSpaces();
        } else {
            await this.$findDefaultSpaces();
        }
    }
}
</script>