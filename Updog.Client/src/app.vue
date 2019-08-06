<template>
    <div id="app">
        <b-navbar variant="light" type="dark" class="border-bottom">
            <b-navbar-brand href="#" to="home">
                <img src="@/assets/logo.png" class="nav-icon" />
            </b-navbar-brand>

            <div class="ml-auto" v-if="!isLoggedIn()">
                <b-button variant="outline-dark" to="login" class="mr-2">Log In</b-button>
                <b-button variant="outline-dark" to="signup" class="ml-2">Sign Up</b-button>
            </div>
            <div class="ml-auto d-flex align-items-center flex-row" v-else>
                <material-icon
                    icon="person"
                    variant="dark"
                    class="border border-dark rounded mr-2"
                    size="md"
                />
                <h5 class="d-inline-block my-0">{{ username() }}</h5>
                <span class="mx-1">|</span>

                <b-button variant="link" class="pl-0" @click="onLogout">Logout</b-button>
            </div>
        </b-navbar>
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
import MaterialIcon from '@/components/material-icon.vue';
import { EventBus } from './core/event-bus';

@Component({
    name: 'app',
    components: {
        MaterialIcon
    }
})
export default class App extends Vue {
    public isLoggedIn(): boolean {
        return Context.login != null;
    }

    public username(): string {
        if (Context.login == null) {
            throw new Error('No user is logged in!');
        }

        return Context.login.user.username;
    }

    public onLogout(): void {
        Context.login = null;
        EventBus.emit('logout');
        this.$forceUpdate();
    }
}
</script>
