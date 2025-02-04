using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhKhChiTietHopDongModel : ModelBase
    {
        public Guid? IIdKhChiTietId { get; set; }

        private Guid _iIdKhTongTheNhiemVuChiId;
        public Guid IIdKhTongTheNhiemVuChiId
        {
            get => _iIdKhTongTheNhiemVuChiId;
            set => SetProperty(ref _iIdKhTongTheNhiemVuChiId, value);
        }

        public Guid? IIdNhHopDongId { get; set; }
        public Guid? IIdTiGiaUsdVndId { get; set; }
        public Guid? IIdTiGiaUsdNgoaiTeKhacId { get; set; }
        public int IRowIndex { get; set; }

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

        private string _tenHopDong;
        public string TenHopDong
        {
            get => _tenHopDong;
            set => SetProperty(ref _tenHopDong, value);
        }

        private string _tiGia;
        public string TiGia
        {
            get => _tiGia;
            set => SetProperty(ref _tiGia, value);
        }

        private string[] _lstTiGia;
        public string[] lstTiGia
        {
            get => _lstTiGia;
            set => SetProperty(ref _lstTiGia, value);
        }

        private ComboboxItem _selectedSoHopDong;
        public ComboboxItem SelectedSoHopDong
        {
            get => _selectedSoHopDong;
            set => SetProperty(ref _selectedSoHopDong, value);
        }

        private string _soHopDongText;
        public string SoHopDongText
        {
            get => _soHopDongText;
            set => SetProperty(ref _soHopDongText, value);
        }
    }
}