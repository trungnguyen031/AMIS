<template>
    <div class="m-dialog">
        <div class="m-dialog__content">
            <div class="m-dialog__content--main">
                <div class="content">
                    <div class="icon-message"></div>
                    <div class="message-content">
                        {{ dialogSrc.IS_UPDATE }}
                    </div>
                </div>
                <div class="content-footer">
                    <button class="m-btn m-btn-extra" @click="confirmDialogUpdate">
                        {{ buttonSrc.CLOSE }}
                    </button>
                    <div class="content-footer-left">
                        <button
                            class="m-btn m-btn-extra mr-8"
                            @click="confirmDialogUpdate(1)"
                        >
                        {{ buttonSrc.DONT_SAVE }}
                        </button>
                        <button class="m-btn" @click="isEditEmployee()">
                            {{ buttonSrc.SAVE_DIALOG }}
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import { MS_BUTTON, DIALOG_TEXT } from "@/constans/layoutResource";
export default {
    data() {
        return {
            buttonSrc: MS_BUTTON,
            dialogSrc: DIALOG_TEXT,
        };
    },
    props: {
        edit: {
            type: Boolean,
        },
    },
    methods: {
        /**
         * Thực hiện sửa nhân viên
         * Author: NDTRUNG(26/11/2022)
         */
        isEditEmployee() {
            // try {
            //     // hàm đóng dialog
            //     this.confirmDialogUpdate();
            //     // hàm sửa
            //     this.$emit("isUpdate");
            // } catch (err) {
            //     console.log(err);
            // }
            try {
                if (!this.edit) {
                    this.confirmDialogUpdate();
                    this.$emit("isAdd");
                } else {
                    this.confirmDialogUpdate();
                    this.$emit("isUpdate");
                }
            } catch (err) {
                console.log(err);
            }
        },

        /**
         * Thực hiện đóng dialog, nếu value == 1 thì đóng form popup
         * Author: NDTRUNG(26/11/2022)
         */
        confirmDialogUpdate(value) {
            try {
                this.$emit("confirmDialogUpdate");
                if (value == 1) {
                    this.$emit("close-popup-edit");
                }
            } catch (err) {
                console.log(err);
            }
        },
    },
};
</script>
<style scoped lang="scss">
// @import url(../../../css/components/dialog/updatedialog.css);
@import "../../../scss/components/dialog/updatedialog.scss";
</style>
