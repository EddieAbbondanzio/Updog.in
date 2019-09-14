import { SpaceApiInteractor } from '@/space/infrastructure/space-api-interactor';
import { Space } from '@/space/domain/space';

/**
 * Interactor to find the default spaces.
 */
export class SpaceFinderByDefault extends SpaceApiInteractor<void, Space[]> {
    public async handle(input: void): Promise<Space[]> {
        const response = await this.http.get<Space[]>('/space/default');
        return response.data.map((s: any) => this.spaceMapper.map(s));
    }
}
