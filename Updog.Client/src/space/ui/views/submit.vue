<template>
    <create-post-form @submit="onSubmit" />
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import CreatePostForm from '@/post/ui/components/create-post-form.vue';
import { PostCreateParams, PostCreatorMixin } from '@/post';

@Component({
    components: {
        CreatePostForm
    }
})
export default class Submit extends PostCreatorMixin {
    public async onSubmit(creationParams: PostCreateParams) {
        const result = await this.$createPost(creationParams);
        this.$redirectToPost(result.id);
    }
}
</script>
