using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class QsChungTuChiTietQuery
    {
        [Column("iID_QSCTChiTiet")]
        public Guid? Id { get; set; }
        [Column("sKyHieu")]
        public string SKyHieu { get; set; }
        [Column("sMoTa")]
        public string SMoTa { get; set; }
        [Column("sM")]
        public string SM { get; set; }
        [Column("bHangCha")]
        public bool? BHangCha { get; set; }
        [Column("iID_MaDonVi")]
        public string IIdMaDonVi { get; set; }
        [Column("fSoThieuUy")]
        public double? FSoThieuUy { get; set; }
        [Column("fSoTrungUy")]
        public double? FSoTrungUy { get; set; }
        [Column("fSoThuongUy")]
        public double? FSoThuongUy { get; set; }
        [Column("fSoDaiUy")]
        public double? FSoDaiUy { get; set; }
        [Column("fSoThieuTa")]
        public double? FSoThieuTa { get; set; }
        [Column("fSoTrungTa")]
        public double? FSoTrungTa { get; set; }
        [Column("fSoThuongTa")]
        public double? FSoThuongTa { get; set; }
        [Column("fSoDaiTa")]
        public double? FSoDaiTa { get; set; }
        [Column("fSoTuong")]
        public double? FSoTuong { get; set; }
        [Column("fSoTSQ")]
        public double? FSoTsq { get; set; }
        [Column("fSoBinhNhi")]
        public double? FSoBinhNhi { get; set; }
        [Column("fSoBinhNhat")]
        public double? FSoBinhNhat { get; set; }
        [Column("fSoHaSi")]
        public double? FSoHaSi { get; set; }
        [Column("fSoTrungSi")]
        public double? FSoTrungSi { get; set; }
        [Column("fSoThuongSi")]
        public double? FSoThuongSi { get; set; }
        [Column("fSoThuongTa_QNCN")]
        public double? FSoThuongTaQNCN { get; set; }
        [Column("fSoTrungTa_QNCN")]
        public double? FSoTrungTaQNCN { get; set; }
        [Column("fSoThieuTa_QNCN")]
        public double? FSoThieuTaQNCN { get; set; }
        [Column("fSoDaiUy_QNCN")]
        public double? FSoDaiUyQNCN { get; set; }
        [Column("fSoThuongUy_QNCN")]
        public double? FSoThuongUyQNCN { get; set; }
        [Column("fSoTrungUy_QNCN")]
        public double? FSoTrungUyQNCN { get; set; }
        [Column("fSoThieuUy_QNCN")]
        public double? FSoThieuUyQNCN { get; set; }
        [Column("fSoVCQP")]
        public double? FSoVcqp { get; set; }
        [Column("fSoCNVQP")]
        public double? FSoCnvqp { get; set; }
        [Column("fSoCcqp")]
        public double FSoCcqp { get; set; }
        [Column("fSoLDHD")]
        public double? FSoLdhd { get; set; }
        [Column("fSoCY_H")]
        public double? FSoCyH { get; set; }
        [Column("fSoCY_KT")]
        public double? FSoCyKt { get; set; }
        [Column("sGhiChu")]
        public string SGhiChu { get; set; }
        [NotMapped]
        public bool BHasData
        {
            get
            {
                foreach (var prop in typeof(NsQsChungTuChiTiet).GetProperties())
                {
                    if (prop.Name.StartsWith("F"))
                    {
                        if (Convert.ToDouble(prop.GetValue(this)) != 0)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }
    }
}
