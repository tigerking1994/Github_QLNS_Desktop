using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model
{
    public class ApproveProjectDetailModel : DetailModelBase
    {
        private Guid? _idQDHangMuc;
        public Guid? IdQDHangMuc
        {
            get => _idQDHangMuc;
            set => SetProperty(ref _idQDHangMuc, value);
        }

        private string _maHangMuc;
        public string MaHangMuc
        {
            get => _maHangMuc;
            set => SetProperty(ref _maHangMuc, value);
        }

        private string _tenHangMuc;
        public string TenHangMuc
        {
            get => _tenHangMuc;
            set => SetProperty(ref _tenHangMuc, value);
        }

        private Guid? _idChiPhi;
        public Guid? IdChiPhi
        {
            get => _idChiPhi;
            set => SetProperty(ref _idChiPhi, value);
        }

        private Guid? _idDuAnChiPhi;
        public Guid? IdDuAnChiPhi
        {
            get => _idDuAnChiPhi;
            set => SetProperty(ref _idDuAnChiPhi, value);
        }
       
        private Guid? _idDuAnHangMuc;
        public Guid? IdDuAnHangMuc
        {
            get => _idDuAnHangMuc;
            set => SetProperty(ref _idDuAnHangMuc, value);
        }

        private double? _giaTriPheDuyet;
        public double? GiaTriPheDuyet
        {
            get => _giaTriPheDuyet;
            set => SetProperty(ref _giaTriPheDuyet, value);
        }

        private Guid _iIdQddauTuId;
        public Guid IIdQddauTuId
        {
            get => _iIdQddauTuId;
            set => SetProperty(ref _iIdQddauTuId, value);
        }

        private Guid? _iIdDuAnId;
        public Guid? IIdDuAnId
        {
            get => _iIdDuAnId;
            set => SetProperty(ref _iIdDuAnId, value);
        }

        private Guid? _hangMucParentId;
        public Guid? HangMucParentId
        {
            get => _hangMucParentId;
            set => SetProperty(ref _hangMucParentId, value);
        }

        private Guid? _idLoaiCongTrinh;
        public Guid? IdLoaiCongTrinh
        {
            get => _idLoaiCongTrinh;
            set => SetProperty(ref _idLoaiCongTrinh, value);
        }

        public string MaOrDer { get; set; }

        private bool _isEditHangMuc;
        public bool IsEditHangMuc
        {
            get => _isEditHangMuc;
            set => SetProperty(ref _isEditHangMuc, value);
        }

        private bool _bIsEdit;
        public bool BIsEdit
        {
            get => _bIsEdit;
            set => SetProperty(ref _bIsEdit, value);
        }

        public string TenChiPhi { get; set; }
        public string TenLoaiCT { get; set; }

        private double? _giaTriDieuChinh;
        public double? GiaTriDieuChinh
        {
            get => _giaTriDieuChinh;
            set => SetProperty(ref _giaTriDieuChinh, value);
        }

        private double? _giaTriTruocDieuChinh;
        public double? GiaTriTruocDieuChinh
        {
            get => _giaTriTruocDieuChinh;
            set => SetProperty(ref _giaTriTruocDieuChinh, value);
        }

        public int? ILanDieuChinh { get; set; }

    }
}
