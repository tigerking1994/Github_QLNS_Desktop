using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class GoiThauDetailModel : DetailModelBase
    {
        private int _loai;
        public int Loai
        {
            get => _loai;
            set => SetProperty(ref _loai, value);
        }

        private string _tenLoai;
        public string TenLoai
        {
            get => _tenLoai;
            set => SetProperty(ref _tenLoai, value);
        }

        private string _noiDung;
        public string NoiDung
        {
            get => _noiDung;
            set => SetProperty(ref _noiDung, value);
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

        private Guid _idGoiThauHangMuc;
        public Guid IdGoiThauHangMuc
        {
            get => _idGoiThauHangMuc;
            set => SetProperty(ref _idGoiThauHangMuc, value);
        }

        private Guid _idHangMuc;
        public Guid IdHangMuc
        {
            get => _idHangMuc;
            set => SetProperty(ref _idHangMuc, value);
        }

        private double? _tongMucDT;
        public double? TongMucDT
        {
            get => _tongMucDT;
            set => SetProperty(ref _tongMucDT, value);
        }

        private double _giaTriPheDuyet;
        public double GiaTriPheDuyet
        {
            get => _giaTriPheDuyet;
            set => SetProperty(ref _giaTriPheDuyet, value);
        }

        private Guid _idGoiThau;
        public Guid IdGoiThau
        {
            get => _idGoiThau;
            set => SetProperty(ref _idGoiThau, value);
        }
        public bool IsDieuChinh { get; set; }

        private double _giaTriTruocDC;
        public double GiaTriTruocDC
        {
            get => _giaTriTruocDC;
            set => SetProperty(ref _giaTriTruocDC, value);
        }

        public int TypeExecute { get; set; }
        public Guid? IIdKhlcnhaThau { get; set; }
    }
}
