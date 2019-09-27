<template>
    <v-form>
        <v-alert type="info">
            Rules:
            <br />1. Don't be a jerk.
        </v-alert>
        <v-card>
            <v-tabs v-model="activeTab" class="mb-6" @change="onTabChange">
                <v-tab>Link</v-tab>
                <v-tab>Text</v-tab>
            </v-tabs>
        </v-card>

        <v-card class="pa-3">
            <v-select
                placeholder="Space"
                name="postSpace"
                v-model="postingToSpace"
                :items="$subscribedSpaces"
                :readonly="lockSpace"
                item-text="name"
                item-value="name"
                v-validate="'required'"
                :error="errors.first('postSpace') != null"
                :error-messages="errors.first('postSpace')"
            />

            <v-tabs-items v-model="activeTab">
                <v-tab-item>
                    <v-text-field
                        id="link-title-textbox"
                        placeholder="Title"
                        v-model.trim="linkTitle"
                        name="linkTitle"
                        v-validate="'required|max:300'"
                        data-vv-scope="createLinkPost"
                        :error="errors.first('createLinkPost.linkTitle') != null"
                        :error-messages="errors.first('createLinkPost.linkTitle')"
                    />
                    <v-text-field
                        type="text"
                        id="link-body-textbox"
                        placeholder="URL"
                        v-model.trim="linkUrl"
                        name="linkUrl"
                        v-validate="'required|url'"
                        data-vv-scope="createLinkPost"
                        :error="errors.first('createLinkPost.linkUrl') != null"
                        :error-messages="errors.first('createLinkPost.linkUrl')"
                    />
                </v-tab-item>
                <v-tab-item>
                    <v-text-field
                        id="text-title-textbox"
                        placeholder="Title"
                        v-model.trim="textTitle"
                        name="textTitle"
                        v-validate="'required|max:300'"
                        data-vv-scope="createTextPost"
                        :error="errors.first('createTextPost.textTitle') != null"
                        :error-messages="errors.first('createTextPost.textTitle')"
                    />
                    <v-textarea
                        id="text-body-textarea"
                        placeholder="Body"
                        v-model.trim="textBody"
                        name="textBody"
                        v-validate="'required|max:10000'"
                        data-vv-scope="createTextPost"
                        counter="10000"
                        :error="errors.first('createTextPost.textBody') != null"
                        :error-messages="errors.first('createTextPost.textBody')"
                    />
                </v-tab-item>
            </v-tabs-items>

            <v-btn color="primary" class="mr-2" @click="onSubmit">Submit</v-btn>
            <v-btn color="error" outlined type="reset" class="ml-2" @click="onReset">Reset</v-btn>
        </v-card>
    </v-form>
</template>

<style scoped>
#text-body-textarea {
    min-height: 200px;
}
</style>


<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import SpaceFinderMixin from '@/space/mixins/space-finder-mixin';
import { PostType } from '@/post/domain/post-type';
import { PostCreateParams } from '@/post/interactors/create/post-create-params';
/**
 * Form to create a new text or link post.
 */
@Component({
    name: 'create-post-form'
})
export default class CreatePostForm extends SpaceFinderMixin {
    /**
     * The currently active tab.
     */
    public activeTab: number = 0;

    /**
     * The title of a text post.
     */
    public linkTitle: string = '';

    /**
     * The URL (body) of a link post.
     */
    public linkUrl: string = '';

    /**
     * The title of a text post.
     */
    public textTitle: string = '';

    /**
     *  The body of a text post.
     */
    public textBody: string = '';

    /**
     * The post to submit to.
     */
    public postingToSpace: string | null = null;

    public lockSpace: boolean = false;

    public async created() {
        if (this.$route.params.spaceName != null) {
            this.postingToSpace = this.$route.params.spaceName;
            this.lockSpace = true;
        }

        await this.$findSubscribedSpaces();

        // isText is a string so we can't just check if it's truthy
        if (this.$route.query.isText === 'true') {
            this.activeTab = 1;
        }

        this.$validator.localize('en', {
            custom: {
                linkTitle: {
                    required: 'Title is required.',
                    max: 'Title may be no longer than 300 characters.'
                },
                linkUrl: {
                    required: 'URL is required.',
                    url: 'URL must be valid'
                },
                textTitle: {
                    required: 'Title is required',
                    max: 'Title may be no longer than 300 characters.'
                },
                textBody: {
                    required: 'Body is required.',
                    max: 'Body may not be longer than 10,000 characters.'
                }
            }
        });
    }

    /**
     * Validate, then submit the form.
     */
    public async onSubmit() {
        // Validate first.
        if (!(await this.$validator.validateAll(this.activeTab === 0 ? 'createLinkPost' : 'createTextPost'))) {
            return;
        }

        const creationParams =
            this.activeTab === 0
                ? new PostCreateParams(this.postingToSpace!, PostType.Link, this.linkTitle, this.linkUrl)
                : new PostCreateParams(this.postingToSpace!, PostType.Text, this.textTitle, this.textBody);

        this.$emit('submit', creationParams);
    }

    /**
     * On tab change update the query string param to reflect if it's a text
     * post or not.
     */
    public async onTabChange(index: number) {
        this.onReset();
        this.$router.push({ query: { isText: index === 1 ? 'true' : 'false' } });
    }

    /**
     * Reset the v-models of the form.
     */
    public async onReset() {
        this.$validator.reset();

        this.linkTitle = '';
        this.linkUrl = '';
        this.textTitle = '';
        this.textBody = '';
    }
}
</script>