import Vue from 'vue';
import { UserRegistration } from '../common/user-registration';
import { UserLogin } from '../common/user-login';
import { UserCredentials } from '../common/user-credentials';
import { UserLoginInteractor } from '../use-cases/user-login-interactor';
import { UserRegisterInteractor } from '../use-cases/user-register-interactor';
import Component from 'vue-class-component';

/**
 * Mixin for user logic.
 */
@Component
export class UserMixin extends Vue {
    /**
     * Log in an existing user.
     * @param creds The user's username and password.
     */
    public $login(creds: UserCredentials): Promise<UserLogin> {
        return new UserLoginInteractor().handle(creds);
    }

    /**
     * Register a new user with the site.
     * @param reg The user's registration.
     */
    public $register(reg: UserRegistration): Promise<UserLogin> {
        return new UserRegisterInteractor().handle(reg);
    }
}
