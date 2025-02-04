using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class GoiThauHangMucModel : DetailModelBase
    {
        private string _tenHangMuc;
        public string TenHangMuc
        {
            get => _tenHangMuc;
            set => SetProperty(ref _tenHangMuc, value);
        }

        private string _tenLoaiCongTrinh;
        public string TenLoaiCongTrinh
        {
            get => _tenLoaiCongTrinh;
            set => SetProperty(ref _tenLoaiCongTrinh, value);
        }

       
        private Guid _idGoiThauHangMuc;
        public Guid IdGoiThauHangMuc
        {
            get => _idGoiThauHangMuc;
            set => SetProperty(ref _idGoiThauHangMuc, value);
        }

        private Guid? _idParent;
        public Guid? IdParent
        {
            get => _idParent;
            set => SetProperty(ref _idParent, value);
        }

        //private double _giaTriPheDuyet;
        //public double GiaTriPheDuyet
        //{
        //    get => _giaTriPheDuyet;
        //    set => SetProperty(ref _giaTriPheDuyet, value);
        //}

        private double _giaTriGoiThau;
        public double GiaTriGoiThau
        {
            get => _giaTriGoiThau;
            set => SetProperty(ref _giaTriGoiThau, value);
        }
    }
}
