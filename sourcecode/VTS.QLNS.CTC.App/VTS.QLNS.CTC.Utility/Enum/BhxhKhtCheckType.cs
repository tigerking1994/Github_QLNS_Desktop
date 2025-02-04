using System.Collections.Generic;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public class EstimateType
    {
        public static Dictionary<EstimateTypeNum, string> EstimateTypeName = new Dictionary<EstimateTypeNum, string>
        {
            {EstimateTypeNum.YEAR, "Đầu năm"},
            {EstimateTypeNum.ADDITIONAL, "Bổ sung"},
            {EstimateTypeNum.ADJUSTED, "Điều chỉnh"},
            {EstimateTypeNum.ALL, "Tất cả"}
        };
    }

    public class Quarters
    {
        public static Dictionary<QuarterEnum, string> QuarterName = new Dictionary<QuarterEnum, string>
        {
            {QuarterEnum.Q1, "Quý I"},
            {QuarterEnum.Q2, "Quý II"},
            {QuarterEnum.Q3, "Quý III"},
            {QuarterEnum.Q4, "Quý IV"}
        };
    }

    public class ExpenseTypes
    {
        public static Dictionary<ExpenseTypeEnum, string> ExpenseTypeName = new Dictionary<ExpenseTypeEnum, string>
        {
            {ExpenseTypeEnum.KCB_QN, "Kinh phí KCB BHYT QN"},
            {ExpenseTypeEnum.KCB_TNQN_NLD, "Kinh phí KCB BHYT TNQN và NLĐ"}
        };
    }

    public class AllocationReportType
    {
        public static Dictionary<ReportCPBSKCBBHYT, string> AllocationReportTypeName = new Dictionary<ReportCPBSKCBBHYT, string>
        {
            {ReportCPBSKCBBHYT.KEHOACH_TNQN, "Kinh phí KCB BHYT quân nhân"},
            {ReportCPBSKCBBHYT.KEHOACH_TNQN_NLD, "Kinh phí KCB BHYT TNQN và NLĐ"},
            {ReportCPBSKCBBHYT.TONGHOP_TNQN, "Kinh phí KCB BHYT quân nhân"},
            {ReportCPBSKCBBHYT.TONGHOP_TNQN_NLD, "Kinh phí KCB BHYT TNQN và NLĐ"},
            {ReportCPBSKCBBHYT.THONGTRI_TNQN, "Kinh phí KCB BHYT quân nhân"},
            {ReportCPBSKCBBHYT.THONGTRI_TNQN_NLD, "Kinh phí KCB BHYT TNQN và NLĐ"}
        };
        public static Dictionary<string, ReportCPBSKCBBHYT> AllocationType = new Dictionary<string, ReportCPBSKCBBHYT>
        {
            {"1",ReportCPBSKCBBHYT.KEHOACH_TNQN},
            {"2",ReportCPBSKCBBHYT.KEHOACH_TNQN_NLD},
            {"3",ReportCPBSKCBBHYT.TONGHOP_TNQN},
            {"4",ReportCPBSKCBBHYT.TONGHOP_TNQN_NLD },
            {"5",ReportCPBSKCBBHYT.THONGTRI_TNQN},
            {"6",ReportCPBSKCBBHYT.THONGTRI_TNQN_NLD}
        };
        public static Dictionary<string, string> AllocationReportTitle2 = new Dictionary<string, string>
        {
            {"1","Cấp bổ sung phí KCB BHYT Quân nhân quý {0} năm {1}"},
            {"2","Cấp bổ sung kinh phí KCB BHYT TNQN và NLĐ quý {0} năm {1}"},
            {"3","Cấp bổ sung kinh phí KCB BHYT Quân nhân"},
            {"4","Cấp bổ sung kinh phí KCB BHYT TNQN và NLĐ"},
            {"5","Cấp bổ sung kinh phí KCB BHYT Quân nhân"},
            {"6","Cấp bổ sung kinh phí KCB BHYT TNQN và NLĐ"}
        };
    }

    public struct BhxhSalary
    {
        public const string CONGCHUAN_BH = "CONGCHUAN_BH";
        public const string GIAMTRU_BH = "OMKHAC_D14NGAY,BENHDAINGAY_D14NGAY,BENHDAINGAY_T14NGAY,OMKHAC_T14NGAY,KHAMTHAI,KHHGD,NAMNGHIKHIVOSINHCON,CONOM,BDN_D14N_BHYTCN_TT,OK_D14N_BHYTCN_TT";
        public const string BENHDAINGAY_D14NGAY = "BENHDAINGAY_D14NGAY";
        public const string BDN_D14N_BHXHCN_TT = "BDN_D14N_BHXHCN_TT";
        public const string OMKHAC_D14NGAY = "OMKHAC_D14NGAY";
        public const string OK_D14N_BHXHCN_TT = "OK_D14N_BHXHCN_TT";
        public const string BDN_D14N_BHYTCN_TT = "BDN_D14N_BHYTCN_TT";
        public const string OK_D14N_BHYTCN_TT = "OK_D14N_BHYTCN_TT";
        public const string HS_TRO_CAP_OM_DAU = "BENHDAINGAY_D14NGAY,BENHDAINGAY_T14NGAY,OMKHAC_D14NGAY,OMKHAC_T14NGAY";
    }

    public struct BhxhCheckScreen
    {
        public const string ROOT_DIALOG = "RootDialog";
        public const string DETAIL_DIALOG = "DetailDialog";
    }
    public struct BhxhMLNS
    {
        public const string KHOI_DU_TOAN = "9020001";
        public const string KHOI_HACH_TOAN = "9020002";
        public const string SI_QUAN_DU_TOAN = "9020001-010-011-0001-0000";
        public const string QNCN_DU_TOAN = "9020001-010-011-0001-0001";
        public const string HSQ_BS_DU_TOAN = "9020001-010-011-0001-0002";
        public const string PHUNHAN_PHUQUAN_DU_TOAN = "9020001-010-011-0001-0003";
        public const string TUYVIEN_QP_DU_TOAN = "9020001-010-011-0001-0004";
        public const string CC_CN_VCQP_DU_TOAN = "9020001-010-011-0002-0000";
        public const string LDHD_DU_TOAN = "9020001-010-011-0002-0001";
        public const string SI_QUAN_HACH_TOAN = "9020002-010-011-0001-0000";
        public const string QNCN_HACH_TOAN = "9020002-010-011-0001-0001";
        public const string HSQ_BS_HACH_TOAN = "9020002-010-011-0001-0002";
        public const string PHUNHAN_PHUQUAN_HACH_TOAN = "9020002-010-011-0001-0003";
        public const string CC_CN_VCQP_HACH_TOAN = "9020002-010-011-0002-0000";
        public const string LDHD_HACH_TOAN = "9020002-010-011-0002-0001";
        public const string THU_MUA_BHYT = "903";
        public const string KCB_BHYT = "904";
        public const string KCB_BHYT_QN = "9050001";
        public const string KCB_BHYT_TNQN_NLD = "9050002";
        public const string KCB_BHYT_CPBS = "904";
        public const string KHT_BHXH_BHYT_BHTN = "902";
        public const string THU_MUA_BHYT_TNQN = "9030001";
        public const string THU_MUA_BHYT_CNVQP = "9030002";
        public const string SLNS_HSSV = "9030004";
        public const string SLNS_LUU_HS = "9030006";
        public const string SLNS_HVQS = "9030003";
        public const string SLNS_SQ_DU_BI = "9030005";
        public const string SLNS_QTC_KCB_QYDV = "9010004,9010005";
        public const string SM_DU_TOAN = "0";
        public const string SM_HACH_TOAN = "1";
        public const string LUONG_CHINH = "LHT_TT";
        public const string PHU_CAP_CHUC_VU = "PCCV_TT";
        public const string PHU_CAP_TNN = "PCTN_TT";
        public const string PHU_CAP_TNVK = "PCTNVK_TT";
        public const string SI_QUAN = "SiQuan";
        public const string QNCN = "QNCN";
        public const string HSQ_BS = "HSQ_BS";
        public const string VCQP = "VCQP";
        public const string LDHD = "LDHD";
        public const string PhuCapHSBL = "HSBL_TT";
        public const string KinhPhi_KCB_BHYT_QN = "9040001";
        public const string KinhPhi_KCB_BHYT_TNQN_NLD = "9040002";
        public const string BHYT_HSSV = "9030003,9030004,9030005,9030006";
        public const string SI_QUAN_PNPQ_DU_TOAN = "9020001-010-011-0001-0003-0001";
        public const string QNCN_PNPQ_DU_TOAN = "9020001-010-011-0001-0003-0002";
        public const string SI_QUAN_TV_DU_TOAN = "9020001-010-011-0001-0004-0001";
        public const string QNCN_TV_DU_TOAN = "9020001-010-011-0001-0004-0002";
        public const string SI_QUAN_TGTG_DU_TOAN = "9020001-010-011-0001-0005-0001";
        public const string QNCN_TGTG_DU_TOAN = "9020001-010-011-0001-0005-0002";
        public const string SI_QUAN_PNPQ_HACH_TOAN = "9020002-010-011-0001-0003-0001";
        public const string QNCN_PNPQ_HACH_TOAN = "9020002-010-011-0001-0003-0002";
    }
    public enum EstimateTypeBHXH
    {
        YEAR = 1,
        LAST_YEAR = 2,
        ADDITIONAL = 3,
        ADJUSTED = 4,
        ADDITIONAL_TRANSFER_LAST_YEAR = 5,
    }
    public struct BHYTHeSoLuong
    {
        public const string LCS = "LCS";
        public const decimal HE_SO_DINH_MUC = 0.045m;
    }
    public struct BhxhLoaiChungTu
    {
        public const int BhxhChungTu = 1;
        public const int BhxhChungTuTongHop = 2;
        public const bool BhxhDaTongHop = true;
    }

    public struct DefaultConst
    {
        public const int BHXH_10_Rows = 10;
    }

    public struct EstimateTitlePrint
    {
        public const string Title1BaoCao = "BÁO CÁO";


        public const string Title2BaoCaoBHXH = "ĐIỀU CHỈNH DỰ TOÁN CHI CHẾ ĐỘ BHXH NĂM";
        public const string Title2BaoCaoKPQL = "ĐIỀU CHỈNH DỰ TOÁN CHI KINH PHI QUẢN LÝ BHXH, BHYT, BHTN NĂM";
        public const string Title2BaoCaoKCBQY = "ĐIỀU CHỈNH DỰ TOÁN CHI KCB TẠI QUÂN Y NĂM";
        public const string Title2BaoCaoKCBTS = "ĐIỀU CHỈNH DỰ TOÁN CHI KCB CHO QUÂN NHÂN TẠI TRƯỜNG SA - DK";
        public const string Title2BaoCaoCSSKHSSV = "ĐIỀU CHỈNH DỰ TOÁN CHI KINH PHÍ CSSK HSSV";
        public const string Title2BaoCaoCSSKNLD = "ĐIỀU CHỈNH DỰ TOÁN CHI KINH PHÍ CSSK NLD";
        public const string Title2BaoCaoTNKCBBHYTQN = "ĐIỀU CHỈNH DỰ TOÁN CHI TỪ NGUỒN KẾT DƯ QUỸ KCB BHYT QUÂN NHÂN ";
        public const string Title2BaoCaoHTBHTN = "ĐIỀU CHỈNH DỰ TOÁN CHI HỖ TRỢ BHTN ";
        public const string Title2BaoCaoMSTTBYT = "ĐIỀU CHỈNH DỰ TOÁN CHI KINH PHÍ MUA SẮM TRANG THIẾT BỊ Y TẾ ";

        public const string Title1BaoCaoDvBHXH = "ĐIỀU CHỈNH DỰ TOÁN CHI CHẾ ĐỘ BHXH NĂM";
        public const string Title1BaoCaoDvKPQL = "ĐIỀU CHỈNH DỰ TOÁN CHI KINH PHI QUẢN LÝ BHXH, BHYT, BHTN NĂM";
        public const string Title1BaoCaoDvKCBQY = "ĐIỀU CHỈNH DỰ TOÁN CHI KCB TẠI QUÂN Y NĂM";
        public const string Title1BaoCaoDvKCBTS = "ĐIỀU CHỈNH DỰ TOÁN CHI KCB CHO QUÂN NHÂN TẠI TRƯỜNG SA - DK";
        public const string Title1BaoCaoDvCSSKHSSV = "ĐIỀU CHỈNH DỰ TOÁN CHI KINH PHÍ CSSK HSSV";
        public const string Title1BaoCaoDvCSSKNLD = "ĐIỀU CHỈNH DỰ TOÁN CHI KINH PHÍ CSSK NLD";
    }
    public enum InsuranceSalaryPrintType
    {
        TRO_CAP_OM_DAU = 1,
        TRO_CAP_THAI_SAN = 2,
        TRO_CAP_TNLD = 3,
        TRO_CAP_HUU_TRI = 4,
        TRO_CAP_XUAT_NGU = 5
    }
    public enum BHXHCheckPrintType
    {
        KE_HOACH_THU_BHXH_BHYT_BHTN = 1,
        PHU_LUC_II = 2,
        PHU_LUC_III = 3,
        PHU_LUC_IV = 4,
        PHU_LUC_V = 5,
        DU_TOAN_THU_CHI_TONG_HOP = 6
    }
    public enum BHYTCheckPrintType
    {
        BHYT_DETAIL = 1,
        BHYT_THAN_NHAN = 2,
        BHYT_HSSV = 3
    }
    public struct BhxhLoaiSM
    {
        public const string QUAN_NHAN = "0001";
        public const string NGUOI_LAO_DONG = "0002";
        public const string TRUY_THU_DU_TOAN = "9020001-010-011-0003";
        public const string TRUY_THU_HACH_TOAN = "9020002-010-011-0003";
    }
    public enum EstimateTypeNum
    {
        ALL = 0,
        YEAR = 1,
        ADDITIONAL = 2,
        ADJUSTED = 3
    }

    public enum EstimateTypeKPQL
    {
        MergeKPQL = 0,
        NotMergeKPQL = 1,
        DetailAgencies = 2,
        AgregateAgencies = 3
    }
    public enum ThangCanCu
    {
        HIEN_TAI = 1,
        GAN_NHAT = 2
    }
    public enum QuarterEnum
    {
        Q1 = 1,
        Q2 = 2,
        Q3 = 3,
        Q4 = 4
    }
    public enum ExpenseTypeEnum
    {
        KCB_QN = 1,
        KCB_TNQN_NLD = 2
    }
    public enum ReportCPBSKCBBHYT
    {
        KEHOACH_TNQN = 1,
        KEHOACH_TNQN_NLD = 2,
        TONGHOP_TNQN = 3,
        TONGHOP_TNQN_NLD = 4,
        THONGTRI_TNQN = 5,
        THONGTRI_TNQN_NLD = 6
    }

    public enum ExplainType
    {
        GIAITHICH_BANGLOI = 1,
        GIAITHICH_TRUYTHU = 2,
        GIAITHICH_TONGHOP_SOSANH = 3,
        GIAITHICH_GIAMDONG = 4
    }

    public enum LoaiDTTThu
    {
        BHXH = 1,
        BHTN = 2,
        BHYT_QN = 3,
        BHYT_NLD = 4
    }

    public enum LoaiDTTM
    {
        BHYT_TN = 1,
        BHYT_HSSV = 2
    }

    public struct BHYTCauHinhThamSo
    {
        public const string LCS = "LCS";
        public const string HESO_BHYT = "HESO_BHYT";
    }

    public enum SettlementReportTypeNum
    {
        Detail = 1,
        Aggregate = 2
    }

    public class SettlementReportType
    {
        public static Dictionary<SettlementReportTypeNum, string> ReportType = new Dictionary<SettlementReportTypeNum, string>
        {
            {SettlementReportTypeNum.Detail, "Báo cáo chi tiết"},
            {SettlementReportTypeNum.Aggregate, "Báo cáo tổng hợp"}
        };
    }
    public class CollectTypeDisplay
    {
        public const string TAT_CA = null;
        public const string BHXH = "BHXH";
        public const string BHYT = "BHYT";
        public const string BHTN = "BHTN";
    }
}
