using MISA.AMIS.KeToan.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.DL
{
    public interface IDepartmentDL : IBaseDL<Department>
    {

        /// <summary>
        /// API Lấy danh sách tất cả đơn vị
        /// </summary>
        /// <returns>Danh sách tất cả đơn vị</returns>
        /// Created by: NDTRUNG (28/11/2022)
        //public IEnumerable<Department> GetAllDepartments();
    }
}
