#nullable disable

using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlDmCapBacLuongNq104 : EntityBase
    {
        public int? Loai { get; set; }
        public string MaDm { get; set; }
        public string MaDmCha { get; set; }
        public string XauNoiMa { get; set; }
        public string TenDm { get; set; }
        public string LoaiDoiTuong { get; set; }
        public decimal? TienLuong { get; set; }
        public decimal? BhcsCq { get; set; }
        public decimal? BhtnCq { get; set; }
        public decimal? BhxhCq { get; set; }
        public decimal? BhytCq { get; set; }
        public decimal? HsBhcs { get; set; }
        public decimal? HsBhtn { get; set; }
        public decimal? HsBhyt { get; set; }
        public decimal? HsBhxh { get; set; }
        public decimal? HsKpcd { get; set; }
        public decimal? KpcdCq { get; set; }
        public decimal? LhtHs { get; set; }
        public decimal? PhuCapRaQuan { get; set; }
        public decimal? TiLeHuong { get; set; }
        public decimal? HsTroCapOmDau { get; set; }
        public decimal? TienNangLuong { get; set; }
        public bool IsReadonly { get; set; }
        public int? NhomDoiTuong { get; set; }
        public int? Nam { get; set; }

        [NotMapped]
        public string LoaiNhom { get; set; }
        [NotMapped]
        public string MaLoai { get; set; }
        [NotMapped]
        public string MaNhom { get; set; }
        [NotMapped]
        public string XauNoiMaNhom { get; set; }

    }
}
