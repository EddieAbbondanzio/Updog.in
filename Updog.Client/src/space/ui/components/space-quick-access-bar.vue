<template>
    <v-system-bar app color="primary">
        <router-link :to="{name: 'home'}" class="font-weight-bold white--text">HOME</router-link>
        <span class="px-3">|</span>
        <div class="d-inline-block" v-for="(space, index) in spaces" :key="space.id">
            <space-link :space="space" class="white--text">{{ space.name | capitalize }}</space-link>

            <span class="px-1 font-weight-bold" v-if="index + 1 < spaces.length">-</span>
        </div>
    </v-system-bar>
</template>


<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { getModule } from 'vuex-module-decorators';
import SpaceLink from '@/space/ui/components/space-link.vue';
import SpaceFinderMixin from '@/space/mixins/space-finder-mixin';

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
    /**
     * Get the spaces to display in alphabetical order.
     */
    get spaces() {
        const spaces = this.$isLoggedIn() ? this.$subscribedSpaces : this.$defaultSpaces;
        return spaces.sort((a, b) => (a.name.toLowerCase() > b.name.toLowerCase() ? 1 : -1));
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