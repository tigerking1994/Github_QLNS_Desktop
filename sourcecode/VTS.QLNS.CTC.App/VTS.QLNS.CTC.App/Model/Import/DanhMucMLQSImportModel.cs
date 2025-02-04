using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    public class DanhMucMLQSImportModel : BaseImportModel
    {
        //private string _id;
        //[ColumnAttribute("Id", 0, "Id", "Guid khóa chính")]
        //[DisplayDetailInfo("Id", true)]
        //public string Id
        //{
        //    get => _id;
        //    set => SetProperty(ref _id, value);
        //}

        private string _m;
        [ColumnAttribute("M", 0, "M", "M", ValidateType.IsIntNumber, true)]
        [DisplayDetailInfo("M")]
        public string M
        {
            get => _m;
            set => SetProperty(ref _m, value);
        }

        private string _tm;
        [ColumnAttribute("TM", 1, "TM", "TM", ValidateType.IsIntNumber)]
        [DisplayDetailInfo("TM")]
        public string TM
        {
            get => _tm;
            set => SetProperty(ref _tm, value);
        }

        private string _xnm;
        [ColumnAttribute("Xâu nối mã", 2, "XauNoiMa", "Xâu nối mã")]
        [DisplayDetailInfo("XNM")]
        public string XNM
        {
            get => _xnm;
            set => SetProperty(ref _xnm, value);
        }

        private string _mota;
        [DisplayDetailInfo("Mô tả")]
        [ColumnAttribute("Mô tả", 3, "Mota", "Mô tả")]
        public string MoTa
        {
            get => _mota;
            set => SetProperty(ref _mota, value);
        }

        private string _hienThi;
        [DisplayDetailInfo("Hiển thị")]
        [ColumnAttribute("Hiển thị", 4, "BHienThi", "Hiển thị", ValidateType.IsIntNumber)]
        public string HienThi
        {
            get => _hienThi;
            set => SetProperty(ref _hienThi, value);
        }

        private string _namLamViec;
        [DisplayDetailInfo("Năm làm việc")]
        [ColumnAttribute("Năm làm việc", 5, "NamLamViec", "Năm làm việc", ValidateType.IsIntNumber, true)]
        public string NamLamViec
        {
            get => _namLamViec;
            set => SetProperty(ref _namLamViec, value);
        }
    }
}
