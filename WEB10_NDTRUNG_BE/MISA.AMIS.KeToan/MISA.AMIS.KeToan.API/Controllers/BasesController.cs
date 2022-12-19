using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.BL;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Enums;
using MISA.AMIS.Common.Resources;

namespace MISA.AMIS.KeToan.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasesController<T> : ControllerBase
    {
        #region Field
        private IBaseBL<T> _baseBL;
        #endregion

        #region Constructor
        public BasesController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }

        #endregion


        #region Method

        /// <summary>
        /// API Lấy danh sách tất cả bản ghi (Đã hoàn thiện)
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Created by: NDTRUNG (04/12/2022)
        [HttpGet]
        public IActionResult GetAllRecords()
        {
            try
            {
                var records = _baseBL.GetAllRecords();

                //Xử lý kết quả trả về từ Database
                if (records != null)
                {
                    //Trả về dữ liệu cho Client
                    return StatusCode(StatusCodes.Status200OK, records);
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, new List<T>());
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = AMISErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exception,
                    UserMsg = Resource.DevMsg_Exception,
                    MoreInfo = Resource.MoreInfo_Exception,
                    TraceId = HttpContext.TraceIdentifier
                });
            }
        }

        /// /// <summary>
        /// API Lấy bản ghi theo ID (Đã hoàn thiện)
        /// </summary>
        ///  <param name="recordID">ID nhân viên</param>
        /// <returns>Thông tin chi tiết đối tượng muốn lấy</returns>
        /// Created by: NDTRUNG (23/11/2022)
        [HttpGet]
        [Route("{recordID}")]
        public IActionResult GetRecordByID(Guid recordID)
        {
            try
            {
                //gọi thông qua BL
                var record = _baseBL.GetRecordByID(recordID);

                //Xử lý kết quả trả về từ Database
                if (record != null)
                {
                    //Trả về dữ liệu cho Client
                    return StatusCode(StatusCodes.Status200OK, record);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ErrorResult
                    {
                        ErrorCode = AMISErrorCode.NotFound,
                        DevMsg = Resource.DevMsg_NotFound,
                        UserMsg = Resource.UserMsg_NotFound,
                        MoreInfo = Resource.MoreInfo_NotFound,
                        TraceId = HttpContext.TraceIdentifier
                    });
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = AMISErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exception,
                    UserMsg = Resource.DevMsg_Exception,
                    MoreInfo = Resource.MoreInfo_Exception,
                    TraceId = HttpContext.TraceIdentifier
                });
            }
        }

        /// <summary>
        /// API Xóa 1 bản ghi (Đã hoàn thiện)
        /// </summary>
        /// <param name="record">Đối tượng nhân viên cần xóa</param>
        /// <param name="recordID">ID của nhân viên muốn xóa</param>
        /// <returns>ID của đối tượng vừa xóa</returns>
        /// Created by: NDTRUNG (23/11/2022)
        [HttpDelete]
        [Route("{recordID}")]
        public IActionResult DeleteRecord([FromRoute] Guid recordID)
        {
            try
            {
                var record = _baseBL.DeleteRecord(recordID);
                return StatusCode(StatusCodes.Status200OK, recordID);

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = AMISErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exception,
                    UserMsg = Resource.DevMsg_Exception,
                    MoreInfo = Resource.MoreInfo_Exception,
                    TraceId = HttpContext.TraceIdentifier
                });
            }
        }

        /// /// <summary>
        /// API Thêm mới 1 bản ghi (Đã hoàn thiện)
        /// </summary>
        ///  <param name="record">Bản ghi</param>
        /// <returns>ID Bản ghi vừa thêm mới</returns>
        /// Created by: NDTRUNG (09/12/2022)
        //[HttpPost]
        //public IActionResult InsertRecord([FromBody] T record)
        //{
        //    try
        //    {
        //        //Xử lý kết quả trả về
        //        var result = _baseBL.InsertRecord(record);
        //        if (result.Success)
        //        {
        //            //Thành công 
        //            return StatusCode(StatusCodes.Status201Created, result.Data);
        //        }
        //        else
        //        {
        //            return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
        //            {
        //                ErrorCode = AMISErrorCode.InvalidData,
        //                DevMsg = Resource.DevMsg_InvalidData,
        //                UserMsg = Resource.UserMsg_InvalidData,
        //                MoreInfo = result.Data,
        //                TraceId = HttpContext.TraceIdentifier
        //            });
        //        }
        //    }
        //    //Try catch bắt exception
        //    catch (Exception exception)
        //    {
        //        Console.WriteLine(exception.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
        //        {
        //            ErrorCode = AMISErrorCode.Exception,
        //            DevMsg = Resource.DevMsg_Exception,
        //            UserMsg = Resource.UserMsg_Exception,
        //            MoreInfo = Resource.MoreInfo_Exception,
        //            TraceId = HttpContext.TraceIdentifier
        //        });
        //    }
        //}

        /// <summary>
        /// API Sửa 1 Bản ghi (Đã hoàn thiện)
        /// </summary>
        /// <param name="record">Bản ghi cần sửa</param>
        /// <param name="recordID">ID của Bản ghi muốn sửa</param>
        /// <returns>ID của Bản ghi vừa sửa</returns>
        /// Created by: NDTRUNG (23/11/2022)
        //[HttpPut]
        //[Route("{recordID}")]
        //public IActionResult UpdateRecord([FromRoute] Guid recordID, [FromBody] T record)
        //{
        //    try
        //    {
        //        //Xử lý kết quả trả về
        //        var result = _baseBL.UpdateRecord(recordID, record);
        //        //var employeeId = _employeeBL.UpdateEmployee(employee, employeeID);
        //        return StatusCode(StatusCodes.Status200OK, result.Data);
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
