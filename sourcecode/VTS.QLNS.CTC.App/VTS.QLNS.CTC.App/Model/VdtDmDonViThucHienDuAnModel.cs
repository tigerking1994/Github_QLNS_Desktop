using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtDmDonViThucHienDuAnModel : ModelBase
    {
        public Guid IIdDonVi { get; set; }

        private string _iidMaDonVi;
        [DisplayName("Mã Đơn vị")]
        [DisplayDetailInfo("Mã Đơn vị")]
        public string IIdMaDonVi 
        {
            get => _iidMaDonVi;
            set => SetProperty(ref _iidMaDonVi, value); 
        }

        private string _sTenDonvi;
        [DisplayName("Tên Đơn vị")]
        [DisplayDetailInfo("Tên Đơn vị")]
        public string STenDonVi 
        {
            get => _sTenDonvi;
            set => SetProperty(ref _sTenDonvi, value);
        }

        public Guid? IIdDonViCha { get; set; }

        private string _sDiaChi;
        [DisplayName("Địa chỉ")]
        [DisplayDetailInfo("Địa chỉ")]
        public string SDiaChi 
        {
            get => _sDiaChi;
            set => SetProperty(ref _sDiaChi, value);
        }

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

        private int? _iCapDonVi;
        [DisplayName("Cấp đơn vị")]
        public int? ICapDonVi
        {
            get => _iCapDonVi;
            set => SetProperty(ref _iCapDonVi, value);
        }

        private string _sTenDonViCha;
        [DisplayName("Tên đơn vị cha (F6)")]
        [DisplayDetailInfo("Tên đơn vị cha")]
        [ColumnTypeAttribute(ColumnType.ReferencePopup)]
        public string TenDonViCha 
        {
            get => _sTenDonViCha;
            set => SetProperty(ref _sTenDonViCha, value);
        }

        public bool IsHangCha => BHangCha;
        public string IIDMaDonViNS { get; set; }

        private string _sTenDonViNS;
        [DisplayName("Đơn vị ngân sách (F6)")]
        [DisplayDetailInfo("Đơn vị ngân sách")]
        [ColumnTypeAttribute(ColumnType.ReferencePopup)]
        public string TenDonViNS
        {
            get => _sTenDonViNS;
            set => SetProperty(ref _sTenDonViNS, value);
        }

        private string _sKyHieu;
        [DisplayName("Ký hiệu")]
        [DisplayDetailInfo("Ký hiệu")]
        public string SKyHieu
        {
            get => _sKyHieu;
            set => SetProperty(ref _sKyHieu, value);
        }
    }
}
