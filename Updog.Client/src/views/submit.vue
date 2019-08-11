<template>
    <master-page>
        <template>
            <b-tabs v-model="activeTab">
                <b-tab title="Link">REEE</b-tab>
                <b-tab title="Text">FOOO</b-tab>
            </b-tabs>

            <b-form>
                <b-input type="text" />
                <b-textarea />
                <b-button variant="primary">Submit</b-button>
                <b-button variant="outline-primary">Reset</b-button>
            </b-form>
        </template>
        <!-- <template slot="side-bar">SIDE BAR</template> -->
        <!-- <template slot="footer">FOOTER!</template> -->
    </master-page>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import CreatePostButtons from '@/post/components/create-post-buttons.vue';
import MasterPage from '@/components/master-page.vue';

@Component({
    components: {
        CreatePostButtons,
        MasterPage
    }
})
export default class Home extends Vue {
    public activeTab: number = 0;

    public created(): void {
        if (this.$route.query.isText) {
            this.activeTab = 1;
        }
    }
}
</script>
