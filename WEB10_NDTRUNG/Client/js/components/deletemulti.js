/**
 * xóa nhiều bàn ghỉ
 * author NDTRUNG (30/10/2022)
 */
async function deleteMulti() {
    await $(".batch-excecution .btn-delete").click(function() {
        let arrID = [];
        $(".td-anchor--start input").each(function() {
            if ($(this).prop("checked") == true) {
                let id = $(this).attr("employeeID");
                arrID.push(id);
            }
        });
        console.log("check arr : ", arrID);
        $(".m-popup-wrapper.warning-delete-form").css("display", "flex");
        $(
            ".warning-delete-form .popup-body__right"
        ).text(resource["VI"].deleteMultiWarning);
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
                arrID.forEach((employeeID) => {
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
                            console.log("res delete multi");
                        },
                    });
                });
                // console.log(departments);
            } catch (error) {
                console.log(error);
            }
            let pageSize;
            pageSize = $(".m-table_pagination .record-in-page input").attr(
                "numberRecord"
            );

            $(".batch-excecution").fadeOut();
            loadData(pageSize);
        });

    });

}