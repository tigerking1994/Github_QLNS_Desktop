using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ThDChungTuReportNSBDQuery
    {
        public Guid IdMucLuc { get; set; }
        public string KyHieu { get; set; }
        public string M { get; set; }
        public string NganhParent { get; set; }
        public string STT { get; set; }
        public string STTBC { get; set; }
        public string MoTa { get; set; }
        public string Nganh { get; set; }
        public bool IsHangCha { get; set; }
        public Guid IdParent { get; set; }
        public double TuChi { get; set; }
        public double SuDungTonKho { get; set; }
        public double ChiDacThuNganhPhanCap { get; set; }
        public double TuChiCTC { get; set; }
        public double SuDungTonKhoCTC { get; set; }
        public double ChiDacThuNganhPhanCapCTC { get; set; }
    }
}
