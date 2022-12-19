<template>
    <div class="m-toast">
        <div class="m-toast-main" :class="{ 'm-toast-main-error': isError }">
            <div class="left-toast" v-if="!isError">
                <div class="icon-succsess ml-16 mr-10"></div>
                <div class="toast-content">
                    <b for="" class="title-toast success-title mr-10">Thành công!</b>
                    <label for=""
                        >Bạn đã {{ toastAct }} thành công nhân viên!</label
                    >
                </div>
            </div>
            <div class="left-toast" v-if="isError">
                <div class="icon-error ml-16 mr-10"></div>
                <div class="toast-content">
                    <b for="" class="title-toast error-title mr-10">Thất bại!</b>
                    <label for="">Thao tác thất bại, vui lòng thử lại!</label>
                </div>
            </div>
            <div class="icon-toast-close mr-16" @click="closeOpenToast()"></div>
        </div>
    </div>
</template>
<script>
export default {
    props: ["toastAct", "isError"],
    methods: {
        /**
         * Thực hiện đóng Toast
         * Author: NDTRUNG(26/11/2022)
         */
        closeOpenToast() {
            try {
                this.$emit("closeOpenToast");
            } catch (err) {
                console.log(err);
            }
        },
    },
};
</script>
<style>
@import url(../../../css/components/toast/toast.css);
</style>
