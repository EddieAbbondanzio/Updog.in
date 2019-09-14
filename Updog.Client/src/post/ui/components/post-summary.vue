<template>
    <div class="bg-light border mb-2 px-3 py-1">
        <div class="d-flex flex-row">
            <!-- Vote Arrows -->
            <post-vote-controller :post="post" />

            <!-- Icon -->
            <post-icon :post="post" />

            <div>
                <!-- Post Title -->
                <div>
                    <span class="mb-0">
                        <router-link
                            :to="{name:'comments', params: { postId: post.id}}"
                            v-if="isTextPost()"
                        >{{ post.title }}</router-link>
                        <a :href="`//${this.post.body}`" v-else>{{ post.title }}</a>
                    </span>

                    <div class="d-flex flex-row">
                        <!-- Expand / Collapse -->
                        <div v-if="showToggle">
                            <material-icon
                                :icon="isExpanded ? 'remove' : 'add_box'"
                                variant="muted"
                                style="font-size: 36px;"
                                @click.native="isExpanded = !isExpanded"
                            />
                        </div>

                        <div class="d-flex flex-column" style="font-size: 14px;">
                            <!-- Timestamp -->
                            <div class="text-muted">
                                Posted
                                <time-stamp :date="post.creationDate" :modified="post.wasUpdated" />by
                                <user-link :user="post.user" />

                                <span v-if="showSpace">
                                    to
                                    <space-link :space="post.space" :showPrefix="true" />
                                </span>
                            </div>

                            <!-- Footer links -->
                            <div class="text-muted post-controls">
                                <b-button
                                    variant="link"
                                    class="text-dark pl-0 pr-1 py-0"
                                    style="font-size: 14px;"
                                    :to="{name: 'comments', params: {postId: post.id}}"
                                >{{ post.commentCount == 1 ? `1 comment` : `${post.commentCount} comments` }}</b-button>
                                <b-button
                                    variant="link"
                                    class="text-muted px-1"
                                    @click="onEditPost"
                                    v-if="canEdit() && showEdit"
                                >edit</b-button>
                                <b-button
                                    variant="link"
                                    class="text-muted px-1"
                                    @click="onDeletePost"
                                    v-if="canDelete() && showEdit"
                                >delete</b-button>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Post Body -->
                <div v-if="isExpanded">
                    <div v-if="!isEditting">{{ post.body }}</div>
                    <div v-else>
                        <textarea
                            v-model.trim="edittedBody"
                            name="editPostBody"
                            v-validate="'required|max:10000'"
                        />
                        <b-form-invalid-feedback
                            class="d-block"
                            :state="false"
                        >{{ errors.first('editPostBody')}}</b-form-invalid-feedback>

                        <b-button variant="primary" @click="onEdittedPost">Save</b-button>
                        <b-button variant="primary-outline" @click="onEditCancel">Cancel</b-button>
                    </div>
                </div>
            </div>
        </div>
    </div>
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
import { UserAuthMixin } from '@/user';

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
        SpaceLink
    },
    mixins: [UserAuthMixin, PostUpdaterMixin]
})
export default class PostSummary extends mixins(UserAuthMixin, PostUpdaterMixin) {
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
        alert('reee');
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