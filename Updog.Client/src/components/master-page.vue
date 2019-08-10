<template>
    <b-container fluid>
        <b-row>
            <b-col>
                <nav-bar />
            </b-col>
        </b-row>

        <b-row>
            <b-col>
                <router-view />
            </b-col>
        </b-row>
    </b-container>
</template>

<style scoped>
.container-fluid {
    padding: 0px !important;
}
</style>

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