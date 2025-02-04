using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ",10 , 0)]
    public class KeHoachVonUngImportModel : BindableBase
    {
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        private string _sTenDuAn;
        [ColumnAttribute("Tên dự án", 1, isRequired: true)]
        public string STenDuAn
        {
            get => _sTenDuAn;
            set => SetProperty(ref _sTenDuAn, value);
        }

        private string _sMaDuAn;
        [ColumnAttribute("Mã dự án", 2, isRequired: true)]
        public string SMaDuAn
        {
            get => _sMaDuAn;
            set => SetProperty(ref _sMaDuAn, value);
        }

        private string _fGiaTriDeNghi;
        [ColumnAttribute("Giá trị đề nghị ứng", 4, ValidateType.IsNumber)]
        public string FGiaTriDeNghi
        {
            get => _fGiaTriDeNghi;
            set => SetProperty(ref _fGiaTriDeNghi, value);
        }

        private string _sGhiChu;
        [ColumnAttribute("Ghi chú", 5)]
        public string SGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
        }
    }
}
