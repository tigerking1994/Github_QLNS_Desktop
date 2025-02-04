using System;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NsQsChungTuChiTiet : EntityBase
    {
        [Column("iID_QSCTChiTiet")]
        public override Guid Id { get; set; }
        public Guid IIdQschungTu { get; set; }
        public Guid IIdMlns { get; set; }
        public Guid? IIdMlnsCha { get; set; }
        public string SKyHieu { get; set; }
        public string SMoTa { get; set; }
        public bool BHangCha { get; set; }
        public int IThangQuy { get; set; }
        public string IIdMaDonVi { get; set; }
        public int? INamLamViec { get; set; }
        public double FSoThieuUy { get; set; }
        public double FSoTrungUy { get; set; }
        public double FSoThuongUy { get; set; }
        public double FSoDaiUy { get; set; }
        public double FSoThieuTa { get; set; }
        public double FSoTrungTa { get; set; }
        public double FSoThuongTa { get; set; }
        public double FSoDaiTa { get; set; }
        public double FSoTuong { get; set; }
        public double FSoTsq { get; set; }
        public double FSoBinhNhi { get; set; }
        public double FSoBinhNhat { get; set; }
        public double FSoHaSi { get; set; }
        public double FSoTrungSi { get; set; }
        public double FSoThuongSi { get; set; }
        public double FSoThuongTaQNCN { get; set; }
        public double FSoTrungTaQNCN { get; set; }
        public double FSoThieuTaQNCN { get; set; }
        public double FSoDaiUyQNCN { get; set; }
        public double FSoThuongUyQNCN { get; set; }
        public double FSoTrungUyQNCN { get; set; }
        public double FSoThieuUyQNCN { get; set; }
        public double FSoCnvqp { get; set; }
        public double FSoLdhd { get; set; }
        public double FSoCnvqpct { get; set; }
        public double FSoQnvqphd { get; set; }
        public double? FTongSo { get; set; }
        public double? FSoSqKh { get; set; }
        public double? FSoHsqbsKh { get; set; }
        public double? FSoCnvqpKh { get; set; }
        public double? FSoLdhdKh { get; set; }
        public double? FSoQncnKh { get; set; }
        public double? FSoVcqp { get; set; }
        public double? FSoCyH { get; set; }
        public double? FSoCcqp { get; set; }
        public double? FSoCyKt { get; set; }
        public string SGhiChu { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNgaySua { get; set; }

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
