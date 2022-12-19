<template>
    <button
        :class="['m-btn', btnExtra ? 'm-btn-extra' : '']"
        :tabindex="tabindex"
    >
        {{ btnText }}
    </button>
</template>
<script>
export default {
    props: {
        // Text của ô input
        btnText: {
            type: String,
            default: "Button",
        },
        // Có phải ô phụ không??
        btnExtra: {
            type: Boolean,
            default: false,
        },
        // Tabindex ô button
        tabindex: {
            type: Number,
        },
    },
};
</script>
<style lang="scss">
/* @import url(../../../css/components/button/button.css); */
@import "../../../scss/components/button/button.scss";
</style>
