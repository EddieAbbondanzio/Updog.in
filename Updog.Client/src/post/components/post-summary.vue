<template>
    <div class="bg-light border mb-2 px-3 py-1">
        <div class="d-flex flex-row" v-if="post != null">
            <div>
                <material-icon
                    :icon="isExpanded ? 'expand_less' : 'expand_more'"
                    variant="dark"
                    size="md"
                    @click.native="onExpand"
                />
            </div>
            <div>
                <div>
                    <h4 class="mb-0">
                        <router-link :to="`post/${post.id}`" v-if="isTextPost()">{{ post.title }}</router-link>
                        <a :href="`//${this.post.body}`" v-else>{{ post.title }}</a>
                    </h4>
                    <p class="text-muted">Posted {{ post.getDifferenceDate() }} by {{ post.author }}</p>
                </div>
                <div v-if="isExpanded">{{ post.body}}</div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { PostInfo } from '../common/post-info';
import { PostType } from '../common/post-type';
import MaterialIcon from '@/components/material-icon.vue';

/**
 * Summary of information about a post. Used on post lists, and post topic page.
 */
@Component({
    name: 'post-summary',
    components: {
        MaterialIcon
    }
})
export default class NewComponent extends Vue {
    /**
     * The post to display.
     */
    @Prop({ default: null })
    public post!: PostInfo | null;

    /**
     * If the component should expand by default.
     */
    @Prop({ default: false })
    public expand!: boolean;

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
    public isTextPost() {
        if (this.post == null) {
            throw new Error('No post!');
        }

        return this.post.type === PostType.Text;
    }

    public onExpand() {
        this.isExpanded = !this.isExpanded;
    }
}
</script>