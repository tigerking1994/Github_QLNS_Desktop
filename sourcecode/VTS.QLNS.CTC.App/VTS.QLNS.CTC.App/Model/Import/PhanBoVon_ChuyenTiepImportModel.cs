using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ", 12, 0)]
    public class PhanBoVon_ChuyenTiepImportModel : BindableBase
    {
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        private string _stt;
        [ColumnAttribute("STT", 0, isRequired: true)]
        public string Stt
        {
            get => _stt;
            set => SetProperty(ref _stt, value);
        }

        private string _sTenDuAn;
        [ColumnAttribute("Tên dự án", 1)]
        public string sTenDuAn
        {
            get => _sTenDuAn;
            set => SetProperty(ref _sTenDuAn, value);
        }

        private string _sMaDuAn;
        [ColumnAttribute("Mã dự án", 2, isRequired: true)]
        public string sMaDuAn
        {
            get => _sMaDuAn;
            set => SetProperty(ref _sMaDuAn, value);
        }

        private string _sLns;
        [ColumnAttribute("Loại ngân sách", 3)]
        public string sLns
        {
            get => _sLns;
            set => SetProperty(ref _sLns, value);
        }

        private string _sL;
        [ColumnAttribute("Loại", 4)]
        public string sL
        {
            get => _sL;
            set => SetProperty(ref _sL, value);
        }

        private string _sK;
        [ColumnAttribute("Khoản", 5)]
        public string sK
        {
            get => _sK;
            set => SetProperty(ref _sK, value);
        }

        private string _sM;
        [ColumnAttribute("Mục", 6)]
        public string sM
        {
            get => _sM;
            set => SetProperty(ref _sM, value);
        }

        private string _sTm;
        [ColumnAttribute("Tiểu mục", 7)]
        public string sTm
        {
            get => _sTm;
            set => SetProperty(ref _sTm, value);
        }

        private string _sTtm;
        [ColumnAttribute("Tiết mục", 8)]
        public string sTtm
        {
            get => _sTtm;
            set => SetProperty(ref _sTtm, value);
        }

        private string _sNg;
        [ColumnAttribute("Ngành", 9)]
        public string sNg
        {
            get => _sNg;
            set => SetProperty(ref _sNg, value);
        }

        private string _fGiaTriPhanBo;
        [ColumnAttribute("Giá trị phân bổ", 16, ValidateType.IsNumber, true)]
        public string fGiaTriPhanBo
        {
            get => _fGiaTriPhanBo;
            set => SetProperty(ref _fGiaTriPhanBo, value);
        }

        private string _sGhiChu;
        [ColumnAttribute("Ghi chú", 17)]
        public string sGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
        }
    }
}
