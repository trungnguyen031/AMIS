<template>
    <div
        id="dropdown__department-name"
        class="dropdown"
        @click="dataShow = !dataShow"
        v-click-away="onClickAway"
    >
        <input
            type="text"
            :class="[
                'm-input dropdown__input',
                validate ? 'm-input__error' : '',
            ]"
            readonly
            :value="inputValue || defaultValue"
            @input="changeInput"
            @blur="blur"
            :tabindex="tabindex"
        />
        <div
            :class="[
                'dropdown__btn icon-down-dd',
                dataShow ? 'trasform-icon' : '',
            ]"
        ></div>
        <div
            :class="['dropdown__data', downData ? 'down-data' : '']"
            v-show="dataShow"
        >
            <ul class="z-index-5">
                <li
                    v-for="dropdownItem in listDropdownItem"
                    :key="dropdownItem.title"
                    :class="[
                        'data-item',
                        inputValue == dropdownItem.title ||
                        inputValue == dropdownItem.DepartmentName
                            ? 'item-active'
                            : '',
                    ]"
                    :data-idDpm="dropdownItem.DepartmentID"
                    @click="
                        handleClickDD(
                            $event,
                            dropdownItem.title || dropdownItem.DepartmentName
                        )
                    "
                >
                    {{ dropdownItem.title || dropdownItem.DepartmentName }}
                </li>
            </ul>
        </div>
        <br />
    </div>
</template>
<script>
export default {
    data() {
        return {
            inputValue: 10,
            modelInput: "",
            dataShow: false,
            isActive: false,
            deparId: {
                id: "",
                inputValue: "",
            },
        };
    },
    props: {
        // Lấy list dropdown từ cpn cha để hiện ra danh sách
        listDropdownItem: {
            default: [],
        },
        downData: {
            type: Boolean,
            default: false,
        },
        // Giá trị mặc định của ô input
        defaultValue: {
            type: [Number, String],
        },
        // Lấy Tên của ô đơn vị từ ID
        dpName: {
            type: String,
        },
        //  ID của phòng ban đang được chọn
        currentDepartmentID: {
            type: String,
        },
        // Có phải là dropdown trong Popup edit không?
        edit: {
            type: Boolean,
        },
        // Có đang ở trạng thái validate không
        validate: {
            type: Boolean,
        },
        // Tabindex ô input
        tabindex: {
            type: Number,
        },
        value: {},
    },
    created() {
        /**
         * Gán giá trị khi khởi tạo dropdown
         * author: NDTRUNG (19/11/2022)
         */
        // if (this.dpName) {
        //     var depar = this.listDropdownItem.find(
        //         dp => dp.DepartmentId == this.dpName
        //     ).DepartmentName;
        //     this.inputValue = depar;
        // } else if (!this.edit) {
        //     this.inputValue = this.defaultValue;
        // } else {
        //     this.inputValue = 10;
        // }
        console.log(this.dpName)
    },
    watch: {
        /**
         * Gán giá trị khi khởi tạo dropdown
         * author: NDTRUNG (19/11/2022)
         */
        dpName() {
            if (!this.dpName) {
                this.inputValue = this.defaultValue;
            }
        },
        listDropdownItem(value) {
            if (value) {
                if (this.dpName) {
                    var departmentName = this.listDropdownItem.find(
                        dp => dp.DepartmentID == this.dpName
                    ).DepartmentName;
                    this.inputValue = departmentName;
                } else if (!this.edit) {
                    this.inputValue = this.defaultValue;
                } else {
                    this.inputValue = 10;
                }
            }
        },
    },
    methods: {
        /**
         * click lấy giá trị item gán vào ô input
         * author: NDTRUNG(26/11/2022)
         */
        handleClickDD(event, value) {
            try {
                this.inputValue = value;
                this.deparId.id = event.target.dataset.iddpm;
                this.deparId.inputValue = this.inputValue;
                //truyền id lên
                this.$emit("dropdown-value", this.deparId);
                // truyền giá trị lên 
                this.$emit("paging_value", this.inputValue);
                this.$emit("validateDD");
            } catch (err) {
                console.log(err);
            }
        },

        /**
         * click ra ngoài đóng dropdown
         * author: NDTRUNG (26/11/2022)
         */
        onClickAway(event) {
            try {
                this.dataShow = false;
            } catch (err) {
                console.log(err);
            }
        },

        /**
         * Sự kiện blur dropdown
         * author: NDTRUNG (26/11/2022)
         */
        blur(event) {
            try {
                this.$emit("blur");
                this.$emit("validateDD");
            } catch (err) {
                console.log(err);
            }
        },
    },
};
</script>
<style lang="scss">
/* @import url(../../../css/components/dropdown/dropdown.css); */
@import "../../../scss/components/dropdown/dropdown.scss";
</style>
