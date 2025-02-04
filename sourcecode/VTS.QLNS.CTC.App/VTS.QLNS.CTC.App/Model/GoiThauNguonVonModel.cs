using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class GoiThauNguonVonModel : DetailModelBase
    {
        private string _tenNguonVon;
        public string TenNguonVon
        {
            get => _tenNguonVon;
            set => SetProperty(ref _tenNguonVon, value);
        }

        private Guid _idGoiThauNguonVon;
        public Guid IdGoiThauNguonVon
        {
            get => _idGoiThauNguonVon;
            set => SetProperty(ref _idGoiThauNguonVon, value);
        }

        private int _idNguonVon;
        public int IdNguonVon
        {
            get => _idNguonVon;
            set => SetProperty(ref _idNguonVon, value);
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
