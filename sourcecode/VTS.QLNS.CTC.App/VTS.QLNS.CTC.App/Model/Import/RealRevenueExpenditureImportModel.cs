using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ", 4, 0)]
    public class RealRevenueExpenditureImportModel : BindableBase
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

        private string _tongSoThu;
        [ColumnAttribute("Tổng số Thu", 6, ValidateType.IsNumber)]
        public string TongSoThu
        {
            get => _tongSoThu;
            set => SetProperty(ref _tongSoThu, value);
        }

        private string _tongSoChiPhi;
        [ColumnAttribute("Tổng số chi phí", 7, ValidateType.IsNumber)]
        public string TongSoChiPhi
        {
            get => _tongSoChiPhi;
            set => SetProperty(ref _tongSoChiPhi, value);
        }

        private string _lns;
        [ColumnAttribute("Mã loại hình", 8)]
        public string Lns
        {
            get => _lns;
            set => SetProperty(ref _lns, value);
        }

        private string _soQuyetDinh;
        [ColumnAttribute("Số quyết định", 9)]
        public string SoQuyetDinh
        {
            get => _soQuyetDinh;
            set => SetProperty(ref _soQuyetDinh, value);
        }

        private string _ngayQuyetDinh;
        [ColumnAttribute("Ngày quyết định", 10, ValidateType.IsDateTime)]
        public string NgayQuyetDinh
        {
            get => _ngayQuyetDinh;
            set => SetProperty(ref _ngayQuyetDinh, value);
        }

        private string _moTaChiTiet;
        [ColumnAttribute("Mô tả", 11)]
        public string MoTaChiTiet
        {
            get => _moTaChiTiet;
            set => SetProperty(ref _moTaChiTiet, value);
        }
    }
}
