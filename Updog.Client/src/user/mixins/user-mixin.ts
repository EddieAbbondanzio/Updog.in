import Vue from 'vue';
import { UserRegistration } from '../use-cases/register/user-registration';
import { UserLogin } from '../common/user-login';
import { UserCredentials } from '../common/user-credentials';
import { UserLoginInteractor } from '../use-cases/login/user-login-interactor';
import { UserRegisterInteractor } from '../use-cases/register/user-register-interactor';
import Component from 'vue-class-component';
import { UserFinderByUsername } from '../use-cases/find-by-username/user-finder-by-username';
import { User } from '../common/user';

/**
 * Mixin for user logic.
 */
@Component
export class UserMixin extends Vue {
    /**
     * Find a user by their unique username.
     * @param username The username to look for.
     */
    public async $findUserByUsername(username: string): Promise<User> {
        return new UserFinderByUsername().handle(username);
    }

    /**
     * Log in an existing user.
     * @param creds The user's username and password.
     */
    public async $login(creds: UserCredentials): Promise<UserLogin> {
        return new UserLoginInteractor().handle(creds);
    }

    /**
     * Register a new user with the site.
     * @param reg The user's registration.
     */
    public async $register(reg: UserRegistration): Promise<UserLogin> {
        return new UserRegisterInteractor().handle(reg);
    }
}
