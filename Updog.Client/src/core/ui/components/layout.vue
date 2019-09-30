<template>
    <div>
        <v-container>
            <v-row class="px-3">
                <!-- Main Content of the page -->
                <v-col :sm="noSideBar ? 12 : 8" :md="noSideBar ? 12 : 8" :lg="noSideBar ? 12 : 10">
                    <slot></slot>
                </v-col>
                <!-- Side bar on the right -->
                <v-col class="pl-3" sm="4" md="4" lg="2" v-if="!noSideBar">
                    <slot name="side-bar"></slot>
                </v-col>
            </v-row>
        </v-container>

        <footer>
            <slot name="footer"></slot>
            <a
                class="grey--text text-lighten-2 float-right pa-3 caption"
                href="https://github.com/EddieAbbondanzio/Updog.in"
            >&lt;/&gt;</a>
        </footer>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import AppBar from '@/core/ui/components/app-bar.vue';

/**
 * Page layout for the site.
 */
@Component({
    name: 'layout',
    components: {
        AppBar
    }
})
export default class Layout extends Vue {
    /**
     * If the sidebar should be hidden.
     */
    @Prop({ default: false })
    public noSideBar!: boolean;
}
</script>