using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhPbdtcBHXHChiTietModel : DetailModelBase
    {
        public Guid IID_DTC_DuToanChiTrenGiao { get; set; }
        public Guid? IID_DTC_PhanBoDuToanChiTiet { get; set; }
        public Guid? IID_MLNS { get; set; }
        public Guid? IID_MLNS_Cha { get; set; }
        public string SLNS { get; set; }
        public string SL { get; set; }
        public string SK { get; set; }
        public string SM { get; set; }
        public string STM { get; set; }
        public string STTM { get; set; }
        public string SNG { get; set; }
        public string STNG { get; set; }
        public string STNG1 { get; set; }
        public string STNG2 { get; set; }
        public string STNG3 { get; set; }
        public string SXauNoiMa { get; set; }
        public string SNoiDung { get; set; }
        public Guid IID_DonVi { get; set; }
        public int? INamLamViec { get; set; }

        private string _iID_MaDonVi;
        public string IID_MaDonVi
        {
            get => _iID_MaDonVi;
            set => SetProperty(ref _iID_MaDonVi, value);
        }
        public string SSoQuyetDinh { get;set; }

        public Guid IID_LoaiCap { get; set; }
       

        private Double? _fTongTien;
        public Double? FTongTien
        {
            get => _fTongTien;
            set => SetProperty(ref _fTongTien, value);
        }

        private Double? _fTienTuChi;
        public Double? FTienTuChi
        {
            get => _fTienTuChi;
            set => SetProperty(ref _fTienTuChi, value);
        }

        private Double? _fTienTuChiTruocDieuChinh;
        public Double? FTienTuChiTruocDieuChinh {
            get => _fTienTuChiTruocDieuChinh;
            set => SetProperty(ref _fTienTuChiTruocDieuChinh, value);
        }

        private bool _isEnableTuChiTruocDieuChinh;

        public bool IsEnableTuChiTruocDieuChinh
        {
            get => _isEnableTuChiTruocDieuChinh;
            set => SetProperty(ref _isEnableTuChiTruocDieuChinh, value);
        }


        private Double? _fTienTuChiSauDieuChinh;
        public Double? FTienTuChiSauDieuChinh
        {
            get => _fTienTuChiSauDieuChinh;
            set => SetProperty(ref _fTienTuChiSauDieuChinh, value);
        }

        private bool _isEnableTuChiSauDieuChinh;

        public bool IsEnableTuChiSauDieuChinh
        {
            get => _isEnableTuChiSauDieuChinh;
            set => SetProperty(ref _isEnableTuChiSauDieuChinh, value);
        }

        private Double? _fTienHienVat;
        public Double? FTienHienVat
        {
            get => _fTienHienVat;
            set => SetProperty(ref _fTienHienVat, value);
        }

        private Double? _fTienHienVatTruocDieuChinh;
        public Double? FTienHienVatTruocDieuChinh
        {
            get => _fTienHienVatTruocDieuChinh;
            set => SetProperty(ref _fTienHienVatTruocDieuChinh, value);
        }

        private bool _isEnableHienVatTruocDieuChinh;

        public bool IsEnableHienVatTruocDieuChinh
        {
            get => _isEnableHienVatTruocDieuChinh;
            set => SetProperty(ref _isEnableHienVatTruocDieuChinh, value);
        }


        private Double? _fTienHienVatSauDieuChinh;
        public Double? FTienHienVatSauDieuChinh
        {
            get => _fTienHienVatSauDieuChinh;
            set => SetProperty(ref _fTienHienVatSauDieuChinh, value);
        }

        private bool _isEnableHienVatSauDieuChinh;

        public bool IsEnableHienVatSauDieuChinh
        {
            get => _isEnableHienVatSauDieuChinh;
            set => SetProperty(ref _isEnableHienVatSauDieuChinh, value);
        }

        public string SGhiChu { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }

        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }

        public bool BHangCha { get; set; }

        public bool IsRemainRow { get;set; }

        public string STenDonVi { get; set; }

        public int Type { get; set; }


        private ObservableCollection<ComboboxItem> _cbxNhanPhanBos;
        public ObservableCollection<ComboboxItem> CbxNhanPhanBos
        {
            get => _cbxNhanPhanBos;
            set => SetProperty(ref _cbxNhanPhanBos, value);
        }

        private ObservableCollection<ComboboxItem> _cbxDonVi;
        public ObservableCollection<ComboboxItem> CbxDonVi
        {
            get => _cbxDonVi;
            set => SetProperty(ref _cbxDonVi, value);
        }

        public bool BEmty { get; set; }

        public bool IsDisableField {  get; set; }
        public override bool IsEditable => !BHangCha && !IsDeleted && !IsDisableField;

        public bool IsEmptyPlanData => FTienTuChi.GetValueOrDefault() == 0;
        public string SCPChiTietToi { get; set; }
        public string SDuToanChiTietToi { get; set; }
        public string SMaLoaiChi { get; set; }
    }
}