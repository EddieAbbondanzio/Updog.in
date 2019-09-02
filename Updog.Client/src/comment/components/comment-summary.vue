<template>
    <div class="py-1">
        <!-- Comment -->
        <div>
            <!-- Header -->
            <div class="d-flex flex-row align-items-center">
                <user-link :user="comment.user" />
                <date-time-stamp :date="comment.creationDate" class="text-muted" />
            </div>

            <!-- Body -->
            <div>{{ comment.body}}</div>

            <!-- Actions -->
            <div class="d-flex flex-row">
                <b-button
                    variant="link"
                    class="text-muted pl-0 pr-1"
                    :to="{name: 'comment', params: { commentId: comment.id}}"
                >permalink</b-button>
                <b-button variant="link" class="text-muted pl-0 pr-1" @click="onReplyClick">reply</b-button>
            </div>

            <!-- Reply Box -->
            <div v-if="isReplying">
                <comment-create-form @submit="onReplySubmit" />
            </div>
        </div>

        <!-- Children -->
        <comment-summary
            v-for="child in comment.children"
            :comment="child"
            v-bind:key="child.id"
            class="pl-3"
        />
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop, Mixins } from 'vue-property-decorator';
import { Comment } from '../common/comment';
import DateTimeStamp from '@/core/components/date-time-stamp.vue';
import UserLink from '@/user/components/user-link.vue';
import MaterialIcon from '@/core/components/material-icon.vue';
import { UserAuthMixin } from '../../user/mixins/user-auth-mixin';
import { CommentCreatorMixin } from '../mixins/comment-creator-mixin';
import CommentCreateForm from '@/comment/components/comment-create-form.vue';
import { CommentCreateParams } from '../use-cases/create/comment-create-params';

/**
 * Component to show a comment on screen.
 */
@Component({
    name: 'comment-summary',
    components: {
        UserLink,
        DateTimeStamp,
        MaterialIcon,
        CommentCreateForm
    },
    mixins: [UserAuthMixin, CommentCreatorMixin]
})
export default class CommentSummary extends Mixins(UserAuthMixin, CommentCreatorMixin) {
    @Prop()
    public comment!: Comment;

    /**
     * If the user is currently replying to this comment.
     */
    public isReplying: boolean = false;

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
}
</script>