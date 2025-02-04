using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhNhuCauChiQuyChiTietModel : ModelBase
    {
        public Guid? IIdNhuCauChiQuyId { get; set; }
        public Guid? IIdKhttNhiemVuChiId { get; set; }

        private Guid? _iIdHopDongId;
        public Guid? IIdHopDongId
        {
            get => _iIdHopDongId;
            set => SetProperty(ref _iIdHopDongId, value);
        }

        private bool _IsNoiDung;
        public bool IsNoiDung
        {
            get => _IsNoiDung;
            set => SetProperty(ref _IsNoiDung, value);
        }

        private bool _IsHopDong;
        public bool IsHopDong
        {
            get => _IsHopDong;
            set => SetProperty(ref _IsHopDong, value);
        }

        private string _sNoiDung;
        public string SNoiDung 
        {
            get => _sNoiDung;
            set => SetProperty(ref _sNoiDung, value);
        }

        private string _sTenDuAn;
        public string STenDuAn
        {
            get => _sTenDuAn;
            set => SetProperty(ref _sTenDuAn, value);
        }

        private double? _fNhuCauQuyNayNgoaiTeKhac;
        public double? FNhuCauQuyNayNgoaiTeKhac 
        { 
            get => _fNhuCauQuyNayNgoaiTeKhac;
            set => SetProperty(ref _fNhuCauQuyNayNgoaiTeKhac, value);
        }

        private double? _fNhuCauQuyNayUsd;
        public double? FNhuCauQuyNayUsd 
        {
            get => _fNhuCauQuyNayUsd;
            set => SetProperty(ref _fNhuCauQuyNayUsd, value);
        }

        private double? _fNhuCauQuyNayEur;
        public double? FNhuCauQuyNayEur 
        {
            get => _fNhuCauQuyNayEur;
            set => SetProperty(ref _fNhuCauQuyNayEur, value);
        }

        private double? _fNhuCauQuyNayVnd;
        public double? FNhuCauQuyNayVnd 
        {
            get => _fNhuCauQuyNayVnd;
            set => SetProperty(ref _fNhuCauQuyNayVnd, value);
        }
        public Guid? IIdParentId { get; set; }

        private Guid? _iIdDuAnId;
        public Guid? IIdDuAnId 
        {
            get => _iIdDuAnId;
            set => SetProperty(ref _iIdDuAnId, value);
        }

        // Bổ sung
        private double? _fKinhPhiDuocCapCacNamTruocSetUSD;
        public double? FKinhPhiDuocCapCacNamTruocSetUSD
        {
            get => _fKinhPhiDuocCapCacNamTruocSetUSD;
            set => SetProperty(ref _fKinhPhiDuocCapCacNamTruocSetUSD, value);
        }

        private double? _fKinhPhiDuocCapCacNamTruocSetVND;
        public double? FKinhPhiDuocCapCacNamTruocSetVND
        {
            get => _fKinhPhiDuocCapCacNamTruocSetVND;
            set => SetProperty(ref _fKinhPhiDuocCapCacNamTruocSetVND, value);
        }

        private double? _fKinhPhiDuocCapCacNamTruocSetEUR;
        public double? FKinhPhiDuocCapCacNamTruocSetEUR
        {
            get => _fKinhPhiDuocCapCacNamTruocSetEUR;
            set => SetProperty(ref _fKinhPhiDuocCapCacNamTruocSetEUR, value);
        }

        private double? _fKinhPhiDuocCapCacNamTruocSetNgoaiTeKhac;
        public double? FKinhPhiDuocCapCacNamTruocSetNgoaiTeKhac
        {
            get => _fKinhPhiDuocCapCacNamTruocSetNgoaiTeKhac;
            set => SetProperty(ref _fKinhPhiDuocCapCacNamTruocSetNgoaiTeKhac, value);
        }

        private double? _fKinhPhiDaChiCacNamTruocSetUSD;
        public double? FKinhPhiDaChiCacNamTruocSetUSD
        {
            get => _fKinhPhiDaChiCacNamTruocSetUSD;
            set => SetProperty(ref _fKinhPhiDaChiCacNamTruocSetUSD, value);
        }

        private double? _fKinhPhiDaChiCacNamTruocSetVND;
        public double? FKinhPhiDaChiCacNamTruocSetVND
        {
            get => _fKinhPhiDaChiCacNamTruocSetVND;
            set => SetProperty(ref _fKinhPhiDaChiCacNamTruocSetVND, value);
        }

        private double? _fKinhPhiDaChiCacNamTruocSetEUR;
        public double? FKinhPhiDaChiCacNamTruocSetEUR
        {
            get => _fKinhPhiDaChiCacNamTruocSetEUR;
            set => SetProperty(ref _fKinhPhiDaChiCacNamTruocSetEUR, value);
        }

        private double? _fKinhPhiDaChiCacNamTruocSetNgoaiTeKhac;
        public double? FKinhPhiDaChiCacNamTruocSetNgoaiTeKhac
        {
            get => _fKinhPhiDaChiCacNamTruocSetNgoaiTeKhac;
            set => SetProperty(ref _fKinhPhiDaChiCacNamTruocSetNgoaiTeKhac, value);
        }

        private double? _fKinhPhiDuocCapDenCuoiQuyTruocSetUSD;
        public double? FKinhPhiDuocCapDenCuoiQuyTruocSetUSD
        {
            get => _fKinhPhiDuocCapDenCuoiQuyTruocSetUSD;
            set => SetProperty(ref _fKinhPhiDuocCapDenCuoiQuyTruocSetUSD, value);
        }

        private double? _fKinhPhiDuocCapDenCuoiQuyTruocSetVND;
        public double? FKinhPhiDuocCapDenCuoiQuyTruocSetVND
        {
            get => _fKinhPhiDuocCapDenCuoiQuyTruocSetVND;
            set => SetProperty(ref _fKinhPhiDuocCapDenCuoiQuyTruocSetVND, value);
        }

        private double? _fKinhPhiDuocCapDenCuoiQuyTruocSetEUR;
        public double? FKinhPhiDuocCapDenCuoiQuyTruocSetEUR
        {
            get => _fKinhPhiDuocCapDenCuoiQuyTruocSetEUR;
            set => SetProperty(ref _fKinhPhiDuocCapDenCuoiQuyTruocSetEUR, value);
        }

        private double? _fKinhPhiDuocCapDenCuoiQuyTruocSetNgoaiTeKhac;
        public double? FKinhPhiDuocCapDenCuoiQuyTruocSetNgoaiTeKhac
        {
            get => _fKinhPhiDuocCapDenCuoiQuyTruocSetNgoaiTeKhac;
            set => SetProperty(ref _fKinhPhiDuocCapDenCuoiQuyTruocSetNgoaiTeKhac, value);
        }

        private double? _fKinhPhiDaChiDenCuoiQuyTruocSetUSD;
        public double? FKinhPhiDaChiDenCuoiQuyTruocSetUSD
        {
            get => _fKinhPhiDaChiDenCuoiQuyTruocSetUSD;
            set => SetProperty(ref _fKinhPhiDaChiDenCuoiQuyTruocSetUSD, value);
        }

        private double? _fKinhPhiDaChiDenCuoiQuyTruocSetVND;
        public double? FKinhPhiDaChiDenCuoiQuyTruocSetVND
        {
            get => _fKinhPhiDaChiDenCuoiQuyTruocSetVND;
            set => SetProperty(ref _fKinhPhiDaChiDenCuoiQuyTruocSetVND, value);
        }

        private double? _fKinhPhiDaChiDenCuoiQuyTruocSetEUR;
        public double? FKinhPhiDaChiDenCuoiQuyTruocSetEUR
        {
            get => _fKinhPhiDaChiDenCuoiQuyTruocSetEUR;
            set => SetProperty(ref _fKinhPhiDaChiDenCuoiQuyTruocSetEUR, value);
        }

        private double? _fKinhPhiDaChiDenCuoiQuyTruocSetNgoaiTeKhac;
        public double? FKinhPhiDaChiDenCuoiQuyTruocSetNgoaiTeKhac
        {
            get => _fKinhPhiDaChiDenCuoiQuyTruocSetNgoaiTeKhac;
            set => SetProperty(ref _fKinhPhiDaChiDenCuoiQuyTruocSetNgoaiTeKhac, value);
        }

        private double? _fNoiDungChiUSD;
        public double? FNoiDungChiUSD
        {
            get => _fNoiDungChiUSD;
            set => SetProperty(ref _fNoiDungChiUSD, value);
        }

        private double? _fNoiDungChiVND;
        public double? FNoiDungChiVND
        {
            get => _fNoiDungChiVND;
            set => SetProperty(ref _fNoiDungChiVND, value);
        }

        //private double? SetnhuCauSuDungKinhPhiUsd;
        //public double? NhuCauSuDungKinhPhiUsd
        //{
        //    get => SetnhuCauSuDungKinhPhiUsd;
        //    set => SetProperty(ref SetnhuCauSuDungKinhPhiUsd, value);
        //}

        //private double? SetnhuCauSuDungKinhPhiVnd;
        //public double? NhuCauSuDungKinhPhiVnd
        //{
        //    get => SetnhuCauSuDungKinhPhiVnd;
        //    set => SetProperty(ref SetnhuCauSuDungKinhPhiVnd, value);
        //}

        //private double? SetnhuCauSuDungKinhPhiEur;
        //public double? NhuCauSuDungKinhPhiEur
        //{
        //    get => SetnhuCauSuDungKinhPhiEur;
        //    set => SetProperty(ref SetnhuCauSuDungKinhPhiEur, value);
        //}

        //private double? SetnhuCauSuDungKinhPhiNgoaiTeKhac;
        //public double? NhuCauSuDungKinhPhiNgoaiTeKhac
        //{
        //    get => SetnhuCauSuDungKinhPhiNgoaiTeKhac;
        //    set => SetProperty(ref SetnhuCauSuDungKinhPhiNgoaiTeKhac, value);
        //}
        private bool _HopDong;
        public bool HopDong 
        {
            get => _HopDong;
            set => SetProperty(ref _HopDong, value);
        }

        private bool _NoiDung;
        public bool NoiDung
        {
            get => _NoiDung;
            set => SetProperty(ref _NoiDung, value);
        }

        private int _iLoaiHopDong;
        public int ILoaiHopDong
        {
            get => _iLoaiHopDong;
            set => SetProperty(ref _iLoaiHopDong, value);
        }

        private List<ComboboxItem> _itemsLoaiNoiDungChi;
        public List<ComboboxItem> ItemsLoaiNoiDungChi
        {
            get => _itemsLoaiNoiDungChi;
            set => SetProperty(ref _itemsLoaiNoiDungChi, value);
        }

        private List<ComboboxItem> _itemsTenNhiemVuChi;
        public List<ComboboxItem> ItemsTenNhiemVuChi
        {
            get => _itemsTenNhiemVuChi;
            set => SetProperty(ref _itemsTenNhiemVuChi, value);
        }

        //private List<ComboboxItem> _itemsTenDuAn;
        //public List<ComboboxItem> ItemsTenDuAn
        //{
        //    get => _itemsTenDuAn;
        //    set => SetProperty(ref _itemsTenDuAn, value);
        //}

        private ComboboxItem _selectedLoaiNoiDungChi;
        public ComboboxItem SelectedLoaiNoiDungChi
        {
            get => _selectedLoaiNoiDungChi;
            set => SetProperty(ref _selectedLoaiNoiDungChi, value);
        }

        private ComboboxItem _selectedTenNhiemVuChi;
        public ComboboxItem SelectedTenNhiemVuChi
        {
            get => _selectedTenNhiemVuChi;
            set => SetProperty(ref _selectedTenNhiemVuChi, value);
        }

        //private ComboboxItem _selectedTenDuAn;
        //public ComboboxItem SelectedTenDuAn
        //{
        //    get => _selectedTenDuAn;
        //    set => SetProperty(ref _selectedTenDuAn, value);
        //}
        public int? ILoaiNoiDungChi { get; set; }
    }
}
