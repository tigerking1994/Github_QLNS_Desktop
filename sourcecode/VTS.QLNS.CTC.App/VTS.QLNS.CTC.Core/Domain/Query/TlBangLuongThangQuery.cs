using System;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class TlBangLuongThangQuery
    {
        public Guid Id { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public string MaCbo { get; set; }
        public string TenCbo { get; set; }
        public string MaDonVi { get; set; }
        public string UserName { get; set; }
        public DateTime? NgayHt { get; set; }
        public string MaCachTl { get; set; }
        public string TenCachTl { get; set; }
        public int? SoTt { get; set; }
        public int? LoaiBl { get; set; }
        public Guid? Parent { get; set; }
        public string MaCb { get; set; }
        public string MaPhuCap { get; set; }
        public decimal GiaTri { get; set; }
        public decimal? HuongPC_SN { get; set; }
        public string MaHieuCanBo { get; set; }

        // Another properties
        public bool? BNuocNgoai { get; set; }
        public bool? BTamGiamTamGiu { get; set; }
        public decimal? HuongPcSn { get; set; }
        public string CongThuc { get; set; }
        public bool IsCalculated { get; set; }
        public bool IsCapNhat { get; set; }
        public Guid IID_CanBoPhuCap { get; set; }
        public string MaTangGiam { get; set; }
        public decimal SoNgayTruyThu { get; set; }
    }
}
