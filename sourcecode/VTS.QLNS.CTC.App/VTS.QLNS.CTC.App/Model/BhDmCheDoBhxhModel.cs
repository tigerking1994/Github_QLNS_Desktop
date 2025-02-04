using System;
using System.ComponentModel;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhDmCheDoBhxhModel : ModelBase
    {
        private string _iIdMaCheDo;
        [DisplayName("Mã danh mục")]
        [DisplayDetailInfo("Mã danh mục")]
        [Validate("Mã danh mục", Utility.Enum.DATA_TYPE.String, true)]
        public string IIdMaCheDo
        {
            get => _iIdMaCheDo;
            set => SetProperty(ref _iIdMaCheDo, value);
        }

        private string _sTenCheDo;
        [DisplayName("Tên danh mục")]
        [DisplayDetailInfo("Tên danh mục")]
        [Validate("Tên danh mục", Utility.Enum.DATA_TYPE.String, true)]
        public string STenCheDo
        {
            get => _sTenCheDo;
            set => SetProperty(ref _sTenCheDo, value);
        }

        private int? _iTrangThai;
        [DisplayName("Trạng thái")]
        [ColumnTypeAttribute(ColumnType.Combobox, "LoadComboboxData")]
        public int? ITrangThai
        {
            get => _iTrangThai;
            set => SetProperty(ref _iTrangThai, value);
        }

        public Guid? IIdCheDoCha { get; set; }

        private string _sTenCheDoCha;
        [DisplayName("Danh mục cha")]
        [DisplayDetailInfo("Danh mục cha")]
        [ColumnTypeAttribute(ColumnType.ReferencePopup)]
        public string TenCheDoCha
        {
            get => _sTenCheDoCha;
            set => SetProperty(ref _sTenCheDoCha, value);
        }

        public string SXauNoiMa { get; set; }

        [DisplayDetailInfo("Ngày tạo")]
        public DateTime DNgayTao { get; set; }

        [DisplayDetailInfo("Người tạo")]
        public string SNguoiTao { get; set; }

        [DisplayDetailInfo("Ngày cập nhật")]
        public DateTime? DNgaySua { get; set; }

        [DisplayDetailInfo("Người cập nhật")]
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

        // Do not override
        public bool IsHangCha => BHangCha;
    }
}
