<template>
  <div class="m-popup">
    <div class="m-popup__content">
      <div class="m-popup__header">
        <div class="m-popup__title">
          <div class="m-popup__title-header">
            <div class="m-popup__title-text">
              {{ titlePopup }}
            </div>
            <label for="">
              <input type="checkbox" class="popup-checkbox" tabindex="22" />
              <span>Là khách hàng</span>
            </label>
            <label for="">
              <input
                id="lastCheckBox"
                type="checkbox"
                class="popup-checkbox"
                tabindex="23"
                @blur="handleTabindex"
              />
              <span>Là nhà cung cấp</span>
            </label>
          </div>
        </div>
        <div class="m-popup__close">
          <div class="icon-help mr-8">
            <div class="tooltip tooltip__help">
              <label for=""
                >Trợ giúp (F1)
                <div></div
              ></label>
            </div>
          </div>
          <div id="js-close" class="icon-close" @click="closePopup(1)">
            <div class="tooltip tooltip__close">
              <label for=""
                >Đóng (ESC)
                <div></div
              ></label>
            </div>
          </div>
        </div>
      </div>
      <div class="m-popup__content-main">
        <div class="m-popup__conntent-employee">
          <div class="content__employee-form">
            <div class="employee-form__left">
              <div class="form-popup">
                <div class="employee-code">
                  <m-form-control
                    labelForm="Mã"
                    required
                    @dataInput="dataForm"
                    formControlName="EmployeeCode"
                    :validateForm="validate.code"
                    :errorText="errors.code"
                    :valueInput="employee.EmployeeCode"
                    :tabindex="1"
                    ref="input"
                    :autofocus="true"
                    @blur="validateFormCode"
                    :tabForcus="tabForcus"
                  ></m-form-control>
                </div>
                <div class="form-group employee-name">
                  <m-form-control
                    labelForm="Tên"
                    required
                    @dataInput="dataForm"
                    :validateForm="validate.name"
                    :errorText="errors.name"
                    formControlName="EmployeeName"
                    :tabindex="2"
                    :valueInput="employee.EmployeeName"
                    @blur="validateFormName"
                  ></m-form-control>
                </div>
              </div>
              <div class="form-popup">
                <div class="form-group department-name">
                  <label for="">Đơn vị <span class="color-red">*</span></label>
                  <m-dropdown
                    :listDropdownItem="listDepartment"
                    defaultValue="Chọn đơn vị"
                    downData
                    :dpName="employee.DepartmentID"
                    :tabindex="3"
                    :validate="validate.DepartmentId"
                    @dropdown-value="dpId"
                    :edit="edit"
                    @validateDD="validateForm"
                    @blur="validateForm"
                  ></m-dropdown>
                  <label
                    for=""
                    class="error_text"
                    v-if="validate.DepartmentId"
                    >{{ errors.DepartmentId }}</label
                  >
                </div>
              </div>
              <div class="form-popup">
                <div class="position-name">
                  <m-form-control
                    labelForm="Chức danh"
                    @dataInput="dataForm"
                    :validateForm="validate.PostionName"
                    :errorText="errors.PostionName"
                    formControlName="PostionName"
                    :tabindex="4"
                    :valueInput="employee.PostionName"
                  ></m-form-control>
                </div>
              </div>
            </div>
            <div class="employee-form__right">
              <div class="form-popup">
                <div class="form-group dob">
                  <label for="">Ngày sinh</label>
                  <input
                    type="date"
                    :class="[
                      'm-input required',
                      validate.DateOfBirth ? 'm-input__error' : '',
                    ]"
                    tabindex="5"
                    v-model="employee.DateOfBirth"
                    @blur="validateForm"
                  />
                  <label for="" class="error_text">{{
                    errors.DateOfBirth
                  }}</label>
                </div>
                <div class="form-group gender">
                  <label for="">Giới tính</label>
                  <div class="m-flex aline-item-center">
                    <input
                      class="input-gender"
                      type="radio"
                      id="Nam"
                      name="fav_language"
                      :value="0"
                      tabindex="6"
                      v-model="employee.Gender"
                      :checked="employee?.Gender == 0"
                    />
                     
                    <label for="Nam" class="lable-gender pb-0">Nam</label><br />
                     

                    <input
                      class="input-gender"
                      type="radio"
                      id="Nam"
                      name="fav_language"
                      :value="1"
                      tabindex="7"
                      v-model="employee.Gender"
                      :checked="employee?.Gender == 1"
                    />

                     
                    <label for="Nữ" class="lable-gender pb-0">Nữ</label><br />
                     

                    <input
                      class="input-gender"
                      type="radio"
                      id="Nam"
                      name="fav_language"
                      :value="2"
                      tabindex="8"
                      v-model="employee.Gender"
                      :checked="employee?.Gender == 2"
                    />

                     
                    <label for="Khác" class="lable-gender pb-0">Khác</label>
                  </div>
                </div>
              </div>
              <div class="form-popup">
                <div class="id-number" title="Số chứng minh nhân dân">
                  <m-form-control
                    labelForm="Số CMND"
                    @dataInput="dataForm"
                    :validateForm="validate.IdentityNumber"
                    :errorText="errors.IdentityNumber"
                    formControlName="IdentityNumber"
                    :valueInput="employee.IdentityNumber"
                    :tabindex="9"
                    @blur="validateForm"
                  ></m-form-control>
                </div>
                <div class="form-group id-date">
                  <label for="">Ngày cấp</label>
                  <input
                    type="date"
                    class="m-input required"
                    v-model="employee.IdentityIssuedDate"
                    tabindex="10"
                  />
                  <label for="" class="error_text">{{
                    errors.IdentityDate
                  }}</label>
                </div>
              </div>
              <div class="form-popup">
                <div class="id-place">
                  <m-form-control
                    labelForm="Nơi cấp"
                    @dataInput="dataForm"
                    :validateForm="validate.IdentityPlace"
                    :errorText="errors.IdentityPlace"
                    formControlName="IdentityPlace"
                    :valueInput="employee.IdentityIssuedPlace"
                    :tabindex="11"
                  ></m-form-control>
                </div>
              </div>
            </div>
          </div>
          <div class="content-employee__contact">
            <div class="form-popup">
              <div class="address">
                <m-form-control
                  labelForm="Địa chỉ"
                  @dataInput="dataForm"
                  formControlName="Address"
                  :validateForm="validate.Address"
                  :errorText="errors.Address"
                  :valueInput="employee.Address"
                  :tabindex="12"
                ></m-form-control>
              </div>
            </div>
            <div class="form-popup">
              <div class="contact" title="Điện thoại di động">
                <m-form-control
                  labelForm="ĐT di động"
                  @dataInput="dataForm"
                  :validateForm="validate.PhoneNumber"
                  :errorText="errors.PhoneNumber"
                  formControlName="PhoneNumber"
                  :valueInput="employee.PhoneNumber"
                  @blur="validateForm"
                  :tabindex="13"
                ></m-form-control>
              </div>
              <div class="contact" title="Điện thoại cố định">
                <m-form-control
                  labelForm="ĐT cố định"
                  @dataInput="dataForm"
                  :validateForm="validate.TelephoneNumber"
                  :errorText="errors.TelephoneNumber"
                  formControlName="TelephoneNumber"
                  :valueInput="employee.TelephoneNumber"
                  @blur="validateForm"
                  :tabindex="14"
                ></m-form-control>
              </div>
              <div class="contact">
                <m-form-control
                  labelForm="Email"
                  @dataInput="dataForm"
                  :validateForm="validate.Email"
                  :errorText="errors.Email"
                  formControlName="Email"
                  :valueInput="employee.Email"
                  @blur="validateForm"
                  :tabindex="15"
                ></m-form-control>
              </div>
            </div>
            <div class="form-popup">
              <div class="contact">
                <m-form-control
                  labelForm="Tài khoản ngân hàng"
                  @dataInput="dataForm"
                  :validateForm="validate.BankAccountNumber"
                  :errorText="errors.BankAccountNumber"
                  formControlName="BankAccountNumber"
                  :valueInput="employee.BankAccountNumber"
                  @blur="validateForm"
                  :tabindex="16"
                ></m-form-control>
              </div>
              <div class="form-group contact">
                <m-form-control
                  labelForm="Tên ngân hàng"
                  @dataInput="dataForm"
                  :validateForm="validate.BankName"
                  :errorText="errors.BankName"
                  formControlName="BankName"
                  :valueInput="employee.BankName"
                  :tabindex="17"
                ></m-form-control>
              </div>
              <div class="form-group contact">
                <m-form-control
                  labelForm="Chi nhánh"
                  @dataInput="dataForm"
                  :validateForm="validate.BankBranchName"
                  :errorText="errors.BankBranchName"
                  formControlName="BankBranchName"
                  :valueInput="employee.BankBranchName"
                  :tabindex="18"
                ></m-form-control>
              </div>
            </div>
          </div>
        </div>
        <div class="m-popup__footer">
          <m-button
            btnText="Hủy"
            btnExtra
            @click="closePopup(1)"
            :tabindex="19"
          ></m-button>
          <div class="m-popup__footer-left">
            <m-button
              btnText="Cất"
              :btnExtra="!edit"
              @click="addEmployee()"
              :tabindex="20"
            ></m-button>
            <m-button
              class="ml-8"
              v-if="!edit"
              btnText="Cất và thêm"
              @click="addEmployee(1)"
              :tabindex="21"
            ></m-button>
          </div>
        </div>
      </div>
    </div>

    <div v-if="addEmployeePopup" class="overlay" @click="closePopup()"></div>
    <div v-if="putEmployee" class="overlay" @click="employeeEdit()"></div>
    <m-warning-dialog
      v-if="isWarning"
      @isWarningDialog="isWarningDialog"
      :employeeCode="this.employee.EmployeeCode"
      :warning="isWarningStatus"
      :errorCode="errorCode"
      :warningCode="dataDialog.show"
    ></m-warning-dialog>
    <m-update-dialog
      v-if="dialogEdit"
      :edit="edit"
      @isAdd="addEmployee(0)"
      @close-popup-edit="closePopup"
      @isUpdate="isEditEmployee"
      @confirmDialogUpdate="confirmDialogUpdate"
    ></m-update-dialog>
    <m-toast
      v-if="isShowToastError"
      :isError="true"
      @closeOpenToast="closeOpenToast"
    ></m-toast>
  </div>
</template>
<script>
import MButton from "../../components/base/button/MButton.vue";
import MDropdown from "../../components/base/dropdown/MDropdown.vue";
import MFormControl from "../../components/base/form-control/MFormControl.vue";
import axios from "axios";
import { BASE_URL, DEPARTMENT_URL } from "../../constans/constans";
import MWarningDialog from "../../components/base/dialog/MWarningDialog.vue";
import MUpdateDialog from "../../components/base/dialog/MUpdateDialog.vue";
import MToast from "../../components/base/toast/MToast.vue";

export default {
  components: {
    MFormControl,
    MDropdown,
    MButton,
    MWarningDialog,
    MUpdateDialog,
    MToast,
  },
  data() {
    return {
      // Thông tin nhân viên
      employee: {
      },
      // Trạng thái validate nhân viên
      validate: {
      },
      // Text cảnh báo nhân viên
      errors: {
      },
      // Danh sách đơn vị
      listDepartment: [],
      // Lấy id của đơn vị truyền xuống dropdown
      dpName: "",
      // ẩn hiện dialog warning?
      isWarning: false,
      // Có phải cảnh báo trùng mã hay không?
      isWarningStatus: false,
      // mã nhân viên có đúng định dạng hay không?
      errorCode: false,
      // bật đóng dialog update
      dialogEdit: false,
      // có update employee hay không
      isEditEmployeeStatus: false,
      // Trạng thái dialog cảnh báo
      dataDialog: {
        show: false,
        message: "",
      },
      // Ẩn hiện toast error
      isShowToastError: false,

      // Tabfocus
      tabForcus: false,
    };
  },
  props: [
    "titlePopup",
    "closeOpenPopup",
    "employeeEditItem",
    "reloadList",
    "addEmployeePopup",
    "putEmployee",
    "employeeEdit",
    "edit",
  ],

  watch: {
    employeeEditItem() {
      console.log(this.employeeEditItem);
    },
    // validateForm() {
    //     this.validate.code = false;
    //     this.errors.code;
    // },
  },
  created() {
    this.getDepartmentList();
    // this.employee = this.employeeEditItem;
    this.getEmployeeEditItem();

    // gán giá trị cho employee
    // if (this.employeeEditItem) {
    //     this.employee = this.employeeEditItem;
    // }
    // Lẫy mã nhân viên mới
    if (!this.edit) {
      this.getnewEmployeeCode();
    }
    // Xử dụng phím tắt
    window.addEventListener("keyup", this.listenerKeyup);
  },

  mounted() {
    console.log(this.$refs.input.focus);
  },

  methods: {
    // #region Tạo sự kiện nhấn phím tắt
    /**
     * Tạo sự kiện nhấn phím tắt
     * author: NDTRUNG (27/11/2022)
     */
    //  listenerKeyup(e) {
    //     try {
    //         if (e.key == "Escape") {
    //             this.closePopup();
    //         } else if (e.ctrlKey) {
    //             if (e.key == "F8") {
    //                 this.addEmployee(1);
    //             }
    //             if (e.key == "F9") {
    //                 this.addEmployee(0);
    //             }
    //         }
    //     } catch (err) {
    //         console.log(err);
    //     }
    // },
    // #endregion

    // #region Bật tắt Dialog update và gửi trạng thái
    /**
     * Bật tắt Dialog update và gửi trạng thái
     * author: NDTRUNG (26/11/2022)
     */
    confirmDialogUpdate() {
      try {
        this.dialogEdit = !this.dialogEdit;
      } catch (err) {
        console.log(err);
      }
    },
    // #endregion

    // #region Lấy Thông tin Nhân viên cần sửa
    /**
     * Lấy Thông tin Nhân viên cần sửa
     * author: NDTRUNG(26/11/2022)
     */
    getEmployeeEditItem() {
      try {
        if (this.employeeEditItem) {
          this.employee = this.employeeEditItem;
        }
      } catch (err) {
        console.log(err);
      }
    },
    // #endregion

    // #region Lấy giá trị ô input gán vào employee
    /**
     * Lấy giá trị ô input gán vào employee
     * author: NDTRUNG (20/11/2022)
     */
    dataForm(data) {
      try {
        switch (data.target) {
          case "EmployeeCode":
            this.employee.EmployeeCode = data.value;
            break;
          case "EmployeeName":
            this.employee.EmployeeName = data.value;

            break;
          case "PostionName":
            this.employee.PostionName = data.value;

            break;
          case "IdentityNumber":
            this.employee.IdentityNumber = data.value;
            break;
          case "IdentityPlace":
            this.employee.IdentityIssuedPlace = data.value;
            break;
          case "Address":
            this.employee.Address = data.value;
            break;
          case "PhoneNumber":
            this.employee.PhoneNumber = data.value;
            break;
          case "TelephoneNumber":
            this.employee.TelephoneNumber = data.value;
            break;
          case "Email":
            this.employee.Email = data.value;
            break;
          case "BankAccountNumber":
            this.employee.BankAccountNumber = data.value;
            break;
          case "BankName":
            this.employee.BankName = data.value;
            break;
          case "BankBranchName":
            this.employee.BankBranchName = data.value;
            break;
          default:
        }
      } catch (err) {
        console.log(err);
      }
    },
    // #endregion

    // #region Lấy mã nhân viên mới
    /*
     * Lấy mã nhân viên mới
     * author: NDTRUNG(27/11/2022)
     */
    getnewEmployeeCode() {
      try {
        axios
          .get(`${BASE_URL}/NewEmployeeCode`)
          .then((res) => (this.employee.EmployeeCode = res.data))
          .catch((e) => console.log(e));
      } catch (error) {
        console.log(error);
      }
    },
    // #endregion

    // #region Validate Form
    /**
     * Validate Form
     * author: NDTRUNG (20/11/2022)
     */
    validateForm() {
      try {
        let isValidate = true;
        (this.validate = {
        }),
          (this.errors = {
          }),
          (this.errors.code = "");
        if (!this.employee.EmployeeCode) {
          this.validate.code = true;
          this.errors.code = "Mã nhân viên không được để trống";
          isValidate = false;
        }
        if (!this.employee.EmployeeName) {
          this.validate.name = true;
          this.errors.name = "Tên nhân viên không được để trống";
          isValidate = false;
        }
        if (!this.employee.DepartmentID) {
          this.validate.DepartmentId = true;
          this.errors.DepartmentId = "Đơn vị không được để trống";
          isValidate = false;
        }
        if (this.validateTime(this.employee.DateOfBirth)) {
          this.validate.DateOfBirth = true;
          this.errors.DateOfBirth = "Ngày sinh không hợp lệ";
          isValidate = false;
        }
        if (this.employee.IdentityNumber) {
          if (!this.isNumber(this.employee.IdentityNumber)) {
            this.validate.IdentityNumber = true;
            this.errors.IdentityNumber = "Số CMND phải là số";
            isValidate = false;
          }
        }
        if (this.employee.IdentityDate) {
          if (this.validateTime(this.employee.IdentityDate)) {
            this.validate.IdentityDate = true;
            this.errors.IdentityDate = "Ngày cấp không hợp lệ";
            isValidate = false;
          }
        }
        if (this.employee.Email) {
          if (!this.isEmail(this.employee.Email)) {
            this.validate.Email = true;
            this.errors.Email = "Email không đúng định dạng";
            isValidate = false;
          }
        }
        if (isValidate == false) {
          return false;
        } else {
          return true;
        }
      } catch (err) {
        console.log(err);
      }
    },
    // #endregion

    // #region Validate ô Mã và Tên nhân viên
    /**
     * Validate ô Mã và Tên nhân viên
     * author: NDTRUNG(20/11/2022)
     */
    validateFormCode() {
      try {
        this.tabForcus = false;
        this.validate.code = false;
        this.errors.code = "";
        if (!this.employee.EmployeeCode) {
          this.validate.code = true;
          this.errors.code = "Mã nhân viên không được để trống";
          return false;
        } else {
          this.validate.code = false;
          this.errors.code = "";
          return true;
        }
      } catch (err) {
        console.log(err);
      }
    },
    validateFormName() {
      try {
        this.validate.name = false;
        this.errors.name = "";
        if (!this.employee.EmployeeName) {
          this.validate.name = true;
          this.errors.name = "Tên nhân viên không được để trống";
          return false;
        } else {
          this.validate.name = false;
          this.errors.name = "";
          return true;
        }
      } catch (err) {
        console.log(err);
      }
    },
    // #endregion

    // #region Validate Ngày tháng, Email, số
    /**
     * Validate Ngày tháng, Email, số
     * author: NDTRUNG (20/11/2022)
     */
    isNumber(value) {
      try {
        return /^-?\d+$/.test(value);
      } catch (err) {
        console.log(err);
      }
    },
    isEmail(email) {
      try {
        let regex =
          /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return regex.test(email);
      } catch (err) {
        console.log(err);
      }
    },
    validateTime(dateTime) {
      try {
        let today = new Date();
        let dataInputDate = new Date(dateTime);
        if (dataInputDate > today) {
          return true;
        }
        return false;
      } catch (err) {
        console.log(err);
      }
    },
    // #endregion

    // #region Gọi API Lấy danh sách Department
    /**
     * Gọi API Lấy danh sách Department
     * author: NDTRUNG (20/11/2022)
     */
    async getDepartmentList() {
      try {
        await axios
          .get(`${DEPARTMENT_URL}`)
          .then((res) => (this.listDepartment = res.data))
          .catch((error) => {
            console.log(error);
          });
      } catch (err) {
        console.log(err);
      }
    },
    // #endregion

    // #region Nhận DepartmentID từ dropdown
    /**
     * Nhận DepartmentID từ dropdown
     * Author: NDTRUNG(20/11/2022)
     */
    dpId(data) {
      try {
        this.employee.DepartmentID = data.id;
      } catch (err) {
        console.log(err);
      }
    },
    // #endregion

    // #region Thêm mới nhân viên
    /**
     * Thêm mới nhân viên
     * author: NDTRUNG (20/11/2022)
     */
     async addEmployee(value) {
            try {
                this.validateForm();
                if (this.validateForm()) {
                    if (this.edit) {
                        try {
                            this.confirmDialogUpdate();
                        } catch (error) {
                            console.log(error);
                        }
                    } else {
                        const res = await axios
                            .post(`${BASE_URL}`, this.employee)
                            .then(res => {
                                this.employee = {};
                                if (value == 0) {
                                    this.getNewEmployeeCode();
                                } else {
                                    this.$emit("onClose");
                                }
                                this.reloadList();

                                this.$emit("openToastAdd", true);
                            })
                            .catch(error => {
                                console.log(error.response);
                                this.errorText = error.response.data.UserMsg;
                                this.isWarningStatus = true;
                                this.errorCode = false;
                                this.isWarningDialog();
                            });
                    }
                }
            } catch (err) {
                console.log(err);
            }
        },
    // #endregion

    // #region Sửa thông tin nhân viên
    /**
     * Sửa thông tin nhân viên
     * author: NDTRUNG(26/11/2022)
     */
     isEditEmployee() {
            try {
                var me = this;
                axios
                    .put(
                        `${BASE_URL}/${this.employee.EmployeeID}`,
                        this.employee
                    )
                    .then(res => {
                        me.$emit("onClose");
                        me.$emit("openToastEdit", true);
                        me.$emit("reloadData");
                    })
                    .catch(e => {
                        this.errorText = e.response.data.UserMsg;
                        me.isWarningDialog();
                    });
            } catch (err) {
                console.log(err);
            }
        },
    // #endregion

    // #region Hiện Dialog warning
    /**
     * Hiện Dialog warning
     * author: NDTRUNG (20/11/2022)
     */
    isWarningDialog() {
      try {
        this.isWarning = !this.isWarning;
      } catch (err) {
        console.log(err);
      }
    },
    // #endregion

    // #region Đóng Popup
    /**
     * Đóng Popup
     * author: NDTRUNG (19/11/2022)
     */
    closePopup() {
      this.$emit("onClose");
    },
    // #endregion

    // #region Focus vào ô mã nhân viên
    /**
     * Focus vào ô mã nhân viên
     * author: NDTRUNG (26/11/2022)
     */
    handleTabindex() {
      try {
        // this.$refs.input.focus();
        this.tabForcus = true;
      } catch (err) {
        console.log(err);
      }
    },
    // #endregion

    // #region Đóng toast lỗi
    /**
     * Đóng toast lỗi
     * author: NDTRUNG (26/11/2022)
     */
    closeOpenToast() {
      try {
        this.isShowToastError = false;
      } catch (err) {
        console.log(err);
      }
    },
    // #endregion
  },
};
</script>
<style lang="scss">
// @import url(../../css/view/employeedetail.css);
@import "../../scss/view/employeedetail.scss";
</style>
