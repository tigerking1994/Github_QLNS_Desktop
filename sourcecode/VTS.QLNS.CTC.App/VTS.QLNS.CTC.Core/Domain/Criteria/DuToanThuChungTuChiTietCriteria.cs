using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Criteria
{
    public class DuToanThuChungTuChiTietCriteria
    {
        public string VoucherIds { get; set; }
        public Guid VoucherId { get; set; }
        public string ChungTuId { get; set; }
        public string LNS { get; set; }
        public int? YearOfWork { get; set; }
        public int? YearOfBudget { get; set; }
        public int? BudgetSource { get; set; }
        public DateTime? VoucherDate { get; set; }
        public DateTime? NgayQuyetDinh { get; set; }
        public string IdDonVi { get; set; }
        public int ILoai { get; set; }
        public int Level { get; set; }
        public int Status { get; set; }
        public string IdDotNhan { get; set; }
        public string SoChungTu { get; set; }
        public string IdNganh { get; set; }
        public string NgayChungTu { get; set; }
        public int IThangQuy { get; set; }
        public int IThangQuyLoai { get; set; }
        public int LoaiChungTu { get; set; }
        public bool? IDuLieuNhan { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserName { get; set; }
        public bool IsNamLuyKe { get; set; }
        public int LoaiDuKien { get; set; }
        public int VoucherIndex { get; set; }
        public bool IsLuyKe { get; set; }
        public int dvt { get; set; }
        public bool IsGetAll { get; set; }
    }
}
