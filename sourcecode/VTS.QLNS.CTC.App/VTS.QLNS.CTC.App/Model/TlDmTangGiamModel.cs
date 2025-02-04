using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VTS.QLNS.CTC.App.Extensions;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlDmTangGiamModel : ModelBase
    {

        [DisplayName("Mã tăng giảm")]
        [DisplayDetailInfo("Mã tăng giảm")]
        public string MaTangGiam { get; set; }

        [DisplayName("Tên tăng giảm")]
        [DisplayDetailInfo("tên tăng giảm")]
        public string TenTangGiam { get; set; }

        [DisplayName("Loại tăng giảm")]
        [DisplayDetailInfo("Loại tăng giảm")]
        public int? LoaiTangGiam { get; set; }
        public bool? Readonly { get; set; }
        public bool? Splits { get; set; }

        [DisplayName("Mã tăng giảm")]
        public string Parent { get; set; }

        public override bool IsHangCha
        {
            get => (Parent == null || Parent == "");
        }
    }
}
