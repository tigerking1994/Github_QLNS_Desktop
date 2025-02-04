using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(2, "DSCanBoCheDoBHXH", 4, 0)]
    public class QtcqExplainImportModel : BindableBase
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

        private string _sMaCanBo;
        [ColumnAttribute("Mã cán bộ", 2)]
        public string SMaCanBo
        {
            get => _sMaCanBo;
            set
            {
                SetProperty(ref _sMaCanBo, value);
            }
        }

        private string _sTenCanBo;
        [ColumnAttribute("Tên cán bộ", 3)]
        public string STenCanBo
        {
            get => _sTenCanBo;
            set
            {
                SetProperty(ref _sTenCanBo, value);
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

        private string _sMaDonVi;
        [ColumnAttribute("Mã đơn vị", 6)]
        public string SMaDonVi
        {
            get => _sMaDonVi;
            set
            {
                SetProperty(ref _sMaDonVi, value);
            }
        }

        private string _sTenDonVi;
        [ColumnAttribute("Tên đơn vị", 7)]
        public string STenDonVi
        {
            get => _sTenDonVi;
            set
            {
                SetProperty(ref _sTenDonVi, value);
            }
        }

        private string _sMaCheDo;
        [ColumnAttribute("Mã chế độ BHXH", 8)]
        public string SMaCheDo
        {
            get => _sMaCheDo;
            set
            {
                SetProperty(ref _sMaCheDo, value);
            }
        }

        private string _iSoNgayHuong;
        [ColumnAttribute("Số ngày hưởng chế độ BHXH", 9)]
        public string ISoNgayHuong
        {
            get => _iSoNgayHuong;
            set
            {
                SetProperty(ref _iSoNgayHuong, value);
            }
        }

        private string _sSoQuyetDinh;
        [ColumnAttribute("Số quyết định", 10)]
        public string SSoQuyetDinh
        {
            get => _sSoQuyetDinh;
            set
            {
                SetProperty(ref _sSoQuyetDinh, value);
            }
        }

        private string _dNgayQuyetDinh;
        [ColumnAttribute("Ngày quyết định", 11)]
        public string DNgayQuyetDinh
        {
            get => _dNgayQuyetDinh;
            set
            {
                SetProperty(ref _dNgayQuyetDinh, value);
            }
        }

        private string _iThangLuongCanCu;
        [ColumnAttribute("Tháng lương căn cứ", 12)]
        public string IThangLuongCanCu
        {
            get => _iThangLuongCanCu;
            set
            {
                SetProperty(ref _iThangLuongCanCu, value);
            }
        }

        private string _fSoTien;
        [ColumnAttribute("Số tiền", 13, ValidateType.IsNumber)]
        public string FSoTien
        {
            get => _fSoTien;
            set => SetProperty(ref _fSoTien, value);
        }

        private string _fGiaTriCanCu;
        [ColumnAttribute("Giá trị căn cứ", 14, ValidateType.IsNumber)]
        public string FGiaTriCanCu
        {
            get => _fGiaTriCanCu;
            set => SetProperty(ref _fGiaTriCanCu, value);
        }

        public bool IsHasData => SMaHieuCanBo != "" || SMaCanBo != "" || STenCanBo != "" || SMaCapBac != "" || STenCapBac != "" || ISoNgayHuong != ""
            || SSoQuyetDinh != "" || DNgayQuyetDinh != "" || FSoTien != "" || FGiaTriCanCu != "";
    }
}
