using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtKhvPhanBoVonChiPhiChiTietModel : ModelBase
    {
        private string _sNoiDung;
        public string SNoiDung
        {
            get => _sNoiDung;
            set => SetProperty(ref _sNoiDung, value);
        }
        private Guid? _iIdPhanBoVonChiPhiId;
        public Guid? IIdPhanBoVonChiPhiId
        {
            get => _iIdPhanBoVonChiPhiId;
            set => SetProperty(ref _iIdPhanBoVonChiPhiId, value);
        }

        private string _sTrangThaiDuAnDangKy;
        public string STrangThaiDuAnDangKy
        {
            get => _sTrangThaiDuAnDangKy;
            set => SetProperty(ref _sTrangThaiDuAnDangKy, value);
        }

        private string _sGhiChu;
        public string SGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
        }

        private double? _fGiaTriPheDuyet;
        public double? FGiaTriPheDuyet
        {
            get => _fGiaTriPheDuyet;
            set => SetProperty(ref _fGiaTriPheDuyet, value);
        }

        private Guid? _iIdLoaiCongTrinh;
        public Guid? IIdLoaiCongTrinh
        {
            get => _iIdLoaiCongTrinh;
            set => SetProperty(ref _iIdLoaiCongTrinh, value);
        }

        private Guid? _iIdParentId;
        public Guid? IIdParentId
        {
            get => _iIdParentId;
            set => SetProperty(ref _iIdParentId, value);
        }

        private Guid? _iIdParent;
        public Guid? IIdParent
        {
            get => _iIdParent;
            set => SetProperty(ref _iIdParent, value);
        }

        private Guid? _iIdDanhMucDtChi;
        public Guid? IIdDanhMucDtChi
        {
            get => _iIdDanhMucDtChi;
            set => SetProperty(ref _iIdDanhMucDtChi, value);
        }

        private double? _fGiaTriPheDuyetDC;
        public double? FGiaTriPheDuyetDC
        {
            get => _fGiaTriPheDuyetDC;
            set => SetProperty(ref _fGiaTriPheDuyetDC, value);
        }

        private int? _iLoaiDuAn;
        public int? ILoaiDuAn
        {
            get => _iLoaiDuAn;
            set => SetProperty(ref _iLoaiDuAn, value);
        }

        private string _sMaChiPhi;
        public string SMaChiPhi
        {
            get => _sMaChiPhi;
            set => SetProperty(ref _sMaChiPhi, value);
        }

        private string _sMaOrder;
        public string SMaOrder
        {
            get => _sMaOrder;
            set => SetProperty(ref _sMaOrder, value);
        }
    }
}
