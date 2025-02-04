using System;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhDtctgBHXHChiTietModel : DetailModelBase
    {
        public Guid _ID;
        public Guid ID { get => _ID; set => SetProperty(ref _ID, value); }
        public Guid IID_DTC_DuToanChiTrenGiao { get; set; }
        public Guid? IID_MucLucNganSach { get; set; }

        public double? _fTienHienVat;
        public double? FTienHienVat { get => _fTienHienVat; set => SetProperty(ref _fTienHienVat, value); }

        public double? _fTienTuChi;
        public double? FTienTuChi { get => _fTienTuChi; set => SetProperty(ref _fTienTuChi, value); }
        public double? _fTongTien;
        public double? FTongTien { get => _fTongTien; set => SetProperty(ref _fTongTien, value); }

        public string SLNS { get; set; }
        public string SL { get; set; }
        public string SK { get; set; }
        public string STM { get; set; }
        public string STTM { get; set; }
        public string SM { get; set; }
        public string SNoiDung { get; set; }
        public string SNG { get; set; }
        public string SXauNoiMa { get; set; }
        public string IIDMaDonVi { get; set; }
        public int INamLamViec { get; set; }

        public Guid? IID_MLNS { get; set; }
        public Guid? IID_MLNS_Cha { get; set; }

        private bool _bHangCha;
        public bool BHangCha
        {
            get => _bHangCha;
            set => SetProperty(ref _bHangCha, value);
        }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        public bool IsUpdate { get; set; }
        public bool IsDisableField { get; set; }
        public override bool IsEditable => !IsHangCha && !IsDeleted && !IsDisableField;
        public bool IsHadData => FTongTien.GetValueOrDefault(0) != 0;
        public string SCPChiTietToi { get; set; }
        public string SDuToanChiTietToi { get; set; }

        public double? FTienTangGiam { get; set; }

        public bool IsRemainRow { get; set; }

        public int IRemainRow { get; set; }
        public double? FTienGiaoDuToan { get; set; }
        public bool? BHangChaDuToan { get; set; }
        public string SMaLoaiChi { get; set; }

        private bool _isChildSummary;
        public bool IsChildSummary
        {
            get => _isChildSummary;
            set => SetProperty(ref _isChildSummary, value);
        }

        private bool _isCollapse;
        public bool IsCollapse
        {
            get => _isCollapse;
            set => SetProperty(ref _isCollapse, value);
        }

        private bool _isExpand = true;
        public bool IsExpand
        {
            get => _isExpand;
            set => SetProperty(ref _isExpand, value);
        }

        private bool _isDisabled = true;
        public bool IsDisabled
        {
            get => _isDisabled;
            set => SetProperty(ref _isDisabled, value);
        }

        public int Level { get; set; }

        public double? _fTienTuChiTrenGiao;
        public double? FTienTuChiTrenGiao { get => _fTienTuChiTrenGiao; set => SetProperty(ref _fTienTuChiTrenGiao, value); }
        public double? _fTienKeHoach;
        public double? FTienKeHoach { get => _fTienKeHoach; set => SetProperty(ref _fTienKeHoach, value); }
        public double? _fTienBoSung;
        public double? FTienBoSung { get => _fTienBoSung; set => SetProperty(ref _fTienBoSung, value); }
    }
}