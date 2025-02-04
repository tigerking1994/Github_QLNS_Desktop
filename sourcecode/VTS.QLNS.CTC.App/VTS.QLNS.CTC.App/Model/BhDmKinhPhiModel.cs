using System;
using System.ComponentModel;
using VTS.QLNS.CTC.App.Extensions;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhDmKinhPhiModel : ModelBase
    {
        private string _maKinhPhi;
        [DisplayName("Mã loại kinh phí")]
        [DisplayDetailInfo("Mã loại kinh phí")]
        [Validate("Mã loại kinh phí", Utility.Enum.DATA_TYPE.String, true)]
        public string MaKinhPhi
        {
            get => _maKinhPhi;
            set => SetProperty(ref _maKinhPhi, value);
        }

        private string _tenKinhPhi;
        [DisplayName("Tên loại kinh phí")]
        [DisplayDetailInfo("Tên loại kinh phí")]
        [Validate("Tên loại kinh phí", Utility.Enum.DATA_TYPE.String, true)]
        public string TenKinhPhi
        {
            get => _tenKinhPhi;
            set => SetProperty(ref _tenKinhPhi, value);
        }

        private string _moTa;
        [DisplayName("Mô tả")]
        [DisplayDetailInfo("Mô tả")]
        public string MoTa
        {
            get => _moTa;
            set => SetProperty(ref _moTa, value);
        }

        private int? _sapXep;
        [DisplayName("Sắp xếp")]
        [DisplayDetailInfo("Sắp xếp")]
        public int? SapXep
        {
            get => _sapXep;
            set => SetProperty(ref _sapXep, value);
        }

        [DisplayDetailInfo("Năm làm việc")]
        public int NamLamViec { get; set; }

        [DisplayDetailInfo("Ngày tạo")]
        public DateTime NgayTao { get; set; }

        [DisplayDetailInfo("Người tạo")]
        public string NguoiTao { get; set; }

        [DisplayDetailInfo("Ngày cập nhật")]
        public DateTime? NgaySua { get; set; }

        [DisplayDetailInfo("Người cập nhật")]
        public string NguoiSua { get; set; }
    }
}
