<template>
    <div class="m-dialog">
        <div class="m-dialog__content">
            <div class="m-dialog__content--main">
                <div class="content">
                    <div class="icon-warning"></div>
                    <div class="message-content">
                        Bạn có chắc chắn muốn xóa {{ isDeleteBatch ? ` ${listClicked.length}` : "" }}
                        Nhân viên đã chọn không?          
                    </div>
                </div>
                <div class="content-footer">
                    <!-- {{ dialogSrc.IS_DELETE }} -->
                    <div>
                        <m-button
                           :btnText="buttonSrc.NO"
                            btnExtra
                            @click="oClDialogDelete"
                            :tabindex="2"
                        ></m-button>
                    </div>
                    <div class="content-footer-left">
                        <m-button
                        class="btnToastDelete"
                            :btnText="buttonSrc.DELETE"
                            @click="handleDelete(employeeID)"
                            :tabindex="1"
                        ></m-button>
                    </div>
                </div>
            </div>
        </div>
        <m-loading v-show="isLoading"></m-loading>
        <div class="overlay" @click="oClDialogDelete"></div>
    </div>
</template>
<script>
import axios from "axios";
import MButton from "../button/MButton.vue";
import { BASE_URL } from "../../../constans/constans";
import MLoading from "../loading/MLoading.vue";
import { MS_BUTTON, DIALOG_TEXT } from "@/constans/layoutResource";

export default {
    components: { MButton, MLoading },
    data() {
        return {
            openToast: true,
            isLoading: false,
            buttonSrc: MS_BUTTON,
            dialogSrc: DIALOG_TEXT,
        };
    },
    props: ["oClDialogDelete", "employeeID", "employeeName", "reloadList","listClicked",
        "isDeleteBatch",],
    methods: {
        /**
         * Thực hiện xóa nhân viên theo ID
         * Author: NDTRUNG (20/11/2022)
         */
        async handleDelete(employee) {
            try {
                this.isLoading = true;
                if (this.listClicked.length > 0) {
                    axios
                        .post(`${BASE_URL}/DeleteMultiple`, {
                            EmployeeIDs: this.listClicked,
                        })
                        .then(res => {
                            this.isLoading = false;
                            this.reloadList();
                            this.oClDialogDelete();
                            this.$emit("openToast", true);
                        })
                        .catch(e => {
                            console.log(e);
                        });
                } else {
                    const res = await axios.delete(`${BASE_URL}/${employee}`);
                    if (res) {
                        this.isLoading = false;
                        this.reloadList();
                        this.oClDialogDelete();
                        this.$emit("openToast", true);
                    }
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Dừng loading
         * Author: NDTRUNG (20/11/2022)
         */
        stopLoading(e) {
            try {
                this.isLoading = !this.isLoading;
            } catch (err) {
                console.log(err);
            }
        },
    },
};
</script>
<style scoped lang="scss">
// @import url(../../../css/components/dialog/deletedialog.css);
@import "../../../scss/components/dialog/deletedialog.scss";
</style>
