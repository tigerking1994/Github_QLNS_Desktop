using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ThanhToanQuaKhoBacChiTietQuery
    {
        public Guid iID_DuAnID { get; set; }
        public string sXauNoiMa { get; set; }
        public string sTenDuAn { get; set; }
        public Guid? iID_MucID { get; set; }
        public Guid? iID_TieuMucID { get; set; }
        public Guid? iID_TietMucID { get; set; }
        public Guid? iID_NganhID { get; set; }
        public Guid? iID_HopDongID { get; set; }
        public Guid? iID_NhaThauID { get; set; }
        public double? fGiaTriThanhToan { get; set; }
        public double? fGiaTriTamUng { get; set; }
        public Guid? iID_DonViTienTeID { get; set; }
        public double? fTiGiaDonVi { get; set; }
        public Guid? iID_TienTeID { get; set; }
        public double? fTiGia { get; set; }
        public double fChiTieuNganSachNam { get; set; }
    }
}
