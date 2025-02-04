using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class AllocationDetailFilterModel : BindableBase
    {
        public AllocationDetailFilterModel()
        {
            this.L = string.Empty;
            this.K = string.Empty;
            this.M = string.Empty;
            this.TM = string.Empty;
            this.TTM = string.Empty;
            this.NG = string.Empty;
            this.TNG = string.Empty;
            this.TNG1 = string.Empty;
            this.TNG2 = string.Empty;
            this.TNG3 = string.Empty;
        }

        private string _l;
        public string L
        {
            get => _l;
            set => SetProperty(ref _l, value);
        }

        private string _k;
        public string K
        {
            get => _k;
            set => SetProperty(ref _k, value);
        }

        private string _m;
        public string M
        {
            get => _m;
            set => SetProperty(ref _m, value);
        }

        private string _tm;
        public string TM
        {
            get => _tm;
            set => SetProperty(ref _tm, value);
        }

        private string _ttm;
        public string TTM
        {
            get => _ttm;
            set => SetProperty(ref _ttm, value);
        }

        private string _ng;
        public string NG
        {
            get => _ng;
            set => SetProperty(ref _ng, value);
        }

        private string _tNG;
        public string TNG
        {
            get => _tNG;
            set => SetProperty(ref _tNG, value);
        }

        private string _tNG1;
        public string TNG1
        {
            get => _tNG1;
            set => SetProperty(ref _tNG1, value);
        }

        private string _tNG2;
        public string TNG2
        {
            get => _tNG2;
            set => SetProperty(ref _tNG2, value);
        }

        private string _tNG3;
        public string TNG3
        {
            get => _tNG3;
            set => SetProperty(ref _tNG3, value);
        }
    }
}
