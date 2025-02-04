using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public class BhKhcKinhphiQuanlyChiTiet : EntityBase
    {
        [Column("iID_BH_KHC_KinhPhiQuanLy_ChiTiet")]
        [Key]
        public override Guid Id { get => base.Id; set => base.Id = value; }
        [Column("iID_KHC_KinhPhiQuanLy")]
        public Guid IID_KHC_KinhPhiQuanLy { get; set; }
        [Column("iID_MucLucNganSach")]
        public Guid IID_MucLucNganSach { get; set; }
        [Column("sM")]
        public string SM { get; set; }
        [Column("sTM")]
        public string STM { get; set; }
        [Column("sNoiDung")]
        public string SNoiDung { get; set; }
        [Column("fTienDaThucHienNamTruoc")]
        public double? FTienDaThucHienNamTruoc { get; set; }
        [Column("fTienUocThucHienNamTruoc")]
        public double? FTienUocThucHienNamTruoc { get; set; }
        [Column("fTienKeHoachThucHienNamNay")]
        public double? FTienKeHoachThucHienNamNay { get; set; }
        [Column("fTienCanBo")]
        public double? FTienCanBo { get; set; }
        [Column("fTienQuanLuc")]
        public double? FTienQuanLuc { get; set; }
        [Column("fTienTaiChinh")]
        public double? FTienTaiChinh { get; set; }
        [Column("fTienQuanY")]
        public double? FTienQuanY { get; set; }
        [Column("sGhiChu")]
        public string SGhiChu { get; set; }
        [Column("dNgaySua")]
        public DateTime? DNgaySua { get; set; }
        [Column("dNgayTao")]
        public DateTime? DNgayTao { get; set; }
        [Column("sNguoiSua")]
        public string SNguoiSua { get; set; }
        [Column("sNguoiTao")]
        public string SNguoiTao { get; set; }

        public string IIdMaDonVi { get; set; }

        public int INamLamViec { get; set; }
        public string SXauNoiMa { get; set; }


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
