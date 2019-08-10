<template>
    <div>
        <nav-bar />
        <router-view class="page-content" />
        <div class="footer" style="height: 100px;">THIS IS THE FOOTER</div>
    </div>
</template>


<style>
.page-content {
    height: calc(100vh - 91px - 100px);
}
</style>


<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import NavBar from '@/components/nav-bar.vue';

/**
 * Master page for the site.
 */
@Component({
    name: 'master-page',
    components: {
        NavBar
    }
})
export default class MasterPage extends Vue {}
</script>