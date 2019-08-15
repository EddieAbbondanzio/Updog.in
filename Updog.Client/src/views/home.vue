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
import MasterPage from '@/components/master-page.vue';
import { PostMixin } from '../post/mixins/post-mixin';
import { PostInfo } from '../post/common/post-info';
import { PaginationInfo } from '../core/pagination-info';
import PostSummary from '@/post/components/post-summary.vue';

@Component({
    components: {
        CreatePostButtons,
        MasterPage,
        PostSummary
    }
})
export default class Home extends PostMixin {
    public posts: PostInfo[] = [];

    public async created() {
        this.posts = await this.$findPostByNew(new PaginationInfo(0, 10));
    }
}
</script>
