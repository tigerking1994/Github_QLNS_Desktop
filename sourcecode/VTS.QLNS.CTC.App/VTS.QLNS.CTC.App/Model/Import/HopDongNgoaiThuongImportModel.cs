using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Danh sách hợp đồng", 2, 0)]
    public class HopDongNgoaiThuongImportModel : BindableBase
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

        private bool _isWarning;
        public bool IsWarning
        {
            get => _isWarning;
            set => SetProperty(ref _isWarning, value);
        }
        private string _soHopDong;
        [ColumnAttribute("Số Hợp Đồng", 1)]
        public string SoHopDong
        {
            get => _soHopDong;
            set => SetProperty(ref _soHopDong, value);
        }
        private string _ngayBanHanh;
        [ColumnAttribute("Ngày Ban Hành", 3)]
        public string NgayBanHanh
        {
            get => _ngayBanHanh;
            set => SetProperty(ref _ngayBanHanh, value);
        }
        private string _tenHopDong;
        [ColumnAttribute("Tên hợp đồng", 2)]
        public string TenHopDong
        {
            get => _tenHopDong;
            set => SetProperty(ref _tenHopDong, value);
        }
        private string _thoiGianThucHienTu;
        [ColumnAttribute("Ngày khởi công dự kiến", 5)]
        public string SKhoiCong
        {
            get => _thoiGianThucHienTu;
            set => SetProperty(ref _thoiGianThucHienTu, value);
        }
        private string _thoiGianThucHienDen;
        [ColumnAttribute("Ngày kết thúc dự kiến", 6)]
        public string SKetThuc
        {
            get => _thoiGianThucHienDen;
            set => SetProperty(ref _thoiGianThucHienDen, value);
        }
        private string _sMaLoaiHopDong;
        [ColumnAttribute("Mã loại hợp đồng", 4)]
        public string SMaLoaiHopDong
        {
            get => _sMaLoaiHopDong;
            set => SetProperty(ref _sMaLoaiHopDong, value);
        }
        private string _sGiaTriUSD;
        [ColumnAttribute("Giá trị hợp đồng USD", 7)]
        public string SGiaTriUSD
        {
            get => _sGiaTriUSD;
            set => SetProperty(ref _sGiaTriUSD, value);
        }
        private string _sGiaTriVND;
        [ColumnAttribute("Giá trị hợp đồng VND", 8)]
        public string SGiaTriVND
        {
            get => _sGiaTriVND;
            set => SetProperty(ref _sGiaTriVND, value);
        }
        private string _sGiaTriNgoaiTeKhac;
        [ColumnAttribute("Giá trị hợp đồng Ngoại tệ khác", 9)]
        public string SGiaTriNgoaiTeKhac
        {
            get => _sGiaTriNgoaiTeKhac;
            set => SetProperty(ref _sGiaTriNgoaiTeKhac, value);
        }
    }
}
