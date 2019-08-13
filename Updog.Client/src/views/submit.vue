<template>
    <master-page>
        <template>
            <create-post-form @submit="onSubmit" />
        </template>
        <!-- <template slot="side-bar">SIDE BAR</template> -->
        <!-- <template slot="footer">FOOTER!</template> -->
    </master-page>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import MasterPage from '@/components/master-page.vue';
import CreatePostForm from '@/post/components/create-post-form.vue';
import { PostCreateParams } from '../post/common/post-create-params';
import { PostMixin } from '@/post/mixins/post-mixin';

@Component({
    components: {
        MasterPage,
        CreatePostForm
    }
})
export default class Home extends PostMixin {
    public async onSubmit(creationParams: PostCreateParams) {
        const result = await this.$createPost(creationParams);
        this.$router.push(`/post/${result.id}`);
    }
}
</script>
