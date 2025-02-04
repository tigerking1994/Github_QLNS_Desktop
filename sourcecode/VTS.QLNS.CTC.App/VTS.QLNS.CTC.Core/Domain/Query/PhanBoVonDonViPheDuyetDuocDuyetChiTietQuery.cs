using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class PhanBoVonDonViPheDuyetDuocDuyetChiTietQuery
    {
        public Guid iID_DuAnID { get; set; }
        public string sTenDuAn { get; set; }
        public string sMaDuAn { get; set; }
        public string sTrangThaiDuAn { get; set; }
        public string sKhoiCong { get; set; }
        public string sKetThuc { get; set; }
        public string sMaKetNoi { get; set; }
        public string sTenLoaiCongTrinh { get; set; }
        public string sTenCapPheDuyet { get; set; }
        public double fGiaTriDauTu { get; set; }
        public Guid? iID_LoaiCongTrinhID { get; set; }
        public Guid? iID_CapPheDuyetID { get; set; }
        public string sLNS { get; set; }
        public string sL { get; set; }
        public string sK { get; set; }
        public string sM { get; set; }
        public string sTM { get; set; }
        public string sTTM { get; set; }
        public string sNG { get; set; }
        public string sGhiChu { get; set; }
        public double? FCapPhatTaiKhoBac { get; set; }
        public double? FCapPhatBangLenhChi { get; set; }
        public double? FGiaTriThuHoiNamTruocKhoBac { get; set; }
        public double? FGiaTriThuHoiNamTruocLenhChi { get; set; }
        public double? FGiaTriThuHoi { get; set; }
        public double? FGiaTrPhanBo { get; set; }
        public double? FGiaTriThuHoiDc { get; set; }
        public double? FGiaTrPhanBoDc { get; set; }
        public double? FCapPhatTaiKhoBacDc { get; set; }
        public double? FCapPhatBangLenhChiDc { get; set; }
        public double? FGiaTriThuHoiNamTruocKhoBacDc { get; set; }
        public double? FGiaTriThuHoiNamTruocLenhChiDc { get; set; }
        public Guid? IIdParent { get; set; }
        public int? ILoaiDuAn { get; set; }
        public string STenDonViThucHienDuAn { get; set; }
        public string IIdMaDonViThucHienDuAn { get; set; }

        public Guid? iID_DuAn_HangMucID { get; set; }
        public double? fThanhToanDeXuat { get; set; }
    }
}
