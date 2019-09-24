<template>
    <v-form>
        <v-card>
            <v-tabs v-model="activeTab" class="mb-6">
                <v-tab>Link</v-tab>
                <v-tab>Text</v-tab>
            </v-tabs>
        </v-card>

        <v-tabs-items v-model="activeTab">
            <v-tab-item>
                <v-card class="pa-3">
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
                </v-card>
            </v-tab-item>
            <v-tab-item>
                <v-card class="pa-3">
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
                        v-on:keyup="onTextBodyKeyUp"
                        v-on:blur="onTextBodyKeyUp"
                        :error="errors.first('createTextPost.textBody') != null"
                        :error-messages="errors.first('createTextPost.textBody')"
                    />
                </v-card>
            </v-tab-item>
        </v-tabs-items>

        <v-btn color="primary" class="mr-2" @click="onSubmit">Submit</v-btn>
        <v-btn color="error" outlined type="reset" class="ml-2" @click="onReset">Reset</v-btn>
    </v-form>
</template>

<style scoped>
#text-body-textarea {
    min-height: 200px;
}
</style>


<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { Post, PostCreateParams, PostType } from '@/post';

/**
 * Form to create a new text or link post.
 */
@Component({
    name: 'create-post-form'
})
export default class CreatePostForm extends Vue {
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
     * The number displayed on screen of how many characters are left
     * on the text post body.
     */
    public textBodyCharactersRemaining = Post.BODY_MAX_LENGTH;

    public created(): void {
        // isText is a boolean so we need to check it's string value, not if it's truthy
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
                ? new PostCreateParams(PostType.Link, this.linkTitle, this.linkUrl)
                : new PostCreateParams(PostType.Text, this.textTitle, this.textBody);

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
     * Update the remaining character count.
     */
    public async onTextBodyKeyUp() {
        this.textBodyCharactersRemaining = Post.BODY_MAX_LENGTH - this.textBody.length;
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
        this.textBodyCharactersRemaining = Post.BODY_MAX_LENGTH;
    }
}
</script>