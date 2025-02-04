using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public partial class BhDanhMucLoaiChiQuery
    {
        [Column("iID")]
        [Key]
        public Guid Id { get; set; }
        [Column("sMaLoaiChi")]
        public string SMaLoaiChi { get; set; }
        [Column("sTenDanhMucLoaiChi")]
        public string STenDanhMucLoaiChi { get; set; }
        [Column("iNamLamViec")]
        public int INamLamViec { get; set; }
        [Column("dNgaySua")]
        public DateTime? DNgaySua { get; set; }
        [Column("dNgayTao")]
        public DateTime? DNgayTao { get; set; }
        [Column("sNguoiSua")]
        public string SNguoiSua { get; set; }
        [Column("sNguoiTao")]
        public string SNguoiTao { get; set; }
        [Column("sMoTa")]
        public string SMoTa { get; set; }
        [Column("iTrangThai")]
        public int? ITrangThai { get; set; }
        [Column("sLNS")]
        public string SLNS { get; set; }
        [Column("sDSXauNoiMa")]
        public string SDSXauNoiMa { get; set; }
    }
}
