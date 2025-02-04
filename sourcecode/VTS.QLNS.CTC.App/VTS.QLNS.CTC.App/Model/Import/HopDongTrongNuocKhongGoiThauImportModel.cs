using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [Sheet(0, "Hợp đồng trong nước", 2, 0)]
    public class HopDongTrongNuocKhongGoiThauImportModel : BindableBase
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

        private string _stt;
        [Column("STT", 0)]
        public string STT
        {
            get => _stt;
            set => SetProperty(ref _stt, value);
        }
        private string _maKeHoachDatHang;
        [Column("Mã kế hoạch đặt hàng (Số quyết định)", 1, ValidateType.IsString, true)]
        public string MaKeHoachDatHang
        {
            get => _maKeHoachDatHang;
            set => SetProperty(ref _maKeHoachDatHang, value);
        }
        private string _soHopDong;
        [Column("Số hợp đồng", 2, ValidateType.IsString, true)]
        public string SoHopDong
        {
            get => _soHopDong;
            set => SetProperty(ref _soHopDong, value);
        }
        private string _tenHopDong;
        [Column("Tên hợp đồng", 3, ValidateType.IsString, true)]
        public string TenHopDong
        {
            get => _tenHopDong;
            set => SetProperty(ref _tenHopDong, value);
        }
        private string _ngayKiHopDong;
        [Column("Ngày kí hợp đồng", 4, ValidateType.IsDateTime, true)]
        public string NgayKiHopDong
        {
            get => _ngayKiHopDong;
            set => SetProperty(ref _ngayKiHopDong, value);
        }
        private string _maLoaiHopDong;
        [Column("Loại hợp đồng", 5, ValidateType.IsString, true)]
        public string MaLoaiHopDong
        {
            get => _maLoaiHopDong;
            set => SetProperty(ref _maLoaiHopDong, value);
        }
        private string _thoiGianThucHienTu;
        [Column("Ngày khởi công dự kiến", 6, ValidateType.IsDateTime)]
        public string SKhoiCong
        {
            get => _thoiGianThucHienTu;
            set => SetProperty(ref _thoiGianThucHienTu, value);
        }
        private string _thoiGianThucHienDen;
        [Column("Ngày kết thúc dự kiến", 7, ValidateType.IsDateTime)]
        public string SKetThuc
        {
            get => _thoiGianThucHienDen;
            set => SetProperty(ref _thoiGianThucHienDen, value);
        }
        private string _thoiGianThucHIen;
        [Column("Thời gian thực hiện (ngày)", 8, ValidateType.IsIntNumber)]
        public string IThoiGianThucHien
        {
            get => _thoiGianThucHIen;
            set => SetProperty(ref _thoiGianThucHIen, value);
        }
        private string _fGiaTriHopDongVND;
        [Column("Giá trị hợp đồng (VNĐ)", 9, ValidateType.IsNumber, true)]
        public string FGiaTriHopDongVND
        {
            get => _fGiaTriHopDongVND;
            set => SetProperty(ref _fGiaTriHopDongVND, value);
        }
        private string _fGiaTriHopDongUSD;
        [Column("Giá trị hợp đồng (USD)", 10, ValidateType.IsNumber)]
        public string FGiaTriHopDongUSD
        {
            get => _fGiaTriHopDongUSD;
            set => SetProperty(ref _fGiaTriHopDongUSD, value);
        }
    }
}
