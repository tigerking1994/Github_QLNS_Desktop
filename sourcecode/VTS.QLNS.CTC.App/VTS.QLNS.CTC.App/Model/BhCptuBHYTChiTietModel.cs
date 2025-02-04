using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhCptuBHYTChiTietModel : DetailModelBase
    {
        public Guid Id { get; set; }
        public Guid? IID_BH_CP_CapTamUng_KCB_BHYT { get; set; }

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
        public string SMoTa { get; set; }
        private string _sGhiChu;
        public string SGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
        }

        private Double? _fQTQuyTruoc;
        public Double? FQTQuyTruoc
        {
            get => _fQTQuyTruoc;
            set
            {
                SetProperty(ref _fQTQuyTruoc, value);
                OnPropertyChanged(nameof(FPhaiCapTamUngQuyNay));
            }
        }

        private Double? _fTamUngQuyNay;
        public Double? FTamUngQuyNay
        {
            get => _fTamUngQuyNay;
            set
            {
                SetProperty(ref _fTamUngQuyNay, value);
            }
        }
        private Double? _fLuyKeCapDenCuoiQuy;

        public Double? FLuyKeCapDenCuoiQuy
        {
            get => _fLuyKeCapDenCuoiQuy;
            set
            { SetProperty(ref _fLuyKeCapDenCuoiQuy, value); }
        }
        private Double? _fLuyKeCapCacQuyTruoc;
        public Double? FLuyKeCapCacQuyTruoc
        {
            get => _fLuyKeCapCacQuyTruoc;
            set
            {
                SetProperty(ref _fLuyKeCapCacQuyTruoc, value);
            }
        }

        public Double? FluyKeCap => FLuyKeCapCacQuyTruoc.GetValueOrDefault() + FTamUngQuyNay.GetValueOrDefault();

        private Double? _fCapThuaQuyTruocChuyenSang;
        public Double? FCapThuaQuyTruocChuyenSang
        {
            get => _fCapThuaQuyTruocChuyenSang;
            set
            {
                SetProperty(ref _fCapThuaQuyTruocChuyenSang, value);
            }
        }

        public Double? FPhaiCapTamUngQuyNay => FTamUngQuyNay.GetValueOrDefault() - FCapThuaQuyTruocChuyenSang.GetValueOrDefault();
        public Guid? IID_CoSoYTe { get; set; }
        public string IID_MaCoSoYTe { get; set; }
        public bool BHangCha { get; set; }
        public string STenCoSoYTe { get; set; }
        public override bool IsEditable => !BHangCha && !IsDeleted;
        public string IIDMaDonVi { get; set; }
        public int? INamLamViec { get; set; }
        public Guid IIdFilter { get; set; } = Guid.NewGuid();
        public bool BHasData => !NumberUtils.DoubleIsNullOrZero(FQTQuyTruoc) || !NumberUtils.DoubleIsNullOrZero(FTamUngQuyNay);

    }
}
