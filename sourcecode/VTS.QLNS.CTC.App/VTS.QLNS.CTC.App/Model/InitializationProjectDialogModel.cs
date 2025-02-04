using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.ViewModel;

namespace VTS.QLNS.CTC.App.Model
{
    public class InitializationProjectDialogModel : ViewModelBase
    {
        private Guid _id;
        public Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private int _iNamKhoiTao;
        public int INamKhoiTao
        {
            get => _iNamKhoiTao;
            set => SetProperty(ref _iNamKhoiTao, value);
        }

        private Guid? _iIdDonViId;
        public Guid? IIdDonViId
        {
            get => _iIdDonViId;
            set => SetProperty(ref _iIdDonViId, value);
        }

        private Guid? _iIdDuAnId;
        public Guid? IIdDuAnId
        {
            get => _iIdDuAnId;
            set => SetProperty(ref _iIdDuAnId, value);
        }

        private Guid? _iIdQddauTuId;
        public Guid? IIdQddauTuId
        {
            get => _iIdQddauTuId;
            set => SetProperty(ref _iIdQddauTuId, value);
        }

        private Guid? _iIdDuToanId;
        public Guid? IIdDuToanId
        {
            get => _iIdDuToanId;
            set => SetProperty(ref _iIdDuToanId, value);
        }

        private double? _fKhvonUng;
        public double? FKhvonUng
        {
            get => _fKhvonUng;
            set {
                SetProperty(ref _fKhvonUng, value);
                OnPropertyChanged(nameof(FGiaTriConPhaiUng));
            }
        }

        private double? _fVonUngDaCap;
        public double? FVonUngDaCap
        {
            get => _fVonUngDaCap;
            set
            {
                SetProperty(ref _fVonUngDaCap, value);
                OnPropertyChanged(nameof(FGiaTriConPhaiUng));
            }
        }

        private double? _fVonUngDaThuHoi;
        public double? FVonUngDaThuHoi
        {
            get => _fVonUngDaThuHoi;
            set => SetProperty(ref _fVonUngDaThuHoi, value);
        }

        private double? _fGiaTriConPhaiUng;
        public double? FGiaTriConPhaiUng
        {
            get => (_fKhvonUng.HasValue ? _fKhvonUng.Value : 0) - (_fVonUngDaCap.HasValue ? _fVonUngDaCap.Value : 0);
            set => SetProperty(ref _fGiaTriConPhaiUng, value);
        }

        private Guid? _iIdDonViTienTeId;
        public Guid? IIdDonViTienTeId
        {
            get => _iIdDonViTienTeId;
            set => SetProperty(ref _iIdDonViTienTeId, value);
        }

        private double? _fTiGiaDonVi;
        public double? FTiGiaDonVi
        {
            get => _fTiGiaDonVi;
            set => SetProperty(ref _fTiGiaDonVi, value);
        }

        private Guid? _iIdTienTeId;
        public Guid? IIdTienTeId
        {
            get => _iIdTienTeId;
            set => SetProperty(ref _iIdTienTeId, value);
        }

        private double? _fTiGia;
        public double? FTiGia
        {
            get => _fTiGia;
            set => SetProperty(ref _fTiGia, value);
        }

        private string _iIdMaDonVi;
        public string IIdMaDonVi
        {
            get => _iIdMaDonVi;
            set => SetProperty(ref _iIdMaDonVi, value);
        }
    }
}
