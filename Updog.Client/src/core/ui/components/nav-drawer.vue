<template>
    <v-navigation-drawer app :value="show">
        <template v-slot:prepend>
            <v-list-item class="p-3">
                <v-list-item-content class="d-flex flex-column align-center">
                    <v-list-item-title class="display-1">My Spaces</v-list-item-title>

                    <v-text-field
                        placeholder="Type to filter"
                        clearable
                        v-model="filter"
                        @input="filterSpaces"
                    />
                </v-list-item-content>
            </v-list-item>
            <v-divider />

            <v-list-item
                v-for="space in spaces"
                v-bind:key="space.name"
                :link="true"
                :to="{name: 'space', params: {spaceName: space.name}}"
            >{{space.name}}</v-list-item>
            <v-list-item v-if="spaces.length == 0" class="d-flex flex-column align-center">
                <span>There's nothing here!</span>
            </v-list-item>
        </template>
    </v-navigation-drawer>
</template>

<script lang="ts">
import { Component, Vue, Prop, Watch } from 'vue-property-decorator';
import { Space, SpaceFinderMixin } from '../../../space';
import SpaceLink from '@/space/ui/components/space-link.vue';

@Component({
    name: 'nav-drawer',
    components: {
        SpaceLink
    }
})
export default class NavDrawer extends SpaceFinderMixin {
    @Prop({ default: false })
    public show!: boolean;

    public filter: string | null = '';

    public spaces: Space[] = [];

    @Watch('show')
    public onShow() {
        this.filterSpaces();
    }

    /**
     * Get the spaces to display in alphabetical order.
     */
    public filterSpaces() {
        const spaces = this.$isLoggedIn() ? this.$subscribedSpaces : this.$defaultSpaces;
        const sortedSpaces = spaces.sort((a, b) => (a.name.toLowerCase() > b.name.toLowerCase() ? 1 : -1));

        if (this.filter != null && this.filter !== '') {
            this.spaces = sortedSpaces.filter(s => s.name.toLowerCase().includes(this.filter!.toLowerCase()));
        } else {
            this.spaces = sortedSpaces;
        }
    }
}
</script>