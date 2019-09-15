<template>
    <router-link
        :to="{name:'comments', params: { postId: post.id}}"
        v-if="isTextPost"
        :class="cssClass"
    >
        <slot>{{post.title}}</slot>
    </router-link>
    <a :href="post.body" :class="cssClass" v-else>
        <slot>{{ post.title}}</slot>
    </a>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { Post } from '../../domain/post';
import { PostType } from '../../domain/post-type';
import { ThemeableText } from '../../../core/ui/common/themeable/themeable-text';

/**
 * Link to a post. Directs to the external resource if a post link,
 * otherwise points to the discussion page of the text post.
 */
@Component({
    name: 'post-link'
})
export default class PostLink extends ThemeableText {
    /**
     * The post to link to.
     */
    @Prop()
    public post!: Post;

    /**
     * Check to see if the post is a text post.
     */
    get isTextPost() {
        return this.post.type == PostType.Text;
    }
}
</script>