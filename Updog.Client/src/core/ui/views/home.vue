<template>
    <layout>
        <template v-if="$posts != null">
            <post-summary v-for="post in $posts" v-bind:key="post.id" :post="post" />
            <pagination-nav :pagination="$posts.pagination" @previous="onPrevious" @next="onNext" />
        </template>
        <template slot="side-bar">
            <v-card class="pa-3">
                <v-card-actions>
                    <create-post-buttons />
                </v-card-actions>
            </v-card>
        </template>
    </layout>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import Layout from '@/core/ui/components/layout.vue';
import PostSummary from '@/post/ui/components/post-summary.vue';
import PaginationNav from '@/core/ui/components/pagination-nav.vue';
import { getModule } from 'vuex-module-decorators';
import { PaginationParams } from '@/core';
import { PostFinderMixin, Post } from '@/post';
import CreatePostButtons from '@/post/ui/components/create-post-buttons.vue';

/**
 * Home page that shows off the newests new posts.
 */
@Component({
    components: {
        CreatePostButtons,
        Layout,
        PostSummary,
        PaginationNav
    }
})
export default class Home extends PostFinderMixin {
    public currentPage: number = 0;

    public async mounted() {
        await this.refreshPosts();
    }

    public async onPrevious() {
        this.currentPage--;
        this.refreshPosts();
    }

    public async onNext() {
        this.currentPage++;
        this.refreshPosts();
    }

    public async refreshPosts() {
        await this.$findPostsByNew(new PaginationParams(this.currentPage, Post.DEFAULT_PAGE_SIZE));
    }
}
</script>
