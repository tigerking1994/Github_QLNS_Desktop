using System;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class DonViPlanBeginYearQuery
    {
        public Guid? Id { get; set; }
        public string Id_DonVi { get; set; }
        public string TenDonVi { get; set; }
        public double? SoKiemTra { get; set; }
        public double? SoDuToan { get; set; }
        public double Tang { get; set; }
        public double Giam { get; set; }
        public string MoTa { get; set; }
        public int? LoaiNganSach { get; set; }
        public int? ILoaiNguonNganSach { get; set; }
        public string Loai { get; set; }
        public bool IsLocked { get; set; }
        public string SSoChungTu { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public string DSDonViTongHop { get; set; }
        public string DSSoChungTuTongHop { get; set; }
        public string NguoiTao { get; set; }
        public string DsLNS { get; set; }
        public bool? BDaTongHop { get; set; }
        public bool IsChildSummary { get; set; }
        public bool IsExpand { get; set; }
        public bool IsCollapse { get; set; }
        public string SoChungTuParent { get; set; }
        public bool? IsSent { get; set; }
    }
}
