using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Utility.Enum;
using static VTS.QLNS.CTC.App.Model.NsSktChungTuChiTietModel;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ chi tiết", 5, 1)]
    public class DemandVoucherDetailImportModel : BindableBase
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
        public string KyHieu
        {
            get => _kyHieu;
            set => SetProperty(ref _kyHieu, value);
        }

        private string _stt;
        public string STT
        {
            get => _stt;
            set => SetProperty(ref _stt, value);
        }

        private string _nganh;
        public string Nganh
        {
            get => _nganh;
            set => SetProperty(ref _nganh, value);
        }

        private string _nganhCha;
        public string NganhCha
        {
            get => _nganhCha;
            set => SetProperty(ref _nganhCha, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private string _x1SoTien;
        public string X1SoTien
        {
            get => _x1SoTien;
            set => SetProperty(ref _x1SoTien, value);
        }

        private string _x2SoTien;
        public string X2SoTien
        {
            get => _x2SoTien;
            set => SetProperty(ref _x2SoTien, value);
        }

        private string _x3SoTien;
        public string X3SoTien
        {
            get => _x3SoTien;
            set => SetProperty(ref _x3SoTien, value);
        }

        private string _x4SoTien;
        public string X4SoTien
        {
            get => _x4SoTien;
            set => SetProperty(ref _x4SoTien, value);
        }

        private string _x5SoTien;
        public string X5SoTien
        {
            get => _x5SoTien;
            set => SetProperty(ref _x5SoTien, value);
        }

        private string _x1SoTienMHHV;
        public string X1SoTienMHHV
        {
            get => _x1SoTienMHHV;
            set => SetProperty(ref _x1SoTienMHHV, value);
        }

        private string _x1SoTienDT;
        public string X1SoTienDT
        {
            get => _x1SoTienDT;
            set => SetProperty(ref _x1SoTienDT, value);
        }

        private string _x2SoTienMHHV;
        public string X2SoTienMHHV
        {
            get => _x2SoTienMHHV;
            set => SetProperty(ref _x2SoTienMHHV, value);
        }

        private string _x2SoTienDT;
        public string X2SoTienDT
        {
            get => _x2SoTienDT;
            set => SetProperty(ref _x2SoTienDT, value);
        }

        private string _x3SoTienMHHV;
        public string X3SoTienMHHV
        {
            get => _x3SoTienMHHV;
            set => SetProperty(ref _x3SoTienMHHV, value);
        }

        private string _x3SoTienDT;
        public string X3SoTienDT
        {
            get => _x3SoTienDT;
            set => SetProperty(ref _x3SoTienDT, value);
        }

        private string _x4SoTienMHHV;
        public string X4SoTienMHHV
        {
            get => _x4SoTienMHHV;
            set => SetProperty(ref _x4SoTienMHHV, value);
        }

        private string _x4SoTienDT;
        public string X4SoTienDT
        {
            get => _x4SoTienDT;
            set => SetProperty(ref _x4SoTienDT, value);
        }

        private string _x5SoTienMHHV;
        public string X5SoTienMHHV
        {
            get => _x5SoTienMHHV;
            set => SetProperty(ref _x5SoTienMHHV, value);
        }

        private string _x5SoTienDT;
        public string X5SoTienDT
        {
            get => _x5SoTienDT;
            set => SetProperty(ref _x5SoTienDT, value);
        }


        private string _huyDong;
        public string HuyDong
        {
            get => _huyDong;
            set => SetProperty(ref _huyDong, value);
        }

        private string _tonKhoDenNgay;
        public string TonkhoDenNgay
        {
            get => _tonKhoDenNgay;
            set => SetProperty(ref _tonKhoDenNgay, value);
        }

        private string _tuChi;
        public string TuChi
        {
            get => _tuChi;
            set => SetProperty(ref _tuChi, value);
        }

        private string _muaHangHienVat;
        public string MuaHangHienVat
        {
            get => _muaHangHienVat;
            set => SetProperty(ref _muaHangHienVat, value);
        }

        private string _dacThu;
        public string DacThu
        {
            get => _dacThu;
            set => SetProperty(ref _dacThu, value);
        }

        private string _khungNganSachDuocDuyet;
        public string KhungNganSachDuocDuyet
        {
            get => _khungNganSachDuocDuyet;
            set => SetProperty(ref _khungNganSachDuocDuyet, value);
        }

        private string _soNganhPhanCap;
        public string SoNganhPhanCap
        {
            get => _soNganhPhanCap;
            set => SetProperty(ref _soNganhPhanCap, value);
        }

        public string TongSo => ((double.TryParse(_soNganhPhanCap, out double value1) ? value1 : 0) +
            (double.TryParse(_dacThu, out double value2) ? value2 : 0)).ToString();


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
