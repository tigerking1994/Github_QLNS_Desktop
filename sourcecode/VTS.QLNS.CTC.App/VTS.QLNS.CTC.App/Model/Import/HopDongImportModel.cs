using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [Sheet(0, "Hợp đồng trong nước", 12, 0)]
    public class HopDongImportModel : BindableBase
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
        [Column("Số hợp đồng", 0, ValidateType.IsString, true)]
        public string SoHopDong
        {
            get => _soHopDong;
            set => SetProperty(ref _soHopDong, value);
        }
        private string _tenHopDong;
        [Column("Tên hợp đồng", 1, ValidateType.IsString, true)]
        public string TenHopDong
        {
            get => _tenHopDong;
            set => SetProperty(ref _tenHopDong, value);
        }
        private string _ngayKiHopDong;
        [Column("Ngày kí hợp đồng", 2)]
        public string NgayKiHopDong
        {
            get => _ngayKiHopDong;
            set => SetProperty(ref _ngayKiHopDong, value);
        }
        //private string _maDonVi;
        //[Column("Đơn vị", 3)]
        //public string MaDonVi
        //{
        //    get => _maDonVi;
        //    set => SetProperty(ref _maDonVi, value);
        //}
        private string _maLoaiHopDong;
        [Column("Loại hợp đồng", 3)]
        public string MaLoaiHopDong
        {
            get => _maLoaiHopDong;
            set => SetProperty(ref _maLoaiHopDong, value);
        }
        private string _maNhaThauDaiDien;
        [Column("Nhà thầu đại diện", 4, ValidateType.IsString, true)]
        public string MaNhaThauDaiDien
        {
            get => _maNhaThauDaiDien;
            set => SetProperty(ref _maNhaThauDaiDien, value);
        }
        private string _thoiGianThucHienTu;
        [Column("Ngày khởi công dự kiến", 5)]
        public string SKhoiCong
        {
            get => _thoiGianThucHienTu;
            set => SetProperty(ref _thoiGianThucHienTu, value);
        }
        private string _thoiGianThucHienDen;
        [Column("Ngày kết thúc dự kiến", 6)]
        public string SKetThuc
        {
            get => _thoiGianThucHienDen;
            set => SetProperty(ref _thoiGianThucHienDen, value);
        }
        private string _thoiGianThucHIen;
        [Column("Thời gian thực hiện", 7)]
        public string IThoiGianThucHien
        {
            get => _thoiGianThucHIen;
            set => SetProperty(ref _thoiGianThucHIen, value);
        }
        private string _hinhThuchopDong;
        [Column("Hình thức hợp đồng", 8)]
        public string HinhThuchopDong
        {
            get => _hinhThuchopDong;
            set => SetProperty(ref _hinhThuchopDong, value);
        }
    }
}
