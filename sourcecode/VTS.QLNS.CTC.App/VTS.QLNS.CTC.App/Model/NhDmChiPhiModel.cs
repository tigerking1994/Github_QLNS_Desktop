using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDmChiPhiModel : ModelBase
    {
        public Guid IIdChiPhi { get; set; }

        private string _sMaChiPhi;
        [DisplayName("Mã chi phí")]
        [DisplayDetailInfo("Mã chi phí")]
        [Validate("Mã chi phí", Utility.Enum.DATA_TYPE.String, 50, true)]
        public string SMaChiPhi
        {
            get => _sMaChiPhi;
            set => SetProperty(ref _sMaChiPhi, value);
        }

        private string _sTenVietTat;
        [DisplayName("Tên viết tắt")]
        [DisplayDetailInfo("Tên viết tắt")]
        [Validate("Tên viết tắt", Utility.Enum.DATA_TYPE.String, 100)]
        public string STenVietTat
        {
            get => _sTenVietTat;
            set => SetProperty(ref _sTenVietTat, value);
        }

        private string _sTenChiPhi;
        [DisplayName("Tên chi phí")]
        [DisplayDetailInfo("Tên chi phí")]
        [Validate("Tên chi phí", Utility.Enum.DATA_TYPE.String, 300, true)]
        public string STenChiPhi
        {
            get => _sTenChiPhi;
            set => SetProperty(ref _sTenChiPhi, value);
        }

        private string _sMoTa;
        [DisplayName("Mô tả")]
        [DisplayDetailInfo("Mô tả")]
        [Validate("Mô tả", Utility.Enum.DATA_TYPE.String, 500)]
        public string SMoTa
        {
            get => _sMoTa;
            set => SetProperty(ref _sMoTa, value);
        }

        private int _iThuTu;
        /*[DisplayName("Thứ tự")]
        [DisplayDetailInfo("Thứ tự")]*/
        public int IThuTu
        {
            get => _iThuTu;
            set => SetProperty(ref _iThuTu, value);
        }

        public DateTime? DNgayTao { get; set; }
        public string SIdMaNguoiDungTao { get; set; }
        public bool? BHangCha { get; set; }
        public string STT { get; set; }
        public string SMaChiPhiOrder
        {
            get
            {
                return this.SMaChiPhi.IsEmpty() ? "0000" : this.SMaChiPhi.PadLeft(4, '0');
            }
        }

    }
}
