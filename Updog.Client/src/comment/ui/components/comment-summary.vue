<template>
    <div class="py-1">
        <!-- Comment -->
        <div class="d-flex flex-row">
            <div>
                <comment-vote-controller :comment="comment" v-if="isExpanded" />
                <div style="width: 24px;" v-else>&nbsp;</div>
            </div>

            <div>
                <!-- Header -->
                <div class="d-flex flex-row align-items-center">
                    <comment-expand-button @toggle="onExpand" />&nbsp;
                    <user-link :user="comment.user" />&nbsp;
                    <span
                        class="text-muted pr-1"
                    >{{ comment.karma}} {{comment.karma === 1 ? 'upvote' : 'upvotes'}}</span>

                    <time-stamp
                        :date="comment.creationDate"
                        :modified="comment.wasUpdated"
                        class="text-muted"
                    />
                </div>

                <!-- Body -->
                <div v-if="isExpanded">
                    <div v-show="!isEditing">{{ comment.body}}</div>
                    <div v-show="isEditing">
                        <textarea
                            v-model="editedBody"
                            name="editCommentBody"
                            v-validate="'required|max:10000'"
                        />
                        <b-form-invalid-feedback
                            class="d-block"
                            :state="false"
                        >{{ errors.first('editCommentBody')}}</b-form-invalid-feedback>
                        <b-button variant="primary" @click="onEditSave">Save</b-button>
                        <b-button variant="outline-primary" @click="onEditCancel">Cancel</b-button>
                    </div>

                    <!-- Actions -->
                    <div class="d-flex flex-row">
                        <!-- Permalink -->
                        <b-button
                            variant="link"
                            class="text-secondary pl-0 pr-1"
                            :to="{name: 'comment', params: { commentId: comment.id}}"
                        >permalink</b-button>

                        <b-button
                            variant="link"
                            class="text-secondary pl-0 pr-1"
                            v-if="canEdit()"
                            @click="onEditClick"
                        >edit</b-button>

                        <!-- Reply -->
                        <b-button
                            variant="link"
                            class="text-secondary pl-0 pr-1"
                            @click="onReplyClick"
                        >reply</b-button>
                    </div>

                    <!-- Reply Box -->
                    <div v-if="isReplying">
                        <comment-create-form @submit="onReplySubmit" />
                    </div>
                </div>
            </div>
        </div>

        <!-- Children -->
        <div v-if="isExpanded">
            <comment-summary
                v-for="child in comment.children"
                :comment="child"
                v-bind:key="child.id"
                class="pl-4"
            />
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop, Mixins } from 'vue-property-decorator';
import TimeStamp from '@/core/ui/components/time-stamp.vue';
import UserLink from '@/user/ui/components/user-link.vue';
import CommentCreateForm from '@/comment/ui/components/comment-create-form.vue';
import CommentVoteController from '@/vote/ui/components/comment-vote-controller.vue';
import { CommentCreatorMixin, CommentCreateParams, CommentUpdateParams } from '@/comment';
import { Comment, CommentUpdaterMixin } from '@/comment';
import CommentExpandButton from '@/comment/ui/components/comment-expand-button.vue';

/**
 * Component to show a comment on screen.
 */
@Component({
    name: 'comment-summary',
    components: {
        UserLink,
        TimeStamp,
        CommentCreateForm,
        CommentVoteController,
        CommentExpandButton
    },
    mixins: [CommentCreatorMixin, CommentUpdaterMixin]
})
export default class CommentSummary extends Mixins(CommentCreatorMixin, CommentUpdaterMixin) {
    @Prop()
    public comment!: Comment;

    /**
     * If the comment body is currently expanded.
     */
    public isExpanded: boolean = true;

    /**
     * If the user is currently replying to this comment.
     */
    public isReplying: boolean = false;

    /**
     * If the user is currently editing this comment.
     */
    public isEditing: boolean = false;

    /**
     * The raw text of the editted comment.
     */
    public editedBody: string = '';

    public created() {
        this.$validator.localize('en', {
            custom: {
                editCommentBody: {
                    required: 'Body is required.',
                    max: 'Body may not be longer than 10,000 characters.'
                }
            }
        });
    }

    /**
     * Event handler for when the comment body is expanded / collapsed.
     */
    public onExpand(expanded: boolean) {
        this.isExpanded = expanded;
    }

    /**
     * User clicked on the "reply" button to a comment. Show the reply commen textbox.
     */
    public async onReplyClick() {
        // If the user isn't logged in, send them to the login page.
        if (!this.$isLoggedIn()) {
            this.$redirectToLogin();
        } else {
            this.isReplying = true;
        }
    }

    /**
     * User clicked submit on the reply. Attempt to send it over to the backend.
     */
    public async onReplySubmit(text: string) {
        const postId = Number.parseInt(this.$route.params.postId, 10);

        const c = await this.$createComment(new CommentCreateParams(text, postId, this.comment.id));
        this.comment.children.push(c);

        this.isReplying = false;
    }

    /**
     * Check to see if the user can actually edit this comment.
     */
    public canEdit() {
        // First check to see if they are even logged in.
        if (!this.$isLoggedIn()) {
            return false;
        }

        return this.comment.user.id === this.$login!.user.id;
    }

    public onEditClick() {
        this.isEditing = true;
        this.editedBody = this.comment.body;
    }

    public async onEditSave() {
        // Validate first.
        if (!(await this.$validator.validate())) {
            return;
        }

        await this.$updateComment(new CommentUpdateParams(this.comment.id, this.editedBody));
        this.isEditing = false;
    }

    public async onEditCancel() {
        this.isEditing = false;
    }
}
</script>