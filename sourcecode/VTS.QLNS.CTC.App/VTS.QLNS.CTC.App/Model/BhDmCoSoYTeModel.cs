using System;
using System.ComponentModel;
using System.Windows;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhDmCoSoYTeModel : DetailModelBase
    {
        public Guid Id { get; set; }
        private string _iIDMaCoSoYTe;
        [DisplayName("Mã cơ sở y tế")]
        [DisplayDetailInfo("Mã cơ sở y tế")]
        [Validate("Mã cơ sở y tế", Utility.Enum.DATA_TYPE.String, true)]
        public string IIDMaCoSoYTe
        {
            get => _iIDMaCoSoYTe;
            set => SetProperty(ref _iIDMaCoSoYTe, value);
        }
        private string _sTenCoSoYTe;
        [DisplayName("Tên cơ sở y tế")]
        [DisplayDetailInfo("Tên cơ sở y tế")]
        [Validate("Tên cơ sở y tế", Utility.Enum.DATA_TYPE.String, true)]
        public string STenCoSoYTe
        {
            get => _sTenCoSoYTe;
            set => SetProperty(ref _sTenCoSoYTe, value);
        }
        public int? INamLamViec { get; set; }
        private int? _iTrangThai;
        [DisplayName("Trạng thái")]
        [ColumnTypeAttribute(ColumnType.Combobox, "LoadComboboxData")]
        [HorizontalAttribute(HorizontalAlignment.Center)]
        public int? ITrangThai
        {
            get => _iTrangThai;
            set => SetProperty(ref _iTrangThai, value);
        }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }
        public string Display => String.Format("{0} - {1}", IIDMaCoSoYTe, STenCoSoYTe);
    }
}
