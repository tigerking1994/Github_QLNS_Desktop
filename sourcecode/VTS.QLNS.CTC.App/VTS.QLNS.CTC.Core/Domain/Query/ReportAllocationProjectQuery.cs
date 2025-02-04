using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportAllocationProjectQuery
    {
        public Guid DuAnID { get; set; }
        public string MaDuAn { get; set; }
        public Guid DonViQuanLyID { get; set; }
        public string TenDuAn { get; set; }
        public string TenDonVi { get; set; }
        public Guid LoaiCongTrinhID { get; set; }
        public Guid CapPheDuyetID { get; set; }
        public string TrangThaiDuAnDangKy { get; set; }
        public string SoQuyetDinh { get; set; }
        public DateTime? NgayDuyet { get; set; }
        public string TienDo { get; set; }
        public double TMDT_NSQP { get; set; }
        public double TMDT_NSNN { get; set; }
        public double TMDT_Khac { get; set; }
        public double Tong_TMDT { get; set; }
        public double LuyKeVonNamTruoc { get; set; }
        public double ChiTieuNganSachNam { get; set; }
        public double ThanhToan { get; set; }
        public double TamUng { get; set; }
        public double ThuUng { get; set; }
        public double KeHoachUngNgoaiChiTieu { get; set; }
        public double CapUngNgoaiChiTieu { get; set; }
        public double ThuUngXDCB { get; set; }
        public double SoUngConPhaiThu { get; set; }
        public double ChiTieuConLaiChuaCap { get; set; }
        public double SoVonConBoTriTiep { get; set; }
    }
}
