<template>
    <div class="bg-light border mb-2 px-3 py-1">
        <div class="d-flex flex-row">
            <div>
                <material-icon
                    :icon="isExpanded ? 'expand_less' : 'expand_more'"
                    variant="dark"
                    size="md"
                    @click.native="isExpanded = !isExpanded"
                />
            </div>
            <div>
                <div>
                    <h4 class="mb-0">
                        <router-link :to="`post/${post.id}`" v-if="isTextPost()">{{ post.title }}</router-link>
                        <a :href="`//${this.post.body}`" v-else>{{ post.title }}</a>
                    </h4>
                    <p class="text-muted">
                        Posted
                        <date-time-stamp :date="post.creationDate" :modified="post.wasUpdated" />by
                        <user-link :user="post.user" />
                    </p>

                    <div v-if="isExpanded">{{ post.body}}</div>

                    <!-- Footer links -->
                    <div class="text-muted post-controls">
                        <router-link
                            class="text-muted"
                            :to="`post/${post.id}`"
                        >{{ post.commentCount == 1 ? `1 comment` : `${post.commentCount} comments` }}</router-link>
                        <a v-if="canEdit()">edit</a>
                        <a v-if="canDelete()">delete</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped>
.post-controls a {
    padding-left: 4px;
    padding-right: 4px;
}
</style>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { PostType } from '../common/post-type';
import MaterialIcon from '@/core/components/material-icon.vue';
import { Post } from '../common/post';
import DateTimeStamp from '@/core/components/date-time-stamp.vue';
import UserLink from '@/user/components/user-link.vue';
import { Context } from '@/core/context';

/**
 * Summary of information about a post. Used on post lists, and post topic page.
 */
@Component({
    name: 'post-summary',
    components: {
        MaterialIcon,
        DateTimeStamp,
        UserLink
    }
})
export default class PostSummary extends Vue {
    /**
     * The post to display.
     */
    @Prop()
    public post!: Post;

    /**
     * If the component should expand by default.
     */
    @Prop({ default: false })
    public expand!: boolean;

    /**
     * If the edit controls should be visible.
     */
    @Prop({ default: false })
    public showEditControls!: boolean;

    /**
     * If the component should show the body
     */
    public isExpanded: boolean = false;

    /**
     * Check to see if we need to expand it by default.
     */
    public created() {
        if (this.expand) {
            this.isExpanded = true;
        }
    }

    /**
     * Check to see if a post is a text post.
     */
    protected isTextPost() {
        return this.post.type === PostType.Text;
    }

    protected canEdit(): boolean {
        return this.showEditControls && this.isPostOwner() && this.post.type === PostType.Text;
    }

    protected canDelete(): boolean {
        return this.showEditControls && this.isPostOwner();
    }

    /**
     * Check to see if the currently logged in user is the post owner.
     */
    protected isPostOwner() {
        if (Context.login == null) {
            return false;
        }

        return this.post.isOwner(Context.login.user);
    }
}
</script>