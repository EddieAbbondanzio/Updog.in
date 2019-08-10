<template>
    <router-view />
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import { User } from './user/common/user';
import { Context } from './core/context';
import { EventBus } from './core/event-bus';

@Component({
    name: 'app'
})
export default class App extends Vue {}
</script>
