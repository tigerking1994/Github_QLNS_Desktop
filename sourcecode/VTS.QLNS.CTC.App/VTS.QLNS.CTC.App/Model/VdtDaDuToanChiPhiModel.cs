using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtDaDuToanChiPhiModel: DetailModelBase
    {
        private string _tenChiPhi;
        public string TenChiPhi
        {
            get => _tenChiPhi;
            set => SetProperty(ref _tenChiPhi, value);
        }

        private Guid _idDuToanChiPhi;
        public Guid IdDuToanChiPhi
        {
            get => _idDuToanChiPhi;
            set => SetProperty(ref _idDuToanChiPhi, value);
        }

        private Guid? _idChiPhi;
        public Guid? IdChiPhi
        {
            get => _idChiPhi;
            set => SetProperty(ref _idChiPhi, value);
        }

        private double _giaTriPheDuyet;
        public double GiaTriPheDuyet
        {
            get => _giaTriPheDuyet;
            set => SetProperty(ref _giaTriPheDuyet, value);
        }

        private Guid? _idDuToan;
        public Guid? IdDuToan
        {
            get => _idDuToan;
            set => SetProperty(ref _idDuToan, value);
        }

        private Guid? _idChiPhiDuAn;
        public Guid? IdChiPhiDuAn
        {
            get => _idChiPhiDuAn;
            set => SetProperty(ref _idChiPhiDuAn, value);
        }

        private int _iThuTu;
        public int IThuTu
        {
            get => _iThuTu;
            set => SetProperty(ref _iThuTu, value);
        }

        private Guid? _idChiPhiDuAnParent;
        public Guid? IdChiPhiDuAnParent
        {
            get => _idChiPhiDuAnParent;
            set => SetProperty(ref _idChiPhiDuAnParent, value);
        }

        private bool _isLoaiChiPhi;
        public bool IsLoaiChiPhi
        {
            get => _isLoaiChiPhi;
            set => SetProperty(ref _isLoaiChiPhi, value);
        }

        private bool _isDuAnChiPhiOld;
        public bool IsDuAnChiPhiOld
        {
            get => _isDuAnChiPhiOld;
            set => SetProperty(ref _isDuAnChiPhiOld, value);
        }

        private bool _isEditHangMuc;
        public bool IsEditHangMuc
        {
            get => _isEditHangMuc;
            set => SetProperty(ref _isEditHangMuc, value);
        }

        private double _fGiaTriDieuChinh;
        public double FGiaTriDieuChinh
        {
            get => _fGiaTriDieuChinh;
            set => SetProperty(ref _fGiaTriDieuChinh, value);
        }

        private double _giaTriTruocDieuChinh;
        public double GiaTriTruocDieuChinh
        {
            get => _giaTriTruocDieuChinh;
            set => SetProperty(ref _giaTriTruocDieuChinh, value);
        }

        public double? FTienPheDuyetQDDT { get; set; }

        private bool _isEditGiaTriChiPhi;
        public bool IsEditGiaTriChiPhi
        {
            get => _isEditGiaTriChiPhi;
            set => SetProperty(ref _isEditGiaTriChiPhi, value);
        }

        private string _sMaOrder;
        public string SMaOrder
        {
            get => _sMaOrder;
            set => SetProperty(ref _sMaOrder, value);
        }
    }
}
