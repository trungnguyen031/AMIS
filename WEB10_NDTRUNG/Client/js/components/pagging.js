/**
 * Phân trang
 * author NDTRUNG(12/11/2022)
 */
function handleDropdownPaginate() {
    $(".m-table_pagination .record-in-page .textfield__icon").click(function() {
        $(
            ".m-table_pagination .record-in-page .dropdownlist--pagination"
        ).toggle();
        // số bản ghi trên 1 trang
        let pageSize;
        $(
            ".m-table_pagination .record-in-page .dropdownlist--pagination .dropdown__item"
        ).click(function() {
            // set value cho thẻ input
            let _this = $(this);
            $(".m-table_pagination .record-in-page input").val(
                _this.text().trim()
            );
            $(".m-table_pagination .record-in-page input").attr({
                numberRecord: _this.attr("value"),
            });
            // ẩn dropdown
            $(this).parent().hide();
            pageSize = $(".m-table_pagination .record-in-page input").attr(
                "numberRecord"
            );
            // load lại table
            loadData(pageSize);
        });
    });
}
/**
 *
 * @param {number} pageNumber
 * @param {number} totalPages
 * xử lý hiển thị phân trang
 */
function renderListPaginate(pageNumber, pageSize, totalPages) {
    console.log("pageNumber : ", pageNumber);
    // render danh sách trang
    $(".pagination-right__action").empty();
    $(".pagination-right__action").append(`
     
         <div class="page-list m-d-flex">
            <div class="page-item page-item--current">1</div>
            <div class="page-item">2</div>
         </div>
         <span class="btn-pagination btn-pagination__prev m-icon m-icon-left ${
            pageNumber == 1 ? "no-click-paginate" : ""
         }"></span>
      <span class="btn-pagination btn-pagination__last m-icon m-icon-right ${
         pageNumber == totalPages ? "no-click-paginate" : ""
      }"></span>
   `);
    $(".pagination-right__action .page-list").empty();
    for (let i = 1; i <= totalPages; i++) {
        $(".pagination-right__action .page-list").append(`
   <div class="page-item ${
      i === pageNumber ? "page-item--current" : ""
   }">${i}</div>
   `);
    }
    // xử lý sự kiện chọn trang
    $(".pagination-right__action .page-item").click(function() {
        let pageNumber = parseInt($(this).text().trim());

        console.log("hanlde pagenumber", pageNumber, pageSize);
        loadData(pageSize, pageNumber);
    });
    // xử lý sự kiện click trước sau
    $(".btn-pagination__prev").click(function() {
        if (pageNumber > 1) {
            pageNumber--;
            loadData(pageSize, pageNumber);
        }
    });
    $(".btn-pagination__last").click(function() {
        if (pageNumber < totalPages) {
            pageNumber++;
            loadData(pageSize, pageNumber);
        }
    });
}

/**
 * Tìm kiếm
 * author NDTRUNG(13/11/2022)
 */

function handleSearchEmployee() {
    try {
        $("#searchEmployee").change(function() {
            searchKeyWord = $(this).val();
            loadData();
        });
    } catch (error) {
        console.log(error);
    }
}