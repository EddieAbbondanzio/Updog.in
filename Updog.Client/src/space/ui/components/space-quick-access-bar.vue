<template>
    <v-system-bar app dark>
        <router-link :to="{name: 'home'}" class="font-weight-bold white--text">HOME</router-link>
        <span class="px-3">|</span>
        <div class="d-inline-block" v-for="(space, index) in spaces" v-bind:key="space.id">
            <space-link :space="space" class="white--text">{{ space.name | capitalize }}</space-link>

            <span class="px-1 font-weight-bold" v-if="index + 1 < spaces.length">-</span>
        </div>
    </v-system-bar>
</template>


<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { SpaceFinderMixin } from '@/space';
import SpaceLink from '@/space/ui/components/space-link.vue';
import { getModule } from 'vuex-module-decorators';
import { UserStore, UserLogin } from '../../../user';
import { Store } from '../../../core';

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
        // Store.subscribe((mutation, state) => {
        //     console.log(mutation);
        // });

        if (this.$isLoggedIn()) {
            await this.$findDefaultSpaces();
        } else {
            await this.$findDefaultSpaces();
        }
    }
}
</script>