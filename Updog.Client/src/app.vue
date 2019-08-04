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
            <div class="ml-auto" v-else>REEE</div>
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

@Component({
    name: 'app',
    components: {}
})
export default class App extends Vue {
    public isLoggedIn(): boolean {
        return User.CURRENT != null;
    }
}
</script>
