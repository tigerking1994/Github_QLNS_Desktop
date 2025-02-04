using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDmLoaiHopDongModel : ModelBase
    {
        public Guid IIdLoaiHopDongId { get; set; }

        private string _sMaLoaiHopDong;
        [Validate("Mã loại hợp đồng", DATA_TYPE.String, true)]
        [DisplayName("Mã loại hợp đồng")]
        [DisplayDetailInfo("Mã loại hợp đồng")]
        public string SMaLoaiHopDong
        {
            get => _sMaLoaiHopDong;
            set => SetProperty(ref _sMaLoaiHopDong, value);
        }

        private string _sTenVietTat;
        [Validate("Tên viết tắt")]
        [DisplayName("Tên viết tắt")]
        [DisplayDetailInfo("Tên viết tắt")]
        public string STenVietTat
        {
            get => _sTenVietTat;
            set => SetProperty(ref _sTenVietTat, value);
        }

        private string _STenLoaiHopDong;
        [Validate("Tên hợp đồng", DATA_TYPE.String, true)]
        [DisplayName("Tên hợp đồng")]
        [DisplayDetailInfo("Tên hợp đồng")]
        public string STenLoaiHopDong
        {
            get => _STenLoaiHopDong;
            set => SetProperty(ref _STenLoaiHopDong, value);
        }

        private string _sMoTa;
        [Validate("Mô tả")]
        [DisplayName("Mô tả")]
        [DisplayDetailInfo("Mô tả")]
        public string SMoTa
        {
            get => _sMoTa;
            set => SetProperty(ref _sMoTa, value);
        }

        public int? iThuTu { get; set; }
        public override bool IsEditable => !IsDeleted;
    }
}
