using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlCachTinhLuongNq104Model : ModelBase
    {
        public int Stt { get; set; }
        public string MaCachTl { get; set; }
        public string TenCachTl { get; set; }

        private string _maCot;
        public string MaCot
        {
            get => _maCot;
            set => SetProperty(ref _maCot, value);
        }

        private string _tenCot;
        public string TenCot
        {
            get => _tenCot;
            set => SetProperty(ref _tenCot, value);
        }

        public string CongThuc { get; set; }
        public string NoiDung { get; set; }
        public string MaKmcp { get; set; }
        public string MaKmcp1 { get; set; }
        public decimal Value { get; set; }
        public bool IsCalculated { get; set; }
        public int? Thang { get; set; }
        public int? Nam { get; set; }
    }
}
