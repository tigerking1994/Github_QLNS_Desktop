using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDaDuToanChiPhiModel: ModelBase
    {
        public Guid? IIdDuToanId { get; set; }
        public Guid? _iIdChiPhiId;
        public Guid? IIdChiPhiId
        {
            get => _iIdChiPhiId;
            set
            {
                SetProperty(ref _iIdChiPhiId, value);
            }
        }
        public Guid? IIdQdDauTuChiPhiId { get; set; }

        private Guid? _iIdParentId;
        public Guid? IIdParentId
        {
            get => _iIdParentId;
            set
            {
                SetProperty(ref _iIdParentId, value);
                OnPropertyChanged(nameof(IsHangCha));
            }
        }

        private string _sMaOrder;
        public string SMaOrder
        {
            get => _sMaOrder;
            set => SetProperty(ref _sMaOrder, value);
        }

        private string _sTenChiPhi;
        public string STenChiPhi
        {
            get => _sTenChiPhi;
            set => SetProperty(ref _sTenChiPhi, value);
        }

        private double? _fGiaTriNgoaiTeKhac;
        public double? FGiaTriNgoaiTeKhac
        {
            get => _fGiaTriNgoaiTeKhac;
            set => SetProperty(ref _fGiaTriNgoaiTeKhac, value);
        }

        private double? _fGiaTriUsd;
        public double? FGiaTriUsd
        {
            get => _fGiaTriUsd;
            set => SetProperty(ref _fGiaTriUsd, value);
        }

        private double? _fGiaTriVnd;
        public double? FGiaTriVnd
        {
            get => _fGiaTriVnd;
            set => SetProperty(ref _fGiaTriVnd, value);
        }

        private double? _fGiaTriEur;
        public double? FGiaTriEur
        {
            get => _fGiaTriEur;
            set => SetProperty(ref _fGiaTriEur, value);
        }

        // Another properties
        private string _sMaChiPhi;
        public string SMaChiPhi
        {
            get => _sMaChiPhi;
            set => SetProperty(ref _sMaChiPhi, value);
        }

        // Another properties
        public double? FGiaTriQdDauTuNgoaiTeKhac { get; set; }
        public double? FGiaTriQdDauTuUsd { get; set; }
        public double? FGiaTriQdDauTuVnd { get; set; }
        public double? FGiaTriQdDauTuEur { get; set; }
        public override bool IsHangCha => IIdParentId.IsNullOrEmpty();
        private ObservableCollection<NhDaDuToanHangMucModel> _duToanHangMucs = new ObservableCollection<NhDaDuToanHangMucModel>();
        public ObservableCollection<NhDaDuToanHangMucModel> DuToanHangMucs
        {
            get => _duToanHangMucs;
            set => SetProperty(ref _duToanHangMucs, value);
        }

        public Guid? IIdDuToanNguonVonId { get; set; }

        private string _sTenNguonVon;
        public string STenNguonVon 
        {
            get => _sTenNguonVon;
            set => SetProperty(ref _sTenNguonVon, value);
        }

        private bool _isHasChildren;
        public bool IsHasChildren
        {
            get => _isHasChildren;
            set => SetProperty(ref _isHasChildren, value);
        }
        public ObservableCollection<NhDmChiPhiModel> ItemsLoaiNoiDungChi { get; set; }
        public virtual bool IsLoaiNoiDungChi => IIdParentId == null;

        public virtual bool IsNoiDungChi => IIdParentId != null;
        private bool? _isEnableEdit;
        public bool? IsEnableEdit
        {
            get => _isEnableEdit;
            set => SetProperty(ref _isEnableEdit, value);
        }
    }
}
