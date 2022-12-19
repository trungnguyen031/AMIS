using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.Enums
{   

    /// <summary>
    /// Mã lỗi
    /// </summary>
    public enum AMISErrorCode
    {   
        /// <summary>
        /// Gặp lỗi
        /// </summary>
        Exception = 1,

        /// <summary>
        /// Dữ liệu đầu vào không hợp lệ
        /// </summary>
        InvalidData = 2,

        /// <summary>
        /// Trùng mã
        /// </summary>
        DupicateCode = 3,

        /// <summary>
        /// Không tìm thấy dữ liệu
        /// </summary>
        NotFound = 4,

        /// <summary>
        /// Thêm mới thất bại
        /// </summary>
        InsertFailed = 5,

        /// <summary>
        /// Cập nhật dữ liệu thất bại
        /// </summary>
        UpdateFailed = 6
    }
}
