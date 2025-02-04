using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtQtDenghiQuyetToanNienDoQuery
    {
        public Guid Id { get; set; }
        public string sSoDeNghi { get; set; }
        public DateTime? dNgayDeNghi { get; set; }
        public Guid? iID_DonViDeNghiID { get; set; }
        public string iID_MaDonViDeNghi { get; set; }
        public string sNguoiDeNghi { get; set; }
        public int? iNamKeHoach { get; set; }
        public Guid? iID_LoaiNguonVonID { get; set; }
        public string sUserCreate { get; set; }
        public DateTime? dDateCreate { get; set; }
        public string sUserUpdate { get; set; }
        public DateTime? dDateUpdate { get; set; }
        public string sUserDelete { get; set; }
        public DateTime? dDateDelete { get; set; }
        public int? iID_NguonVonID { get; set; }
        public string sTenDonVi { get; set; }
        public string sTenNguonVon { get; set; }
        public string sLoaiNguonVon { get; set; }
        public double? fGiaTriNamNay { get; set; }
        public double? fGiaTriNamTruoc { get; set; }
    }
}
