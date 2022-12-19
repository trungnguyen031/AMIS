using MISA.AMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.DL
{
    public interface IBaseDL<T>
    {
        #region Method
        /// /// <summary>
        /// Lấy bản ghi theo ID (Đã hoàn thiện)
        /// </summary>
        ///  <param name="recordID">ID của bản ghi muốn lấy</param>
        /// <returns>Đối tượng</returns>
        /// Created by: NDTRUNG (03/12/2022)
        public T GetRecordByID(Guid recordID);

        /// <summary>
        /// Xóa 1 nhân viên (Đã hoàn thiện)
        /// </summary>
        /// <param name="record">Đối tượng cần xóa</param>
        /// <param name="recordID">ID của đối tượng muốn xóa</param>
        /// <returns>ID của bản ghi muốn xóa</returns>
        /// Created by: NDTRUNG (23/11/2022)
        public T DeleteRecord(Guid recordID);

        /// <summary>
        /// Lấy danh sách toàn bộ bản ghi (Đã hoàn thiện)
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Created by: NDTRUNG (10/11/2022)
        public IEnumerable<T> GetAllRecords();

        /// <summary>
        /// Thêm 1 bản ghi
        /// </summary>
        /// <param name="record">Thông tin của bản ghi</param>
        /// <returns>bản ghi vừa thêm nếu thành công</returns>
        /// Created : NDTRUNG(08/12/2022)
        public Guid InsertRecord(T record);

        /// <summary>
        /// Sửa 1 bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID của bản ghi cần sửa</param>
        /// <param name="record">Thông tin của bản ghi</param>
        /// <returns>Thông tin bản ghi sau khi chỉnh sửa thành công</returns>
        /// Created : NDTRUNG(08/12/2022)
        public int UpdateRecord(Guid recordID, T record);

        #endregion
    }
}
