using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Extensions;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlDmNgayNghiModel : ModelBase
    {

        private string _sMaNgayNghi;
        [DisplayName("Mã ngày nghỉ")]
        [DisplayDetailInfo("Mã ngày nghỉ")]
        [ColumnIndex(0)]
        public string SMaNgayNghi
        {
            get => _sMaNgayNghi;
            set => SetProperty(ref _sMaNgayNghi, value);
        }

        private string _sTenNgayNghi;
        [DisplayName("Tên ngày nghỉ")]
        [DisplayDetailInfo("Tên ngày nghỉ")]
        public string STenNgayNghi
        {
            get => _sTenNgayNghi;
            set => SetProperty(ref _sTenNgayNghi, value);
        }

        private DateTime? _dTuNgay;
        [DisplayName("Ngày bắt đầu")]
        [DisplayDetailInfo("Ngày bắt đầu")]
        public DateTime? DTuNgay
        {
            get => _dTuNgay;
            set
            {
                SetProperty(ref _dTuNgay, value);
            }
        }

        private DateTime? _dDenNgay;
        [DisplayName("Ngày kết thúc")]
        [DisplayDetailInfo("Ngày kết thúc")]
        public DateTime? DDenNgay
        {
            get => _dDenNgay;
            set
            {
                SetProperty(ref _dDenNgay, value);
            }
        }

        public int? INamLamViec { get; set; }
    }
}
