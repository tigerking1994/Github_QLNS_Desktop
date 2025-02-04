using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.ViewModel;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class AllocationModel : BindableBase
    {
        public Guid Id { get; set; }
        public string SoChungTu { get; set; }
        public int? SoChungTuIndex { get; set; }
        public DateTime? NgayChungTu { get; set; }
        public string NgayChungTuString { get; set; }
        public string SoQuyetDinh { get; set; }
        public DateTime? NgayQuyetDinh { get; set; }
        public string NgayQuyetDinhString { get; set; }
        public string MoTa { get; set; }
        public string IdDonVi { get; set; }
        public string TenDonVi { get; set; }
        public string Lns { get; set; }
        public int IType { get; set; }
        public string ITypeMoTa { get; set; }
        public string ILoai { get; set; }
        public string ILoaiMoTa { get; set; }
        public int? NamLamViec { get; set; }
        public int? ITrangThai { get; set; }
        public string ChiTietToi { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreator { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModifier { get; set; }
        public string Tag { get; set; }
        public string Log { get; set; }

        private bool _isLocked;
        public bool IsLocked {
            get => _isLocked;
            set => SetProperty(ref _isLocked, value);
        }
        public int? NguonNganSach { get; set; }
        public int? NamNganSach { get; set; }
        public string IdDonViTao { get; set; }
        public string IGuiNhan { get; set; }

        public string TenLoai { get; set; }
        public double? SoTuChi { get; set; }

        private double _tongDuToan;
        public double TongDuToan
        {
            get => _tongDuToan;
            set => SetProperty(ref _tongDuToan, value);
        }

        private double _tongDaCap;
        public double TongDaCap
        {
            get => _tongDaCap;
            set => SetProperty(ref _tongDaCap, value);
        }

        private double _tongConLai;
        public double TongConLai
        {
            get => _tongConLai;
            set => SetProperty(ref _tongConLai, value);
        }

        private double _tongCapPhat;
        public double TongCapPhat
        {
            get => _tongCapPhat;
            set => SetProperty(ref _tongCapPhat, value);
        }

        public string LoaiCap { get; set; }

        public string TenLoaiCap
        {
            get
            {
                return Utility.LoaiCap.GetName(LoaiCap);
            }
        }

        private double _tongDonViDeNghi;
        public double TongDonViDeNghi
        {
            get => _tongDonViDeNghi;
            set => SetProperty(ref _tongDonViDeNghi, value);
        }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        public string IIdMaDmcapPhat { get; set; }
        public string DSSoChungTuTongHop { get; set; }
        public bool IsChildSumary { get; set; }

        private bool _isExpand;
        public bool IsExpand
        {
            get => _isExpand;
            set => SetProperty(ref _isExpand, value);
        }

        private bool _isCollapse;
        public bool IsCollapse
        {
            get => _isCollapse;
            set => SetProperty(ref _isCollapse, value);
        }

        public string SoChungTuParent { get; set; }

        private bool? _bDaTongHop;
        public bool? BDaTongHop
        {
            get => _bDaTongHop.GetValueOrDefault(false);
            set => SetProperty(ref _bDaTongHop, value);
        }
        public double ? SoCapPhat { get; set; }
        public string BDaTongHopString => BDaTongHop.GetValueOrDefault(false) ? "Đã tổng hợp" : "";
    }
}
