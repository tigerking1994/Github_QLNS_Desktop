using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtDaQddtNguonVonModel : DetailModelBase
    {
        private string _tenNguonVon;
        public string TenNguonVon
        {
            get => _tenNguonVon;
            set => SetProperty(ref _tenNguonVon, value);
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

        private double _giaTriPheDuyet;
        public double GiaTriPheDuyet
        {
            get => _giaTriPheDuyet;
            set => SetProperty(ref _giaTriPheDuyet, value);
        }

        private Guid _idQDDauTu;
        public Guid IdQDDauTu
        {
            get => _idQDDauTu;
            set => SetProperty(ref _idQDDauTu, value);
        }

        private double _giaTriDieuChinh;
        public double GiaTriDieuChinh
        {
            get => _giaTriDieuChinh;
            set => SetProperty(ref _giaTriDieuChinh, value);
        }

        private double _giaTriTruocDieuChinh;
        public double GiaTriTruocDieuChinh
        {
            get => _giaTriTruocDieuChinh;
            set => SetProperty(ref _giaTriTruocDieuChinh, value);
        }

        public double? GiaTriPheDuyetCTDT { get; set; }
    }
}
