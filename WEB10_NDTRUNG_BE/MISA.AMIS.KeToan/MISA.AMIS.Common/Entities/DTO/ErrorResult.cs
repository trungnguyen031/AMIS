using MISA.AMIS.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.Entities
{   
    /// <summary>
    /// Lỗi trả về
    /// </summary>
    public class ErrorResult
    {   
        /// <summary>
        /// Mã lỗi
        /// </summary>
        public AMISErrorCode ErrorCode { get; set; }

        /// <summary>
        /// Lỗi trả về cho Dev
        /// </summary>
        public string DevMsg { get; set; }

        /// <summary>
        /// Lỗi trả về cho Người dùng
        /// </summary>
        public string UserMsg { get; set; }

        /// <summary>
        /// Thông tin chi tiết
        /// </summary>
        public object MoreInfo { get; set; }

        /// <summary>
        /// TraceID
        /// </summary>
        public string? TraceId { get; set; }

    }
}
