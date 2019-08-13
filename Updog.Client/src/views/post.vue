<template>
    <master-page>
        <template>
            <div class="bg-light border" v-if="post != null">
                <h1>{{ post.title }}</h1>
                <p>{{ post.body }}</p>
                <p>{{ post.author}}</p>
            </div>
        </template>
        <template slot="side-bar">
            <create-post-buttons />
        </template>
        <template slot="footer">FOOTER!</template>
    </master-page>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import CreatePostButtons from '@/post/components/create-post-buttons.vue';
import MasterPage from '@/components/master-page.vue';
import { PostMixin } from '../post/mixins/post-mixin';
import { PostInfo } from '@/post/common/post-info';

/**
 * View a post via it's ID.
 */
@Component({
    components: {
        CreatePostButtons,
        MasterPage
    }
})
export default class Post extends PostMixin {
    public post: PostInfo | null = null;

    public async created() {
        const postId = Number.parseInt(this.$route.params.id, 10);
        this.post = await this.$findPostById(postId);
    }
}
</script>
