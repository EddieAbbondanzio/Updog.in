import { Module, VuexModule, Mutation, Action } from 'vuex-module-decorators';
import { User } from '../domain/user';
import { UserCredentials } from '../domain/user-credentials';
import { UserLoginInteractor } from '../interactors/login/user-login-interactor';
import { UserLogin } from '../domain/user-login';
import { UserRegisterInteractor } from '../interactors/register/user-register-interactor';
import { UserFinderByUsername } from '../interactors/find-by-username/user-finder-by-username';
import { UserRegistration } from '../domain/user-registration';
import { UserMutation } from './user-mutation';
import { StoreName } from '@/core/store/store-name';

/**
 * Vuex store module for managing user data.
 */
@Module({ namespaced: true, name: StoreName.User })
export default class UserStore extends VuexModule {
    /**
     * The actively logged in user.
     */
    public userLogin: UserLogin | null = null;

    public users: User[] = [];

    /**
     * Get the auth token for the currently logged in user.
     */
    get authToken() {
        if (this.userLogin == null) {
            return '';
        }

        return this.userLogin.authToken;
    }

    /**
     * Set a log in in the store module.
     * @param login The login to set.
     */
    @Mutation
    public [UserMutation.SetLogin](login: UserLogin) {
        this.userLogin = login;
    }

    /**
     * Clear the active login.
     */
    @Mutation
    public [UserMutation.ClearLogin]() {
        this.userLogin = null;
    }

    @Mutation
    public [UserMutation.CacheUser](user: User) {
        if (this.users.findIndex(u => u.id === user.id) === -1) {
            this.users.push(user);
        }
    }

    /**
     * Find a user via their username.
     * @param username The username to look for.
     */
    @Action({ rawError: true })
    public async findByUsername(username: string) {
        const u = await new UserFinderByUsername().handle(username);
        this.context.commit(UserMutation.CacheUser, u);
    }

    /**
     * Log in an existing user with the backend.
     * @param userCreds The username and password to log in with.
     */
    @Action({ rawError: true })
    public async login(userCreds: UserCredentials) {
        const login = await new UserLoginInteractor().handle(userCreds);
        this.context.commit(UserMutation.SetLogin, login);
    }

    /**
     * Log out the active user.
     */
    @Action({ rawError: true })
    public async logout() {
        this.context.commit(UserMutation.ClearLogin);
    }

    /**
     * Register a new user with the backend.
     * @param userReg The user registration data
     */
    @Action({ rawError: true })
    public async register(userReg: UserRegistration) {
        const login = await new UserRegisterInteractor().handle(userReg);
        this.context.commit(UserMutation.SetLogin, login);
    }
}
