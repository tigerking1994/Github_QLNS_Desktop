using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model
{
    public class PheDuyetThanhToanChiTietModel : BindableBase
    {

        private int _iLoaiDeNghi;
        public int ILoaiDeNghi
        {
            get => _iLoaiDeNghi;
            set => SetProperty(ref _iLoaiDeNghi, value);
        }

        private int _iLoaiNamKeHoach;
        public int ILoaiNamKeHoach
        {
            get => _iLoaiNamKeHoach;
            set => SetProperty(ref _iLoaiNamKeHoach, value);
        }

        private double _fGiaTriTrongNuoc;
        public double FGiaTriTrongNuoc
        {
            get => _fGiaTriTrongNuoc;
            set => SetProperty(ref _fGiaTriTrongNuoc, value);
        }

        private double _fGiaTriNgoaiNuoc;
        public double FGiaTriNgoaiNuoc
        {
            get => _fGiaTriNgoaiNuoc;
            set => SetProperty(ref _fGiaTriNgoaiNuoc, value);
        }

        private Guid? _IIdKeHoachVonID;
        public Guid? IIdKeHoachVonID
        {
            get => _IIdKeHoachVonID;
            set => SetProperty(ref _IIdKeHoachVonID, value);
        }

        private double _fTongSo;
        public double FTongSo
        {
            get => _fTongSo;
            set => SetProperty(ref _fTongSo, value);
        }

        private double _fDefaultValueTN;
        public double FDefaultValueTN
        {
            get => _fDefaultValueTN;
            set => SetProperty(ref _fDefaultValueTN, value);
        }

        private double _fDefaultValueNN;
        public double FDefaultValueNN
        {
            get => _fDefaultValueNN;
            set => SetProperty(ref _fDefaultValueNN, value);
        }

        public Guid? IIdMuc { get; set; }
        public Guid? IIdTieuMuc { get; set; }
        public Guid? IIdTietMuc { get; set; }
        public Guid? IIdNganh { get; set; }

        private string _sGhiChu;
        public string SGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
        }

        private string _sTenMucLuc;
        public string STenMucLuc
        {
            get => _sTenMucLuc;
            set => SetProperty(ref _sTenMucLuc, value);
        }

        private bool _isDeleted;
        public bool IsDeleted
        {
            get => _isDeleted;
            set => SetProperty(ref _isDeleted, value);
        }

        private double _fTongVonDaTamUng;
        public double FTongVonDaTamUng
        {
            get => _fTongVonDaTamUng;
            set => SetProperty(ref _fTongVonDaTamUng, value);
        }

        private double _fTongVonDaThuHoi;
        public double FTongVonDaThuHoi
        {
            get => _fTongVonDaThuHoi;
            set => SetProperty(ref _fTongVonDaThuHoi, value);
        }

        private VdtTtKeHoachVonQuery _selectedKeHoachVon;
        public VdtTtKeHoachVonQuery SelectedKeHoachVon
        {
            get => _selectedKeHoachVon;
            set => SetProperty(ref _selectedKeHoachVon, value);
        }

        private ObservableCollection<VdtTtKeHoachVonQuery> _itemsKeHoachVon;
        public ObservableCollection<VdtTtKeHoachVonQuery> ItemsKeHoachVon
        {
            get => _itemsKeHoachVon;
            set => SetProperty(ref _itemsKeHoachVon, value);
        }

        private string _lns;
        public string LNS 
        { 
            get => _lns; 
            set => SetProperty(ref _lns, value); 
        }
        
        private string _l;
        public string L 
        { 
            get => _l; 
            set => SetProperty(ref _l, value); 
        }

        private string _k;
        public string K 
        { 
            get => _k; 
            set => SetProperty(ref _k, value); 
        }

        private string _m;
        public string M 
        { 
            get => _m; 
            set => SetProperty(ref _m, value); 
        }

        private string _tm;
        public string TM 
        { 
            get => _tm; 
            set => SetProperty(ref _tm, value); 
        }

        private string _ttm;
        public string TTM 
        { 
            get => _ttm; 
            set => SetProperty(ref _ttm, value); 
        }

        private string _ng;
        public string NG 
        { 
            get => _ng; 
            set => SetProperty(ref _ng, value); 
        }

        private ComboboxItem _selectedLNS;
        public ComboboxItem SelectedLNS
        {
            get => _selectedLNS;
            set => SetProperty(ref _selectedLNS, value);
        }

        private ComboboxItem _selectedL;
        public ComboboxItem SelectedL
        {
            get => _selectedL;
            set => SetProperty(ref _selectedL, value);
        }

        private ComboboxItem _selectedK;
        public ComboboxItem SelectedK
        {
            get => _selectedK;
            set => SetProperty(ref _selectedK, value);
        }

        private ComboboxItem _selectedM;
        public ComboboxItem SelectedM
        {
            get => _selectedM;
            set => SetProperty(ref _selectedM, value);
        }

        private ComboboxItem _selectedTM;
        public ComboboxItem SelectedTM
        {
            get => _selectedTM;
            set => SetProperty(ref _selectedTM, value);
        }

        private ComboboxItem _selectedTTM;
        public ComboboxItem SelectedTTM
        {
            get => _selectedTTM;
            set => SetProperty(ref _selectedTTM, value);
        }

        private ComboboxItem _selectedNG;
        public ComboboxItem SelectedNG
        {
            get => _selectedNG;
            set => SetProperty(ref _selectedNG, value);
        }

        private ObservableCollection<ComboboxItem> _itemsLNS;
        public ObservableCollection<ComboboxItem> ItemsLNS
        {
            get => _itemsLNS;
            set => SetProperty(ref _itemsLNS, value);
        }

        private ObservableCollection<ComboboxItem> _itemsL;
        public ObservableCollection<ComboboxItem> ItemsL
        {
            get => _itemsL;
            set => SetProperty(ref _itemsL, value);
        }

        private ObservableCollection<ComboboxItem> _itemsK;
        public ObservableCollection<ComboboxItem> ItemsK
        {
            get => _itemsK;
            set => SetProperty(ref _itemsK, value);
        }

        private ObservableCollection<ComboboxItem> _itemsM;
        public ObservableCollection<ComboboxItem> ItemsM
        {
            get => _itemsM;
            set => SetProperty(ref _itemsM, value);
        }

        private ObservableCollection<ComboboxItem> _itemsTM;
        public ObservableCollection<ComboboxItem> ItemsTM
        {
            get => _itemsTM;
            set => SetProperty(ref _itemsTM, value);
        }

        private ObservableCollection<ComboboxItem> _itemsTTM;
        public ObservableCollection<ComboboxItem> ItemsTTM
        {
            get => _itemsTTM;
            set => SetProperty(ref _itemsTTM, value);
        }

        private ObservableCollection<ComboboxItem> _itemsNG;
        public ObservableCollection<ComboboxItem> ItemsNG
        {
            get => _itemsNG;
            set => SetProperty(ref _itemsNG, value);
        }
    }
}
