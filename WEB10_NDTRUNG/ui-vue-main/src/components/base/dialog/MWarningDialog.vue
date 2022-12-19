<template>
    <div class="m-dialog">
        <div class="m-dialog__content">
            <div class="m-dialog__content--main">
                <div class="content">
                    <div class="icon-warning"></div>
                    <div class="message-content">
                        {{ dialogText() }}
                    </div>
                </div>
                <div class="content-footer">
                    <div class="content-footer-left">
                        <button class="m-btn" @click="isWarningDialog">
                            Đồng ý
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    props: ["employeeCode", "warning", "errorCode", "warningCode"],
    methods: {
        /**
         * Hiển thị form cảnh báo với các trường hợp
         * Author: NDTRUNG (25/11/2022)
         */
        dialogText() {
            try {
                // Cảnh báo khi trường Mã nhân viên bị trùng
                if (this.warning) {
                    return `Mã nhân viên '${this.employeeCode}' đã tồn tại trong hệ
                            thống, vui lòng kiểm tra lại!`;
                    // Cảnh báo khi trùng mã nhân viên
                } else if (this.errorCode) {
                    return `Thông tin mã nhân viên không hợp lệ!`;
                    // Cảnh báo khi sửa trùng mã
                } else if (this.warningCode) {
                    return `Mã nhân viên '${this.employeeCode}' đã tồn tại trong hệ
                            thống, vui lòng kiểm tra lại!`;
                }
                // Cảnh báo khi thiếu thông tin nhân viên
                return `Vui lòng điền đầy đủ và chính xác thông tin!`;
            } catch (err) {
                console.log(err);
            }
        },

        /**
         * Thực hiện đóng dialog Cảnh báo
         * Author: NDTRUNG(25/11/2022)
         */
        isWarningDialog() {
            try {
                this.$emit("isWarningDialog");
            } catch (err) {
                console.log(err);
            }
        },
    },
};
</script>
<style scoped>
@import url(../../../css/components/dialog/warningdialog.css);
/* @import "../../../scss/components/dialog/warningdialog.scss"; */
</style>
