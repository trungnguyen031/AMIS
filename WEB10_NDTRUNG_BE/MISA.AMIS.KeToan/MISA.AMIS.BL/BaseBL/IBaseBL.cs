using MISA.AMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.BL
{
    public interface IBaseBL<T>
    {
        #region Method

        /// <summary>
        /// Lấy danh sách toàn bộ bản ghi (Đã hoàn thiện)
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Created by: NDTRUNG (04/12/2022)
        public IEnumerable<T> GetAllRecords();

        /// /// <summary>
        /// Lấy bản ghi theo ID (Đã hoàn thiện)
        /// </summary>
        ///  <param name="recordID">ID của bản ghi muốn lấy</param>
        /// <returns>Đối tượng</returns>
        /// Created by: NDTRUNG (03/12/2022)
        public T GetRecordByID(Guid recordID);

        /// <summary>
        /// Thêm 1 bản ghi
        /// </summary>
        /// <param name="record">Thông tin của bản ghi</param>
        /// <returns>bản ghi vừa thêm nếu thành công</returns>
        /// Created : NDTRUNG(08/12/2022)
        public ServiceResponse InsertRecord(T record);

        /// <summary>
        /// Sửa 1 bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID của bản ghi cần sửa</param>
        /// <param name="record">Thông tin của bản ghi</param>
        /// <returns>Thông tin bản ghi sau khi chỉnh sửa thành công</returns>
        /// Created : NDTRUNG(08/12/2022)
        public ServiceResponse UpdateRecord(Guid recordID, T record);

        /// <summary>
        /// Xóa 1 bản ghi (Đã hoàn thiện)
        /// </summary>
        /// <param name="recordID">ID của đối tượng muốn xóa</param>
        /// <returns>Đối tượng cần xóa</returns>
        /// Created by: NDTRUNG (04/12/2022)
        public T DeleteRecord(Guid recordID);

        #endregion
    }
}
