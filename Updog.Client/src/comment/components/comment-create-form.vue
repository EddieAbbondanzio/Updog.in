<template>
    <b-form class="comment-create-form">
        <b-form-group>
            <b-form-textarea
                class="mb-3"
                v-model="comment"
                width="480"
                name="commentCreateTextArea"
                v-validate="`required|max:${getMaxLength()}`"
            />
            <b-form-invalid-feedback
                class="d-block"
                :state="false"
            >{{ errors.first('commentCreateTextArea')}}</b-form-invalid-feedback>
        </b-form-group>
        <b-button variant="primary" @click="onSubmit">Submit</b-button>
    </b-form>
</template>


<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { Comment } from '@/comment/common/comment';

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

    public onSubmit() {
        this.$emit('submit', this.comment);
    }

    public getMaxLength() {
        return Comment.BODY_MAX_LENGTH;
    }
}
</script>