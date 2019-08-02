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

@Component({
    components: {
        UserLoginForm
    }
})
export default class Login extends Vue {}
</script>
