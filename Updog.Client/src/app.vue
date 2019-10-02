<template>
    <v-app>
        <space-quick-access-bar />
        <app-bar @toggleNav="onToggleNav" />
        <nav-drawer :show="showNav" />

        <v-content class="d-flex flex-grow-1">
            <router-view />
        </v-content>
    </v-app>
</template>

<style lang="scss">
@import './assets/styles/style.scss';
</style>

<script lang="ts">
// import './plugins/vuetify';

import { Component, Vue } from 'vue-property-decorator';
import Cookie from 'js-cookie';
import UserLoginMixin from '@/user/mixins/user-login-mixin';
import SpaceQuickAccessBar from '@/space/ui/components/space-quick-access-bar.vue';
import AppBar from '@/core/ui/components/app-bar.vue';
import NavDrawer from '@/core/ui/components/nav-drawer.vue';

@Component({
    name: 'app',
    components: {
        SpaceQuickAccessBar,
        AppBar,
        NavDrawer
    }
})
export default class App extends UserLoginMixin {
    public showNav: boolean = false;

    public async created() {
        const authToken = Cookie.get('auth');

        if (authToken != null) {
            try {
                await this.$reloginUser(authToken);
            } catch {
                // Magic!
            }
        }
    }

    public onToggleNav() {
        this.showNav = !this.showNav;
    }
}
</script>
