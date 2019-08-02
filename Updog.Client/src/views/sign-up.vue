<template>
    <b-container>
        <b-row>
            <b-col md="8" lg="6" offset-md="2" offset-lg="3">
                <user-register-form />
            </b-col>
        </b-row>
    </b-container>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import UserLoginForm from '@/user/components/user-register-form.vue';

@Component({
    components: {
        UserLoginForm
    }
})
export default class SignUp extends Vue {}
</script>
