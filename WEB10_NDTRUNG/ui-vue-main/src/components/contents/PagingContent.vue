<template>
    <div class="m-main__paging">
        <label for="" class="m-paging__left"
            >Tổng: <b>{{ totalRecord }}</b></label
        >

        <div class="m-paging__right">
            <label class="mr-8" for="">Số bản ghi/trang : </label>
            <div id="combobox__paging" class="combobox mr-16">
                <div class="paging-list">
                    <m-dropdown
                        :listDropdownItem="listPaging"
                        :defaultValue="10"
                        @paging_value="pagingNumber"
                    ></m-dropdown>
                </div>
            </div>

            <label for="" class="paging__label mr-16"
                ><b> {{ updatePage }}</b> bản ghi</label
            >

            <button class="icon-left mr-16" @click="downPage()"></button>
            <button class="icon-right" @click="nextPage()"></button>
        </div>
    </div>
</template>
<script>
import MDropdown from "../base/dropdown/MDropdown.vue";
export default {
    components: { MDropdown },
    props: {
        // Tổng bản ghi
        totalRecord: {
            type: Number,
        },
        // Tổng số trang
        totalPage: {
            type: Number,
        },
        // trang hiện tại
        pageNumberData: {
            type: Number,
        },
    },

    data() {
        return {
            listPaging: [
                {
                    title: 10,
                },
                {
                    title: 20,
                },
                {
                    title: 50,
                },
                {
                    title: 100,
                },
            ],
            pageNumber: 1,
            numberItem: 10,
            fisrtData: 0,
            lastData: 0,
        };
    },
    computed: {
        /**
         * Hiện bản ghi số bắt đầu đến số kết thúc
         * Author: NDTRUNG(25/11/2022)
         */
        updatePage() {
            return `${this.numberItem * (this.pageNumberData - 1) + 1} - ${
                this.numberItem * this.pageNumberData > this.totalRecord
                    ? this.totalRecord
                    : this.numberItem * this.pageNumberData
            }`;
        },
         // Thứ tự của bản ghi bắt đầu lấy = số trang - 1 * tổng số bản ghi trên 1 trang
    },
    emits: ["pageNumber:pageNumber", "numberItem:numberItem"],
    watch: {
        // fisrtData() {
        //     this.fisrtData = this.numberItem * (this.pageNumberData - 1) + 1;
        // },
        // lastData() {
        //     this.lastData = this.pageNumberData = this.totalPage
        //         ? this.numberItem * this.pageNumberData
        //         : this.numberItem * this.pageNumberData -
        //           (this.numberItem * this.pageNumberData - this.totalRecord);
        // },
    },
    methods: {
        /**
         * Tiến 1 trang
         * author: NDTRUNG (25/11/2022)
         */
        nextPage() {
            try {
                this.pageNumber = this.pageNumberData;
                if (this.pageNumber < this.totalPage) {
                    this.pageNumber += 1;
                }
                this.$emit("nextPage", this.pageNumber);
            } catch (err) {
                console.log(err);
            }
        },
        /**
         * lùi 1 trang
         * author: NDTRUNG (25/11/2022)
         */
        downPage() {
            try {
                this.pageNumber = this.pageNumberData;
    
                if (this.pageNumber > 1) {
                    this.pageNumber--;
                }
                this.$emit("downPage", this.pageNumber);
            } catch (err) {
                console.log(err);
            }
        },

        /**
         * lấy dữ liệu dropdown
         * Author: NDTRUNG (25/11/2022)
         */
        pagingNumber(data) {
            try {
                this.numberItem = data;
                this.$emit("number-item", this.numberItem);
            } catch (err) {
                console.log(err);
            }
        },
    },
};
</script>
<style lang="scss">
// @import url(../../css/layout/paging.css);
@import "../../scss/layout/paging.scss";
</style>
