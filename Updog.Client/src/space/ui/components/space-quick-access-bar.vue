<template>
    <div class="bg-dark space-quick-access px-3 text-sm">
        <router-link :to="{name: 'home'}" class="text-200">HOME</router-link>
        <span class="px-3 text-500">|</span>
        <div class="d-inline-block" v-for="(space, index) in spaces" v-bind:key="space.id">
            <space-link :space="space" variant="light">{{ space.name | capitalize }}</space-link>

            <span class="px-1 text-500" v-if="index + 1 < spaces.length">-</span>
        </div>
    </div>
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
    get spaces() {
        if (this.$isLoggedIn()) {
            return this.$subscribedSpaces;
        } else {
            return this.$defaultSpaces;
        }
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