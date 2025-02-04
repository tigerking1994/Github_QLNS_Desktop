using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtKhvPhanBoVonDonViChiTietDieuChinhQuery
    {
        public Guid iID_DuAnID { get; set; }
        public string sTenDuAn { get; set; }
        public string sMaDuAn { get; set; }
        public string sThoiGianThucHien { get; set; }
        public Guid? iID_CapPheDuyetID { get; set; }
        public string sTenCapPheDuyet { get; set; }
        public Guid? iID_LoaiCongTrinhID { get; set; }
        public string sTenLoaiCongTrinh { get; set; }
        public string sTenChuDauTu { get; set; }
        public double? fTongMucDauTuDuocDuyet { get; set; }
        public double? fLuyKeVonNamTruoc { get; set; }
        public double? fKeHoachVonDuocDuyetNamNay { get; set; }
        public double? fVonKeoDaiCacNamTruoc { get; set; }
        public double? fUocThucHien { get; set; }
        public double? fThuHoiVonUngTruoc { get; set; }
        public double? fThanhToan { get; set; }
        public string sTrangThaiDuAnDangKy { get; set; }
        public double? FUocThucHienSauDc { get; set; }
        public double? FThuHoiVonUngTruocSauDc { get; set; }
        public double? FThanhToanSauDc { get; set; }
        public Guid? IIDParentId { get; set; }
        public int? ILoaiDuAn { get; set; }
    }
}
