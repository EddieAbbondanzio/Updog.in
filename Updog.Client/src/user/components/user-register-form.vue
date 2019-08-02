<template>
    <b-form class="bg-light border px-5 pt-5 pb-3 my-5">
        <h1 class="pb-3">Sign Up</h1>
        <b-form-group>
            <b-form-input
                type="text"
                placeholder="Username"
                v-model="registerUsername"
                name="registerUsername"
                v-validate="'required|min:4'"
            />
            <b-form-invalid-feedback
                class="d-block"
                :state="false"
            >{{ errors.first('registerUsername')}}</b-form-invalid-feedback>
        </b-form-group>
        <b-form-group>
            <b-form-input
                type="email"
                placeholder="Email"
                v-model="registerEmail"
                name="registerEmail"
                v-validate="'email'"
            />
            <b-form-invalid-feedback
                class="d-block"
                :state="false"
            >{{ errors.first('registerEmail')}}</b-form-invalid-feedback>
        </b-form-group>
        <b-form-group>
            <b-form-input
                type="password"
                placeholder="Password"
                v-model="registerPassword"
                ref="registerPassword"
                name="registerPassword"
                v-validate="'required'"
            />
            <b-form-invalid-feedback
                class="d-block"
                :state="false"
            >{{ errors.first('registerPassword')}}</b-form-invalid-feedback>
        </b-form-group>
        <b-form-group>
            <b-form-input
                type="password"
                placeholder="Confirm password"
                v-model="registerConfirmPassword"
                name="registerConfirmPassword"
                v-validate="'required|confirmed:registerPassword'"
            />
            <b-form-invalid-feedback
                class="d-block"
                :state="false"
            >{{ errors.first('registerConfirmPassword')}}</b-form-invalid-feedback>
        </b-form-group>

        <b-form-group class="form-buttons pt-3">
            <b-button variant="primary" @click="onRegister">Sign Up</b-button>
            <b-button variant="outline-primary" @click="onReset">Reset</b-button>
        </b-form-group>
    </b-form>
</template>

<style scoped>
.form-buttons button {
    margin-left: 8px;
    margin-right: 8px;
}
</style>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import { UserRegistration } from '@/user/common/user-registration';
import { UserService } from '@/user/user-service';

/**
 * Login form for logging in users via username / password.
 */
@Component({
    name: 'user-login-form',
    components: {}
})
export default class UserRegisterForm extends Vue {
    public registerUsername: string = '';
    public registerEmail: string = '';
    public registerPassword: string = '';
    public registerConfirmPassword: string = '';

    public created() {
        this.$validator.localize('en', {
            custom: {
                registerUsername: {
                    required: 'Username is required.',
                    min: 'Username must be at least 4 characters.'
                },
                registerEmail: {
                    email: 'Email must be a valid address.'
                },
                registerPassword: {
                    required: 'Password is required',
                    min: 'Password must be at least 8 characters.'
                },
                registerConfirmPassword: {
                    required: 'Confirm password is required',
                    confirmed: 'Passwords do not match.'
                }
            }
        });
    }

    /**
     * Attempt to log in the user.
     */
    public async onRegister() {
        // Validate first.
        if (!(await this.$validator.validate())) {
            return;
        }

        const userReg: UserRegistration = new UserRegistration(
            this.registerUsername,
            this.registerPassword,
            this.registerEmail
        );

        const service: UserService = new UserService();
        await service.register(userReg);
    }

    public onReset() {
        this.registerUsername = '';
        this.registerEmail = '';
        this.registerPassword = '';
        this.registerConfirmPassword = '';
        this.$validator.reset();
    }
}
</script>
