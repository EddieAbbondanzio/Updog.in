<template>
    <div>
        <header>
            <nav-bar />
        </header>

        <b-container fluid>
            <b-row>
                <!-- Main Content of the page -->
                <b-col md="8" lg="10">
                    <slot></slot>
                </b-col>
                <!-- Side bar on the right -->
                <b-col md="4" lg="2">
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
import NavBar from '@/components/nav-bar.vue';

/**
 * Master page for the site.
 */
@Component({
    name: 'master-page',
    components: {
        NavBar
    }
})
export default class MasterPage extends Vue {}
</script>