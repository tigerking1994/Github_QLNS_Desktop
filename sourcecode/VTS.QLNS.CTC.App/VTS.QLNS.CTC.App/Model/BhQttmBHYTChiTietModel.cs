using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhQttmBHYTChiTietModel : DetailModelBase
    {
        public Guid Id { get; set; }
        public Guid? VoucherID { get; set; }
        public int? INamLamViec { get; set; }
        private double? _fDuToan;
        public double? FDuToan
        {
            get => _fDuToan;
            set
            {
                SetProperty(ref _fDuToan, value);
            }
        }
        private double? _fDaQuyetToan;
        public double? FDaQuyetToan
        {
            get => _fDaQuyetToan;
            set
            {
                SetProperty(ref _fDaQuyetToan, value);
            }
        }
        public double? FConLai => FDuToan - FDaQuyetToan - FSoPhaiThu;
        private double? _fSoPhaiThu;
        public double? FSoPhaiThu
        {
            get => _fSoPhaiThu;
            set
            {
                SetProperty(ref _fSoPhaiThu, value);
                OnPropertyChanged(nameof(FConLai));
            }
        }

        private string _fGhiChu;
        public string SGhiChu
        {
            get => _fGhiChu;
            set
            {
                SetProperty(ref _fGhiChu, value);
            }
        }
        public string IIDMaDonVi { get; set; }
        public string STenDonVi { get; set; }
        public Guid IIDMLNS { get; set; }
        public Guid IIDMLNSCha { get; set; }
        public string SXauNoiMa { get; set; }
        public string SLns { get; set; }
        public string STenBhMLNS { get; set; }
        private bool _isAdd;
        public bool IsAdd
        {
            get => _isAdd;
            set => SetProperty(ref _isAdd, value);
        }
        public bool? BHangCha { get; set; }
        public bool IsHasData => FDuToan != 0 || FDaQuyetToan != 0 || FConLai != 0 || FSoPhaiThu != 0 ||!string.IsNullOrEmpty(SGhiChu);
        public string SL { get; set; }
        public string SK { get; set; }
        public string SM { get; set; }
        public string STM { get; set; }
        public string STTM { get; set; }
        public string SNG { get; set; }
        public string STNG { get; set; }
        public string SQuyNamMoTa { get; set; }
        public DateTime DNgayChungTu { get; set; }
    }
}
