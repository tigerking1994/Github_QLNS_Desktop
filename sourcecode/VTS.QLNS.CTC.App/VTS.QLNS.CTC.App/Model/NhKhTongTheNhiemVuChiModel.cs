using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhKhTongTheNhiemVuChiModel : ModelBase
    {
        public Guid? IIdKhTongTheId { get; set; }

        private Guid? _iIdNhiemVuChiId;
        public Guid? IIdNhiemVuChiId
        {
            get => _iIdNhiemVuChiId;
            set => SetProperty(ref _iIdNhiemVuChiId, value);
        }

        private Guid? _iIdDonViThuHuongId;
        public Guid? IIdDonViThuHuongId
        {
            get => _iIdDonViThuHuongId;
            set => SetProperty(ref _iIdDonViThuHuongId, value);
        }
        public string SMaDonViThuHuong { get; set; }

        public string SMaOrder { get; set; }

        private double? _fGiaTriKhTtcp;
        public double? FGiaTriKhTtcp
        {
            get => _fGiaTriKhTtcp;
            set => SetProperty(ref _fGiaTriKhTtcp, value);
        }

        private double? _fGiaTriKhBqp;
        public double? FGiaTriKhBqp
        {
            get => _fGiaTriKhBqp;
            set => SetProperty(ref _fGiaTriKhBqp, value);
        }

        private double? _fGiaTriKhBqpVnd;
        public double? FGiaTriKhBqpVnd
        {
            get => _fGiaTriKhBqpVnd;
            set => SetProperty(ref _fGiaTriKhBqpVnd, value);
        }

        // Another properties

        private double? _fGiaTriKhBqpGiaiDoan;
        public double? FGiaTriKhBqpGiaiDoan
        {
            get => _fGiaTriKhBqpGiaiDoan;
            set => SetProperty(ref _fGiaTriKhBqpGiaiDoan, value);
        }

        private double? _fGiaTriKhTtcpGiaiDoan;
        public double? FGiaTriKhTtcpGiaiDoan
        {
            get => _fGiaTriKhTtcpGiaiDoan;
            set => SetProperty(ref _fGiaTriKhTtcpGiaiDoan, value);
        }

        private string _tenDonVi;
        public string TenDonVi
        {
            get => _tenDonVi;
            set => SetProperty(ref _tenDonVi, value);
        }

        private string _sMaNhiemVuChi;
        public string SMaNhiemVuChi
        {
            get => _sMaNhiemVuChi;
            set => SetProperty(ref _sMaNhiemVuChi, value);
        }

        private string _sTenNhiemVuChi;
        public string STenNhiemVuChi
        {
            get => _sTenNhiemVuChi;
            set => SetProperty(ref _sTenNhiemVuChi, value);
        }

        public int? ILoaiNhiemVuChi { get; set; }

        private Guid? _parentNhiemVuChiId;
        public Guid? ParentNhiemVuChiId
        {
            get => _parentNhiemVuChiId;
            set
            {
                SetProperty(ref _parentNhiemVuChiId, value);
                OnPropertyChanged(nameof(IsHangCha));
            }
        }

        public bool HasValue
        {
            get
            {
                //bool hasDonViThuHuongId = IIdDonViThuHuongId.HasValue;
                //bool hasNhiemVuChiId = IIdNhiemVuChiId.HasValue;
                bool hasValueTtcp = FGiaTriKhTtcp.HasValue && FGiaTriKhTtcp.Value != 0;
                bool hasValueBqp = FGiaTriKhBqp.HasValue && FGiaTriKhBqp.Value != 0;
                return hasValueTtcp || hasValueBqp;
            }
        }
    }
}