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
    public partial class BhQtcqKCBChiTiet : EntityBase
    {
        [Column("ID_QTC_Quy_KCB_ChiTiet")]
        [Key]
        public override Guid Id { get; set; }    
        public Guid IIdQTCQuyKCB { get; set; }
        public Guid IIdMucLucNganSach { get; set; }
        public string SNoiDung { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get;set; }
        public Double? FTienDuToanNamTruocChuyenSang { get;set; }
        public Double? FTienDuToanGiaoNamNay { get; set; }
        public Double? FTienTongDuToanDuocGiao { get; set; }
        public Double? FTienThucChi { get;set; }
        public Double? FTienQuyetToanDaDuyet { get; set; }
        public Double? FTienDeNghiQuyetToanQuyNay { get; set; }
        public Double? FTienXacNhanQuyetToanQuyNay { get; set; }
        public string SXauNoiMa { get; set; }
        public string IIdMaDonVi { get; set; }
        public int? INamLamViec { get; set; }
        public string SGhiChu { get;set; }

    }
}
