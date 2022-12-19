//Resource
var resource = {
    VI: {
        //Error:
        errorMsg: "Có lỗi xảy ra, vui lòng liên hệ MISA",
        errorValidate: "Dữ liệu không hợp lệ",
        emlpoyyeeisNotEmpty: "Mã nhân viên không được phép để trống",
        employeeIDDumplicated: "Mã nhân viên đã tồn tại trong hệ thống",
        //Warning:
        deleteWarning: "Bạn có muốn xóa nhân viên này không?",
        deleteMultiWarning: "Bạn có muốn xóa hàng loạt bản ghi không?",
        //Alert:
        toastSuccess: "Thành Công!",
        toastAddSucess: "Nhân viên đã được thêm mới!",
        toastUpdateSucess: "Nhân viên đã được cập nhật!",
        toastDeleteSucess: "Nhân viên đã được xóa!",
        //Title:
        updateTitle: "Cập nhật thông tin nhân viên"
    },
    EN: {
        errorMsg: "Error! Please contact MISA for help",
        errorValidate: "Data not validate"
    }
};

// alert(resource["VI"].emlpoyyeeisNotEmpty);

var misaEnum = {
    gender: {
        Male: 1,
        Famele: 0,
        Other: 2
    },
    formMode: {
        Add: 1,
        Edit: 2
    }
}

//  DeleteConfirm: function (msg) {
//     'Ban có chắc chắn muốn xóa ${}'
// }