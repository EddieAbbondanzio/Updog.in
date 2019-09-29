<template>
    <div class="d-flex flex-row align-center">
        <comments-link v-if="isTextPost" :post="post">{{post.title}}</comments-link>
        <a :href="post.body" v-else>
            <slot>{{ post.title}}</slot>
        </a>

        <span class="caption grey--text text--darken-1">&nbsp;({{ hostName}})</span>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { Post } from '../../domain/post';
import { PostType } from '../../domain/post-type';
import { UrlUtils } from '@/core/utils/url-utils';
import CommentsLink from '@/comment/ui/components/comments-link.vue';

/**
 * Link to a post. Directs to the external resource if a post link,
 * otherwise points to the discussion page of the text post.
 */
@Component({
    name: 'post-link',
    components: {
        CommentsLink
    }
})
export default class PostLink extends Vue {
    /**
     * The post to link to.
     */
    @Prop()
    public post!: Post;

    /**
     * Check to see if the post is a text post.
     */
    get isTextPost() {
        return this.post.type === PostType.Text;
    }

    get hostName() {
        if (this.isTextPost) {
            return 'self';
        } else {
            return UrlUtils.getHostName(this.post.body);
        }
    }
}
</script>