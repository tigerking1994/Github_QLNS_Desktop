using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtKhvKeHoach5NamChuyenTiepReportQuery
    {
        [NotMapped]
        public string STT { get; set; }
        public string IIdMaDonVi { get; set; }
        public string STenDuAn { get; set; }
        public string STienDoThucHien { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public string NgayThangQuyetDinh
        {
            get => (IsHangCha.HasValue && !IsHangCha.Value) ? (string.Format("{0} - {1}", SSoQuyetDinh, (DNgayQuyetDinh.HasValue ? DNgayQuyetDinh.Value.ToString("dd/MM/yyyy") : string.Empty))) : string.Empty;
        }
        public double? TongMucDauTu { get; set; }
        public double? TongMucDauTuNSQP { get; set; }
        public double? ChiPhiDuPhong { get; set; }
        public double? TongSo { get; set; }
        public double? VonBoTriHetNam { get; set; }
        public double? VonDaBoTriNam { get; set; }
        public double? TongMucDauTuPhaiBoTri { get; set; }
        public double? KeHoachVonNamDoDuyet { get; set; }
        public double? KeHoachVonDeNghiBoTriNam { get; set; }
        public double? ChenhLechSoVoiQuyetDinhBo { get; set; }
        public string SGhiChu { get; set; }
        public bool? IsHangCha { get; set; }
    }
}
