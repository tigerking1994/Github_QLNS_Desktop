using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VTS.QLNS.CTC.App.Extensions;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtDmChiPhiModel : ModelBase
    {
        public Guid IIdChiPhi { get; set; }

        private int _iThuTu;
        [DisplayName("Thứ tự")]
        [DisplayDetailInfo("Thứ tự")]
        public int IThuTu
        {
            get => _iThuTu;
            set => SetProperty(ref _iThuTu, value);
        }

        private string _sMaChiPhi;
        [DisplayName("Mã chi phí")]
        [DisplayDetailInfo("Mã chi phí")]
        public string SMaChiPhi 
        {
            get => _sMaChiPhi;
            set => SetProperty(ref _sMaChiPhi, value);
        }

        private string _sTenVietTat;
        [DisplayName("Tên viết tắt")]
        [DisplayDetailInfo("Tên viết tắt")]
        public string STenVietTat 
        {
            get => _sTenVietTat;
            set => SetProperty(ref _sTenVietTat, value);
        }

        private string _sTenChiPhi;
        [DisplayName("Tên chi phí")]
        [DisplayDetailInfo("Tên chi phí")]
        public string STenChiPhi 
        {
            get => _sTenChiPhi;
            set => SetProperty(ref _sTenChiPhi, value);
        }

        private string _sMoTa;
        [DisplayName("Mô tả")]
        [DisplayDetailInfo("Mô tả")]
        public string SMoTa 
        {
            get => _sMoTa;
            set => SetProperty(ref _sMoTa, value);
        }

        public DateTime? DNgayTao { get; set; }
        public string SIdMaNguoiDungTao { get; set; }
        public int? ISoLanSua { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SIpsua { get; set; }
        public string SIdMaNguoiDungSua { get; set; }
    }
}
