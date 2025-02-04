using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class BhQtcqBHXHChiTiet : EntityBase
    {
        [Column("ID_QTC_Quy_CheDoBHXH_ChiTiet")]
        [Key]
        public override Guid Id { get; set; }
        public Guid IdQTCQuyCheDoBHXH { get; set; }
        public Guid IIdMucLucNganSach { get; set; }
        public string SLoaiTroCap { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
        public Double? FTienDuToanDuyet { get; set; }
        public int? ISoLuyKeCuoiQuyNay { get; set; }
        public Double? FTienLuyKeCuoiQuyNay { get; set; }
        public int? ISoSQDeNghi { get; set; }
        public Double? FTienSQDeNghi { get; set; }
        public int ?ISoQNCNDeNghi { get; set; }
        public Double? FTienQNCNDeNghi { get; set; }
        public int? ISoCNVCQPDeNghi { get; set; }
        public Double? FTienCNVCQPDeNghi { get; set; }
        public int? ISoHSQBSDeNghi { get; set; }
        public Double? FTienHSQBSDeNghi { get; set; }
        public int? ISoLDHDDeNghi { get; set; }
        public Double? FTienLDHDDeNghi { get; set; }
        public int? ITongSoDeNghi { get;set; }
        public Double? FTongTienDeNghi { get;set; }
        public Double? FTongTienPheDuyet { get;set; }
        public int? INamLamViec { get; set; }
        public string SXauNoiMa { get; set; }
        public string SLNS { get; set; }
        public string IIDMaDonVi { get; set; }
    }
}
