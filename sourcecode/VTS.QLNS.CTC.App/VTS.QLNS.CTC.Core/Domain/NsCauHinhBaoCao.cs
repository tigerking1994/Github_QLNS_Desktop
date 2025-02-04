using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NsCauHinhBaoCao : EntityBase
    {
        public string SMaBaoCao { get; set; }
        public string STenBaoCao { get; set; }
        public string IIdMaDonVi { get; set; }
        public int? INamLamViec { get; set; }
        public int? ILoaiBaoCao { get; set; }
        public string SMaGhiChu { get; set; }
        public string SGhiChu { get; set; }
        public int? ILoaiCauHinh { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
    }
}
