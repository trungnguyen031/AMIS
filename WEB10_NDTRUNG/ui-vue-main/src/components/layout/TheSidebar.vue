<template>
    <div :class="['m-sidebar', updateSidebar ? 'sidebar-toggle' : '']">
        <div class="m-sidebar__header">
            <div class="icon-toolbar sidebar-hiden"></div>
            <div class="m-sidebar__logo sidebar-hiden"></div>
            <b class="sidebar-hiden">KẾ TOÁN</b>
            <div
                id="mo-rong-side-bar"
                class="icon-option-white"
                @click="openSidebar()"
            >
                <div class="tooltip tooltip__option-white">
                    <label for=""
                        >Mở rộng Sidebar
                        <div></div
                    ></label>
                </div>
            </div>
        </div>
        <div class="m-sidebar__main">
            <div class="m-sidebar__menu">
                <a class="m-sidebar__menu-item item-1">
                    <div class="item-icon icon-dashboard"></div>
                    <div class="item-text sidebar-hiden">Tổng quan</div>
                </a>
                <a class="m-sidebar__menu-item m-sidebar__menu-item--selected item-2">
                    <div class="item-icon icon-cash"></div>
                    <div class="item-text sidebar-hiden">Tiền mặt</div>
                </a>
                <a class="m-sidebar__menu-item item-3">
                    <div class="item-icon icon-bank"></div>
                    <div class="item-text sidebar-hiden">Tiền gửi</div>
                </a>
                <a class="m-sidebar__menu-item item-4">
                    <div class="item-icon icon-purchase"></div>
                    <div class="item-text sidebar-hiden">Mua hàng</div>
                </a>
                <a class="m-sidebar__menu-item item-5">
                    <div class="item-icon icon-sale"></div>
                    <div class="item-text sidebar-hiden">Bán hàng</div>
                </a>
                <a class="m-sidebar__menu-item item-6">
                    <div class="item-icon icon-invoice"></div>
                    <div class="item-text sidebar-hiden">Quản lý hóa đơn</div>
                </a>
                <a class="m-sidebar__menu-item item-7">
                    <div class="item-icon icon-stock"></div>
                    <div class="item-text sidebar-hiden">Kho</div>
                </a>
                <a class="m-sidebar__menu-item item-8">
                    <div class="item-icon icon-tools"></div>
                    <div class="item-text sidebar-hiden">Công cụ dụng cụ</div>
                </a>
                <a class="m-sidebar__menu-item item-9">
                    <div class="item-icon icon-tax"></div>
                    <div class="item-text sidebar-hiden">Thuế</div>
                </a>
                <a class="m-sidebar__menu-item item-10">
                    <div class="item-icon icon-price"></div>
                    <div class="item-text sidebar-hiden">Giá thành</div>
                </a>
                <a class="m-sidebar__menu-item item-11">
                    <div class="item-icon icon-general"></div>
                    <div class="item-text sidebar-hiden">Tổng hợp</div>
                </a>
                <a class="m-sidebar__menu-item item-12">
                    <div class="item-icon icon-budget"></div>
                    <div class="item-text sidebar-hiden">Ngân sách</div>
                </a>
                <a class="m-sidebar__menu-item item-13">
                    <div class="item-icon icon-report"></div>
                    <div class="item-text sidebar-hiden">Báo cáo</div>
                </a>
                <a class="m-sidebar__menu-item item-14">
                    <div class="item-icon icon-finance"></div>
                    <div class="item-text sidebar-hiden">
                        Phân tích tài chính
                    </div>
                </a>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    data() {
        return {
            updateSidebar: null,
        };
    },
    props: {},
    created() {
        /**
         * Lấy giá trị khi mở và thu gọn sidebar
         * Author: NDTRUNG(10/11/2022)
         */
        this.eventBus.on("closeSidebar", e => (this.updateSidebar = e));
    },
    methods: {
        // #region openSidebar
        /**
         * Thực hiện đóng mở sidebar và đẩy biến updateSidebar lên cục bộ
         * Author: NDTRUNG(10/11/2022)
         */
        openSidebar() {
            try {
                this.updateSidebar = !this.updateSidebar;
                this.eventBus.emit("openSidebar", this.updateSidebar);
            } catch (err) {
                console.log(err);
            }
        },
        // #endregion
    },
};
</script>
<style lang="scss">
// @import url(../../css/layout/sidebar.css);
@import "../../scss/layout/sidebar.scss";
</style>
