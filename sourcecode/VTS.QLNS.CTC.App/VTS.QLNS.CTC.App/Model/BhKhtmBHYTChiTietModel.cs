using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhKhtmBHYTChiTietModel : DetailModelBase
    {
        public Guid Id { get; set; }
        public Guid IdKhtmBHYT { get; set; }
        private int? _iSoNguoi;
        public int? ISoNguoi
        {
            get => _iSoNguoi;
            set
            {
                SetProperty(ref _iSoNguoi, value);
                OnPropertyChanged(nameof(FThanhTien));
            }
        }
        private int? _iSoThang;
        public int? ISoThang
        {
            get
            {
                if (!IsHangCha && !IsAggregate)
                {
                    return _iSoThang = _iSoThang > 12 ? 12 : _iSoThang;
                }
                else
                {
                    return _iSoThang;
                }
            }
            set
            {
                SetProperty(ref _iSoThang, value);
                OnPropertyChanged(nameof(FThanhTien));
            }
        }
        private double? _fDinhMuc;
        public double? FDinhMuc
        {
            get
            {
                if (!IsHangCha)
                {
                    return _fDinhMuc = Math.Round((double)(DHeSoBHYT * DHeSoLCS).GetValueOrDefault());
                }
                else
                {
                    return _fDinhMuc;
                }
            }
            set
            {
                SetProperty(ref _fDinhMuc, value);
                OnPropertyChanged(nameof(FThanhTien));
            }
        }
        private double? _fThanhTien;
        public double? FThanhTien
        {
            get
            {
                if (!IsHangCha && !IsAggregate)
                {
                    return _fThanhTien = ISoNguoi.GetValueOrDefault() * ISoThang.GetValueOrDefault() * FDinhMuc;
                }
                else
                {
                    return _fThanhTien;
                }
            }
            set
            {
                SetProperty(ref _fThanhTien, value);
            }
        }

        private string _sGhiChu;
        public string SGhiChu
        {
            get => _sGhiChu;
            set
            {
                SetProperty(ref _sGhiChu, value);
            }
        }

        private string _sXauNoiMa;
        public string SXauNoiMa
        {
            get => _sXauNoiMa;
            set
            {
                SetProperty(ref _sXauNoiMa, value);
            }
        }

        private string _sLNS;
        public string SLNS
        {
            get => _sLNS;
            set
            {
                SetProperty(ref _sLNS, value);
            }
        }

        private int? _iNamLamViec;
        public int? INamLamViec
        {
            get => _iNamLamViec;
            set
            {
                SetProperty(ref _iNamLamViec, value);
            }
        }

        public DateTime? DNgayTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }
        public Guid IIDNoiDung { get; set; }
        public string STenNoiDung { get; set; }
        public Guid IdParent { get; set; }
        public string IIDMaDonVi { get; set; }
        public string STenDonVi { get; set; }
        public int IndexDataState { get; set; }
        public string Stt { get; set; }
        public bool IsFirstParentRow { get; set; }
        public int Level { get; set; }
        public string SM { get; set; }

        private bool _isAdd;
        public bool IsAdd
        {
            get => _isAdd;
            set => SetProperty(ref _isAdd, value);
        }
        public bool? BHangCha { get; set; }
        public decimal? DHeSoLCS { get; set; }
        public decimal? DHeSoBHYT { get; set; }
        public string SL { get; set; }
        public string SK { get; set; }
        public string STM { get; set; }
        public string STTM { get; set; }
        public string SNG { get; set; }
        public string STNG { get; set; }
        private bool _isAggregate;
        public bool IsAggregate
        {
            get => _isAggregate;
            set => SetProperty(ref _isAggregate, value);
        }
    }
}
