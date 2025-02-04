using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlDmDonViModel : ModelBase
    {
        private string _maDonVi;
        [DisplayName("Mã đơn vị")]
        [DisplayDetailInfo("Mã đơn vị")]
        public string MaDonVi 
        {
            get => _maDonVi;
            set => SetProperty(ref _maDonVi, value);
        }

        private string _tenDonVi;
        [DisplayName("Tên đơn vị")]
        [DisplayDetailInfo("Tên đơn vị")]
        public string TenDonVi
        {
            get => _tenDonVi;
            set => SetProperty(ref _tenDonVi, value); 
        }

        private string _tenDonViCha;
        //[DisplayName("Đơn vị cha (F6)")]
        //[DisplayDetailInfo("Đơn vị cha")]
        //[ColumnTypeAttribute(ColumnType.ReferencePopup)]
        public string TenDonViCha
        {
            get => _tenDonViCha;
            set => SetProperty(ref _tenDonViCha, value);
        }

        private string _parentId;
        public string ParentId
        {
            get => _parentId;
            set
            {
                SetProperty(ref _parentId, value);
                OnPropertyChanged(nameof(IsHangCha));
            }
        }

        private string _xauNoiMa;
        public string XauNoiMa
        {
            get => _xauNoiMa;
            set => SetProperty(ref _xauNoiMa, value);
        }

        public string MaTenDonVi => MaDonVi + " - " + TenDonVi;

        public override bool IsHangCha => BHangCha;

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

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        private bool? _iTrangThai;
        [DisplayName("Trạng thái hoạt động")]
        [ColumnType(ColumnType.Checkbox)]
        public bool? ITrangThai 
        {
            get => _iTrangThai;
            set => SetProperty(ref _iTrangThai, value);
        }

        public ICollection<TlDmCanBo> TlDmCanBos { get; set; }
    }
}
