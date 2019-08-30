import { Module, VuexModule, Mutation, Action } from 'vuex-module-decorators';
import { User } from '../common/user';
import { UserCredentials } from '../common/user-credentials';
import { UserLoginInteractor } from '../use-cases/login/user-login-interactor';
import { UserLogin } from '../common/user-login';
import { UserRegisterInteractor } from '../use-cases/register/user-register-interactor';
import { UserFinderByUsername } from '../use-cases/find-by-username/user-finder-by-username';
import { UserRegistration } from '../common/user-registration';
import { UserMutation } from './user-mutation';

/**
 * Vuex store module for managing user data.
 */
@Module({ namespaced: true, name: 'user' })
export default class UserModule extends VuexModule {
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
    @Action
    public async findByUsername(username: string) {
        const u = await new UserFinderByUsername().handle(username);
        this.context.commit(UserMutation.CacheUser, u);
    }

    /**
     * Log in an existing user with the backend.
     * @param userCreds The username and password to log in with.
     */
    @Action
    public async login(userCreds: UserCredentials) {
        const login = await new UserLoginInteractor().handle(userCreds);
        this.context.commit(UserMutation.SetLogin, login);
    }

    /**
     * Log out the active user.
     */
    @Action
    public async logout() {
        this.context.commit(UserMutation.ClearLogin);
    }

    /**
     * Register a new user with the backend.
     * @param userReg The user registration data
     */
    @Action
    public async register(userReg: UserRegistration) {
        const login = await new UserRegisterInteractor().handle(userReg);
        this.context.commit(UserMutation.SetLogin, login);
    }
}
