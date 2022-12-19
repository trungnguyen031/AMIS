//Hiển thị danh sách, gọi API Thêm, sửa, xóa và Validate dữ liệu
/**
 * Load dữ liêu từ Database
 * author NDTRUNG (21/10/2022)
 */
var formMode = "add";
let searchKeyWord = "";
// var employeeID = null;
async function loadData(pageSize = 100, pageNumber = 1) {
    let numberReords;
    let totalPages;
    let pagingUrl = getPagingURL(pageSize, pageNumber);
    try {
        // gọi api thực hiện lấy dữ liệu
        $(".m-loading").show();
        await $.ajax({
            type: "GET",
            url: pagingUrl,
            success: function(response) {
                records = response.Data;
                numberReords = response.TotalRecord;
                totalPages = response.TotalPage;
                // empty table current
                $(".m-table__content tbody").empty();
                newFunction();
                // $(".td-anchor.td-anchor--end a").click(function () {
                //    openForm();
                // });
                $(".m-table_pagination .pagination__left b").text(numberReords);
                // console.log(totalPages);
                renderListPaginate(pageNumber, pageSize, totalPages);
            },
        });

        // xử lý dữ liệu

        // hanlde event click checkbox table
        clickCheckboxTable(numberReords);
        // handle event click dropdown in coloumn action of table
        hamdleClickDropdownActionTable();
        deleteMulti();
        $(".m-loading").hide();
    } catch (error) {
        console.log(error);
    }
}

function newFunction() {
    for (const item of records) {
        // console.log(item);
        const {
            EmployeeId,
            EmployeeCode,
            EmployeeName,
            Gender,
            GenderName,
            DateOfBirth,
            IdentityNumber,
            IdentityPlace,
            DepartmentName,
            BankAccountNumber,
            BankName,
            BankBranchName,
        } = item;
        $(".m-table__content tbody").append(
            `
                     <tr >
                        <td class="td-anchor td-anchor--start">
                           <input type="checkbox" employeeID=${EmployeeId} />
                        </td>
                        <td>${convertNullString(EmployeeCode)}</td>
                        <td>${convertNullString(EmployeeName)}</td>
                        <td>${convertNullString(GenderName)}</td>
                        <td class="m-text-center">${convertDateOfBirth(
                DateOfBirth
            )}</td>
                        <td>${convertNullString(IdentityNumber)}</td>
                        <td>${convertNullString(IdentityPlace)}</td>
                        <td>${convertNullString(DepartmentName)}</td>
                        <td>${convertNullString(BankAccountNumber)}</td>
                        <td>${convertNullString(BankName)}</td>
                        <td>${convertNullString(BankBranchName)}</td>
                        <td class="td-anchor td-anchor--end m-d-flex-auto" >

                              <a href="#" onclick="onEditBtn('${EmployeeId}')">Sửa</a>

                                 <div
                                 class="m-icon m-icon--dropdown"
                                 style="position:relative"
                                 >
                                    <ul class='dropdownlist'>
                                       <li class='dropdown__item' EmployeeCode=${EmployeeCode}>Nhân bản</li>
                                   
                                       <li class='dropdown__item' onclick="onDelete('${EmployeeId}')">Xóa</li>
                                    </ul>
                                 </div>
                        </td>
                     </tr>
                  `
        );
    }
}

function getPagingURL(pageSize, pageNumber) {
    let pagingUrl = `https://amis.manhnv.net/api/v1/Employees/filter?pageSize=${pageSize}&pageNumber=${pageNumber}`;
    if (searchKeyWord === "") {
        pagingUrl = `https://amis.manhnv.net/api/v1/Employees/filter?pageSize=${pageSize}&pageNumber=${pageNumber}`;
    } else {
        pagingUrl = `https://amis.manhnv.net/api/v1/Employees/filter?pageSize=${pageSize}&pageNumber=${pageNumber}&employeeFilter=${searchKeyWord}`;
    }
    return pagingUrl;
}

/**
 * Mở Form nhân viên
 * author NDTRUNG (18/10/2022)
 */
async function openForm(item = {}) {

    $(".m-form-wrapper").css("display", "flex");
    // Lấy mã nhân viên mới:
    if (formMode == "add") {
        $.ajax({
            url: "https://amis.manhnv.net/api/v1/Employees/NewEmployeeCode",
            method: "GET",
            success: function(newEmployeeCode) {
                $("#ma_nhan_vien").val(newEmployeeCode);
            }
        });
    }
    $("#ma_nhan_vien").focus();
    // load dữ liệu departments vào form
    let departments = await getDataDepartments();
    if (departments.length > 0) {
        $("#ten_don_vi ul").empty();
        for (const item of departments) {
            const { DepartmentId, DepartmentName } = item;

            $("#ten_don_vi ul").append(
                `
            <li class="dropdown__item" departmentId=${DepartmentId}>${DepartmentName}</li>
            `
            );
        }
    }
    // Ấn hiển thị đơn vị (dropdown departments)
    $("#ten_don_vi button").click(function() {
        console.log("dropdown department");
        $("#ten_don_vi ul").toggle();
    });

    // Ấn chọn đơn vị (dropdown departments item)
    $("#ten_don_vi ul li").click(function() {
        let _this = this;
        $("#ten_don_vi ul li").removeClass("active");
        $(this).addClass("active");
        $("#ten_don_vi ul").fadeOut();
        $("#ten_don_vi input").attr({
            value: `${$(_this).text()}`,
            departmentId: `${$(_this).attr("departmentId")}`,
        });
    });
}
/**
 * Đóng Form nhân viên
 * author NDTRUNG (18/10/2022)
 */
function hideForm(item = {}) {
    $(".m-form-wrapper").css("display", "none");
}
/**
 * Xử lý dữ liệu và trả về
 * author NDTRUNG (18/10/2022)
 */
function handleSaveOnClick(employeeID) {
    // validate dữ liệu
    let isValid = validateData();
    if (isValid) {
        //thu thập dữ liệu
        let maNhanVien = $("#ma_nhan_vien").val();
        let tenNhanVien = $("#ten_nhan_vien").val();
        let maDonVi = $("#ten_don_vi input").attr("departmentId");
        // let maDonVi = "142cb08f-7c31-21fa-8e90-67245e8b283e";
        let chucDanh = $("#chuc_danh").val();
        let ngaySinh = $("#ngay_sinh").val();
        let gioiTinh;
        let cccd = $("#cccd").val();
        let ngayCapCCCD = $("#ngay_cap_cccd").val();
        let noiCapCCCD = $("#noi_cap_cccd").val();
        let diaChi = $("#dia_chi").val();
        let dtDiDong = $("#sdt").val();
        let dtCoDinh = $("#sdt_co_dinh").val();
        let email = $("#email").val();
        let tkNganHang = $("#tk_ngan_hang").val();
        let tenNganHang = $("#ten_ngan_hang").val();
        let chiNhanh = $("#chi_nhanh").val();
        // Chọn giới tính
        $(
            ".m-form-wrapper .textfield__container--radio input[type='radio'][name='gender-form']"
        ).each(function() {
            if ($(this).prop("checked") == true) {
                gioiTinh = $(this).attr("value");
            }
        });
        let employee = {
            EmployeeCode: maNhanVien,
            EmployeeName: tenNhanVien,
            Gender: gioiTinh,
            PositionName: chucDanh,
            DateOfBirth: ngaySinh,
            IdentityNumber: cccd,
            IdentityDate: ngayCapCCCD,
            IdentityPlace: noiCapCCCD,
            PhoneNumber: dtDiDong,
            DepartmentId: maDonVi,
            Email: email,
            Address: diaChi,
            TelephoneNumber: dtCoDinh,
            BankAccountNumber: tkNganHang,
            BankName: tenNganHang,
            BankBranchName: chiNhanh,
        };

        console.log("employee data: ", employee);
        let statusCode = null;
        // let api = "";
        // let method = "";
        // //Gọi API thực hiện cất dữ liệu

        // if (type == "edit" && employeeID) {
        //    api = `https://amis.manhnv.net/api/v1/employees/${employeeID}`;
        //    method = "PUT";
        // } else {
        //    api = "https://amis.manhnv.net/api/v1/employees";
        //    method = "POST";
        // }
        // console.log("api: ", api);
        console.log(formMode);
        if (formMode == "edit") {

            try {
                $.ajax({
                    type: "PUT",
                    url: `https://amis.manhnv.net/api/v1/Employees/${employeeID}`,
                    data: JSON.stringify(employee),
                    dataType: "json",
                    contentType: "application/json",
                    success: function(response) {
                        console.log(response);
                        //employee = response;
                        // Thêm nhân viên mới thành công
                        // Ẩn form
                        hideForm();
                        // Hiện toast message
                        $("m-toast-message").css(
                            "display",
                            "block"
                        );
                        $(
                            ".toast-title"
                        ).text(resource["VI"].toastSuccess);
                        $(
                            ".toast-text"
                        ).text(resource["VI"].toastUpdateSucess);

                        $(".m-toast-wrapper").show(function() {
                            $(this).animate({ right: "40px" });
                        });
                        $(".m-toast-wrapper .toast-close").click(function() {
                            $(".m-toast-wrapper").animate({ right: "-500px" },
                                function() {
                                    $(this).hide();
                                }
                            );
                        });
                        setTimeout(function() {
                            $(".m-toast-wrapper").animate({ right: "-500px" },
                                function() {
                                    $(this).hide();
                                }
                            );
                        }, 3000);

                        loadData();
                    },
                });
                console.log("Sua")
                console.log(employee);
                // return departments;
            } catch (error) {
                console.log(error);
                alert(resource["VI"].errorMsg);
            }
        } else {
            /**
             * Thêm mới nhân viên
             * author NDTRUNG (18/10/2022)
             */
            fetch(`https://amis.manhnv.net/api/v1/employees`, {
                    method: "POST",
                    body: JSON.stringify(employee),
                    headers: {
                        "Content-Type": "application/json",
                        // 'Content-Type': 'application/x-www-form-urlencoded',

                    },
                })
                .then((res) => {
                    statusCode = res.status;
                    console.log(res)
                    return res.json();
                })
                .then((res) => {
                    switch (statusCode) {
                        case 400:
                            $(".m-popup-wrapper.error-submit-form").css(
                                "display",
                                "flex"
                            );
                            $(
                                ".m-popup-wrapper.error-submit-form .popup-body__right"
                            ).text(resource["VI"].employeeIDDumplicated);
                            $(
                                ".m-popup-wrapper.error-submit-form .popup-footer__right .btn-primary"
                            ).click(function() {
                                $(".m-popup-wrapper.error-submit-form").css(
                                    "display",
                                    "none"
                                );
                            });
                            break;
                        case 201: // Thêm nhân viên mới thành công
                            // Ẩn form
                            //hideForm();
                            // Hiện toast message
                            $("m-toast-message").css(
                                "display",
                                "block"
                            );
                            $(
                                ".toast-title"
                            ).text(resource["VI"].toastSuccess);
                            $(
                                ".toast-text"
                            ).text(resource["VI"].toastAddSucess);
                            $(".m-toast-wrapper").show(function() {
                                $(this).animate({ right: "40px" });
                            });
                            $(".m-toast-wrapper .toast-close").click(function() {
                                $(".m-toast-wrapper").animate({ right: "-500px" },
                                    function() {
                                        $(this).hide();
                                    }
                                );
                            });
                            setTimeout(function() {
                                $(".m-toast-wrapper").animate({ right: "-500px" },
                                    function() {
                                        $(this).hide();
                                    }
                                );
                            }, 3000);

                            loadData();
                            //load lại dữ liệu
                            break;
                        default:
                            alert(resource["VI"].errorMsg);
                            break;

                    }
                })
                .catch((err) => {
                    console.log(err);
                });
        }
        //  kiểm tra kết quả trả vê -> đưa ra thông báo
    }
}
/**
 * thực hiện validate dữ liệu
 * author NDTRUNG (20/10/2022)
 */
function validateData() {
    let isValid = true;
    // các thông tin bắt buộc nhập
    $(".textfield__input--required").each(function() {
        // console.log(element);
        let value = $(this).val();
        if (!value) {
            $(this).addClass("textfield--error");
            isValid = false;
        } else {
            $(this).removeClass("textfield--error");
        }
        // $(this).parent().removeClass("textfield--error");
    });
    // các thông tin đúng định dạng

    // ngày tháng
    return isValid;
}

/**
 * Thực hiện get dữ liệu các đơn vị
 * @returns danh sách các đơn vị
 * author NDTRUNG(10/11/2022)
 */
async function getDataDepartments() {
    try {
        let departments = [];
        await $.ajax({
            type: "GET",
            url: "https://amis.manhnv.net/api/v1/Departments",
            success: function(response) {
                departments = [...response];
            },
        });
        // console.log(departments);
        return departments;
    } catch (error) {
        console.log(error);
    }
}
/**
 *
 * @param {string} date
 * @returns cấu trúc DD/MM/YYYY
 * author NDTRUNG(23/10/2022)
 */
function convertDateOfBirth(date) {
    if (checkForamtDate(date)) {
        if (date) {
            date = new Date(date);
            let day = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
            let month =
                date.getMonth() < 9 ? "0" + date.getMonth() : date.getMonth() + 1;
            let year = date.getFullYear();
            return `${day}-${month}-${year}`;
        }
    }
    return "";
}
/**
 *
 * @param {string} date
 * @returns kiểm tra đầu vào đúng định dạng không
 * author NDTRUNG(23/10/2022)
 */
function checkForamtDate(date) {
    return new Date(date) !== "Invalid Date" && !isNaN(new Date(date));
}
/**
 * kiểm tra chuỗi và trả về chuổi rỗng
 * author NDTRUNG(23/10/2022)
 */
function convertNullString(str) {
    return str ? str : "";
}
/**
 * Sửa một bản ghi
 * author NDTRUNG(30/10/2022)
 */
async function onEditBtn(employeeID) {
    formMode = "edit";
    let employee = null;
    //Binding dữ liệu bằng API
    try {
        await $.ajax({
            type: "GET",
            url: `https://amis.manhnv.net/api/v1/Employees/${employeeID}`,
            success: function(response) {
                employee = response;
            },
        });
        console.log(employee);
        // return departments;
    } catch (error) {
        console.log(error);
    }
    openForm();
    $(
        ".m-form-container .form__header .form-header__title"
    ).text(resource["VI"].updateTitle);

    if (employee) {
        let {
            //employeeID = $(this).data(employee.employeeID),
            Address,
            BankAccountNumber,
            BankBranchName,
            BankName,
            DateOfBirth,
            DepartmentId,
            DepartmentName,
            Email,
            EmployeeCode,
            EmployeePosition,
            EmployeeName,
            Gender,
            GenderName,
            IdentityDate,
            IdentityNumbe,
            IdentityPlace,
            PhoneNumber,
            TelephoneNumber,
        } = employee;

        $("#ma_nhan_vien").val(EmployeeCode);
        $("#ten_nhan_vien").val(EmployeeName);
        $("#ten_don_vi input").attr({
            departmentId: DepartmentId,
            value: DepartmentName,
        });
        //  "142cb08f-7c31-21fa-8e90-67245e8b283e";
        $("#chuc_danh").val(EmployeePosition);
        $("#ngay_sinh").val(convertDateOfBirth(DateOfBirth));
        $(".textfield__input--radio").each(function() {
            if ($(this).attr("value") == Gender) {
                $(this).prop("checked", true);
            }
        });
        $("#cccd").val(IdentityNumbe);
        $("#ngay_cap_cccd").val(IdentityDate);
        $("#noi_cap_cccd").val(IdentityPlace);
        $("#dia_chi").val(Address);
        $("#sdt").val(PhoneNumber);
        $("#sdt_co_dinh").val(TelephoneNumber);
        $("#email").val(Email);
        $("#tk_ngan_hang").val(BankAccountNumber);
        $("#ten_ngan_hang").val(BankName);
        $("#chi_nhanh").val(BankBranchName);
        // handle select gender
        // $(
        //    ".m-form-wrapper .textfield__container--radio input[type='radio'][name='gender-form']"
        // ).each(function () {
        //    if ($(this).prop("checked") == true) {
        //       gioiTinh = $(this).attr("value");
        //    }
        // });
        $(".form-footer__right .btn-primary").click(function() {
            handleSaveOnClick(employeeID);
        });
    }
}
/**
 * Xóa một bản ghi
 * author NDTRUNG(30/10/2022)
 */
async function onDelete(employeeID) {
    $(".m-popup-wrapper.warning-delete-form").css("display", "flex");
    $(
        ".warning-delete-form .popup-body__right"
    ).text(resource["VI"].deleteWarning);
    $(
        ".m-popup-wrapper.warning-delete-form .popup-footer__left .btn-sub"
    ).click(function() {
        $(".m-popup-wrapper.warning-delete-form").css("display", "none");
    });
    $(
        ".m-popup-wrapper.warning-delete-form .popup-footer__right .btn-primary"
    ).click(function() {
        $(".m-popup-wrapper.warning-delete-form").css("display", "none");
        try {
            $.ajax({
                type: "DELETE",
                url: `https://amis.manhnv.net/api/v1/Employees/${employeeID}`,
                success: function(response) {
                    if (response == 1) {
                        $("m-toast-message").css(
                            "display",
                            "block"
                        );
                        $(
                            ".toast-title"
                        ).text(resource["VI"].toastSuccess);
                        $(
                            ".toast-text"
                        ).text(resource["VI"].toastDeleteSucess);

                        $(".m-toast-wrapper").show(function() {
                            $(this).animate({ right: "40px" });
                        });
                        $(".m-toast-wrapper .toast-close").click(function() {
                            $(".m-toast-wrapper").animate({ right: "-500px" },
                                function() {
                                    $(this).hide();
                                }
                            );
                        });
                        setTimeout(function() {
                            $(".m-toast-wrapper").animate({ right: "-500px" },
                                function() {
                                    $(this).hide();
                                }
                            );
                        }, 3000);
                    }
                },
            });
            // console.log(departments);
        } catch (error) {
            console.log(error);
        }

        let pageSize;
        pageSize = $(".m-table_pagination .record-in-page input").attr(
            "numberRecord"
        );
        loadData(pageSize);
    });
}