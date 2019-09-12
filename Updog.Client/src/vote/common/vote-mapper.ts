import { Vote } from '../domain/vote';
import { Mapper } from '@/core/mapper';

/**
 * Mapper to rebuild a vote entity.
 */
export class VoteMapper implements Mapper<{ [key: string]: any }, Vote> {
    public map(source: { [key: string]: any }): Vote {
        return new Vote(source.resourceType, source.resourceId, source.direction);
    }
}
