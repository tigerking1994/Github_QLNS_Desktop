using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhQtTaiSanModel : ModelBase
    {
        public override Guid Id { get; set; }
        public Guid? IIdKhttNhiemVuChiId { get; set; }
        public Guid? IIdDuAnId { get; set; }

        private string _sMaTaiSan;
        public string SMaTaiSan
        {
            get => _sMaTaiSan;
            set => SetProperty(ref _sMaTaiSan, value);
        }

        private string _sTenTaiSan;
        public string STenTaiSan
        {
            get => _sTenTaiSan;
            set => SetProperty(ref _sTenTaiSan, value);
        }

        public Guid? IIdLoaiTaiSan { get; set; }
        public Guid? IIdChungTuTaiSanId { get; set; }

        public string SMoTaTaiSan { get; set; }
        public DateTime? DNgayBatDauSuDung { get; set; }
        public int? ITrangThai { get; set; }
        public string SLoaiTaiSan { get; set; }
        public string STrangThai { get; set; }
        public int? ILoaiTaiSan { get; set; }
        public int? ITinhTrangSuDung { get; set; }
        
        private double? _fSoLuong;
        public double? FSoLuong
        {
            get => _fSoLuong;
            set => SetProperty(ref _fSoLuong, value);
        }
       
        private double? _fNguyenGia;
        public double? FNguyenGia
        {
            get => _fNguyenGia;
            set => SetProperty(ref _fNguyenGia, value);
        }

        private Guid? _iIdMaDonViId;
        public Guid? IIdMaDonViId { 
            get => _iIdMaDonViId; 
            set => SetProperty(ref _iIdMaDonViId, value); 
        }
        public Guid IIdHopDongId { get; set; }
        public string SDonViTinh { get; set; }
        [NotMapped]
        public string STenLoaiTaiSan { get; set; }
        [NotMapped]
        public string STenDonVi { get; set; }

        private string _sTenDuAn;
        [NotMapped]
        public string STenDuAn
        {
            get => _sTenDuAn;
            set => SetProperty(ref _sTenDuAn, value);
        }

        private string _sTenHopDong;
        [NotMapped]
        public string STenHopDong
        {
            get => _sTenHopDong;
            set => SetProperty(ref _sTenHopDong, value);
        }

        [NotMapped]
        public double? FTaiSan1 { get; set; }
        [NotMapped]
        public double? FTaiSan2 { get; set; }
        [NotMapped]
        public double? FTrangThai1 { get; set; }
        [NotMapped]
        public double? FTrangThai2 { get; set; }
        [NotMapped]
        public double? FTrangThai3 { get; set; }
        [NotMapped]
        public double? FTinhTrangSuDung1 { get; set; }
        [NotMapped]
        public double? FTinhTrangSuDung2 { get; set; }
        [NotMapped]
        public double? FTinhTrangSuDung3 { get; set; }

        [NotMapped]
        private ObservableCollection<DonViModel> _itemsDonVi;
        public ObservableCollection<DonViModel> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private DonViModel _selectedDonVi;

        public DonViModel SelectedDonVi
        {
            get => _selectedDonVi;
            set => SetProperty(ref _selectedDonVi, value);
        }
        public string STinhTrangSuDung { get; set; }
        public string STT { get; set; }
        public string SNgayBatDauSuDung
        {
            get
            {
                return this.DNgayBatDauSuDung.HasValue ? this.DNgayBatDauSuDung.Value.ToString("dd/MM/yyyy") : string.Empty;
            }
        }
    }
}