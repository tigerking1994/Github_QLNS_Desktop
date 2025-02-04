using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Utility
{
    public class BHXHConstants
    {
        public const double BHXH_NLD = 0.08;
        public const double BHXH_NSD = 0.175;
        public const double BHXH_NSD_K = 0.225;
        public const double BHYT_NLD = 0.015;
        public const double BHYT_NSD = 0.045;
        public const double BHYT_NSD_K = 12;
        public const double BHYT_NSD_KK = 0.03;
        public const double KHC_KCB_TYLETHU_DEFAULT = 10;
        public const double CKP_TYLETHU_DEFAULT = 25;
        public const double CKP_TYLETHU_NOTDEFAULT = 0;


        public static string GROUP_PLAN_REPORT = "BÁO CÁO - THỐNG KÊ";
        public static string GROUP_ESTIMATE_REPORT = "BÁO CÁO - THỐNG KÊ";
        public static string GROUP_ALLOCATION_REPORT = "BÁO CÁO - THỐNG KÊ";
        public static string GROUP_SETTLEMENT_REPORT = "BÁO CÁO - THỐNG KÊ";

        public const string KHT_BHXH_BHYT_BHTN = "KHT_BHXH_BHYT_BHTN";
        public const string KHTM_BHYT_THANNHAN = "KHTM_BHYT_THANNHAN";
        public const string KHC_CHEDOBHXH = "KHC_CHEDOBHXH";
        public const string KHC_KINHPHIQUANLY = "KHC_KINHPHIQUANLY";
        public const string KHC_KCB_QUANYDONVI = "KHC_KCB_QUANYDONVI";
        public const string KHC_KINHPHIKHAC = "KHC_KINHPHIKHAC";
        public const string TONGHOP_KH_THU_CHI = "TONGHOP_KH_THU_CHI";

        public const string GIAO_DUTOAN_THU_CHI = "GIAO_DUTOAN_THU_CHI";

        public const string DIEUCHINH_DUTOAN_THU = "DIEUCHINH_DUTOAN_THU";
        public const string DIEUCHINH_DUTOAN_CHI = "DIEUCHINH_DUTOAN_CHI";

        public const string CAP_KINH_PHI = "CAP_KINH_PHI";
        public const string CAP_TAM_UNG_KCB_BHYT = "CAP_TAM_UNG_KCB_BHYT";
        public const string CAP_BOSUNG_KCB_BHYT = "CAP_BOSUNG_KCB_BHYT";

        public const string QT_THU_BHXH_BHYT_BHTN = "QT_THU_BHXH_BHYT_BHTN";
        public const string QT_THU_BHYT_THANNHAN = "QT_THU_BHYT_THANNHAN";

        public const string QTC_CHEDOBHXH_QUY = "QTC_CHEDOBHXH_QUY";
        public const string QTC_KINHPHIQUANLY_QUY = "QTC_KINHPHIQUANLY_QUY";
        public const string QTC_KCB_QUANLYDONVI_QUY = "QTC_KCB_QUANLYDONVI_QUY";
        public const string QTC_KINHPHIKHAC_QUY = "QTC_KINHPHIKHAC_QUY";

        public const string QTC_CHEDOBHXH_NAM = "QTC_CHEDOBHXH_NAM";
        public const string QTC_KINHPHIQUANLY_NAM = "QTC_KINHPHIQUANLY_NAM";
        public const string QTC_KCB_QUANYDONVI_NAM = "QTC_KCB_QUANYDONVI_NAM";
        public const string QTC_KINHPHIKHAC_NAM = "QTC_KINHPHIKHAC_NAM";

        public const string TONGHOP_QT_THU_CHI_BHXH = "TONGHOP_QT_THU_CHI_BHXH";
    }

    public class CapKinhPhi
    {
        public static string GetLns(int iLoaiKinhPhi)
        {
            switch (iLoaiKinhPhi)
            {
                case (int)TypeCapKinhPhiBHYT.KINH_PHI_KCB_BHYT_QUAN_NHAN:
                    return LNSValue.LNS_9040001;
                case (int)TypeCapKinhPhiBHYT.KINH_PHI_KCB_BHYT_QN_NLD:
                    return LNSValue.LNS_9040002;
                default:
                    return null;
            }
        }
    }

    public enum TypeSettlement
    {
        THU = 1,
        CHI = 2,
    }

    public enum ConstantNumber
    {
        ZERO = 0,
        ONE = 1,
        TEN = 10
    }

    public enum AdjustSummaryReportType
    {
        AgencyDetail = 0,
        AgencySummary = 1,
    }

    public enum NoteTypeBhxh
    {
        AgencyDetail = 1,
        AgencySummary = 2,
        AgencyDetailSummary = 3,
    }

    public enum LoaiCauHinh
    {
        MacDinh = 0,
        GhiChu = 1,
        CanCu = 2,
        TatCa = 3
    }

    public enum NoteColWidth
    {
        Normal = 300,
        Extend = 580
    }
}
