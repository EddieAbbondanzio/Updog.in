<template>
    <v-card class="p-2 mb-2 d-flex flex-row">
        <div class="d-flex align-self-start align-center">
            <post-vote-controller :post="post" class="mr-2" />
            <post-icon :post="post" class="mr-2" />
        </div>

        <div class="flex-grow-1">
            <div>
                <!-- Post Title -->
                <post-link :post="post" />

                <div class="d-flex flex-row">
                    <!-- Expand / Collapse -->
                    <div v-if="showToggle && isTextPost()">
                        <post-expand-button @toggle="isExpanded = !isExpanded" />
                    </div>

                    <div class="d-flex flex-column text-sm">
                        <post-time-stamp
                            :post="post"
                            class="grey--text text--darken-1 caption"
                            :showSpace="showSpace"
                        />

                        <!-- Footer links -->
                        <div class="muted--text post-controls caption">
                            <comments-link :post="post" variant="muted" />&nbsp;
                            <a
                                href="#"
                                class="grey--text text--darken-3"
                                @click.prevent="onEditPost"
                                v-if="canEdit() && showEdit"
                            >edit</a>&nbsp;
                            <a
                                href="#"
                                class="grey--text text--darken-3"
                                @click.prevent="onDeletePost"
                                v-if="canDelete() && showEdit"
                            >delete</a>&nbsp;
                            <are-you-sure
                                v-if="isDeleting"
                                @yes="onDeleteConfirm"
                                @no="onDeleteCancel"
                            />
                        </div>
                    </div>
                </div>
            </div>

            <!-- Post Body -->
            <v-expand-transition>
                <div v-show="isExpanded">
                    <div v-show="!isEditting">{{ post.body }}</div>
                    <div v-show="isEditting">
                        <v-textarea
                            class="pb-3"
                            v-model.trim="edittedBody"
                            name="editPostBody"
                            v-validate="'required|max:10000'"
                            :error="errors.first('editPostBody') != null ? true : false"
                            :error-messages="errors.first('editPostBody')"
                        />

                        <v-btn class="mr-2" color="primary" @click="onEdittedPost">Save</v-btn>
                        <v-btn class="ml-2" color="primary" outlined @click="onEditCancel">Cancel</v-btn>
                    </div>
                </div>
            </v-expand-transition>
        </div>
    </v-card>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import TimeStamp from '@/core/ui/components/time-stamp.vue';
import UserLink from '@/user/ui/components/user-link.vue';
import { mixins } from 'vue-class-component';
import PostVoteController from '@/vote/ui/components/post-vote-controller.vue';
import PostIcon from '@/post/ui/components/post-icon.vue';
import SpaceLink from '@/space/ui/components/space-link.vue';
import { PostUpdaterMixin, Post, PostUpdateParams, PostType } from '@/post';
import PostLink from '@/post/ui/components/post-link.vue';
import PostExpandButton from '@/post/ui/components/post-expand-button.vue';
import PostTimeStamp from '@/post/ui/components/post-time-stamp.vue';
import CommentsLink from '@/comment/ui/components/comments-link.vue';
import AreYouSure from '@/core/ui/components/are-you-sure.vue';

/**
 * Summary of information about a post. Used on post lists, and post topic page.
 */
@Component({
    name: 'post-summary',
    components: {
        TimeStamp,
        UserLink,
        PostVoteController,
        PostIcon,
        SpaceLink,
        PostLink,
        PostExpandButton,
        PostTimeStamp,
        CommentsLink,
        AreYouSure
    }
})
export default class PostSummary extends PostUpdaterMixin {
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
    public showEdit!: boolean;

    /**
     * If the expand / collapse button should be visible.
     */
    @Prop({ default: true })
    public showToggle!: boolean;

    @Prop({ default: true })
    public showSpace!: boolean;

    /**
     * If the component should show the body
     */
    public isExpanded: boolean = false;

    /**
     * If the user is editting the post.
     */
    public isEditting: boolean = false;

    public isDeleting: boolean = false;

    public edittedBody: string = '';

    /**
     * Check to see if we need to expand it by default.
     */
    public created() {
        if (this.expand) {
            this.isExpanded = true;
        }

        this.$validator.localize('en', {
            custom: {
                editPostBody: { required: 'Body is required.', max: 'Body may not be longer than 10,000 characters.' }
            }
        });
    }

    public onEditPost() {
        this.isEditting = true;
        this.edittedBody = this.post.body;
    }

    public async onEdittedPost() {
        // Validate first.
        if (!(await this.$validator.validate())) {
            return;
        }

        const params = new PostUpdateParams(this.post.id, this.edittedBody);
        await this.$updatePost(params);

        this.isEditting = false;
    }

    public onEditCancel() {
        this.isEditting = false;
        this.edittedBody = '';
    }

    public onDeletePost() {
        this.isDeleting = true;
    }

    /**
     * Check to see if a post is a text post.
     */
    protected isTextPost() {
        return this.post.type === PostType.Text;
    }

    /**
     * If the user can edit the post.
     */
    protected canEdit(): boolean {
        return this.showEdit && this.isPostOwner() && this.post.type === PostType.Text;
    }

    /**
     * If the user can delete the post.
     */
    protected canDelete(): boolean {
        return this.showEdit && this.isPostOwner();
    }

    protected async onDeleteConfirm() {
        await this.$deletePost(this.post);
        this.$router.push({ name: 'home' });
    }

    protected onDeleteCancel() {
        this.isDeleting = false;
    }

    /**
     * Check to see if the currently logged in user is the post owner.
     */
    protected isPostOwner() {
        if (!this.$isLoggedIn()) {
            return false;
        }

        return this.post.isOwner(this.$login!.user);
    }
}
</script>