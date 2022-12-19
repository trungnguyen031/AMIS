<template>
    <div>
        <div class="m-main__header">
            <label for="">Nhân viên</label>
            <div class="m-main__header-right">
                <m-button
                    btnText="Thêm mới nhân viên"
                    @click="closeOpenPopup()"
                ></m-button>
            </div>
        </div>
        <EmployeeDetail
            v-if="popupStatus"
            titlePopup="Thêm mới nhân viên"
            :closeOpenPopup="closeOpenPopup"
            :reloadList="reloadList"
            :addEmployeePopup="popupAdd"
            @openToastAdd="openToastAdd"
            @onClose="onClose"
        ></EmployeeDetail>
    </div>
</template>
<script>
import MButton from "../base/button/MButton.vue";
import EmployeeDetail from "../../views/employees/EmployeeDetail.vue";
import MToast from "../base/toast/MToast.vue";

export default {
    components: { MButton, EmployeeDetail, MToast },
    props: ["reloadList"],
    data() {
        return {
            popupStatus: false,
            toastStatus: false,
            // mở nut X popup thêm
            popupAdd: true,
        };
    },
    methods: {
        /**
         * Thực hiện đóng mở popup
         * Author: NDTRUNG (19/11/2022)
         */
        closeOpenPopup() {
            try {
                this.popupStatus = !this.popupStatus;
                this.eventBus.emit("togglePopup", this.popupStatus);
            } catch (err) {
                console.log(err);
            }
        },

        /**
         * Thực hiện đóng mở Toast thêm nhân viên
         * Author: NDTRUNG(19/11/2022)
         */
        openToastAdd(data) {
            try {
                this.$emit("openToastAdd", data);
            } catch (err) {
                console.log(err);
            }
        },

        /**
         * Thực hiện đóng mở Toast sửa nhân viên
         * Author: NDTRUNG(19/11/2022)
         */
        openToastEdit(data) {
            try {
                this.toastStatus = data;
                setTimeout(() => {
                    this.toastStatus = false;
                }, 4000);
            } catch (err) {
                console.log(err);
            }
        },

        /**
         * Thực hiện đóng mở Toast thêm nhân viên
         * Author: NDTRUNG(19/11/2022)
         */
        onClose() {
            try {
                this.popupStatus = false;
            } catch (err) {
                console.log(err);
            }
        },

        /**
         * Đóng toast nhân viên
         * Author: NDTRUNG(19/11/2022)
         */
        closeOpenToast() {
            try {
                this.toastStatus = !this.toastStatus;
            } catch (err) {
                console.log(err);
            }
        },
    },
};
</script>
<style lang="scss">
// @import url(../../css/layout/headercontent.css);
@import "../../scss/layout/headercontent.scss";
</style>
