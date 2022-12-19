<template>
    <div class="m-header">
        <div class="m-header__left">
            <div
                id="thu-gon-sidebar"
                class="icon-option sidebar-hiden"
                @click="closeSidebar()"
                v-show="!colHeader"
            >
                <div class="tooltip tooltip__option">
                    <label for=""
                        >Thu gọn Sidebar
                        <div></div
                    ></label>
                </div>
            </div>
            <b class="header-text"
                >CÔNG TY TNHH SẢN XUẤT - THƯƠNG MẠI - DỊCH VỤ QUI PHÚC</b
            >
            <div class="icon-down"></div>
        </div>
        <div class="m-header__right">
            <div class="icon-bell">
                <div class="tooltip tooltip__bell">
                    <label for=""
                        >Thông báo
                        <div></div
                    ></label>
                </div>
            </div>
            <div class="icon-user"></div>
            <b class="user-name">Nguyễn Đức Trung</b>
            <div class="icon-down-small"></div>
        </div>
    </div>
</template>
<script>
export default {
    data() {
        return {
            // Trạng thái icon 3 gạch
            colHeader: false,
        };
    },
    props: ["openCloseSidebar"],
    created() {
        /**
         * Thực hiện lấy giá trị khi đóng mở sidebar trên biến cục bộ
         * Author: NDTRUNG(10/11/2022)
         */
        this.eventBus.on("openSidebar", e => (this.colHeader = e));
    },
    methods: {
        // #region closeSidebar
        /**
         * Ẩn Icon Option trên header khi thu gọn sidebar
         * Author: NDTRUNG(10/11/2022)
         */
        closeSidebar() {
            try {
                this.colHeader = !this.colHeader;
                this.eventBus.emit("closeSidebar", this.colHeader);
            } catch (err) {
                console.log(err);
            }
        },
        // #endregion
    },
};
</script>
<style lang="scss">
// @import url(../../css/layout/header.css);
@import "../../scss/layout/header.scss";
</style>
