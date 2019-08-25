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
                    <!-- Post Title -->
                    <div>
                        <h4 class="mb-0">
                            <router-link
                                :to="`post/${post.id}`"
                                v-if="isTextPost()"
                            >{{ post.title }}</router-link>
                            <a :href="`//${this.post.body}`" v-else>{{ post.title }}</a>
                        </h4>
                        <p class="text-muted">
                            Posted
                            <date-time-stamp :date="post.creationDate" :modified="post.wasUpdated" />by
                            <user-link :user="post.user" />
                        </p>
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

                    <!-- Footer links -->
                    <div class="text-muted post-controls">
                        <b-button
                            variant="link"
                            class="text-muted pl-0 pr-1"
                            :to="`/post/${post.id}`"
                        >{{ post.commentCount == 1 ? `1 comment` : `${post.commentCount} comments` }}</b-button>
                        <b-button
                            variant="link"
                            class="text-muted px-1"
                            @click="onEditPost"
                            v-if="canEdit && showEditControls"
                        >edit</b-button>
                        <b-button
                            variant="link"
                            class="text-muted px-1"
                            @click="onDeletePost"
                            v-if="canDelete && showEditControls"
                        >delete</b-button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { PostType } from '../common/post-type';
import MaterialIcon from '@/core/components/material-icon.vue';
import { Post } from '../common/post';
import DateTimeStamp from '@/core/components/date-time-stamp.vue';
import UserLink from '@/user/components/user-link.vue';
import { Context } from '@/core/context';
import { PostMixin } from '../mixins/post-mixin';
import { PostUpdateParams } from '../use-cases/update/post-update-params';

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
export default class PostSummary extends PostMixin {
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
        this.post = await this.$updatePost(params);
    }

    public onEditCancel() {
        this.isEditting = false;
        this.edittedBody = '';
    }

    public onDeletePost() {}

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
        return this.showEditControls && this.isPostOwner() && this.post.type === PostType.Text;
    }

    /**
     * If the user can delete the post.
     */
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