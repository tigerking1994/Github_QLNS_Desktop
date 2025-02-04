using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class DmChuDauTuModel : ModelBase
    {
        public Guid? IIDDonViCha { get; set; }

        private string _iIDMaDonVi;
        [DisplayName("Mã chủ đầu tư")]
        [DisplayDetailInfo("Mã chủ đầu tư")]
        public string IIDMaDonVi
        {
            get => _iIDMaDonVi;
            set => SetProperty(ref _iIDMaDonVi, value);
        }

        private string _sTenDonVi;
        [DisplayName("Tên chủ đầu tư")]
        [DisplayDetailInfo("Tên chủ đầu tư")]
        public string STenDonVi 
        {
            get => _sTenDonVi;
            set => SetProperty(ref _sTenDonVi, value);
        }

        private string _sKyHieu;
        [DisplayName("Ký hiệu")]
        [DisplayDetailInfo("Ký hiệu")]
        public string SKyHieu 
        {
            get => _sKyHieu;
            set => SetProperty(ref _sKyHieu, value);
        }

        private string _sMoTa;
        [DisplayName("Mô tả")]
        [DisplayDetailInfo("Mô tả")]
        public string SMoTa 
        {
            get => _sMoTa;
            set => SetProperty(ref _sMoTa, value);
        }

        public int? INamLamViec { get; set; }
        public int? ITrangThai { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }

        private bool _bHangCha;
        public bool BHangCha
        {
            get => _bHangCha;
            set
            {
                SetProperty(ref _bHangCha, value);
                OnPropertyChanged(nameof(IsHangCha));
            }
        }

        public bool IsHangCha => BHangCha;

        private string _tenCdtParent;
        [DisplayName("Chủ đầu tư cha (F6)")]
        [DisplayDetailInfo("Chủ đầu tư cha")]
        [TypeOfDialogAttribute(typeof(DmChuDauTuModel), typeof(DmChuDauTu), typeof(DmChuDauTuService), typeof(IDmChuDauTuService))]
        [MapperMethodAttribute("MapDmChuDauTuParentToChuDauTu")]
        [InitSelectedItemsMethodAttribute("SetSelectedDmChuDauTu")]
        [ColumnTypeAttribute(ColumnType.ReferencePopup)]
        [IsAllowMultipleSelectAttribute(false)]
        public string TenCdtParent
        {
            get => _tenCdtParent;
            set => SetProperty(ref _tenCdtParent, value);
        }

        public string TenDonViDisplay => string.Concat(IIDMaDonVi, " - ", STenDonVi);

        private string _maSoDVSDNS;
        [DisplayName("Mã số ĐVSDNS")]
        [DisplayDetailInfo("Mã số ĐVSDNS")]
        public string MaSoDVSDNS
        {
            get => _maSoDVSDNS;
            set => SetProperty(ref _maSoDVSDNS, value);
        }

        private string _sTKTrongNuoc;
        [DisplayName("STK trong nước")]
        [DisplayDetailInfo("STK trong nước")]
        public string STKTrongNuoc 
        {
            get => _sTKTrongNuoc;
            set => SetProperty(ref _sTKTrongNuoc, value);
        }

        private string _chiNhanhTrongNuoc;
        [DisplayName("Chi nhánh trong nước")]
        [DisplayDetailInfo("Chi nhánh trong nước")]
        public string ChiNhanhTrongNuoc 
        {
            get => _chiNhanhTrongNuoc;
            set => SetProperty(ref _chiNhanhTrongNuoc, value);
        }

        private string _sTKNuocNgoai;
        [DisplayName("STK nước ngoài")]
        [DisplayDetailInfo("STK nước ngoài")]
        public string STKNuocNgoai 
        { 
            get => _sTKNuocNgoai;
            set => SetProperty(ref _sTKNuocNgoai, value);
        }

        private string _chiNhanhNuocNgoai;
        [DisplayName("Chi nhánh nước ngoài")]
        [DisplayDetailInfo("Chi nhánh nước ngoài")]
        public string ChiNhanhNuocNgoai 
        {
            get => _chiNhanhNuocNgoai;
            set => SetProperty(ref _chiNhanhNuocNgoai, value);
        }

        public string SMaCdtTenCdt => IIDMaDonVi + " - " + STenDonVi;
    }
}
