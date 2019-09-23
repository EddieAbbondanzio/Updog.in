<template>
    <layout>
        <template>
            <create-post-form @submit="onSubmit" />
        </template>
    </layout>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import Layout from '@/core/ui/components/layout.vue';
import CreatePostForm from '@/post/ui/components/create-post-form.vue';
import { PostCreateParams, PostCreatorMixin } from '@/post';

@Component({
    components: {
        Layout,
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
