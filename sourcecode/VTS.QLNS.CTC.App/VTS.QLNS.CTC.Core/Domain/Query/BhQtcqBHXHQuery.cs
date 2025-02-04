using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhQtcqBHXHQuery
    {
        [Column("ID_QTC_Quy_CheDoBHXH")]
        public Guid Id { get; set; }

        [Column("iID_DonVi")]
        public Guid? IIdDonVi { get; set; }

        [Column("iID_MaDonVi")]
        public string IIdMaDonVi { get; set; }

        [Column("sSoChungTu")]
        public string SSoChungTu { get; set; }

        [Column("dNgayChungTu")]
        public DateTime DNgayChungTu { get; set; }

        [Column("sSoQuyetDinh")]
        public string SSoQuyetDinh { get; set; }

        [Column("dNgayQuyetDinh")]
        public DateTime DNgayQuyetDinh { get; set; }

        [Column("iQuyChungTu")]
        public int IQuyChungTu { get; set; }

        [Column("iNamChungTu")]
        public int INamChungTu { get; set; }

        [Column("sMoTa")]
        public string SMoTa { get; set; }

        [Column("dNgaySua")]
        public DateTime? DNgaySua { get; set; }

        [Column("dNgayTao")]
        public DateTime? DNgayTao { get; set; }

        [Column("sNguoiSua")]
        public string SNguoiSua { get; set; }

        [Column("sNguoiTao")]
        public string SNguoiTao { get; set; }

        [Column("sTongHop")]
        public string STongHop { get; set; }

        [Column("iID_TongHopID")]
        public Guid? IID_TongHopID { get; set; }

        [Column("iLoaiTongHop")]
        public int ILoaiTongHop { get; set; }

        [Column("bIsKhoa")]
        public bool BIsKhoa { get; set; }

        [Column("fTongTien_DuToanDuyet")]
        public Double? FTongTienDuToanDuyet { get; set; }

        [Column("iTongSo_LuyKeCuoiQuyNay")]
        public int ITongSoLuyKeCuoiQuyNay { get; set; }

        [Column("fTongTien_LuyKeCuoiQuyNay")]
        public Double? FTongTienLuyKeCuoiQuyNay { get; set; }

        [Column("iTongSoSQ_DeNghi")]
        public int ITongSoSQDeNghi { get; set; }

        [Column("fTongTienSQ_DeNghi")]
        public Double? FTongTienSQDeNghi { get; set; }

        [Column("iTongSoQNCN_DeNghi")]
        public int ITongSoQNCNDeNghi { get; set; }

        [Column("fTongTienQNCN_DeNghi")]
        public Double? FTongTienQNCNDeNghi { get; set; }

        [Column("iTongSoCNVCQP_DeNghi")]
        public int ITongSoCNVCQPDeNghi { get; set; }

        [Column("fTongTienCNVCQP_DeNghi")]
        public Double? FTongTienCNVCQPDeNghi { get; set; }

        [Column("iTongSoHSQBS_DeNghi")]
        public int ITongSoHSQBSDeNghi { get; set; }

        [Column("fTongTienHSQBS_DeNghi")]
        public Double? FTongTienHSQBSDeNghi { get; set; }

        [Column("iTongSoHDLDDeNghi")]
        public int ITongSoHDLDDeNghi { get; set; }

        [Column("fTongTienHDLDDeNghi")]
        public Double? FTongTienHDLDDeNghi { get; set; }

        [Column("iTongSo_DeNghi")]
        public int ITongSoDeNghi { get; set; }

        [Column("fTongTien_DeNghi")]
        public Double? FTongTienDeNghi { get; set; }

        [Column("fTongTien_PheDuyet")]
        public Double? FTongTienPheDuyet { get; set; }


        [Column("sDSSoChungTuTongHop")]
        public string SDSSoChungTuTongHop { get; set; }

        [Column("bDaTongHop")]
        public bool BDaTongHop { get; set; }

        [Column("sTenDonVi")]
        public string STenDonVi { get;set; }
        public string SDSLNS { get;set; }

    }
}
