<template>
    <master-page>
        <template>
            <div class="bg-light border mb-3 p-1" v-for="post in posts" v-bind:key="post.id">
                <h4 class="mb-0">
                    <router-link :to="`post/${post.id}`">{{ post.title }}</router-link>
                </h4>
                <p class="text-muted">Posted {{ post.getDifferenceDate() }} by {{ post.author }}</p>
            </div>
        </template>
        <template slot="side-bar">
            <create-post-buttons />
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

@Component({
    components: {
        CreatePostButtons,
        MasterPage
    }
})
export default class Home extends PostMixin {
    public posts: PostInfo[] = [];

    public async created() {
        this.posts = await this.$findPostByNew(new PaginationInfo(0, 10));
    }
}
</script>
