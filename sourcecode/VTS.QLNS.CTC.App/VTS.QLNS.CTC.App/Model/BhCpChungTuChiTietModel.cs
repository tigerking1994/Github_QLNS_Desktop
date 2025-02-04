using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhCpChungTuChiTietModel : ModelBase
    {
        public override Guid Id { get; set; }
        private Guid? _iID_CP_ChungTu;
        public Guid? IID_CP_ChungTu { get => _iID_CP_ChungTu; set => SetProperty(ref _iID_CP_ChungTu, value); }
        private Guid? _iID_MucLucNganSach;
        public Guid? IID_MucLucNganSach
        {
            get => _iID_MucLucNganSach;
            set => SetProperty(ref _iID_MucLucNganSach, value);
        }
        private string _sM;
        public string SM
        {
            get => _sM;
            set => SetProperty(ref _sM, value);
        }
        private string _sTM;
        public string STM
        {
            get => _sTM;
            set => SetProperty(ref _sTM, value);
        }
        private string _sNoiDung;
        public string SNoiDung
        {
            get => _sNoiDung;
            set => SetProperty(ref _sNoiDung, value);
        }
        private double? _fTienDuToan;
        public double? FTienDuToan
        {
            get => _fTienDuToan;
            set => SetProperty(ref _fTienDuToan, value);
        }
        private double? _fTienKeHoachCap;
        public double? FTienKeHoachCap
        {
            get => _fTienKeHoachCap;
            set
            {
                SetProperty(ref _fTienKeHoachCap, value);
                OnPropertyChanged(nameof(FTienDaCap));
            }
        }
        public double? FTienDaCap
        {
            get
            {
                if (!IsHangCha) return FTienDaCaQuyTruoc.GetValueOrDefault(0) + FTienKeHoachCap.GetValueOrDefault(0);
                else return FTienDaCaQuyTruoc.GetValueOrDefault(0);
            }
        }

        private double? _fConLai;
        public double? FConLai
        {
            get => _fConLai;
            set => SetProperty(ref _fConLai, value);
        }
        public double? FTienKeHoachCapQuyTruoc { get; set; }
        public double? FConLaiChange => FTienDuToan.GetValueOrDefault() - (FTienKeHoachCapQuyTruoc.GetValueOrDefault(0) + FTienKeHoachCap.GetValueOrDefault(0));
        private Guid? _iID_DonVi;
        public Guid? IID_DonVi
        {
            get => _iID_DonVi;
            set => SetProperty(ref _iID_DonVi, value);
        }
        private string _iID_MaDonVi;
        public string IID_MaDonVi
        {
            get => _iID_MaDonVi;
            set => SetProperty(ref _iID_MaDonVi, value);
        }
        private string _sGhiChu;
        public string SGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
        }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgayTao { get; set; }
        public DateTime? DNgaySua { get; set; }

        public bool IsAuToFillTuChi { get; set; }
        public string SXauNoiMa { get; set; }
        public int? INamLamViec { get; set; }
        public string SMoTa { get; set; }
        private bool _isAdd;
        public bool IsAdd
        {
            get => _isAdd;
            set => SetProperty(ref _isAdd, value);
        }
        public Guid IdParent { get; set; }
        public string STenDonVi { get; set; }
        public bool BHangCha { get; set; }
        public string SMaTenDonVi => IID_MaDonVi + " - " + STenDonVi;
        public string STTM { get; set; }
        public double? FTienDaCaQuyTruoc { get; set; }
        public bool IsHasData => FTienDaCap.GetValueOrDefault() != 0 || FTienDuToan.GetValueOrDefault() != 0 || FTienKeHoachCap.GetValueOrDefault() != 0;
    }
}
