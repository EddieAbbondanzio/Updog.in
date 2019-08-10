<template>
    <b-navbar variant="light" type="dark" class="border-bottom">
        <b-navbar-brand href="#" to="/">
            <img src="@/assets/logo.png" class="nav-icon" />
        </b-navbar-brand>

        <user-widget :user="user" @logout="onLogout()" />
    </b-navbar>
</template>

<style>
.nav-icon {
    height: 64px;
}
</style>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import UserWidget from '@/user/components/user-widget.vue';
import { User } from '../user/common/user';
import { EventBus } from '../core/event-bus';
import { UserLogin } from '../user/common/user-login';

/**
 * Nav bar atop the page. Shows the logged in user and brand image / name.
 */
@Component({
    name: 'nav-bar',
    components: {
        UserWidget
    }
})
export default class NavBar extends Vue {
    /**
     * The active user.
     */
    public user: User | null = null;

    public created(): void {
        EventBus.on('login', async (login: UserLogin) => {
            this.user = login.user;
        });
    }

    public onLogout(): void {
        this.user = null;
    }
}
</script>