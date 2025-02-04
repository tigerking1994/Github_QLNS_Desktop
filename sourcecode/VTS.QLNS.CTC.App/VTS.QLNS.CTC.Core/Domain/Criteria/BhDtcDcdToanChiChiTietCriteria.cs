using System;

namespace VTS.QLNS.CTC.Core.Domain.Criteria
{
    public class BhDtcDcdToanChiChiTietCriteria
    {
        public Guid DtcDcdToanChiId { get; set; }
        public int NamLamViec { get; set; }
        public int? ILoaiTongHop { get; set; }
        public string IdDonVi { get; set; }
        public int ITrangThai { get; set; }
        public string ListIdChungTuTongHop { get; set; }
        public string IdChungTu { get; set; }
        public string TenDonVi { get; set; }
        public int LoaiChungTu { get; set; }
        public string NguoiTao { get; set; }
        public Guid ILoaiDanhMucChi { get; set; }
        public string LNS { get; set; }
        public DateTime? NgayChungTu { get; set; }
        public string UserName { get; set; }
        public Guid? IID_TongHopID { get; set; }
        public int DonViTinh { get; set; }
        public string MaLoaiChi { get; set; }

    }
}
