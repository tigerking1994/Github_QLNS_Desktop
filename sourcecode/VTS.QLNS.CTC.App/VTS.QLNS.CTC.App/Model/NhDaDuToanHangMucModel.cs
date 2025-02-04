using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDaDuToanHangMucModel : DetailModelBase
    {
        public Guid IIdDuToanChiPhiId { get; set; }
        public Guid? IIdQdDauTuHangMucId { get; set; }
        public Guid? IIdHangMucPhanChiaId { get; set; }

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

        private string _sMaHangMuc;
        public string SMaHangMuc
        {
            get => _sMaHangMuc;
            set => SetProperty(ref _sMaHangMuc, value);
        }

        private string _sTenHangMuc;
        public string STenHangMuc
        {
            get => _sTenHangMuc;
            set => SetProperty(ref _sTenHangMuc, value);
        }

        private string _sMaOrder;
        public string SMaOrder
        {
            get => _sMaOrder;
            set => SetProperty(ref _sMaOrder, value);
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

        private Guid? _iIdLoaiCongTrinhId;
        public Guid? IIdLoaiCongTrinhId
        {
            get => _iIdLoaiCongTrinhId;
            set => SetProperty(ref _iIdLoaiCongTrinhId, value);
        }

        // Another properties
        public override bool IsHangCha => IIdParentId.IsNullOrEmpty();
        public double? FGiaTriQdDauTuNgoaiTeKhac { get; set; }
        public double? FGiaTriQdDauTuUsd { get; set; }
        public double? FGiaTriQdDauTuVnd { get; set; }
        public double? FGiaTriQdDauTuEur { get; set; }
    }
}
