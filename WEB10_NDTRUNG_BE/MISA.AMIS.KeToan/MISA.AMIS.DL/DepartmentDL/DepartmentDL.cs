using Dapper;
using MISA.AMIS.KeToan.Common.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.DL
{
    public class DepartmentDL : BaseDL<Department>, IDepartmentDL
    {
            //public IEnumerable<Department> GetAllDepartments()
            //{

            //    //Khởi tạo kết nối tới Database MySql
            //    string connectionString = "Server=localhost;Port=3306;Database=misa.web10.nsdh.ndtrung;uid=root;Pwd=12345678;";
            //    var mySqlConnection = new MySqlConnection(connectionString);

            //    //Chuẩn bị tên stored procedure
            //    string storedProcedureName = "Proc_department_GetAll";

            //    //Thực hiện gọi vào DB
            //    var departments = mySqlConnection.Query<Department>(storedProcedureName, commandType: System.Data.CommandType.StoredProcedure);

            //    return departments;
            //}             
    }
}
