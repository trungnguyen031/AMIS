namespace MISA.AMIS.KeToan.Common.Entities
{
    /// <summary>
    /// Dữ liệu trà về khi phân trang
    /// </summary>
    /// <typeparam name="T">Kiểu dữ liệu của đối tượng trong mảng trả về</typeparam>
    public class PagingResult<T>
    {   
        /// <summary>
        /// Tổng số bản ghi thỏa mãn điều kiện
        /// </summary>
        public long TotalRecord { get; set; }


        /// <summary>
        /// Mảng đối tượng thỏa mãn điều kiện lọc và phân trang
        /// </summary>
        public List<T> Data { get; set; } = new List<T>();
    }
}
