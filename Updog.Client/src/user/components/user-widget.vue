<template>
    <div class="ml-auto">
        <div v-if="user == null">
            <b-button variant="outline-dark" to="login" class="mr-2">Log In</b-button>
            <b-button variant="outline-dark" to="signup" class="ml-2">Sign Up</b-button>
        </div>
        <div class="d-flex align-items-center flex-row" v-else>
            <material-icon
                icon="person"
                variant="dark"
                class="border border-dark rounded mr-2"
                size="md"
            />
            <h5 class="d-inline-block my-0">{{ user.username }}</h5>
            <span class="mx-1">|</span>

            <b-button variant="link" class="pl-0" @click="onLogout">Logout</b-button>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { Context } from '@/core/context';
import { EventBus } from '@/core/event-bus';
import MaterialIcon from '@/components/material-icon.vue';
import { User } from '../common/user';
import { UserLogin } from '../common/user-login';

/**
 * Component to handle logging in, and signing up new users.
 */
@Component({
    name: 'new-component',
    components: {
        MaterialIcon
    }
})
export default class UserWidget extends Vue {
    /**
     * The logged in user.
     */
    public user: User | null = null;

    public created(): void {
        EventBus.on('login', this.onLogin);

        if (Context.login != null) {
            this.user = Context.login.user;
        }
    }

    public destroyed(): void {
        EventBus.off('login', this.onLogin);
    }

    public async onLogin(login: UserLogin) {
        this.user = login.user;
    }

    public onLogout(): void {
        this.user = null;
        Context.login = null;
        EventBus.emit('logout');
    }
}
</script>