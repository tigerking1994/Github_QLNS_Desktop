using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public class SociallnsuranceAllocationType
    {

    }
    public struct AllocationTypeLoaiChungTu
    {
        public const int ChungTu = 1;
        public const int ChungTuTongHop = 2;
    }

    public enum AllocationTypeLoaiKinhPhi
    {
        CHI_CAC_CHE_DO_BHXH = 1,
        CHI_KINH_PHI_Ql_BHXH_BHYT = 2,
        CHI_KINH_PHI_KCB_QYDV = 3,
        CHI_KINH_PHI_KCB_TS = 4,
        CHI_KINH_PHI_CHAM_SSK_BANDAU_HSSV = 5,
        CHI_KINH_PHI_CHAM_SSK_BANDAU_NLD = 6,
        CHI_KINH_PHI_TU_NGUOC_KET_DU_QUY_KCB_BHYT_QN = 7,
    }

    public struct AllocationTitle
    {
        public const string Title1 = "THÔNG TRI";
        public const string Title2BHXH = "Cấp kinh phí chi các chế độ bảo hiểm xã hội";
        public const string Title2KPQL = "Cấp kinh phí quản lý bảo hiểm xã hội, bảo hiểm y tế";
        public const string Title2KCBQY = "Cấp kinh phí KCB tại quân y đơn vị";
        public const string Title2KCBTS = "Cấp kinh phí KCB tại Trường sa - DK";
        public const string Title2CSSKNLD = "Cấp kinh phí chăm sóc sức khỏe ban đầu người lao động";
        public const string Title2CSSKHSSV = "Cấp kinh phí chăm sóc sức khỏe ban đầu học sinh, sinh viên";
        public const string Title2KCBBHYT = "Cấp kinh phí từ nguồn kế dư Quỹ KCB BHYT quân nhân";
    }
    public enum AllocationPrintTypeOfBH
    {
        PRINT_AllOCATION_NOTICE = 1,
        PRINT_ALLOCATION_PLAN = 2,
        PRINT_ALLOCATION_NOTICE_DETAIL = 3,
        PRINT_ALLOCATION_PLAN_DETAIL = 4,
        PRINT_ALLOCATION_AGENCY=5,
        PRINT_ALLOCATION_TYPES = 6
    }
    public struct AllocationMLNS
    {
        public const string STM = "0";
    }
}
