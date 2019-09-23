<template>
    <v-form class="comment-create-form">
        <v-textarea
            label="Type your comment"
            class="mb-3"
            v-model="comment"
            width="480"
            name="commentCreateTextArea"
            v-validate="`required|max:${getMaxLength()}`"
            :error="errors.first('commentCreateTextArea') != null"
            :error-messages="errors.first('commentCreateTextArea')"
        />
        <v-btn color="primary" @click="onSubmit">Submit</v-btn>
    </v-form>
</template>

<style scoped>
.comment-create-form {
    max-width: 720px;
}
</style>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { Comment } from '@/comment';

/**
 * Form to create a new comment.
 */
@Component({
    name: 'comment-create-form'
})
export default class CommentCreateForm extends Vue {
    /**
     * The content of the comment.
     */
    public comment: string = '';

    public created() {
        this.$validator.localize('en', {
            custom: {
                commentCreateTextArea: {
                    required: 'Comment body is required.',
                    max: 'Comment body must be 10,000 characters or less.'
                }
            }
        });
    }

    public async onSubmit() {
        // Validate first.
        if (!(await this.$validator.validate())) {
            return;
        }
        this.$emit('submit', this.comment);
        this.clear();
    }

    public clear(): void {
        this.comment = '';
        this.$validator.reset();
    }

    public getMaxLength() {
        return Comment.BODY_MAX_LENGTH;
    }
}
</script>