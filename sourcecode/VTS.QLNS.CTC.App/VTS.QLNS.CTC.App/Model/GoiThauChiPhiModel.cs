using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class GoiThauChiPhiModel : DetailModelBase
    {
        private string _tenChiPhi;
        public string TenChiPhi
        {
            get => _tenChiPhi;
            set => SetProperty(ref _tenChiPhi, value);
        }

        private Guid _idGoiThauChiPhi;
        public Guid IdGoiThauChiPhi
        {
            get => _idGoiThauChiPhi;
            set => SetProperty(ref _idGoiThauChiPhi, value);
        }

        private Guid _idChiPhi;
        public Guid IdChiPhi
        {
            get => _idChiPhi;
            set => SetProperty(ref _idChiPhi, value);
        }

        private double _giaTriPheDuyet;
        public double GiaTriPheDuyet
        {
            get => _giaTriPheDuyet;
            set => SetProperty(ref _giaTriPheDuyet, value);
        }

        private double _giaTriGoiThau;
        public double GiaTriGoiThau
        {
            get => _giaTriGoiThau;
            set => SetProperty(ref _giaTriGoiThau, value);
        }
    }
}
