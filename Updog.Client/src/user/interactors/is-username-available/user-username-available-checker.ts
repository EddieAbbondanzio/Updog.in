import { UserApiInteractor } from '@/user/infrastructure/user-api-interactor';

/**
 * Interactor to check if a username is available for use.
 */
export class UserUsernameAvailableChecker extends UserApiInteractor<string, boolean> {
    public async handle(username: string): Promise<boolean> {
        try {
            await this.http.head(`/user/${username}`);
            return false;
        } catch (error) {
            switch (error.response.status) {
                // Banned username
                case 400:
                    return false;
                // No existing user found, username is free.
                case 404:
                    return true;
                // Something went REALLY wrong.
                default:
                    throw error;
            }
        }
    }
}
