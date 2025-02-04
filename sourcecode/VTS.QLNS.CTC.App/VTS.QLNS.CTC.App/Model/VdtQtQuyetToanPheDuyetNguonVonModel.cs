using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtQtQuyetToanPheDuyetNguonVonModel : DetailModelBase
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

        private int? _iIdNguonVonId;
        public int? IIdNguonVonId
        {
            get => _iIdNguonVonId;
            set => SetProperty(ref _iIdNguonVonId, value);
        }

        private double _giaTriPheDuyet;
        public double GiaTriPheDuyet
        {
            get => _giaTriPheDuyet;
            set => SetProperty(ref _giaTriPheDuyet, value);
        }

        private double _fTienToTrinh;
        public double FTienToTrinh
        {
            get => _fTienToTrinh;
            set => SetProperty(ref _fTienToTrinh, value);
        }

        private Guid _idDuToan;
        public Guid IdDuToan
        {
            get => _idDuToan;
            set => SetProperty(ref _idDuToan, value);
        }

        private double _tienDeNghi;
        public double TienDeNghi
        {
            get => _tienDeNghi;
            set => SetProperty(ref _tienDeNghi, value);
        }
    }
}
