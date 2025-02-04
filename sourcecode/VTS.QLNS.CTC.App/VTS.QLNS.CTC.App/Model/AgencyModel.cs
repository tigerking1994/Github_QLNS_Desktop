using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class AgencyModel : BindableBase
    {
        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }
        private string _id;
        public string Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string _agencyName;
        public string AgencyName
        {
            get => _agencyName;
            set => SetProperty(ref _agencyName, value);
        }

        private bool _isFilter = true;
        public bool IsFilter
        {
            get => _isFilter;
            set => SetProperty(ref _isFilter, value);
        }

        public bool IsHitTestVisible { get; set; } = true;

        public string Loai { get; set; }

        public string TenDonVi { get; set; }
        public string IIDMaDonVi { get; set; }
        public string MaTenDonVi { get; set; }

    }
}
