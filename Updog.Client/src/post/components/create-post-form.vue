<template>
    <b-form class="bg-light p-3 border">
        <b-tabs v-model="activeTab" class="post-submit-form" content-class="m-3">
            <b-tab title="Link">
                <b-form-group>
                    <b-form-input
                        type="text"
                        id="link-title-textbox"
                        placeholder="Title"
                        v-model="linkTitle"
                    />
                </b-form-group>
                <b-form-group>
                    <b-form-input
                        type="text"
                        id="link-body-textbox"
                        placeholder="URL"
                        v-model="linkUrl"
                    />
                </b-form-group>
            </b-tab>
            <b-tab title="Text">
                <b-form-group>
                    <b-form-input
                        type="text"
                        id="text-title-textbox"
                        placeholder="Title"
                        v-model="textTitle"
                    />
                </b-form-group>
                <b-form-group>
                    <b-form-textarea id="text-body-textarea" placeholder="Body" v-model="textBody" />
                </b-form-group>
            </b-tab>
        </b-tabs>

        <b-button variant="primary" class="mr-2" @click="onSubmit">Submit</b-button>
        <b-button variant="outline-danger" type="reset" class="ml-2" @click="onReset">Reset</b-button>
    </b-form>
</template>

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
            this.activeTab == 0
                ? new PostCreateParams(PostType.Link, this.linkTitle, this.linkUrl)
                : new PostCreateParams(PostType.Text, this.textTitle, this.textBody);

        this.$emit('submit', creationParams);
    }

    /**
     * Reset the v-models of the form.
     */
    public async onReset() {
        this.linkTitle = '';
        this.linkUrl = '';
        this.textTitle = '';
        this.textBody = '';
    }
}
</script>