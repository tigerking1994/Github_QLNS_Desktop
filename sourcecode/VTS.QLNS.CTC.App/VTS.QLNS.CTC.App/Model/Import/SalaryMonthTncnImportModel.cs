using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Danh sách", 4, 0)]
    public class SalaryMonthTncnImportModel : BindableBase
    {
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

        private string _stt;
        [ColumnAttribute("STT", 0)]
        public string Stt
        {
            get => _stt;
            set => SetProperty(ref _stt, value);
        }

        private string _maCanBo;
        [ColumnAttribute("Mã đối tượng hưởng lương, phụ cấp (*)", 1)]
        public string MaCanBo
        {
            get => _maCanBo;
            set => SetProperty(ref _maCanBo, value);
        }

        private string _tenCb;
        [ColumnAttribute("Tên đối tượng hưởng lương, phụ cấp", 2)]
        public string TenCb
        {
            get => _tenCb;
            set => SetProperty(ref _tenCb, value);
        }

        private string _maCb;
        [ColumnAttribute("Tên đối tượng hưởng lương, phụ cấp", 3)]
        public string MaCb
        {
            get => _maCb;
            set => SetProperty(ref _maCb, value);
        }

        private string _maDonVi;
        [ColumnAttribute("Mã đơn vị", 4)]
        public string MaDonVi
        {
            get => _maDonVi;
            set => SetProperty(ref _maDonVi, value);
        }

        private string _tenDonVi;
        [ColumnAttribute("Tên đơn vị", 5)]
        public string TenDonVi
        {
            get => _tenDonVi;
            set => SetProperty(ref _tenDonVi, value);
        }

        private string _tienThuong;
        [ColumnAttribute("Tiền thưởng", 6, ValidateType.IsNumber)]
        public string TienThuong
        {
            get => _tienThuong;
            set => SetProperty(ref _tienThuong, value);
        }

        private string _loiIchKhac;
        [ColumnAttribute("Lợi ích khác", 7, ValidateType.IsNumber)]
        public string LoiIchKhac
        {
            get => _loiIchKhac;
            set => SetProperty(ref _loiIchKhac, value);
        }

        private string _tienThueDuocGiam;
        [ColumnAttribute("Thu nhập tính thuế được giảm", 8, ValidateType.IsNumber)]
        public string TienThueDuocGiam
        {
            get => _tienThueDuocGiam;
            set => SetProperty(ref _tienThueDuocGiam, value);
        }

        private string _thueTNCNDaNop;
        [ColumnAttribute("Thuế TNCN đã nộp/khấu trừ", 9, ValidateType.IsNumber)]
        public string ThueTNCNDaNop
        {
            get => _thueTNCNDaNop;
            set => SetProperty(ref _thueTNCNDaNop, value);
        }
    }
}
