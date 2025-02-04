using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtTtDeNghiThanhToanChiTietQuery
    {
        public Guid iID_DuAnID { get; set; }
        public Guid? iID_HopDongID { get; set; }
        public Guid? iID_NhaThauID { get; set; }
        public double? fGiaTriThanhToanTN { get; set; }
        public double? fGiaTriThanhToanNN { get; set; }
        public double? fGiaTriThuHoiNamTruocTN { get; set; }
        public double? fGiaTriThuHoiNamTruocNN { get; set; }
        public double? fGiaTriThuHoiNamNayTN { get; set; }
        public double? fGiaTriThuHoiNamNayNN { get; set; }
        public string sXauNoiMa { get; set; }
        public string sTenDuAn { get; set; }
        public double? fLuyKeThanhToanKLHT { get; set; }
        public double? fChiTieuNganSachNam { get; set; }
        public double? fGiaTriDaThanhToanTrongNam { get; set; }
        public double? fSoThucThanhToanDotNay { get; set; }
        public string sGhiChu { get; set; }
        public string sThongTinNhaThau { get; set; }
        public string sThongTinHopDong { get; set; }
        public Guid? iId_GoiThau { get; set; }
        public Guid? iID_MucID { get; set; }
        public Guid? iID_TieuMucID { get; set; }
        public Guid? iID_TietMucID { get; set; }
        public Guid? iID_NganhID { get; set; }
        public string M { get; set; }
        public string TM { get; set; }
        public string TTM { get; set; }
        public string NG { get; set; }
    }
}
