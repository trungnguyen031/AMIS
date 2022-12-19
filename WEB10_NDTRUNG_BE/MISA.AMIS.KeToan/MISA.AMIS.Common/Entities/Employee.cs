using MISA.AMIS.Common.Attributes;
using MISA.AMIS.KeToan.Common.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MISA.AMIS.KeToan.Common.Entities
{
    public class Employee
    {
        /// <summary>
        /// ID nhân viên
        /// </summary>
        [Key]
        public Guid EmployeeID { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [UniCode("Mã nhân viên đã tồn tại trong hệ thống")]
        [IsNotNullOrEmpty("Mã nhân viên không được phép để trống")]
        [FormatRegex("^[a-zA-Z0-9]+[0-9]$", "Mã nhân viên phải kết thúc bằng số")]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        [BirhOfDate("Ngày sinh phải nhỏ hơn ngày hiện tại , năm sinh lớn hơn 1900")]
        public string EmployeeName { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        [DisplayName("Giới tính")]
        public Gender? Gender { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Số CMND
        /// </summary>
        public string? IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp
        /// </summary>
        public DateTime? IdentityIssuedDate { get; set; }

        /// <summary>
        /// Nơi cấp
        /// </summary>
        public string? IdentityIssuedPlace { get; set; }

        /// <summary>
        /// Chức danh
        /// </summary>
        public string? PostionName { get; set; }

        /// <summary>
        /// ID Đơn vị
        /// </summary>
        [IsNotNullOrEmpty("Vui lòng chọn 1 đơn vị")]
        public Guid DepartmentID { get; set; }

        /// <summary>
        /// Tên Đơn vị
        /// </summary>
   
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Điện thoại di động
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Điện thoại cố định
        /// </summary>
        public string? TelephoneNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [IsEmail("Email không đúng định dạng")]
        public string? Email { get; set; }

        /// <summary>
        /// Tài Khoản Ngân Hàng
        /// </summary>
        public string? BankAccountNumber { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string? BankName { get; set; }

        /// <summary>
        /// Chi nhánh
        /// </summary>
        public string? BankBranchName { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày chỉnh sửa gần nhất
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người chỉnh sửa gần nhất
        /// </summary>
        public string? ModifiedBy { get; set; }
    }
}
