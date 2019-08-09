<template>
    <div id="app">
        <nav-bar />
        <router-view />
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