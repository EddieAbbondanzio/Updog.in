<template>
    <b-modal ref="popup">REEEEE</b-modal>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { BvModal } from 'bootstrap-vue';

/**
 * Popup to alert the user that they should sign up.
 */
@Component({
    name: 'user-not-logged-in-popup'
})
export default class UserNotLoggedInPopup extends Vue {
    public $refs!: {
        popup: any;
    };

    public show() {
        this.$refs.popup.show();
    }

    public hide() {
        this.$refs.popup.hide();
    }
}
</script>