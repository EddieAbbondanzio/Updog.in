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
                    />
                    <b-form-invalid-feedback
                        class="d-block"
                        :state="false"
                    >{{ errors.first('linkTitle')}}</b-form-invalid-feedback>
                </b-form-group>
                <b-form-group>
                    <b-form-input
                        type="text"
                        id="link-body-textbox"
                        placeholder="URL"
                        v-model.trim="linkUrl"
                        name="linkUrl"
                        v-validate="'required|url'"
                    />
                    <b-form-invalid-feedback
                        class="d-block"
                        :state="false"
                    >{{ errors.first('linkUrl')}}</b-form-invalid-feedback>
                </b-form-group>
            </b-tab>
            <b-tab title="Text">
                <b-form-group>
                    <b-form-input
                        type="text"
                        id="text-title-textbox"
                        placeholder="Title"
                        v-model.trim="textTitle"
                    />
                    <b-form-invalid-feedback
                        class="d-block"
                        :state="false"
                    >{{ errors.first('textTitle')}}</b-form-invalid-feedback>
                </b-form-group>
                <b-form-group>
                    <b-form-textarea
                        id="text-body-textarea"
                        placeholder="Body"
                        v-model.trim="textBody"
                    />
                    <b-form-invalid-feedback
                        class="d-block"
                        :state="false"
                    >{{ errors.first('textBody')}}</b-form-invalid-feedback>
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
import { PostCreateParams } from '@/post/common/post-create-params';
import { PostType } from '../common/post-type';

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

    public created(): void {
        if (this.$route.query.isText) {
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
                    confirmed: 'Passwords do not match.'
                }
            }
        });
    }

    /**
     * Validate, then submit the form.
     */
    public async onSubmit() {
        // Validate first.
        if (!(await this.$validator.validate())) {
            return;
        }

        const creationParams =
            this.activeTab === 0
                ? new PostCreateParams(PostType.Link, this.linkTitle, this.linkUrl)
                : new PostCreateParams(PostType.Text, this.textTitle, this.textBody);

        this.$emit('submit', creationParams);
    }

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