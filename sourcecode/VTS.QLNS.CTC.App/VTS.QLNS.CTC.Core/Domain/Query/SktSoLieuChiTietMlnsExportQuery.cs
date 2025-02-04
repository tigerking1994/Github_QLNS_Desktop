using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class SktSoLieuChiTietMlnsExportQuery
    {
        public Guid Id { get; set; }
        public Guid? IdDb { get; set; }
        public Guid MlnsId { get; set; }
        public Guid? MlnsIdParent { get; set; }
        public string XauNoiMa { get; set; }
        public string LNS { get; set; }
        public string L { get; set; }
        public string K { get; set; }
        public string M { get; set; }
        public string TM { get; set; }
        public string TTM { get; set; }
        public string NG { get; set; }
        public string TNG { get; set; }
        public string TNG1 { get; set; }
        public string TNG2 { get; set; }
        public string TNG3 { get; set; }
        public string MoTa { get; set; }
        public string Chuong { get; set; }
        public bool BHangCha { get; set; }
        public string IdDonVi { get; set; }
        public string TenDonVi { get; set; }
        public double? ChiTiet { get; set; }
        public double? UocThucHien { get; set; }
        public string SKT_KyHieu { get; set; }
        public double? HangNhap { get; set; }
        public double? HangMua { get; set; }
        public double? PhanCap { get; set; }
        public double? PhanCapConLai { get; set; }
        public double? ChuaPhanCap { get; set; }
        public string ChiTietToi { get; set; }
        public bool BHangChaDuToan { get; set; }
        public double FDuToan { get; set; }
        public double FQuyetToan { get; set; }
    }
}
