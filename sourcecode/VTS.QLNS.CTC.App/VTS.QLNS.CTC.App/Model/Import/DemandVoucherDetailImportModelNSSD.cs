using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Utility.Enum;
using static VTS.QLNS.CTC.App.Model.NsSktChungTuChiTietModel;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ chi tiết", 5, 1)]
    public class DemandVoucherDetailImportModelNSSD : BindableBase
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

        public List<string> ListKyHieuChild { get; set; }
        public string KyHieuParent { get; set; }

        private string _kyHieu;
        [ColumnAttribute("Ký hiệu", 0, ValidateType.KyHieu)]
        public string KyHieu
        {
            get => _kyHieu;
            set => SetProperty(ref _kyHieu, value);
        }

        private string _stt;
        [ColumnAttribute("STT", 1)]
        public string STT
        {
            get => _stt;
            set => SetProperty(ref _stt, value);
        }

        private string _nganh;
        //[ColumnAttribute("Ngành", 2)]
        public string Nganh
        {
            get => _nganh;
            set => SetProperty(ref _nganh, value);
        }

        private string _nganhCha;
        //[ColumnAttribute("Ngành cha", 3)]
        public string NganhCha
        {
            get => _nganhCha;
            set => SetProperty(ref _nganhCha, value);
        }

        private string _description;
        //[ColumnAttribute("Mô tả", 4)]
        [ColumnAttribute("Mô tả", 2)]
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private string _x1SoTien;
        [ColumnAttribute(3)]
        public string X1SoTien
        {
            get => _x1SoTien;
            set => SetProperty(ref _x1SoTien, value);
        }

        private string _x2SoTien;
        [ColumnAttribute(4)]
        public string X2SoTien
        {
            get => _x2SoTien;
            set => SetProperty(ref _x2SoTien, value);
        }

        private string _x3SoTien;
        [ColumnAttribute(5)]
        public string X3SoTien
        {
            get => _x3SoTien;
            set => SetProperty(ref _x3SoTien, value);
        }

        private string _x4SoTien;
        [ColumnAttribute(6)]
        public string X4SoTien
        {
            get => _x4SoTien;
            set => SetProperty(ref _x4SoTien, value);
        }

        private string _x5SoTien;
        [ColumnAttribute(7)]
        public string X5SoTien
        {
            get => _x5SoTien;
            set => SetProperty(ref _x5SoTien, value);
        }

        private string _huyDong;
        [ColumnAttribute("Huy động tồn kho", 8, ValidateType.IsNumber)]
        public string HuyDong
        {
            get => _huyDong;
            set => SetProperty(ref _huyDong, value);
        }

        private string _tonKhoDenNgay;
        [ColumnAttribute("Tồn kho đến ngày", 9, ValidateType.IsNumber)]
        public string TonkhoDenNgay
        {
            get => _tonKhoDenNgay;
            set => SetProperty(ref _tonKhoDenNgay, value);
        }

        private string _tuChi;
        [ColumnAttribute("Tự chi", 10, ValidateType.IsNumber)]
        public string TuChi
        {
            get => _tuChi;
            set => SetProperty(ref _tuChi, value);
        }

        private string _muaHangHienVat;
        [ColumnAttribute("Mua hàng cấp hiện vật", 11, ValidateType.IsNumber)]
        public string MuaHangHienVat
        {
            get => _muaHangHienVat;
            set => SetProperty(ref _muaHangHienVat, value);
        }

        private string _dacThu;
        [ColumnAttribute("Đặc thù", 12, ValidateType.IsNumber)]
        public string DacThu
        {
            get => _dacThu;
            set => SetProperty(ref _dacThu, value);
        }

        private ChiTietCanCu _x1;
        public ChiTietCanCu X1
        {
            set
            {
                SetProperty(ref _x1, value);
            }
            get
            {
                if (_x1 == null)
                {
                    _x1 = new ChiTietCanCu();
                }

                return _x1;
            }
        }

        private ChiTietCanCu _x2;
        public ChiTietCanCu X2
        {
            set
            {
                SetProperty(ref _x2, value);
            }
            get
            {
                if (_x2 == null)
                {
                    _x2 = new ChiTietCanCu();
                }

                return _x2;
            }
        }
        private ChiTietCanCu _x3;
        public ChiTietCanCu X3
        {
            set
            {
                SetProperty(ref _x3, value);
            }
            get
            {
                if (_x3 == null)
                {
                    _x3 = new ChiTietCanCu();
                }

                return _x3;
            }
        }
        private ChiTietCanCu _x4;
        public ChiTietCanCu X4
        {
            set
            {
                SetProperty(ref _x4, value);
            }
            get
            {
                if (_x4 == null)
                {
                    _x4 = new ChiTietCanCu();
                }

                return _x4;
            }
        }
        private ChiTietCanCu _x5;
        public ChiTietCanCu X5
        {
            set
            {
                SetProperty(ref _x5, value);
            }
            get
            {
                if (_x5 == null)
                {
                    _x5 = new ChiTietCanCu();
                }

                return _x5;
            }
        }
    }
}
