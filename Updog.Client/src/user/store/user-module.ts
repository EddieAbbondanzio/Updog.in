import { Module, VuexModule, Mutation, Action, MutationAction } from 'vuex-module-decorators';
import { User } from '../common/user';
import { UserCredentials } from '../common/user-credentials';
import { UserLoginInteractor } from '../use-cases/login/user-login-interactor';
import { UserLogin } from '../common/user-login';
import { UserRegisterInteractor } from '../use-cases/register/user-register-interactor';
import { UserFinderByUsername } from '../use-cases/find-by-username/user-finder-by-username';
import { UserRegistration } from '../common/user-registration';

/**
 * Vuex store module for managing user data.
 */
@Module({ namespaced: true, name: 'user' })
export default class UserModule extends VuexModule {
    private userLogin: UserLogin | null = null;

    /**
     * Set a log in in the store module.
     * @param login The login to set.
     */
    @Mutation
    public setLogin(login: UserLogin) {
        this.userLogin = login;
    }

    /**
     * Find a user via their username.
     * @param username The username to look for.
     */
    @Action
    public async findByUsername(username: string) {
        return new UserFinderByUsername().handle(username);
    }

    /**
     * Log in an existing user with the backend.
     * @param userCreds The username and password to log in with.
     */
    @Action
    public async login(userCreds: UserCredentials) {
        const login = await new UserLoginInteractor().handle(userCreds);
        this.context.commit('setLogin', login);

        return login;
    }

    /**
     * Register a new user with the backend.
     * @param userReg The user registration data
     */
    @Action
    public async register(userReg: UserRegistration) {
        const login = await new UserRegisterInteractor().handle(userReg);
        this.context.commit('setLogin', login);

        return login;
    }
}
