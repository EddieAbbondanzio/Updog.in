<template>
    <div class="ml-auto">
        <div v-if="$login == null">
            <v-btn color="primary" :to="{ name:'login' }" class="mr-2">Log In</v-btn>
            <v-btn color="primary" outlined :to="{ name: 'signup' }" class="ml-2">Sign Up</v-btn>
        </div>
        <div v-else>
            <v-menu bottom left>
                <template v-slot:activator="{ on }">
                    <v-btn color="normal" text v-on="on" large>
                        <div class="d-flex align-center">
                            <v-avatar color="primary" size="36" :title="username" class="mr-2">
                                <v-icon color="white">person</v-icon>
                            </v-avatar>

                            <span class="title text-none">{{ username}}</span>

                            <v-icon color="grey darken-3" x-large>arrow_drop_down</v-icon>
                        </div>
                    </v-btn>
                </template>

                <v-list>
                    <div class="px-4 pt-4 d-flex flex-column align-center">
                        <v-avatar color="primary" size="48" :title="username" class="mb-2 d-block">
                            <v-icon color="white">person</v-icon>
                        </v-avatar>
                        <span class="title d-block">{{username}}</span>
                        <span class="subtitle" :title="karmaDetails">{{ karma }}&nbsp;karma</span>
                    </div>
                    <v-divider />

                    <!-- Options -->
                    <v-list-item
                        :link="true"
                        :to="{ name: 'user', params: { username: username}}"
                        class="text-decoration-none"
                    >
                        <v-list-item-title>Profile</v-list-item-title>
                        <v-list-item-icon>
                            <v-icon>person</v-icon>
                        </v-list-item-icon>
                    </v-list-item>
                    <v-list-item :link="true" class="text-decoration-none" @click="onLogout">
                        <v-list-item-title>Logout</v-list-item-title>
                        <v-list-item-icon>
                            <v-icon color="red">exit_to_app</v-icon>
                        </v-list-item-icon>
                    </v-list-item>
                </v-list>
            </v-menu>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import UserLink from '@/user/ui/components/user-link.vue';
import { AuthenticatedMixin } from '@/user';
import { UserLoginMixin } from '../../mixins/user-login-mixin';
import Cookie from 'js-cookie';
import { NumberUtils } from '../../../core';

/**
 * Component to handle logging in, and signing up new users.
 */
@Component({
    name: 'new-component',
    components: {
        UserLink
    }
})
export default class UserWidget extends UserLoginMixin {
    get username() {
        return this.$login!.user.username;
    }

    get karma() {
        return NumberUtils.formatWithK(this.$login!.user.postKarma + this.$login!.user.commentKarma);
    }

    get karmaDetails() {
        return `${NumberUtils.formatWithK(this.$login!.user.postKarma)} post karma \n${NumberUtils.formatWithK(
            this.$login!.user.commentKarma
        )} comment karma`;
    }

    public async onLogout() {
        Cookie.remove('auth');
        await this.$logoutUser();
    }
}
</script>