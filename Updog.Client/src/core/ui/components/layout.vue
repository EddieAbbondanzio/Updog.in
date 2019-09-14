<template>
    <div>
        <header>
            <nav-bar />
        </header>

        <b-container fluid>
            <b-row class="py-3">
                <!-- Main Content of the page -->
                <b-col :md="noSideBar ? 12 : 8" :lg="noSideBar ? 12 : 10">
                    <slot></slot>
                </b-col>
                <!-- Side bar on the right -->
                <b-col md="4" lg="2" v-if="!noSideBar">
                    <slot name="side-bar"></slot>
                </b-col>
            </b-row>
        </b-container>

        <footer>
            <slot name="footer"></slot>
        </footer>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import NavBar from '@/core/ui/components/nav-bar.vue';

/**
 * Page layout for the site.
 */
@Component({
    name: 'layout',
    components: {
        NavBar
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