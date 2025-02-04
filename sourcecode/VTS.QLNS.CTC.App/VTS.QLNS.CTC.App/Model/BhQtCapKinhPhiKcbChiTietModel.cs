using System;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhQtCapKinhPhiKcbChiTietModel : DetailModelBase
    {
        public Guid Id { get; set; }
        public Guid IIDCTCapKinhPhiKCB { get; set; }

        private Guid _iIdMlns;
        public Guid IIdMlns
        {
            get => _iIdMlns;
            set => SetProperty(ref _iIdMlns, value);
        }

        private Guid _iIdMlnsCha;
        public Guid IIdMlnsCha
        {
            get => _iIdMlnsCha;
            set => SetProperty(ref _iIdMlnsCha, value);
        }

        private string _sXauNoiMa;
        public string SXauNoiMa
        {
            get => _sXauNoiMa;
            set => SetProperty(ref _sXauNoiMa, value);
        }

        private string _sLns;
        public string SLns
        {
            get => _sLns;
            set => SetProperty(ref _sLns, value);
        }
        public string STenMLNS { get; set; }
        public Guid IIDCoSoYTe { get; set; }
        public string sMaCoSoYTe { get; set; }
        public string STenCoSoYTe { get; set; }
        public string SGhiChu { get; set; }
        private double? _fKeHoachCap;
        public double? FKeHoachCap
        {
            get => _fKeHoachCap;
            set
            {
                SetProperty(ref _fKeHoachCap, value);
                OnPropertyChanged(nameof(FConLai));
            }
        }

        private double? _fDaQuyetToan;
        public double? FDaQuyetToan
        {
            get => _fDaQuyetToan;
            set => SetProperty(ref _fDaQuyetToan, value);
        }

        public double? FConLai => FQuyetToanQuyNay - FKeHoachCap;

        private double? _fQuyetToanQuyNay;
        public double? FQuyetToanQuyNay
        {
            get => _fQuyetToanQuyNay;
            set
            {
                SetProperty(ref _fQuyetToanQuyNay, value);
                OnPropertyChanged(nameof(FConLai));
            }
        }

        private double? _fQuyetToan4Quy;
        public double? FQuyetToan4Quy
        {
            get => _fQuyetToan4Quy;
            set
            {
                SetProperty(ref _fQuyetToan4Quy, value);
            }
        }

        public bool BHangCha { get; set; }
        public bool HasData => !IsHangCha && (FKeHoachCap != 0 || FDaQuyetToan != 0 || FConLai != 0 || FQuyetToanQuyNay != 0 || !string.IsNullOrEmpty(SGhiChu));

        private bool _isAdd;
        public bool IsAdd
        {
            get => _isAdd;
            set => SetProperty(ref _isAdd, value);
        }
    }
}
