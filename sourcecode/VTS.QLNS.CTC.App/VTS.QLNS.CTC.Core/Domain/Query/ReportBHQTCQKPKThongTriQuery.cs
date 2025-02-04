using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportBHQTCQKPKThongTriQuery
    {
        // Thong tri loai1
        public string Stt { get; set; }
        public string SMaDonVi { get; set; }
        public Guid? IID_DonVi { get; set; }
        public string STenDonVi { get; set; }
        public double? FTongTienDeNghiQuyetToanQuyNay { get; set; }
        // Thong tri loai2
        public Guid IdMlns { get; set; }
        public Guid IdMlnsCha { get; set; }
        public string SLNS { get; set; }
        public string SL { get; set; }
        public string SK { get; set; }
        [NotMapped]
        public string SLK => string.IsNullOrEmpty(SL) && string.IsNullOrEmpty(SK) ? string.Empty : SL + StringUtils.DIVISION + SK;
        public string SM { get; set; }
        public string STM { get; set; }
        public string STTM { get; set; }
        public string SNG { get; set; }
        public string SXauNoiMa { get; set; }
        public double FTien_DuToanNamTruocChuyenSang { get; set; }
        public double FTien_DuToanGiaoNamNay { get; set; }
        public double FTien_TongDuToanDuocGiao { get; set; }
        public double FTienThucChi { get; set; }
        public double FTienQuyetToanDaDuyet { get; set; }
        public double FTienDeNghiQuyetToanQuyNay { get; set; }
        public double? FTienXacNhanQuyetToanQuyNay { get; set; }
        public string SMoTa { get; set; }
        public string SNoiDung { get; set; }
        public bool IsHangCha { get; set; }
        public bool IsNotData => FTien_DuToanNamTruocChuyenSang != 0 || FTienXacNhanQuyetToanQuyNay != 0
                        || FTienQuyetToanDaDuyet != 0 || FTienThucChi != 0
                        || FTien_DuToanGiaoNamNay != 0 || FTienDeNghiQuyetToanQuyNay != 0
                        || FTien_TongDuToanDuocGiao != 0;
    }
}
