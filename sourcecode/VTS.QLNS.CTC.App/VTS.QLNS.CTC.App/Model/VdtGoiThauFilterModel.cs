using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtGoiThauFilterModel : BindableBase
    {
        public VdtGoiThauFilterModel()
        {
            this.TenGoiThau = string.Empty;
            this.DuAn = string.Empty;
            this.GiaTriFrom = null;
            this.GiaTriTo = null;
        }

        private string _tenGoiThau;
        public string TenGoiThau
        {
            get => _tenGoiThau;
            set => SetProperty(ref _tenGoiThau, value);
        }

        private string _duAn;
        public string DuAn
        {
            get => _duAn;
            set => SetProperty(ref _duAn, value);
        }

        private double? _giaTriFrom;
        public double? GiaTriFrom
        {
            get => _giaTriFrom;
            set => SetProperty(ref _giaTriFrom, value);
        }

        private double? _giaTriTo;
        public double? GiaTriTo
        {
            get => _giaTriTo;
            set => SetProperty(ref _giaTriTo, value);
        }
    }
}
