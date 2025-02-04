using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NsDtChungTuCongKhaiQuery
    {
        public Guid Id_DanhMuc { get; set; }
        public Guid? Id_DanhMucCha { get; set; }
        public string STT { get; set; }
        public string sMoTa { get; set; }
        public bool bHangCha { get; set; }

        public string sMa { get; set; }
        public double? fTuChi { get; set; }


    }
}
