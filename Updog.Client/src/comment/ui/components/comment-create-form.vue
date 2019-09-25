<template>
    <v-form class="comment-create-form">
        <v-textarea
            ref="bodyTextarea"
            label="Type your comment"
            class="mb-3"
            v-model="comment"
            width="480"
            name="commentCreateTextArea"
            v-validate="`required|max:${getMaxLength()}`"
            :error="errors.first('commentCreateTextArea') != null"
            :error-messages="errors.first('commentCreateTextArea')"
        />
        <v-btn color="primary" @click="onSubmit" class="mr-3">Submit</v-btn>
        <v-btn v-show="parentId !== 0" color="error" outlined @click="onCancel">Cancel</v-btn>
    </v-form>
</template>

<style scoped>
.comment-create-form {
    max-width: 720px;
}
</style>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { Comment, CommentCreateParams } from '@/comment';
import { CommentCreatorMixin } from '../../mixins/comment-creator-mixin';

/**
 * Form to create a new comment.
 */
@Component({
    name: 'comment-create-form'
})
export default class CommentCreateForm extends CommentCreatorMixin {
    public $refs!: {
        bodyTextarea: HTMLTextAreaElement;
    };

    get postId() {
        return Number.parseInt(this.$route.params.postId, 10);
    }

    @Prop({ default: 0 })
    public parentId!: number;

    /**
     * The content of the comment.
     */
    public comment: string = '';

    public created() {
        // Check to see if we have a cached comment from earlier?
        if (this.$cachedCommentInProgress != null) {
            this.comment = this.$cachedCommentInProgress;
            this.$clearCommentInProgress();
        }

        this.$validator.localize('en', {
            custom: {
                commentCreateTextArea: {
                    required: 'Comment body is required.',
                    max: 'Comment body must be 10,000 characters or less.'
                }
            }
        });
    }

    /**
     * Activate the form.
     */
    public focus() {
        this.$refs.bodyTextarea.focus();
    }

    public async onSubmit() {
        // Validate first.
        if (!(await this.$validator.validate())) {
            return;
        }

        if (!this.$isLoggedIn()) {
            this.$cacheCommentInProgress(this.comment);
            this.$redirectToLogin();
            return;
        }

        await this.$createComment(new CommentCreateParams(this.comment, this.postId, this.parentId));

        this.$emit('submit', this.comment);
        this.clear();
    }

    public async onCancel() {
        this.$emit('cancel');
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