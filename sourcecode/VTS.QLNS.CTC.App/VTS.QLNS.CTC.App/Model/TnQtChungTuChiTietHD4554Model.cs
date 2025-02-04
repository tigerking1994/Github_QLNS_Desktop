using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class TnQtChungTuChiTietHD4554Model : DetailModelBase
    {
        public Guid Id { get; set; }
        public Guid? IIdTnQtChungTu { get; set; }
        public bool BHangCha { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        private double? _fSoTien;
        public double? FSoTien
        {
            get
            {
                return _fSoTien;
            }
            set
            {
                SetProperty(ref _fSoTien, value);
            }
        }

        private double? _fSoTienDeNghi;
        public double? FSoTienDeNghi
        {
            get => _fSoTienDeNghi;
            set
            {
                if (_fSoTien.GetValueOrDefault(0) == 0 && _fSoTienDeNghi.GetValueOrDefault(0) == 0 && !BHangCha && !IsFirstLoadFromDB)
                {
                    _fSoTien = value;
                    OnPropertyChanged(nameof(FSoTien));
                }

                IsFirstLoadFromDB = false;
                SetProperty(ref _fSoTienDeNghi, value);
            }
        }
        public string IIdMaDonVi { get; set; }
        public int? INguonNganSach { get; set; }
        public int? INamLamViec { get; set; }
        public int? INamNganSach { get; set; }
        public int? IThangQuy { get; set; }
        public int? IThangQuyLoai { get; set; }
        private string _sGhiChu;
        public string SGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
        }
        public string SK { get; set; }
        public string SL { get; set; }
        public string SLNS { get; set; }
        public string SM { get; set; }
        public string SNG { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
        public string STM { get; set; }
        public string STNG { get; set; }
        public string STNG1 { get; set; }
        public string STNG2 { get; set; }
        public string STNG3 { get; set; }
        public string STTM { get; set; }
        public string SXauNoiMa { get; set; }
        public string SNoiDung { get; set; }
        public Guid? IID_MLNS { get; set; }
        public Guid IID_MLNS_Cha { get; set; }
        public string SNoidung { get; set; }
        public string ChiTietToi { get; set; }
        public string SChiTietToi { get; set; }
        public bool HasData => FSoTien.GetValueOrDefault(0) != 0;
        public bool IsFirstLoadFromDB { get; set; } = true;
    }
}
