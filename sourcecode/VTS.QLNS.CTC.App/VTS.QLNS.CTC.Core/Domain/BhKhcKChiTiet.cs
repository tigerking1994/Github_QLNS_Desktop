using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public class BhKhcKChiTiet : EntityBase
    {
        [Column("iID_BH_KHC_K_ChiTiet")]
        [Key]
        public override Guid Id { get; set; }
        public Guid IID_KHC_K { get; set; }
        public Guid IID_MucLucNganSach { get; set; }
        public string SNoiDung { get; set; }
        public double? FTienDaThucHienNamTruoc { get; set; }
        public double? FTienUocThucHienNamTruoc { get; set; }
        public double? FTienKeHoachThucHienNamNay { get; set; }
        public string SGhiChu { get ; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get;set; }

        public string SXauNoiMa { get; set; }
 
        public int INamLamViec { get; set; }

        public string IIdMaDonVi { get; set; }

        [NotMapped]
        public string Stt { get; set; }
        [NotMapped]
        public string SMoTa { get; set; }
        [NotMapped]
        public string Nganh { get; set; }
        [NotMapped]
        public string STenDonVi { get; set; }
        [NotMapped]
        public Guid? IdParent { get; set; }
        [NotMapped]
        public bool IsAdd { get; set; }
        [NotMapped]
        public bool IsAuToFillTuChi { get; set; }
        [NotMapped]
        public bool IsHangCha { get; set; }
        [NotMapped]
        public bool BHangCha { get; set; }
        [NotMapped]
        public string STTM { get; set; }
    }
}
