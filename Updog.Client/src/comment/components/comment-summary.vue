<template>
    <div class="py-1">
        <div>
            <div class="d-flex flex-row align-items-center">
                <user-link :user="comment.user" />
                <date-time-stamp :date="comment.creationDate" class="text-muted" />
                <router-link
                    class="d-flex align-items-center"
                    :to="{name: 'comment', params: { commentId: comment.id}}"
                >
                    <material-icon icon="link" variant="muted" size="sm" />
                </router-link>
            </div>
            <div>{{ comment.body}}</div>

            <!-- Children comments -->
            <comment-summary
                v-for="child in comment.children"
                :comment="child"
                v-bind:key="child.id"
                class="pl-3"
            />
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { Comment } from '../common/comment';
import DateTimeStamp from '@/core/components/date-time-stamp.vue';
import UserLink from '@/user/components/user-link.vue';
import MaterialIcon from '@/core/components/material-icon.vue';

/**
 * Comment to show a comment on screen.
 */
@Component({
    name: 'comment-summary',
    components: {
        UserLink,
        DateTimeStamp,
        MaterialIcon
    }
})
export default class CommentSummary extends Vue {
    @Prop()
    public comment!: Comment;
}
</script>