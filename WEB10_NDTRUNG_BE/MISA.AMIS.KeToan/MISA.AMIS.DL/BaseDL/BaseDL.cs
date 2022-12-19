using Dapper;
using MISA.AMIS.Common.Constants;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.DL
{
    public class BaseDL<T> : IBaseDL<T>
    {
        /// <summary>
        /// Lấy danh sách toàn bộ bản ghi (Đã hoàn thiện)
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Created by: NDTRUNG (04/12/2022)
        public IEnumerable<T> GetAllRecords()
        {
            //Chuẩn bị tên stored procedure
            string storedProcedureName = String.Format(ProcedureName.PROC_GETALL, typeof(T).Name);

            //Thực hiện gọi vào DB
            var records = new List<T>();
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                records = (List<T>)mySqlConnection.Query<T>(storedProcedureName, commandType: System.Data.CommandType.StoredProcedure);
            }
            return records;
        }

        /// <summary>
        /// Xóa 1 bản ghi (Đã hoàn thiện)
        /// </summary>
        /// <param name="record">Đối tượng cần xóa</param>
        /// <param name="recordID">ID của đối tượng muốn xóa</param>
        /// <returns>Đối tượng cần xóa</returns>
        /// Created by: NDTRUNG (04/12/2022)
        public T DeleteRecord(Guid recordID)
        {
            //Chuẩn bị tên stored procedure
            string storedProcedureName = String.Format(ProcedureName.PROC_DELETE, typeof(T).Name);

            // Gán tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add($"${typeof(T).Name}ID", recordID);

            //Khởi tạo kết nối tới Database MySql
            T record;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                //Gọi vào Database 
                record = mySqlConnection.QueryFirstOrDefault<T>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
            return record;
        }

        /// /// <summary>
        /// Lấy bản ghi theo ID (Đã hoàn thiện)
        /// </summary>
        ///  <param name="recordID">ID của bản ghi muốn lấy</param>
        /// <returns>Đối tượng</returns>
        /// Created by: NDTRUNG (03/12/2022)
        public T GetRecordByID(Guid recordID)
        {
            //Chuẩn bị tên stored procedure
            string storedProcedureName = String.Format(ProcedureName.PROC_GET_BY_ID, typeof(T).Name);

            // Gán tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add($"${typeof(T).Name}ID", recordID);

            //Khởi tạo kết nối tới Database MySql
            T record;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                //Gọi vào Database 
                record = mySqlConnection.QueryFirstOrDefault<T>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
            return record;
        }


        /// <summary>
        /// Thêm 1 bản ghi
        /// </summary>
        /// <param name="record">Thông tin của bản ghi</param>
        /// <returns>bản ghi vừa thêm nếu thành công</returns>
        /// Created : NDTRUNG(08/12/2022)
        public Guid InsertRecord(T record)
        {
            //lấy chuỗi kết nối
            var connectionString = DatabaseContext.ConnectionString;

            //lấy tên Procedure 
            string sqlCommand = String.Format(ProcedureName.PROC_INSERT, typeof(T).Name);

            //Khai báo tham số cho procedure
            var parameters = new DynamicParameters();

            //lấy ra mảng các thuộc tính của Generic
            var properties = record.GetType().GetProperties();

            // Khai báo giá trị của thuộc tính
            object propValue;

            //Tạo Guid cho ID
            var newRecordID = Guid.NewGuid();

            //Lặp qua mảng các thuộc  tính
            foreach (var prop in properties)
            {
                // Kiểm tra xem có phải ID không
                var primaryKeyAttribute = KeyAttribute.GetCustomAttribute(prop, typeof(KeyAttribute));

                if (primaryKeyAttribute != null)
                {
                    propValue = newRecordID;
                }
                else
                {
                    propValue = prop.GetValue(record);
                }
                parameters.Add($"@{prop.Name}", propValue);
            }
            using (var mySqlConnection = new MySqlConnection(connectionString))
            {
                {
                    mySqlConnection.Open();
                    var transaction = mySqlConnection.BeginTransaction();
                    var result = mySqlConnection.Execute(sqlCommand, parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);
                    if (result > 0)
                    {
                        transaction.Commit();
                        mySqlConnection.Close();
                        return newRecordID;
                    }
                    else
                    {
                        transaction.Rollback();
                        mySqlConnection.Close();
                        return Guid.Empty;
                    }
                }
            }
        }

        /// <summary>
        /// Sửa 1 bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID của bản ghi cần sửa</param>
        /// <param name="record">Thông tin của bản ghi</param>
        /// <returns>Thông tin bản ghi sau khi chỉnh sửa thành công</returns>
        /// Created : NDTRUNG(08/12/2022)
        public int UpdateRecord(Guid recordID, T record)
        {
            //lấy chuỗi kết nối
            var connectionString = DatabaseContext.ConnectionString;

            //lấy tên Procedure 
            string sqlCommand = String.Format(ProcedureName.PROC_INSERT, typeof(T).Name);

            //Khai báo tham số cho procedure
            var parameters = new DynamicParameters();

            //lấy ra mảng các thuộc tính của Generic
            var properties = record.GetType().GetProperties();

            object propValue;

            foreach (var prop in properties)
            {
                // Kiểm tra xem có phải ID không
                var primaryKeyAttribute = KeyAttribute.GetCustomAttribute(prop, typeof(KeyAttribute));

                if (primaryKeyAttribute != null)
                {
                    propValue = recordID;
                }
                else
                {
                    propValue = prop.GetValue(record);
                }
                parameters.Add($"@{prop.Name}", propValue);
            }

            using (var mySqlConnection = new MySqlConnection(connectionString))
            {
                mySqlConnection.Open();
                var transaction = mySqlConnection.BeginTransaction();
                var result = mySqlConnection.Execute(sqlCommand, parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);
                if (result > 0)
                {
                    transaction.Commit();
                    mySqlConnection.Close();
                    return result;
                }
                else
                {
                    transaction.Rollback();
                    mySqlConnection.Close();
                    return result;
                }
            }
        }
    }
}
