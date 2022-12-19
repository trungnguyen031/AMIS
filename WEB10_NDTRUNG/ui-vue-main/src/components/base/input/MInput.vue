<template>
    <input
        ref="input"
        type="text"
        :class="[
            'm-input',
            inputError ? 'm-input__error' : '',
            inputInfo ? 'm-error-info' : '',
        ]"
        :placeholder="placeholder"
        @change="returnValueInput"
        name="name"
        :value="valueInput || ''"
        @blur="onBlur"
        :autofocus="autofocus"
    />
</template>
<script>
export default {
    props: {
        // có phải input bị sai không
        inputError: {
            type: Boolean,
            default: false,
        },
        // Placeholder
        placeholder: {
            type: String,
            default: "",
        },
        name: {
            type: String,
        },
        // có phải input info trong form comtrol không
        inputInfo: {
            type: Boolean,
            default: false,
        },
        // Giá trị ô input
        valueInput: {
            type: String,
            default: "",
        },
        // Focus ô đầu tiên
        autofocus: {
            type: Boolean,
            default: false,
        },
        // Focus ô cuối
        tabForcus: {
            type: Boolean,
        },
    },

    watch: {
        tabForcus(value) {
            if (value == true) {
                this.$refs.input.focus();
            }
        },
    },

    mounted() {
        // forcus ô input đầu tiên có autofocus
        if (this.autofocus) {
            this.$refs.input.focus();
        }
    },
    methods: {
        // Truyền dữ liệu lên cpn cha
        // NDTRUNG(19/11/2022)
        returnValueInput(e) {
            try {
                this.$emit("dataInput", {
                    value: e.target.value,
                    target: this.name,
                });
            } catch (err) {
                console.log(err);
            }
        },
        /**
         * Sự kiện onblur ô input để validate
         * @param {} e
         */
        onBlur(e) {
            try {
                this.$emit("blur", this.valueInput);
            } catch (err) {
                console.log(err);
            }
        },
    },
};
</script>
<style lang="scss">
// @import url(../../../css/components/input/input.css);
@import "../../../scss/components/input/input.scss";
</style>
