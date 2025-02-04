using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class BhQtcnBHXHChiTiet : EntityBase
    {
        [Column("ID_QTC_Nam_CheDoBHXH_ChiTiet")]
        [Key]
        public override Guid Id { get; set; }
        public Guid IIdQTCNamCheDoBHXH { get; set; }
        public Guid IIdMucLucNganSach { get; set; }
        public string SLoaiTroCap { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }
        public int? ISoDuToanDuocDuyet { get; set; }
        public Double? FTienDuToanDuyet { get; set; }
        public int? ITongSoThucChi { get; set; }
        public Double? FTongTienThucChi { get; set; }
        public int? ISoSQThucChi { get; set; }
        public Double? FTienSQThucChi { get; set; }
        public int? ISoQNCNThucChi { get; set; }
        public Double? FTienQNCNThucChi { get; set; }
        public int? ISoCNVCQPThucChi { get; set; }
        public Double? FTienCNVCQPThucChi { get; set; }
        public int? ISoHSQBSThucChi { get; set; }
        public Double? FTienHSQBSThucChi { get; set; }
        public Double? FTienThua { get; set; }
        public Double? FTienThieu { get; set; }
        public Double? FTiLeThucHienTrenDuToan { get; set; }
        public int? ISoLDHDThucChi { get; set; }
        public Double? FTienLDHDThucChi { get; set; }
        public string IIdMaDonVi { get; set; }
        public string SXauNoiMa { get; set; }
        public int? INamLamViec { get; set; }

    }
}
