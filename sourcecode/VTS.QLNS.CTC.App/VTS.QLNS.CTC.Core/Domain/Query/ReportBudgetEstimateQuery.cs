using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportBudgetEstimateQuery
    {
        public Guid? IIDMLNS {  get; set; }
        public Guid? IIDMLNSCha { get; set; }
        public Guid? IIDMLSKT { get; set; }
        public Guid? IIDMLSKTCha { get; set; }
        public bool IsHangCha { get; set; }
        public string SLNS { get; set; }
        public string SL { get; set; }
        public string SK { get; set; }
        public string SM { get; set; }
        public string STM { get; set; }
        public string STTM { get; set; }
        public string SNG { get; set; }
        public string STNG { get; set; }
        public string STNG1 { get; set; }
        public string STNG2 { get; set; }
        public string STNG3 { get; set; }
        public string SXauNoiMa { get; set; }
        public string SKyHieu { get; set; }
        public string SMoTa { get; set; }
        public double FMucTienPhanBo { get; set; }
        public double FTuChi { get; set; }
        public double FHangNhap { get; set; }
        public double FHangMua { get; set; }
        public double SSoSanh { get; set; }
        public double FHangNhapBanThan { get; set; }
        public double FHangMuaBanThan { get; set; }
        public double HangNhapDV1 { get; set; }
        public double HangMuaDV1 { get; set; }
        public double HangNhapDV2 { get; set; }
        public double HangMuaDV2 { get; set; }
        public double HangNhapDV3 { get; set; }
        public double HangMuaDV3 { get; set; }
        public double HangNhapDV4 { get; set; }
        public double HangMuaDV4 { get; set; }
        public double HangNhapDV5 { get; set; }
        public double HangMuaDV5 { get; set; }
        public double TongHangNhap { get; set; }
        public double TongHangMua { get; set; }
        public double FTuChiBanThan { get; set; }
        public double TuChiDV1 { get; set; }
        public double TuChiDV2 { get; set; }
        public double TuChiDV3 { get; set; }
        public double TuChiDV4 { get; set; }
        public double TuChiDV5 { get; set; }
        public double TuChiDV6 { get; set; }
        public double TongTuChi { get; set; }
        public string IIdMaDonVi { get; set; }
        public List<DataReportDynamic> ListDataValue { get; set; } = new List<DataReportDynamic>();
        public List<DataReportDynamic2> ListDataValue2 { get; set; } = new List<DataReportDynamic2>();
        public string MergeRange { get; set; } = default;
    }
}
