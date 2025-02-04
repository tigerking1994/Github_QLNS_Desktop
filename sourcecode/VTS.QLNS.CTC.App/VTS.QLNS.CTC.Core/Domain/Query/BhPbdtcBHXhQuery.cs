using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhPbdtcBHXhQuery
    {
        [Column("ID")]
        public Guid Id { get; set; }

        [Column("sSoChungTu")]
        public string SSoChungTu { get; set; }

        [Column("dNgayChungTu")]
        public DateTime? DNgayChungTu { get; set; }

        [Column("sSoQuyetDinh")]
        public string SSoQuyetDinh { get; set; }

        [Column("dNgayQuyetDinh")]
        public DateTime? DNgayQuyetDinh { get; set; }

        [Column("sLNS")]
        public string SLNS { get; set; }

        [Column("sDotNhan")]
        public string SDotNhan { get; set; }

        [Column("sID_MaDonVi")]
        public string SID_MaDonVi { get; set; }

        [Column("fTongTienTuChi")]
        public Double? FTongTienTuChi { get; set; }

        [Column("fTongTienHienVat")]
        public Double? FTongTienHienVat { get; set; }

        [Column("fTongTien")]
        public Double? FTongTien { get; set; }

        [Column("iLoaiDotNhanPhanBo")]
        public int? ILoaiDotNhanPhanBo { get; set; }

        [Column("iLoaiChungTu")]
        public int ILoaiChungTu { get; set; }

        [Column("sMoTa")]
        public string SMoTa { get; set; }

        [Column("iNamChungTu")]
        public int INamChungTu { get; set; }

        [Column("bIsKhoa")]
        public bool BIsKhoa { get; set; }

        [Column("sNguoiTao")]
        public string SNguoiTao { get; set; }

        [Column("dNgayTao")]
        public DateTime DNgayTao { get; set; }

        [Column("sNguoiSua")]
        public string SNguoiSua { get; set; }

        [Column("dNgaySua")]
        public DateTime DNgaySua { get; set; }

        [Column("iID_DotNhan")]    
        public string IID_DotNhan { get; set; }

        [Column("iID_LoaiDanhMucChi")]
        public Guid? IIDLoaiDanhMucChi { get; set; }

        [Column("sMaLoaiChi")]
        public string SMaLoaiChi { get; set; }

        [Column("sLoaiChi")]
        public string SLoaiChi { get; set; }

    }
}
