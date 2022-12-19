<template>
    <div class="m-main">
        <header-content
            :reloadList="getListPageOne"
            @openToastAdd="openToastAdd"
        ></header-content>
        <EmployeeList
            :listEmployee="listEmployee"
            :reloadList="getListPageOne"
            :noContent="noContent"
            @searchValue="searchValue"
            @reloadData="getListPageOne"
        ></EmployeeList>
        <paging-content
            :totalRecord="totalRecord"
            :totalPage="totalPage"
            :pageNumberData="pageNumberData"
            @number-item="funNumberItem"
            @nextPage="clickNextPage"
            @downPage="clickDownPage"
        ></paging-content>
        <m-loading v-if="isLoading"></m-loading>
        <m-toast
            v-if="toastStatus"
            toastAct="thêm mới"
            @closeOpenToast="closeOpenToast"
        ></m-toast>
    </div>
</template>
<script>
import axios from "axios";
import HeaderContent from "../contents/HeaderContent.vue";
import EmployeeList from "../../views/employees/EmployeeList";
import PagingContent from "../contents/PagingContent.vue";
import { BASE_URL } from "../../constans/constans";
import MLoading from "../base/loading/MLoading.vue";
import MToast from "../base/toast/MToast.vue";

export default {
    components: {
        HeaderContent,
        PagingContent,
        EmployeeList,
        MLoading,
        MToast,
    },
    data() {
        return {
            listEmployee: [],
            totalRecord: 0,
            totalPage: 0,
            isLoading: false,
            pageNumberData: 1,
            numberItemData: 10,
            search: "",
            toastStatus: false,
            noContent: false,
        };
    },
    watch: {
        /**
         * Lấy lại danh sách khi thay đổi trang
         * Author: NDTRUNG(20/11/2022)
         */
        pageNumberData() {
            this.getList();
        },

        /**
         * Lấy lại danh sách khi thay số bản ghi trên trang
         * Author: NDTRUNG(20/11/2022)
         */
        numberItemData() {
            this.getListPageOne();
        },

        /**
         * Lấy lại danh sách khi thay giá trị search
         * Author: NDTRUNG(20/11/2022)
         */
        search() {
            this.getListPageOne();
        },
        // isLoading() {
        //     if (this.isLoading == true) {
        //         setTimeout(() => {
        //             this.isLoading == false;
        //         }, 4000);
        //     }
        // },
    },
    updated() {
        // if (this.isLoading == true) {
        //     setTimeout(() => {
        //         this.isLoading == false;
        //     }, 4000);
        // }
    },
    created() {
        // Thực hiện lấy dữ liệu bắt đầu trang
        this.getList();

        // this.eventBus.emit("getList", this.getList());
    },
    methods: {
        // #region Lấy list data
        /**
         * Lấy list data
         * author: NDTRUNG(20/11/2022)
         */
        async getList() {
            // Bật loading
            this.isLoading = true;

            try {
                const res = await axios.get(
                    `${BASE_URL}/filter?${this.search}pageSize=${this.numberItemData}&pageNumber=${this.pageNumberData}`
                );
                if (res) {
                    console.log(res);
                    // Lấy số bản ghi
                    if (res.status == 204) {
                        this.totalRecord = 0;
                        this.noContent = true;
                    } else {
                        this.totalRecord = res.data.TotalRecord;
                        this.noContent = false;
                    }
                    
                    // Gán dữ liệu vào list
                    this.listEmployee = res.data.Data;
                    
                    // Thêm trường checked cho từng ô dữ liệu
                    
                    this.listEmployee.map(e => (e.isChecked = false));
                    
                    // Thêm giá trị đóng mở thanh chức năng
                    this.listEmployee.map(e => (e.closeOpenDelete = false));

                    // Lấy số bản ghi
                    this.totalRecord = res.data.TotalRecord;

                    // Lấy số trang
                    this.totalPage = (this.totalRecord -(this.totalRecord % this.numberItemData)) / this.numberItemData + 1;
                    
                    // Tắt Loading    
                    this.isLoading = false;
                } else {
                    setTimeout(() => {
                        // Tắt Loading
                        this.isLoading = false;
                    }, 4000);
                }
            } catch (error) {
                this.isLoading = false;
                console.log(error);
            }
        },
        // #endregion

        // #region Lấy listdata trang đầu tiên
        /**
         * Lấy listdata trang đầu tiên
         * author: NDTRUNG (20/11/2022)
         */
        getListPageOne() {
            try {
                this.pageNumberData = 1;
                this.getList();
            } catch (err) {
                console.log(err);
            }
        },
        // #endregion

        // #region Lấy số trang và số dữ liệu trên 1 trang từ component Paging
        /**
         * Lấy số trang và số dữ liệu trên 1 trang từ component Paging
         * author: NDTRUNG(20/11/2022)
         */
        funNumberItem(data) {
            try {
                this.pageNumberData = 1;
                this.numberItemData = data;
            } catch (err) {
                console.log(err);
            }
        },
        // #endregion

        // #region Thực hiện tìm kiếm
        /**
         * Thực hiện tìm kiếm
         * author: NDTRUNG(20/11/2022)
         */
        searchValue(data) {
            try {
                if (data) {
                    this.search = `keyword=${data}&`;
                } else {
                    this.search = "";
                }
            } catch (err) {
                console.log(err);
            }
        },
        // #endregion
       
        // #region Tiến lùi số trang
        /**
         * Tiến lùi số trang
         * author: NDTRUNG(20/11/2022)
         */
        clickNextPage(data) {
            try {
                this.pageNumberData = data;
            } catch (err) {
                console.log(err);
            }
        },
        clickDownPage(data) {
            try {
                this.pageNumberData = data;
            } catch (err) {
                console.log(err);
            }
        },
        // #endregion
        
        // #region Hiện Toast Message
        /**
         * Hiện Toast Message
         * author: NDTRUNG(20/11/2022)
         */
        openToastAdd(data) {
            try {
                this.toastStatus = data;
                setTimeout(() => {
                    this.toastStatus = false;
                }, 4000);
            } catch (err) {
                console.log(err);
            }
        },
        // #endregion

         // #region Đóng Toast Message
        /**
         * Đóng Toast Message
         * author: NDTRUNG(20/11/2022)
         */
        closeOpenToast() {
            try {
                this.toastStatus = !this.toastStatus;
            } catch (err) {
                console.log(err);
            }
        },
        // #endregion  
    },
};
</script>

<style lang="scss">
// @import url(../../css/layout/content.css);
@import "../../scss/layout/content.scss";
</style>
