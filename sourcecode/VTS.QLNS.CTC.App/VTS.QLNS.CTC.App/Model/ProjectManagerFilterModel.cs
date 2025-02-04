using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class ProjectManagerFilterModel : BindableBase
    {
        public ProjectManagerFilterModel()
        {
            this.TenDuAn = string.Empty;
            this.ThoiGianFrom = null;
            this.ThoiGianDen = null;
        }
        private string _tenDuAn;
        public string TenDuAn
        {
            get => _tenDuAn;
            set => SetProperty(ref _tenDuAn, value);
        }
        private string _thoiGianFrom;
        public string ThoiGianFrom
        {
            get => _thoiGianFrom;
            set => SetProperty(ref _thoiGianFrom, value);
        }
        private string _thoiGianDen;
        public string ThoiGianDen
        {
            get => _thoiGianDen;
            set => SetProperty(ref _thoiGianDen, value);
        }
    }
}
