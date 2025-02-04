using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Report
{
    public class ExpertisePrintCTCReportModel : DetailModelBase
    {
        private string _kyHieu;
        public string KyHieu
        {
            get => _kyHieu;
            set => SetProperty(ref _kyHieu, value);
        }

        private string _sTT;
        public string STT
        {
            get => _sTT;
            set => SetProperty(ref _sTT, value);
        }
        public string STTBC { get; set; }

        private string _moTa;
        public string MoTa
        {
            get => _moTa;
            set => SetProperty(ref _moTa, value);
        }

        public string Nganh { get; set; }
        public Guid IdMucLuc { get; set; }
        public Guid IdParent { get; set; }
        public double TuChi1 { get; set; }
        public double SuDungTonKho1 { get; set; }
        public double ChiDacThuNganhPhanCap1 { get; set; }
        public double TuChi2 { get; set; }
        public double TuChi3 { get; set; }
    }
}
