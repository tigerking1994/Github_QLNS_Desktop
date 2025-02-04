using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ chi tiết", 6, 0)]
    public class BhCpChungTuDetailImportModel : BindableBase
    {
        private bool _isError;
        public bool IsError
        {
            get => _isError;
            set => SetProperty(ref _isError, value);
        }
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        private bool _isWarning;
        public bool IsWarning
        {
            get => _isWarning;
            set => SetProperty(ref _isWarning, value);
        }

        private bool _isHangcha;
        public bool IsHangCha
        {
            get => _isHangcha;
            set => SetProperty(ref _isHangcha, value);
        }
        private string _sXauNoiMa;
        [ColumnAttribute("Xâu nối mã", 0, Utility.Enum.ValidateType.IsXauNoiMaBH)]
        public string SXauNoiMa
        {
            get => _sXauNoiMa;
            set => SetProperty(ref _sXauNoiMa, value);
        }

        private string _sNoiDung;
        [ColumnAttribute("Nội dung", 1)]
        public string SNoiDung
        {
            get => _sNoiDung;
            set => SetProperty(ref _sNoiDung, value);
        }

        private string _sDonVi;
        [ColumnAttribute("Đơn vị", 2)]
        public string SDonVi
        {
            get => _sDonVi;
            set => SetProperty(ref _sDonVi, value);
        }
        private string _sDuToan;
        [ColumnAttribute("Dự toán", 3)]
        public string SDuToan
        {
            get => _sDuToan;
            set => SetProperty(ref _sDuToan, value);
        }

        private string _sDaCap;
        [ColumnAttribute("Đã cấp", 4)]
        public string SDaCap
        {
            get => _sDaCap;
            set => SetProperty(ref _sDaCap, value);
        }

        private string _sCapPhat;
        [ColumnAttribute("Cấp phát", 5)]
        public string SCapPhat
        {
            get => _sCapPhat;
            set => SetProperty(ref _sCapPhat, value);
        }

        private string _sGhiChu;
        [ColumnAttribute("Ghi chú", 6)]
        public string SGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
        }
        public bool IsHasData => !string.IsNullOrWhiteSpace(SDuToan) || !string.IsNullOrWhiteSpace(SGhiChu) || !string.IsNullOrWhiteSpace(SCapPhat) || !string.IsNullOrWhiteSpace(SDaCap);
        public double? FDuToan
        {
            get
            {
                if (double.TryParse(SDuToan, out double fduToan))
                {
                    return fduToan;
                }
                return 0;
            }
        }

        public double? FDaCap
        {
            get
            {
                if (double.TryParse(SDaCap, out double fDaCap))
                {
                    return fDaCap;
                }
                return 0;
            }
        }

        public double? FCapPhat
        {
            get
            {
                if (double.TryParse(SCapPhat, out double fCapPhat))
                {
                    return fCapPhat;
                }
                return 0;
            }
        }
    }
}
