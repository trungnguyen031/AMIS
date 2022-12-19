<template>
    <div>
        <the-sidebar></the-sidebar>
        <the-header></the-header>
        <the-content></the-content>
    </div>
</template>
<script>
import TheContent from "./components/layout/TheContent.vue";
import TheHeader from "./components/layout/TheHeader.vue";
import TheSidebar from "./components/layout/TheSidebar.vue";
export default {
    components: {
        TheSidebar,
        TheHeader,
        TheContent,
    },
};
</script>
<style>
@import url(./css/style.css);
</style>
