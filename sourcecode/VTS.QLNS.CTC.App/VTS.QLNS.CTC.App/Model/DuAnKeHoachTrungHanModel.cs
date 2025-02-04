using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Model
{
    public class DuAnKeHoachTrungHanModel : CheckBoxItem
    {
        private string _sMaDuAn;
        public string SMaDuAn
        {
            get => _sMaDuAn;
            set => SetProperty(ref _sMaDuAn, value);
        }

        public Guid? IIdDuAnId { get; set; }

        private string _sTenDuAn;
        public string STenDuAn
        {
            get => _sTenDuAn;
            set => SetProperty(ref _sTenDuAn, value);
        }

        private string _sKhoiCong;
        public string SKhoiCong
        {
            get => _sKhoiCong;
            set => SetProperty(ref _sKhoiCong, value);
        }

        private string _sKetThuc;
        public string SKetThuc
        {
            get => _sKetThuc;
            set => SetProperty(ref _sKetThuc, value);
        }

        public string SMaKetNoi { get; set; }

        private Guid? _iIdChuDauTuId;
        public Guid? IIdChuDauTuId
        {
            get => _iIdChuDauTuId;
            set => SetProperty(ref _iIdChuDauTuId, value);
        }

        private string _iIdMaChuDauTu;
        public string IIdMaChuDauTu
        {
            get => _iIdMaChuDauTu;
            set => SetProperty(ref _iIdMaChuDauTu, value);
        }

        public string STenChuDauTu { get; set; }

        private string _sDiaDiem;
        public string SDiaDiem
        {
            get => _sDiaDiem;
            set => SetProperty(ref _sDiaDiem, value);
        }

        private double? _fHanMucDauTu;
        public double? FHanMucDauTu
        {
            get => _fHanMucDauTu;
            set => SetProperty(ref _fHanMucDauTu, value);
        }

        public string STenNguonVon { get; set; }

        private int _iLoaiDuAn;
        public int ILoaiDuAn
        {
            get => _iLoaiDuAn;
            set
            {
                SetProperty(ref _iLoaiDuAn, value);
                OnPropertyChanged(nameof(IsEnableDrp));
            }
        }

        public string STenLoaiDuAn { get; set; }

        private int _iIdNguonVonId;
        public int IIdNguonVonId
        {
            get => _iIdNguonVonId;
            set => SetProperty(ref _iIdNguonVonId, value);
        }

        private Guid? _iIdLoaiCongTrinhId;
        public Guid? IIdLoaiCongTrinhId
        {
            get => _iIdLoaiCongTrinhId;
            set => SetProperty(ref _iIdLoaiCongTrinhId, value);
        }

        public string STenLoaiCongTrinh { get; set; }

        private ObservableCollection<ComboboxItem> _drpNguonVon;
        public ObservableCollection<ComboboxItem> DrpNguonVon
        {
            get => _drpNguonVon;
            set => SetProperty(ref _drpNguonVon, value);
        }

        private bool _isEnableChk = true;
        public bool IsEnableChk
        {
            get => _isEnableChk;
            set => SetProperty(ref _isEnableChk, value);
        }

        private bool _isEnableDrp;
        public bool IsEnableDrp
        { 
            get
            {
                _isEnableDrp = ILoaiDuAn.Equals(1) ? true : false;
                return _isEnableDrp;
            }
            set => SetProperty(ref _isEnableDrp, value);
        }

        private bool _isEnableDrpNguonVon;
        public bool IsEnableDrpNguonVon
        {
            get
            {
                _isEnableDrpNguonVon = !IsEnableDd;

                if(IsHangMuc)
                {
                    _isEnableDrpNguonVon = false;
                }    
                return _isEnableDrpNguonVon;
            }
            set => SetProperty(ref _isEnableDrpNguonVon, value);
        }

        private bool _isDuAnOffer;
        public bool IsDuAnOffer
        {
            get => _isDuAnOffer;
            set => SetProperty(ref _isDuAnOffer, value);
        }

        private bool _isEnableDd;
        public bool IsEnableDd
        {
            get => _isEnableDd;
            set => SetProperty(ref _isEnableDd, value);
        }

        public Guid? IIdKeHoach5NamChiTietId { get; set; }
        public Guid? IIdKeHoach5NamId { get; set; }
        public double? FGiaTriNamThuNhat { get; set; }
        public double? FGiaTriNamThuHai { get; set; }
        public double? FGiaTriNamThuBa { get; set; }
        public double? FGiaTriNamThuTu { get; set; }
        public double? FGiaTriNamThuNam { get; set; }
        public double? FGiaTriSau5Nam { get; set; }
        public double? FGiaTriKeHoach { get; set; }
        public string SGhiChu { get; set; }

        private bool _isClone;
        public bool IsClone
        {
            get => _isClone;
            set => SetProperty(ref _isClone, value);
        }

        public int IMaDuAnIndex { get; set; }

        private bool _isModified;
        public bool IsModified
        {
            get => _isModified;
            set => SetProperty(ref _isModified, value);
        }
        public Guid? IdDuAnNguonVon { get; set; }

        private string _iIdMaDonVi;
        public string IIdMaDonVi
        {
            get => _iIdMaDonVi;
            set => SetProperty(ref _iIdMaDonVi, value);
        }
        public string STenDonVi { get; set; }

        private Guid? _iIdDonViId;
        public Guid? IIdDonViId
        {
            get => _iIdDonViId;
            set => SetProperty(ref _iIdDonViId, value);
        }

        private bool _isHangMuc;
        public bool IsHangMuc
        {
            get => _isHangMuc;
            set => SetProperty(ref _isHangMuc, value);
        }

        private bool _isDuAn;
        public bool IsDuAn
        {
            get => _isDuAn;
            set => SetProperty(ref _isDuAn, value);
        }

        public Guid? IIdParentHangMuc { get; set; }
        public string SMaHangMuc { get; set; }
        public int? IndexHangMuc { get; set; }

        private Guid? _iIdDuAnHangMucId;
        public Guid? IIdDuAnHangMucId
        {
            get => _iIdDuAnHangMucId;
            set => SetProperty(ref _iIdDuAnHangMucId, value);
        }

        private bool _isFilter = true;
        public bool IsFilter
        {
            get => _isFilter;
            set => SetProperty(ref _isFilter, value);
        }

        private Guid? _idDiscern;
        public Guid? IdDiscern
        {
            get => _idDiscern;
            set => SetProperty(ref _idDiscern, value);
        }

        public string sMaOrder { get; set; }
        public int? Loai { get; set; }
        public bool IsDuAnHangMuc { get; set; }
        public Guid? IdDuAnKhthDeXuat { get; set; }
        public DateTime? DDateCreate { get; set; }
    }
}
