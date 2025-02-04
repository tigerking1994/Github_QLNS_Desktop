using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NsQtChungTuChiTietGiaiThichLuongTru : EntityBase
    {
        [Column("iID_QTCTCTGiaiThich_LuongTru")]
        public override Guid Id { get; set; }
        public Guid IIdQtchungTu { get; set; }
        public string IIdGiaiThich { get; set; }
        public int IThangQuy { get; set; }
        public int IThangQuyLoai { get; set; }
        public string IIdMaDonVi { get; set; }
        public string SMoTa { get; set; }
        public string IIdDoiTuong { get; set; }
        public string SHoTen { get; set; }
        public double FLuongThang { get; set; }
        public double FNgayNghi { get; set; }
        public double FSoNguoi { get; set; }
        public double FLuongCapBac { get; set; }
        public double FLuongThamNien { get; set; }
        public double FLuongPhuCapCongVu { get; set; }
        public double FLuongPhuCapKhacBh { get; set; }
        public double FLuongPhuCapKhac { get; set; }
        public double FTongBaoHiem { get; set; }
        public int INamLamViec { get; set; }
        public int IStatus { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
    }
}
