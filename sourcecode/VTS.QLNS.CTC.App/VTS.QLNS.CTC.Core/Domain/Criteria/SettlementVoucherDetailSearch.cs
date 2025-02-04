using System;

namespace VTS.QLNS.CTC.Core.Domain.Criteria
{
    /// <summary>
    /// thông tin để tìm kiếm chứng từ chi tiết
    /// </summary>
    public class SettlementVoucherDetailSearch
    {
        /// <summary>
        /// Chứng từ Id
        /// </summary>
        public Guid VoucherId { get; set; }

        public string VoucherName { get; set; }

        /// <summary>
        /// danh sách LNS (theo chuỗi, cách nhau bằng dấu ','
        /// </summary>
        public string LNS { get; set; }

        /// <summary>
        /// năm làm việc
        /// </summary>
        public int YearOfWork { get; set; }

        /// <summary>
        /// năm quyết toán
        /// </summary>
        public int YearOfBudget { get; set; }

        /// <summary>
        /// Loại chứng từ
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// nguồn ngân sách
        /// </summary>
        public int BudgetSource { get; set; }

        /// <summary>
        /// id đơn vị
        /// </summary>
        public string AgencyId { get; set; }

        /// <summary>
        /// ngày chứng từ
        /// </summary>
        public DateTime VoucherDate { get; set; }

        public string QuarterMonth { get; set; }

        public int QuarterMonthType { get; set; }
        public string QuarterMonthBefore { get; set; }

        public string UserName { get; set; }

        public int Dvt { get; set; }
    }
}
