using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class ChuTruongDauTuDetailModel : DetailModelBase
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

        private Guid _idChuTruongChiPhi;
        public Guid IdChuTruongChiPhi
        {
            get => _idChuTruongChiPhi;
            set => SetProperty(ref _idChuTruongChiPhi, value);
        }

        private Guid _idChiPhi;
        public Guid IdChiPhi
        {
            get => _idChiPhi;
            set => SetProperty(ref _idChiPhi, value);
        }

        private Guid _idChuTruongNguonVon;
        public Guid IdChuTruongNguonVon
        {
            get => _idChuTruongNguonVon;
            set => SetProperty(ref _idChuTruongNguonVon, value);
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

        private Guid _idChuTruongdauTu;
        public Guid IdChuTruongDauTu
        {
            get => _idChuTruongdauTu;
            set => SetProperty(ref _idChuTruongdauTu, value);
        }
    }
}
