import { ApiInteractor } from '@/core/api-interactor';
import { UserRegistration } from '../common/user-registration';
import { UserLogin } from '../common/user-login';
import { User } from '../common/user';

/**
 * Interactor to handle registering a user with the back end.
 */
export class UserRegisterInteractor extends ApiInteractor<UserRegistration, UserLogin> {
    public async handle(input: UserRegistration): Promise<UserLogin> {
        const response = await this.http.post<UserLogin>('/user/', input);

        return new UserLogin(
            new User(response.data.user.username, response.data.user.joinedDate, response.data.user.email),
            response.data.authToken
        );
    }
}
