<template>
    <div class="form-group">
        <label for=""
            >{{ labelForm }}
            <span class="color-red" v-if="required">*</span></label
        >
        <m-input
            inputInfo
            :inputError="validateForm || isValidate"
            @dataInput="inputValue"
            :name="formControlName"
            :valueInput="valueInput"
            @blur="blur"
            :autofocus="autofocus"
            :tabindex="tabindex"
            :tabForcus="tabForcus"
        ></m-input>

        <label for="" class="error_text" v-if="validateForm || isValidate">{{
            errorText || errorMsg
        }}</label>
    </div>
</template>
<script>
import MInput from "../input/MInput.vue";
export default {
    components: { MInput },
    data() {
        return {
            isValidate: false,
            errorMsg: "",
        };
    },
    props: {
        // Lable của form
        labelForm: {
            type: String,
            default: "Lable",
        },
        // Có bắt buộc hay không
        required: {
            type: Boolean,
            default: false,
        },
        // Có validate không
        validateForm: {
            type: Boolean,
        },
        // Hiển thị text lỗi
        errorText: {
            type: String,
            default: "Ô này không được để trống",
        },
        // Tên form control
        formControlName: {
            type: String,
        },
        // Giá trị ô input
        valueInput: {
            type: String,
        },
        // Focus ô đầu tiên
        autofocus: {
            type: Boolean,
            default: false,
        },
        // Tabindex
        tabindex: {
            type: Number,
        },
        // Focus ô cuối
        tabForcus: {
            type: Boolean,
        },
    },
    watch: {
        valueInput: {
            deep: true,
            handler(newVal, oldVal) {},
        },
    },

    methods: {
        /**
         * Tắt error khi validate
         * @param {*} data
         * author : NDTRUNG (19/11/2022)
         */

        inputValue(data) {
            try {
                if (data) {
                    this.isValidate = false;
                    this.errorMsg = ``;
                }
                this.$emit("dataInput", data);
            } catch (err) {
                console.log(err);
            }
        },
        /**
         * Validate 2 form bắt buộc nhập là Mã và Tên Nhân viên
         * @param {*} data
         * author: NDTRUNG (19/11/2022)
         */
        blur(data) {
            try {
                this.$emit("blur");
            } catch (err) {
                console.log(err);
            }
        },
        /**
         * Kiểm tra có phải là số không
         * Author: NDTRUNG(20/11/2022)
         */
        isNumber(value) {
            try {
                return /^-?\d+$/.test(value);
            } catch (err) {
                console.log(err);
            }
        },
    },
};
</script>
<style lang="scss">
// @import url(../../../css/components/formcontrol/formcontrol.css);
@import "../../../scss/components/formcontrol/formcontrol.scss";
</style>
