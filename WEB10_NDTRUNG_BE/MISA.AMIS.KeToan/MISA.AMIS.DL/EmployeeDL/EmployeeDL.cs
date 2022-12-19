using Dapper;
using MISA.AMIS.Common.Constants;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Entities.DTO;
using MISA.AMIS.KeToan.Common.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.DL
{
    //EmployeeDL kế thừa BaseDL, implement IEmployeeDL
    public class EmployeeDL : BaseDL<Employee>, IEmployeeDL
    {
        ///// <summary>
        ///// Lấy danh sách tất cả nhân viên (Đã hoàn thiện)
        ///// </summary>
        ///// <returns>Danh sách tất cả nhân viên</returns>
        ///// Created by: NDTRUNG (10/11/2022)
        //public IEnumerable<Employee> GetAllEmployees()
        //{
        //    //Khởi tạo kết nối tới Database MySql
        //    string connectionString = "Server=localhost;Port=3306;Database=misa.web10.nsdh.ndtrung;uid=root;Pwd=12345678;";
        //    var mySqlConnection = new MySqlConnection(connectionString);

        //    //Chuẩn bị tên stored procedure
        //    string storedProcedureName = "Proc_employee_GetAll";

        //    //Thực hiện gọi vào DB
        //    var employees = mySqlConnection.Query<Employee>(storedProcedureName, commandType: System.Data.CommandType.StoredProcedure);

        //    return employees;
        //}

        /// /// <summary>
        /// Lấy danh nhân viên theo ID (Đã hoàn thiện)
        /// </summary>
        ///  <param name="employeeID">ID nhân viên</param>
        /// <returns>Thông tin chi tiết 1 nhân viên</returns>
        /// Created by: NDTRUNG (23/11/2022)
        //public Employee GetEmployeeByID(Guid employeeID)
        //{
        //    //Chuẩn bị tên stored procedure
        //    string storedProcedureName = "Proc_employee_GetByID";

        //    // Gán tham số đầu vào
        //    var parameters = new DynamicParameters();
        //    parameters.Add("$employeeID", employeeID);

        //    //Khởi tạo kết nối tới Database MySql
        //    string connectionString = "Server=localhost;Port=3306;Database=misa.web10.nsdh.ndtrung;uid=root;Pwd=12345678;";
        //    var mySqlConnection = new MySqlConnection(connectionString);

        //    //Gọi vào Database 
        //    var employee = mySqlConnection.QueryFirstOrDefault<Employee>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

        //    return employee;
        //}

        /// <summary>
        /// Lấy mã nhân viên mới tự động tăng
        /// </summary>
        /// <returns>Mã nhân viên mới tự động tăng</returns>
        /// Created by: NDTRUNG (23/11/2022)
        public string GetNewEmployeeCode()
        {
            //Khởi tạo kết nối tới Database MySql
            var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

            // Chuẩn bị tên stored procedure
            string storedProcedureName = "Proc_employee_GetMaxCode";

            // Thực hiện gọi vào DB để chạy stored procedure ở trên
            string maxEmployeeCode = mySqlConnection.QueryFirstOrDefault<string>(storedProcedureName, commandType: System.Data.CommandType.StoredProcedure);

            // Xử lý sinh mã nhân viên mới tự động tăng
            // Cắt chuỗi mã nhân viên lớn nhất trong hệ thống để lấy phần số
            // Mã nhân viên mới = "NV" + Giá trị cắt chuỗi ở  trên + 1
            string newEmployeeCode = "NV" + (Int64.Parse(maxEmployeeCode.Substring(2)) + 1).ToString();

            return newEmployeeCode;
        }

        /// <summary>
        /// Lấy danh sách nhân viên cho phép phân trang và tìm kiếm
        /// </summary>
        /// <param keyword="keyword">Từ khóa muốn tìm kiếm</param>
        /// <param name="pageSize">Số trang muốn lấy</param>
        /// <param name="pageNumber">Thứ tự trang muốn lấy</param>
        /// <returns>Một đối tượng gồm:
        /// + Danh sách nhân viên thỏa mãn điều kiện lọc và phân trang
        /// + Tổng số nhân viên thỏa mãn điều kiện</returns>
        /// Created by: NDTRUNG (24/11/2022)
        public PagingResult<Employee> FilterEmployees(string? keyword, int pageSize = 10, int pageNumber = 1)
        {
            //Khởi tạo kết nối tới Database MySql
            var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

            // Chuẩn bị tên Stored procedure
            string storedProcedureName = "Proc_employee_GetPagging";

            // Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();
            // Thứ tự của bản ghi bắt đầu lấy = số trang - 1 * tổng số bản ghi trên 1 trang
            parameters.Add("@v_Offset", (pageNumber - 1) * pageSize);
            // Số bản ghi trên 1 trang
            parameters.Add("@v_Limit", pageSize);
            parameters.Add("@v_Sort", "ModifiedDate DESC");
            // Chuẩn bị điều kiện WHERE
            // WHERE (EmployeeCode LIKE '%{keyword}%' OR EmployeeName LIKE '%{keyword}%'
            var orConditions = new List<string>();
            // var andConditions = new List<string>();
            string whereClause = "";

            if (keyword != null)
            {
                orConditions.Add($"EmployeeCode LIKE '%{keyword}%'");
                orConditions.Add($"EmployeeName LIKE '%{keyword}%'");
            }
            if (orConditions.Count > 0)
            {
                whereClause = $"({string.Join(" OR ", orConditions)})";
            }

            //if (andConditions.Count > 0)
            //{
            //    whereClause += $" AND {string.Join(" AND ", andConditions)}";
            //}

            parameters.Add("@v_Where", whereClause);

            // Thực hiện gọi vào DB để chạy stored procedure với tham số đầu vào ở trên
            var multipleResults = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);


            var employees = multipleResults.Read<Employee>().ToList();
            var totalCount = multipleResults.Read<long>().Single();
            return new PagingResult<Employee>()
            {
                //Danh sách nhân viên
                Data = employees,
                //Tổng số bản ghi
                TotalRecord = totalCount
            };

        }

        /// /// <summary>
        /// Thêm mới 1 nhân viên (Đã hoàn thiện)
        /// </summary>
        ///  <param name="employee">Đối tượng Nhân viên cần thêm mới</param>
        /// <returns>ID của nhân viên vừa thêm. Nếu insert thất bại thì return về Guid rỗng</returns>
        /// Created by: NDTRUNG (23/11/2022)
        public Guid InsertEmployee(Employee employee)
        {
            // Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();

            // Chuẩn bị tham số đầu vào cho stored procedure
            var newEmployeeID = Guid.NewGuid();
            parameters.Add("@EmployeeId", newEmployeeID);
            parameters.Add("@EmployeeCode", employee.EmployeeCode);
            parameters.Add("@EmployeeName", employee.EmployeeName);
            parameters.Add("@dateOfBirth", employee.DateOfBirth);
            parameters.Add("@gender", employee.Gender);
            parameters.Add("@identityNumber", employee.IdentityNumber);
            parameters.Add("@identityIssuedDate", employee.IdentityIssuedDate);
            parameters.Add("@identityIssuedPlace", employee.IdentityIssuedPlace);
            parameters.Add("@positionName", employee.PostionName);
            parameters.Add("@departmentID", employee.DepartmentID);
            parameters.Add("@phonenumber", employee.PhoneNumber);
            parameters.Add("@telephonenumber", employee.TelephoneNumber);
            parameters.Add("@email", employee.Email);
            parameters.Add("@bankaccountnumber", employee.BankAccountNumber);
            parameters.Add("@bankname", employee.BankName);
            parameters.Add("@bankbranchname", employee.BankBranchName);
            parameters.Add("@address", employee.Address);
            parameters.Add("@createddate", employee.CreatedDate);
            parameters.Add("@createdby", employee.CreatedBy);
            parameters.Add("@modifieddate", employee.ModifiedDate);
            parameters.Add("@modifiedby", employee.ModifiedBy);

            //Chuẩn bị tên stored procedure
            string storedProcedureName = "Proc_employee_Insert";

            //Khởi tạo kết nối tới Database MySql
            var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

            //Thực hiện gọi vào DB
            var numberOfAffectedRows = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            //Xử lý kết quả trả về
            if (numberOfAffectedRows > 0)
            {
                return newEmployeeID;
            }
            else
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        /// Sửa 1 nhân viên (Đã hoàn thiện)
        /// </summary>
        /// <param name="employee">Đối tượng nhân viên cần sửa</param>
        /// <param name="employeeID">ID của nhân viên muốn sửa</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: NDTRUNG (23/11/2022)
        public Guid UpdateEmployee(Employee employee, Guid employeeID)
        {

            //Khởi tạo kết nối tới Database MySql
            var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

            //Chuẩn bị tên stored procedure
            string storedProcedureName = "Proc_employee_Update";


            //Gán tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("$EmployeeID", employeeID);
            parameters.Add("@EmployeeCode", employee.EmployeeCode);
            parameters.Add("@EmployeeName", employee.EmployeeName);
            parameters.Add("@dob", employee.DateOfBirth);
            parameters.Add("@gender", employee.Gender);
            parameters.Add("@identityNumber", employee.IdentityNumber);
            parameters.Add("@identityIssuedDate", employee.IdentityIssuedDate);
            parameters.Add("@identityIssuedPlace", employee.IdentityIssuedPlace);
            parameters.Add("@positionName", employee.PostionName);
            parameters.Add("@departmentID", employee.DepartmentID);
            parameters.Add("@phonenumber", employee.PhoneNumber);
            parameters.Add("@telephonenumber", employee.TelephoneNumber);
            parameters.Add("@email", employee.Email);
            parameters.Add("@bankaccountnumber", employee.BankAccountNumber);
            parameters.Add("@bankname", employee.BankName);
            parameters.Add("@bankbranchname", employee.BankBranchName);
            parameters.Add("@address", employee.Address);
            parameters.Add("@createddate", employee.CreatedDate);
            parameters.Add("@createdby", employee.CreatedBy);
            parameters.Add("@modifieddate", employee.ModifiedDate);
            parameters.Add("@modifiedby", employee.ModifiedBy);

            //Thực hiện gọi vào DB
            var numberOfAffectedRows = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            //Xử lý kết quả trả về
            if (numberOfAffectedRows > 0)
            {
                return employeeID;
            }
            else
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        /// Xóa nhiều nhân viÊN
        /// </summary>
        /// <param name="listEmpID">Danh sách iD nhân viên cần xóa</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: NDTRUNG (07/12/2022)
        public int DeleteMultipleEmployee(ListEmployeeID listEmployeeID)
        {
            var listRemove = "";
            // Khời tạo kết nối tới DB MySQL
            var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);
            MySqlTransaction transaction = null;

            var storedProcedureName = "Proc_employee_DeleteMultiple";

            var ids = listEmployeeID.EmployeeIDs.Select(x => $"'{x}'").ToList();
            listRemove = string.Join(",", ids);



            //Chuẩn bị câu lệnh SQL
            var parammeters = new DynamicParameters();
            parammeters.Add("listId", listRemove);
            parammeters.Add("countRows", dbType: DbType.Int32, direction: ParameterDirection.Output);
            //string sql = $" DELETE FROM employee WHERE EmployeeID IN ('{str}');";

            mySqlConnection.Open();
            int numberOfRowsAffected = 0;
            try
            {
                transaction = mySqlConnection.BeginTransaction();
                //Thực hiện gọi vào DB
                numberOfRowsAffected = mySqlConnection.Execute(storedProcedureName, parammeters, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);
                int countRows = parammeters.Get<int>("countRows");

                if (countRows == listEmployeeID.EmployeeIDs.Count)
                {
                    transaction.Commit();
                    return countRows;
                }
                else
                {
                    transaction.Rollback();
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            finally
            {
                mySqlConnection.Close();
            }
            return numberOfRowsAffected;
        }

        /// <summary>
        /// kiểm tra trùng mã
        /// </summary>
        /// <param name="employeeCode">mã nhân viên</param>
        /// <returns>true, recordID và recordCode</returns>
        /// Author: NDTRung(11/12/2022)
        public ServiceResponse CheckDuplicate(string employeeCode)
        {
            //lấy chuỗi kết nối
            var connectionString = DatabaseContext.ConnectionString;

            //lấy tên Procedure 
            string sqlCommand = "Proc_employee_FindByCode";

            var parameters = new DynamicParameters();
            parameters.Add("recordCode", employeeCode);

            parameters.Add("empID", dbType: DbType.String, direction: ParameterDirection.InputOutput);
            parameters.Add("empCode", dbType: DbType.String, direction: ParameterDirection.InputOutput);

            using (var mySqlConnection = new MySqlConnection(connectionString))
            {
                var multipleResults = mySqlConnection.Query(sqlCommand, parameters, commandType: CommandType.StoredProcedure);
                Guid? recordID = parameters.Get<Guid?>("empID");
                string? recordCode = parameters.Get<string?>("empCode");
                return new ServiceResponse
                {
                    Success = true,
                    Data = new
                    {
                        recordID = recordID,
                        recordCode = recordCode
                    }
                };
            }
        }
    }
}

