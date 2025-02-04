using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhPbdttmBHYTChiTietQuery
    {
        public Guid ID { get; set; }

        [Column("iID_DTTM_BHYT_ThanNhan_PhanBo")]
        public Guid IID_DTTM_BHYT_ThanNhan_PhanBo { get; set; }

        [Column("iID_DTTM_BHYT_ThanNhan")]
        public Guid IID_DTTM_BHYT_ThanNhan { get; set; }

        [Column("iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet")]
        public Guid? IID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet { get; set; }

        [Column("sSoChungTu")]
        public string SSoChungTu { get; set; }

        [Column("dNgayChungTu")]
        public DateTime DNgayChungTu { get; set; }

        [Column("sSoQuyetDinh")]
        public string SSoQuyetDinh { get; set; }

        [Column("dNgayQuyetDinh")]
        public DateTime DNgayQuyetDinh { get; set; }

        [Column("iID_MaDonVi")]
        public string IID_MaDonVi { get; set; }

        [Column("iNamLamViec")]
        public int INamLamViec { get; set; }

        [Column("iID_MLNS")]
        public Guid? IID_MLNS { get; set; }

        [Column("iID_MLNS_Cha")]
        public Guid? IID_MLNS_Cha { get; set; }

        [Column("sLNS")]
        public string SLNS { get; set; }

        [Column("sL")]
        public string SL { get; set; }

        [Column("sK")]
        public string SK { get; set; }

        [Column("sM")]
        public string SM { get; set; }

        [Column("sTM")]
        public string STM { get; set; }

        [Column("sTTM")]
        public string STTM { get; set; }

        [Column("sNG")]
        public string SNG { get; set; }

        [Column("sTNG")]
        public string STNG { get; set; }

        [Column("sNoiDung")]
        public string SNoiDung { get;set; }

        [Column("sXauNoiMa")]
        public string SXauNoiMa { get; set; }

        [Column("fDuToanTruocDieuChinh")]
        public Double? FDuToanTruocDieuChinh { get; set; }

        [Column("fDuToan")]
        public Double? FDuToan { get; set; }

        [Column("fDuToanSauDieuChinh")]
        public Double? FDuToanSauDieuChinh { get; set; }

        [Column("sNguoiTao")]
        public string SNguoiTao { get; set; }

        [Column("sNguoiSua")]
        public string SNguoiSua { get; set; }

        [Column("dNgayTao")]
        public DateTime DNgayTao { get; set; }

        [Column("dNgaySua")]
        public DateTime DNgaySua { get; set; }

        [Column("bIsKhoa")]
        public bool BIsKhoa { get; set; }

        [Column("IsRemainRow")]
        public bool IsRemainRow { get; set; }

        [Column("sTenDonVi")]
        public string STenDonVi { get; set; }

        public int Type { get; set; }

        [Column("bEmty")]
        public bool BEmty { get; set; }

        [Column("bHangCha")]
        public bool BHangCha { get; set; }

    }
}
