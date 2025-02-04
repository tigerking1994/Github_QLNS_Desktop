using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class ReportProcessProjectViewModel : BindableBase
    {
        private string _id;
        public string Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string _idParent;
        public string IdParent
        {
            get => _idParent;
            set => SetProperty(ref _idParent, value);
        }

        private bool _isHangCha;
        public bool IsHangCha
        {
            get => _isHangCha;
            set => SetProperty(ref _isHangCha, value);
        }

        private int _typeData;
        public int TypeData
        {
            get => _typeData;
            set => SetProperty(ref _typeData, value);
        }

        private string _tenNhaThau;
        public string TenNhaThau
        {
            get => _tenNhaThau;
            set => SetProperty(ref _tenNhaThau, value);
        }

        private string _soHopDong;
        public string SoHopDong
        {
            get => _soHopDong;
            set => SetProperty(ref _soHopDong, value);
        }

        private int _thoiGianThucHien;
        public int ThoiGianThucHien
        {
            get => _thoiGianThucHien;
            set => SetProperty(ref _thoiGianThucHien, value);
        }

        private double _tienHopDong;
        public double TienHopDong
        {
            get => _tienHopDong;
            set => SetProperty(ref _tienHopDong, value);
        }

        private string _soDeNghi;
        public string SoDeNghi
        {
            get => _soDeNghi;
            set => SetProperty(ref _soDeNghi, value);
        }

        private DateTime? _ngayThanhToan;
        public DateTime? NgayThanhToan
        {
            get => _ngayThanhToan;
            set => SetProperty(ref _ngayThanhToan, value);
        }

        private Guid? _duAnId;
        public Guid? DuAnId
        {
            get => _duAnId;
            set => SetProperty(ref _duAnId, value);
        }

        private Guid? _nhaThauId;
        public Guid? NhaThauId
        {
            get => _nhaThauId;
            set => SetProperty(ref _nhaThauId, value);
        }

        private Guid? _hopDongId;
        public Guid? HopDongId
        {
            get => _hopDongId;
            set => SetProperty(ref _hopDongId, value);
        }

        private double _soThanhToan;
        public double SoThanhToan
        {
            get => _soThanhToan;
            set => SetProperty(ref _soThanhToan, value);
        }

        private double _soTamUng;
        public double SoTamUng
        {
            get => _soTamUng;
            set => SetProperty(ref _soTamUng, value);
        }

        private double _soThuHoiTamUng;
        public double SoThuHoiTamUng
        {
            get => _soThuHoiTamUng;
            set => SetProperty(ref _soThuHoiTamUng, value);
        }

        public double TongCongGiaiNgan
        {
            get
            {
                return (SoThanhToan + SoTamUng) - SoThuHoiTamUng;
            }
        }

        private DateTime? _ngayCapUng;
        public DateTime? NgayCapUng
        {
            get => _ngayCapUng;
            set => SetProperty(ref _ngayCapUng, value);
        }

        private double _soDaCapUng;
        public double SoDaCapUng
        {
            get => _soDaCapUng;
            set => SetProperty(ref _soDaCapUng, value);
        }
        public string Mlns { get; set; }
    }
}
