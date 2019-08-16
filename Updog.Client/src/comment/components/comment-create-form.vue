<template>
    <b-form class="comment-create-form">
        <b-form-textarea class="mb-3" v-model="comment" width="480" />
        <b-button variant="primary" @click="onSubmit">Submit</b-button>
    </b-form>
</template>


<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';

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

    public onSubmit() {
        this.$emit('submit', this.comment);
    }
}
</script>