using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhDmCauHinhThamSoModel : ModelBase
    {
        private string _sMa;
        [DisplayName("Mã cấu hình")]
        [DisplayDetailInfo("Mã cấu hình")]
        [Validate("Mã cấu hình", Utility.Enum.DATA_TYPE.String, true)]
        public string SMa
        {
            get => _sMa;
            set => SetProperty(ref _sMa, value);
        }

        private string _sTen;
        [DisplayName("Tên cấu hình")]
        [DisplayDetailInfo("Tên cấu hình")]
        [Validate("Tên cấu hình", Utility.Enum.DATA_TYPE.String, true)]
        public string STen
        {
            get => _sTen;
            set => SetProperty(ref _sTen, value);
        }

        private float _fGiaTri;
        [DisplayName("Giá trị")]
        [DisplayDetailInfo("Giá trị")]
        [FormatAttribute("{0:N4}")]
        public float fGiaTri
        {
            get => _fGiaTri;
            set => SetProperty(ref _fGiaTri, value);
        }

        private int? _bTrangThai;
        [DisplayName("Sử dụng")]
        [ColumnTypeAttribute(ColumnType.Combobox, "LoadComboboxData")]
        public int? BTrangThai
        {
            get => _bTrangThai;
            set => SetProperty(ref _bTrangThai, value);
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

        [DisplayDetailInfo("Ngày tạo")]
        public DateTime DNgayTao { get; set; }

        [DisplayDetailInfo("Người tạo")]
        public string SNguoiTao { get; set; }

        [DisplayDetailInfo("Ngày cập nhật")]
        public DateTime? DNgaySua { get; set; }

        [DisplayDetailInfo("Người cập nhật")]
        public string SNguoiSua { get; set; }
        public override bool IsEditable => !IsDeleted;
    }
}
