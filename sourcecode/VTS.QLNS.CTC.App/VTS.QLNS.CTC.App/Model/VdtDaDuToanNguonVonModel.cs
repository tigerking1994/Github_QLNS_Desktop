using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtDaDuToanNguonVonModel : DetailModelBase
    {
        private string _tenNguonVon;
        public string TenNguonVon
        {
            get => _tenNguonVon;
            set => SetProperty(ref _tenNguonVon, value);
        }

        private Guid _idDuToanNguonVon;
        public Guid IdDuToanNguonVon
        {
            get => _idDuToanNguonVon;
            set => SetProperty(ref _idDuToanNguonVon, value);
        }

        private int? _idNguonVon;
        public int? IdNguonVon
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

        private double _fGiaTriDieuChinh;
        public double FGiaTriDieuChinh
        {
            get => _fGiaTriDieuChinh;
            set => SetProperty(ref _fGiaTriDieuChinh, value);
        }

        private double _giaTriTruocDieuChinh;
        public double GiaTriTruocDieuChinh
        {
            get => _giaTriTruocDieuChinh;
            set => SetProperty(ref _giaTriTruocDieuChinh, value);
        }

        private Guid _idDuToan;
        public Guid IdDuToan
        {
            get => _idDuToan;
            set => SetProperty(ref _idDuToan, value);
        }

        public double? FTienPheDuyetQDDT { get; set; }
    }
}
