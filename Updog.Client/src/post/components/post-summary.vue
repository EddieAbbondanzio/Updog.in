<template>
    <div class="bg-light border mb-2 px-3 py-1">
        <div v-if="post != null">
            <h4 class="mb-0">
                <router-link :to="`post/${post.id}`" v-if="isTextPost()">{{ post.title }}</router-link>
                <a :href="`//${this.post.body}`" v-else>{{ post.title }}</a>
            </h4>
            <p class="text-muted">Posted {{ post.getDifferenceDate() }} by {{ post.author }}</p>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { PostInfo } from '../common/post-info';
import { PostType } from '../common/post-type';

/**
 * Summary of information about a post.
 */
@Component({
    name: 'post-summary'
})
export default class NewComponent extends Vue {
    /**
     * The post to display.
     */
    @Prop({ default: null })
    public post!: PostInfo | null;

    /**
     * Check to see if a post is a text post.
     */
    public isTextPost() {
        if (this.post == null) {
            throw new Error('No post!');
        }

        return this.post.type === PostType.Text;
    }
}
</script>