using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class PhanBoVonDonViPheDuyetQuery
    {
        public Guid Id { get; set; }
        public string sSoQuyetDinh { get; set; }
        public DateTime? dNgayQuyetDinh { get; set; }
        public int? iNamKeHoach { get; set; }
        public Guid? iId_LoaiNguonVonId { get; set; }
        public Guid? iId_DonViQuanLyId { get; set; }
        public string iID_MaDonViQuanLy { get; set; }
        public Guid? iId_NhomQuanLyId { get; set; }
        public Guid? iId_LoaiNganSachId { get; set; }
        public Guid? iId_KhoanNganSachId { get; set; }
        public string sLoaiDieuChinh { get; set; }
        public Guid? iId_ParentId { get; set; }
        public bool? bActive { get; set; }
        public bool? bIsGoc { get; set; }
        public bool? BKhoa { get; set; }
        public bool? bLaThayThe { get; set; }
        public double? fGiaTrDeNghi { get; set; }
        public double? fGiaTrPhanBo { get; set; }
        public double? fGiaTriThuHoi { get; set; }
        public Guid? iId_DonViTienTeId { get; set; }
        public Guid? iId_TienTeId { get; set; }
        public double? fTiGiaDonVi { get; set; }
        public double? fTiGia { get; set; }
        public string sUserCreate { get; set; }
        public DateTime? dDateCreate { get; set; }
        public string sUserUpdate { get; set; }
        public DateTime? dDateUpdate { get; set; }
        public string sUserDelete { get; set; }
        public DateTime? dDateDelete { get; set; }
        public bool? bIsCanBoDuyet { get; set; }
        public bool? bIsDuyet { get; set; }
        public int? iId_NguonVonId { get; set; }
        public string sNguonVon { get; set; }
        public string sLoaiNguonVon { get; set; }
        public string sTenDonVi { get; set; }
        public double fChiTieuDauNam { get; set; }
        public double fChiTieuBoXung { get; set; }
        public int iSoLanDieuChinh { get; set; }
        public Guid? IIdPhanBoGocID { get; set; }

        public string sCanBoDuyet
        {
            get
            {
                return this.bIsCanBoDuyet ?? false ? "Đã duyệt" : "Chưa duyệt";
            }
        }

        public string sDuyet
        {
            get
            {
                return this.bIsDuyet ?? false ? "Đã duyệt" : "Chưa duyệt";
            }
        }
        public bool? BActive { get; set; }
        public string DieuChinhTu { get; set; }
        public double? FCapPhatTaiKhoBac { get; set; }
        public double? FCapPhatBangLenhChi { get; set; }
    }
}
