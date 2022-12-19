using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using MySqlConnector;
using MISA.AMIS.KeToan.Common.Entities;
using System.ComponentModel.DataAnnotations;
using MISA.AMIS.BL;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Enums;
using MISA.AMIS.Common.Resources;

namespace MISA.AMIS.KeToan.API.Controllers
{
    //[Route("api/v1/[controller]")]
    //[ApiController]
    public class DepartmentsController : BasesController<Department>
    {
        /// <summary>
        /// API Lấy danh sách tất cả đơn vị
        /// </summary>
        /// <returns>Danh sách tất cả đơn vị</returns>
        /// Created by: NDTRUNG (28/11/2022)
        /// 

        #region Field

        private IDepartmentBL _departmentBL;

        #endregion

        #region Constructor

        public DepartmentsController(IDepartmentBL departmentBL) : base(departmentBL)
        {
            _departmentBL = departmentBL;
        }

        #endregion

        #region Method
        //[HttpGet]
        //public IActionResult GetAllDepartments()
        //{
        //    try
        //    {
        //        var departments = _departmentBL.GetAllDepartments();       
        //        if (departments != null)
        //        {
        //            //Trả về dữ liệu cho Client
        //            return StatusCode(StatusCodes.Status200OK, departments);
        //        }
        //        else
        //        {
        //            return StatusCode(StatusCodes.Status200OK, new List<Department>());
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        Console.WriteLine(exception.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
        //        {
        //            ErrorCode = AMISErrorCode.Exception,
        //            DevMsg = Resource.DevMsg_Exception,
        //            UserMsg = Resource.DevMsg_Exception,
        //            MoreInfo = Resource.MoreInfo_Exception,
        //            TraceId = HttpContext.TraceIdentifier
        //        });
        //    }
        //} 
        #endregion
    }
}
