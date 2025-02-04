using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhQtcqGiaiThichTroCapTaiNanQuery
    {
        public string STT { get; set; }
        public string STenCanBo { get; set; }
        public string SMaCapBac { get; set; }
        public string SSoQuyetDinh { get; set; }
        public string STenPhanHo {  get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public double? FTienGiamDinh { get; set; }
        public double? FTienGiamDinhTL { get; set; }
        public double? FTienTroCap1Lan { get; set; }
        public double? FTienTroCap1LanTL { get; set; }
        public double? FTienTCTP { get; set; }
        public double? FTienTCTPTL { get; set; }
        public double? FTienTCHangThang { get; set; }
        public double? FTienTCHangThangTL { get; set; }
        public double? FTienTCPHCNvPV { get; set; }
        public double? FTienTCPHCNvPVTL { get; set; }
        public double? FTienTCCDTNLD { get; set; }
        public double? FTienTCCDTNLDTL { get; set; }
        public int? ISoNgayDSPHSK { get; set; }
        public int? ISoNgayDSPHSKTL { get; set; }
        public double? FTienDSPHSK { get; set; }
        public double? FTienDSPHSKTL { get; set; }
        public double? FTongTienCong => FTienGiamDinh.GetValueOrDefault(0) + FTienTroCap1Lan.GetValueOrDefault(0) + FTienTCTP.GetValueOrDefault(0) + FTienTCHangThang.GetValueOrDefault(0) + FTienTCPHCNvPV.GetValueOrDefault(0) + FTienTCCDTNLD.GetValueOrDefault(0) + FTienDSPHSK.GetValueOrDefault(0);
        public double? FTongTienCongTL => FTienGiamDinhTL.GetValueOrDefault(0) + FTienTroCap1LanTL.GetValueOrDefault(0) + FTienTCTPTL.GetValueOrDefault(0) + FTienTCHangThangTL.GetValueOrDefault(0) + FTienTCPHCNvPVTL.GetValueOrDefault(0) + FTienTCCDTNLDTL.GetValueOrDefault(0) + FTienDSPHSKTL.GetValueOrDefault(0);
        public double? FTongCong => FTongTienCong.GetValueOrDefault(0) + FTongTienCongTL.GetValueOrDefault(0);
        public string SNgayQuyetDinh { get; set; }
        public bool IsHadData => FTienGiamDinh.GetValueOrDefault(0) != 0 || FTienGiamDinhTL.GetValueOrDefault(0) != 0
                                 || FTienTroCap1Lan.GetValueOrDefault(0) != 0 || FTienTroCap1LanTL.GetValueOrDefault(0) != 0
                                 || FTienTCTP.GetValueOrDefault(0) != 0 || FTienTCTPTL.GetValueOrDefault(0) != 0
                                 || FTienTCHangThang.GetValueOrDefault(0) != 0 || FTienTCHangThangTL.GetValueOrDefault(0) != 0
                                 || FTienTCPHCNvPV.GetValueOrDefault(0) != 0 || FTienTCPHCNvPVTL.GetValueOrDefault(0) != 0
                                 || FTienTCCDTNLD.GetValueOrDefault(0) != 0 || FTienTCCDTNLDTL.GetValueOrDefault(0) != 0
                                 || ISoNgayDSPHSK.GetValueOrDefault(0) != 0 || ISoNgayDSPHSK.GetValueOrDefault(0) != 0
                                 || FTienDSPHSK.GetValueOrDefault(0) != 0 || FTienDSPHSKTL.GetValueOrDefault(0) != 0;
        public bool IsHangCha { get;set; }
        public int Type { get;set; }
        public bool? IsParent { get;set; }
    }
}
