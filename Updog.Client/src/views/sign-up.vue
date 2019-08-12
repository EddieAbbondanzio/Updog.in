<template>
    <master-page noSideBar="true">
        <b-container>
            <b-row>
                <b-col md="8" lg="6" offset-md="2" offset-lg="3">
                    <user-register-form @submit="onSubmit" />
                </b-col>
            </b-row>
        </b-container>
    </master-page>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import UserRegisterForm from '@/user/components/user-register-form.vue';
import { EventBus } from '@/core/event-bus';
import { User } from '@/user/common/user';
import { UserLogin } from '@/user/common/user-login';
import { Context } from '@/core/context';
import MasterPage from '@/components/master-page.vue';
import { UserMixin } from '../user/mixins/user-mixin';
import { UserRegistration } from '../user/common/user-registration';

@Component({
    components: {
        UserRegisterForm,
        MasterPage
    }
})
export default class SignUp extends UserMixin {
    public async onSubmit(userReg: UserRegistration) {
        const login = await this.$register(userReg);
        Context.login = login;
        this.$router.push({ name: 'home' });

        EventBus.emit('login', login);
    }
}
</script>
