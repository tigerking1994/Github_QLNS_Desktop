using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhQtcqKCBQuery
    {
        [Column("ID_QTC_Quy_KCB")]
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

        [Column("fTongTien_DuToanNamTruocChuyenSang")]
        public Double? FTongTienDuToanNamTruocChuyenSang { get; set; }

        [Column("fTongTien_DuToanGiaoNamNay")]
        public Double? FTongTienDuToanGiaoNamNay { get; set; }

        [Column("fTongTien_TongDuToanDuocGiao")]
        public Double? FTongTienTongDuToanDuocGiao { get; set; }

        [Column("fTongTienThucChi")]
        public Double? FTongTienThucChi { get; set; }

        [Column("fTongTienQuyetToanDaDuyet")]
        public Double? FTongTienQuyetToanDaDuyet { get; set; }

        [Column("fTongTienDeNghiQuyetToanQuyNay")]
        public Double? FTongTienDeNghiQuyetToanQuyNay { get; set; }

        [Column("fTongTienXacNhanQuyetToanQuyNay")]
        public Double? FTongTienXacNhanQuyetToanQuyNay { get; set; }

        [Column("bDaTongHop")]
        public bool BDaTongHop { get; set; }

        [Column("sDSSoChungTuTongHop")]
        public string SDSSoChungTuTongHop { get; set; }

        [Column("sTenDonVi")]
        public string STenDonVi { get; set; }
        public string SDSLNS { get; set; }

    }
}
