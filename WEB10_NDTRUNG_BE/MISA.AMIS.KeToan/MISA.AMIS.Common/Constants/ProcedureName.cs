using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.Constants
{
    public class ProcedureName
    {
        /// <summary>
        /// Template cho Procedcure lấy toàn bộ bản ghi
        /// </summary>
        public static string PROC_GETALL = "Proc_{0}_GetAll";

        /// <summary>
        /// Template cho Procedcure lấy bản ghi theo ID
        /// </summary>
        public static string PROC_GET_BY_ID = "Proc_{0}_GetByID";

        /// <summary>
        /// Template cho Procedcure xóa bản ghi
        /// </summary>
        public static string PROC_DELETE = "Proc_{0}_Delete";

        /// <summary>
        /// Template cho Procedcure lấy mã tự động tăng
        /// </summary>
        public static string PROC_MAXCODE = "Proc_{0}_GetMaxCode";

        /// <summary>
        /// Template cho Procedcure Thêm mới bản ghi
        /// </summary>
        public static string PROC_INSERT = "Proc_{0}_Insert";

        /// <summary>
        /// Template cho Procedcure Sửa bản ghi
        /// </summary>
        public static string PROC_UPDATE = "Proc_{0}_Update";

        /// <summary>
        /// Template cho Procedcure Phân trang và tìm kiếm
        /// </summary>
        public static string PROC_GETPAGING = "Proc_{0}_GetPagging";
    }
}
