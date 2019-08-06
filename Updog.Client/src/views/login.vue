<template>
    <b-container>
        <b-row>
            <b-col md="8" lg="6" offset-md="2" offset-lg="3">
                <user-login-form />
            </b-col>
        </b-row>
    </b-container>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import UserLoginForm from '@/user/components/user-login-form.vue';
import { EventBus } from '../core/event-bus';
import { UserLogin } from '../user/common/user-login';
import { User } from '../user/common/user';
import { Context } from '@/core/context';

@Component({
    components: {
        UserLoginForm
    }
})
export default class Login extends Vue {
    public created() {
        EventBus.on('login', async (login: UserLogin) => {
            Context.login = login;
            this.$router.push('home');
        });
    }
}
</script>
