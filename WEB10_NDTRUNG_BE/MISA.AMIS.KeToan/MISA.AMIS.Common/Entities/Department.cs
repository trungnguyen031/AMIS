namespace MISA.AMIS.KeToan.Common.Entities
{
    public class Department
    {
        /// <summary>
        /// ID Đơn vị
        /// </summary>
        public Guid DepartmentID { get; set; }

        /// <summary>
        /// Mã Đơn vị
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên Đơn vị
        /// </summary>
        public string DepartmentName { get; set; }


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
