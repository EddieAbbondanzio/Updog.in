<template>
    <router-view />
</template>

<style lang="scss">
@import './assets/styles/style.scss';
</style>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import Cookie from 'js-cookie';
import { UserLoginMixin } from './user';

@Component({
    name: 'app'
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
