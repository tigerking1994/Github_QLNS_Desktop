using System.ComponentModel;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using System.Windows;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhDmThamDinhQuyetToanModel : ModelBase
    {
        private int _iMa;
        [DisplayName("Mã")]
        [DisplayDetailInfo("Mã")]
        [Validate("Mã", Utility.Enum.DATA_TYPE.Int, true)]
        public int IMa
        {
            get => _iMa;
            set => SetProperty(ref _iMa, value);
        }

        private int? _iSTT;
        [DisplayName("STT")]
        [DisplayDetailInfo("STT")]
        [Validate("STT", Utility.Enum.DATA_TYPE.Int, true)]
        public int? ISTT
        {
            get => _iSTT;
            set => SetProperty(ref _iSTT, value);
        }


        private string _sSTT;
        [DisplayName("Chỉ mục")]
        [DisplayDetailInfo("Chỉ mục")]
        [Validate("Chỉ mục", Utility.Enum.DATA_TYPE.String, true)]
        public string SSTT
        {
            get => _sSTT;
            set => SetProperty(ref _sSTT, value);
        }



        private string _sNoiDung;
        [DisplayName("Nội dung")]
        [DisplayDetailInfo("Nội dung")]
        [Validate("Nội dung", Utility.Enum.DATA_TYPE.String, true)]
        public string SNoiDung
        {
            get => _sNoiDung;
            set => SetProperty(ref _sNoiDung, value);
        }

        private int? _iMaCha;
        [DisplayName("Mã cha")]
        [DisplayDetailInfo("Mã cha")]
        //[TypeOfDialogAttribute(typeof(BhDmThamDinhQuyetToanModel), typeof(BhDmThamDinhQuyetToan), typeof(BhDmThamDinhQuyetToanService), typeof(IBhDmThamDinhQuyetToanService))]
        //[ColumnTypeAttribute(ColumnType.ReferencePopup)]
        //[IsAllowMultipleSelectAttribute(true)]
        [Validate("Mã cha", Utility.Enum.DATA_TYPE.Int, false)]
        public int? IMaCha
        {
            get => _iMaCha;
            set => SetProperty(ref _iMaCha, value);

        }
        public string SXauNoiMa { get; set; }

        private int _iKieuChu;
        [DisplayName("Kiểu chữ")]
        [ColumnTypeAttribute(ColumnType.Combobox, "LoadComboboxData")]
        [HorizontalAttribute(HorizontalAlignment.Center)]
        public new int IKieuChu
        {
            get => _iKieuChu;
            set => SetProperty(ref _iKieuChu, value);
        }
        //public int? IMaCha { get; set; }

        public int INamLamViec { get; set; }
        public bool ILock { get; set; }
        private bool _iTrangThai;
        [DisplayName("Trạng thái")]
        [ColumnTypeAttribute(ColumnType.Combobox, "LoadComboboxData")]
        [HorizontalAttribute(HorizontalAlignment.Center)]
        public bool ITrangThai
        {
            get => _iTrangThai;
            set => SetProperty(ref _iTrangThai, value);
        }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }
    }
}
