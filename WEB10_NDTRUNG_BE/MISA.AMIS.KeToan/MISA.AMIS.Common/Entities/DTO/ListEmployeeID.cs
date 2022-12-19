using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.Entities.DTO
{
    /// <summary>
    /// Danh sách ID của nhân viên
    /// </summary>
    public class ListEmployeeID
    {
        /// <summary>
        /// Danh sách mã nhân viên
        /// </summary>
        public List<Guid> EmployeeIDs { get; set; }
    }
}
