<template>
    <router-link :to="{name:'comments', params: { postId: post.id}}" :class="cssClass">
        <slot>{{ post.commentCount == 1 ? `1 comment` : `${post.commentCount} comments` }}</slot>
    </router-link>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { Comment } from '../../domain/comment';
import { ThemeableText } from '@/core/ui/common/themeable/themeable-text';
import { Post } from '../../../post';

/**
 * Link to a post's comments.
 */
@Component({
    name: 'comment-link'
})
export default class CommentsLink extends ThemeableText {
    /**
     * The post to link to.
     */
    @Prop()
    public post!: Post;
}
</script>