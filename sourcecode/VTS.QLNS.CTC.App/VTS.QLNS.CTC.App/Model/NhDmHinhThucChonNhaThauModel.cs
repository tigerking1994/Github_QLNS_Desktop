using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDmHinhThucChonNhaThauModel : ModelBase
    {
        private string _SMaHinhThucChonNhaThau;
        [DisplayName("Mã hình thức chọn nhà thầu")]
        [DisplayDetailInfo("Mã hình thức chọn nhà thầu")]
        [Validate("Mã hình thức chọn nhà thầu", DATA_TYPE.String, true)]
        public string SMaHinhThucChonNhaThau
        {
            get => _SMaHinhThucChonNhaThau;
            set => SetProperty(ref _SMaHinhThucChonNhaThau, value);
        }

        private string _sTenVietTat;
        [DisplayName("Tên viết tắt")]
        [DisplayDetailInfo("Tên viết tắt")]
        [Validate("Tên viết tắt")]
        public string STenVietTat
        {
            get => _sTenVietTat;
            set => SetProperty(ref _sTenVietTat, value);
        }

        private string _sTenHinhThucChonNhaThau;
        [DisplayName("Tên hình thức chọn nhà thầu")]
        [DisplayDetailInfo("Tên hình thức chọn nhà thầu")]
        [Validate("Tên hình thức chọn nhà thầu", DATA_TYPE.String, true)]
        public string STenHinhThucChonNhaThau
        {
            get => _sTenHinhThucChonNhaThau;
            set => SetProperty(ref _sTenHinhThucChonNhaThau, value);
        }

        private string _SMoTa;
        [DisplayName("Mô tả")]
        [DisplayDetailInfo("Mô tả")]
        [Validate("Mô tả")]
        public string SMoTa
        {
            get => _SMoTa;
            set => SetProperty(ref _SMoTa, value);
        }

        public int? IThuTu { get; set; }
    }
}