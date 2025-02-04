using System;
using System.Collections.Generic;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportDanhMucCongKhaiTaiChinhThuChi
    {
        public string STT { get; set; }
        public string TenDonVi { get; set; }
        public string NoiDung { get; set; }
        public string MoTa { get; set; }
        public double TongCong { get; set; }
        public double DuToanDuocGiao { get; set; }
        public double SoChuaPhanBo { get; set; }
        public double SoPhanBo { get; set; }
        public double BanThan { get; set; }
        public bool bHangCha { get; set; }
        public string sMa { get; set; }
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public List<NsDtChungTuChiTietCongKhaiQuery> LstGiaTri { get; set; }
        public List<NsDtChungTuChiTietCongKhaiQuery> LstTong { get; set; }
    }
}
