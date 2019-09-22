<template>
    <v-app>
        <space-quick-access-bar />

        <v-app-bar app>
            <v-toolbar-title class="headline text-uppercase">
                <span>Vuetify</span>
                <span class="font-weight-light">MATERIAL DESIGN</span>
            </v-toolbar-title>
            <v-spacer></v-spacer>
            <v-btn text href="https://github.com/vuetifyjs/vuetify/releases/latest" target="_blank">
                <span class="mr-2">Latest Release</span>
            </v-btn>
        </v-app-bar>

        <v-content>
            <router-view />
        </v-content>
    </v-app>
    <!-- <router-view /> -->
</template>

<style lang="scss">
@import './assets/styles/style.scss';
</style>

<script lang="ts">
import '@/core/plugins/vuetify';

import { Component, Vue } from 'vue-property-decorator';
import Cookie from 'js-cookie';
import { UserLoginMixin } from './user';
import SpaceQuickAccessBar from '@/space/ui/components/space-quick-access-bar.vue';

@Component({
    name: 'app',
    components: {
        SpaceQuickAccessBar
    }
})
export default class App extends UserLoginMixin {
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
}
</script>
