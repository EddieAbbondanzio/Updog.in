<template>
    <master-page>
        <b-container>
            <b-row>
                <b-col md="8" lg="6" offset-md="2" offset-lg="3">
                    <user-register-form />
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

@Component({
    components: {
        UserRegisterForm,
        MasterPage
    }
})
export default class SignUp extends Vue {
    public created() {
        EventBus.on('login', async (login: UserLogin) => {
            Context.login = login;
            this.$router.push({ name: 'home' });
        });
    }
}
</script>
