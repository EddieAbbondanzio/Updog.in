<template>
    <master-page>
        <template>CONTENT STUFF</template>
        <template slot="side-bar">
            <post-widget />
        </template>
        <template slot="footer">FOOTER!</template>
    </master-page>
    <!-- 
    <b-container fluid class="page-content">
        <b-row>
            <b-col md="8" lg="10" class="bg-dark">
                <div style="height: 3000px; background-color: red;">REEEEE</div>
            </b-col>
            <b-col md="4" lg="2">
                <post-widget />
            </b-col>
        </b-row>
    </b-container>-->
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import PostWidget from '@/post/components/post-widget.vue';
import MasterPage from '@/components/master-page.vue';

@Component({
    components: {
        PostWidget,
        MasterPage
    }
})
export default class Home extends Vue {}
</script>
