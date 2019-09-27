<template>
    <create-post-form @submit="onSubmit" />
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import { PostCreateParams } from '@/post/interactors/create/post-create-params';
import PostCreatorMixin from '@/post/mixins/post-creator-mixin';
import CreatePostForm from '@/post/ui/components/create-post-form.vue';

@Component({
    components: {
        CreatePostForm
    }
})
export default class Submit extends PostCreatorMixin {
    public async onSubmit(creationParams: PostCreateParams) {
        const result = await this.$createPost(creationParams);
        this.$redirectToPost(result.space.name, result.id);
    }
}
</script>
