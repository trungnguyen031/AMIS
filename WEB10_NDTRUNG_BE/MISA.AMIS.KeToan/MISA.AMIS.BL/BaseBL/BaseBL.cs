using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Enums;
using MISA.AMIS.Common.Resources;
using MISA.AMIS.DL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.BL
{
    public class BaseBL<T> : IBaseBL<T>
    {
        #region Field
        private IBaseDL<T> _baseDL;
        #endregion

        #region Constructor
        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }
        #endregion

        #region Method
        /// <summary>
        /// Lấy danh sách toàn bộ bản ghi (Đã hoàn thiện)
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Created by: NDTRUNG (04/12/2022)
        public IEnumerable<T> GetAllRecords()
        {
            return _baseDL.GetAllRecords();
        }

        /// <summary>
        /// Lấy 1 bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID bản ghi muốn lấy</param>
        /// <returns>Đối tượng muốn lấy</returns>
        /// Created by: NDTRUNG (04/12/2022)
        public T GetRecordByID(Guid recordID)
        {
            return _baseDL.GetRecordByID(recordID);
        }

        /// <summary>
        /// Xóa 1 nhân viên (Đã hoàn thiện)
        /// </summary>
        /// <param name="recordID">ID của đối tượng muốn xóa</param>
        /// <returns>ID của bản ghi muốn xóa</returns>
        /// Created by: NDTRUNG (04/12/2022)
        public T DeleteRecord(Guid recordID)
        {
            return _baseDL.DeleteRecord(recordID);
        }

        /// /// <summary>
        /// Thêm mới 1 Bản ghi (Đã hoàn thiện)
        /// </summary>
        ///  <param name="record">Đối tượng Bản ghi cần thêm mới</param>
        /// <returns>ID của Bản ghi vừa thêm. Nếu insert thất bại thì return về Guid rỗng</returns>
        /// Created by: NDTRUNG (04/12/2022)
        public ServiceResponse InsertRecord(T record)
        {
            var validateResult = ValidateRequestData(record);
            if (validateResult != null && validateResult.Success)
            {
                //employee.EmployeeID = Guid.NewGuid();
                var newRecordID = _baseDL.InsertRecord(record);
                if (newRecordID != Guid.Empty)
                {
                    return new ServiceResponse
                    {
                        Success = true,
                        Data = newRecordID
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
        /// <param name="employee">Đối tượng Bản ghi cần validate</param>
        /// /// <param name="httpContext">HttpContext truyền vào từ request API</param>
        /// <returns>Đối tượng ServiceResponse mô tả validate thành công hay thất bại</returns>
        /// Created by: NDTRUNG (04/12/2022)
        private ServiceResponse ValidateRequestData(T record)
        {
            //Lấy hết thuộc tính của class Bản ghi
            var properties = typeof(T).GetProperties();
            // danh sách các lỗi Validate
            var validateFailures = new List<string>();

            //Kiểm tra xem property nào có attribute là Reuqired
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(record);

                // Lấy danh sách những Attritbutes điền ở trên Properties
                // K liên quan gì đến kế thừa nên để là false
                var requiredAttribute = (RequiredAttribute)property.GetCustomAttributes(typeof(RequiredAttribute), false).FirstOrDefault();
                // Kiểm tra xem property đó có attribute Required không và giá trị FE truyền lên có null hoặc empty không
                if (requiredAttribute != null && string.IsNullOrEmpty(propertyValue?.ToString()))
                {
                    validateFailures.Add(requiredAttribute.ErrorMessage);
                }
            }
            //Kiểm tra xem có gặp lỗi Validate không:
            if (validateFailures.Count > 0)

                return new ServiceResponse
                {
                    Success = false,

                    Data = new ErrorResult
                    {
                        ErrorCode = AMISErrorCode.InvalidData,
                        DevMsg = Resource.DevMsg_IsNullOrEmpty,
                        UserMsg = Resource.UserMsg_IsNullOrEmpty,
                        MoreInfo = validateFailures,
                    }
                };

            return new ServiceResponse
            {
                Success = true
            };

            //var keyAttribute = (KeyAttribute)property.GetCustomAttributes(typeof(KeyAttribute), false).FirstOrDefault();
            //// Có value:
            //if (requiredAttribute != null && string.IsNullOrEmpty(propertyValue.ToString()))
            //{

            //}
        }
        /// <summary>
        /// Sửa 1 nhân viên (Đã hoàn thiện)
        /// </summary>
        /// <param name="record">Đối tượng Bản ghi cần sửa</param>
        /// <param name="recordID">ID của Bản ghi muốn sửa</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: NDTRUNG (23/11/2022)
        public ServiceResponse UpdateRecord(Guid recordID, T record)
        {
            //return _employeeDL.UpdateEmployee(employee, employeeID);
            var validateResult = ValidateRequestData(record);
            if (validateResult != null && validateResult.Success)
            {
                //employee.EmployeeID = Guid.NewGuid();
                var result = _baseDL.UpdateRecord(recordID, record);
                if (result > 0)
                {
                    return new ServiceResponse
                    {
                        Success = true,
                        Data = result
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
        #endregion
    }
}
