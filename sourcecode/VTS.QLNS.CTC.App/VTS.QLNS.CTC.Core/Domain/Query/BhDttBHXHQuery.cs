using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhDttBHXHQuery
    {
        [Column("iID_DTT_BHXH")]
        public Guid Id { get; set; }
        public string SSoChungTu { get; set; }
        public int? INamLamViec { get; set; }
        public DateTime DNgayChungTu { get; set; }
        [Column("iID_MaDonVi")]
        public string IIDMaDonVi { get; set; }
        public string SMoTa { get; set; }
        public bool BIsKhoa { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public int? ILoaiDuToan { get; set; }
        public DateTime? DNgayTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }
        public string STongHop { get; set; }
        public double? FThuBHXHNLDDong { get; set; }
        public double? FThuBHXHNSDDong { get; set; }
        public double? FThuBHXH { get; set; }
        public double? FThuBHYTNLDDong { get; set; }
        public double? FThuBHYTNSDDong { get; set; }
        public double? FTongBHYT { get; set; }
        public double? FThuBHTNNLDDong { get; set; }
        public double? FThuBHTNNSDDong { get; set; }
        public double? FThuBHTN { get; set; }
        public double? FDuToan { get; set; }
        public string SDslns { get; set; }
    }
}
