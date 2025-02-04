using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    public class DanhMucDonViImportModel : BaseImportModel
    {
        //private string _id;
        //[ColumnAttribute("Id", 0, "Id", "Guid khóa chính")]
        //[DisplayDetailInfo("Id", true)]
        //public string Id
        //{
        //    get => _id;
        //    set => SetProperty(ref _id, value);
        //}

        private string _iIDMaDonVi;
        [ColumnAttribute("Mã đơn vị", 0, "Id_donvi", "Mã đơn vị", ValidateType.IsNumber, true)]
        [DisplayDetailInfo("Mã đơn vị", false)]
        public string IIDMaDonVi
        {
            get => _iIDMaDonVi;
            set => SetProperty(ref _iIDMaDonVi, value);
        }

        private string _tenDonVi;
        [ColumnAttribute("Tên đơn vị", 1, "Ten_donvi", "Tên đơn vị", ValidateType.IsString, true)]
        [DisplayDetailInfo("Tên đơn vị", false)]
        public string TenDonVi
        {
            get => _tenDonVi;
            set => SetProperty(ref _tenDonVi, value);
        }

        private string _kyHieu;
        [ColumnAttribute("Ký hiệu", 2, "KyHieu", "Ký hiệu")]
        [DisplayDetailInfo("Ký hiệu", false)]
        public string KyHieu
        {
            get => _kyHieu;
            set => SetProperty(ref _kyHieu, value);
        }

        private string _moTa;
        [DisplayDetailInfo("Mô tả", false)]
        [ColumnAttribute("Mô tả", 3, "Mota", "Mô tả")]
        public string MoTa
        {
            get => _moTa;
            set => SetProperty(ref _moTa, value);
        }

        private string _loai;
        [ColumnAttribute("Loại", 4, "Loai", "Loại", ValidateType.IsNumber)]
        [DisplayDetailInfo("Loại", false)]
        public string Loai
        {
            get => _loai;
            set => SetProperty(ref _loai, value);
        }

        private string _namLamViec;
        [ColumnAttribute("Năm làm việc", 5, "NamLamViec", "Năm làm việc", ValidateType.IsNumber, true)]
        [DisplayDetailInfo("Năm làm việc", false)]
        public string NamLamViec
        {
            get => _namLamViec;
            set => SetProperty(ref _namLamViec, value);
        }
    }
}
