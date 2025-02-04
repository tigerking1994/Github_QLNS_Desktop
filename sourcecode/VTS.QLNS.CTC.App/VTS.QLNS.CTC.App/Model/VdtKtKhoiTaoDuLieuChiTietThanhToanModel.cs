using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtKtKhoiTaoDuLieuChiTietThanhToanModel : DetailModelBase
    {
        private Guid? _iIDHopDongId;
        public Guid? IIDHopDongId
        {
            get => _iIDHopDongId;
            set => SetProperty(ref _iIDHopDongId, value);
        }

        private string _sSoHopDong;
        public string SSoHopDong
        {
            get => _sSoHopDong;
            set => SetProperty(ref _sSoHopDong, value);
        }

        private string _sTenNhaThau;
        public string STenNhaThau
        {
            get => _sTenNhaThau;
            set => SetProperty(ref _sTenNhaThau, value);
        }

        private string _sTenHopDong;
        public string STenHopDong
        {
            get => _sTenHopDong;
            set => SetProperty(ref _sTenHopDong, value);
        }

        private double? _fTienHopDong;
        public double? FTienHopDong
        {
            get => _fTienHopDong;
            set => SetProperty(ref _fTienHopDong, value);
        }

        private double? _fLuyKeTtklhtTnKhvn;
        public double? FLuyKeTtklhtTnKhvn
        {
            get => _fLuyKeTtklhtTnKhvn;
            set => SetProperty(ref _fLuyKeTtklhtTnKhvn, value);
        }

        private double? _fLuyKeTUChuaThuHoiTnKhvn;
        public double? FLuyKeTUChuaThuHoiTnKhvn
        {
            get => _fLuyKeTUChuaThuHoiTnKhvn;
            set => SetProperty(ref _fLuyKeTUChuaThuHoiTnKhvn, value);
        }

        private double? _fLuyKeTtklhtNnKhvn;
        public double? FLuyKeTtklhtNnKhvn
        {
            get => _fLuyKeTtklhtNnKhvn;
            set => SetProperty(ref _fLuyKeTtklhtNnKhvn, value);
        }
        private double? _fLuyKeTUChuaThuHoiNnKhvn;
        public double? FLuyKeTUChuaThuHoiNnKhvn
        {
            get => _fLuyKeTUChuaThuHoiNnKhvn;
            set => SetProperty(ref _fLuyKeTUChuaThuHoiNnKhvn, value);
        }
        // -------------
        private double? _fLuyKeTtklhtTnKhvu;
        public double? FLuyKeTtklhtTnKhvu
        {
            get => _fLuyKeTtklhtTnKhvu;
            set => SetProperty(ref _fLuyKeTtklhtTnKhvu, value);
        }

        private double? _fLuyKeTUChuaThuHoiTnKhvu;
        public double? FLuyKeTUChuaThuHoiTnKhvu
        {
            get => _fLuyKeTUChuaThuHoiTnKhvu;
            set => SetProperty(ref _fLuyKeTUChuaThuHoiTnKhvu, value);
        }

        private double? _fLuyKeTtklhtNnKhvu;
        public double? FLuyKeTtklhtNnKhvu
        {
            get => _fLuyKeTtklhtNnKhvu;
            set => SetProperty(ref _fLuyKeTtklhtNnKhvu, value);
        }
        private double? _fLuyKeTUChuaThuHoiNnKhvu;
        public double? FLuyKeTUChuaThuHoiNnKhvu
        {
            get => _fLuyKeTUChuaThuHoiNnKhvu;
            set => SetProperty(ref _fLuyKeTUChuaThuHoiNnKhvu, value);
        }

        private Guid? _iIDChiPhiId;
        public Guid? IIDChiPhiId
        {
            get => _iIDChiPhiId;
            set => SetProperty(ref _iIDChiPhiId, value);
        }

        private int? _iLoai;
        public int? ILoai
        {
            get => _iLoai;
            set => SetProperty(ref _iLoai, value);
        }

        public string LoaiString => _iLoai != null ? VdtKtLoaiChiTietThanhToan.Get(_iLoai) : "";
    }
}
