<template>
    <master-page>
        <template>
            <post-summary v-for="post in posts" v-bind:key="post.id" :post="post" />
        </template>
        <template slot="side-bar">
            <div class="bg-light border p-3">
                <create-post-buttons />
            </div>
        </template>
    </master-page>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import CreatePostButtons from '@/post/components/create-post-buttons.vue';
import MasterPage from '@/core/components/master-page.vue';
import { PostMixin } from '../post/mixins/post-mixin';
import { PaginationInfo } from '../core/pagination-info';
import PostSummary from '@/post/components/post-summary.vue';
import { Post } from '../post/common/post';

@Component({
    components: {
        CreatePostButtons,
        MasterPage,
        PostSummary
    }
})
export default class Home extends PostMixin {
    public posts: Post[] = [];

    public async created() {
        this.posts = await this.$findPostsByNew(new PaginationInfo(0, Post.DEFAULT_PAGE_SIZE));
    }
}
</script>
