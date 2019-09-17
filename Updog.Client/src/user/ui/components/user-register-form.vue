<template>
    <b-form class="bg-light border px-5 pt-5 pb-3 my-5">
        <div class="pb-3">
            <h1 class="pb-0 mb-0">Sign Up</h1>
            <h5 class="text-muted">
                Already a member?
                <router-link to="/login">Sign in.</router-link>
            </h5>
        </div>
        <b-form-group>
            <b-form-input
                type="text"
                placeholder="Username"
                v-model="username"
                ref="registerUsernameTextbox"
                name="registerUsername"
                v-validate="'required|min:4|isUnique'"
                v-b-popover.hover.focus.right="'Username must be at least 4 characters'"
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
                v-model="email"
                name="registerEmail"
                v-validate="'email'"
            />
            <b-form-invalid-feedback
                class="d-block"
                :state="false"
            >{{ errors.first('registerEmail')}}</b-form-invalid-feedback>
        </b-form-group>
        <b-form-group class="d-none">
            <b-form-input type="text" v-model="honey" placeholder="Name" />
        </b-form-group>
        <b-form-group>
            <b-form-input
                type="password"
                placeholder="Password"
                v-model="password"
                ref="registerPassword"
                name="registerPassword"
                v-validate="'required'"
                v-b-popover.hover.focus.right="'Password must be at least 8 characters'"
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
                v-model="confirmPassword"
                name="registerConfirmPassword"
                v-validate="'required|confirmed:registerPassword'"
            />
            <b-form-invalid-feedback
                class="d-block"
                :state="false"
            >{{ errors.first('registerConfirmPassword')}}</b-form-invalid-feedback>
        </b-form-group>

        <b-form-group class="form-buttons pt-3">
            <b-button variant="primary" @click="submit">Sign Up</b-button>
            <b-button variant="outline-primary" @click="reset">Reset</b-button>
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
import { Component, Vue, Mixins } from 'vue-property-decorator';
import { UserRegistration } from '@/user/domain/user-registration';
import { UserFinderMixin } from '../../mixins/user-finder-mixin';
import { mixins } from 'vue-class-component';
import { UserRegistrarMixin } from '@/user/mixins/user-registrar-mixin';
import { Form } from '../../../core';
import { UserLogin } from '../../domain/user-login';

/**
 * Login form for logging in users via username / password.
 */
@Component({
    name: 'user-login-form'
})
export default class UserRegisterForm extends UserRegistrarMixin implements Form<UserLogin | null> {
    public $refs!: {
        registerUsernameTextbox: HTMLInputElement;
    };

    public username: string = '';
    public email: string = '';
    public password: string = '';
    public confirmPassword: string = '';
    public honey: string = '';

    public created() {
        // Add way to check the backend for available username.
        this.$validator.extend('isUnique', async value => this.$isUsernameAvailable(value));

        this.$validator.localize('en', {
            custom: {
                registerUsername: {
                    required: 'Username is required.',
                    min: 'Username must be at least 4 characters.',
                    isUnique: 'Username is unavailable.'
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

    public mounted() {
        this.$refs.registerUsernameTextbox.focus();
    }

    /**
     * Attempt to log in the user.
     */
    public async submit() {
        if (this.honey !== '') {
            throw new Error('Oh bother');
        }

        // Validate first.
        if (!(await this.$validator.validate())) {
            return null;
        }

        const login = await this.$registerUser(new UserRegistration(this.username, this.password, this.email));

        this.$emit('register', login);
        return login;
    }

    /**
     * Reset the form back to initial state.
     */
    public reset() {
        this.username = '';
        this.email = '';
        this.password = '';
        this.confirmPassword = '';
        this.$validator.reset();
    }
}
</script>