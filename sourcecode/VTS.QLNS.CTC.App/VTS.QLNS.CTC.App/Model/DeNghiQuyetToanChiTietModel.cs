using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class DeNghiQuyetToanChiTietModel : DetailModelBase
    {
        private Guid _chiPhiId;
        public Guid ChiPhiId
        {
            get => _chiPhiId;
            set => SetProperty(ref _chiPhiId, value);
        }

        private Guid _hangMucId;
        public Guid HangMucId
        {
            get => _hangMucId;
            set => SetProperty(ref _hangMucId, value);
        }

        private Guid _goiThauId;
        public Guid GoiThauId
        {
            get => _goiThauId;
            set => SetProperty(ref _goiThauId, value);
        }

        private Guid _idHangMucParent;
        public Guid IdHangMucParent
        {
            get => _idHangMucParent;
            set => SetProperty(ref _idHangMucParent, value);
        }

        private string _maOrder;
        public string MaOrDer
        {
            get => _maOrder;
            set => SetProperty(ref _maOrder, value);
        }

        private int _phanCap;
        public int PhanCap
        {
            get => _phanCap;
            set => SetProperty(ref _phanCap, value);
        }

        private double? _giaTriPheDuyet;
        public double? GiaTriPheDuyet
        {
            get => _giaTriPheDuyet;
            set
            {
                SetProperty(ref _giaTriPheDuyet, value);
                OnPropertyChanged(nameof(SoVoiDuToan));
            }
        }

        private double? _giaTriPheDuyetQDDT;
        public double? GiaTriPheDuyetQDDT
        {
            get => _giaTriPheDuyetQDDT;
            set
            {
                SetProperty(ref _giaTriPheDuyetQDDT, value);
            }
        }

        private string _tenChiPhi;
        public string TenChiPhi
        {
            get => _tenChiPhi;
            set => SetProperty(ref _tenChiPhi, value);
        }

        private Guid _idChiPhiDuAnParent;
        public Guid IdChiPhiDuAnParent
        {
            get => _idChiPhiDuAnParent;
            set => SetProperty(ref _idChiPhiDuAnParent, value);
        }

        private bool _isShow;
        public bool IsShow
        {
            get => _isShow;
            set => SetProperty(ref _isShow, value);
        }

        public string Stt { get; set; }

        private double _fGiaTriKiemToan;
        public double FGiaTriKiemToan
        {
            get => _fGiaTriKiemToan;
            set
            {
                SetProperty(ref _fGiaTriKiemToan, value);
                OnPropertyChanged(nameof(SoVoiKetQuaKiemToan));
            }
        }

        private double _fGiaTriDeNghiQuyetToan;
        public double FGiaTriDeNghiQuyetToan
        {
            get => _fGiaTriDeNghiQuyetToan;
            set
            {
                SetProperty(ref _fGiaTriDeNghiQuyetToan, value);
                OnPropertyChanged(nameof(SoVoiDuToan));
                OnPropertyChanged(nameof(SoVoiQuyetToan));
                OnPropertyChanged(nameof(SoVoiKetQuaKiemToan));
            }
        }

        private double _fGiaTriAB;
        public double FGiaTriAB
        {
            get => _fGiaTriAB;
            set
            {
                SetProperty(ref _fGiaTriAB, value);
                OnPropertyChanged(nameof(SoVoiQuyetToan));
            }
        }

        public double SoVoiDuToan => FGiaTriDeNghiQuyetToan - (GiaTriPheDuyet.HasValue ? GiaTriPheDuyet.Value : 0);
        public double SoVoiQuyetToan => FGiaTriDeNghiQuyetToan - FGiaTriAB;
        public double SoVoiKetQuaKiemToan => FGiaTriDeNghiQuyetToan - _fGiaTriKiemToan;

        private bool _isChiPhi;
        public bool IsChiPhi
        {
            get => _isChiPhi;
            set => SetProperty(ref _isChiPhi, value);
        }

        private bool _isGoiThau;
        public bool IsGoiThau
        {
            get => _isGoiThau;
            set => SetProperty(ref _isGoiThau, value);
        }

        public string TenLoai => IsChiPhi ? "Chi phí" : (IsGoiThau ? "Gói thầu" : "Hạng mục");

        private List<DeNghiQuyetToanChiTietModel> _listHangMuc;
        public List<DeNghiQuyetToanChiTietModel> ListHangMuc
        {
            get => _listHangMuc;
            set => SetProperty(ref _listHangMuc, value);
        }

        public string MaOrderDb { get; set; }

        public Guid? ChiPhiIdParentOfHangMuc { get; set; }

        private string _maChiPhi;
        public string MaChiPhi
        {
            get => _maChiPhi;
            set => SetProperty(ref _maChiPhi, value);
        }

        private string _maHangMuc;
        public string MaHangMuc
        {
            get => _maHangMuc;
            set => SetProperty(ref _maHangMuc, value);
        }
    }
}
