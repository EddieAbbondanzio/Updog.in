<template>
    <master-page>
        <template>CONTENT STUFF</template>
        <template slot="side-bar">
            <create-post-buttons />
        </template>
        <template slot="footer">FOOTER!</template>
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
export default class Home extends Vue {}
</script>
