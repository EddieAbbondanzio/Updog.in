<template>
    <master-page>
        <template v-if="$posts != null">
            <post-summary v-for="post in $posts" v-bind:key="post.id" :post="post" />
            <pagination-navigation
                :pagination="$posts.pagination"
                @previous="onPrevious"
                @next="onNext"
            />
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
import { PostFinderMixin } from '../post/mixins/post-finder-mixin';
import { PaginationParams } from '../core/pagination/pagination-params';
import PostSummary from '@/post/components/post-summary.vue';
import { Post } from '../post/common/post';
import { PagedResultSet } from '../core/pagination/paged-result-set';
import PaginationNavigation from '@/core/components/pagination-navigation.vue';
import { UserCredentials } from '../user/common/user-credentials';
import { getModule } from 'vuex-module-decorators';
import UserModule from '../user/store/user-module';
import { UserLogin } from '../user/common/user-login';
import PostModule from '../post/store/post-module';
import { PostCreateParams } from '../post/use-cases/create/post-create-params';
import { PostType } from '../post/common/post-type';
import { PaginationInfo } from '../core/pagination/pagination-info';

/**
 * Home page that shows off the newests new posts.
 */
@Component({
    components: {
        CreatePostButtons,
        MasterPage,
        PostSummary,
        PaginationNavigation
    }
})
export default class Home extends PostFinderMixin {
    public currentPage: number = 0;

    public async mounted() {
        // console.log(await this.postStore.create(null!));
        await this.refreshPosts();
        // this.$store.state.user.test;
        // console.log(await getModule(UserModule, this.$store).login(new UserCredentials('fake', 'password')));
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
        await this.$findByNew(new PaginationParams(this.currentPage, Post.DEFAULT_PAGE_SIZE));
    }
}
</script>
