using Microsoft.AspNetCore.Http;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Enums;
using MISA.AMIS.Common.Resources;
using MISA.AMIS.DL;
using MISA.AMIS.KeToan.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.Style;
using MISA.AMIS.KeToan.Common.Enums;
using MISA.AMIS.Common.Entities.DTO;
using MISA.AMIS.Common.Attributes;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace MISA.AMIS.BL
{
    //EmployeeBL kế thừa  BaseBL<Employee>, implement IEmployeeBL
    public class EmployeeBL : BaseBL<Employee>, IEmployeeBL
    {
        #region Field

        private IEmployeeDL _employeeDL;
        //private để trong Class EmployeeBL mới sửa được _employeeDL

        #endregion

        #region Constructor

        public EmployeeBL(IEmployeeDL employeeDL) : base(employeeDL)
        {
            //khởi tạo đối tượng
            _employeeDL = employeeDL;
        }
        #endregion

        #region Method

        /// <summary>
        /// API Lấy danh sách nhân viên cho phép phân trang và tìm kiếm
        /// </summary>
        /// <param keyword="keyword">Từ khóa muốn tìm kiếm</param>
        /// <param name="pageSize">Số trang muốn lấy</param>
        /// <param name="pageNumber">Thứ tự trang muốn lấy</param>
        /// <returns>Một đối tượng gồm:
        /// + Danh sách nhân viên thỏa mãn điều kiện lọc và phân trang
        /// + Tổng số nhân viên thỏa mãn điều kiện</returns>
        /// Created by: NDTRUNG (01/12/2022)
        public PagingResult<Employee> FilterEmployees(string? keyword, int pageSize = 10, int pageNumber = 1)
        {
            return _employeeDL.FilterEmployees(keyword, pageSize, pageNumber);
        }

        /// <summary>
        /// Xóa nhiều nhân viên
        /// </summary>
        /// <param name="listEmpID">Danh sách iD nhân viên cần xóa</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: NDTRUNG (07/12/2022)
        public int DeleteMultipleEmployee(ListEmployeeID listEmployeeID)
        {
            return _employeeDL.DeleteMultipleEmployee(listEmployeeID);
        }

        /// /// <summary>
        /// Lấy Toàn bộ nhân viên(Đã hoàn thiện)
        /// </summary>
        /// <returns>Danh sách nhân viên nhân viên</returns>
        /// Created by: NDTRUNG (23/11/2022)
        //public IEnumerable<Employee> GetAllEmployees()
        //{
        //    return _employeeDL.GetAllEmployees();
        //}

        /// /// <summary>
        /// Lấy danh nhân viên theo ID (Đã hoàn thiện)
        /// </summary>
        ///  <param name="employeeID">ID nhân viên</param>
        /// <returns>Thông tin chi tiết 1 nhân viên</returns>
        /// Created by: NDTRUNG (23/11/2022)
        //public Employee GetEmployeeByID(Guid employeeID)
        //{   
        //    //trọc vào DL
        //    return _employeeDL.GetEmployeeByID(employeeID);
        //}

        /// </summary>
        /// <returns>Mã nhân viên mới tự động tăng</returns>
        /// Created by: NDTRUNG (03/12/2022)
        public string GetNewEmployeeCode()
        {
            return _employeeDL.GetNewEmployeeCode();
        }


        /// /// <summary>
        /// Thêm mới 1 nhân viên (Đã hoàn thiện)
        /// </summary>
        ///  <param name="employee">Đối tượng Nhân viên cần thêm mới</param>
        /// <returns>ID của nhân viên vừa thêm. Nếu insert thất bại thì return về Guid rỗng</returns>
        /// Created by: NDTRUNG (04/12/2022)
        public ServiceResponse InsertEmployee(Employee employee)
        {
            var validateResult = ValidateRequestData(null, employee);
            if (validateResult != null && validateResult.Success)
            {
                //employee.EmployeeID = Guid.NewGuid();
                var newEmployeeID = _employeeDL.InsertEmployee(employee);
                if (newEmployeeID != Guid.Empty)
                {
                    return new ServiceResponse
                    {
                        Success = true,
                        Data = newEmployeeID
                    };
                }
                else
                {
                    return new ServiceResponse
                    {
                        Success = false,

                        Data = new ErrorResult
                        {
                            ErrorCode = AMISErrorCode.InsertFailed,
                            DevMsg = Resource.DevMsg_InsertFailed,
                            UserMsg = Resource.UserMsg_InsertFailed,
                            MoreInfo = Resource.MoreInfo_Exception
                        }
                    };
                }
            }
            else
            {
                return new ServiceResponse
                {
                    Success = false,
                    Data = validateResult?.Data
                };
            }
           
        }

        /// <summary>
        /// Validate dữ liệu truyền lên API
        /// </summary>
        /// <param name="employee">Đối tượng nhân viên cần validate</param>
        /// /// <param name="httpContext">HttpContext truyền vào từ request API</param>
        /// <returns>Đối tượng ServiceResponse mô tả validate thành công hay thất bại</returns>
        /// Created by: NDTRUNG (04/12/2022)
        public ServiceResponse ValidateRequestData(Guid? recordID, Employee record)
        {
            //lấy ra tất cả attribute có attribute là "IsNotNullOrEmptyAttribute"
            var properties = record.GetType().GetProperties();
            //var properties = record.GetType().GetProperties();
            foreach (var prop in properties)
            {
                var propName = prop.Name;
                // lấy giá trị của property truyền lên
                var propValue = prop.GetValue(record);

                var isUniqueCode = Attribute.IsDefined(prop, typeof(UniCodeAttribute));

                // Kiểm tra xem property có attribute là endWithNumber không
                var isEndWithNumber = Attribute.IsDefined(prop, typeof(EndWithNumberAttribute));

                // Kiểm tra xem property có attribute là isNotNullOrEmpty không
                var isNotNullOrEmpty = Attribute.IsDefined(prop, typeof(IsNotNullOrEmptyAttribute));

                // Kiểm tra xem property có attribute là isEmail không
                var isEmail = Attribute.IsDefined(prop, typeof(IsEmailAttribute));

                // Kiểm tra xem property có attribute là BirhOfDate không
                var BirhOfDate = Attribute.IsDefined(prop, typeof(BirhOfDateAttribute));

                // Kiểm tra xem property có attribute là OnlyNumber không
                var isRegexFormat = Attribute.IsDefined(prop, typeof(FormatRegexAttribute));


                if (isUniqueCode == true)
                {
                    //Kiểm tra nếu recordID là null thì gán cho record ID bằng Guid Empty
                    if (recordID == null)
                    {
                        recordID = Guid.Empty;
                    }
                    //lấy ra bản ghi trước khi chỉnh sửa , ép kiểu Guid cho recordID, = null thì là 000-0000-000-000-000
                    var oldRecord = _employeeDL.GetRecordByID((Guid)recordID);
                    bool compareCode = false;
                    // Lấy ra mã trước lúc chỉnh sửa
                    if (oldRecord != null)
                    {
                        var oldRecordCode = oldRecord.GetType().GetProperty(propName).GetValue(oldRecord);
                        compareCode = CompareCode(oldRecordCode.ToString(), propValue.ToString(), 2);

                    }

                    // so sánh 2 mã  ( true nếu mã cũ lớn hơn mã mới)
                    if (compareCode)
                    {
                        return new ServiceResponse
                        {
                            Success = false,

                            Data = new ErrorResult
                            {
                                ErrorCode = AMISErrorCode.DupicateCode,
                                DevMsg = Resource.DevMsg_InvalidData,
                                UserMsg = Resource.UserMsg_Code_LessThan,
                                MoreInfo = Resource.MoreInfo_Exception
                            }
                        };
                    }
                    var data = _employeeDL.CheckDuplicate(propValue.ToString()).Data;
                    var codeData = data.GetType().GetProperty("recordCode").GetValue(data)?.ToString();
                    var attribute = prop.GetCustomAttributes(typeof(UniCodeAttribute), true).FirstOrDefault();
                    var errorMessage = (attribute as UniCodeAttribute).ErrorMessage;
                    // nếu có ID =>  là sửa
                    if (recordID != Guid.Empty)
                    {
                        if (data != null)
                        {

                            // Lấy ra id của bản ghi trong db
                            var idData = data.GetType().GetProperty("recordID")?.GetValue(data)?.ToString();
                            if (idData != recordID.ToString())
                            {
                                return new ServiceResponse
                                {
                                    Success = false,

                                    Data = new ErrorResult
                                    {
                                        ErrorCode = AMISErrorCode.DupicateCode,
                                        DevMsg = Resource.DevMsg_DuplicateCode,
                                        UserMsg = Resource.UserMsg_DuplicateCode,
                                        MoreInfo = Resource.MoreInfo_Exception
                                    }
                                };
                            }
                        }
                    }
                    else
                    {
                        if (data != null)
                        {

                            if (codeData == propValue.ToString())
                            {
                                return new ServiceResponse
                                {
                                    Success = false,

                                    Data = new ErrorResult
                                    {
                                        ErrorCode = AMISErrorCode.DupicateCode,
                                        DevMsg = Resource.DevMsg_DuplicateCode,
                                        UserMsg = Resource.UserMsg_DuplicateCode,
                                        MoreInfo = Resource.MoreInfo_Exception
                                    }
                                };
                            }
                        }
                    }

                    // Kiểm tra nếu không phải sửa thì có trùng với mã trong database không

                    // Nếu là sửa thì mã được phép trùng nhưng ID phải giống nhau. ID khác nhau đưa ra cảnh báo
                }

                if (isNotNullOrEmpty == true)
                {
                    var attribute = prop.GetCustomAttributes(typeof(IsNotNullOrEmptyAttribute), true).FirstOrDefault();
                    var errorMessage = (attribute as IsNotNullOrEmptyAttribute).ErrorMessage;
                    if (propValue == null || propValue.ToString().Trim() == "" || propValue.ToString() == Guid.Empty.ToString())
                    {
                        return new ServiceResponse
                        {
                            Success = false,

                            Data = new ErrorResult
                            {
                                ErrorCode = AMISErrorCode.InvalidData,
                                DevMsg = Resource.DevMsg_InvalidData,
                                UserMsg = Resource.UserMsg_IsNullOrEmpty,
                                MoreInfo = errorMessage
                            }
                        };
                    }
                }

                if (isEmail == true)
                {
                    var attribute = prop.GetCustomAttributes(typeof(IsEmailAttribute), true).FirstOrDefault();
                    var errorMessage = (attribute as IsEmailAttribute).ErrorMessage;
                    bool checkEmail = IsValidEmail(propValue?.ToString());
                    if (propValue != null && !checkEmail)
                    {
                        return new ServiceResponse
                        {
                            Success = false,

                            Data = new ErrorResult
                            {
                                ErrorCode = AMISErrorCode.InvalidData,
                                DevMsg = Resource.DevMsg_InvalidData,
                                UserMsg = Resource.UserMsg_InvalidEmail,
                                MoreInfo = errorMessage
                            }
                        };
                    }
                }

             

                if (isRegexFormat == true)
                {
                    //lấy ra attribute
                    var attribute = prop.GetCustomAttributes(typeof(FormatRegexAttribute), true).FirstOrDefault();

                    // lấy ra regex 
                    var regex = new Regex((attribute as FormatRegexAttribute).Format);

                    var errorMessage = (attribute as FormatRegexAttribute).ErrorMessage;

                    if (propValue != null && !regex.IsMatch(propValue.ToString()))
                    {
                        return new ServiceResponse
                        {
                            Success = false,

                            Data = new ErrorResult
                            {
                                ErrorCode = AMISErrorCode.InvalidData,
                                DevMsg = Resource.DevMsg_InvalidData,
                                UserMsg = Resource.UserMsg_InvalidData,
                                MoreInfo = errorMessage
                            }
                        };
                    }
                }
            }
            return new ServiceResponse
            {
                Success = true,     
            };
        }

            /// <summary>
            /// Sửa 1 nhân viên (Đã hoàn thiện)
            /// </summary>
            /// <param name="employee">Đối tượng nhân viên cần sửa</param>
            /// <param name="employeeID">ID của nhân viên muốn sửa</param>
            /// <returns>Số bản ghi bị ảnh hưởng</returns>
            /// Created by: NDTRUNG (23/11/2022)
            public ServiceResponse UpdateEmployee(Employee employee, Guid employeeID)
        {
            //return _employeeDL.UpdateEmployee(employee, employeeID);
            var validateResult = ValidateRequestData(employeeID, employee);
            if (validateResult != null && validateResult.Success)
            {
                //employee.EmployeeID = Guid.NewGuid();
                var employeeId = _employeeDL.UpdateEmployee(employee, employeeID);
                if (employeeId != Guid.Empty)
                {
                    return new ServiceResponse
                    {
                        Success = true,
                        Data = employeeId
                    };
                }
                else
                {
                    return new ServiceResponse
                    {
                        Success = false,

                        Data = new ErrorResult
                        {
                            ErrorCode = AMISErrorCode.InsertFailed,
                            DevMsg = Resource.DevMsg_InsertFailed,
                            UserMsg = Resource.UserMsg_InsertFailed,
                            MoreInfo = Resource.MoreInfo_Exception
                        }
                    };
                }
            }
            else
            {
                return new ServiceResponse
                {
                    Success = false,
                    Data = validateResult?.Data
                };
            }
        }


        /// /// <summary>
        /// Xóa 1 nhân viên (Đã hoàn thiện)
        /// </summary>
        ///  <param name="employeeID">ID nhân viên cần xóa</param>
        /// <returns>ID nhân viên đã xóa</returns>
        /// Created by: NDTRUNG (23/11/2022)
        //public Guid DeleteEmployee(Guid employeeID)
        //{
        //    return _employeeDL.DeleteEmployee(employeeID);
        //}

        /// <summary>
        /// API xuất khẩu excel
        /// </summary>
        /// <returns>File excel</returns>
        public ServiceResponse ExportExcel()
        {

            var result = GetAllRecords();
            if (result == null || result.Count() == 0)
            {
                return new ServiceResponse
                {
                    Success = false,

                    Data = new ErrorResult
                    {
                        ErrorCode = AMISErrorCode.InvalidData,
                        DevMsg = Resource.DevMsg_IsNullOrEmpty,
                        UserMsg = Resource.UserMsg_IsNullOrEmpty
                    }
                };
            }
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(stream))
            {
                // Danh sách cột
                string listColName = "ABCDEFGHI";
                // Title của bảng
                var workSheet = package.Workbook.Worksheets.Add("DANH SÁCH NHÂN VIÊN");

                #region Style cho các ô header
                workSheet.Cells.Style.Font.SetFromFont("Times New Roman", 11);
                workSheet.Cells["A1:I1"].Merge = true;
                workSheet.Cells["A2:I2"].Merge = true;
                workSheet.Cells["A1"].Value = "Danh sách nhân viên";
                workSheet.Cells["A1"].Style.Font.SetFromFont("Arial", 16, true);
                workSheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells["A1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                workSheet.Cells["A3:I3"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells["A3:I3"].Style.Fill.BackgroundColor.SetColor(OfficeOpenXml.Drawing.eThemeSchemeColor.Accent3);
                workSheet.Cells["A3:I3"].Style.Font.SetFromFont("Arial", 10, true);
                workSheet.Cells["A3:I3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                workSheet.Cells["A3:I3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                #endregion

                #region Đặt chiều rộng các cột 
                workSheet.Column(1).Width = 5;
                workSheet.Column(2).Width = 15;
                workSheet.Column(3).Width = 26;
                workSheet.Column(4).Width = 12;
                workSheet.Column(5).Width = 15;
                workSheet.Column(6).Width = 26;
                workSheet.Column(7).Width = 26;
                workSheet.Column(8).Width = 16;
                workSheet.Column(9).Width = 26;
                #endregion

                #region Header các cột
                workSheet.Cells["A3"].Value = "STT";
                workSheet.Cells["B3"].Value = "Mã nhân viên";
                workSheet.Cells["C3"].Value = "Tên Nhân viên";
                workSheet.Cells["D3"].Value = "Giới tính";
                workSheet.Cells["E3"].Value = "Ngày sinh";
                workSheet.Cells["F3"].Value = "Chức Danh";
                workSheet.Cells["G3"].Value = "Tên đơn vị";
                workSheet.Cells["H3"].Value = "Số tài khoản";
                workSheet.Cells["I3"].Value = "Tên ngân hàng";
                workSheet.Cells["A3:I3"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                #endregion

                int rowStart = 3;
                foreach (var text in listColName)
                {
                    workSheet.Cells[$"{text}{rowStart}"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    workSheet.Cells[$"{text}{rowStart}"].Style.WrapText = true;
                }
                foreach (var val in result.Select((value, i) => new { i, value }))
                {
                    for (int col = 1; col <= 9; col++)
                    {
                        workSheet.Cells[val.i + 1 + rowStart, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        workSheet.Cells[val.i + 1 + rowStart, col].Style.WrapText = true;
                        if (col == 5)
                        {
                            workSheet.Cells[val.i + 1 + rowStart, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        }
                    }

                    workSheet.Cells[val.i + 1 + rowStart, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    workSheet.Cells[val.i + 1 + rowStart, 1].Value = val.i + 1;
                    workSheet.Cells[val.i + 1 + rowStart, 2].Value = val.value.EmployeeCode.ToString();
                    workSheet.Cells[val.i + 1 + rowStart, 3].Value = val.value.EmployeeName.ToString();
                    workSheet.Cells[val.i + 1 + rowStart, 4].Value = val.value.Gender == Gender.Male ? Resource.Display_Excel_Male : val.value.Gender == Gender.Female ? Resource.Display_Excel_FeMale : Resource.Display_excel_Other;
                    workSheet.Cells[val.i + 1 + rowStart, 5].Value = val.value.DateOfBirth?.Year.ToString() == "0001" ? "" : val.value.DateOfBirth?.ToString("dd/MM/yyyy");
                    workSheet.Cells[val.i + 1 + rowStart, 6].Value = val.value.PostionName == null ? "" : val.value.PostionName.ToString();
                    workSheet.Cells[val.i + 1 + rowStart, 7].Value = val.value.DepartmentName == null ? "" : val.value.DepartmentName.ToString();
                    workSheet.Cells[val.i + 1 + rowStart, 8].Value = val.value.BankAccountNumber == null ? "" : val.value.BankAccountNumber.ToString();
                    workSheet.Cells[val.i + 1 + rowStart, 9].Value = val.value.BankName == null ? "" : val.value.BankName.ToString();

                }
                package.SaveAs(stream);
            }
            stream.Position = 0;
            return new ServiceResponse
            {
                Success = true,

                Data = stream
            };
        }

        /// <summary>
        /// kiểm tra trùng mã
        /// </summary>
        /// <param name="employeeCode">mã nhân viên</param>
        /// <returns>true, recordID và recordCode</returns>
        /// Author: NDTRung(11/12/2022)
        public ServiceResponse CheckDuplicate(string employeeCode)
        {
            return _employeeDL.CheckDuplicate(employeeCode);
        }
        /// <summary>
        /// validate Email
        /// </summary>
        /// <param name="email">giá trị email truyền vào</param>
        /// <returns>true nếu là email, false nếu không phải email</returns>
        private static bool IsValidEmail(string email)
        {
            if (email == null) return false;
            if (email.Trim().EndsWith("."))
            {
                return false;
            }
            try
            {
                MailAddress mail = new MailAddress(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
                throw;
            }
        }

        /// <summary>
        /// validate ngày sinh
        /// </summary>
        /// <param name="date">Ngày sinh</param>
        /// <returns>true: nếu nhỏ hơn ngày hiện tại và tuổi bé hơn 200 ngược lại false </returns>
        public static bool IsValidDate(string date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                if (dt > DateTime.Now)
                {
                    return false;
                }
                else if (dt.Year < 1900)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// So sánh 2 mã
        /// </summary>
        /// <param name="oldCode"></param>
        /// <param name="newCode"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool CompareCode(string oldCode, string newCode, int index)
        {
            int oldCodeSub = int.Parse(oldCode.Substring(index));
            int newCodeSub = int.Parse(newCode.Substring(index));
            return oldCodeSub > newCodeSub;
        }
        #endregion
    }
}
