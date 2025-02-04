using System;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhThamDinhQuyetToanChiTietModel : ModelBase
    {
        public int IMa { get; set; }
        public int IMaCha { get; set; }
        public int ILoai { get; set; }
        public string SSTT { get; set; }
        public int ISTT { get; set; }
        public string SNoiDung { get; set; }
        public string SNoiDungDisplay => $"{SSTT} {SNoiDung}";
        public string SXauNoiMa { get; set; }
        public int IKieuChu { get; set; }
        public int INamLamViec { get; set; }
        public bool ITrangThai { get; set; }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }
        public Guid IID_BH_TDQT_ChungTuChiTiet { get; set; }
        public string STenDonVi { get; set; }
        public string IID_MaDonVi { get; set; }
        private double _fSoBaoCao;
        public double FSoBaoCao
        {
            get => _fSoBaoCao;
            set
            {
                SetProperty(ref _fSoBaoCao, value);
                OnPropertyChanged(nameof(FChenhLech));
            }
        }

        private double _fSoThamDinh;
        public double FSoThamDinh
        {
            get => _fSoThamDinh;
            set
            {
                SetProperty(ref _fSoThamDinh, value);
                OnPropertyChanged(nameof(FChenhLech));
            }
        }
        public double FChenhLech => Math.Abs(FSoThamDinh - FSoBaoCao);
        private double _fQuanNhan;
        public double FQuanNhan
        {
            get => _fQuanNhan;
            set
            {
                SetProperty(ref _fQuanNhan, value);
                OnPropertyChanged(nameof(FTongSo));
            }
        }
        private double _fCNVLDHD;
        public double FCNVLDHD
        {
            get => _fCNVLDHD;
            set
            {
                SetProperty(ref _fCNVLDHD, value);
                OnPropertyChanged(nameof(FTongSo));
            }
        }
        public double FTongSo => FQuanNhan + FCNVLDHD;
        private string _sGhiChu;
        public string SGhiChu
        {
            get => _sGhiChu;
            set
            {
                SetProperty(ref _sGhiChu, value);
            }
        }
        public bool HasData => FSoBaoCao != 0 || FSoThamDinh != 0 || FQuanNhan != 0 || FCNVLDHD != 0 || !string.IsNullOrEmpty(SGhiChu);

        private double _fKinhPhiQL;
        public double FKinhPhiQL
        {
            get => _fKinhPhiQL;
            set
            {
                SetProperty(ref _fKinhPhiQL, value);
            }
        }

        private double _fKinhPhiKCBQuanY;
        public double FKinhPhiKCBQuanY
        {
            get => _fKinhPhiKCBQuanY;
            set
            {
                SetProperty(ref _fKinhPhiKCBQuanY, value);
            }
        }

        private double _fKinhPhiKCBQuanNhan;
        public double FKinhPhiKCBQuanNhan
        {
            get => _fKinhPhiKCBQuanNhan;
            set
            {
                SetProperty(ref _fKinhPhiKCBQuanNhan, value);
            }
        }

        private double _fTongCong;
        public double FTongCong
        {
            get => _fTongCong;
            set
            {
                SetProperty(ref _fTongCong, value);
            }
        }

        public bool IsTitle {  get; set; }
        public override bool IsEditable => !IsHangCha && !IsDeleted && !IsTitle;
        public bool HasReportData => FSoBaoCao != 0;
        public bool HasEvaluationData => FSoThamDinh != 0;
        public bool HasDiffData => FChenhLech != 0;
        public bool HasReportNEvaluateDate => FSoBaoCao != 0 && FSoThamDinh != 0;
    }
}
