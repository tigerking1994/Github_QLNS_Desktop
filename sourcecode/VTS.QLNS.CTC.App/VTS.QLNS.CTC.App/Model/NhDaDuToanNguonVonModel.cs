using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDaDuToanNguonVonModel : DetailModelBase
    {
        public Guid? IIdDuToanId { get; set; }
        public Guid? IIdQdDauTuNguonVonId { get; set; }

        private int? _iIdNguonVonId;
        public int? IIdNguonVonId
        {
            get => _iIdNguonVonId;
            set => SetProperty(ref _iIdNguonVonId, value);
        }
        private Guid? _iIdParentId;
        public Guid? IID_ParentID
        {
            get => _iIdParentId;
            set
            {
                SetProperty(ref _iIdParentId, value);
                OnPropertyChanged(nameof(IsHangCha));
            }
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
        public double? FGiaTriQdDauTuNgoaiTeKhac { get; set; }
        public double? FGiaTriQdDauTuUsd { get; set; }
        public double? FGiaTriQdDauTuVnd { get; set; }
        public double? FGiaTriQdDauTuEur { get; set; }

        private ObservableCollection<NhDaDuToanChiPhiModel> _duToanChiPhis = new ObservableCollection<NhDaDuToanChiPhiModel>();
        public ObservableCollection<NhDaDuToanChiPhiModel> DuToanChiPhis
        {
            get => _duToanChiPhis;
            set => SetProperty(ref _duToanChiPhis, value);
        }

        private bool? _isEnableEdit;
        public bool? IsEnableEdit
        {
            get => _isEnableEdit;
            set => SetProperty(ref _isEnableEdit, value);
        }
    }
}
