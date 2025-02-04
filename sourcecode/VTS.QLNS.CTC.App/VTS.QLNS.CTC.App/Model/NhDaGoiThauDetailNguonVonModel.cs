using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDaGoiThauDetailNguonVonModel : BindableBase
    {
        public Guid? Id { get; set; }

        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value);
        }

        public int? IIdNguonVonID { get; set; }
        public string STenNguonVon { get; set; }

        private double _fGiaTriNgoaiTeKhac;
        public double FGiaTriNgoaiTeKhac
        {
            get => _fGiaTriNgoaiTeKhac;
            set => SetProperty(ref _fGiaTriNgoaiTeKhac, value);
        }

        private double _fGiaTriUSD;
        public double FGiaTriUSD
        {
            get => _fGiaTriUSD;
            set => SetProperty(ref _fGiaTriUSD, value);
        }

        private double _fGiaTriVND;
        public double FGiaTriVND
        {
            get => _fGiaTriVND;
            set => SetProperty(ref _fGiaTriVND, value);
        }

        private double _fGiaTriEUR;
        public double FGiaTriEUR
        {
            get => _fGiaTriEUR;
            set => SetProperty(ref _fGiaTriEUR, value);
        }

        private double _fGiaTriPheDuyetNgoaiTeKhac;
        public double FGiaTriPheDuyetNgoaiTeKhac
        {
            get => _fGiaTriPheDuyetNgoaiTeKhac;
            set => SetProperty(ref _fGiaTriPheDuyetNgoaiTeKhac, value);
        }

        private double _fGiaTriPheDuyetUSD;
        public double FGiaTriPheDuyetUSD
        {
            get => _fGiaTriPheDuyetUSD;
            set => SetProperty(ref _fGiaTriPheDuyetUSD, value);
        }

        private double _fGiaTriPheDuyetVND;
        public double FGiaTriPheDuyetVND
        {
            get => _fGiaTriPheDuyetVND;
            set => SetProperty(ref _fGiaTriPheDuyetVND, value);
        }

        private double _fGiaTriPheDuyetEUR;
        public double FGiaTriPheDuyetEUR
        {
            get => _fGiaTriPheDuyetEUR;
            set => SetProperty(ref _fGiaTriPheDuyetEUR, value);
        }

        private Guid? _iIdGoiThauID;
        public Guid? IIdGoiThauID
        {
            get => _iIdGoiThauID;
            set => SetProperty(ref _iIdGoiThauID, value);
        }
    }
}
