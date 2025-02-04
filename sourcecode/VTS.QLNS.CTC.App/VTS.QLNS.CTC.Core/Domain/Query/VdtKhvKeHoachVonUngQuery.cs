using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtKhvKeHoachVonUngQuery
    {
        public Guid Id { get; set; }
        public string sSoQuyetDinh { get; set; }
        public DateTime? dNgayQuyetDinh { get; set; }
        public int? iNamKeHoach { get; set; }
        public Guid? iId_DonViQuanLyId { get; set; }
        public string iID_MaDonViQuanLy { get; set; }
        public Guid? iId_NhomQuanLyId { get; set; }
        public string sLoaiNganSach { get; set; }
        public string sKhoanNganSach { get; set; }
        public double? fGiaTriUng { get; set; }
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
        public int? iId_NguonVonId { get; set; }
        public Guid? iID_LoaiNguonVonID { get; set; }
        public string sTenDonViQuanLy { get; set; }
        public string sTenNguonVon { get; set; }
        public string sTenLoaiNguonVon { get; set; }
        public Guid? iID_KeHoachUngDeXuatID { get; set; }
        public bool? bActive { get; set; }
        public bool? bIsGoc { get; set; }
        public Guid? iId_ParentId { get; set; }
        public string sSoLanDieuChinh { get; set; }
    }
}
