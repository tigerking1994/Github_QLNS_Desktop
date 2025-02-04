using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class PhanBoVonDonViChiTietPheDuyetQuery //: DetailModelBase
    {
        public Guid? IdChungTu { get; set; }
        public Guid? IdChungTuParent { get; set; }
        public bool? BActive { get; set; }
        public bool? IsGoc { get; set; }
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
        public double fVonDaBoTri { get; set; }

        public double fChiTieuBoXungTrongNam { get; set; }
        public double fNamTruocChuyenSang { get; set; }
        public double? fThuUngXDCB { get; set; }
        public double? fChiTieuNganSach { get; set; }
        public string sGhiChu { get; set; }
        public double? FCapPhatTaiKhoBac { get; set; }
        public double? FCapPhatBangLenhChi { get; set; }
        public double? FGiaTriThuHoiNamTruocKhoBac { get; set; }
        public double? FGiaTriThuHoiNamTruocLenhChi { get; set; }
        public double? FGiaTriThuHoi { get; set; }
        public double? FGiaTrPhanBo { get; set; }
        public int? ILoaiDuAn { get; set; }
        public string STenDonViThucHienDuAn { get; set; }
        public string IIdMaDonViThucHienDuAn { get; set; }
        [NotMapped]
        public bool IsModified { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }
        [NotMapped]
        public double? FCapPhatTaiKhoBacDc { get; set; }
        [NotMapped]
        public double? FCapPhatBangLenhChiDc { get; set; }
        [NotMapped]
        public double? FGiaTriThuHoiNamTruocKhoBacDc { get; set; }
        [NotMapped]
        public double? FGiaTriThuHoiNamTruocLenhChiDc { get; set; }
        [NotMapped]
        public double? FGiaTriThuHoiDc { get; set; }
        [NotMapped]
        public double? FGiaTrPhanBoDc { get; set; }
        public Guid? IIdParent { get; set; }
        public double? fChiTieuGoc { get; set; }
        public double? fThanhToanDeXuat { get; set; }

        public Guid? iID_DuAn_HangMucID { get; set; }
    }
}
