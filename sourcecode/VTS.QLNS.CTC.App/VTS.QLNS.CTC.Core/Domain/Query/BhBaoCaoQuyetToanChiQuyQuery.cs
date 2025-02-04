using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhBaoCaoQuyetToanChiQuyQuery
    {
        [Column("sTT")]
        public string STT { get; set; }

        [Column("iID_MaDonVi")]
        public string IID_MaDonVi { get; set; }

        [Column("sTenDonVi")]
        public string STenDonVi { get; set; }

        [Column("iSoNgayDuoi14BenhDaiNgay")]
        public int? ISoNgayDuoi14BenhDaiNgay { get; set; }

        [Column("fSoTienDuoi14BenhDaiNgay")]
        public Double? FSoTienDuoi14BenhDaiNgay { get; set; }

        [Column("iSoNgayTren14BenhDaiNgay")]
        public int? ISoNgayTren14BenhDaiNgay { get; set; }

        [Column("fSoTienTren14BenhDaiNgay")]
        public Double? FSoTienTren14BenhDaiNgay { get; set; }

        [Column("iSoNgayDuoi14OmKhac")]
        public int? ISoNgayDuoi14OmKhac { get; set; }

        [Column("fSoTienDuoi14OmKhac")]
        public Double? FSoTienDuoi14OmKhac { get; set; }

        [Column("iSoNgayTren14OmKhac")]
        public int? ISoNgayTren14OmKhac { get; set; }

        [Column("fSoTienTren14OmKhac")]
        public Double? FSoTienTren14OmKhac { get; set; }

        [Column("iSoNgayConOm")]
        public int? ISoNgayConOm { get; set; }

        [Column("fSoTienConOm")]
        public Double? FSoTienConOm { get; set; }

        [Column("iSoNgayPHSK")]
        public int? ISoNgayPHSK { get; set; }

        [Column("fSoTienPHSK")]
        public Double? FSoTienPHSK { get; set; }

        [Column("fTongTien")]
        public Double? FTongTien { get; set; }

        [Column("iSoNgaySinhConNNuoiCon")]
        public int? ISoNgaySinhConNNuoiCon { get; set; }

        [Column("fSoTienSinhConNNuoiCon")]
        public Double? FSoTienSinhConNNuoiCon { get; set; }

        [Column("iSoNgaySinhTroCapSinhCon")]
        public int? ISoNgaySinhTroCapSinhCon { get; set; }

        [Column("fSoTienSinhTroCapSinhCon")]
        public Double? FSoTienSinhTroCapSinhCon { get; set; }

        [Column("iSoNgayKhamThaiKHHGD")]
        public int? ISoNgayKhamThaiKHHGD { get; set; }

        [Column("fSoTienKhamThaiKHHGD")]
        public Double? FSoTienKhamThaiKHHGD { get; set; }

        [Column("iSoNgayPHSKThaiSan")]
        public int? ISoNgayPHSKThaiSan { get; set; }

        [Column("fSoTienPHSKThaiSan")]
        public Double? FSoTienPHSKThaiSan { get; set; }


        [Column("bHangCha")]
        public bool BHangCha { get; set; }
        [Column("IsHangCha")]
        public bool IsHangCha { get; set; }
        [Column("type")]
        public int Type { get; set; }
        [Column("IsParent")]
        public bool? IsParent { get; set; }
    }
}
