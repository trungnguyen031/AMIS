using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Entities.DTO;
using MISA.AMIS.KeToan.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.DL
{
    //IEmployeeDL kế thừa  IBaseDL<Employee>
    public interface IEmployeeDL : IBaseDL<Employee>
    {
        //public IEnumerable<Employee> GetAllEmployees();


        /// /// <summary>
        /// Lấy danh nhân viên theo ID (Đã hoàn thiện)
        /// </summary>
        ///  <param name="employeeID">ID nhân viên</param>
        /// <returns>Thông tin chi tiết 1 nhân viên</returns>
        /// Created by: NDTRUNG (23/11/2022)
       // public Employee GetEmployeeByID(Guid employeeID);

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
        public PagingResult<Employee> FilterEmployees(string? keyword, int pageSize = 10, int pageNumber = 1);

        /// <summary>
        /// Lấy mã nhân viên mới tự động tăng
        /// </summary>
        /// <returns>Mã nhân viên mới tự động tăng</returns>
        /// Created by: NDTRUNG (23/11/2022)
        public string GetNewEmployeeCode();

        /// /// <summary>
        /// Thêm mới 1 nhân viên (Đã hoàn thiện)
        /// </summary>
        ///  <param name="employee">Đối tượng Nhân viên cần thêm mới</param>
        /// <returns>ID của nhân viên vừa thêm. Nếu insert thất bại thì return về Guid rỗng</returns>
        /// Created by: NDTRUNG (23/11/2022)
        public Guid InsertEmployee(Employee employee);

        /// <summary>
        /// Sửa 1 nhân viên (Đã hoàn thiện)
        /// </summary>
        /// <param name="employee">Đối tượng nhân viên cần sửa</param>
        /// <param name="employeeID">ID của nhân viên muốn sửa</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: NDTRUNG (23/11/2022)
        public Guid UpdateEmployee(Employee employee, Guid employeeID);

        /// <summary>
        /// Xóa 1 nhân viên (Đã hoàn thiện)
        /// </summary>
        /// <param name="employee">Đối tượng nhân viên cần xóa</param>
        /// <param name="employeeID">ID của nhân viên muốn xóa</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: NDTRUNG (23/11/2022)
        //public Guid DeleteEmployee(Guid employeeID);

        /// <summary>
        /// Xóa nhiều nhân viên
        /// </summary>
        /// <param name="listEmpID">Danh sách iD nhân viên cần xóa</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: NDTRUNG (07/12/2022)
        public int DeleteMultipleEmployee(ListEmployeeID listEmployeeID);

        /// <summary>
        /// kiểm tra trùng mã
        /// </summary>
        /// <param name="employeeCode">mã nhân viên</param>
        /// <returns>true, recordID và recordCode</returns>
        /// Author: NDTRung(11/12/2022)
        public ServiceResponse CheckDuplicate(string employeeCode);
    }
}
