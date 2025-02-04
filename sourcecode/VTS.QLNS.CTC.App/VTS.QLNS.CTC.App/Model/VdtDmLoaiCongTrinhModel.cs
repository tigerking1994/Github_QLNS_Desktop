using Newtonsoft.Json;
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
    public class VdtDmLoaiCongTrinhModel : ModelBase
    {
        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value);
        }

        public Guid IIdLoaiCongTrinh { get; set; }
        public Guid? IIdParent { get; set; }

        private string _sMaLoaiCongTrinh;
        [DisplayName("Mã loại công trình")]
        [DisplayDetailInfo("Mã loại công trình")]
        public string SMaLoaiCongTrinh 
        {
            get => _sMaLoaiCongTrinh;
            set => SetProperty(ref _sMaLoaiCongTrinh, value);
        }

        private string _sTenVietTat;
        [DisplayName("Tên viết tắt")]
        [DisplayDetailInfo("Tên viết tắt")]
        public string STenVietTat 
        {
            get => _sTenVietTat;
            set => SetProperty(ref _sTenVietTat, value);
        }

        private string _sTenLoaiCongTrinh;
        [DisplayName("Tên loại công trình")]
        [DisplayDetailInfo("Tên loại công trình")]
        public string STenLoaiCongTrinh 
        {
            get => _sTenLoaiCongTrinh;
            set => SetProperty(ref _sTenLoaiCongTrinh, value);
        }

        private string _sMoTa;
        [DisplayName("Mô tả")]
        [DisplayDetailInfo("Mô tả")]
        public string SMoTa 
        {
            get => _sMoTa;
            set => SetProperty(ref _sMoTa, value);
        }

        public int? _iThuTu;
        [DisplayName("Thứ tự")]
        [DisplayDetailInfo("Thứ tự")]
        public int? IThuTu 
        {
            get => _iThuTu;
            set => SetProperty(ref _iThuTu, value);
        }

        private string _tenLoaiCongTrinhCha;
        [DisplayName("Tên loại công trình cha (F6)")]
        [DisplayDetailInfo("Tên loại công trình cha")]
        [TypeOfDialogAttribute(typeof(VdtDmLoaiCongTrinhModel), typeof(VdtDmLoaiCongTrinh), typeof(DmLoaiCongTrinhService), typeof(IDmLoaiCongTrinhService))]
        [MapperMethodAttribute("ConvertDmLoaiCongTrinhToDmLoaiCongTrinh")]
        [InitSelectedItemsMethodAttribute("SetSelectedLoaiCongTrinh")]
        [ColumnTypeAttribute(ColumnType.ReferencePopup)]
        [IsAllowMultipleSelectAttribute(false)]
        public string TenLoaiCongTrinhCha
        {
            get => _tenLoaiCongTrinhCha;
            set => SetProperty(ref _tenLoaiCongTrinhCha, value);
        }

        public bool? BActive { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SIdMaNguoiDungTao { get; set; }
        public int? ISoLanSua { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SIpsua { get; set; }
        public string SIdMaNguoiDungSua { get; set; }

        private string _lns;
        [DisplayName("LNS")]
        [DisplayDetailInfo("LNS")]
        public string Lns
        {
            get => _lns;
            set => SetProperty(ref _lns, value);
        }

        private string _l;
        [DisplayName("L")]
        [DisplayDetailInfo("L")]
        public string L
        {
            get => _l;
            set => SetProperty(ref _l, value);
        }

        private string _k;
        [DisplayName("K")]
        [DisplayDetailInfo("K")]
        public string K
        {
            get => _k;
            set => SetProperty(ref _k, value);
        }

        private string _m;
        [DisplayName("M")]
        [DisplayDetailInfo("M")]
        public string M
        {
            get => _m;
            set => SetProperty(ref _m, value);
        }

        private string _tm;
        [DisplayName("TM")]
        [DisplayDetailInfo("TM")]
        public string Tm
        {
            get => _tm;
            set => SetProperty(ref _tm, value);
        }

        private string _ttm;
        [DisplayName("TTM")]
        [DisplayDetailInfo("TTM")]
        public string Ttm
        {
            get => _ttm;
            set => SetProperty(ref _ttm, value);
        }

        private string _ng;
        [DisplayName("NG")]
        [DisplayDetailInfo("NG")]
        public string Ng
        {
            get => _ng;
            set => SetProperty(ref _ng, value);
        }

        private string _tng;
        [DisplayName("TNG")]
        [DisplayDetailInfo("TNG")]
        public string Tng
        {
            get => _tng;
            set => SetProperty(ref _tng, value);
        }

        private string _tng1;
        [DisplayName("TNG1")]
        [DisplayDetailInfo("TNG1")]
        public string Tng1
        {
            get => _tng1;
            set => SetProperty(ref _tng1, value);
        }

        private string _tng2;
        [DisplayName("TNG2")]
        [DisplayDetailInfo("TNG2")]
        public string Tng2
        {
            get => _tng2;
            set => SetProperty(ref _tng2, value);
        }

        private string _tng3;
        [DisplayName("TNG3")]
        [DisplayDetailInfo("TNG3")]
        public string Tng3
        {
            get => _tng3;
            set => SetProperty(ref _tng3, value);
        }

        [JsonIgnore]
        public VdtDmLoaiCongTrinhModel _parent;
        [JsonIgnore]
        public VdtDmLoaiCongTrinhModel Parent 
        {
            get => _parent;
            set
            {
                _parent = value;
                TenLoaiCongTrinhCha = value == null ? string.Empty : value.STenLoaiCongTrinh;
                OnPropertyChanged(nameof(IsHangCha));
            }
        }

        public bool IsHangCha
        {
            get => !IIdParent.HasValue;
        }

        [JsonIgnore]
        public virtual ICollection<VdtDmLoaiCongTrinhModel> Children { get; set; }
    }
}
