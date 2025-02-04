using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportBHQTCQKPQuanLyThongTriQuery
    {
        // Thong tri loai1
        public string Stt { get; set; }
        public string SMaDonVi { get; set; }
        public Guid? IID_DonVi { get; set; }
        public string STenDonVi { get; set; }
        public double? FTongTienDeNghi { get; set; }
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
        public double? FTienDuToanDuocGiao { get; set; }
        public double? FTienThucChi { get; set; }
        public double? FTienQuyetToanDaDuyet { get; set; }
        public double? FTienDeNghiQuyetToanQuyNay { get; set; }
        public double? FTienXacNhanQuyetToanQuyNay { get; set; }
        public string SNoiDung { get; set; }
        public string SMoTa { get; set; }
        public string SGhiChu { get; set; }
        public bool IsHangCha { get; set; }
        public bool IsNotData => FTienDuToanDuocGiao.GetValueOrDefault(0) != 0 || FTienThucChi.GetValueOrDefault(0) != 0
                                || FTienQuyetToanDaDuyet.GetValueOrDefault(0) != 0 || FTienDeNghiQuyetToanQuyNay.GetValueOrDefault(0) != 0
                                || FTienXacNhanQuyetToanQuyNay.GetValueOrDefault(0) != 0;
        public string SDuToanChiTietToi { get;set; }
    }
}
