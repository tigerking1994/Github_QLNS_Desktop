using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtNcNhuCauChiQuery
    {
        public Guid Id { get; set; }
        public string sSoDeNghi { get; set; }
        public DateTime? dNgayDeNghi { get; set; }
        public Guid? iID_DonViQuanLyID { get; set; }
        public string iID_MaDonViQuanLy { get; set; }
        public int? iNamKeHoach { get; set; }
        public int? iID_NguonVonID { get; set; }
        public int iQuy { get; set; }
        public string sNguoiLap { get; set; }
        public string sTenDonVi { get; set; }
        public string sTenNguonVon { get; set; }
        public string SNoiDung { get; set; }
    }
}
