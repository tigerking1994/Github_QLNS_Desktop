using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(2, "Chi Phi", 6, 1)]
    public class DeNghiQuyetToanChiPhiKhacImportModel : BindableBase
    {
        private bool _isErrorMLNS;
        public bool IsErrorMLNS
        {
            get => _isErrorMLNS;
            set => SetProperty(ref _isErrorMLNS, value);
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

        private bool _bHangcha;
        public bool BHangCha
        {
            get => _bHangcha;
            set => SetProperty(ref _bHangcha, value);
        }

        private string _tenDuAn;
        [ColumnAttribute("Tên dự án", 0)]
        public string TenDuAn
        {
            get => _tenDuAn;
            set => SetProperty(ref _tenDuAn, value);
        }

        private string _maDuAn;
        [ColumnAttribute("Mã dự án", 1)]
        public string MaDuAn
        {
            get => _maDuAn;
            set => SetProperty(ref _maDuAn, value);
        }

        private string _noiDungChiPhi;
        [ColumnAttribute("Nội dung chi phí", 2)]
        public string NoiDungChiPhi
        {
            get => _noiDungChiPhi;
            set => SetProperty(ref _noiDungChiPhi, value);
        }

        private string _giaTri;
        [ColumnAttribute("Giá trị", 4)]
        public string GiaTri
        {
            get => _giaTri;
            set => SetProperty(ref _giaTri, value);
        }
    }
}

