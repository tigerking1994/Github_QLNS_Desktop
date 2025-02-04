using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class InitializationProjectModel : BindableBase
    {
        private Guid _id;
        public Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private int _namKhoiTao;
        public int NamKhoiTao
        {
            get => _namKhoiTao;
            set => SetProperty(ref _namKhoiTao, value);
        }

        private Guid? _duAnId;
        public Guid? DuAnId
        {
            get => _duAnId;
            set => SetProperty(ref _duAnId, value);
        }

        private string _tenDuAn;
        public string TenDuAn
        {
            get => _tenDuAn;
            set => SetProperty(ref _tenDuAn, value);
        }

        private string _donViId;
        public string DonViId
        {
            get => _donViId;
            set => SetProperty(ref _donViId, value);
        }

        private string _tenDonVi;
        public string TenDonVi
        {
            get => _tenDonVi;
            set => SetProperty(ref _tenDonVi, value);
        }

        private double? _tongMucDauTuPheDuyet;
        public double? TongMucDauTuPheDuyet
        {
            get => _tongMucDauTuPheDuyet;
            set => SetProperty(ref _tongMucDauTuPheDuyet, value);
        }

        private double? _kHVonBoTriHetNamTruoc;
        public double? KHVonBoTriHetNamTruoc
        {
            get => _kHVonBoTriHetNamTruoc;
            set => SetProperty(ref _kHVonBoTriHetNamTruoc, value);
        }

        private double? _luyKeThanhToanKLHT;
        public double? LuyKeThanhToanKLHT
        {
            get => _luyKeThanhToanKLHT;
            set => SetProperty(ref _luyKeThanhToanKLHT, value);
        }

        private double? _luyKeThanhToanTamUng;
        public double? LuyKeThanhToanTamUng
        {
            get => _luyKeThanhToanTamUng;
            set => SetProperty(ref _luyKeThanhToanTamUng, value);
        }

        private double? _vonThanhToanKLHT;
        public double? VonThanhToanKLHT
        {
            get => _vonThanhToanKLHT;
            set => SetProperty(ref _vonThanhToanKLHT, value);
        }

        private double? _cheDoChuaThuHoi;
        public double? CheDoChuaThuHoi
        {
            get => _cheDoChuaThuHoi;
            set => SetProperty(ref _cheDoChuaThuHoi, value);
        }
    }
}
