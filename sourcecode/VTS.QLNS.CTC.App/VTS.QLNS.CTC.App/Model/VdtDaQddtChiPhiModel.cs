using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtDaQddtChiPhiModel : DetailModelBase
    {
        private string _tenChiPhi;
        public string TenChiPhi
        {
            get => _tenChiPhi;
            set => SetProperty(ref _tenChiPhi, value);
        }

        private Guid? _idQDChiPhi;
        public Guid? IdQDChiPhi
        {
            get => _idQDChiPhi;
            set => SetProperty(ref _idQDChiPhi, value);
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

        private Guid? _idQDDauTu;
        public Guid? IdQDDauTu
        {
            get => _idQDDauTu;
            set => SetProperty(ref _idQDDauTu, value);
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

        private bool _isEditHangMuc;
        public bool IsEditHangMuc 
        {
            get => _isEditHangMuc;
            set => SetProperty(ref _isEditHangMuc, value);
        }

        private double _giaTriDieuChinh;
        public double GiaTriDieuChinh
        {
            get => _giaTriDieuChinh;
            set => SetProperty(ref _giaTriDieuChinh, value);
        }

        private double _giaTriTruocDieuChinh;
        public double GiaTriTruocDieuChinh
        {
            get => _giaTriTruocDieuChinh;
            set => SetProperty(ref _giaTriTruocDieuChinh, value);
        }

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


        private string _sMaChiPhi;
        public string SMaChiPhi
        {
            get => _sMaChiPhi;
            set => SetProperty(ref _sMaChiPhi, value);
        }
    }
}
