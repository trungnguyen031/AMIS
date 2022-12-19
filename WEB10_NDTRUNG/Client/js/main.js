/**
 * Thực hiện các sự kiện chung
 * author NDTRUNG (18/10/2022)
 */
$(document).ready(function() {
    HandleShortcutkey();
    initEvents();
    loadData();
    handleSearchEmployee();
    deleteMulti()
});
/**
 * Khởi tạo các sự kiện
 * author: NDTRUNG (20/10/2022)
 */
function initEvents() {
    try {
        //Reload lại dữ liệu:
        $("#btnRefresh").click(function() {
            loadData();
        });
        // toggle Sidebar
        toggleSidebar();
        // handle click btn thêm mới nhân viên
        $(".main-header__actions .btn__add-employee").click(function() {
            // open Form
            openForm();
        });
        // hanlde tabIndex
        handleTabIndex();
        // handle dropdown records in page pagination
        handleDropdownPaginate();
        // Ấn nút X đóng form
        $(".form-header__action .m-icon--close").click(function() {
            $(".m-popup-wrapper.warning-close-form").css("display", "flex");
            $(
                ".m-popup-wrapper.warning-close-form .popup-footer__left .btn-sub"
            ).click(function() {
                $(".m-popup-wrapper.warning-close-form").css("display", "none");
            });
            $(
                ".m-popup-wrapper.warning-close-form .popup-footer__right .btn-sub"
            ).click(function() {
                $(".m-popup-wrapper.warning-close-form").css("display", "none");
                $(".textfield__input--required").each(function() {
                    $(this).removeClass("textfield--error");
                });
                hideForm();
            });
            $(
                ".m-popup-wrapper.warning-close-form .popup-footer__right .btn-primary"
            ).click(function() {
                $(".m-popup-wrapper.warning-close-form").css("display", "none");
                handleSaveOnClick();
            });
        });
        // Ấn nút Hủy của dialog
        $(".form-footer__left .btn-sub").click(function() {
            hideForm();
        });
        // Ấn nút cất của dialog
        $("#btnSave").click(handleSaveOnClick);
        $("#btnSave").click(hideForm);
        // Ấn nút cất và thêm của dialog
        $("#btnSaveAdd").click(handleSaveOnClick);
    } catch (error) {
        console.error(error);
    }
}
/**
 * Tạo phím tắt
 * author NDTRUNG(5/11/2022)
 */
function HandleShortcutkey() {
    var checkCtrl = false
    $('*').keydown(function(e) {
        //Nhấn Enter đễ Xác nhận Dialog
        // if (e.keyCode == '13') {
        //     $("#dialogError").hide()
        //         checkCtrl = true
        // }
        //Nhấn Inert để thêm nhân viên
        if (e.keyCode == '45') {
            openForm();
            checkCtrl = true
        }
        // Esc: Đóng form
        if (e.keyCode == '27') {
            hideForm();
            checkCtrl = true
        }
    }).keyup(function(ev) {
        if (ev.keyCode == '17') {
            checkCtrl = true
        }
        //F8: Lưu và cất
    }).keydown(function(event) {
        if (checkCtrl) {
            if (event.keyCode == '119') {
                $("#btnSaveAdd").click();
                checkCtrl = false
            }
        }
        //F9: Hủy
    }).keydown(function(event) {
        if (checkCtrl) {
            if (event.keyCode == '120') {
                $("#btnCancel").click();
                checkCtrl = false
            }
        }
        //F7 : Cất
    }).keydown(function(event) {
        if (checkCtrl) {
            if (event.keyCode == '118') {
                $("#btnSave").click();
                checkCtrl = false
            }
        }
    })
}
/**
 * xử lý TabIndex trong form
 * author NDTRUNG(23/10/2022)
 */
function handleTabIndex() {
    $("#btnCancel").keydown(function(e) {
        e.preventDefault();
        let code = e.keyCode || e.which;

        if (code == "9") {
            $("#ma_nhan_vien").focus();
        }
    });
}
/**
 * Thực hiện hành động click checkbox trong table
 * @param {int} numberReords
 * author NDTRUNG (22/10/2022)
 */
function clickCheckboxTable(numberReords) {
    let numberChecked = 0;
    $("#tblEmployee thead th input").click(function() {
        if ($(this).prop("checked") == true) {
            $("#tblEmployee tbody td input").prop("checked", true);
            // $(".batch-excecution").css("display", "block");
            $(".batch-excecution").fadeIn();
            numberChecked = numberReords;
        } else {
            $("#tblEmployee tbody td input").prop("checked", false);
            // $(".batch-excecution").css("display", "none");
            $(".batch-excecution").fadeOut();

            numberChecked = 0;
        }
        // console.log(numberChecked);
    });
    $;
    $("#tblEmployee tbody td input").click(function() {
        if ($(this).prop("checked") == true) {
            $(this).parent().css("background-color", "#f2f9ff");
            $(this).parent().siblings().css("background-color", "#f2f9ff");
            numberChecked++;
        } else {
            $(this).parent().css("background-color", "#fff");
            $(this).parent().siblings().css("background-color", "#fff");
            numberChecked--;
        }
        if (numberChecked === numberReords) {
            $("#tblEmployee thead th input").prop("checked", true);
        } else {
            $("#tblEmployee thead th input").prop("checked", false);
        }
        if (numberChecked > 0) {
            // $(".batch-excecution").css("display", "block");
            $(".batch-excecution").fadeIn();
        } else {
            // $(".batch-excecution").css("display", "none");
            $(".batch-excecution").fadeOut();
        }
    });
}
/**
 * thực hiện lựa chọn "nhân bản hoặc xóa " trong table
 * author NDTRUNG (22/10/2022)
 */
function hamdleClickDropdownActionTable() {
    let _this;
    $(".m-table__content tbody .m-icon--dropdown").click(function() {
        $(".m-table__content tbody .m-icon--dropdown").addClass("no-selected");
        $(".m-table__content tbody .td-anchor.td-anchor--end").css(
            "z-index",
            "unset"
        );
        _this = $(this);
        $(this).removeClass("no-selected");
        $(".m-table__content .td-anchor--end .m-icon--dropdown.no-selected")
            .children()
            .hide();
        $(this).children().toggle();
        $(this).parent().css("z-index", 1);

        $(".m-table__content .td-anchor--end .m-icon--dropdown").css(
            "border",
            "1px solid #fff"
        );
        $(this).css("border", "1px solid #0075c0");
    });

    $(document).click(function(e) {
        if (_this) {
            if (!_this.is(e.target)) {
                _this.children().hide();
                _this.css("border", "1px solid #fff");
            }
        }
    });
}