using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtKtKhoiTao : EntityBase
    {
        public VdtKtKhoiTao()
        {
           // VdtKtKhoiTaoChiTiets = new HashSet<VdtKtKhoiTaoChiTiet>();
        }

        public Guid Id { get; set; }
        public int INamKhoiTao { get; set; }
        public Guid? IIdDonViId { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public Guid? IIdQddauTuId { get; set; }
        public Guid? IIdDuToanId { get; set; }
        public double? FKhvonUng { get; set; }
        public double? FVonUngDaCap { get; set; }
        public double? FVonUngDaThuHoi { get; set; }
        public double? FGiaTriConPhaiUng { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public double? FTiGia { get; set; }
        public DateTime? DDateCreate { get; set; }
        public string SUserCreate { get; set; }
        public DateTime? DDateUpdate { get; set; }
        public string SUserUpdate { get; set; }
        public DateTime? DDateDelete { get; set; }
        public string SUserDelete { get; set; }
        public string IIdMaDonVi { get; set; }
        public bool BIsDuAnCu { get; set; }
        //public virtual ICollection<VdtKtKhoiTaoChiTiet> VdtKtKhoiTaoChiTiets { get; set; }
    }
}
