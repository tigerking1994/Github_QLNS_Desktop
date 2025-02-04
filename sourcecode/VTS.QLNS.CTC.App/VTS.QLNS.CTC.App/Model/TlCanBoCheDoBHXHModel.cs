using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlCanBoCheDoBHXHModel : ModelBase
    {
        private string _sMaCanBo;
        public string SMaCanBo
        {
            get => _sMaCanBo;
            set => SetProperty(ref _sMaCanBo, value);
        }

        private string _sMaCheDo;
        public string SMaCheDo
        {
            get => _sMaCheDo;
            set => SetProperty(ref _sMaCheDo, value);
        }

        private string _sMaCheDoCha;
        public string SMaCheDoCha
        {
            get => _sMaCheDoCha;
            set => SetProperty(ref _sMaCheDoCha, value);
        }

        private string _sTenCheDo;
        public string STenCheDo
        {
            get => _sTenCheDo;
            set => SetProperty(ref _sTenCheDo, value);
        }

        private DateTime? _dTuNgay;
        public DateTime? DTuNgay
        {
            get => _dTuNgay;
            set
            {
                SetProperty(ref _dTuNgay, value);
                OnPropertyChanged(nameof(ISoNgayNghi));
            }
        }

        private DateTime? _dDenNgay;
        public DateTime? DDenNgay
        {
            get => _dDenNgay;
            set
            {
                SetProperty(ref _dDenNgay, value);
                OnPropertyChanged(nameof(ISoNgayNghi));
            }
        }

        private int? _iSoNgayNghi;
        public int? ISoNgayNghi
        {
            //get
            //{
            //    if (DTuNgay != null && DDenNgay != null)
            //    {
            //        return _iSoNgayNghi = (DDenNgay.GetValueOrDefault() - DTuNgay.GetValueOrDefault()).Days + 1;
            //    }
            //    return _iSoNgayNghi;
            //}
            get => _iSoNgayNghi.GetValueOrDefault();
            set
            {
                SetProperty(ref _iSoNgayNghi, value);
            }
        }

        private double? _fSoNgayHuongBHXH;
        public double? FSoNgayHuongBHXH
        {
            get => _fSoNgayHuongBHXH.GetValueOrDefault();
            set
            {
                SetProperty(ref _fSoNgayHuongBHXH, value);
            }
        }

        private double? _fSoNgayNghiPhep;
        public double? FSoNgayNghiPhep
        {
            get => _fSoNgayNghiPhep.GetValueOrDefault();
            set
            {
                SetProperty(ref _fSoNgayNghiPhep, value);
            }
        }

        private decimal? _fSoTien;
        public decimal? FSoTien
        {
            get => _fSoTien;
            set
            {
                SetProperty(ref _fSoTien, value);
            }
        }

        private decimal? _fGiaTriCanCu;
        public decimal? FGiaTriCanCu
        {
            get => _fGiaTriCanCu.GetValueOrDefault();
            set
            {
                SetProperty(ref _fGiaTriCanCu, value);
            }
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
            set
            {
                SetProperty(ref _dNgayQuyetDinh, value);
                OnPropertyChanged(nameof(ISoNgayNghi));
            }
        }

        private int? _dThangLuongCanCuDong;
        public int? IThangLuongCanCuDong
        {
            get => _dThangLuongCanCuDong;
            set
            {
                SetProperty(ref _dThangLuongCanCuDong, value);
                OnPropertyChanged(nameof(ISoNgayNghi));
            }
        }

        private int? _iThang;
        public int? IThang
        {
            get => _iThang;
            set
            {
                SetProperty(ref _iThang, value);
            }
        }

        private int? _iNam;
        public int? INam
        {
            get => _iNam;
            set
            {
                SetProperty(ref _iNam, value);
            }
        }

        private bool _isDisplay;
        public bool IsDisplay
        {
            get => _isDisplay;
            set
            {
                SetProperty(ref _isDisplay, value);
            }
        }

        private bool _bhangCha;
        public bool BHangCha
        {
            get => _bhangCha;
            set
            {
                SetProperty(ref _bhangCha, value);
            }
        }

        private int? _iNamCanCuDong;
        public int? INamCanCuDong
        {
            get => _iNamCanCuDong;
            set
            {
                SetProperty(ref _iNamCanCuDong, value);
            }
        }

        public string STenCheDoCha { get; set; }

        public bool? BTinhCN { get; set; }
        public bool? BTinhNgayLe { get; set; }
        public bool? BTinhT7 { get; set; }

        public bool IsDelete => FSoNgayHuongBHXH.GetValueOrDefault() == 0 && (ISoNgayNghi.GetValueOrDefault() == 0 || ISoNgayNghi == null)
            && DTuNgay?.ToString("dd/MM/yyyy") == null && DDenNgay?.ToString("dd/MM/yyyy") == null && DNgayQuyetDinh?.ToString("dd/MM/yyyy") == null
            && SSoQuyetDinh == null && (FSoTien.GetValueOrDefault() == 0 || FSoTien == null) && (FGiaTriCanCu.GetValueOrDefault() == 0 || FGiaTriCanCu == null);
    }
}
