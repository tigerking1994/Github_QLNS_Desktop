using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhDcDuToanThuChiTietQuery
    {
        [Column("iID_DTT_BHXH_DieuChinh_ChiTiet")]
        public Guid Id { get; set; }
        [Column("iID_DTT_BHXH_DieuChinh")]
        public Guid IIDDttDieuChinh { get; set; }
        [Column("iID_MucLucNganSach")]
        public Guid IIDMLNS { get; set; }
        [Column("sNoiDung")]
        public string SNoiDung { get; set; }

        [Column("fThuBHXH_NLD")]
        public double? FThuBHXHNLD { get; set; }
        [Column("fThuBHXH_NSD")]
        public double? FThuBHXHNSD { get; set; }

        [Column("fThuBHYT_NLD")]
        public double? FThuBHYTNLD { get; set; }
        [Column("fThuBHYT_NSD")]
        public double? FThuBHYTNSD { get; set; }
        [Column("fThuBHTN_NLD")]
        public double? FThuBHTNNLD { get; set; }
        [Column("fThuBHTN_NSD")]
        public double? FThuBHTNNSD { get; set; }
        [Column("fThuBHXH_NLD_QTDauNam")]
        public double? FThuBHXHNLDQTDauNam { get; set; }
        [Column("fThuBHXH_NSD_QTDauNam")]
        public double? FThuBHXHNSDQTDauNam { get; set; }
        [Column("fThuBHYT_NLD_QTDauNam")]
        public double? FThuBHYTNLDQTDauNam { get; set; }
        [Column("fThuBHYT_NSD_QTDauNam")]
        public double? FThuBHYTNSDQTDauNam { get; set; }
        [Column("fThuBHTN_NLD_QTDauNam")]
        public double? FThuBHTNNLDQTDauNam { get; set; }
        [Column("fThuBHTN_NSD_QTDauNam")]
        public double? FThuBHTNNSDQTDauNam { get; set; }
        [Column("fThuBHXH_NLD_QTCuoiNam")]
        public double? FThuBHXHNLDQTCuoiNam { get; set; }
        [Column("fThuBHXH_NSD_QTCuoiNam")]
        public double? FThuBHXHNSDQTCuoiNam { get; set; }
        [Column("fThuBHYT_NLD_QTCuoiNam")]
        public double? FThuBHYTNLDQTCuoiNam { get; set; }
        [Column("fThuBHYT_NSD_QTCuoiNam")]
        public double? FThuBHYTNSDQTCuoiNam { get; set; }
        [Column("fThuBHTN_NLD_QTCuoiNam")]
        public double? FThuBHTNNLDQTCuoiNam { get; set; }
        [Column("fThuBHTN_NSD_QTCuoiNam")]
        public double? FThuBHTNNSDQTCuoiNam { get; set; }
        [Column("fTongThuBHXH_NLD")]
        public double? FTongThuBHXHNLD { get; set; }
        [Column("fTongThuBHXH_NSD")]
        public double? FTongThuBHXHNSD { get; set; }
        [Column("fTongThuBHYT_NLD")]
        public double? FTongThuBHYTNLD { get; set; }
        [Column("fTongThuBHYT_NSD")]
        public double? FTongThuBHYTNSD { get; set; }
        [Column("fTongThuBHTN_NLD")]
        public double? FTongThuBHTNNLD { get; set; }
        [Column("fTongThuBHTN_NSD")]
        public double? FTongThuBHTNNSD { get; set; }
        [Column("fTongCong")]
        public double? FTongCong { get; set; }
        [Column("fThuBHXH_NLD_Tang")]
        public double? FThuBHXHNLDTang { get; set; }
        [Column("fThuBHXH_NSD_Tang")]
        public double? FThuBHXHNSDTang { get; set; }
        [Column("fThuBHXH_Tang")]
        public double? FThuBHXHTang { get; set; }
        [Column("fThuBHYT_NLD_Tang")]
        public double? FThuBHYTNLDTang { get; set; }
        [Column("fThuBHYT_NSD_Tang")]
        public double? FThuBHYTNSDTang { get; set; }
        [Column("fThuBHYT_Tang")]
        public double? FThuBHYTTang { get; set; }
        [Column("fThuBHTN_NLD_Tang")]
        public double? FThuBHTNNLDTang { get; set; }
        [Column("fThuBHTN_NSD_Tang")]
        public double? FThuBHTNNSDTang { get; set; }
        [Column("fThuBHTN_Tang")]
        public double? FThuBHTNTang { get; set; }
        [Column("fThuBHXH_NLD_Giam")]
        public double? FThuBHXHNLDGiam { get; set; }
        [Column("fThuBHXH_NSD_Giam")]
        public double? FThuBHXHNSDGiam { get; set; }
        [Column("fThuBHXH_Giam")]
        public double? FThuBHXHGiam { get; set; }
        [Column("fThuBHYT_NLD_Giam")]
        public double? FThuBHYTNLDGiam { get; set; }
        [Column("fThuBHYT_NSD_Giam")]
        public double? FThuBHYTNSDGiam { get; set; }
        [Column("fThuBHYT_Giam")]
        public double? FThuBHYTGiam { get; set; }
        [Column("fThuBHTN_NLD_Giam")]
        public double? FThuBHTNNLDGiam { get; set; }
        [Column("fThuBHTN_NSD_Giam")]
        public double? FThuBHTNNSDGiam { get; set; }
        [Column("fThuBHTN_Giam")]
        public double? FThuBHTNGiam { get; set; }

        [Column("dNgaySua")]
        public DateTime? DNgaySua { get; set; }
        [Column("dNgayTao")]
        public DateTime? DNgayTao { get; set; }
        [Column("sNguoiSua")]
        public string SNguoiSua { get; set; }
        [Column("sNguoiTao")]
        public string SNguoiTao { get; set; }
        [Column("sGhiChu")]
        public string SGhiChu { get; set; }
        [Column("iID_MaDonVi")]
        public string IIdMaDonVi { get; set; }
        [Column("iNamLamViec")]
        public int? INamLamViec { get; set; }
        public string SXauNoiMa { get; set; }
        public bool? IsHangCha { get; set; }
        public string SMoTa { get; set; }
        public int ICountRow { get; set; }
    }
}
