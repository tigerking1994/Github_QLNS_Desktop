using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Thông tin chứng từ", 4, 0)]
    public class RevenueExpenditurePlanDetailImportModel : BindableBase
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

        private string _mlnsId;
        [ColumnAttribute("MLNSId", 0)]
        public string MlnsId
        {
            get => _mlnsId;
            set => SetProperty(ref _mlnsId, value);
        }

        private string _mlnsParentId;
        [ColumnAttribute("MLNSIdParent", 1)]
        public string MlnsIdParent
        {
            get => _mlnsParentId;
            set => SetProperty(ref _mlnsParentId, value);
        }

        private string _lns;
        [ColumnAttribute("LNS", 2)]
        public string LNS
        {
            get => _lns;
            set => SetProperty(ref _lns, value);
        }

        private string _l;
        [ColumnAttribute("L", 3)]
        public string L
        {
            get => _l;
            set => SetProperty(ref _l, value);
        }

        private string _k;
        [ColumnAttribute("K", 4)]
        public string K
        {
            get => _k;
            set => SetProperty(ref _k, value);
        }

        private string _m;
        [ColumnAttribute("M", 5)]
        public string M
        {
            get => _m;
            set => SetProperty(ref _m, value);
        }

        private string _tm;
        [ColumnAttribute("TM", 6)]
        public string TM
        {
            get => _tm;
            set => SetProperty(ref _tm, value);
        }

        private string _ttm;
        [ColumnAttribute("TTM", 7)]
        public string TTM
        {
            get => _ttm;
            set => SetProperty(ref _ttm, value);
        }

        private string _ng;
        [ColumnAttribute("NG", 8)]
        public string NG
        {
            get => _ng;
            set => SetProperty(ref _ng, value);
        }

        private string _tng;
        [ColumnAttribute("TNG", 9)]
        public string TNG
        {
            get => _tng;
            set => SetProperty(ref _tng, value);
        }

        [ColumnAttribute(ValidateType.IsXauNoiMa)]
        public string ConcatenateCode
        {
            get => string.Join("-", new string[] { _lns, _l, _k, _m, _tm, _ttm, _ng, _tng }.Where(s => !string.IsNullOrEmpty(s)));
        }

        private string _description;
        [ColumnAttribute("Mô tả", 10)]
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private string _selfPaymentSettlement;
        [ColumnAttribute("Tự chi", 11, ValidateType.IsNumber)]
        public string SelfPaymentSettlement
        {
            get => _selfPaymentSettlement;
            set => SetProperty(ref _selfPaymentSettlement, value);
        }

        private string _note;
        [ColumnAttribute("Ghi chú", 12)]
        public string Note
        {
            get => _note;
            set => SetProperty(ref _note, value);
        }
    }
}
