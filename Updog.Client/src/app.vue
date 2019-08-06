<template>
    <div id="app">
        <nav-bar />
        <router-view />
    </div>
</template>

<style>
.nav-icon {
    height: 64px;
}
</style>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import { User } from './user/common/user';
import { Context } from './core/context';
import { EventBus } from './core/event-bus';
import NavBar from '@/components/nav-bar.vue';

@Component({
    name: 'app',
    components: {
        NavBar
    }
})
export default class App extends Vue {}
</script>
