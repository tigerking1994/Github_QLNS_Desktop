using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtKhvKeHoach5NamDeXuatReportQuery
    {
        [NotMapped]
        public string STT { get; set; }
        public Guid? IdLoaiCongTrinh { get; set; }
        public Guid? IdLoaiCongTrinhParent { get; set; }
        public string SMaLoaiCongTrinh { get; set; }
        public int? Loai { get; set; }
        public string STenDuAn { get; set; }
        public string STenDonVi { get; set; }
        public string SDiaDiem { get; set; }
        public string SThoiGianThucHien { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public double? FHanMucDauTu { get; set; }
        public string STenNguonVon { get; set; }
        public double? FTongSoNhuCau { get; set; }
        public double? FTongSo { get; set; }
        public double? FGiaTriNamThuNhat { get; set; }
        public double? FGiaTriNamThuHai { get; set; }
        public double? FGiaTriNamThuBa { get; set; }
        public double? FGiaTriNamThuTu { get; set; }
        public double? FGiaTriNamThuNam { get; set; }
        public double? FGiaTriBoTri { get; set; }
        public string SGhiChu { get; set; }
        public bool IsHangCha { get; set; }
        public int? LoaiParent { get; set; }
        public int? IIdNguonVon { get; set; }
        public double? LuyKeVonNSQPDaBoTri { get; set; }
        public double? LuyKeVonNSQPDeNghiBoTri { get; set; }
        public double? TongLuyKe { get; set; }
        [NotMapped]
        public string SSoQuyetDinhNgayQuyetDinh { get; set; }
        public double? FHanMucDauTuQP { get; set; }
        public double? FHanMucDauTuNN { get; set; }
        public double? FHanMucDauTuDP { get; set; }
        public double? FHanMucDauTuOrther { get; set; }
        public Guid? iID_KeHoach5NamID { get; set; }
        public string IdMaDonViQuanLy { get; set; }

    }
}
