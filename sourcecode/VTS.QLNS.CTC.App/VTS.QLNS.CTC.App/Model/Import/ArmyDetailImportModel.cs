using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ chi tiết", 6, 0)]
    public class ArmyDetailImportModel : BindableBase
    {
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }
        [ColumnAttribute("Xâu nối mã", 0)]
        public string XauNoiMa { get; set; }
        [ColumnAttribute("Nội dung", 1)]
        public string MoTa { get; set; }

        private string _sTongSo;
        [ColumnAttribute("Tổng số", 2, ValidateType.IsNumber)]
        public string STongSo
        {
            get => _sTongSo;
            set
            {
                SetProperty(ref _sTongSo, value);
            }
        }

        private string _rThieuUy;
        [ColumnAttribute("11", 12, ValidateType.IsNumber)]
        public string RThieuUy
        {
            get => _rThieuUy;
            set
            {
                SetProperty(ref _rThieuUy, value);
            }
        }

        private string _rTrungUy;
        [ColumnAttribute("12", 11, ValidateType.IsNumber)]
        public string RTrungUy
        {
            get => _rTrungUy;
            set
            {
                SetProperty(ref _rTrungUy, value);
            }
        }

        private string _rThuongUy;
        [ColumnAttribute("13", 10, ValidateType.IsNumber)]
        public string RThuongUy
        {
            get => _rThuongUy;
            set
            {
                SetProperty(ref _rThuongUy, value);
            }
        }

        private string _rDaiUy;
        [ColumnAttribute("14", 9, ValidateType.IsNumber)]
        public string RDaiUy
        {
            get => _rDaiUy;
            set
            {
                SetProperty(ref _rDaiUy, value);
            }
        }

        private string _rThieuTa;
        [ColumnAttribute("21", 8, ValidateType.IsNumber)]
        public string RThieuTa
        {
            get => _rThieuTa;
            set
            {
                SetProperty(ref _rThieuTa, value);
            }
        }

        private string _rTrungTa;
        [ColumnAttribute("22", 7, ValidateType.IsNumber)]
        public string RTrungTa
        {
            get => _rTrungTa;
            set
            {
                SetProperty(ref _rTrungTa, value);
            }
        }

        private string _rThuongTa;
        [ColumnAttribute("23", 6, ValidateType.IsNumber)]
        public string RThuongTa
        {
            get => _rThuongTa;
            set
            {
                SetProperty(ref _rThuongTa, value);
            }
        }

        private string _rDaiTa;
        [ColumnAttribute("24", 5, ValidateType.IsNumber)]
        public string RDaiTa
        {
            get => _rDaiTa;
            set
            {
                SetProperty(ref _rDaiTa, value);
            }
        }

        private string _rTuong;
        [ColumnAttribute("Tg", 4, ValidateType.IsNumber)]
        public string RTuong
        {
            get => _rTuong;
            set
            {
                SetProperty(ref _rTuong, value);
            }
        }

        [ColumnAttribute("(+)", 3, ValidateType.IsNumber)]
        public string SumSyQuan { get; set; }

        [ColumnAttribute("(+)", 13, ValidateType.IsNumber)]
        public string SumQNCN { get; set; }

        private string _soThuongTaQNCN;
        [ColumnAttribute("23", 14, ValidateType.IsNumber)]
        public string SoThuongTaQNCN
        {
            get => _soThuongTaQNCN;
            set
            {
                SetProperty(ref _soThuongTaQNCN, value);
            }
        }
        
        private string _soTrungTaQNCN;
        [ColumnAttribute("22", 15, ValidateType.IsNumber)]
        public string SoTrungTaQNCN
        {
            get => _soTrungTaQNCN;
            set
            {
                SetProperty(ref _soTrungTaQNCN, value);
            }
        }
        
        private string _soThieuTaQNCN;
        [ColumnAttribute("21", 16, ValidateType.IsNumber)]
        public string SoThieuTaQNCN
        {
            get => _soThieuTaQNCN;
            set
            {
                SetProperty(ref _soThieuTaQNCN, value);
            }
        }
        
        private string _soDaiUyQNCN;
        [ColumnAttribute("14", 17, ValidateType.IsNumber)]
        public string SoDaiUyQNCN
        {
            get => _soDaiUyQNCN;
            set
            {
                SetProperty(ref _soDaiUyQNCN, value);
            }
        }
        
        private string _soThuongUyQNCN;
        [ColumnAttribute("13", 18, ValidateType.IsNumber)]
        public string SoThuongUyQNCN
        {
            get => _soThuongUyQNCN;
            set
            {
                SetProperty(ref _soThuongUyQNCN, value);
            }
        }
        
        private string _soTrungUyQNCN;
        [ColumnAttribute("12", 19, ValidateType.IsNumber)]
        public string SoTrungUyQNCN
        {
            get => _soTrungUyQNCN;
            set
            {
                SetProperty(ref _soTrungUyQNCN, value);
            }
        }
        
        private string _soThieuUyQNCN;
        [ColumnAttribute("11", 20, ValidateType.IsNumber)]
        public string SoThieuUyQNCN
        {
            get => _soThieuUyQNCN;
            set
            {
                SetProperty(ref _soThieuUyQNCN, value);
            }
        }

        [ColumnAttribute("TSQ", 12, ValidateType.IsNumber)]
        public string TSQ { get; set; }

        private string _rBinhNhi;
        [ColumnAttribute("B2", 27, ValidateType.IsNumber)]
        public string RBinhNhi
        {
            get => _rBinhNhi;
            set
            {
                SetProperty(ref _rBinhNhi, value);
            }
        }

        private string _rBinhNhat;
        [ColumnAttribute("B1", 26, ValidateType.IsNumber)]
        public string RBinhNhat
        {
            get => _rBinhNhat;
            set
            {
                SetProperty(ref _rBinhNhat, value);
            }
        }

        private string _rHaSi;
        [ColumnAttribute("H1", 25, ValidateType.IsNumber)]
        public string RHaSi
        {
            get => _rHaSi;
            set
            {
                SetProperty(ref _rHaSi, value);
            }
        }

        private string _rTrungSi;
        [ColumnAttribute("H2", 24, ValidateType.IsNumber)]
        public string RTrungSi
        {
            get => _rTrungSi;
            set
            {
                SetProperty(ref _rTrungSi, value);
            }
        }

        private string _rThuongSi;
        [ColumnAttribute("H3", 23, ValidateType.IsNumber)]
        public string RThuongSi
        {
            get => _rThuongSi;
            set
            {
                SetProperty(ref _rThuongSi, value);
            }
        }

        [ColumnAttribute("(+)", 21, ValidateType.IsNumber)]
        public string SumBS { get; set; }

        private string _rQncn;
        [ColumnAttribute("QNCN", 19, ValidateType.IsNumber)]
        public string RQncn
        {
            get => _rQncn;
            set
            {
                SetProperty(ref _rQncn, value);
            }
        }

        private string _rCcqp;
        [ColumnAttribute("CCQP", 28, ValidateType.IsNumber)]
        public string RCcqp
        {
            get => _rCcqp;
            set
            {
                SetProperty(ref _rCcqp, value);
            }
        }

        private string _rVcqp;
        [ColumnAttribute("VCQP", 30, ValidateType.IsNumber)]
        public string RVcqp
        {
            get => _rVcqp;
            set
            {
                SetProperty(ref _rVcqp, value);
            }
        }               

        private string _rCnvqp;
        [ColumnAttribute("CNQP", 29, ValidateType.IsNumber)]
        public string RCnvqp
        {
            get => _rCnvqp;
            set
            {
                SetProperty(ref _rCnvqp, value);
            }
        }

        private string _rLdhd;
        [ColumnAttribute("Khác", 31, ValidateType.IsNumber)]
        public string RLdhd
        {
            get => _rLdhd;
            set
            {
                SetProperty(ref _rLdhd, value);
            }
        }
    }
}
