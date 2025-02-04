using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class KHLCNhaThauModel : ModelBase
    {
        public int? IRowIndex { get; set; }

        public string ILoaiCanCu
        {
            get
            {
                return IIdQdDauTuId.HasValue ? LoaiKHLCNTTypeName.QDDT : (IIdChuTruongDauTuId.HasValue ? LoaiKHLCNTType.CHU_TRUONG_DAU_TU :LoaiKHLCNTType.DU_TOAN);
            }
        }

        private Guid? _iIdDuToanId;
        public Guid? IIdDuToanId
        {
            get => _iIdDuToanId;
            set => SetProperty(ref _iIdDuToanId, value);
        }

        private Guid? _iIdQdDauTuId;
        public Guid? IIdQdDauTuId
        {
            get => _iIdQdDauTuId;
            set => SetProperty(ref _iIdQdDauTuId, value);
        }

        private Guid? _iIdChuTruongDauTuId;
        public Guid? IIdChuTruongDauTuId
        {
            get => _iIdChuTruongDauTuId;
            set => SetProperty(ref _iIdChuTruongDauTuId, value);
        }

        private string _sSoQuyetDinh;
        public string SSoQuyetDinh
        {
            get => _sSoQuyetDinh;
            set => SetProperty(ref _sSoQuyetDinh, value);
        }

        private DateTime? _dNgayQuyetDinh;
        public DateTime? DNgayQuyetDinh
        {
            get => _dNgayQuyetDinh;
            set => SetProperty(ref _dNgayQuyetDinh, value);
        }

        private Guid? _iIdDonViQuanLyId;
        public Guid? IIdDonViQuanLyId
        {
            get => _iIdDonViQuanLyId;
            set => SetProperty(ref _iIdDonViQuanLyId, value);
        }

        public string STenDonVi { get; set; }

        private string _iIdMaDonViQuanLy;
        public string IIdMaDonViQuanLy 
        {
            get => _iIdMaDonViQuanLy;
            set => SetProperty(ref _iIdMaDonViQuanLy, value);
        }

        private Guid? _iIdDuAnId;
        public Guid? IIdDuAnId
        {
            get => _iIdDuAnId;
            set => SetProperty(ref _iIdDuAnId, value);
        }

        public string STenDuAn { get; set; }

        private string _sMoTa;
        public string SMoTa
        {
            get => _sMoTa;
            set => SetProperty(ref _sMoTa, value);
        }

        private string _sUserCreate;
        public string SUserCreate
        {
            get => _sUserCreate;
            set => SetProperty(ref _sUserCreate, value);
        }

        private bool _bActive;
        public bool BActive
        {
            get => _bActive;
            set => SetProperty(ref _bActive, value);
        }

        private Guid? _iIdParentId;
        public Guid? IIdParentId
        {
            get => _iIdParentId;
            set => SetProperty(ref _iIdParentId, value);
        }

        private Guid? _iIdLcNhaThauGocId;
        public Guid? IIdLcNhaThauGocId
        {
            get => _iIdLcNhaThauGocId;
            set => SetProperty(ref _iIdLcNhaThauGocId, value);
        }

        private bool _bKhoa;
        public bool BKhoa
        {
            get => _bKhoa;
            set => SetProperty(ref _bKhoa, value);
        }

        public int ISoLanDieuChinh { get; set; }
        public int ITotalFiles { get; set; }
        public bool BIsGoc { get; set; }
    }
}
