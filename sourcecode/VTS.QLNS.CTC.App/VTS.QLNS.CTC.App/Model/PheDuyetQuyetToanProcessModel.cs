using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class PheDuyetQuyetToanProcessModel : DetailModelBase
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

        public int Stt { get; set; }

        private double _fGiaTriKiemToan;
        public double FGiaTriKiemToan
        {
            get => _fGiaTriKiemToan;
            set
            {
                SetProperty(ref _fGiaTriKiemToan, value);
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
                OnPropertyChanged(nameof(SoVoiDeNghi));
            }
        }

        private double _fGiaTriAB;
        public double FGiaTriAB
        {
            get => _fGiaTriAB;
            set
            {
                SetProperty(ref _fGiaTriAB, value);
            }
        }

        private double _giaTriThamTra;
        public double GiaTriThamTra
        {
            get => _giaTriThamTra;
            set
            {
                SetProperty(ref _giaTriThamTra, value);
            }
        }

        private double _giaTriQuyetToan;
        public double GiaTriQuyetToan
        {
            get => _giaTriQuyetToan;
            set
            {
                SetProperty(ref _giaTriQuyetToan, value);
                OnPropertyChanged(nameof(SoVoiDuToan));
                OnPropertyChanged(nameof(SoVoiDeNghi));
            }
        }

        public double SoVoiDuToan => GiaTriQuyetToan - (GiaTriPheDuyet.HasValue ? GiaTriPheDuyet.Value : 0);
        public double SoVoiDeNghi => GiaTriQuyetToan - FGiaTriDeNghiQuyetToan;

        private bool _isChiPhi;
        public bool IsChiPhi
        {
            get => _isChiPhi;
            set => SetProperty(ref _isChiPhi, value);
        }

        public string TenLoai => IsChiPhi ? "Chi phí" : (_goiThauId != null && _goiThauId != Guid.Empty ? "Gói thầu" : "Hạng mục");

        private List<PheDuyetQuyetToanProcessModel> _listHangMuc;
        public List<PheDuyetQuyetToanProcessModel> ListHangMuc
        {
            get => _listHangMuc;
            set => SetProperty(ref _listHangMuc, value);
        }

        public string MaOrderDb { get; set; }

        public Guid? ChiPhiIdParentOfHangMuc { get; set; }
    }
}
