using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class ThongTinHopDongModel : ModelBase
    {
        public Guid? IIdDuAnId { get; set; }
        public Guid? IIdGoiThauId { get; set; }
        public Guid? IIdHopDongGocId { get; set; }
        public bool? BIsGoc { get; set; }
        public bool? BActive { get; set; }
        public string SSoHopDong { get; set; }
        private int _iThoiGianThucHien;
        public int IThoiGianThucHien
        {
            get => _iThoiGianThucHien;
            set
            {
                SetProperty(ref _iThoiGianThucHien, value);
                OnPropertyChanged(nameof(DKetThucDuKien));
            }
        }
        public DateTime DNgayHopDong { get; set; }
        private DateTime? _dKhoiCongDuKien;
        public DateTime? DKhoiCongDuKien
        {
            get => _dKhoiCongDuKien;
            set
            {
                SetProperty(ref _dKhoiCongDuKien, value);
                OnPropertyChanged(nameof(DKetThucDuKien));
            }
        }
        private DateTime? _dKetThucDuKien;
        public DateTime? DKetThucDuKien
        {
            set => SetProperty(ref _dKetThucDuKien, value);
            get
            {
                if (IThoiGianThucHien == null) return null;
                if (DKhoiCongDuKien == null) return null;
                else return ((DateTime)DKhoiCongDuKien).AddDays(IThoiGianThucHien);
            }
        }
        private DateTime? _dBatDauBaoLanhHopDong;
        public DateTime? DBatDauBaoLanhHopDong
        {
            get => _dBatDauBaoLanhHopDong;
            set => SetProperty(ref _dBatDauBaoLanhHopDong, value);
        }

        private DateTime? _dKetThucBaoLanhHopDong;
        public DateTime? DKetThucBaoLanhHopDong
        {
            get => _dKetThucBaoLanhHopDong;
            set => SetProperty(ref _dKetThucBaoLanhHopDong, value);
        }

        public Guid? IIdNhaThauThucHienId { get; set; }
        public string SSoTaiKhoan { get; set; }
        public string SNganHang { get; set; }
        public int ITinhTrangHopDong { get; set; }
        public Guid? IIdLoaiHopDongId { get; set; }
        public string SHinhThucHopDong { get; set; }
        public double? FTienHopDong { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public double? FTiGia { get; set; }
        public Guid? IIdParentId { get; set; }
        public string SUserCreate { get; set; }
        public DateTime? DDateCreate { get; set; }
        public string SUserUpdate { get; set; }
        public DateTime? DDateUpdate { get; set; }
        public string SUserDelete { get; set; }
        public DateTime? DDateDelete { get; set; }
        public string NoiDungHopDong { get; set; }
        public bool? BKhoa { get; set; }
        public int? ILandieuchinh { get; set; }
        public string STenHopDong { get; set; }
        private double _fGiaTriHopDong;
        public double FGiaTriHopDong 
        {
            get => _fGiaTriHopDong;
            set => SetProperty(ref _fGiaTriHopDong, value);
        }
    }
}
