using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Model
{
    public class DuAnByDenghiThanhToanUngModel : CheckBoxItem
    {
        public string sMaDuAn { get; set; }
        public string sTenDuAn { get; set; }
        public Guid? iID_DuAnID { get; set; }
        public string sTenPhanCapDuAn { get; set; }
        public Guid? iId_HopDongId { get; set; }
        public Guid? iId_NhaThauId { get; set; }
        public double? fGiaTriThanhToan { get; set; }
        public double? fGiaTriTamUng { get; set; }
        public double? fGiaTriThuHoiUngNgoaiChiTieu { get; set; }
        public double? fGiaTriThuHoi { get; set; }
        public Guid? iId_DonViTienTeId { get; set; }
        public Guid? iId_TienTeId { get; set; }
        public double? fTiGiaDonVi { get; set; }
        public double? fTiGia { get; set; }
        public double? fLuyKeUng { get; set; }
        public double? fLuyKeThanhToan { get; set; }
        public double? fLuyKeChiTieu { get; set; }
    }
}
