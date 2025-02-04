using System;
using VTS.QLNS.CTC.App.Model.Import;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhQtcqKCBChiTietModel : DetailModelBase
    {
        public override Guid Id { get; set; }
        public Guid IIdQTCQuyKCB { get; set; }
        public Guid IIdMucLucNganSach { get; set; }
        public string SNoiDung { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }

        public Double? _fTienDuToanNamTruocChuyenSang;
        public Double? FTienDuToanNamTruocChuyenSang { get => _fTienDuToanNamTruocChuyenSang; set => SetProperty(ref _fTienDuToanNamTruocChuyenSang, value); }

        public Double? _fTienDuToanGiaoNamNay;
        public Double? FTienDuToanGiaoNamNay { get => _fTienDuToanGiaoNamNay; set => SetProperty(ref _fTienDuToanGiaoNamNay, value); }

        public Double? _fTienTongDuToanDuocGiao;
        public Double? FTienTongDuToanDuocGiao
        {
            get => _fTienTongDuToanDuocGiao = FTienDuToanGiaoNamNay.GetValueOrDefault(0) + FTienDuToanNamTruocChuyenSang.GetValueOrDefault(0);
            set => SetProperty(ref _fTienTongDuToanDuocGiao, value);
        }

        public Double? _fTienThucChi;
        public Double? FTienThucChi
        {
            get => _fTienThucChi = FTienQuyetToanDaDuyet.GetValueOrDefault(0) + FTienDeNghiQuyetToanQuyNay.GetValueOrDefault(0);
            set
            {
                SetProperty(ref _fTienThucChi, value);
            }
        }

        public Double? _fTienQuyetToanDaDuyet;
        public Double? FTienQuyetToanDaDuyet
        {
            get => _fTienQuyetToanDaDuyet;
            set
            {
                SetProperty(ref _fTienQuyetToanDaDuyet, value);
                OnPropertyChanged(nameof(FTienThucChi));
            }
        }

        public Double? _fTienDeNghiQuyetToanQuyNay;
        public Double? FTienDeNghiQuyetToanQuyNay
        {
            get => _fTienDeNghiQuyetToanQuyNay;
            set
            {
                SetProperty(ref _fTienDeNghiQuyetToanQuyNay, value);
                OnPropertyChanged(nameof(FTienThucChi));
            }
        }

        public Double? _fTienXacNhanQuyetToanQuyNay;
        public Double? FTienXacNhanQuyetToanQuyNay
        {
            get => _fTienXacNhanQuyetToanQuyNay;
            set
            {
                SetProperty(ref _fTienXacNhanQuyetToanQuyNay, value);
            }
        }

        public bool BHangCha { get; set; }
        public override bool IsEditable => !BHangCha && !IsDeleted;

        public Guid? IID_MLNS { get; set; }
        public Guid? IID_MLNS_Cha { get; set; }
        public string SXauNoiMa { get; set; }
        public string STenDonVi { get; set; }
        public string IIdMaDonVi { get; set; }
        public int? INamLamViec { get; set; }
        public string SLNS { get; set; }
        public string SL { get; set; }
        public string SK { get; set; }
        public string SM { get; set; }
        public string STM { get; set; }
        public string STTM { get; set; }
        public string SNG { get; set; }
        public string STNG { get; set; }
        public string SDuToanChiTietToi { get; set; }
        public bool IsHasData => FTienXacNhanQuyetToanQuyNay.GetValueOrDefault(0) != 0 || FTienDeNghiQuyetToanQuyNay.GetValueOrDefault(0) != 0;
        private string _sGhiChu;
        public string SGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
        }
    }
}
