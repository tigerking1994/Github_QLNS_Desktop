using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class ProjectManagerDetailModel : DetailModelBase
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
        private Guid _idQDChiPhi;
        public Guid IdQDChiPhi
        {
            get => _idQDChiPhi;
            set => SetProperty(ref _idQDChiPhi, value);
        }

        private Guid _idChiPhi;
        public Guid IdChiPhi
        {
            get => _idChiPhi;
            set => SetProperty(ref _idChiPhi, value);
        }

        private Guid _idQDNguonVon;
        public Guid IdQDNguonVon
        {
            get => _idQDNguonVon;
            set => SetProperty(ref _idQDNguonVon, value);
        }

        private int _idNguonVon;
        public int IdNguonVon
        {
            get => _idNguonVon;
            set => SetProperty(ref _idNguonVon, value);
        }
        private Guid _idQDHangMuc;
        public Guid IdQDHangMuc
        {
            get => _idQDHangMuc;
            set => SetProperty(ref _idQDHangMuc, value);
        }
        private Guid _idHangMuc;
        public Guid IdHangMuc
        {
            get => _idHangMuc;
            set => SetProperty(ref _idHangMuc, value);
        }

        private string _tenHangMuc;
        public string TenHangMuc
        {
            get => _tenHangMuc;
            set => SetProperty(ref _tenHangMuc, value);
        }

        private double _giaTriPheDuyet;
        public double GiaTriPheDuyet
        {
            get => _giaTriPheDuyet;
            set => SetProperty(ref _giaTriPheDuyet, value);
        }
        private Guid _iIdQddauTuId;
        public Guid IIdQddauTuId
        {
            get => _iIdQddauTuId;
            set => SetProperty(ref _iIdQddauTuId, value);
        }
        private Guid _iIdDuAnId;
        public Guid IIdDuAnId
        {
            get => _iIdDuAnId;
            set => SetProperty(ref _iIdDuAnId, value);
        }
        private Guid _idChuTruongDauTu;
        public Guid IdChuTruongDauTu
        {
            get => _idChuTruongDauTu;
            set => SetProperty(ref _idChuTruongDauTu, value);
        }
        private Guid _idQdDauTu;
        public Guid IdQdDauTu
        {
            get => _idQdDauTu;
            set => SetProperty(ref _idQdDauTu, value);
        }
    }
}
