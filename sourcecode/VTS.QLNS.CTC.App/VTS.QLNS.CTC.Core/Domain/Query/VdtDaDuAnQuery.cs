using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtDaDuAnQuery
    {
        public Guid IID_DuAnID { get; set; }
        public string SMaDuAn { get; set; }
        public string SMaKetNoi { get; set; }
        public string STenDuAn { get; set; }
        public Guid? IID_DonViQuanLyID { get; set; }
        public Guid? IID_ChuDauTuID { get; set; }
        public string iID_MaDonViQuanLy { get; set; }
        public string iID_MaChuDauTuID { get; set; }
        public string SMucTieu { get; set; }
        public string SQuyMo { get; set; }
        public string SDiaDiem { get; set; }
        public string SSuCanThietDauTu { get; set; }
        public string SSoTaiKhoan { get; set; }
        public string SDiaDiemMoTaiKhoan { get; set; }
        public string SDienTichSuDungDat { get; set; }
        public string SNguonGocSuDungDat { get; set; }
        public double? FTongMucDauTuDuKien { get; set; }
        public double? FTongMucDauTuThamDinh { get; set; }
        public double? FTongMucDauTu { get; set; }
        public Guid? IID_TienTeID { get; set; }
        public Guid? IID_DonViTienTeID { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public double? FTiGia { get; set; }
        public Guid? IID_NhomDuAnID { get; set; }
        public Guid? IID_NganhDuAnID { get; set; }
        public Guid? IID_LoaiDuAnId { get; set; }
        public Guid? IID_HinhThucDauTuID { get; set; }
        public Guid? IID_HinhThucQuanLyID { get; set; }
        public Guid? IID_NhomQuanLyID { get; set; }
        public Guid? IID_LoaiCongTrinhID { get; set; }
        public Guid? IID_CapPheDuyetID { get; set; }
        public string SKhoiCong { get; set; }
        public string SKetThuc { get; set; }
        public DateTime? DKhoiCongThucTe { get; set; }
        public DateTime? DKetThucThucTe { get; set; }
        public string STrangThaiDuAn { get; set; }
        public bool? BLaDuAnChinhThuc { get; set; }
        public Guid? IID_ParentID { get; set; }
        public string SCanBoPhuTrach { get; set; }
        public bool? BIsKetThuc { get; set; }
        public string SUserCreate { get; set; }
        public DateTime? DDateCreate { get; set; }
        public string SUserUpdate { get; set; }
        public DateTime? DDateUpdate { get; set; }
        public string SUserDelete { get; set; }
        public DateTime? DDateDelete { get; set; }
        public bool? BIsDeleted { get; set; }
        public bool? BIsCanBoDuyet { get; set; }
        public bool? BIsDuyet { get; set; }
        public bool? BIsDuPhong { get; set; }
        public double? FHanMucDauTu { get; set; }
        public double? TongMucDauTu { get; set; }
        public double? KeHoachUng { get; set; }
        public double? VonUngDaCap { get; set; }
        public double? VonUngThuHoi { get; set; }
        public Guid? iID_QDDauTuID { get; set; }
        public string SoQDDauTu { get; set; }
        public DateTime? NgayQDDauTu { get; set; }
        [NotMapped]
        public string SDisplayName
        {
            get
            {
                return string.Format("{0} - {1}", SMaDuAn, STenDuAn);
            }
        }
    }
}
