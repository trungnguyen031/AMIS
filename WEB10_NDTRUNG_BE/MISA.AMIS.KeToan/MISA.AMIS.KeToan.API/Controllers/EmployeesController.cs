using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using MySqlConnector;
using System.ComponentModel.DataAnnotations;
using MISA.AMIS.BL;
using MISA.AMIS.Common.Resources;
using MISA.AMIS.Common.Enums;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.Common.Entities.DTO;

namespace MISA.AMIS.KeToan.API.Controllers
{
    //[Route("api/v1/[controller]")]
    //[ApiController]
    // public class EmployeesController : ControllerBase
    public class EmployeesController : BasesController<Employee>
    {

        #region Field

        private IEmployeeBL _employeeBL;

        #endregion

        #region Constructor

        //Khi khởi tạo, truyền đối tượng đã injection bên Program.cs
        public EmployeesController(IEmployeeBL employeeBL) : base(employeeBL)
        {
            //Gán giá trị cho Field = giá trị = employeeBL đã được inject qua Program.cs
            _employeeBL = employeeBL;
        }

        #endregion

        #region Method
        ///// /// <summary>
        ///// API Lấy nhân viên theo ID (Đã hoàn thiện)
        ///// </summary>
        /////  <param name="employeeID">ID nhân viên</param>
        ///// <returns>Thông tin chi tiết 1 nhân viên</returns>
        ///// Created by: NDTRUNG (23/11/2022)
        //[HttpGet]
        //[Route("{employeeID}")]
        //public IActionResult GetEmployeeByID(Guid employeeID)
        //{
        //    try
        //    {
        //        //gọi thông qua BL
        //        var employee = _employeeBL.GetEmployeeByID(employeeID);

        //        //Xử lý kết quả trả về từ Database
        //        if (employee != null)
        //        {
        //            //Trả về dữ liệu cho Client
        //            return StatusCode(StatusCodes.Status200OK, employee);
        //        }
        //        else
        //        {
        //            return StatusCode(StatusCodes.Status404NotFound, new ErrorResult
        //            {
        //                ErrorCode = AMISErrorCode.NotFound,
        //                DevMsg = Resource.DevMsg_NotFound,
        //                UserMsg = Resource.UserMsg_NotFound,
        //                MoreInfo = Resource.MoreInfo_NotFound,
        //                TraceId = HttpContext.TraceIdentifier
        //            });
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

        ///// <summary>
        ///// API Lấy danh sách tất cả nhân viên (Đã hoàn thiện)
        ///// </summary>
        ///// <returns>Danh sách tất cả nhân viên</returns>
        ///// Created by: NDTRUNG (10/11/2022)
        //[HttpGet]
        //public IActionResult GetAllEmployees()
        //{
        //    try
        //    {
        //        var employees = _employeeBL.GetAllEmployees();

        //        //Xử lý kết quả trả về từ Database
        //        if (employees != null)
        //        {
        //            //Trả về dữ liệu cho Client
        //            return StatusCode(StatusCodes.Status200OK, employees);
        //        }
        //        else
        //        {
        //            return StatusCode(StatusCodes.Status200OK, new List<Employee>());
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


        /// /// <summary>
        /// API Thêm mới 1 nhân viên (Đã hoàn thiện)
        /// </summary>
        ///  <param name="employee">Nhân viên</param>
        /// <returns>ID nhân viên vừa thêm mới</returns>
        /// Created by: NDTRUNG (23/11/2022)
        [HttpPost]
        public IActionResult InsertEmployee([FromBody] Employee employee)
        {
            try
            {
                //Xử lý kết quả trả về
                var result = _employeeBL.InsertEmployee(employee);
                //var newEmployeeID = Guid.NewGuid();
                if (result.Success)
                {
                    //Thành công 
                    return StatusCode(StatusCodes.Status201Created, result.Data);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = AMISErrorCode.InvalidData,
                        DevMsg = Resource.DevMsg_InvalidData,
                        UserMsg = Resource.UserMsg_InvalidData,
                        MoreInfo = result.Data,
                        TraceId = HttpContext.TraceIdentifier
                    });
                }
            }
            //Try catch bắt exception
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = AMISErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exception,
                    UserMsg = Resource.UserMsg_Exception,
                    MoreInfo = Resource.MoreInfo_Exception,
                    TraceId = HttpContext.TraceIdentifier
                });
            }
        }

        /// <summary>
        /// API Sửa 1 nhân viên (Đã hoàn thiện)
        /// </summary>
        /// <param name="employee">Đối tượng nhân viên cần sửa</param>
        /// <param name="employeeID">ID của nhân viên muốn sửa</param>
        /// <returns>ID của nhân viên vừa sửa</returns>
        /// Created by: NDTRUNG (23/11/2022)
        [HttpPut]
        [Route("{employeeID}")]
        public IActionResult UpdateEmployee([FromBody] Employee employee, [FromRoute] Guid employeeID)
        {
            try
            {
                //Xử lý kết quả trả về
                var result = _employeeBL.UpdateEmployee(employee, employeeID);
                //var employeeId = _employeeBL.UpdateEmployee(employee, employeeID);
                return StatusCode(StatusCodes.Status200OK, result.Data);
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

        ///// <summary>
        ///// API Lấy mã nhân viên mới tự động tăng
        /// </summary>
        /// <returns>Mã nhân viên mới tự động tăng</returns>
        /// Created by: NDTRUNG (23/11/2022)
        [HttpGet]
        [Route("NewEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                var newEmployeeCode = _employeeBL.GetNewEmployeeCode();

                // Trả về dữ liệu cho client
                return StatusCode(StatusCodes.Status200OK, newEmployeeCode);
            }
            catch (Exception exception)
            {
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
        /// API Lấy danh sách nhân viên cho phép phân trang và tìm kiếm
        /// </summary>
        /// <param keyword="keyword">Từ khóa muốn tìm kiếm</param>
        /// <param name="pageSize">Số trang muốn lấy</param>
        /// <param name="pageNumber">Thứ tự trang muốn lấy</param>
        /// <returns>Một đối tượng gồm:
        /// + Danh sách nhân viên thỏa mãn điều kiện lọc và phân trang
        /// + Tổng số nhân viên thỏa mãn điều kiện</returns>
        /// Created by: NDTRUNG (24/11/2022)
        [HttpGet]
        [Route("filter")]

        public IActionResult FilterEmployees([FromQuery] string? keyword, [FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
        {
            try
            {
                var multipleResults = _employeeBL.FilterEmployees(keyword, pageSize, pageNumber);
                return StatusCode(StatusCodes.Status200OK, multipleResults);

            }
            catch (Exception exception)
            {
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
        /// API xóa hàng loạt nhân viên
        /// </summary>
        /// <param name="listEmpID"></param>
        /// <returns></returns>
        /// Created by: NDTRUNG (07/12/2022)
        [HttpPost]
        [Route("DeleteMultiple")]
        public int DeleteMultipleEmployee(ListEmployeeID listEmployeeID)
        {
            var employees = _employeeBL.DeleteMultipleEmployee(listEmployeeID);
            return employees;
        }

        /// <summary>
        /// Xuất file excel
        /// </summary>
        /// <returns></returns>
        /// Created by: NDTRUNG (08/12/2022)
        [HttpPost]
        [Route("ExportExcel")]
        public IActionResult ExportExcel()
        {
            try
            {
                #region MyRegion
                //var result = _employeeBL.GetAllReCord();
                //// Xử lý khi result  là null 
                //if (result == null || result.Count() == 0)
                //{
                //    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                //        AMISErrorCode.Exception,
                //        Resource.DevMsg_Exception,
                //        Resource.UserMsg_Exception,
                //        moreInfo: Resource.More_Info,
                //        traceId: HttpContext.TraceIdentifier
                //        )
                //);
                //}
                //var stream = new MemoryStream();
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                //using (var package = new ExcelPackage(stream))
                //{
                //    string listColName = "ABCDEFGHI";
                //    var workSheet = package.Workbook.Worksheets.Add(Resource.Excel_List_Employee);

                //    #region Style cho các ô header
                //    workSheet.Cells.Style.Font.SetFromFont("Times New Roman", 11);
                //    workSheet.Cells["A1:I1"].Merge = true;
                //    workSheet.Cells["A2:I2"].Merge = true;
                //    workSheet.Cells["A1"].Value = Resource.Excel_List_Employee;
                //    workSheet.Cells["A1"].Style.Font.SetFromFont("Arial", 16, true);
                //    workSheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                //    workSheet.Cells["A1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                //    workSheet.Cells["A3:I3"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                //    workSheet.Cells["A3:I3"].Style.Fill.BackgroundColor.SetColor(OfficeOpenXml.Drawing.eThemeSchemeColor.Accent3);
                //    workSheet.Cells["A3:I3"].Style.Font.SetFromFont("Arial", 10, true);
                //    workSheet.Cells["A3:I3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                //    workSheet.Cells["A3:I3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                //    #endregion

                //    #region Đặt chiều rộng các cột 
                //    workSheet.Column(1).Width = 5;
                //    workSheet.Column(2).Width = 15;
                //    workSheet.Column(3).Width = 26;
                //    workSheet.Column(4).Width = 12;
                //    workSheet.Column(5).Width = 15;
                //    workSheet.Column(6).Width = 26;
                //    workSheet.Column(7).Width = 26;
                //    workSheet.Column(8).Width = 16;
                //    workSheet.Column(9).Width = 26;
                //    #endregion

                //    #region Header các cột
                //    //var properties = typeof(Employee).GetProperties();
                //    //int i = 1;
                //    //foreach (var prop in properties)
                //    //{
                //    //    var propName = prop.Name;
                //    //    var displayName = propName;
                //    //    var displayAttr = prop.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
                //    //    if (displayAttr != null)
                //    //    {
                //    //        displayName = displayAttr.Name;
                //    //    }
                //    //    workSheet.Cells[string.Format("{0}3", listColName[i - 1])].Value = displayName;
                //    //    i++;
                //    //}
                //    workSheet.Cells["A3"].Value = "STT";
                //    workSheet.Cells["B3"].Value = "Mã nhân viên";
                //    workSheet.Cells["C3"].Value = "Tên Nhân viên";
                //    workSheet.Cells["D3"].Value = "Giới tính";
                //    workSheet.Cells["E3"].Value = "Ngày sinh";
                //    workSheet.Cells["F3"].Value = "Chức Danh";
                //    workSheet.Cells["G3"].Value = "Tên đơn vị";
                //    workSheet.Cells["H3"].Value = "Số tài khoản";
                //    workSheet.Cells["I3"].Value = "Tên ngân hàng";
                //    workSheet.Cells["A3:I3"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                //    #endregion

                //    #region render bản ghi
                //    int rowStart = 3;
                //    foreach (var text in listColName)
                //    {
                //        workSheet.Cells[$"{text}{rowStart}"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                //        workSheet.Cells[$"{text}{rowStart}"].Style.WrapText = true;
                //    }
                //    foreach (var val in result.Select((value, i) => new { i, value }))
                //    {
                //        for (int col = 1; col <= 9; col++)
                //        {
                //            workSheet.Cells[val.i + 1 + rowStart, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                //            workSheet.Cells[val.i + 1 + rowStart, col].Style.WrapText = true;
                //            if (col == 5)
                //            {
                //                workSheet.Cells[val.i + 1 + rowStart, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                //            }
                //        }

                //        workSheet.Cells[val.i + 1 + rowStart, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                //        workSheet.Cells[val.i + 1 + rowStart, 1].Value = val.i + 1;
                //        workSheet.Cells[val.i + 1 + rowStart, 2].Value = val.value.EmployeeCode.ToString();
                //        workSheet.Cells[val.i + 1 + rowStart, 3].Value = val.value.EmployeeName.ToString();
                //        workSheet.Cells[val.i + 1 + rowStart, 4].Value = val.value.Gender == Gender.Male ? Resource.Display_Excel_Male : val.value.Gender == Gender.Female ? Resource.Display_Excel_FeMale : Resource.Display_excel_Other;
                //        workSheet.Cells[val.i + 1 + rowStart, 5].Value = val.value.DateofBirth?.Year == null ? "" : val.value.DateofBirth?.ToString("dd/MM/yyyy");
                //        workSheet.Cells[val.i + 1 + rowStart, 6].Value = val.value.JobPositionName == null ? "" : val.value.JobPositionName.ToString();
                //        workSheet.Cells[val.i + 1 + rowStart, 7].Value = val.value.DepartmentName.ToString() == null ? "" :val.value.DepartmentName.ToString();
                //        workSheet.Cells[val.i + 1 + rowStart, 8].Value = val.value.BankNumber == null ? "" : Int64.Parse(val.value.BankNumber);
                //        workSheet.Cells[val.i + 1 + rowStart, 9].Value = val.value.BankName == null ? "" : val.value.BankName.ToString();

                //    }
                //    package.SaveAs(stream); 
                //    #endregion
                //}
                //stream.Position = 0; 
                #endregion
                var result = _employeeBL.ExportExcel();
                if (result.Success)
                {
                    string excelName = $"Employee-{DateTime.Now.ToString("ddMMyyyyHHmmssfff")}.xlsx";
                    return File((MemoryStream)(result.Data), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = AMISErrorCode.Exception,
                        DevMsg = Resource.DevMsg_Exception,
                        UserMsg = Resource.DevMsg_Exception,
                        MoreInfo = Resource.MoreInfo_Exception,
                        TraceId = HttpContext.TraceIdentifier
                    });

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
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
        #endregion
    }
}

