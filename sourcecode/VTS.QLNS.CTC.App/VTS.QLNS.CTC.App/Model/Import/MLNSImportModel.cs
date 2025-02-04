using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    public class MLNSImportModel : BaseImportModel
    {
        //private string _id;
        //[ColumnAttribute("Id", 0, "Id", "Guid khóa chính")]
        //[DisplayDetailInfo("Id", true)]
        //public string Id
        //{
        //    get => _id;
        //    set => SetProperty(ref _id, value);
        //}

        private string _lns;
        [ColumnAttribute("LNS", 0, "LNS", "LNS", ValidateType.IsIntNumber, true)]
        [DisplayDetailInfo("LNS")]
        public string Lns
        {
            get => _lns;
            set => SetProperty(ref _lns, value);
        }

        private string _l;
        [ColumnAttribute("L", 1, "L", "L", ValidateType.IsIntNumber)]
        [DisplayDetailInfo("L")]
        public string L
        {
            get => _l;
            set => SetProperty(ref _l, value);
        }

        private string _k;
        [ColumnAttribute("K", 2, "K", "K", ValidateType.IsIntNumber)]
        [DisplayDetailInfo("K")]
        public string K
        {
            get => _k;
            set => SetProperty(ref _k, value);
        }

        private string _m;
        [ColumnAttribute("M", 3, "M", "M", ValidateType.IsIntNumber)]
        [DisplayDetailInfo("M")]
        public string M
        {
            get => _m;
            set => SetProperty(ref _m, value);
        }

        private string _tm;
        [ColumnAttribute("TM", 4, "TM", "TM", ValidateType.IsIntNumber)]
        [DisplayDetailInfo("TM")]
        public string TM
        {
            get => _tm;
            set => SetProperty(ref _tm, value);
        }

        private string _ttm;
        [ColumnAttribute("TTM", 5, "TTM", "TTM", ValidateType.IsIntNumber)]
        [DisplayDetailInfo("TTM")]
        public string TTM
        {
            get => _ttm;
            set => SetProperty(ref _ttm, value);
        }

        private string _ng;
        [DisplayDetailInfo("NG")]
        [ColumnAttribute("NG", 6, "NG", "NG", ValidateType.IsIntNumber)]
        public string NG
        {
            get => _ng;
            set => SetProperty(ref _ng, value);
        }

        private string _tng;
        [ColumnAttribute("TNG", 7, "TNG", "TNG", ValidateType.IsIntNumber)]
        [DisplayDetailInfo("TNG")]
        public string TNG
        {
            get => _tng;
            set => SetProperty(ref _tng, value);
        }

        private string _mota;
        [DisplayDetailInfo("Mô tả")]
        [ColumnAttribute("Mô tả", 8, "Mota", "Mô tả")]
        public string MoTa
        {
            get => _mota;
            set => SetProperty(ref _mota, value);
        }

        private string _namLamViec;
        [DisplayDetailInfo("Năm làm việc")]
        [ColumnAttribute("Năm làm việc", 9, "NamLamViec", "Năm làm việc", ValidateType.IsIntNumber, true)]
        public string NamLamViec 
        {
            get => _namLamViec;
            set => SetProperty(ref _namLamViec, value);
        }
    }
}
