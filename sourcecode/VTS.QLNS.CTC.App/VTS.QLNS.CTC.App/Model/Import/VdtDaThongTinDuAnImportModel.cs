using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Danh sách dự án", 1, 0)]
    public class VdtDaThongTinDuAnImportModel : ModelBase
    {
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        private bool _isError;
        public bool IsError
        {
            get => _isError;
            set => SetProperty(ref _isError, value);
        }

        private string _iStt;
        [ColumnAttribute("STT", 0)]
        public string IStt
        {
            get => _iStt;
            set => SetProperty(ref _iStt, value);
        }

        private string _sTenDuAn;
        [ColumnAttribute("Tên dự án", 1)]
        public string STenDuAn
        {
            get => _sTenDuAn;
            set => SetProperty(ref _sTenDuAn, value);
        }

        public Guid IIdDuAnId { get; set; }

        private string _sMaDuAn;
        [ColumnAttribute("Mã dự án", 2)]
        public string SMaDuAn
        {
            get => _sMaDuAn;
            set => SetProperty(ref _sMaDuAn, value);
        }

        private string _sKhoiCong;
        [ColumnAttribute("Khởi công", 3, ValidateType.IsNumber)]
        public string SKhoiCong
        {
            get => _sKhoiCong;
            set => SetProperty(ref _sKhoiCong, value);
        }

        private string _sKetThuc;
        [ColumnAttribute("Kết thúc", 4)]
        public string SKetThuc
        {
            get => _sKetThuc;
            set => SetProperty(ref _sKetThuc, value);
        }

        private string _sTenHangMuc;
        [ColumnAttribute("Tên hạng mục", 5)]
        public string STenHangMuc
        {
            get => _sTenHangMuc;
            set => SetProperty(ref _sTenHangMuc, value);
        }

        private string _sMaLoaiCongTrinh;
        [ColumnAttribute("Mã loại công trình", 6)]
        public string SMaLoaiCongTrinh
        {
            get => _sMaLoaiCongTrinh;
            set => SetProperty(ref _sMaLoaiCongTrinh, value);
        }

        public Guid IIdLoaiCongTrinhId { get; set; }

        private string _iIdNguonVonId;
        [ColumnAttribute("Mã nguồn vốn", 7, ValidateType.IsNumber)]
        public string IIdNguonVonId
        {
            get => _iIdNguonVonId;
            set => SetProperty(ref _iIdNguonVonId, value);
        }

        private string _sDiaDiem;
        [ColumnAttribute("Địa điểm", 8)]
        public string SDiaDiem
        {
            get => _sDiaDiem;
            set => SetProperty(ref _sDiaDiem, value);
        }

        private string _sMucTieu;
        [ColumnAttribute("Mục tiêu", 9)]
        private string SMucTieu
        {
            get => _sMucTieu;
            set => SetProperty(ref _sMucTieu, value);
        }

        private string _fHanMucDauTu;
        [ColumnAttribute("Hạn mức đầu tư", 10, ValidateType.IsNumber)]
        public string FHanMucDauTu
        {
            get => _fHanMucDauTu;
            set => SetProperty(ref _fHanMucDauTu, value);
        }
    }
}
