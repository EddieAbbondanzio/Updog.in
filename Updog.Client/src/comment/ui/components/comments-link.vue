<template>
    <router-link :to="{name:'comments', params: { postId: post.id, spaceName: post.space.name}}">
        <slot>
            <span
                class="caption grey--text text--darken-3"
            >{{ post.commentCount == 1 ? `1 comment` : `${post.commentCount} comments` }}</span>
        </slot>
    </router-link>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { Comment } from '../../domain/comment';
import { Post } from '../../../post';

/**
 * Link to a post's comments.
 */
@Component({
    name: 'comments-link'
})
export default class CommentsLink extends Vue {
    /**
     * The post to link to.
     */
    @Prop()
    public post!: Post;
}
</script>