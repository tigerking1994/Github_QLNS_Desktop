using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhDtctgBHXHQuery
    {
        [Column("ID")]
        public Guid Id { get; set; }
        public string IdLuyKe { get; set; }

        [Column("sSoChungTu")]
        public string SSoChungTu { get; set; }
        [Column("iID_MaDonVi")]
        public string IID_MaDonVi { get; set; }
        [Column("dNgayChungTu")]
        public DateTime? DNgayChungTu { get; set; }
        [Column("sTenDonVi")]
        public string STenDonVi { get; set; }
        [Column("sSoQuyetDinh")]
        public string SSoQuyetDinh { get; set; }
        [Column("dNgayQuyetDinh")]
        public DateTime? DNgayQuyetDinh { get; set; }
        [Column("sLNS")]
        public string SLNS { get; set; }
        [Column("fTongTienTuChi")]
        public Double? FTongTienTuChi { get; set; }
        [Column("fTongTienHienVat")]
        public Double? FTongTienHienVat { get; set; }
        [Column("fTongTien")]
        public Double? FTongTien { get; set; }
        [Column("fSoPhanBo")]
        public Double? FSoPhanBo { get; set; }
        [Column("fDaPhanBo")]
        public Double? FDaPhanBo { get; set; }
        [Column("fSoChuaPhanBo")]
        public Double? FSoChuaPhanBo { get; set; }
        [Column("iLoaiDotNhanPhanBo")]
        public int ILoaiDotNhanPhanBo { get; set; }
        [Column("sMoTa")]
        public string SMoTa { get; set; }
        [Column("iNamLamViec")]
        public int INamLamViec { get; set; }
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

        [Column("iLoaiChungTu")]
        public int? ILoaiChungTu { get; set; }

        [Column("iID_LoaiDanhMucChi")]
        public Guid? IIdLoaiDanhMucChi { get; set; }

        [Column("sLoaiChi")]
        public string SLoaiChi { get; set; }
        public string SMaLoaiChi { get; set; }
    }
}
