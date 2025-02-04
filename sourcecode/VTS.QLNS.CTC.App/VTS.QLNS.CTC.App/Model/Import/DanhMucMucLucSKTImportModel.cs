using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    public class DanhMucMucLucSKTImportModel : BaseImportModel
    {
        //private string _id;
        //[ColumnAttribute("Id", 0, "Id", "Guid khóa chính")]
        //[DisplayDetailInfo("Id", true)]
        //public string Id
        //{
        //    get => _id;
        //    set => SetProperty(ref _id, value);
        //}

        private string _sM;
        [ColumnAttribute("Mục - Tiểu mục", 0, "M", "Mục - Tiểu mục")]
        [DisplayDetailInfo("Mục - Tiểu mục")]
        public string SM
        {
            get => _sM;
            set => SetProperty(ref _sM, value);
        }

        private string _sNGCha;
        [ColumnAttribute("Ngành cha", 1, "Nganh_parent", "Ngành cha")]
        [DisplayDetailInfo("Ngành cha")]
        public string SNGCha
        {
            get => _sNGCha;
            set => SetProperty(ref _sNGCha, value);
        }

        private string _sNg;
        [ColumnAttribute("Ngành chi tiết", 2, "Nganh", "Ngành chi tiết")]
        [DisplayDetailInfo("Ngành chi tiết")]
        public string SNg
        {
            get => _sNg;
            set => SetProperty(ref _sNg, value);
        }

        private string _sSTT;
        [ColumnAttribute("STT", 3, "STT", "STT")]
        [DisplayDetailInfo("STT")]
        public string SSTT
        {
            get => _sSTT;
            set => SetProperty(ref _sSTT, value);
        }

        private string _sSttBC;
        [ColumnAttribute("STT in Báo cáo", 4, "SttBC", "STT in Báo cáo")]
        [DisplayDetailInfo("STT in Báo cáo")]
        public string SSttBC
        {
            get => _sSttBC;
            set => SetProperty(ref _sSttBC, value);
        }

        private string _sKyHieu;
        [ColumnAttribute("Mã", 5, "KyHieu", "Mã", ValidateType.IsString, true)]
        [DisplayDetailInfo("Mã")]
        public string SKyHieu
        {
            get => _sKyHieu;
            set => SetProperty(ref _sKyHieu, value);
        }

        private string _sMoTa;
        [ColumnAttribute("Mô tả", 6, "Mota", "Mô tả")]
        [DisplayDetailInfo("Mô tả")]
        public string SMoTa
        {
            get => _sMoTa;
            set => SetProperty(ref _sMoTa, value);
        }

        private string _kyHieuCha;
        [ColumnAttribute("Mã cha", 7, "KyHieuCha", "Mã cha")]
        [DisplayDetailInfo("Mã cha")]
        public string KyHieuCha
        {
            get => _kyHieuCha;
            set => SetProperty(ref _kyHieuCha, value);
        }

        private string _iNamLamViec;
        [DisplayDetailInfo("Năm làm việc")]
        [ColumnAttribute("Năm làm việc", 8, "NamLamViec", "Năm làm việc", ValidateType.IsIntNumber, true)]
        public string INamLamViec
        {
            get => _iNamLamViec;
            set => SetProperty(ref _iNamLamViec, value);
        }
    }
}
