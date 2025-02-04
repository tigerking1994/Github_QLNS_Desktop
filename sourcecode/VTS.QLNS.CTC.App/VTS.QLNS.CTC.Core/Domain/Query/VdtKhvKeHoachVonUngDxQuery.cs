using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtKhvKeHoachVonUngDxQuery
    {
        public Guid Id { get; set; }
        public string sSoDeNghi { get; set; }
        public DateTime? dNgayDeNghi { get; set; }
        public int? iNamKeHoach { get; set; }
        public Guid? iID_DonViQuanLyID { get; set; }
        public string iID_MaDonViQuanLy { get; set; }
        public Guid? iID_NhomQuanLyID { get; set; }
        public double? fGiaTriUng { get; set; }
        public Guid? iID_DonViTienTeID { get; set; }
        public Guid? iID_TienTeID { get; set; }
        public double? fTiGiaDonVi { get; set; }
        public double? fTiGia { get; set; }
        public int? iID_NguonVonID { get; set; }
        public string sTongHop { get; set; }
        public string sTenDonViQuanLy { get; set; }
        public string sTenNguonVon { get; set; }
        public string sUserCreate { get; set; }
        public bool? bKhoa { get; set; }
        public string sSoLanDieuChinh { get; set; }
        public Guid? iID_ParentId { get; set; }
        public bool? bActive { get; set; }
        public bool? bIsGoc { get; set; }
    }
}
