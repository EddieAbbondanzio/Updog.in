<template>
    <div class="ml-auto">
        <div v-if="$login == null">
            <b-button variant="primary" :to="{ name:'login' }" class="mr-2">Log In</b-button>
            <b-button variant="outline-primary" :to="{ name: 'signup' }" class="ml-2">Sign Up</b-button>
        </div>
        <div class="d-flex align-items-center flex-row" v-else>
            <material-icon
                icon="person"
                variant="dark"
                class="border border-dark rounded mr-2"
                size="md"
            />
            <h5 class="d-inline-block my-0">
                <user-link :user="$login.user" />
            </h5>
            <span class="mx-1">|</span>

            <b-button variant="link" class="pl-0" @click="$logoutUser()">Logout</b-button>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import UserLink from '@/user/ui/components/user-link.vue';
import { AuthenticatedMixin } from '@/user';

/**
 * Component to handle logging in, and signing up new users.
 */
@Component({
    name: 'new-component',
    components: {
        UserLink
    }
})
export default class UserWidget extends AuthenticatedMixin {}
</script>