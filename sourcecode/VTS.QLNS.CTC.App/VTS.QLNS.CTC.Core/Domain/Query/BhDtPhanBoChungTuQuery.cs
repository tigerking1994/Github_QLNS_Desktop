using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhDtPhanBoChungTuQuery
    {
        [Column("iID_DTT_BHXH_PhanBo_ChungTu")]
        public Guid Id { get; set; }
        [Column("sSoChungTu")]
        public string SSoChungTu { get; set; }
        [Column("iSoChungTuIndex")]
        public int? ISoChungTuIndex { get; set; }
        [Column("dNgayChungTu")]
        public DateTime? DNgayChungTu { get; set; }
        [Column("sSoQuyetDinh")]
        public string SSoQuyetDinh { get; set; }
        [Column("dNgayQuyetDinh")]
        public DateTime? DNgayQuyetDinh { get; set; }
        [Column("sMoTa")]
        public string SMoTa { get; set; }
        [Column("sDSID_MaDonVi")]
        public string SDsidMaDonVi { get; set; }
        [Column("sDSLNS")]
        public string SDslns { get; set; }
        [Column("iLoaiDuToan")]
        public int? ILoaiDuToan { get; set; }
        [Column("iNamNganSach")]
        public int? INamNganSach { get; set; }
        [Column("iNamLamViec")]
        public int? INamLamViec { get; set; }
        [Column("fTongDuToan")]
        public double? FTongDuToan { get; set; }
        [Column("fThuBHXH_NLD")]
        public double? FThuBHXHNLD { get; set; }
        [Column("fThuBHXH_NSD")]
        public double? FThuBHXHNSD { get; set; }
        [Column("fTongBHXH")]
        public double? FTongBHXH { get; set; }
        [Column("fThuBHYT_NLD")]
        public double? FThuBHYTNLD { get; set; }
        [Column("fThuBHYT_NSD")]
        public double? FThuBHYTNSD { get; set; }
        [Column("fTongBHYT")]
        public double? FTongBHYT { get; set; }
        [Column("fThuBHTN_NLD")]
        public double? FThuBHTNNLD { get; set; }
        [Column("fThuBHTN_NSD")]
        public double? FThuBHTNNSD { get; set; }
        [Column("fTongBHTN")]
        public double? FTongBHTN { get; set; }
        [Column("bKhoa")]
        public bool BKhoa { get; set; }
        [Column("sGhiChu")]
        public string SGhiChu { get; set; }
        [Column("dNgayTao")]
        public DateTime? DNgayTao { get; set; }
        [Column("sNguoiTao")]
        public string SNguoiTao { get; set; }
        [Column("dNgaySua")]
        public DateTime? DNgaySua { get; set; }
        [Column("sNguoiSua")]
        public string SNguoiSua { get; set; }
        [Column("bLuongNhanDuLieu")]
        public bool? BLuongNhanDuLieu { get; set; }
        [Column("sTenDonVi")]
        public string STenDonVi { get; set; }
    }
}
