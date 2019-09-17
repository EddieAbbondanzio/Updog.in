<template>
    <b-form class="bg-light p-3 border">
        <b-tabs
            v-model="activeTab"
            class="post-submit-form"
            content-class="m-3"
            @input="onTabChange"
        >
            <b-tab title="Link">
                <b-form-group>
                    <b-form-input
                        type="text"
                        id="link-title-textbox"
                        placeholder="Title"
                        v-model.trim="linkTitle"
                        name="linkTitle"
                        v-validate="'required|max:300'"
                        data-vv-scope="createLinkPost"
                    />
                    <b-form-invalid-feedback
                        class="d-block"
                        :state="false"
                    >{{ errors.first('createLinkPost.linkTitle')}}</b-form-invalid-feedback>
                </b-form-group>
                <b-form-group>
                    <b-form-input
                        type="text"
                        id="link-body-textbox"
                        placeholder="URL"
                        v-model.trim="linkUrl"
                        name="linkUrl"
                        v-validate="'required|url'"
                        data-vv-scope="createLinkPost"
                    />
                    <b-form-invalid-feedback
                        class="d-block"
                        :state="false"
                    >{{ errors.first('createLinkPost.linkUrl')}}</b-form-invalid-feedback>
                </b-form-group>
            </b-tab>
            <b-tab title="Text">
                <b-form-group>
                    <b-form-input
                        type="text"
                        id="text-title-textbox"
                        placeholder="Title"
                        v-model.trim="textTitle"
                        name="textTitle"
                        v-validate="'required|max:300'"
                        data-vv-scope="createTextPost"
                    />
                    <b-form-invalid-feedback
                        class="d-block"
                        :state="false"
                    >{{ errors.first('createTextPost.textTitle')}}</b-form-invalid-feedback>
                </b-form-group>
                <b-form-group>
                    <b-form-textarea
                        id="text-body-textarea"
                        placeholder="Body"
                        v-model.trim="textBody"
                        name="textBody"
                        v-validate="'required|max:10000'"
                        data-vv-scope="createTextPost"
                        v-on:keyup="onTextBodyKeyUp"
                        v-on:blur="onTextBodyKeyUp"
                    />
                    <div
                        class="text-muted"
                    >{{ textBodyCharactersRemaining == 1 ? '1 character remaining' : ` ${textBodyCharactersRemaining.toLocaleString()} characters remaining`}}</div>
                    <b-form-invalid-feedback
                        class="d-block"
                        :state="false"
                    >{{ errors.first('createTextPost.textBody')}}</b-form-invalid-feedback>
                </b-form-group>
            </b-tab>
        </b-tabs>

        <b-button variant="primary" class="mr-2" @click="onSubmit">Submit</b-button>
        <b-button variant="outline-danger" type="reset" class="ml-2" @click="onReset">Reset</b-button>
    </b-form>
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