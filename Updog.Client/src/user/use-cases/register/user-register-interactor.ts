import { ApiInteractor } from '@/core/api-interactor';
import { UserRegistration } from './user-registration';
import { UserLogin } from '../../common/user-login';
import { User } from '../../common/user';
import { UserMapper } from '@/user/common/user-mapper';
import { UserApiInteractor } from '@/user/common/user-api-interactor';

/**
 * Interactor to handle registering a user with the back end.
 */
export class UserRegisterInteractor extends UserApiInteractor<UserRegistration, UserLogin> {
    public async handle(input: UserRegistration): Promise<UserLogin> {
        const response = await this.http.post<UserLogin>('/user/', input);

        return new UserLogin(this.userMapper.map(response.data.user), response.data.authToken);
    }
}
