using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhDmLoaiTaiSanQuery
    {
        public Guid Id { get; set; }
        public string SMaLoaiTaiSan { get; set; }
        public string STenLoaiTaiSan { get; set; }
        public string SMoTa { get; set; }
    }
}
