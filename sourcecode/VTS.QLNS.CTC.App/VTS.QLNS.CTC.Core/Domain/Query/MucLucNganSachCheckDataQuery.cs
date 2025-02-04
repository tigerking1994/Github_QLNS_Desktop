using System;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class MucLucNganSachCheckDataQuery
    {
        public Guid MLNSId { get; set; }
        public Guid ID { get; set; }
        public Guid ID_Parent { get; set; }
        public int IRowIndex { get; set; }
        public Guid IdCacQuyetDinh { get; set; }
        public Guid IdNhiemVuChi { get; set; }
        public string SSoQuyetDinh { get; set; }
        public int ILoaiQuyetDinh { get; set; }
        public string STenNhiemVuChi { get; set; }
        public string LoaiChungTu { get; set; }
        public string SoChungTu { get; set; }
        public DateTime? NgayChungTu { get; set; }
        public string SoQuyetDinh { get; set; }
        public DateTime? NgayQuyetDinh { get; set; }
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public string TenDonViDayDu => string.Format("{0} - {1}", MaDonVi, TenDonVi);
        public string MoTa { get; set; }
        public double SoTien { get; set; }
        public string ThangQuy { get; set; }
        public string LoaiQuyetToan { get; set; }
        public string LoaiCapPhat { get; set; }
        public bool Selected { get; set; }
        public string Loai { get; set; }
        public string XauNoiMa { get; set; }
        public string LNS { get; set; }
        public int INamNganSach { get; set; }

    }
}
