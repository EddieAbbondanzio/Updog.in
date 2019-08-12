<template>
    <master-page>
        <b-container>
            <b-row>
                <b-col md="8" lg="6" offset-md="2" offset-lg="3">
                    <user-login-form @submit="onSubmit" />
                </b-col>
            </b-row>
        </b-container>
    </master-page>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import UserLoginForm from '@/user/components/user-login-form.vue';
import { EventBus } from '../core/event-bus';
import { UserLogin } from '../user/common/user-login';
import { User } from '../user/common/user';
import { Context } from '@/core/context';
import MasterPage from '@/components/master-page.vue';
import { UserMixin } from '../user/mixins/user-mixin';
import { UserCredentials } from '../user/common/user-credentials';

@Component({
    components: {
        UserLoginForm,
        MasterPage
    }
})
export default class Login extends UserMixin {
    public async onSubmit(creds: UserCredentials) {
        const login = await this.$login(creds);
        Context.login = login;
        this.$router.push({ name: 'home' });
        EventBus.emit('login', login);
    }
}
</script>
