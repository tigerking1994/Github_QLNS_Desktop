using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class PhanBoVonChiTietQuery //: DetailModelBase
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
        public double? FCapPhatTaiKhoBacDc { get; set; }
        public double? FCapPhatBangLenhChi { get; set; }
        public double? FTonKhoanTaiDonVi { get; set; }
        public double? FGiaTriThuHoiNamTruocKhoBac { get; set; }
        public double? FGiaTriThuHoiNamTruocLenhChi { get; set; }
        public int? ILoaiDuAn { get; set; }
        public string STenDonViThucHienDuAn { get; set; }
        public string IIdMaDonViThucHienDuAn { get; set; }
        [NotMapped]
        public bool IsModified { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }
        public double? FCapPhatBangLenhChiDc { get; set; }
        public double? FTonKhoanTaiDonViDc { get; set; }
        public double? FGiaTriThuHoiNamTruocKhoBacDc { get; set; }
        public double? FGiaTriThuHoiNamTruocLenhChiDc { get; set; }
        public Guid? IIdParent { get; set; }
        public double? fChiTieuGoc { get; set; }
        public double? fThanhToanDeXuat { get; set; }
        public Guid? IID_DuAn_HangMucID { get; set; }
    }
}
