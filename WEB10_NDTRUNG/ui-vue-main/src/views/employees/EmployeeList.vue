<template>
    <div class="m-main__table">
        <div class="m-main__table-header">
            <div>
                <div class="table-header-left" v-show="listClicked.length > 0">
                    <label for="" class="mr-16"
                        >{{ employeeResource.SELECTED }}:
                        <b>{{ listClicked.length }}</b></label
                    >
                    <label
                        for=""
                        class="lable-unchecked mr-16"
                        @click="clearListDelete"
                        >{{ employeeResource.SELECTED }}</label
                    >
                    <m-button
                        class="btn-delete"
                        @click="deleteBatch()"
                        :btnText="buttonSrc.IS_DELETE"
                    ></m-button>
                </div>
            </div>
            <div class="table-header-right">
                <div>
                    <m-input
                        class="m-w-300 m-input-icon icon-search"
                        placeholder="Tìm kiếm theo tên, mã nhân viên"
                        @dataInput="searchValue"
                        name="searchInput"
                    ></m-input>
                </div>
                <div class="icon-reload tooltip-relative" @click="reloadList()">
                    <div class="tooltip tooltip-reload">
                        <label for=""
                            >Lấy lại dữ liệu
                            <div></div
                        ></label>
                    </div>
                </div>
                <div class="icon-excel tooltip-relative" @click="exportExcel">
                    <div class="tooltip tooltip-excel">
                        <label for=""
                            >Xuất khẩu
                            <div></div
                        ></label>
                    </div>
                </div>
            </div>
        </div>
        <div class="m-main__table-body">
            <table id="tblEmployee" class="m-table">
                <thead>
                    <th class="text-align-center thead__stiky">
                        <input
                            type="checkbox"
                            v-model="checkAll"
                            @click="selectAllTable()"
                        />
                    </th>
                    <th class="text-align-left">MÃ NHÂN VIÊN</th>
                    <th class="text-align-left">TÊN NHÂN VIÊN</th>
                    <th class="text-align-left">GIỚI TÍNH</th>
                    <th class="text-align-center">NGÀY SINH</th>
                    <th class="text-align-left thead__id-number">
                        SỐ CMND
                        <div class="tooltip tooltip__id-number">
                            <label for=""
                                >Số chứng minh nhân dân
                                <div></div
                            ></label>
                        </div>
                    </th>
                    <th class="text-align-left">CHỨC DANH</th>
                    <th class="text-align-left">TÊN ĐƠN VỊ</th>
                    <th class="text-align-left">SỐ TÀI KHOẢN</th>
                    <th class="text-align-left">TÊN NGÂN HÀNG</th>
                    <th
                        class="text-align-left"
                        title="Chi nhánh tài khoản ngân hàng"
                    >
                        CHI NHÁNH TK NGÂN HÀNG
                    </th>
                    <th class="text-align-center">CHỨC NĂNG</th>
                </thead>
                <tbody>
                    <tr
                        :class="employee.isChecked ? 'm-row-selected' : ''"
                        v-for="employee in listEmployee"
                        :key="employee.EmployeeID"
                        @dblclick="employeeEdit($event, employee)"
                    >
                    <td class="text-align-center">
                            <div
                                style="
                                    display: flex;
                                    justify-content: center;
                                    align-items: center;
                                "
                            >
                                <input
                                    type="checkbox"
                                    class="is-checked"
                                    v-model="employee.isChecked"
                                    @click="addListDelete(employee)"
                                />
                            </div>
                        </td>
                        <td class="text-align-left">
                            {{ employee.EmployeeCode }}
                        </td>
                        <td class="text-align-left">
                            {{ employee.EmployeeName }}
                        </td>
                        <td class="text-align-left">
                            {{fomatGender(employee.Gender)  }}
                        </td>
                        <td class="text-align-center">
                            {{ dateTime(employee.DateOfBirth) }}
                        </td>
                        <td class="text-align-left">
                            {{ employee.IdentityNumber }}
                        </td>
                        <td class="text-align-left">
                            {{ employee.PostionName }}
                        </td>
                        <td class="text-align-left">
                            {{ employee.DepartmentName }}
                        </td>
                        <td class="text-align-left">
                            {{ employee.BankAccountNumber }}
                        </td>
                        <td class="text-align-left">
                            {{ employee.BankName }}
                        </td>
                        <td class="text-align-left">
                            {{ employee.BankBranchName }}
                        </td>
                        <td
                            :class="[
                                'text-align-center td-sticky',
                                employee.closeOpenDelete ? 'z-index-5' : '',
                            ]"
                        >
                            <div class="update-table">
                                <button
                                    for=""
                                    @click="employeeEdit($event, employee)"
                                >
                                    Sửa
                                </button>
                                <div
                                    class="icon-update"
                                    @click="
                                        employee.closeOpenDelete =
                                            !employee.closeOpenDelete
                                    "
                                >
                                    <div
                                        :class="[
                                            'icon-down-table icon-update',
                                            employee.closeOpenDelete
                                                ? 'icon-update-focus trasform-icon'
                                                : '',
                                        ]"
                                    ></div>

                                    <div
                                        class="list-update"
                                        v-if="employee.closeOpenDelete"
                                        v-click-away="
                                            () => {
                                                onClickAway($event, employee);
                                            }
                                        "
                                    >
                                        <ul class="z-index-5">
                                            <li
                                                @click="
                                                    oClDialogDelete(
                                                        employee.EmployeeID,
                                                        employee.EmployeeCode
                                                    )
                                                "
                                            >
                                                Xóa
                                            </li>
                                            <li
                                                @click="
                                                    employeeEdit($event, employee, 1)
                                                "
                                            >
                                              Nhân bản
                                            </li>
                                        </ul>
                                        <!-- <div class="overlay-delete"></div> -->
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>

            <EmployeeDetail
                v-if="popupStatus"
                :titlePopup=titlePopup
                :employeeEditItem="employeeEditItem"
                :putEmployee="putEmployeePopup"
                :employeeEdit="employeeEdit"
                :reloadList="reloadList"
                @onClose="onClose"
                @openToastEdit="openToastEdit"
                @reloadData="reloadData"
                :edit="isEdit"
            ></EmployeeDetail>
            <m-delete-dialog
                v-if="isDelete"
                :employeeID="idEmployee"
                :employeeName="nameEmployee"
                :listClicked="listClicked"
                :isDeleteBatch="isDeleteBatch"
                :reloadList="reloadList"
                :oClDialogDelete="oClDialogDelete"
                :employeeEdit="employeeEdit"
                @openToast="openToast"
            ></m-delete-dialog>
            <m-toast
                v-if="toastStatusEdit"
                toastAct="sửa"
                @closeOpenToast="closeOpenToastEdit"
            ></m-toast>
            <m-toast
                v-show="toastStatus"
                toastAct="xóa"
                @closeOpenToast="closeOpenToast"
            ></m-toast>
        </div>
        <div v-if="noContent" class="noContent">
            <b for="" class="">Không có dữ liệu!</b>
        </div>
    </div>
</template>
<script>
import MButton from "../../components/base/button/MButton.vue";
import MDeleteDialog from "../../components/base/dialog/MDeleteDialog.vue";
import MInput from "../../components/base/input/MInput.vue";
import EmployeeDetail from "./EmployeeDetail.vue";
import MToast from "../../components/base/toast/MToast.vue";
import { directive } from "vue3-click-away";
import { switchCase } from "@babel/types";
import axios from 'axios';
import { BASE_URL } from '@/constans/constans';
import { TITLE_POPUP } from "@/constans/resource";
import {
    EMPLOYEE_ROUTER,
    TOOLTIP,
    MS_BUTTON,
    EMPLOYEE_DETAIL,
} from "@/constans/layoutResource";

export default {
    components: { MInput, MButton, MDeleteDialog, EmployeeDetail, MToast },
    props: ["listEmployee", "reloadList", "noContent"],
    data() {
        return {
            checkAll: false,
            totalEmployeeClicked: 0,
            listClicked: [],
            isDelete: false,
            isEdit: true,
            idEmployee: "",
            nameEmployee: "",
            getList: null,
            employeeEditItem: {},
            // mở Popup
            popupStatus: false,
            // Mở Toast
            toastStatus: false,
            // dữ liệu ô Search
            searchValueInput: "",
            putEmployeePopup: true,
            toastStatusEdit: false,
            titlePopup : "",
            isDeleteBatch: false,
            toastMsg: "",
            titlePopup: TITLE_POPUP,
            employeeResource: EMPLOYEE_ROUTER,
            tooltipResource: TOOLTIP,
            buttonSrc: MS_BUTTON,

        };
    },
    // Click outside
    directives: {
        ClickAway: directive,
    },
    // created() {
    //     this.eventBus.on("getList", e => (this.getList = e));
    // },
    methods: {
        // #region Hiển thị giới tính trên bảng Table
        /**
         * Hiển thị giới tính trên bảng Table
         * Author: NDTRUNG(30/11/2022)
         */
        fomatGender(value){
            try {
                switch(value){
                    case 0 :
                        return 'Nam';
                        break;
                    case 1 :
                        return 'Nữ';
                        break;
                    case 2 :
                        return 'Khác';
                        break;
                }
            } catch (err) {
                console.log(err);
            }
        },
        // #endregion

        // #region Lấy giá trị ô input search
        /**
         * Lấy giá trị ô input search
         * Author: NDTRUNG(19/11/2022)
         */
        searchValue(data) {
            try {
                this.searchValueInput = data.value;
                this.$emit("searchValue", this.searchValueInput);
            } catch (err) {
                console.log(err);
            }
        },
        // #endregion

        // #region Hiện Toast Xóa
        /**
         * Hiện Toast Xóa
         * Author: NDTRUNG(26/11/2022)
         */
        openToast(data) {
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

        // #region Đóng mở thanh chức năng
        /**
         * Đóng mở thanh chức năng
         * author: NDTRUNG(19/11/2022)
         */
        closeOpen(index) {
            try {
                index = !index;
            } catch (err) {
                console.log(err);
            }
        },
        // #endregion

        // #region Định dạng hiển thị ngày tháng năm lên bảng
        /**
         * Định dạng hiển thị ngày tháng năm lên bảng
         * author: NDTRUNG(19/11/2022)
         */
        dateTime(dateAPI) {
            try {
                if (dateAPI) {
                    let date = new Date(dateAPI);
                    let day = date.getDate();
                    day = day < 10 ? `0${day}` : day;
                    let month = date.getMonth() + 1;
                    month = month < 10 ? `0${month}` : month;
                    let year = date.getFullYear();
                    return `${day}/${month}/${year}`;
                }
            } catch (err) {
                console.log(err);
            }
        },
        // #endregion

        // #region Tích chọn all table
        /**
         * Tích chọn all table
         * Author: NDTRUNG(19/11/2022)
         */
         selectAllTable() {
            try {
                if (!this.checkAll) {
                    this.listEmployee.map(e => (e.isChecked = true));
                    this.listClicked = this.listEmployee.map(e => e.EmployeeID);
                } else {
                    this.listEmployee.map(e => (e.isChecked = false));
                    this.listClicked = [];
                }
                console.log(this.listClicked);
            } catch (error) {
                console.log(error);
            }
        },
        // #endregion

        // #region Hiện Dialog Delete
        /**
         * Hiện Dialog Delete
         * Author: NDTRUNG(20/11/2022)
         */
        oClDialogDelete(id, name) {
            try {
                this.isDelete = !this.isDelete;
                this.idEmployee = id;
                this.nameEmployee = name;
            } catch (err) {
                console.log(err);
            }
        },
        // #endregion

         /**
         * Tích chọn employe thêm vào list xóa
         * Author: NDTRUNG(09/12/2022)
         */
         addListDelete(employee) {
            try {
                employee.isChecked = !employee.isChecked;
                if (employee.isChecked == true) {
                    this.listClicked.push(employee.EmployeeID);
                } else if (employee.isChecked == false) {
                    this.listClicked = this.listClicked.filter(
                        e => e != employee.EmployeeID
                    );
                }
                if (this.listClicked.length < this.listEmployee.length) {
                    this.checkAll = false;
                }
            } catch (err) {
                console.log(err);
            }
        },

        /**
         * Gọi API thực hiện xóa nhiều
         * Author: NDTRUNG(09/12/2022)
         */
        deleteBatch() {
            try {
                this.isDeleteBatch = true;
                this.isDelete = !this.isDelete;
            } catch (err) {
                console.log(err);
            }
        },

        // #region Định dạng ngày tháng năm
        /**
         * Định dạng ngày tháng năm
         * Author: NDTRUNG(20/11/2022)
         */
        formatDate(date) {
            try {
                if (date) {
                    var d = new Date(date),
                        month = "" + (d.getMonth() + 1),
                        day = "" + d.getDate(),
                        year = d.getFullYear();
    
                    if (month.length < 2) month = "0" + month;
                    if (day.length < 2) day = "0" + day;
    
                    return [year, month, day].join("-");
                }
            } catch (err) {
                console.log(err);
            }
        },

        /**
         * Clear danh sách IP xóa
         */
         clearListDelete() {
            try {
                this.checkAll = false;
                this.listEmployee.map(e => (e.isChecked = false));
                this.listClicked = [];
            } catch (err) {
                console.log(err);
            }
        },

        // #endregion

        // #region Binding thông tin nhân viên cần sửa vào popup sửa nhân viên
        /**
         * Binding thông tin nhân viên cần sửa vào popup sửa nhân viên, Nếu replication == 1, form là nhân bản
         * NDTRUNG(05/12/2022)
         */
        employeeEdit(event, employee, replication) {
            try {
               
                if (replication == 1) {
                    this.isEdit = false;
                    this.titlePopup = "Nhân Bản";
                }
                else{
                    this.titlePopup = "Cập nhật thông tin nhân viên";
                this.isEdit = true;
                }
                //mở employee detail
                this.popupStatus = !this.popupStatus;
                // gán giá trị cho employee cần sửa (employeeEditItem)
                this.employeeEditItem = employee;
                //format kiểu dữ liệu ngày tháng vào ô input
                this.employeeEditItem.DateOfBirth = this.formatDate(
                    this.employeeEditItem.DateOfBirth
                );
                this.employeeEditItem.IdentityIssuedDate = this.formatDate(
                    this.employeeEditItem.IdentityIssuedDate
                );
            } catch (err) {
                console.log(err);
            }
        },
        // #endregion

        // #region Đóng Toast Edit
        /**
         * Đóng Toast Edit
         * author: NDTRUNG (26/11/2022)
         */
        closeOpenToast() {
            try {
                this.toastStatus = !this.toastStatus;
            } catch (err) {
                console.log(err);
            }
        },
        closeOpenToastEdit() {
            try {
                this.toastStatusEdit = !this.toastStatusEdit;
            } catch (err) {
                console.log(err);
            }
        },
        // #endregion

        // #region Đóng Popup Edit
        /**
         * Đóng Popup Edit
         * author: NDTRUNG (26/11/2022)
         */
        onClose() {
            try {
                this.popupStatus = false;
            } catch (err) {
                console.log(err);
            }
        },
        // #endregion

        // #region Hiện Toast Edit
        /**
         * Hiện Toast Edit
         * author: NDTRUNG(26/11/2022)
         */
        openToastEdit(data) {
            try {
                this.toastStatusEdit = data;
                setTimeout(() => {
                    this.toastStatusEdit = false;
                }, 4000);
            } catch (err) {
                console.log(err);
            }
        },
        // #endregion

        // #region Lấy lại dữ liệu
        /**
         * Lấy lại dữ liệu
         * author: NDTRUNG(19/11/2022)
         */
        reloadData() {
            try {
                this.$emit("reloadData");
            } catch (err) {
                console.log(err);
            }
        },

        /**
         * Hàm đóng
         * author: NDTRUNG(03/11/2022)
         */
        onClickAway(event, emp) {
            try {
                emp.closeOpenDelete = false;
            } catch (err) {
                console.log(err);
            }
        },
        // #endregion

        // #region Xuất khẩu
        /**
         * Xuất khẩu
         * Author: NDTRUNG(08/12/2022)
         */
         exportExcel() {
            try {
                axios
                    .post(`${BASE_URL}/ExportExcel`)
                    .then(res => {
                        this.toastMsg = "Xuất khẩu";
                        this.toastStatus = true;
                        setTimeout(() => {
                            this.toastStatus = false;
                        }, 4000);
                        res.config.responseType = "blob";
                        axios(res.config).then(res => {
                            const url = window.URL.createObjectURL(
                                new Blob([res.data])
                            );
                            const link = document.createElement("a");
                            link.href = url;
                            link.setAttribute(
                                "download",
                                "Danh sách nhân viên.xlsx"
                            );
                            document.body.appendChild(link);
                            link.click();
                        });
                    })
                    .catch(e => console.log(e));
            } catch (err) {
                console.log(err);
            }
        },
        // #endregion
    },
};
</script>
<style lang="scss">
// @import url(../../css/view/employeelist.css);
@import "../../scss/view/employeelist.scss";
</style>
