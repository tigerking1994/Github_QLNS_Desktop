using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Trợ cấp giải thích", 7, 0)]
    public class QtcqTcGiaiThichDetailImportModel : BindableBase
    {
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        private bool _isErrorMLNS;
        public bool IsErrorMLNS
        {
            get => _isErrorMLNS;
            set => SetProperty(ref _isErrorMLNS, value);
        }

        private bool _isHangcha;
        public bool IsHangCha
        {
            get => _isHangcha;
            set => SetProperty(ref _isHangcha, value);
        }

        private bool _isError;
        public bool IsError
        {
            get => _isError;
            set => SetProperty(ref _isError, value);
        }

        private bool _isWarning;
        public bool IsWarning
        {
            get => _isWarning;
            set => SetProperty(ref _isWarning, value);
        }

        private bool _bHangcha;
        public bool BHangCha
        {
            get => _bHangcha;
            set => SetProperty(ref _bHangcha, value);
        }

        private string _sXauNoiMa;
        [ColumnAttribute("Xâu nối mã", 0)]
        public string SXauNoiMa
        {
            get => _sXauNoiMa;
            set
            {
                SetProperty(ref _sXauNoiMa, value);
            }
        }

        private string _sMaHieuCanBo;
        [ColumnAttribute("Mã hiệu cán bộ", 1)]
        public string SMaHieuCanBo
        {
            get => _sMaHieuCanBo;
            set
            {
                SetProperty(ref _sMaHieuCanBo, value);
            }
        }

        private string _sTenCanBo;
        [ColumnAttribute("Tên cán bộ", 2)]
        public string STenCanBo
        {
            get => _sTenCanBo;
            set
            {
                SetProperty(ref _sTenCanBo, value);
            }
        }

        private string _sMaCanBo;
        [ColumnAttribute("Mã cán bộ", 3)]
        public string SMaCanBo
        {
            get => _sMaCanBo;
            set
            {
                SetProperty(ref _sMaCanBo, value);
            }
        }


        private string _sMaCapBac;
        [ColumnAttribute("Mã cấp bậc", 4)]
        public string SMaCapBac
        {
            get => _sMaCapBac;
            set
            {
                SetProperty(ref _sMaCapBac, value);
            }
        }

        private string _sTenCapBac;
        [ColumnAttribute("Tên cấp bậc", 5)]
        public string STenCapBac
        {
            get => _sTenCapBac;
            set
            {
                SetProperty(ref _sTenCapBac, value);
            }
        }

        private string _sTenPhanHo;
        [ColumnAttribute("Tên phân hộ", 6)]
        public string STenPhanHo
        {
            get => _sTenPhanHo;
            set
            {
                SetProperty(ref _sTenPhanHo, value);
            }
        }

        private string _iSoNgayHuong;
        [ColumnAttribute("Số ngày hưởng chế độ BHXH", 7)]
        public string ISoNgayHuong
        {
            get => _iSoNgayHuong;
            set
            {
                SetProperty(ref _iSoNgayHuong, value);
            }
        }

        private string _sSoQuyetDinh;
        [ColumnAttribute("Số quyết định", 8)]
        public string SSoQuyetDinh
        {
            get => _sSoQuyetDinh;
            set
            {
                SetProperty(ref _sSoQuyetDinh, value);
            }
        }

        private string _dNgayQuyetDinh;
        [ColumnAttribute("Ngày quyết định", 9)]
        public string DNgayQuyetDinh
        {
            get => _dNgayQuyetDinh;
            set
            {
                SetProperty(ref _dNgayQuyetDinh, value);
            }
        }

        private string _dTuNgay;
        [ColumnAttribute("Từ ngày", 10)]
        public string DTuNgay
        {
            get => _dTuNgay;
            set
            {
                SetProperty(ref _dTuNgay, value);
            }
        }

        private string _dDenNgay;
        [ColumnAttribute("Đến ngày", 11)]
        public string DDenNgay
        {
            get => _dDenNgay;
            set
            {
                SetProperty(ref _dDenNgay, value);
            }
        }

        private string _fSoTien;
        [ColumnAttribute("Số tiền", 12, ValidateType.IsNumber)]
        public string FSoTien
        {
            get => _fSoTien;
            set => SetProperty(ref _fSoTien, value);
        }

        private string _fTienLuongThangDongBHXH;
        [ColumnAttribute("Tiền lương tháng đóng BHXH", 13, ValidateType.IsNumber)]
        public string FTienLuongThangDongBHXH
        {
            get => _fTienLuongThangDongBHXH;
            set => SetProperty(ref _fTienLuongThangDongBHXH, value);
        }

        private string _sMaCheDo;
        [ColumnAttribute("Số sổ BHXH", 14)]
        public string SMaCheDo
        {
            get => _sMaCheDo;
            set
            {
                SetProperty(ref _sMaCheDo, value);
            }
        }

        private string _iSoNgayTruyLinh;
        [ColumnAttribute("Số ngày truy lĩnh", 15, ValidateType.IsNumber)]
        public string ISoNgayTruyLinh
        {
            get => _iSoNgayTruyLinh;
            set
            {
                SetProperty(ref _iSoNgayTruyLinh, value);
            }
        }


        private string _fTienTruyLinh;
        [ColumnAttribute("Số tiền truy lĩnh", 16, ValidateType.IsNumber)]
        public string FTienTruyLinh
        {
            get => _fTienTruyLinh;
            set => SetProperty(ref _fTienTruyLinh, value);
        }
        private string _sSoSoBHXH;
        [ColumnAttribute("Số sổ BHXH", 17)]
        public string SSoSoBHXH
        {
            get => _sSoSoBHXH;
            set
            {
                SetProperty(ref _sSoSoBHXH, value);
            }
        }

        private string _sMaDonVi;
        [ColumnAttribute("Mã đơn vị", 18)]
        public string SMaDonVi
        {
            get => _sMaDonVi;
            set
            {
                SetProperty(ref _sMaDonVi, value);
            }
        }


        public bool IsHasData => SMaHieuCanBo != "" || SMaCanBo != "" || STenCanBo != "" || SMaCapBac != "" || STenCapBac != "" || ISoNgayHuong != ""
            || SSoQuyetDinh != "" || DNgayQuyetDinh != "" || FSoTien != "";
    }
}
