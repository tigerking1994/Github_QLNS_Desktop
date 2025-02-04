using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class DuAnDeNghiThanhToanQuery
    {
        public Guid iID_DuAnID { get; set; }
        public string sTenDuAn { get; set; }
        public string sMaDuAn { get; set; }
        public string TenPhanCap { get; set; }
        public string SoQuyetDinhDauTu { get; set; }
        public double? GiaTriDauTu { get; set; }
        public double? HanMucDauTu { get; set; }
        public string sXauNoiMa { get; set; }
        public Guid? iID_MucID { get; set; }
        public Guid? iID_TieuMucID { get; set; }
        public Guid? iID_TietMucID { get; set; }
        public Guid? iID_NganhID { get; set; }
        public double? fLuyKeThanhToanKL { get; set; }
        public double? fThanhToanTrongNam { get; set; }
        public double? fChiTieuNganSachNam { get; set; }
    }
}
