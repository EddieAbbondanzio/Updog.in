<template>
    <div class="py-1">
        <div>
            <div>
                {{ comment.user.username }}
                <date-time-stamp :date="comment.creationDate" class="text-muted" />
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

/**
 * Comment to show a comment on screen.
 */
@Component({
    name: 'comment-summary',
    components: {
        DateTimeStamp
    }
})
export default class CommentSummary extends Vue {
    @Prop()
    public comment!: Comment;
}
</script>