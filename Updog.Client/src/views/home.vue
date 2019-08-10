<template>
    <b-container fluid class="page-content">
        <b-row>
            <b-col md="8" lg="10" class="bg-dark">
                <div style="height: 3000px; background-color: red;">REEEEE</div>
            </b-col>
            <b-col md="4" lg="2">
                <post-widget />
            </b-col>
        </b-row>
    </b-container>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import PostWidget from '@/post/components/post-widget.vue';
import axios from 'axios';

@Component({
    components: {
        PostWidget
    }
})
export default class Home extends Vue {}
</script>
