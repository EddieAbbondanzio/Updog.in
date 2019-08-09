<template>
    <master-page />
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import { User } from './user/common/user';
import { Context } from './core/context';
import { EventBus } from './core/event-bus';
import MasterPage from '@/components/master-page.vue';

@Component({
    name: 'app',
    components: {
        MasterPage
    }
})
export default class App extends Vue {}
</script>
