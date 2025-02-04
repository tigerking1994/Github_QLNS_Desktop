using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ", 4, 0)]
    public class RevenueExpenditurePlanImportModel : BindableBase
    {
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        private string _soChungTu;
        [ColumnAttribute("Số chứng từ", 0)]
        public string SoChungTu
        {
            get => _soChungTu;
            set => SetProperty(ref _soChungTu, value);
        }

        private string _ngayChungTu;
        [ColumnAttribute("Ngày chứng từ", 1, ValidateType.IsDateTime, true)]
        public string NgayChungTu
        {
            get => _ngayChungTu;
            set => SetProperty(ref _ngayChungTu, value);
        }

        private string _tuChiSum;
        [ColumnAttribute("Tự chi", 4, ValidateType.IsNumber)]
        public string TuChiSum
        {
            get => _tuChiSum;
            set => SetProperty(ref _tuChiSum, value);
        }

        private string _soQuyetDinh;
        [ColumnAttribute("Số quyết định", 6)]
        public string SoQuyetDinh
        {
            get => _soQuyetDinh;
            set => SetProperty(ref _soQuyetDinh, value);
        }

        private string _ngayQuyetDinh;
        [ColumnAttribute("Ngày quyết định", 7, ValidateType.IsDateTime)]
        public string NgayQuyetDinh
        {
            get => _ngayQuyetDinh;
            set => SetProperty(ref _ngayQuyetDinh, value);
        }

        private string _moTaChiTiet;
        [ColumnAttribute("Mô tả", 8)]
        public string MoTaChiTiet
        {
            get => _moTaChiTiet;
            set => SetProperty(ref _moTaChiTiet, value);
        }

        private string _lns;
        [ColumnAttribute("LNS", 5)]
        public string Lns
        {
            get => _lns;
            set => SetProperty(ref _lns, value);
        }
    }
}
