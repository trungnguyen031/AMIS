using MISA.AMIS.DL;
using MISA.AMIS.KeToan.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.BL
{

    public class DepartmentBL : BaseBL<Department>, IDepartmentBL
    {
        #region Field

        private IDepartmentDL _departmentDL;

        #endregion

        #region Constructor

        public DepartmentBL(IDepartmentDL departmentDL) : base(departmentDL)
        {

            _departmentDL = departmentDL;
        }
        #endregion

        #region Method
        //public IEnumerable<Department> GetAllDepartments()
        //{
        //    return _departmentDL.GetAllDepartments();
        //}
        #endregion
    }

}
