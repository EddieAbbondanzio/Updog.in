<template>
    <master-page noSideBar="true">
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
import MasterPage from '@/core/components/master-page.vue';
import { UserCredentials } from '../user/common/user-credentials';
import { UserLoginMixin } from '../user/mixins/user-login-mixin';

@Component({
    components: {
        UserLoginForm,
        MasterPage
    }
})
export default class Login extends UserLoginMixin {
    public async onSubmit(creds: UserCredentials) {
        const login = await this.$login(creds);
        Context.login = login;
        this.$router.push({ name: 'home' });
        EventBus.emit('login', login);
    }
}
</script>
