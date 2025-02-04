using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Utility.Enum;
using static VTS.QLNS.CTC.App.Model.NsSktChungTuChiTietModel;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ chi tiết", 5, 1)]
    public class DemandVoucherDetailImportModelNSBD : BindableBase
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

        private string _x1SoTienMHHV;
        [ColumnAttribute(5)]
        public string X1SoTienMHHV
        {
            get => _x1SoTienMHHV;
            set => SetProperty(ref _x1SoTienMHHV, value);
        }

        private string _x1SoTienDT;
        [ColumnAttribute(6)]
        public string X1SoTienDT
        {
            get => _x1SoTienDT;
            set => SetProperty(ref _x1SoTienDT, value);
        }

        private string _x2SoTienMHHV;
        [ColumnAttribute(7)]
        public string X2SoTienMHHV
        {
            get => _x2SoTienMHHV;
            set => SetProperty(ref _x2SoTienMHHV, value);
        }

        private string _x2SoTienDT;
        [ColumnAttribute(8)]
        public string X2SoTienDT
        {
            get => _x2SoTienDT;
            set => SetProperty(ref _x2SoTienDT, value);
        }

        private string _x3SoTienMHHV;
        [ColumnAttribute(9)]
        public string X3SoTienMHHV
        {
            get => _x3SoTienMHHV;
            set => SetProperty(ref _x3SoTienMHHV, value);
        }

        private string _x3SoTienDT;
        [ColumnAttribute(10)]
        public string X3SoTienDT
        {
            get => _x3SoTienDT;
            set => SetProperty(ref _x3SoTienDT, value);
        }

        private string _x4SoTienMHHV;
        [ColumnAttribute(11)]
        public string X4SoTienMHHV
        {
            get => _x4SoTienMHHV;
            set => SetProperty(ref _x4SoTienMHHV, value);
        }

        private string _x4SoTienDT;
        [ColumnAttribute(12)]
        public string X4SoTienDT
        {
            get => _x4SoTienDT;
            set => SetProperty(ref _x4SoTienDT, value);
        }

        private string _x5SoTienMHHV;
        [ColumnAttribute(13)]
        public string X5SoTienMHHV
        {
            get => _x5SoTienMHHV;
            set => SetProperty(ref _x5SoTienMHHV, value);
        }

        private string _x5SoTienDT;
        [ColumnAttribute(14)]
        public string X5SoTienDT
        {
            get => _x5SoTienDT;
            set => SetProperty(ref _x5SoTienDT, value);
        }


        private string _tonKhoDenNgay;
        [ColumnAttribute("Tồn kho đến ngày", 15, ValidateType.IsNumber)]
        public string TonKhoDenNgay
        {
            get => _tonKhoDenNgay;
            set => SetProperty(ref _tonKhoDenNgay, value);
        }

        private string _huyDong;
        [ColumnAttribute("Huy động tồn kho", 16, ValidateType.IsNumber)]
        public string HuyDong
        {
            get => _huyDong;
            set => SetProperty(ref _huyDong, value);
        }



        private string _tuChi;
        //[ColumnAttribute("Tự chi", 15, ValidateType.IsNumber)]
        public string TuChi
        {
            get => _tuChi;
            set => SetProperty(ref _tuChi, value);
        }

        private string _muaHangHienVat;
        [ColumnAttribute("Mua hàng cấp hiện vật", 17, ValidateType.IsNumber)]
        public string MuaHangHienVat
        {
            get => _muaHangHienVat;
            set => SetProperty(ref _muaHangHienVat, value);
        }

        private string _khungNganSachDuocDuyet;
        [ColumnAttribute("Khung ngân sách được duyệt", 18, ValidateType.IsNumber)]
        public string KhungNganSachDuocDuyet
        {
            get => _khungNganSachDuocDuyet;
            set => SetProperty(ref _khungNganSachDuocDuyet, value);
        }

        private string _soNganhPhanCap;
        [ColumnAttribute("Số ngành đã phân cấp theo khung ngân sách", 19, ValidateType.IsNumber)]
        public string SoNganhPhanCap
        {
            get => _soNganhPhanCap;
            set => SetProperty(ref _soNganhPhanCap, value);
        }

        private string _dacThu;
        [ColumnAttribute("Đặc thù", 20, ValidateType.IsNumber)]
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
