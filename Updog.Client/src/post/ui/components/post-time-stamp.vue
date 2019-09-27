<template>
    <div>
        Posted
        <time-stamp :date="post.creationDate" :modified="post.wasUpdated" />&nbsp;by
        <user-link :user="post.user" variant="primary" />

        <span v-if="showSpace">
            to
            <space-link :space="post.space" :showPrefix="true" />
        </span>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { Post } from '../../domain/post';
import TimeStamp from '@/core/ui/components/time-stamp.vue';
import UserLink from '@/user/ui/components/user-link.vue';
import SpaceLink from '@/space/ui/components/space-link.vue';

/**
 * A really "fancy" time stamp that shows when, where, and by whom a post
 * was made by.
 */
@Component({
    name: 'post-time-stamp',
    components: {
        TimeStamp,
        UserLink,
        SpaceLink
    }
})
export default class PostTimeStamp extends Vue {
    /**
     * The post to work with.
     */
    @Prop()
    public post!: Post;

    /**
     * If the space it was posted to should be shown.
     */
    @Prop({ default: true })
    public showSpace!: boolean;
}
</script>