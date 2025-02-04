using System.Collections.Generic;

namespace VTS.QLNS.CTC.Utility
{
    public class NSConstants
    {
        public const string QLNS_DB_NAME = "QLNS";
        public const string BO_QUOC_PHONG = "Bộ Quốc Phòng";

        public const int NUMBER_MAX_LENGTH = 17;
        public const int ReportRowHeight = 140;
        public const int NOT_FOUND_INDEX = -1;
        public const int ZERO = 0;
        public const int MLNS_LENGTH_1 = 1;
        public const int MLNS_LENGTH_3 = 3;
        public const int MLNS_LENGTH_5 = 5;
        public const int MLNS_LENGTH_7 = 7;
        public const int DEFAULT_INDEX = 1;
        public const string ROWHEIGHT = "ROWHEIGHT";
        public const string SO_NHU_CAU = "SO_NHU_CAU";
        public const string SO_NHU_CAU_3_NAM = "SO_NHU_CAU_3_NAM";
        public const string NGANH_THAM_DINH = "NGANH_THAM_DINH";
        public const string SO_KIEM_TRA_NHAN = "SO_KIEM_TRA_NHAN";
        public const string SO_KIEM_TRA_PHAN_BO = "SO_KIEM_TRA_PHAN_BO";
        public const string DU_TOAN_DAU_NAM = "DU_TOAN_DAU_NAM";
        public const string DU_TOAN_NHAN_PHAN_BO = "DU_TOAN_NHAN_PHAN_BO";
        public const string DU_TOAN_PHAN_BO = "DU_TOAN_PHAN_BO";
        public const string DU_TOAN_DIEU_CHINH = "DU_TOAN_DIEU_CHINH";
        public const string CAP_PHAT = "CAP_PHAT";
        public const string QUYET_TOAN = "QUYET_TOAN";
        public const string BANG_KE = "BANG_KE";
        public const string QUAN_SO = "QUAN_SO";
        public const string QUYET_TOAN_NAM = "QUYET_TOAN_NAM";
        public const string ADMIN_GROUP = "Admin";
        public const string ADMIN = "ADMIN";
        public const string SYSTEM = "SYSTEM";
        public const string SUPER_ADMIN = "SUPERADMIN";
        public const string SUPER_ADMIN_PWD = "000000";
        public const string NHU_CAU_CHI_QUY = "NHU_CAU_CHI_QUY";
        public const string QUYET_TOAN_QUY = "QuyetToanQuy";
        public const string CHUNG_TU_KHOI_TAO = "Chứng từ chi tiết khởi tạo đầu kì";
    }

    public class LoaiModuleMLNS
    {
        public const string NHAN_DU_TOAN = "NHAN_DU_TOAN";
        public const string PHAN_BO_DU_TOAN = "PHAN_BO_DU_TOAN";
        public const string DU_TOAN_DAU_NAM = "DU_TOAN_DAU_NAM";
        public const string PHAN_CAP_NGAN_SACH_NGANH = "PHAN_CAP_NGAN_SACH_NGANH";
        public const string QUYET_TOAN = "QUYET_TOAN";
        public const string CAP_PHAT = "CAP_PHAT";
    }

    public class LoaiChiBangKe
    {
        public const int ChiTSCD = 1;
        public const int ChiTrucTiep = 2;
        public const int ChiNhapKho = 3;
    }

    public class LoaiDonVi
    {
        public const string ROOT = "0";
        public const string NOI_BO = "1";
        public const string TOAN_QUAN = "2";
    }

    public class LoaiDonViBanHanh
    {
        public const string DON_VI_QUAN_LY = "1";
        public const string DON_VI_SU_DUNG = "2";
        public const string CAP_QUAN_LY_TAI_CHINH = "3";
        public const string DON_VI_DUOC_CHON = "4";
        public const string TUY_CHINH = "5";
    }
    public class KhoiDonVi
    {
        public const string TAT_CA = "0";
        public const string DOANH_NGHIEP = "1";
        public const string DU_TOAN = "2";
        public const string BENH_VIEN_TU_CHU = "3";
    }
    public class SystemConstants
    {
        public const string ROOT_DIALOG = "RootDialog";
        public const string DETAIL_DIALOG = "DetailDialog";
    }

    public class Separator
    {
        public const string COMMA_SPLIT = ", ";
        public const string COMMA_UNDERLINE = "_";
    }

    public class NSLabel
    {
        public const string SELECTED_COUNT_GROUPS_STR = "Chọn nhóm ({0}/{1})";
        public const string SELECTED_COUNT_TL_DONVI_STR = "Chọn phân hộ ({0}/{1})";
        public const string SELECTED_COUNT_AUTHORITIES_STR = "Chọn quyền ({0}/{1})";
        public const string SELECTED_COUNT_DONVI_STR = "Chọn đơn vị ({0}/{1})";
        public const string SELECTED_COUNT_LNS_STR = "Chọn LNS ({0}/{1})";
    }

    public class ThoiGian
    {
        public const string QUY1 = "1,2,3";
        public const string QUY2 = "4,5,6";
        public const string QUY3 = "7,8,9";
        public const string QUY4 = "10,11,12";
        public const string CANAM = "1,2,3,4,5,6,7,8,9,10,11,12";
    }

    public class NSChiTietToi
    {
        public const int NGANH = 0;
        public const int TIEU_MUC = 1;
        public const int MUC = 2;
        public const int TIEU_TIET_MUC = 3;

        public const string MOTA_NGANH = "Ngành";
        public const string MOTA_TIEU_MUC = "Tiểu mục";
        public const string MOTA_MUC = "Mục";

        public const string DB_VALUE_NGANH = "NG";
        public const string DB_VALUE_TIEU_MUC = "TM";
        public const string DB_VALUE_MUC = "M";
    }

    public class LoaiCap
    {
        public const int CAP_UNG = 1;
        public const int CAP_HOP_THUC = 2;
        public const int CAP_THANH_KHOAN = 3;
        public const int CAP_THU = 4;
        public const string CAP_UNG_DISPLAY = "Cấp ứng";
        public const string CAP_HOP_THUC_DISPLAY = "Cấp hợp thức";
        public const string CAP_THANH_KHOAN_DISPLAY = "Cấp thanh khoản";
        public const string CAP_THU_DISPLAY = "Cấp thu";

        public static string GetName(string input)
        {
            int type = 0;
            if (!string.IsNullOrEmpty(input))
            {
                type = int.Parse(input);
            }
            else
            {
                return string.Empty;
            }
            switch (type)
            {
                case CAP_UNG:
                    return CAP_UNG_DISPLAY;
                case CAP_HOP_THUC:
                    return CAP_HOP_THUC_DISPLAY;
                case CAP_THANH_KHOAN:
                    return CAP_THANH_KHOAN_DISPLAY;
                case CAP_THU:
                    return CAP_THU_DISPLAY;
                default:
                    return string.Empty;
            }
        }
    }

    public class AdjustTypes
    {
        public const string Add = "BoXung";
        public const string Sub = "DieuChinh";

        public static string GetName(string sType)
        {
            switch (sType)
            {
                case AdjustTypes.Add:
                    return AdjustTypeNames.ADD;
                case AdjustTypes.Sub:
                    return AdjustTypeNames.SUB;
                default:
                    return string.Empty;

            }
        }
    }

    public struct AdjustTypeNames
    {
        public const string ADD = "Bổ sung(+)";
        public const string SUB = "Điều chỉnh(-)";
    }

    public class NSEntityStatus
    {
        public const int ACTIVED = 1;
        public const int NON_ACTIVED = -1;
    }

    public static class DBContextSaveChangeState
    {
        public const int ERROR = 0;
        public const int SUCCESS = 1;
    }

    public static class TypeDisplay
    {
        public const string TAT_CA = "TAT_CA";
        public const string DA_NHAP = "DA_NHAP";
        public const string DU_LIEU_CHUA_CHUYEN_DOI = "DU_LIEU_CHUA_CHUYEN_DOI";
        public const string DA_NHAN_DUTOAN = "DA_NHANH_DUTOAN";
        public const string CO_DU_LIEU = "CO_DU_LIEU";
        public const string KHONG_CO_DU_LIEU = "KHONG_CO_DU_LIEU";
        public const string CO_SO_LIEU_DT_QT_SKT = "Có số liệu(DT,QT,SKT)";
        public const string DA_NHAP_SKT = "Đã nhập SKT";
        public const string HIEN_THI_TAT_CA = "Hiển thị tất cả";
        public const string CO_SO_LIEU = "Có số liệu";
        public const string DA_NHAP_SO_DU_TOAN = "Đã nhập số dự toán";
        public const string DA_NHAP_CON_LAI = "Có còn lại";
        public const string CHUA_CO_SO_LIEU = "Chưa có số liệu";

        public const string BH_TAT_CA = "Tất cả";


        public const string TONG_DONVI = "Tổng số";
        public const string CHITIET_DONVI = "Chi tiết đơn vị";

        public const string SO_LIEU_BAO_CAO = "SO_LIEU_BAO_CAO";
        public const string SO_LIEU_THAM_DINH = "SO_LIEU_THAM_DINH";
        public const string SO_LIEU_CHENH_LECH = "SO_LIEU_CHENH_LECH";
        public const string SO_LIEU_BC_TD = "SO_LIEU_BC_TD";
    }

    public enum LoaiThu
    {
        All = 1,
        BHXH = 2,
        BHYT = 3,
        BHTN = 4
    }

    public enum TCPrintType
    {
        All = 1,
        Thu = 2,
        Chi = 3
    }

    public enum QuarterMonth
    {
        MONTH = 0,
        QUARTER = 1,
        YEAR = 2,
    }

    public enum LoaiCapPhat
    {
        CAP_TREN = 0,
        CAP_DUOI = 1
    }

    public class QlnsData
    {
        public static Dictionary<string, string> namNganSach = new Dictionary<string, string> { { "1", "1 - Năm trước đã cấp" }, { "2", "2 - Năm nay" }, { "4", "4 - Năm trước chưa cấp" } };
        public static Dictionary<string, string> nguonNganSach = new Dictionary<string, string> { { "1", "1 - Ngân sách quốc phòng" }, { "2", "2 - Ngân sách nhà nước" } };
    }

    public class LoaiBaoCao
    {
        public const string CHI_TIET_TUNG_DON_VI = "Chi tiết từng đơn vị";
        public const string TONG_HOP_DON_VI = "Tổng hợp đơn vị";
        public const string TONG_HOP_DON_VI_LNS1 = "Tổng hợp đơn vị - LNS1";
        public const string TONG_HOP_DON_VI_LNS3 = "Tổng hợp đơn vị - LNS3";
        public const string TONG_HOP_DON_VI_LNS = "Tổng hợp đơn vị - LNS";

        public const string TONG_HOP_QUYET_TOAN_DU_AN_HOAN_THANH_TO_TRINH = "Tổng hợp quyết toán dự án hoàn thành tờ trình";
        public const string TONG_HOP_QUYET_TOAN_DU_AN_HOAN_THANH_PHU_LUC = "Tổng hợp quyết toán dự án hoàn thành phụ lục";
        public const string TONG_HOP_QUYET_TOAN_DU_AN_HOAN_THANH_PHU_LUC_TONGQUAT = "Tổng hợp quyết toán dự án hoàn thành phụ lục tổng quát";
        public const string TONG_HOP_QUYET_TOAN_DU_AN_HOAN_THANH_PHU_LUC_CHITIET = "Tổng hợp quyết toán dự án hoàn thành phụ lục chi tiết";

        public const string MAC_DINH = "Mặc định";
        public const string TOI_NGANH = "Ngành";
        public const string TIEU_MUC = "Tiểu mục";
        public const string MUC = "Mục";
        public const string LNS = "LNS";

        public const string DU_TOAN_DAU_NAM_CHITIET_DONVI = "Chi tiết đơn vị";
        public const string DU_TOAN_DAU_NAM_CHI_NGAN_SACH = "Báo cáo dự toán chi ngân sách";
        public const string DU_TOAN_DAU_NAM_TONG_HOP = "Tổng hợp";
        public const string DU_TOAN_DAU_NAM_TONG_HOP_DU_TOAN = "Tổng hợp dự toán(MLNS hàng dọc, đơn vị hàng ngang)";
        public const string DU_TOAN_DAU_NAM_TONG_HOP_DU_TOAN_UOC_THUC_HIEN = "Tổng hợp DT - Ước thực hiện(MLNS hàng dọc, đơn vị hàng ngang)";
        public const string DU_TOAN_DAU_NAM_TONG_HOP_DU_TOAN_DONVI0 = "Tổng hợp dự toán";
        public const string DU_TOAN_DAU_NAM_CHI_TIET_THEO_NGANH = "Báo cáo dự toán đầu năm chi tiết theo ngành";

        public const string DU_TOAN_DAU_NAM_DU_TOAN_NAM = "Dự toán năm (Ngành nghiệp vụ bảo đảm toàn quân)";
        public const string DU_TOAN_DAU_NAM_CHI_TIET_THEO_DON_VI_SU_DUNG = "Dự toán năm chi tiết theo đơn vị sử dụng (Ngành nghiệp vụ bảo đảm toàn quân)";
        public const string DU_TOAN_DAU_NAM_DUTOAN_PHANBO_NGANSACH_DACTHU_DOC = "Dự toán phân bổ ngân sách đặc thù cho các đơn vị (Mục lục dọc - Đơn vị ngang)";
        public const string DU_TOAN_DAU_NAM_DUTOAN_PHANBO_NGANSACH_DACTHU_NGANG = "Dự toán phân bổ ngân sách đặc thù cho các đơn vị (Đơn vị dọc - Mục lục ngang)";
        public const string DU_TOAN_DAU_NAM_DUTOAN_CHIMUAHANGTAPTRUNG_CAPHIENVAT = "Dự toán chi mua hàng tập trung để cấp hiện vật cho đơn vị";
        public const string TONG_HOP_DU_TOAN_NGAN_SACH_DAC_THU_NGANH = "Báo cáo tổng hợp dự toán ngân sách đặc thù ngành";

        public const string DUTOAN_CHITIET_DONVI = "Chi tiết đơn vị";
        public const string DUTOAN_TONGHOP_DONVI = "Tổng hợp - đơn vị";
        public const string DUTOAN_TONGHOP_DONVI_MLNS_DOC = "Tổng hợp - đơn vị MLNS dọc";
        public const string DUTOAN_TONGHOP_SOPHANBO = "Tổng hợp số phân bổ";
        public const string TONG_HOP_DON_VI_THEO_KHOI = "Tổng hợp đơn vị theo khối";

        public const string BaoHiemThamDinhQuyetToanChiCsskHssv = "Phê duyệt quyết toán chi CSSK HSSV";
        public const string BaoHiemThamDinhQuyetToanChiCsskNld = "Phê duyệt quyết toán chi CSSK NLĐ";

        public const string DU_TOAN_NS_CHITIET_DONVI = "Chi tiết đơn vị";
        public const string DU_TOAN_NS_CHI_TONGHOP_DONVI = "Tổng hợp đơn vị";
        public const string DU_TOAN_NS_CHI_MUCLUC_DONVI = "Tổng hợp mục lục dọc - đơn vị ngang";
    }

    public class KieuGiay
    {
        public const string BIEU_TRINH_KY = "Biểu trình ký";
        public const string PHU_LUC = "Phụ lục";
    }

    public struct BHXHMLNSChiToi
    {
        public const string DuToanChiToi = "M";
        public const string DuToanChiToiKPQL = "LNS";
        public const string DuToanChiToiKCBQY = "LNS";
    }
    public class LoaiBHXHMLNS
    {
        public const string SM = "SM";
        public const string STM = "STM";
        public const string SNG = "SNG";
        public const string STNG = "STNG";
        public const string SL = "SL";
        public const string SK = "SK";
        public const string STTM = "STTM";
        public const string STNG1 = "STNG1";
        public const string STNG2 = "STNG2";
        public const string STNG3 = "STNG3";
    }

    public enum IKhoi
    {
        KHOIALL = 0,
        KHOIDUTOAN = 1,
        KHOIHACHTOAN = 2
    }

    public class MaLoaiChiBHXH
    {
        public const string SMABHXH = "01";
        public const string SMAKPQL = "02";
        public const string SMAKCBQYDV = "03";
        public const string SMAKCBTS = "04";
        public const string SMAHSSVNLD = "07";
        public const string SMAKCBBHYT = "05";
        public const string SMAMSTTBYT = "06";
        public const string SMABHTN = "08";
    }
    public class LNSValue
    {
        public const string LNS_1 = "1";
        public const string LNS_2 = "2";
        public const string LNS_3 = "3";
        public const string LNS_101 = "101";
        public const string LNS_1010000 = "1010000";
        public const string LNS_1010100 = "1010100";
        public const string LNS_1010001 = "1010001";
        public const string LNS_1010002 = "1010002";
        public const string LNS_1010200 = "1010200";
        public const string LNS_1010300 = "1010300";
        public const string LNS_1010400 = "1010400";
        public const string LNS_1040100 = "1040100";
        public const string LNS_1040200 = "1040200";
        public const string LNS_1040300 = "1040300";
        public const string LNS_9050001 = "9050001";
        public const string LNS_9050002 = "9050002";
        public const string LNS_9010006 = "9010006";
        public const string LNS_9010007 = "9010007";
        public const string LNS_9010003 = "9010003";
        public const string LNS_9040001 = "9040001";
        public const string LNS_9040002 = "9040002";
        public const string LNS_9010001_9010002 = "9010001,9010002";
        //public const string LNS_9010004_9010005 = "9010004,9010005"; // comment change 10/01/2024
        public const string LNS_9010004_9010005 = "9010004";
        public const string LNS_9050001_9050002 = "905,9050001,9050002";
        //public const string LNS_9010006_9010007 = "9010006,9010007";// comment change 10/01/2024
        public const string LNS_9010006_9010007 = "9010006";
        public const string LNS_9010008 = "9010008";
        public const string LNS_9010009 = "9010009";
        public const string LNS_9010010 = "9010010";
        public const string LNS_901 = "901";
        public const string LNS_905 = "905";
        public const string LNS_9 = "9";
        public const string LNS_901_9010001_9010002 = "901,9010001,9010002";
        public const string LNS_9010001 = "9010001";
        public const string LNS_9010002 = "9010002";
        public const string LNS_9010004 = "9010004";
        public const string LNS_9_901 = "9,901";
    }

    public class DonViTinh
    {
        public const string NGHIN_DONG = "Nghìn đồng";
        public const string DONG = "Đồng";
        public const string NGHIN_DONG_VALUE = "1000";
        public const string DONG_VALUE = "1";
        public const string TRIEU_DONG = "Triệu đồng";
        public const string TRIEU_VALUE = "1000000";
        public const string TY_DONG = "Tỷ đồng";
        public const string TY_VALUE = "1000000000";
    }

    public class LoaiBaoCaoUpload
    {
        public const string TONG_HOP = "Tổng hợp";
        public const int TONG_HOP_VALUE = 0;
        public const string B_QUAN_LY = "Theo B quản lý";
        public const int B_QUAN_LY_VALUE = 1;
    }

    public class LoaiKinhPhi
    {
        public const int TAT_CA_VALUE = 0;
        public const int QUOC_PHONG_VALUE = 1;
        public const int NHA_NUOC_VALUE = 2;
        public const int DAC_BIET_VALUE = 3;
        public const int KHAC_VALUE = 4;
        public const string TAT_CA_DISPLAY = "Tất cả";
        public const string QUOC_PHONG_DISPLAY = "Ngân sách quốc phòng";
        public const string NHA_NUOC_DISPLAY = "Ngân sách Nhà nước";
        public const string DAC_BIET_DISPLAY = "Ngân sách Đặc biệt";
        public const string KHAC_DISPLAY = "Ngân sách khác";

        public static string GetName(int type)
        {
            switch (type)
            {
                case QUOC_PHONG_VALUE:
                    return QUOC_PHONG_DISPLAY;
                case NHA_NUOC_VALUE:
                    return NHA_NUOC_DISPLAY;
                case DAC_BIET_VALUE:
                    return DAC_BIET_DISPLAY;
                case KHAC_VALUE:
                    return KHAC_DISPLAY;
                default:
                    return string.Empty;
            }
        }
    }

    public class LoaiGiay
    {
        public const string MACDINH = "Mặc định";
        public const string DOC = "A4 dọc";
        public const string NGANG = "A4 ngang";
        public const string NGANG_A3 = "A3 ngang";
    }

    public enum ColumnType
    {
        Combobox,
        Checkbox,
        ReferencePopup,
        DateTime
    }

    public enum GenericDialogType
    {
        Donvi,
        PhongBan
    }

    public class IguiNhanStatus
    {
        public const int NORMAL = 0;
        public const int GUINHAN = 2;
    }

    public class ItrangThaiStatus
    {
        public const int OFF = 0;
        public const int ON = 1;
    }

    public class NguonNganSach
    {
        public const int NSQP = 2;
        public const int NSK = 1;
        public const string TEN_NSQQ = "Ngân sách 104";
        public const string TEN_NSK = "Ngân sách khác";
    }

    public class LoaiNganhThamDinh
    {
        public const int CTCTCDN = 1;
        public const int INOI_BO = 1;
        public const int CTNTD = 2;
        public const int ITOAN_QUAN = 2;
        public const string TEN_CTCTCDN = "Chứng từ CTC đề nghị";
        public const string TEN_CTNTD = "Chứng từ ngành thẩm định";
        public const string TOAN_QUAN = "Toàn quân";
        public const string NOI_BO = "Nội bộ";

        public static string GetNameLoai(int loai)
        {
            if (loai == CTCTCDN)
            {
                return TEN_CTCTCDN;
            }
            else if (loai == CTNTD)
            {
                return TEN_CTNTD;
            }
            return string.Empty;
        }
    }

    public enum ServiceType
    {
        SoPhanBoHienTai,
        NhanPhanBo
    }

    public enum TypeExecute
    {
        Insert = 1,
        Update = 2,
        Delete = 3,
        Adjust = 4
    }

    public enum NguonVonStatus
    {
        ChuaSuDung = 0,
        DangSuDung = 1,
        DaSuDung = 2
    }
    public struct TypeExecuteName
    {
        public const string INSERT = "Thêm mới";
        public const string UPDATE = "Cập nhật";
        public const string ADJUST = "Điều chỉnh";
        public const string DELETE = "Xóa";
    }

    public enum ReportEstimateSettlement
    {
        Agency = 1,
        AgencyMonth = 2,
        AgencyQuater = 3,
        AgencySummary = 4,
        AgencySummaryMonth = 5,
        Summary = 6
    }

    public enum ReportArmy
    {
        AgencyDetail = 0,
        Summary = 1,
        SummaryJurisprudence = 2,
        Army = 3,
        SummaryAgency = 4
    }

    public enum ReportYearSettlement
    {

    }

    public struct TransactionStatus
    {
        public const bool Success = true;
        public const bool Error = false;
    }

    public enum VoucherListLNS
    {
        All = 0,
        ESTIMATE_SETTLEMENT = 1,
        VOUCHER_LIST = 2
    }

    public class DanhMucChuKy
    {
        public const string CHUC_DANH = "DM_CHU_KY_CHUC_DANH";
        public const string TEN = "DM_CHU_KY_TEN";
        public const string TIEU_DE_1 = "112";
        public const string TIEU_DE_2 = "113";
        public const string CHU_KY_GROUP = "DM_ChuKy_Group";
        public const string DV_QUANLY = "DV_QUANLY";
        public const string DM_CAUHINH = "DM_CauHinh";
        public const string NHOM_CHUC_DANH = "DM_NHOM_CHU_KY_CHUC_DANH";
        public const string NHOM_TEN = "DM_NHOM_CHU_KY_TEN";
    }

    public enum SummaryLNSReportType
    {
        AgencyDetail = 0,
        AgencySummary = 1,
        AgencySummaryRank = 2,
        AgencySummaryDetail = 3,
        Type = 4,
        AgencySummaryBlock = 5,
    }

    public enum SummaryReportDataType
    {
        Summary = 0,
        SelfPay = 1,
        Artifact = 2,
    }

    public enum SummaryYearReportType
    {
        SummaryLNS = 0,
        SummaryAgency = 1,
        AgencyDetail = 2
    }

    public enum SummaryYearPrintType
    {
        Branch = 0,
        SubSector = 1
    }

    public enum SummaryAgencyReportType
    {
        A4 = 0,
        A3 = 1,
        EstimateSettlement = 2
    }

    public enum SummaryPrintType
    {
        Detail = 0,
        Summary = 1
    }

    public class ImageConst
    {
        public const string IMAGE_DIRECTORY = @"C:\qlns-prj\avatar";
        public const string IMAGE_TYPE = "Images (*.BMP;*.JPG;*.GIF,*.PNG,*.TIFF)|*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF|" + "All files (*.*)|*.*";
    }

    public class LoaiHopDong
    {
        public const string GIAO_VIEC = "Hợp đồng giao việc";
        public const string KINH_TE = "Hợp đồng kinh tế";
    }

    public enum SummaryVoucherListReportType
    {
        SummaryMLNS = 0,
        SummaryAgency = 1,
        AgencyDetail = 2
    }

    public enum LoaiKeHoachNam
    {
        KeHoachVonNamDuocDuyet = 1,
        KeHoachVonNam = 2,
        TongHopKeHoachVonNam = 3
    }

    public enum ThanhToanType
    {
        THANH_TOAN_DUOC_DUYET = 2,
        THANH_TOAN_TRONG_CHI_TIEU = 1
    }

    public enum ConnectionType
    {
        LocalDb = 0,
        SqlServer = 1
    }

    public enum ScheduleType
    {
        Day = 1,
        Week = 2,
        Month = 3
    }

    public enum DayType
    {
        Daily = 1,
        Hourly = 2
    }

    public enum DMCauHinh
    {
        BACKUP_001 = 1,
        BACKUP_002 = 2,
        BACKUP_003 = 3,
        BACKUP_004 = 4,
        BACKUP_005 = 5,
        BACKUP_006 = 6,
        BACKUP_007 = 7,
        BACKUP_008 = 8,
        BACKUP_009 = 9,
        BACKUP_010 = 10,
    }

    public enum QueryToolSelection
    {
        SqlQuery = 0,
        Result = 1,
        Notification = 2
    }

    public enum ImportTabIndex
    {
        DATA_INDEX = -1,
        Data = 0,
        MLNS = 1,
        DataDetail = 2,
        Aggregate = 3,
        Explain = 4
    }

    public enum LoaiThongTri
    {
        THONG_TRI_THANH_TOAN = 0,
        THONG_TRI_QUYET_TOAN = 1
    }

    public enum LoaiThongTriThu
    {
        TongHop,
        NguoiLaoDong,
        NguoiSuDungLaoDong,
        TongHopChung,
        TongHopChungNLD,
        TongHopChungNSD
    }

    public class OPEN_FROM_PHEDUYETTHANHTOAN
    {
        public const int PHEDUYETTHANHTOAN = 1;
        public const int THONGTRI = 0;
        public const int THONGTRIQUYETTOAN = 2;
    }

    public enum AgencyType
    {
        LEVEL0 = 0,
        LEVEL1 = 1,
        LEVEL2 = 2
    }

    public enum YearBubgetType
    {
        LastYear = 1,
        Currentyear = 2,
    }

    public struct MenuItemContants
    {
        public static string GROUP_FUNCTION = "CHỨC NĂNG";
        public static string GROUP_PROJECT_MANAGER = "QUẢN LÝ DỰ ÁN";
        public static string GROUP_CAPITAL_PLAN_OF_YEAR = "KẾ HOẠCH VỐN NĂM";
        public static string GROUP_CAPITAL_PLAN = "KẾ HOẠCH VỐN ỨNG";

        public static string GROUP_MID_TERM_PLAN = "Kế hoạch trung hạn";

        public static string GROUP_PROJECT_MANAGEMENT = "Quản lý dự án";


        public static string GROUP_ALLOCATION = "CẤP PHÁT";
        public static string GROUP_ANNUAL_SETTLEMENT = "QUYẾT TOÁN NIÊN ĐỘ";
        public static string GROUP_FINISH_SETTLEMENT = "Quyết toán hoàn thành";
        public static string GROUP_REPORT = "BÁO CÁO - THỐNG KÊ";
        public static string GROUP_TRANSFER_FROM_FOXPRO = "FOXPRO";
        public static string GROUP_TRANSFER_FROM_KTDT = "KTDT";

        public static string GROUP_FOREX_IMPORT_CONTRACT = "QUẢN LÝ HỢP ĐỒNG NHẬP KHẨU";
        public static string GROUP_FOREX_MUASAM_NGOAITHUONG = "MUA SẮM NGOẠI THƯƠNG";
        public static string GROUP_FOREX_MUASAM_TRONGNUOC = "MUA SẮM TRONG NƯỚC";
        public static string GROUP_FOREX_CHUANBI_DAUTU = "CHUẨN BỊ ĐẦU TƯ";
        public static string GROUP_FOREX_PHANCHI_TRONGNUOC = "PHẦN CHI TRONG NƯỚC";
        public static string GROUP_FOREX_PHANCHI_NGOAITHUONG = "PHẦN CHI NGOẠI THƯƠNG";
        public static string GROUP_FOREX_PROJECT_MANAGEMENT = "QUẢN LÝ DỰ ÁN";
        public static string GROUP_FOREX_PREPARE_TO_INVEST = "CHUẨN BỊ ĐẦU TƯ";
        public static string GROUP_FOREX_QUYETTOAN_NIENDO = "QUYẾT TOÁN NIÊN ĐỘ";
        public static string GROUP_FOREX_QUYETTOAN_DUAN_HOANTHANH = "QUYẾT TOÁN DỰ ÁN HOÀN THÀNH";
        public static string GROUP_FOREX_QUYETTOAN_BAOCAO = "BÁO CÁO";
        public static string GROUP_FOREX_ASSET = "TÀI SẢN";
        public static string GROUP_FOREX_PROJECT = "DỰ ÁN";
        public static string GROUP_FOREX_CHUYENDULIEU_QUYETTOAN = "CHUYỂN DỮ LIỆU QUYẾT TOÁN";
        public static string GROUP_FOREX_CO_HINH_THANH_GOITHAU = "CÓ HÌNH THÀNH GÓI THẦU";
        public static string GROUP_FOREX_KHONG_HINH_THANH_GOITHAU = "KHÔNG HÌNH THÀNH GÓI THẦU";
        public static string GROUP_FOREX_CHI_PHI_KHAC = "CHI PHÍ KHÁC";
        public static string GROUP_FOREX_CHI_PHI_KHAC_QUYET_DINH = "Quyết định chi phí khác";

        public static string GROUP_QTT_THU = "QUYẾT TOÁN THU";
        public static string GROUP_THU = "PHẦN THU";
        public static string GROUP_CHI = "PHẦN CHI";
        public static string GROUP_QT_CHI_QUY = "QUYẾT TOÁN CHI QUÝ";
        public static string GROUP_QT_CHI_NAM = "QUYẾT TOÁN CHI NĂM";
        public static string GROUP_THAM_DINH_QT = "PHÊ DUYỆT QUYẾT TOÁN";
    }

    public enum DialogType
    {
        LoadDonViOfDanhMucNganh,
        LoadMLNSOfSktMucLuc,
        LoadMLNSOfNsDonVi,
        LoadDMChuyenNganhOfDonVi,
        LoadMLNSOfBHXH,
        LoadMLNSOfMLQTNam
    }

    public enum MLNSFiled
    {
        LNS = 0,
        L = 1,
        K = 2,
        M = 3,
        TM = 4,
        TTM = 5,
        NG = 6,
        TNG = 7,
        TNG1 = 8,
        TNG2 = 9,
        TNG3 = 10
    }

    public enum SMLNSFiled
    {
        SLns = 0,
        SL = 1,
        SK = 2,
        SM = 3,
        STm = 4,
        STtm = 5,
        SNg = 6,
        STng = 7,
        STng1 = 8,
        STng2 = 9,
        STng3 = 10
    }

    public class CachTinhLuong
    {
        public const string CACH0 = "CACH0";
        public const string CACH1 = "CACH1";
        public const string CACH2 = "CACH2";
        public const string CACH5 = "CACH5";
        public const string NOTE = "bảng lương đã chỉnh sửa";
        public const string CACH3 = "CACH3";
        public const string CACH4 = "CACH4";
        public const string CACH0_CACH5 = "CACH0,CACH5";
    }

    public class PhuCap
    {
        public const string THUE_TNCN = "THUE_TNCN";
        public const string TENNGANHANG = "TENNGANHANG";
        public const string TIENTAUXE_TT = "TIENTAUXE_TT";
        public const string TIENANDUONG_TT = "TIENANDUONG_TT";
        public const string TIENCTLH_TT = "TIENCTLH_TT";
        public const string THANG_TCVIECLAM = "THANG_TCVIECLAM";
        public const string Huong_ThangTNN = "Huong_ThangTNN";
        public const string PCTNVK_HS = "PCTNVK_HS";
        public const string TT = "_TT";
        public const string HSBL_HS = "HSBL_HS";
        public const string LCS = "LCS";
        public const string PCTN_HS = "PCTN_HS";
        public const string PCCOV_HS = "PCCOV_HS";
        public const string GTNN = "GTNN";
        public const string GTPT_DG = "GTPT_DG";
        public const string TA_DG = "TA_DG";
        public const string THUETNCN_TT_CONGTHUC = "XAUTO";
        public const string THUETNCN_TT = "THUETNCN_TT";
        public const string LUONGTHUE_TT = "LUONGTHUE_TT";
        public const string BHXHDV_HS = "BHXHDV_HS";
        public const string BHXHCN_HS = "BHXHCN_HS";
        public const string BHYTDV_HS = "BHYTDV_HS";
        public const string BHYTCN_HS = "BHYTCN_HS";
        public const string BHTNDV_HS = "BHTNDV_HS";
        public const string BHTNCN_HS = "BHTNCN_HS";
        public const string LHT_HS = "LHT_HS";
        public const string LHT_HS_CU = "LHT_HS_CU";
        public const string PCCV_HS = "PCCV_HS";
        public const string NTN = "NTN";
        public const string OMDAU_NGAY = "OMDAU_NGAY";
        public const string THAISAN_NGAY = "THAISAN_NGAY";
        public const string HUUTRI_NGAY = "HUUTRI_NGAY";
        public const string TNLDBNN_NGAY = "TNLDBNN_NGAY";
        public const string TUTUAT_NGAY = "TUTUAT_NGAY";
        public const string OMDAU_TT = "OMDAU_TT";
        public const string THAISAN_TT = "THAISAN_TT";
        public const string HUUTRI_TT = "HUUTRI_TT";
        public const string TNLDBNN_TT = "TNLDBNN_TT";
        public const string TUTUAT_TT = "TUTUAT_TT";
        public const string PCTN_TT = "PCTN_TT";
        public const string PCKV_TT = "PCKV_TT";
        public const string PCCV_TT = "PCCV_TT";
        public const string PCTHUHUT_TT = "PCTHUHUT_TT";
        public const string PCTRA_SUM = "PCTRA_SUM";
        public const string PCCOV_TT = "PCCOV_TT";
        public const string PCDACTHU_SUM = "PCDACTHU_SUM";
        public const string PCKHAC_SUM = "PCKHAC_SUM";
        public const string BHCN_TT = "BHCN_TT";
        public const string TA_TT = "TA_TT";
        public const string GTKHAC_TT = "GTKHAC_TT";
        public const string TM = "TM";
        public const string LHT_TT = "LHT_TT";
        public const string PCCT_TT = "PCCT_TT";
        public const string THUONG_TT = "THUONG_TT";
        public const string THUNHAPKHAC_TT = "THUNHAPKHAC_TT";
        public const string GTPT_SN = "GTPT_SN";
        public const string GIAMTHUE_TT = "GIAMTHUE_TT";
        public const string THUEDANOP_TT = "THUEDANOP_TT";
        public const string THANHTIEN = "THANHTIEN";
        public const string THANHTIEN_NH = "THANHTIEN_NH";
        public const string HSBL_TT = "HSBL_TT";
        public const string PCTNVK_TT = "PCTNVK_TT";
        public const string PCTHD_TT = "PCTHD_TT";
        public const string PCTHD_HS = "PCTHD_HS";
        public const string BHXH_TT = "BHXH_TT";
        public const string BHYT_TT = "BHYT_TT";
        public const string BHTN_TT = "BHTN_TT";
        public const string TRICHLUONG_TT = "TRICHLUONG_TT";
        public const string TRICHLUONG_SN = "TRICHLUONG_SN";
        public const string TTL = "TTL";
        public const string NamTN = "NamTn"; // them de in bao cao
        public const string TTL_LHT = "TTL_LHT";
        public const string TTL_PCCV = "TTL_PCCV";
        public const string TTL_PCCOV = "TTL_PCCOV";
        public const string LHT_TT_CU = "LHT_TT_CU";
        public const string PCCV_TT_CU = "PCCV_TT_CU";
        public const string LUONGTHANG_SUM = "LUONGTHANG_SUM";
        public const string PHAITRU_SUM = "PHAITRU_SUM";
        public const string BHXHCN_TT = "BHXHCN_TT";
        public const string BHYTCN_TT = "BHYTCN_TT";
        public const string BHXHDV_TT = "BHXHDV_TT";
        public const string BHYTDV_TT = "BHYTDV_TT";
        public const string BHTNDV_TT = "BHTNDV_TT";
        public const string LPC_HS = "LPC_HS";
        public const string PCTRA_HS = "PCTRA_HS";
        public const string PCDACTHU_HS = "PCDACTHU_HS";
        public const string PCDACTHU_TT = "PCDACTHU_TT";
        public const string GIAMTRU = "GIAMTRU";
        public const string BAOHIEM = "BAOHIEM";
        public const string TRUYLINH = "TRUYLINH";
        public const string BHTNCN_TT = "BHTNCN_TT";
        public const string PCCV_TIEN = "PCCV_TIEN";
        public const string Nop_BHTN = "Nop_BHTN";
        public const string Huong_PCCOV = "Huong_PCCOV";
        public const string THUEDANOP = "THUEDANOP_TT";
        public const string THUONG = "THUONG_TT";
        public const string THUNHAPKHAC = "THUNHAPKHAC_TT";
        public const string TRICHLUONG = "TRICHLUONG_TT";
        public const string GIAMTHUE = "GIAMTHUE_TT";
        public const string THUETAINGUON_TT = "THUETAINGUON_TT";
        public const string LUONGTHUE_DIEUCHINH = "LUONGTHUE_DIEUCHINH";
        public const string THUEDANOP_DIEUCHINH = "THUEDANOP_DIEUCHINH";
        public const string GiAMTRU_NGUOINOP = "GTNN";
        public const string GiAMTRU_NGUOIPHUTHUOC = "GTPT_DG";
        public const string PCKV_HS = "PCKV_HS";
        public const string GTPT_TT = "GTPT_TT";
        public const string CONGCHUAN_SN = "CONGCHUAN_SN";
        public const string TILE_HUONG = "TILE_HUONG";
        public const string TRICHLUONG_TIEN = "TRICHLUONG_TIEN";
        public const string TRUYLINHKHAC_SUM = "TRUYLINHKHAC_SUM";
        public const string TA_BB_DG = "TA_BB_DG";
        public const string TA_TRUCTRAI_DG = "TA_TRUCTRAI_DG";
        public const string TA_DOCHAI_DG = "TA_DOCHAI_DG";
        public const string TA_OM_DG = "TA_OM_DG";
        public const string TA_TRUCQY_DG1 = "TA_TRUCQY_DG1";
        public const string TA_TRUCQY_DG2 = "TA_TRUCQY_DG2";
        public const string TA_TRUCQY_DG3 = "TA_TRUCQY_DG3";
        public const string TA_TRUCQY_DG4 = "TA_TRUCQY_DG4";
        public const string TA_TRUCQY = "TA_TRUCQY";
        public const string PCKIE_TT = "PCKIE_TT";
        public const string PCKIE_HS = "PCKIE_HS";
        public const string THUETNCN_NAM = "THUETNCN_NAM";
        public const string PCTEMTHU_TT = "PCTEMTHU_TT";
        public const string TILE_HUONGNN = "TILE_HUONGNN";
        public const string TILE_HUONGTGTG = "TILE_HUONGTGTG";
        public const string PCNU_HS = "PCNU_HS";
        public const string PCANQP_HS = "PCANQP_HS";
        public const string PCTHUHUT_HS_CU = "PCTHUHUT_HS_CU";
        public const string PCTHUHUT_HS = "PCTHUHUT_HS";
        public const string PCCU_HS = "PCCU_HS";
        public const string TA_TONG = "TA_TONG";
        public const string TCVIECLAM_TT = "TCVIECLAM_TT";
        public const string THANG_TCXN = "THANG_TCXN";
        public const string PC8_HS = "PC8_HS";
        public const string PC_TTX = "TIENTAUXE_TT";
        public const string PC_TAD = "TIENANDUONG_TT";
        public const string PC_CTLH = "TIENCTLH_TT";
        public const string CHIKHAC = "CHIKHAC";
        public const string PHAITRUKHAC_SUM = "PHAITRUKHAC_SUM";
        public const string SONGAYHUONG = "SONGAYHUONG";
        public const string TLCB_TT = "TLCB_TT";
        public const string TNLCB_TT = "TNLCB_TT";
        public const string TLBLCB_TT = "TLBLCB_TT";
        public const string TLCV_CD_TT = "TLCV_CD_TT";
        public const string TNLCV_CD_TT = "TNLCV_CD_TT";
        public const string TLBLCV_CD_TT = "TLBLCV_CD_TT";
        public const string TRUYTHU_SN = "TRUYTHU_SN";
        public const string LCB_TT = "LCB_TT";
        public const string NLCB_TT = "NLCB_TT";
        public const string BHCN = "BHCN";

        //Lương mới
        public const string LCB_SUM = "LCB_SUM";
        public const string LCBHT_TT = "LCBHT_TT";
        public const string NLCBHT_TT = "NLCBHT_TT";
        public const string LBLCBHT_TT = "LBLCBHT_TT";
        public const string LCV_SUM = "LCV_SUM";
        public const string LCVHT_TT = "LCVHT_TT";
        public const string NLCVHT_TT = "NLCVHT_TT";
        public const string LBLCVHT_TT = "LBLCVHT_TT";
        public const string LCD_SUM = "LCD_SUM";
        public const string LCDHT_TT = "LCDHT_TT";
        public const string NLCDHT_TT = "NLCDHT_TT";
        public const string LBLCDHT_TT = "LBLCDHT_TT";
        public const string LCVCD_SUM = "LCVCD_SUM";
        public const string LCVCDHT_TT = "LCVCDHT_TT";
        public const string NLCVCDHT_TT = "NLCVCDHT_TT";
        public const string LBLCVCDHT_TT = "LBLCVCDHT_TT";
    }

    public class PhuCapNq104
    {
        public const string LCBHT_TT = "LCBHT_TT";
        public const string LCB_SUM = "LCB_SUM";
        public const string NLCBHT_TT = "NLCBHT_TT";
        public const string LBLCBHT_TT = "LBLCBHT_TT";
        public const string LCVCD_SUM = "LCVCD_SUM";
        public const string LCVCDHT_TT = "LCVCDHT_TT";
        public const string NLCVCDHT_TT = "NLCVCDHT_TT";
        public const string LBLCVCDHT_TT = "LBLCVCDHT_TT";
        public const string PCKIE_TT = "PCKIE_TT";
        public const string PCKV_TT = "PCKV_TT";
        public const string PCTN_TT = "PCTN_TT";
        public const string PCTNCV_TT = "PCTNCV_TT";
        public const string PCTNVK_TT = "PCTNVK_TT";
        public const string PCCTKK_TT = "PCCTKK_TT";
        public const string PCANQP_TT = "PCANQP_TT";
        public const string PCDACTHU_SUM = "PCDACTHU_SUM";
        public const string TA_DG = "TA_DG";

        //public const string THUE_TNCN = "THUE_TNCN";
        //public const string TENNGANHANG = "TENNGANHANG";
        //public const string TIENTAUXE_TT = "TIENTAUXE_TT";
        //public const string TIENANDUONG_TT = "TIENANDUONG_TT";
        //public const string TIENCTLH_TT = "TIENCTLH_TT";
        //public const string THANG_TCVIECLAM = "THANG_TCVIECLAM";
        //public const string Huong_ThangTNN = "Huong_ThangTNN";
        //public const string PCTNVK_HS = "PCTNVK_HS";
        //public const string TT = "_TT";
        //public const string HSBL_HS = "HSBL_HS";
        //public const string LCS = "LCS";
        //public const string PCTN_HS = "PCTN_HS";
        //public const string PCCOV_HS = "PCCOV_HS";
        //public const string GTNN = "GTNN";
        //public const string GTPT_DG = "GTPT_DG";
        //public const string TA_DG = "TA_DG";
        //public const string THUETNCN_TT_CONGTHUC = "XAUTO";
        //public const string THUETNCN_TT = "THUETNCN_TT";
        //public const string LUONGTHUE_TT = "LUONGTHUE_TT";
        //public const string BHXHDV_HS = "BHXHDV_HS";
        //public const string BHXHCN_HS = "BHXHCN_HS";
        //public const string BHYTDV_HS = "BHYTDV_HS";
        //public const string BHYTCN_HS = "BHYTCN_HS";
        //public const string BHTNDV_HS = "BHTNDV_HS";
        //public const string BHTNCN_HS = "BHTNCN_HS";
        //public const string LHT_HS = "LHT_HS";
        //public const string LHT_HS_CU = "LHT_HS_CU";
        //public const string PCCV_HS = "PCCV_HS";
        //public const string NTN = "NTN";
        //public const string OMDAU_NGAY = "OMDAU_NGAY";
        //public const string THAISAN_NGAY = "THAISAN_NGAY";
        //public const string HUUTRI_NGAY = "HUUTRI_NGAY";
        //public const string TNLDBNN_NGAY = "TNLDBNN_NGAY";
        //public const string TUTUAT_NGAY = "TUTUAT_NGAY";
        //public const string OMDAU_TT = "OMDAU_TT";
        //public const string THAISAN_TT = "THAISAN_TT";
        //public const string HUUTRI_TT = "HUUTRI_TT";
        //public const string TNLDBNN_TT = "TNLDBNN_TT";
        //public const string TUTUAT_TT = "TUTUAT_TT";
        //public const string PCTN_TT = "PCTN_TT";
        //public const string PCKV_TT = "PCKV_TT";
        //public const string PCCV_TT = "PCCV_TT";
        //public const string PCTHUHUT_TT = "PCTHUHUT_TT";
        //public const string PCTRA_SUM = "PCTRA_SUM";
        //public const string PCCOV_TT = "PCCOV_TT";
        //public const string PCDACTHU_SUM = "PCDACTHU_SUM";
        //public const string PCKHAC_SUM = "PCKHAC_SUM";
        //public const string BHCN_TT = "BHCN_TT";
        //public const string TA_TT = "TA_TT";
        //public const string GTKHAC_TT = "GTKHAC_TT";
        //public const string TM = "TM";
        //public const string LHT_TT = "LHT_TT";
        //public const string PCCT_TT = "PCCT_TT";
        //public const string THUONG_TT = "THUONG_TT";
        //public const string THUNHAPKHAC_TT = "THUNHAPKHAC_TT";
        //public const string GTPT_SN = "GTPT_SN";
        //public const string GIAMTHUE_TT = "GIAMTHUE_TT";
        //public const string THUEDANOP_TT = "THUEDANOP_TT";
        //public const string THANHTIEN = "THANHTIEN";
        //public const string THANHTIEN_NH = "THANHTIEN_NH";
        //public const string HSBL_TT = "HSBL_TT";
        //public const string PCTNVK_TT = "PCTNVK_TT";
        //public const string PCTHD_TT = "PCTHD_TT";
        //public const string PCTHD_HS = "PCTHD_HS";
        //public const string BHXH_TT = "BHXH_TT";
        //public const string BHYT_TT = "BHYT_TT";
        //public const string BHTN_TT = "BHTN_TT";
        //public const string TRICHLUONG_TT = "TRICHLUONG_TT";
        //public const string TRICHLUONG_SN = "TRICHLUONG_SN";
        //public const string TTL = "TTL";
        //public const string NamTN = "NamTn"; // them de in bao cao
        //public const string TTL_LHT = "TTL_LHT";
        //public const string TTL_PCCV = "TTL_PCCV";
        //public const string TTL_PCCOV = "TTL_PCCOV";
        //public const string LHT_TT_CU = "LHT_TT_CU";
        //public const string PCCV_TT_CU = "PCCV_TT_CU";
        //public const string LUONGTHANG_SUM = "LUONGTHANG_SUM";
        //public const string PHAITRU_SUM = "PHAITRU_SUM";
        //public const string BHXHCN_TT = "BHXHCN_TT";
        //public const string BHYTCN_TT = "BHYTCN_TT";
        //public const string BHXHDV_TT = "BHXHDV_TT";
        //public const string BHYTDV_TT = "BHYTDV_TT";
        //public const string BHTNDV_TT = "BHTNDV_TT";
        //public const string LPC_HS = "LPC_HS";
        //public const string PCTRA_HS = "PCTRA_HS";
        //public const string PCDACTHU_HS = "PCDACTHU_HS";
        //public const string PCDACTHU_TT = "PCDACTHU_TT";
        //public const string GIAMTRU = "GIAMTRU";
        //public const string BAOHIEM = "BAOHIEM";
        //public const string TRUYLINH = "TRUYLINH";
        //public const string BHTNCN_TT = "BHTNCN_TT";
        //public const string PCCV_TIEN = "PCCV_TIEN";
        //public const string Nop_BHTN = "Nop_BHTN";
        //public const string Huong_PCCOV = "Huong_PCCOV";
        //public const string THUEDANOP = "THUEDANOP_TT";
        //public const string THUONG = "THUONG_TT";
        //public const string THUNHAPKHAC = "THUNHAPKHAC_TT";
        //public const string TRICHLUONG = "TRICHLUONG_TT";
        //public const string GIAMTHUE = "GIAMTHUE_TT";
        //public const string THUETAINGUON_TT = "THUETAINGUON_TT";
        //public const string LUONGTHUE_DIEUCHINH = "LUONGTHUE_DIEUCHINH";
        //public const string THUEDANOP_DIEUCHINH = "THUEDANOP_DIEUCHINH";
        //public const string GiAMTRU_NGUOINOP = "GTNN";
        //public const string GiAMTRU_NGUOIPHUTHUOC = "GTPT_DG";
        //public const string PCKV_HS = "PCKV_HS";
        //public const string GTPT_TT = "GTPT_TT";
        //public const string CONGCHUAN_SN = "CONGCHUAN_SN";
        //public const string TILE_HUONG = "TILE_HUONG";
        //public const string TRICHLUONG_TIEN = "TRICHLUONG_TIEN";
        //public const string TRUYLINHKHAC_SUM = "TRUYLINHKHAC_SUM";
        //public const string TA_BB_DG = "TA_BB_DG";
        //public const string TA_TRUCTRAI_DG = "TA_TRUCTRAI_DG";
        //public const string TA_DOCHAI_DG = "TA_DOCHAI_DG";
        //public const string TA_OM_DG = "TA_OM_DG";
        //public const string TA_TRUCQY_DG1 = "TA_TRUCQY_DG1";
        //public const string TA_TRUCQY_DG2 = "TA_TRUCQY_DG2";
        //public const string TA_TRUCQY_DG3 = "TA_TRUCQY_DG3";
        //public const string TA_TRUCQY_DG4 = "TA_TRUCQY_DG4";
        //public const string TA_TRUCQY = "TA_TRUCQY";
        //public const string PCKIE_TT = "PCKIE_TT";
        //public const string PCKIE_HS = "PCKIE_HS";
        //public const string THUETNCN_NAM = "THUETNCN_NAM";
        //public const string PCTEMTHU_TT = "PCTEMTHU_TT";
        //public const string TILE_HUONGNN = "TILE_HUONGNN";
        //public const string PCNU_HS = "PCNU_HS";
        //public const string PCANQP_HS = "PCANQP_HS";
        //public const string PCTHUHUT_HS_CU = "PCTHUHUT_HS_CU";
        //public const string PCTHUHUT_HS = "PCTHUHUT_HS";
        //public const string PCCU_HS = "PCCU_HS";
        //public const string TA_TONG = "TA_TONG";
        //public const string TCVIECLAM_TT = "TCVIECLAM_TT";
        //public const string THANG_TCXN = "THANG_TCXN";
        //public const string PC8_HS = "PC8_HS";
        //public const string PC_TTX = "TIENTAUXE_TT";
        //public const string PC_TAD = "TIENANDUONG_TT";
        //public const string PC_CTLH = "TIENCTLH_TT";
        //public const string CHIKHAC = "CHIKHAC";
        //public const string PHAITRUKHAC_SUM = "PHAITRUKHAC_SUM";
        //public const string SONGAYHUONG = "SONGAYHUONG";
        //public const string TLCB_TT = "TLCB_TT";
        //public const string TNLCB_TT = "TNLCB_TT";
        //public const string TLBLCB_TT = "TLBLCB_TT";
        //public const string TLCV_CD_TT = "TLCV_CD_TT";
        //public const string TNLCV_CD_TT = "TNLCV_CD_TT";
        //public const string TLBLCV_CD_TT = "TLBLCV_CD_TT";
    }

    public class LoaiDoiTuong
    {
        public const string SQ = "1";
        public const string QNCN = "2";
        public const string CNQP = "3.1";
        public const string CCQP = "3.2";
        public const string VCQP = "3.3";
        public const string HSQCS = "4";
        public const string HCY = "5";
        public const string CMKTCY = "6";

    }

    public struct HINH_THUC_CHON_NHA_THAU
    {
        public static string DAU_THAU_RONG_RAI = "Đấu thầu rộng rãi";
        public static string DAU_THAU_HAN_CHE = "Đấu thầu hạn chế";
        public static string CHI_DINH_THAU = "Chỉ định thầu";
        public static string CHAO_HANG_CANH_TRANH = "Chào hàng cạnh tranh";
        public static string MUA_SAM_TRUC_TIEP = "Mua sắm trực tiếp";
        public static string TU_THUC_HIEN = "Tự thực hiện";
        public static string TRUONG_HOP_DAC_BIET = "Lựa chọn NT, NĐT trong trường hợp đặc biệt";
        public static string CONG_DONG = "Tham gia thực hiện của cộng đồng";
    }

    public struct PHUONG_THUC_CHON_NHA_THAU
    {
        public static string MOT_GIAI_DOAN_1_HO_SO = "1 giai đoạn, 1 túi hồ sơ";
        public static string MOT_GIAI_DOAN_2_HO_SO = "1 giai đoạn, 2 túi hồ sơ";
        public static string HAI_GIAI_DOAN_1_HO_SO = "2 giai đoạn, 1 túi hồ sơ";
        public static string HAI_GIAI_DOAN_2_HO_SO = "2 giai đoạn, 2 túi hồ sơ";
    }

    public struct LOAI_HOP_DONG
    {
        public static string HOP_DONG_TRON_GOI = "Hợp đồng trọn gói";
        public static string HOP_DONG_CO_DINH = "Hợp đồng theo đơn giá cố định";
        public static string HOP_DONG_DIEU_CHINH = "Hợp đồng theo đơn giá điều chỉnh";
    }

    public class CachNhanLuong
    {
        public const string TAI_DON_VI = "1";
        public const string QUA_TAI_KHOAN = "0";
    }

    public enum BaoCaoLuong
    {
        DSCPL_A4_CU = 1,
        DSCPL_A4 = 2,
        DSCPL_A3 = 3,
        GTCTTL = 4,
        GTPCCV = 5,
        GTCTPC_TX = 40,
        GTCTPC_NV = 41,
        GTCTPC_TNK = 42,
        GTCTPC_GTK = 43,
        TTNCN = 7,
        TTNCN_CB = 8,
        QTTTNCN = 9,
        BTLTL = 10,
        BTLBH = 21,
        DSCPPC = 11,
        GTCTPC_HQS = 12,
        DSCTCN = 13,
        DSCTCN_CT = 14,
        THPCCV = 15,
        THLPC = 16,
        THLPC_DV = 17,
        THLPC_N_DV = 18,
        BTHLPC = 19,
        GTPCTN = 191,
        THQSQT = 20,
        GTCTPCTNVKTHD = 22,
        INKIEM = 23,
        DCQSNKH = 24,
        CTQSRQ = 25,
        CTQSNH = 26,
        THLNKH = 27,
        CTLNKH = 28,
        QTQS = 29,
        BDQHKE = 30,
        LKHMLNS = 31,
        TLCCDSQ = 32,
        TLCCDQNCN = 33,
        TLBCTA = 34,
        BLTBP = 35,
        BTLTT = 36,
        GTCTRQXN = 37,
        BTLTLNN = 38,
        CTQSTG = 39,
        BTHLPCBP = 120
    }

    public struct Gender
    {
        public static string NAM = "Nam";
        public static string NU = "Nữ";
    }

    public struct ExportPrefix
    {
        public const string PATH_TL_LUONG = "Luong/";
        public const string PATH_TL_LUONG_NEW = "LuongNew/";
        public const string PATH_TL_SKT = "NganSach/SKT";
        public const string PATH_TL_NTD = "NganSach/NganhThamDinh";
        public const string PATH_TL_QUYETTOAN = "NganSach/QuyetToan";
        public const string PATH_TL_CP = "NganSach/CapPhat";
        public const string PATH_TL_PCNSN = "NganSach/PhanCapNganSachNganh";
        public const string PATH_TL_DT = "NganSach/DuToan";
        public const string PATH_TL_DTDN = "NganSach/DuToanDauNam";
        public const string PATH_TL_CPTT = "VonDauTu/CapPhatThanhToan";
        public const string PATH_TL_TTTT = "VonDauTu/ThongTriThanhToan";
        public const string PATH_TL_THDA = "VonDauTu/ThucHienDuAn";
        public const string PATH_TL_THDT = "VonDauTu/ThucHienDauTu";
        public const string PATH_TL_KTDT = "VonDauTu/KetThucDauTu";
        public const string PATH_TL_KHCQ = "VonDauTu/KeHoachChiQuy";
        public const string PATH_TL_QT = "VonDauTu/QuyetToan";
        public const string PATH_TL_KT = "VonDauTu/KhoiTaoDuAn";
        public const string PATH_TL_KHTH = "VonDauTu/KeHoachTrungHan";
        public const string PATH_TL_KHVN = "VonDauTu/KeHoachVonNam";
        public const string PATH_TL_TTDUAN = "VonDauTu/ThongTinDuAn";
        public const string PATH_TL_QLDUAN = "VonDauTu/QLDuAn";
        public const string PATH_TL_TTGT = "VonDauTu/ThongTinGoiThau";
        public const string PATH_TL_TTHD = "VonDauTu/ThongTinHopDong";
        public const string PATH_TL_NGOAIHOI = "NgoaiHoi/";
        public const string PATH_NH_CP_DNTT = "NgoaiHoi/CapPhat";
        public const string PATH_NH_QT = "NgoaiHoi/QuyetToan";
        public const string PATH_NH_THDA = "NgoaiHoi/ThucHienDuAn";
        public const string PATH_NH_DM = "NgoaiHoi/DanhMuc";
        public const string PATH_NH_DUAN = "NgoaiHoi/DuAn";
        public const string PATH_TL_KHVU = "VonDauTu/KeHoachVonUng";
        public const string PATH_NH_KTCP = "NgoaiHoi/KhoiTaoCapPhat";
        public const string PATH_NH_MUASAM = "NgoaiHoi/MuaSam";
        public const string PATH_BH_KHT = "BaoHiem/KHT";
        public const string PATH_BH_QTT = "BaoHiem/QTT";
        public const string PATH_BH_THAMDINHQUYETTOAN = "BaoHiem/THAMDINHQUYETTOAN";
        public const string PATH_BH_QTTM = "BaoHiem/QTTM";
        public const string PATH_BH_KHT_IMPORT_TEMPLATE = "AppData/Template/Xlxs/BaoHiem/KHT";
        public const string PATH_BH_QTT_IMPORT_TEMPLATE = "AppData/Template/Xlxs/BaoHiem/QTT";
        public const string PATH_BH_QTTM_IMPORT_TEMPLATE = "AppData/Template/Xlxs/BaoHiem/QTTM";
        public const string PATH_BH_KHC = "BaoHiem/KHC";
        public const string PATH_BH_KHCK = "BaoHiem/KHCK";
        public const string PATH_BH_NDT = "BaoHiem/NDT";
        public const string PATH_BH_KHTM = "BaoHiem/KHTM";
        public const string PATH_BH_KHTM_IMPORT_TEMPLATE = "AppData/Template/Xlxs/BaoHiem/KHTM";
        public const string PATH_BH_CPTU = "BaoHiem/CPTU";
        public const string PATH_BH_QTCNBHXH = "BaoHiem/QTCNBHXH";
        public const string PATH_BH_QTCQBHXH = "BaoHiem/QTCQBHXH";
        public const string PATH_VDT_PDDA = "VonDauTu/PheDuyetDuAn";
        public const string PATH_BH_DTT = "BaoHiem/DTT";


        public const string PATH_BH_KHC_KPQL = "BaoHiem/KHCKPQL";
        public const string PATH_BH_DT_DTCPBCL = "BaoHiem/DTCPBC";
        public const string PATH_BH_KHC_IMPORT_TEMPLATE = "AppData/Template/Xlxs/BaoHiem/KHC";
        public const string PATH_BH_KHC_QLKP_IMPORT_TEMPLATE = "AppData/Template/Xlxs/BaoHiem/KHCKPQL";

        public const string PATH_BH_DTT_IMPORT_TEMPLATE = "AppData/Template/Xlxs/BaoHiem/DTT";

        public const string PATH_BH_KHC_KCB = "BaoHiem/KHCKCB";
        public const string PATH_BH_KHC_KCB_IMPORT_TEMPLATE = "AppData/Template/Xlxs/BaoHiem/KHCKCB";

        public const string PATH_BH_DT_DCDT = "BaoHiem/DTDCDT";
        public const string PATH_BH_DT_DCDT_IMPORT_TEMPLATE = "AppData/Template/Xlxs/BaoHiem/DTDCDT";

        public const string PATH_BH_DC_DTT = "BaoHiem/DieuChinhDTT";
        public const string PATH_BH_DTTM_BHYT = "BaoHiem/DTTMBHYT";
        public const string PATH_BH_DTTM_BHYT_IMPORT_TEMPLATE = "AppData/Template/Xlxs/BaoHiem/DTTMBHYT";
        public const string PATH_BH_DC_DTT_IMPORT_TEMPLATE = "AppData/Template/Xlxs/BaoHiem/DieuChinhDTT";
        public const string PATH_BH_CPBS = "BaoHiem/CapPhatBoSung";
        public const string PATH_BH_IMPORT_CPBS = "AppData/Template/Xlxs/BaoHiem/CapPhatBoSung";
        public const string PATH_BH_CP = "BaoHiem/CapPhat";
        public const string PATH_BH_CP_IMPORT_TEMPLATE = "AppData/Template/Xlxs/BaoHiem/CapPhat";
        public const string PATH_BH_QTC_QUYKPQL = "BaoHiem/QTCQKPQL";
        public const string PATH_BH_QTC_QUYKPQL_IMPORT_TEMPLATE = "AppData/Template/Xlxs/BaoHiem/QTCQKPQL";
        public const string PATH_BH_QTCQKCB = "BaoHiem/QTCQKCB";
        public const string PATH_BH_QTC_NAMKPQL = "BaoHiem/QTCNKPQL";
        public const string PATH_BH_QTC_NAMKPQL_IMPORT_TEMPLATE = "AppData/Template/Xlxs/BaoHiem/QTCNKPQL";
        public const string PATH_BH_QTC_QUYKPK = "BaoHiem/QTCQKPK";
        public const string PATH_BH_QTC_QUYKPK_IMPORT_TEMPLATE = "AppData/Template/Xlxs/BaoHiem/QTCQKPK";
        public const string PATH_BH_QTCNKCB = "BaoHiem/QTCNKCB";
        public const string PATH_BH_QTC_NAMKPK = "BaoHiem/QTCNKPK";
        public const string PATH_BH_QTC_NKPK_IMPORT_TEMPLATE = "AppData/Template/Xlxs/BaoHiem/QTCNKPK";

        public const string PATH_BH_KHC_K = "BaoHiem/KHCK";
        public const string PATH_BH_KHC_K_IMPORT_TEMPLATE = "AppData/Template/Xlxs/BaoHiem/KHCK";
        public const string PATH_BH_QTC_KP_KCB_BHYT = "BaoHiem/QTCKPKCB";
        public const string PATH_BH_QTC_KP_KCB_BHYT_IMPORT = "AppData/Template/Xlxs/BaoHiem/QTCKPKCB";
        public const string PATH_BH_CPTU_IMPORT_TEMPLATE = "AppData/Template/Xlxs/BaoHiem/CPTU";
        public const string PATH_TL_THUNOP_NGANSACH = "NganSach/ThuNopNganSach";
    }

    public struct ExportFileName
    {
        public const string RPT_TL_LUONG_THANG = "rptLuong_BangLuong_Thang.xlsx";
        public const string RPT_TL_LUONG_THANG_BIEN_PHONG = "rptLuong_BangLuong_Thang_Bien_Phong.xlsx";
        public const string RPT_TL_LUONG_THANG_BIEN_PHONG_1 = "rptLuong_BangLuong_Thang_Bien_Phong_1.xlsx";
        public const string RPT_TL_LUONG_TRUY_LINH = "rptLuong_BangLuong_TruyLinh.xlsx";
        public const string RPT_TL_LUONG_TRUY_LINH_TONG_HOP = "rptLuong_BangLuong_TruyLinh_TongHop.xlsx";
        public const string RPT_TL_LUONG_TRUY_LINH_NHIEU_QD = "rptLuong_BangLuong_TruyLinh_NhieuNgay_QuyetDinh.xlsx";
        public const string RPT_TL_LUONG_TRUY_THU = "rptLuong_BangLuong_TruyThu.xlsx";
        public const string RPT_TL_LUONG_TRUY_LINH_DONG_PHU_CAP = "rptLuong_BangLuong_TruyLinh_Dong.xlsx";
        public const string RPT_TL_LUONG_TRUY_LINH_DONG_PHU_CAP_TONG_HOP = "rptLuong_BangLuong_TruyLinh_Dong_Tong_Hop.xlsx";
        public const string RPT_TL_LUONG_TRUY_THU_DONG_PHU_CAP = "rptLuong_BangLuong_TruyThu_Dong.xlsx";
        public const string RPT_TL_LUONG_TRUY_LINH_DONG_PHU_CAP2 = "rptLuong_BangLuong_TruyLinh_Dong2.xlsx";
        public const string RPT_TL_LUONG_TRUY_LINH_DONG_PHU_CAP2_TONG_HOP = "rptLuong_BangLuong_TruyLinh_Dong2_TongHop.xlsx";
        public const string RPT_TL_LUONG_TRUY_THU_DONG_PHU_CAP2 = "rptLuong_BangLuong_TruyThu_Dong2.xlsx";
        public const string RPT_TL_LUONG_BAO_HIEM = "rptLuong_BangLuong_BaoHiem.xlsx";
        public const string RPT_TL_QUYET_TOAN_TTNCN = "rptLuong_QuyetToan_Nam_ThueTncn.xlsx";
        public const string RPT_TL_DS_TRA_NGAN_HANG = "rptLuong_DanhSach_ChiTra_NganHang.xlsx";
        public const string RPT_TL_BANGKE_TRICHTHUETNCN = "rptLuong_BangKe_TrichThueTNCN.xlsx";
        public const string RPT_TL_BANGKE_TRICHTHUETNCN_A3 = "rptLuong_BangKe_TrichThueTNCN_A3.xlsx";
        public const string RPT_TL_DANHSACH_CHITRA_LUONGCN = "rptLuong_DanhSach_ChiTra_LuongCN.xlsx";
        public const string RPT_TL_DS_CHITRA_LUONGCN_AN_DANH = "rptLuong_DanhSach_ChiTra_AnDanh.xlsx";
        public const string RPT_TL_DS_CHITRA_LUONGCN_AN_DANH_1HANG = "rptLuong_DanhSach_ChiTra_AnDanh_1Hang.xlsx";
        public const string RPT_TL_TONGHOP_LUONG_PHUCAP_DONVI = "rptLuong_TongHop_Luong_PhuCap_DonVi.xlsx";
        public const string RPT_TL_TONGHOP_LUONG_PHUCAP_DONVI_A3 = "rptLuong_TongHop_Luong_PhuCap_DonVi_A3.xlsx";
        public const string RPT_TL_TONGHOP_LUONG_NGACHDONVI = "rptLuong_TongHop_Luong_NgachDonVi.xlsx";
        public const string RPT_TL_TONGHOP_LUONG_NGACHDONVI_A3 = "rptLuong_TongHop_Luong_NgachDonVi_A3.xlsx";
        public const string RPT_TL_QUYETTOAN_QUANSO = "rptLuong_QuyetToan_QuanSo.xlsx";
        public const string RPT_TL_GIAITHICH_CHITIET_PHUCAPTNVKTHD = "rptLuong_GiaiThich_ChiTiet_PhuCapTNVKTHD.xlsx";
        public const string RPT_TL_GIAY_GT_TC = "rptLuong_Giay_GioiThieu_TaiChinh.xlsx";
        public const string RPT_TL_GIAY_GT_TC_A4 = "rptLuong_Giay_GioiThieu_TaiChinh_A4.xlsx";
        public const string RPT_TL_GIAY_GT_TC_A4_KHONG_PC_HUONG = "rptLuong_Giay_GioiThieu_TaiChinh_A4_Khong_Pc_Huong.xlsx";
        public const string RPT_TL_TONG_HOP_LUONG_PHU_CAP_THEO_NGACH = "rptLuong_Tong_Hop_Luong_Theo_Ngach.xlsx";
        public const string RPT_TL_GIAI_THICH_CHI_TIET_LUONG = "rptLuong_Giai_Thich_Luong_Chi_Tiet.xlsx";
        public const string RPT_TL_GIAI_THICH_CHI_TIET_LUONG_A4 = "rptLuong_Giai_Thich_Luong_Chi_Tiet_A4.xlsx";
        public const string RPT_TL_GIAI_THICH_PHUCAPKHAC = "rptLuong_BangLuong_GiaiThich_PhuCapKhac.xlsx";
        public const string RPT_TL_GIAITHICH_CHITIET_PHUCAP_KHAC_TRANG1 = "rptLuong_GiaiThich_ChiTiet_PhuCap_Khac_Trang1.xlsx";
        public const string RPT_TL_GIAITHICH_CHITIET_PHUCAP_KHAC_SUMMARY = "rptLuong_GiaiThich_ChiTiet_PhuCap_Khac_Summary.xlsx";
        public const string RPT_TL_GIAITHICH_CHITIET_PHUCAP_KHAC_TRANG2 = "rptLuong_GiaiThich_ChiTiet_PhuCap_Khac_Trang2.xlsx";
        public const string RPT_TL_GIAITHICH_CHITIET_PHUCAP_KHAC_TRANG1_A3 = "rptLuong_GiaiThich_ChiTiet_PhuCap_Khac_Trang1_A3.xlsx";
        public const string RPT_TL_GIAITHICH_CHITIET_PHUCAP_KHAC_SUMMARY_A3 = "rptLuong_GiaiThich_ChiTiet_PhuCap_Khac_Summary_A3.xlsx";
        public const string RPT_TL_GIAITHICH_CHITIET_PHUCAP_KHAC_TRANG2_A3 = "rptLuong_GiaiThich_ChiTiet_PhuCap_Khac_Trang2_A3.xlsx";
        public const string RPT_TL_GIAITHICH_CHITIET_PHUCAP_TRUYLINH_KHAC_TRANG1 = "rptLuong_GiaiThich_ChiTiet_PhuCap_TruyLinh_Khac_Trang1.xlsx";
        public const string RPT_TL_GIAITHICH_CHITIET_PHUCAP_TRUYLINH_KHAC_SUMMARY = "rptLuong_GiaiThich_ChiTiet_PhuCap_TruyLinh_Khac_Summary.xlsx";
        public const string RPT_TL_GIAITHICH_CHITIET_PHUCAP_TRUYLINH_KHAC_TRANG2 = "rptLuong_GiaiThich_ChiTiet_PhuCap_TruyLinh_Khac_Trang2.xlsx";
        public const string RPT_TL_GIAITHICH_CHITIET_PHUCAP_TRUYLINH_KHAC_TRANG1_A3 = "rptLuong_GiaiThich_ChiTiet_PhuCap_TruyLinh_Khac_Trang1_A3.xlsx";
        public const string RPT_TL_GIAITHICH_CHITIET_PHUCAP_TRUYLINH_KHAC_SUMMARY_A3 = "rptLuong_GiaiThich_ChiTiet_PhuCap_TruyLinh_Khac_Summary_A3.xlsx";
        public const string RPT_TL_GIAITHICH_CHITIET_PHUCAP_TRUYLINH_KHAC_TRANG2_A3 = "rptLuong_GiaiThich_ChiTiet_PhuCap_TruyLinh_Khac_Trang2_A3.xlsx";
        public const string RPT_TL_LUONGNAM_KEHOACH = "rptLuong_NamKeHoach.xlsx";
        public const string RPT_TL_QT_THUONGXUYEN = "rptLuong_QuyetToan_ThuongXuyen.xlsx";
        public const string RPT_TL_QT_THUONGXUYEN_DUTOAN = "rptLuong_QuyetToan_ThuongXuyen_DuToan.xlsx";
        public const string RPT_TL_LUONG_THANG_IMPORT = "rptLuong_Luong_Thang_Import.xlsx";
        public const string RPT_TL_QLTHU_NOP_BHXH_IMPORT = "rptLuong_QLThuNop_BHXH_Import.xlsx";
        public const string RPT_TL_LUONG_THANG_BHXH_IMPORT = "rptLuong_Luong_Thang_BHXH_Import.xlsx";
        public const string RPT_TL_QT_THUONGXUYEN_THEO_COT = "rptLuong_QuyetToan_ThuongXuyen_Theo_Cot.xlsx";
        public const string RPT_TL_QT_THUONGXUYEN_THEO_COT_TRANG_1 = "rptLuong_QuyetToan_ThuongXuyen_Theo_Cot_Trang_1.xlsx";
        public const string RPT_TL_QT_THUONGXUYEN_THEO_COT_TRANG_2 = "rptLuong_QuyetToan_ThuongXuyen_Theo_Cot_Trang.xlsx";
        public const string RPT_TL_DIEUCHINH_QS_KEHOACH = "rptLuong_DieuChinh_QuanSo_KeHoach.xlsx";
        public const string RPT_TL_CHITIET_QS_RAQUAN_KEHOACH = "rptLuong_ChiTiet_QS_RaQuan_KeHoach.xlsx";
        public const string RPT_TL_CHITIET_QS_NGHIHUU_KEHOACH = "rptLuong_ChiTiet_QS_NghiHuu_KeHoach.xlsx";
        public const string RPT_TL_IN_KIEM = "rptLuong_InKiem.xlsx";
        public const string RPT_TL_QT_NAM_KEHOACH = "rptLuong_QuyetToan_Luong_Nam_KeHoach.xlsx";
        public const string RPT_TL_BIEN_DONG_LUONG = "rptLuong_ChiTiet_BienDong_Luong.xlsx";
        public const string RPT_TL_PHUCAP_CHITIET = "rptLuong_Phucap_ChiTiet.xlsx";
        public const string RPT_TL_DS_CAPPHAT_PHUCAP = "rptLuong_DS_CapPhat_PhuCap.xlsx";
        public const string RPT_TL_DS_CAPPHAT_PHUCAP_A3 = "rptLuong_DS_CapPhat_PhuCap_A3.xlsx";
        public const string RPT_TL_DS_CAPPHAT_PHUCAP_TONGHOP = "rptLuong_DS_CapPhat_PhuCap_TongHop.xlsx";
        public const string RPT_TL_DS_CAPPHAT_PHUCAP_TONGHOP_A3 = "rptLuong_DS_CapPhat_PhuCap_TongHop_A3.xlsx";
        public const string RPT_TL_QS_CHUNGTU_A3 = "rptLuong_QS_ChungTu_A3.xlsx";
        public const string RPT_TL_QS_CHUNGTU_A4 = "rptLuong_QS_ChungTu_A4.xlsx";
        public const string RPT_TL_CHITIET_CANBO_KEHOACH = "rptLuong_ChiTiet_CanBo_KeHoach.xlsx";
        public const string RPT_TL_BIENDONG_QUANHAM_KEHOACH = "rptLuong_ChiTiet_BienDong_QuanHam_KeHoach.xlsx";
        public const string RPT_TL_LUONG_THANG_SUMMARY = "rptLuong_BangLuong_Thang _Summary.xlsx";
        public const string RPT_TL_LUONG_THANG_A3 = "rptLuong_BangLuong_Thang _A3.xlsx";
        public const string RPT_TL_THU_NOP_BHXH_A3 = "rptLuong_Thu_Nop_BHXH _A3.xlsx";
        public const string RPT_TL_THU_NOP_BHXH = "rptLuong_Thu_Nop_BHXH.xlsx";
        public const string RPT_TL_LUONG_THANG_A3_SUMMARY = "rptLuong_BangLuong_Thang _A3 _Summary.xlsx";
        public const string RPT_TL_TONG_HOP_LUONG_PHU_CAP_THEO_NGACH_A3 = "rptLuong_Tong_Hop_Luong_Theo_Ngach _A3.xlsx";
        public const string RPT_TL_QTTX_THEO_CACH_TINH_LUONG = "rptLuong_QuyetToan_ThuongXuyen_Theo_Cach_Tinh_Luong.xlsx";
        public const string RPT_TL_TRUYLINH_CHUYENCHEDO_QNCN = "rptLuong_TruyLinhChuyenCheDo_Qncn.xlsx";
        public const string RPT_TL_TRUYLINH_CHUYENCHEDO_SQ = "rptLuong_TruyLinhChuyenCheDo_SQ.xlsx";
        public const string RPT_TL_THANG_THEO_YEU_TO_LUONG_TO_1 = "rptLuong_Bang_Luong_Thang_Theo_Yeu_To_Luong_To_1.xlsx";
        public const string RPT_TL_THANG_THEO_YEU_TO_LUONG_TO_2 = "rptLuong_Bang_Luong_Thang_Theo_Yeu_To_Luong_To_2.xlsx";
        public const string RPT_TL_THANG_THEO_CHIEU_DOC = "rptLuong_Bang_Luong_Thang_Theo_Chieu_Doc.xlsx";
        public const string RPT_TL_BAOCAO_TIENAN = "rptLuong_BaoCao_TienAn.xlsx";
        public const string RPT_TL_BAOCAO_GIAITHICH_LOI = "rptLuong_QuyetToan_ChungTu_GiaiThich_Loi.xlsx";
        public const string RPT_TL_BAOCAO_GIAITHICH_SOLIEU = "rptLuong_QuyetToan_ChungTu_GiaiThich_SoLieu.xlsx";
        public const string RPT_TL_TO_BIA_QTTX = "rptLuong_To_Bia_Qttx.xlsx";
        public const string RPT_TL_DIENBIEN_LUONG_CANBO = "rptLuong_DienBienLuong_CanBo.xlsx";
        public const string RPT_TL_DANHSACH_FOXPRO = "rptLuong_DanhSach_PhuCap_Va_DoiTuong_ChuaMap.xlsx";
        public const string RPT_TL_GIAITHICH_RAQUAN_XUATNGU = "rptLuong_DS_RaQuan.xlsx";
        public const string RPT_TL_CHITRA_NGANHANG_THUNHAPKHAC = "rptLuong_BaoCao_TriTraNganHang_ThuNhapKhac.xlsx";
        public const string RPT_TL_TNCN_IMPORT_EXPORT = "rptLuong_Luong_Tncn_Import.xlsx";
        public const string RPT_TL_PHU_CAP_CHUA_MAP = "rptLuong_Phu_Cap_Chua_Map.xlsx";
        public const string RPT_TL_QUYET_TOAN_TTNCN_A3 = "rptLuong_QuyetToan_Nam_ThueTncn_A3.xlsx";
        public const string RPT_TL_CHITIET_QUANSO_TANGGIAM = "rptLuong_ChiTiet_QuanSo_TangGiam.xlsx";
        public const string RPT_TL_GIAITHICH_BIENPHONG = "rptLuong_Giai_Thich_Phu_Cap_Bien_Phong.xlsx";
        public const string RPT_TL_GIAITHICH_BIENPHONG_HESO = "rptLuong_Giai_Thich_Phu_Cap_Bien_Phong_Theo_HeSo.xlsx";
        public const string RPT_TL_LUONG_THANG_A3_DONG = "rptLuong_BangLuong_Thang _A3_Dong.xlsx";
        public const string RPT_TL_LUONG_THANG_DONG = "rptLuong_BangLuong_Thang_Dong.xlsx";
        public const string RPT_TL_TONGHOP_LUONG_PHUCAP_BIENPHONG = "rptLuong_TongHop_Luong_PhuCap_BienPhong.xlsx";
        public const string RPT_TL_LUONG_GIAITHICH_PHUCAP_THEO_NGAY = "rptLuong_Bang_GiaiThich_Cac_PhuCap_Theo_Ngay.xlsX";
        public const string RPT_TL_LUONG_GIAITHICH_PHUCAP_THEO_NGAY_A3 = "rptLuong_Bang_GiaiThich_Cac_PhuCap_Theo_Ngay_A3.xlsx";
        public const string RPT_TL_CONGTHUC_IMPORT_EXPORT = "rptLuong_CongThuc_Import.xlsx";
        public const string RPT_TL_QUY_LUONG_CAN_CU = "rptLuong_Quy_Luong_Can_Cu.xlsx";

        //Luong new
        public const string RPT_TL_LUONG_THANG_NEW = "rptLuong_BangLuong_Thang.xlsx";
        public const string RPT_TL_LUONG_THANG_BIEN_PHONG_NEW = "rptLuong_BangLuong_Thang_Bien_Phong.xlsx";
        public const string RPT_TL_LUONG_THANG_BIEN_PHONG_1_NEW = "rptLuong_BangLuong_Thang_Bien_Phong_1.xlsx";
        public const string RPT_TL_LUONG_TRUY_LINH_NEW = "rptLuong_BangLuong_TruyLinh.xlsx";
        public const string RPT_TL_LUONG_TRUY_LINH_TONG_HOP_NEW = "rptLuong_BangLuong_TruyLinh_TongHop.xlsx";
        public const string RPT_TL_LUONG_TRUY_LINH_NHIEU_QD_NEW = "rptLuong_BangLuong_TruyLinh_NhieuNgay_QuyetDinh.xlsx";
        public const string RPT_TL_LUONG_TRUY_THU_NEW = "rptLuong_BangLuong_TruyThu.xlsx";
        public const string RPT_TL_LUONG_TRUY_LINH_DONG_PHU_CAP_NEW = "rptLuong_BangLuong_TruyLinh_Dong.xlsx";
        public const string RPT_TL_LUONG_TRUY_LINH_DONG_PHU_CAP_TONG_HOP_NEW = "rptLuong_BangLuong_TruyLinh_Dong_Tong_Hop.xlsx";
        public const string RPT_TL_LUONG_TRUY_THU_DONG_PHU_CAP_NEW = "rptLuong_BangLuong_TruyThu_Dong.xlsx";
        public const string RPT_TL_LUONG_TRUY_LINH_DONG_PHU_CAP2_NEW = "rptLuong_BangLuong_TruyLinh_Dong2.xlsx";
        public const string RPT_TL_LUONG_TRUY_LINH_DONG_PHU_CAP2_TONG_HOP_NEW = "rptLuong_BangLuong_TruyLinh_Dong2_TongHop.xlsx";
        public const string RPT_TL_LUONG_TRUY_THU_DONG_PHU_CAP2_NEW = "rptLuong_BangLuong_TruyThu_Dong2.xlsx";
        public const string RPT_TL_LUONG_BAO_HIEM_NEW = "rptLuong_BangLuong_BaoHiem.xlsx";
        public const string RPT_TL_QUYET_TOAN_TTNCN_NEW = "rptLuong_QuyetToan_Nam_ThueTncn.xlsx";
        public const string RPT_TL_DS_TRA_NGAN_HANG_NEW = "rptLuong_DanhSach_ChiTra_NganHang.xlsx";
        public const string RPT_TL_BANGKE_TRICHTHUETNCN_NEW = "rptLuong_BangKe_TrichThueTNCN.xlsx";
        public const string RPT_TL_BANGKE_TRICHTHUETNCN_A3_NEW = "rptLuong_BangKe_TrichThueTNCN_A3.xlsx";
        public const string RPT_TL_DANHSACH_CHITRA_LUONGCN_NEW = "rptLuong_DanhSach_ChiTra_LuongCN.xlsx";
        public const string RPT_TL_DS_CHITRA_LUONGCN_AN_DANH_NEW = "rptLuong_DanhSach_ChiTra_AnDanh.xlsx";
        public const string RPT_TL_DS_CHITRA_LUONGCN_AN_DANH_1HANG_NEW = "rptLuong_DanhSach_ChiTra_AnDanh_1Hang.xlsx";
        public const string RPT_TL_TONGHOP_LUONG_PHUCAP_DONVI_NEW = "rptLuong_TongHop_Luong_PhuCap_DonVi.xlsx";
        public const string RPT_TL_TONGHOP_LUONG_PHUCAP_DONVI_A3_NEW = "rptLuong_TongHop_Luong_PhuCap_DonVi_A3.xlsx";
        public const string RPT_TL_TONGHOP_LUONG_NGACHDONVI_NEW = "rptLuong_TongHop_Luong_NgachDonVi.xlsx";
        public const string RPT_TL_TONGHOP_LUONG_NGACHDONVI_A3_NEW = "rptLuong_TongHop_Luong_NgachDonVi_A3.xlsx";
        public const string RPT_TL_QUYETTOAN_QUANSO_NEW = "rptLuong_QuyetToan_QuanSo.xlsx";
        public const string RPT_TL_GIAITHICH_CHITIET_PHUCAPTNVKTHD_NEW = "rptLuong_GiaiThich_ChiTiet_PhuCapTNVKTHD.xlsx";
        public const string RPT_TL_GIAY_GT_TC_NEW = "rptLuong_Giay_GioiThieu_TaiChinh.xlsx";
        public const string RPT_TL_GIAY_GT_TC_A4_NEW = "rptLuong_Giay_GioiThieu_TaiChinh_A4.xlsx";
        public const string RPT_TL_GIAY_GT_TC_A4_KHONG_PC_HUONG_NEW = "rptLuong_Giay_GioiThieu_TaiChinh_A4_Khong_Pc_Huong.xlsx";
        public const string RPT_TL_TONG_HOP_LUONG_PHU_CAP_THEO_NGACH_NEW = "rptLuong_Tong_Hop_Luong_Theo_Ngach.xlsx";
        public const string RPT_TL_GIAI_THICH_CHI_TIET_LUONG_NEW = "rptLuong_Giai_Thich_Luong_Chi_Tiet.xlsx";
        public const string RPT_TL_GIAI_THICH_CHI_TIET_LUONG_A4_NEW = "rptLuong_Giai_Thich_Luong_Chi_Tiet_A4.xlsx";
        public const string RPT_TL_GIAI_THICH_PHUCAPKHAC_NEW = "rptLuong_BangLuong_GiaiThich_PhuCapKhac.xlsx";
        public const string RPT_TL_GIAITHICH_CHITIET_PHUCAP_KHAC_TRANG1_NEW = "rptLuong_GiaiThich_ChiTiet_PhuCap_Khac_Trang1.xlsx";
        public const string RPT_TL_GIAITHICH_CHITIET_PHUCAP_KHAC_SUMMARY_NEW = "rptLuong_GiaiThich_ChiTiet_PhuCap_Khac_Summary.xlsx";
        public const string RPT_TL_GIAITHICH_CHITIET_PHUCAP_KHAC_TRANG2_NEW = "rptLuong_GiaiThich_ChiTiet_PhuCap_Khac_Trang2.xlsx";
        public const string RPT_TL_GIAITHICH_CHITIET_PHUCAP_KHAC_TRANG1_A3_NEW = "rptLuong_GiaiThich_ChiTiet_PhuCap_Khac_Trang1_A3.xlsx";
        public const string RPT_TL_GIAITHICH_CHITIET_PHUCAP_KHAC_SUMMARY_A3_NEW = "rptLuong_GiaiThich_ChiTiet_PhuCap_Khac_Summary_A3.xlsx";
        public const string RPT_TL_GIAITHICH_CHITIET_PHUCAP_KHAC_TRANG2_A3_NEW = "rptLuong_GiaiThich_ChiTiet_PhuCap_Khac_Trang2_A3.xlsx";
        public const string RPT_TL_GIAITHICH_CHITIET_PHUCAP_TRUYLINH_KHAC_TRANG1_NEW = "rptLuong_GiaiThich_ChiTiet_PhuCap_TruyLinh_Khac_Trang1.xlsx";
        public const string RPT_TL_GIAITHICH_CHITIET_PHUCAP_TRUYLINH_KHAC_SUMMARY_NEW = "rptLuong_GiaiThich_ChiTiet_PhuCap_TruyLinh_Khac_Summary.xlsx";
        public const string RPT_TL_GIAITHICH_CHITIET_PHUCAP_TRUYLINH_KHAC_TRANG2_NEW = "rptLuong_GiaiThich_ChiTiet_PhuCap_TruyLinh_Khac_Trang2.xlsx";
        public const string RPT_TL_GIAITHICH_CHITIET_PHUCAP_TRUYLINH_KHAC_TRANG1_A3_NEW = "rptLuong_GiaiThich_ChiTiet_PhuCap_TruyLinh_Khac_Trang1_A3.xlsx";
        public const string RPT_TL_GIAITHICH_CHITIET_PHUCAP_TRUYLINH_KHAC_SUMMARY_A3_NEW = "rptLuong_GiaiThich_ChiTiet_PhuCap_TruyLinh_Khac_Summary_A3.xlsx";
        public const string RPT_TL_GIAITHICH_CHITIET_PHUCAP_TRUYLINH_KHAC_TRANG2_A3_NEW = "rptLuong_GiaiThich_ChiTiet_PhuCap_TruyLinh_Khac_Trang2_A3.xlsx";
        public const string RPT_TL_LUONGNAM_KEHOACH_NEW = "rptLuong_NamKeHoach.xlsx";
        public const string RPT_TL_QT_THUONGXUYEN_NEW = "rptLuong_QuyetToan_ThuongXuyen.xlsx";
        public const string RPT_TL_QT_THUONGXUYEN_DUTOAN_NEW = "rptLuong_QuyetToan_ThuongXuyen_DuToan.xlsx";
        public const string RPT_TL_LUONG_THANG_IMPORT_NEW = "rptLuong_Luong_Thang_Import.xlsx";
        public const string RPT_TL_QLTHU_NOP_BHXH_IMPORT_NEW = "rptLuong_QLThuNop_BHXH_Import.xlsx";
        public const string RPT_TL_LUONG_THANG_BHXH_IMPORT_NEW = "rptLuong_Luong_Thang_BHXH_Import.xlsx";
        public const string RPT_TL_QT_THUONGXUYEN_THEO_COT_NEW = "rptLuong_QuyetToan_ThuongXuyen_Theo_Cot.xlsx";
        public const string RPT_TL_QT_THUONGXUYEN_THEO_COT_TRANG_1_NEW = "rptLuong_QuyetToan_ThuongXuyen_Theo_Cot_Trang_1.xlsx";
        public const string RPT_TL_QT_THUONGXUYEN_THEO_COT_TRANG_2_NEW = "rptLuong_QuyetToan_ThuongXuyen_Theo_Cot_Trang.xlsx";
        public const string RPT_TL_DIEUCHINH_QS_KEHOACH_NEW = "rptLuong_DieuChinh_QuanSo_KeHoach.xlsx";
        public const string RPT_TL_CHITIET_QS_RAQUAN_KEHOACH_NEW = "rptLuong_ChiTiet_QS_RaQuan_KeHoach.xlsx";
        public const string RPT_TL_CHITIET_QS_NGHIHUU_KEHOACH_NEW = "rptLuong_ChiTiet_QS_NghiHuu_KeHoach.xlsx";
        public const string RPT_TL_IN_KIEM_NEW = "rptLuong_InKiem.xlsx";
        public const string RPT_TL_QT_NAM_KEHOACH_NEW = "rptLuong_QuyetToan_Luong_Nam_KeHoach.xlsx";
        public const string RPT_TL_BIEN_DONG_LUONG_NEW = "rptLuong_ChiTiet_BienDong_Luong.xlsx";
        public const string RPT_TL_PHUCAP_CHITIET_NEW = "rptLuong_Phucap_ChiTiet.xlsx";
        public const string RPT_TL_DS_CAPPHAT_PHUCAP_NEW = "rptLuong_DS_CapPhat_PhuCap.xlsx";
        public const string RPT_TL_DS_CAPPHAT_PHUCAP_A3_NEW = "rptLuong_DS_CapPhat_PhuCap_A3.xlsx";
        public const string RPT_TL_DS_CAPPHAT_PHUCAP_TONGHOP_NEW = "rptLuong_DS_CapPhat_PhuCap_TongHop.xlsx";
        public const string RPT_TL_DS_CAPPHAT_PHUCAP_TONGHOP_A3_NEW = "rptLuong_DS_CapPhat_PhuCap_TongHop_A3.xlsx";
        public const string RPT_TL_QS_CHUNGTU_A3_NEW = "rptLuong_QS_ChungTu_A3.xlsx";
        public const string RPT_TL_QS_CHUNGTU_A4_NEW = "rptLuong_QS_ChungTu_A4.xlsx";
        public const string RPT_TL_CHITIET_CANBO_KEHOACH_NEW = "rptLuong_ChiTiet_CanBo_KeHoach.xlsx";
        public const string RPT_TL_BIENDONG_QUANHAM_KEHOACH_NEW = "rptLuong_ChiTiet_BienDong_QuanHam_KeHoach.xlsx";
        public const string RPT_TL_LUONG_THANG_SUMMARY_NEW = "rptLuong_BangLuong_Thang _Summary.xlsx";
        public const string RPT_TL_LUONG_THANG_A3_NEW = "rptLuong_BangLuong_Thang _A3.xlsx";
        public const string RPT_TL_THU_NOP_BHXH_A3_NEW = "rptLuong_Thu_Nop_BHXH _A3.xlsx";
        public const string RPT_TL_THU_NOP_BHXH_NEW = "rptLuong_Thu_Nop_BHXH.xlsx";
        public const string RPT_TL_LUONG_THANG_A3_SUMMARY_NEW = "rptLuong_BangLuong_Thang _A3 _Summary.xlsx";
        public const string RPT_TL_TONG_HOP_LUONG_PHU_CAP_THEO_NGACH_A3_NEW = "rptLuong_Tong_Hop_Luong_Theo_Ngach _A3.xlsx";
        public const string RPT_TL_QTTX_THEO_CACH_TINH_LUONG_NEW = "rptLuong_QuyetToan_ThuongXuyen_Theo_Cach_Tinh_Luong.xlsx";
        public const string RPT_TL_TRUYLINH_CHUYENCHEDO_QNCN_NEW = "rptLuong_TruyLinhChuyenCheDo_Qncn.xlsx";
        public const string RPT_TL_TRUYLINH_CHUYENCHEDO_SQ_NEW = "rptLuong_TruyLinhChuyenCheDo_SQ.xlsx";
        public const string RPT_TL_THANG_THEO_YEU_TO_LUONG_TO_1_NEW = "rptLuong_Bang_Luong_Thang_Theo_Yeu_To_Luong_To_1.xlsx";
        public const string RPT_TL_THANG_THEO_YEU_TO_LUONG_TO_2_NEW = "rptLuong_Bang_Luong_Thang_Theo_Yeu_To_Luong_To_2.xlsx";
        public const string RPT_TL_THANG_THEO_CHIEU_DOC_NEW = "rptLuong_Bang_Luong_Thang_Theo_Chieu_Doc.xlsx";
        public const string RPT_TL_BAOCAO_TIENAN_NEW = "rptLuong_BaoCao_TienAn.xlsx";
        public const string RPT_TL_BAOCAO_GIAITHICH_LOI_NEW = "rptLuong_QuyetToan_ChungTu_GiaiThich_Loi.xlsx";
        public const string RPT_TL_BAOCAO_GIAITHICH_SOLIEU_NEW = "rptLuong_QuyetToan_ChungTu_GiaiThich_SoLieu.xlsx";
        public const string RPT_TL_TO_BIA_QTTX_NEW = "rptLuong_To_Bia_Qttx.xlsx";
        public const string RPT_TL_DIENBIEN_LUONG_CANBO_NEW = "rptLuong_DienBienLuong_CanBo.xlsx";
        public const string RPT_TL_DANHSACH_FOXPRO_NEW = "rptLuong_DanhSach_PhuCap_Va_DoiTuong_ChuaMap.xlsx";
        public const string RPT_TL_GIAITHICH_RAQUAN_XUATNGU_NEW = "rptLuong_DS_RaQuan.xlsx";
        public const string RPT_TL_CHITRA_NGANHANG_THUNHAPKHAC_NEW = "rptLuong_BaoCao_TriTraNganHang_ThuNhapKhac.xlsx";
        public const string RPT_TL_TNCN_IMPORT_EXPORT_NEW = "rptLuong_Luong_Tncn_Import.xlsx";
        public const string RPT_TL_PHU_CAP_CHUA_MAP_NEW = "rptLuong_Phu_Cap_Chua_Map.xlsx";
        public const string RPT_TL_QUYET_TOAN_TTNCN_A3_NEW = "rptLuong_QuyetToan_Nam_ThueTncn_A3.xlsx";
        public const string RPT_TL_CHITIET_QUANSO_TANGGIAM_NEW = "rptLuong_ChiTiet_QuanSo_TangGiam.xlsx";
        public const string RPT_TL_GIAITHICH_BIENPHONG_NEW = "rptLuong_Giai_Thich_Phu_Cap_Bien_Phong.xlsx";
        public const string RPT_TL_GIAITHICH_BIENPHONG_HESO_NEW = "rptLuong_Giai_Thich_Phu_Cap_Bien_Phong_Theo_HeSo.xlsx";
        public const string RPT_TL_LUONG_THANG_A3_DONG_NEW = "rptLuong_BangLuong_Thang _A3_Dong.xlsx";
        public const string RPT_TL_LUONG_THANG_DONG_NEW = "rptLuong_BangLuong_Thang_Dong.xlsx";
        public const string RPT_TL_TONGHOP_LUONG_PHUCAP_BIENPHONG_NEW = "rptLuong_TongHop_Luong_PhuCap_BienPhong.xlsx";
        public const string RPT_TL_LUONG_GIAITHICH_PHUCAP_THEO_NGAY_NEW = "rptLuong_Bang_GiaiThich_Cac_PhuCap_Theo_Ngay.xlsX";
        public const string RPT_TL_LUONG_GIAITHICH_PHUCAP_THEO_NGAY_A3_NEW = "rptLuong_Bang_GiaiThich_Cac_PhuCap_Theo_Ngay_A3.xlsx";
        public const string RPT_TL_CONGTHUC_IMPORT_EXPORT_NEW = "rptLuong_CongThuc_Import.xlsx";
        public const string RPT_TL_QUY_LUONG_CAN_CU_NEW = "rptLuong_Quy_Luong_Can_Cu.xlsx";
        //Ngan Sach
        //So nhu cau
        public const string RPT_NS_SNC_DONVI_NSSD = "rptNS_DonVi_NSSD.xls";
        public const string RPT_NS_SNC_DONVI_PHULUC_NSSD = "rptNS_DonVi_PhuLuc_NSSD.xls";
        public const string RPT_NS_SNC_DONVI_NSBD = "rptNS_DonVi_NSBD.xls";
        public const string RPT_NS_SNC_DONVI_PHULUC_NSBD = "rptNS_DonVi_PhuLuc_NSBD.xls";
        public const string RPT_NS_SNC_TONGHOP_TRINHKY_NSSD = "rptNS_SNC_TongHop_TrinhKy_NSSD.xlsx";
        public const string RPT_NS_SNC_PHAN_BO_PHU_LUC_DON_VI = "rptNS_PhanBo_ChiTiet_SoKiemTra_TheoNganh_PhuLuc_DonVi.xlsx";
        public const string RPT_NS_SNC_TONGHOP_PHULUC1_NSSD_TO1 = "rptNS_SNC_TongHop_PhuLuc1_NSSD_To1.xlsx";
        public const string RPT_NS_SNC_TONGHOP_PHULUC1_NSSD_TO2 = "rptNS_SNC_TongHop_PhuLuc1_NSSD_To.xlsx";
        public const string RPT_NS_SNC_TONGHOP_PHULUC1_NSSD_TUCHI_TO1 = "rptNS_SNC_TongHop_PhuLuc1_NSSD_TuChi_To1.xlsx";
        public const string RPT_NS_SNC_TONGHOP_PHULUC1_NSSD_TUCHI_TO2 = "rptNS_SNC_TongHop_PhuLuc1_NSSD_TuChi_To.xlsx";
        public const string RPT_NS_SNC_TONGHOP_PHULUC1_NSSD_HUYDONG_TO1 = "rptNS_SNC_TongHop_PhuLuc1_NSSD_HuyDong_To1.xlsx";
        public const string RPT_NS_SNC_TONGHOP_PHULUC1_NSSD_HUYDONG_TO2 = "rptNS_SNC_TongHop_PhuLuc1_NSSD_HuyDong_To.xlsx";
        public const string RPT_NS_SNC_TONGHOP_PHULUC2_NSSD = "rptNS_SNC_TongHop_PhuLuc2_NSSD.xlsx";
        public const string RPT_NS_SNC_TONGHOP_TRINHKY_NSBD = "rptNS_SNC_TongHop_TrinhKy_NSBD.xlsx";
        public const string RPT_NS_SNC_TONGHOP_PHULUC1_NSBD_TO1 = "rptNS_SNC_TongHop_PhuLuc1_NSBD_To1.xlsx";
        public const string RPT_NS_SNC_TONGHOP_PHULUC1_NSBD_TO2 = "rptNS_SNC_TongHop_PhuLuc1_NSBD_To.xlsx";
        public const string RPT_NS_SNC_TONGHOP_PHULUC2_NSBD = "rptNS_SNC_TongHop_PhuLuc2_NSBD.xlsx";
        public const string RPT_NS_SNC_TONGHOP_PHULUC3_NSQP = "rptNS_SNC_TongHop_PhuLuc3.xlsx";
        public const string RPT_NS_SNC_TONGHOP_PHULUC3_NSNN = "rptNS_SNC_TongHop_PhuLuc3_NSNN.xlsx";
        public const string RPT_NS_SNC_TONGHOP_PHULUC4 = "rptNS_SNC_TongHop_PhuLuc4.xlsx";
        public const string RPT_NS_SNC_TONGHOP_PHULUC4_NSNN = "rptNS_SNC_TongHop_PhuLuc4_NSNN.xlsx";
        public const string RPT_NS_SNC_TONGHOP_PHULUC5 = "rptNS_SNC_TongHop_PhuLuc5.xlsx";
        public const string RPT_NS_SNC_TONGHOP_PHULUC6 = "rptNS_SNC_TongHop_PhuLuc6.xlsx";
        //So Kiem Tra
        public const string RPT_NS_SKT_SO_SANH_NAM_TRUOC_NAM_NAY = "rptNS_SKT_SoSanhNamTruocNamNay.xlsx";
        public const string RPT_NS_SKT_SO_SANH_NAM_TRUOC_NAM_NAY_EXCEL = "rptNS_SKT_SoSanhNamTruocNamNay_Excel.xlsx";
        public const string RPT_NS_SKT_SO_SANH_NAM_TRUOC_NAM_NAY_TONGHOP_EXCEL = "rptNS_SKT_SoSanhNamTruocNamNay_TongHop_Excel.xlsx";
        public const string RPT_NS_SKT_NHANSOKIEMTRA_TRINHKY_NSSD = "rptNS_SKT_NhanSoKiemTra_TrinhKy_NSSD.xlsx";
        public const string RPT_NS_SKT_NHANSOKIEMTRA_PHULUC_NSSD = "rptNS_SKT_NhanSoKiemTra_PhuLuc_NSSD.xlsx";
        public const string RPT_NS_SKT_NHANSOKIEMTRA_TRINHKY_NSBD = "rptNS_SKT_NhanSoKiemTra_TrinhKy_NSBD.xlsx";
        public const string RPT_NS_SKT_NHANSOKIEMTRA_PHULUC_NSBD = "rptNS_SKT_NhanSoKiemTra_PhuLuc_NSBD.xlsx";
        public const string RPT_NS_TONGHOP_SOKIEMTRA_BENHVIENTUCHU = "rptNS_TongHop_SoKiemTra_BenhVienTuChu.xlsx";
        public const string RPT_NS_TONGHOP_SOKIEMTRA_BENHVIENTUCHU_DOC = "rptNS_TongHop_SoKiemTra_BenhVienTuChu_Doc.xlsx";
        //Phan bo so kiem tra
        public const string RPT_NS_PHANBO_SOKIEMTRA_DONVI_TRINHKY_NSSD = "rptNS_PhanBo_SoKiemTra_DonVi_TrinhKy_NSSD.xlsx";
        public const string RPT_NS_PHANBO_SOKIEMTRA_DONVI_PHULUC_NSSD = "rptNS_PhanBo_SoKiemTra_DonVi_PhuLuc_NSSD.xlsx";
        public const string RPT_NS_PHANBO_SOKIEMTRA_DONVI_TRINHKY_NSBD = "rptNS_PhanBo_SoKiemTra_DonVi_TrinhKy_NSBD.xlsx";
        public const string RPT_NS_PHANBO_SOKIEMTRA_DONVI_PHULUC_NSBD = "rptNS_PhanBo_SoKiemTra_DonVi_PhuLuc_NSBD.xlsx";
        public const string RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_TRINHKY_NSSD = "rptNS_TongHop_PhanBo_SoKiemTra_TrinhKy_NSSD.xlsx";
        public const string RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_TRINHKY_NSBD = "rptNS_TongHop_PhanBo_SoKiemTra_TrinhKy_NSBD.xlsx";
        public const string RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_PHULUC_NSSD_TO1 = "rptNS_TongHop_PhanBo_SoKiemTra_PhuLuc_NSSD_To1.xlsx";
        public const string RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_PHULUC_NSSD_TO1_ONEPAPER = "rptNS_TongHop_PhanBo_SoKiemTra_PhuLuc_NSSD_To1_OnePaper.xlsx";
        public const string RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_PHULUC_NSSD_TO1_ONEPAPER_EXCEL = "rptNS_TongHop_PhanBo_SoKiemTra_PhuLuc_NSSD_To1_OnePaper_Excel.xlsx";
        public const string RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_PHULUC_NSBD_TO1 = "rptNS_TongHop_PhanBo_SoKiemTra_PhuLuc_NSBD_To1.xlsx";
        public const string RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_PHULUC_NSBD_TO1_ONEPAPER = "rptNS_TongHop_PhanBo_SoKiemTra_PhuLuc_NSBD_To1_OnePaper.xlsx";
        public const string RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_PHULUC_NSSD_TO2 = "rptNS_TongHop_PhanBo_SoKiemTra_PhuLuc_NSSD_To.xlsx";
        public const string RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_PHULUC_NSSD_TO2_ONEPAPER = "rptNS_TongHop_PhanBo_SoKiemTra_PhuLuc_NSSD_To_OnePaper.xlsx";
        public const string RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_PHULUC_NSSD_TO1_EXCEL = "rptNS_TongHop_PhanBo_SoKiemTra_PhuLuc_NSSD_To_Excel.xlsx";
        public const string RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_PHULUC_NSBD_TO2 = "rptNS_TongHop_PhanBo_SoKiemTra_PhuLuc_NSBD_To.xlsx";
        public const string RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_PHULUC_NSBD_TO2_ONEPAPER = "rptNS_TongHop_PhanBo_SoKiemTra_PhuLuc_NSBD_To_OnePaper.xlsx";
        public const string RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_THEONGANH_TRINHKY = "rptNS_PhanBo_SoKiemTra_TheoNganh_TrinhKy.xlsx";
        public const string RPT_NS_CHITIET_PHANBO_SOKIEMTRA_NSBD = "rptNS_ChiTiet_PhanBo_SoKiemTra_NSBD.xlsx";
        public const string RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_THEONGANH_PHULUC_TO1 = "rptNS_PhanBo_SoKiemTra_TheoNganh_PhuLuc_Trang1.xlsx";
        public const string RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_THEONGANH_PHULUC_TO2 = "rptNS_PhanBo_SoKiemTra_TheoNganh_PhuLuc_Trang.xlsx";
        public const string RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_THEONGANH_PHULUC_EMPTY = "rptNS_PhanBo_SoKiemTra_TheoNganh_PhuLuc_Empty.xlsx";
        public const string RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_THEONGANH_PHULUC_DONVI_DOC_TO1 = "rptNS_PhanBo_SoKiemTra_TheoNganh_PhuLuc_DonVi_Doc_Trang1.xlsx";
        public const string RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_THEONGANH_PHULUC_DONVI_DOC_TO2 = "rptNS_PhanBo_SoKiemTra_TheoNganh_PhuLuc_DonVi_Doc_Trang.xlsx";
        public const string RPT_NS_NHAN_SOKIEMTRA_THEONGANH_PHULUC = "rptNS_Nhan_SoKiemTra_TheoNganh_PhuLuc.xlsx";
        public const string RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_THEONGANH_PHULUC_DONVI_EXECL_1TO = "rptNS_PhanBo_SoKiemTra_TheoNganh_PhuLuc_DonVi_Excel_1To.xlsx";
        public const string RPT_NS_PHUONG_AN_PHANBO_SOKIEMTRA_02A_TO1 = "rptNS_PhanBo_SoKiemTra_PhuongAn_PhanBo_SKT_02a_Trang1.xlsx";
        public const string RPT_NS_PHUONG_AN_PHANBO_SOKIEMTRA_02A_TO2 = "rptNS_PhanBo_SoKiemTra_PhuongAn_PhanBo_SKT_02a_Trang.xlsx";
        public const string RPT_NS_PHUONG_AN_PHANBO_SOKIEMTRA_02A_EMPTY = "rptNS_PhanBo_SoKiemTra_PhuongAn_PhanBo_SKT_02a_Empty.xlsx";
        public const string RPT_NS_PHUONG_AN_PHANBO_SOKIEMTRA_02B = "rptNS_PhanBo_SoKiemTra_PhuongAn_PhanBo_SKT_02b.xlsx";
        public const string RPT_NS_DTDN_DUTOAN_NGAN_SACH_DONVI = "rptNS_DuToanDauNam_DuToanNganSach_ChiTietDonVi.xlsx";
        public const string RPT_NS_DTDN_DUTOAN_NGAN_SACH_DONVINGANG_TO = "rptNS_DuToanDauNam_DuToanNganSach_ChiTiet_DonViNgang_To.xlsx";
        public const string RPT_NS_DTDN_DUTOAN_NGAN_SACH_DONVINGANG_TO1 = "rptNS_DuToanDauNam_DuToanNganSach_ChiTiet_DonViNgang_To1.xlsx";
        public const string RPT_NS_DTDN_DUTOAN_NGAN_SACH_DONVINGANG_EXCEL = "rptNS_DuToanDauNam_DuToanNganSach_ChiTiet_DonViNgang_Excel.xlsx";
        public const string RPT_NS_DTDN_DUTOAN_NGAN_SACH_DONVI_NSDTN = "rptNS_DuToanDauNam_DuToanNganSach_ChiTietDonVi_NSDTN.xlsx";
        public const string RPT_NS_DTDN_DUTOAN_NGAN_SACH_DONVINGANG_NSDTN_TO = "rptNS_DuToanDauNam_DuToanNganSach_ChiTiet_DonViNgang_NSDTN_To.xlsx";
        public const string RPT_NS_DTDN_DUTOAN_NGAN_SACH_DONVINGANG_NSDTN_TO1 = "rptNS_DuToanDauNam_DuToanNganSach_ChiTiet_DonViNgang_NSDTN_To1.xlsx";
        public const string RPT_NS_DTDN_DUTOAN_NGAN_SACH_DONVINGANG_NSDTN_EXCEL = "rptNS_DuToanDauNam_DuToanNganSach_ChiTiet_DonViNgang_NSDTN_Excel.xlsx";

        public const string RPT_NS_DOICHIEU_SKT_DTDN = "rptNS_DoiChieu_SKT_DTDN.xlsx";
        public const string RPT_NS_DOICHIEU_SKT_DTDN_DACTHU = "rptNS_DoiChieu_SKT_DTDN_DacThu.xlsx";


        //Du toan
        public const string RPT_NS_DUTOAN_TOBIA = "rptNS_DuToan_ToBia";
        public const string RPT_NS_DUTOAN_TOBIA2 = "rptNS_DuToan_ToBia2";
        public const string RPT_NS_DUTOAN_TOBIA3 = "rptNS_DuToan_ToBia3";
        public const string RPT_NS_DUTOAN_TOBIA4 = "rptNS_DuToan_ToBia4";
        public const string RPT_NS_DUTOAN_TOBIA5 = "rptNS_DuToan_ToBia5";
        public const string RPT_NS_DUTOAN_TOBIA6 = "rptNS_DuToan_ToBia6";
        public const string RPT_NS_DUTOAN_TOBIA7 = "rptNS_DuToan_ToBia7";

        public const string RPT_NS_DUTOAN_TONGHOPSOPHANBODUTOAN = "rptNS_DuToan_TongHopSoPhanBoDuToan";
        public const string RPT_NS_DUTOAN_TONGHOPSOPHANBODUTOANCHIONEPAGEEXCEL = "rptNS_DuToan_TongHopSoPhanBoDuToanChiAllExcel";
        public const string RPT_NS_DUTOAN_TONGHOPSOPHANBODUTOANHIENVATONEPAGEEXCEL = "rptNS_DuToan_TongHopSoPhanBoDuToanHienVatAllExcel";
        public const string RPT_NS_DUTOAN_TONGHOPSOPHANBODUTOAN_TO2 = "rptNS_DuToan_TongHopSoPhanBoDuToan_To2";
        public const string RPT_NS_DUTOAN_TONGHOPSOPHANBODUTOANALL = "rptNS_DuToan_TongHopSoPhanBoDuToanAll";
        public const string RPT_NS_DUTOAN_TONGHOPSOPHANBODUTOANALL_EXCEL = "rptNS_DuToan_TongHopSoPhanBoDuToanAllExcel";
        public const string RPT_NS_DUTOAN_TONGHOPSOPHANBODUTOANONEPAPER = "rptNS_DuToan_TongHopSoPhanBoDuToanAll_OnePaper";
        public const string RPT_NS_DUTOAN_TONGHOPSOPHANBODUTOANALL_TO2 = "rptNS_DuToan_TongHopSoPhanBoDuToanAll_To2";
        public const string RPT_NS_DUTOAN_TONGHOPSOPHANBODUTOANONEPAPER_TO2 = "rptNS_DuToan_TongHopSoPhanBoDuToanAll_To2_OnePaper";

        public const string RPT_NS_DUTOAN_PHANBO_TONGHOP_TO1 = "rptNS_DuToan_PhanBo_TongHop_To1";
        public const string RPT_NS_DUTOAN_NGANH = "rptNS_DuToan_Nganh";
        public const string RPT_NS_DUTOAN_CONGKHAI_THUCHI_TO1 = "rptNS_DuToan_CongKhai_ThuChi_To1.xlsx";
        public const string RPT_NS_DUTOAN_CONGKHAI_THUCHI_TO1_EXCEL = "rptNS_DuToan_CongKhai_Excel.xlsx";
        public const string RPT_NS_DUTOAN_CONGKHAI_THUCHI_TO2 = "rptNS_DuToan_CongKhai_ThuChi_To2.xlsx";
        public const string RPT_NS_DUTOAN_CONGKHAI_THUCHI_TO_EMPTY = "rptNS_DuToan_CongKhai_ThuChi_To_Empty.xlsx";
        public const string RPT_NS_DUTOAN_CONGKHAI_THUCHI_EXCEL = "rptNS_DuToan_CongKhai_ThuChi_Excel.xlsx";
        public const string RPT_NS_DUTOAN_CONGKHAI_TO1 = "rptNS_DuToan_CongKhai_To1.xlsx";
        public const string RPT_NS_DUTOAN_CONGKHAI_TO2 = "rptNS_DuToan_CongKhai_To2.xlsx";
        public const string RPT_NS_DUTOAN_CONGKHAI_TO1_EMPTY = "rptNS_DuToan_CongKhai_To1_Empty.xlsx";
        public const string RPT_NS_DUTOAN_CONGKHAI_02CKNS = "rptNS_DuToan_CongKhai_02CKNS.xlsx";
        public const string RPT_NS_DUTOAN_CONGKHAI_02CKNS_EXCEL = "rptNS_DuToan_CongKhai_02CKNS_Excel.xlsx";
        public const string RPT_NS_DUTOAN_CONGKHAI_02CKNS_To1 = "rptNS_DuToan_CongKhai_02CKNS.xlsx";
        public const string RPT_NS_DUTOAN_CONGKHAI_02CKNS_To2 = "rptNS_DuToan_CongKhai_02CKNS.xlsx";
        public const string RPT_NS_DUTOAN_CONGKHAI_02CKNS_To1_EMPTY = "rptNS_DuToan_CongKhai_02CKNS_To1_Empty.xlsx";

        public const string RPT_NS_DUTOAN_CHITIET_DONVI2_HD4554 = "rptNS_DuToan_ChiTiet_DonVi2_HD4554";
        public const string RPT_NS_DUTOAN_CHITIET_DONVI2_HD4554_Excel = "rptNS_DuToan_ChiTiet_DonVi2_HD4554_NSSD_Excel.xlsx";
        public const string RPT_NS_DUTOAN_CHITIET_DONVI = "rptNS_DuToan_ChiTiet_DonVi";
        public const string RPT_NS_DUTOAN_CHITIET_DONVI2 = "rptNS_DuToan_ChiTiet_DonVi2";
        public const string RPT_NS_DUTOAN_CHITIET_DONVI3 = "rptNS_DuToan_ChiTiet_DonVi3";
        public const string RPT_NS_DUTOAN_CHITIET_DONVI4 = "rptNS_DuToan_ChiTiet_DonVi4";
        public const string RPT_NS_DUTOAN_CHITIET_DONVI5 = "rptNS_DuToan_ChiTiet_DonVi5";
        public const string RPT_NS_DUTOAN_CHITIET_DONVI6 = "rptNS_DuToan_ChiTiet_DonVi6";
        public const string RPT_NS_DUTOAN_CHITIET_DONVI7 = "rptNS_DuToan_ChiTiet_DonVi7";
        public const string RPT_NS_DUTOAN_DONVI_TONGHOP = "rptNS_DuToan_DonVi_TongHop";
        public const string RPT_NS_DUTOAN_CHITIEU_NGANH = "rptNS_DuToan_ChiTieu_Nganh";
        public const string RPT_NS_DUTOAN_CHITIEU_TONGHOP_DONVI = "rptNS_DuToan_ChiTieu_TongHop_DonVi";
        public const string RPT_NS_DUTOAN_CHITIEU_TONGHOP_DONVI_EXCEL = "rptNS_DuToan_ChiTieu_TongHop_DonVi_To1_Excel";
        public const string RPT_NS_DUTOAN_CHITIEU_TONGHOP_DONVI_ONEPAPER = "rptNS_DuToan_ChiTieu_TongHop_DonVi_OnePaper";
        public const string RPT_NS_DUTOAN_CHITIEU_TONGHOP_DOTNHAN = "rptNS_DuToan_ChiTieu_TongHop_DotNhan";
        public const string RPT_NS_DUTOAN_CHITIEU_TONGHOP_DOTNHAN_EXCEL = "rptNS_DuToan_ChiTieu_TongHop_DotNhan_To1_Excel";
        public const string RPT_NS_DUTOAN_CHITIEU_TONGHOP_NGANH = "rptNS_DuToan_ChiTieu_TongHop_Nganh";
        public const string RPT_NS_DUTOAN_CHITIEU_LNS = "rptNS_DuToan_ChiTieu_LNS";
        public const string RPT_NS_DUTOAN_CHITIEU_LNS_EXCEl = "rptNS_DuToan_ChiTieu_LNS_Normal_Excel";
        public const string RPT_NS_DUTOAN_CHITIEU_LNS_CHONTO = "rptNS_DuToan_ChiTieu_Lns_ChonTo";
        public const string RPT_NS_DUTOAN_CHITIEU_LUYKE_TONGHOP = "rptNS_DuToan_ChiTieu_LuyKe_TongHop";
        public const string RPT_NS_DUTOAN_CHITIEU_DUPHONG_DOTNHAN = "rptNS_DuToan_ChiTieu_TongHop_DotNhan_DuPhong_To1_Excel";
        public const string RPT_NS_DUTOAN_CHITIEU_DUPHONG = "rptNS_DuToan_ChiTieu_DuPhong";
        public const string RPT_NS_DUTOAN_THONGKE_SOQUYETDINH = "rptNS_DuToan_ThongKe_SoQuyetDinh";
        public const string RPT_NS_DUTOAN_THONGKE_SOQUYETDINH_TRANG = "rptNS_DuToan_ThongKe_SoQuyetDinh_Trang";
        public const string RPT_NS_DUTOAN_PHANBO_TONGHOP_DONVI_TO1 = "rptNS_DuToan_PhanBo_TongHop_DonVi_To1.xlsx";
        public const string RPT_NS_DUTOAN_PHANBO_TONGHOP_DONVI_TO1_ONEPAGE = "rptNS_DuToan_PhanBo_TongHop_DonVi_To1_OnePaper.xlsx";
        public const string RPT_NS_DUTOAN_PHANBO_TONGHOP_DONVI_TO2 = "rptNS_DuToan_PhanBo_TongHop_DonVi_To.xlsx";
        public const string RPT_NS_DUTOAN_CHITIEU_NGANH_ALL_AGENCY = "rptNS_DuToan_ChiTieu_Nganh_All_Agency";
        public const string RPT_NS_DUTOAN_CHITIEU_NGANH_ALL_AGENCY_MLNS = "rptNS_DuToan_ChiTieu_Nganh_All_Agency_MLNS";
        public const string RPT_NS_DUTOAN_DIEUCHINH = "rptNS_DuToan_DieuChinh.xlsx";
        public const string RPT_NS_DUTOAN_DIEUCHINH_TONGHOP = "rptNS_DuToan_DieuChinh_TongHop.xlsx";
        public const string RPT_NS_DUTOAN_DIEUCHINH_TONGHOP2 = "rptNS_DuToan_Dieu_Chinh_Tong_Hop.xlsx";
        public const string RPT_NS_DUTOAN_DIEUCHINH_TONGHOP2_ONEPAGE = "rptNS_DuToan_Dieu_Chinh_Tong_Hop_OnePage.xlsx";
        public const string RPT_NS_DUTOAN_THUNOP = "rptNS_DuToan_ThuNop.xlsx";

        public const string RPT_NS_DUTOAN_QD_CONGKHAINGANSACH = "rptNS_DuToan_QdCongKhaiNganSach.xlsx";
        public const string RPT_NS_DUTOAN_QD_CONGKHAINGANSACH_To2 = "rptNS_DuToan_QdCongKhaiNganSach_To2.xlsx";
        public const string RPT_NS_DUTOAN_PAPB_MAUSO1_TOBAOCAO = "rptNS_DuToan_PAPB_MauSo1_ToBaoCao.xlsx";
        public const string RPT_NS_DUTOAN_PAPB_MAUSO1_PHULUCI = "rptNS_DuToan_PAPB_MauSo1_PhuLucI.xlsx";
        public const string RPT_NS_DUTOAN_PAPB_MAUSO1_PHULUCII_TO1 = "rptNS_DuToan_PAPB_MauSo1_PhuLucII_DonViNgang_To1.xlsx";
        public const string RPT_NS_DUTOAN_PAPB_MAUSO1_PHULUCII_TO = "rptNS_DuToan_PAPB_MauSo1_PhuLucII_DonViNgang_To.xlsx";
        public const string RPT_NS_DUTOAN_PAPB_MAUSO1_PHULUCII_THU_TO1 = "rptNS_DuToan_PAPB_MauSo1_PhuLucII_DonViNgang_Thu_To1.xlsx";
        public const string RPT_NS_DUTOAN_PAPB_MAUSO1_PHULUCII_THU_TO = "rptNS_DuToan_PAPB_MauSo1_PhuLucII_DonViNgang_Thu_To.xlsx";
        public const string RPT_NS_DUTOAN_PAPB_MAUSO1_PHULUCII_CHI_TO1 = "rptNS_DuToan_PAPB_MauSo1_PhuLucII_DonViNgang_Chi_To1.xlsx";
        public const string RPT_NS_DUTOAN_PAPB_MAUSO1_PHULUCII_CHI_TO = "rptNS_DuToan_PAPB_MauSo1_PhuLucII_DonViNgang_Chi_To.xlsx";
        public const string RPT_NS_DUTOAN_PAPB_MAUSO1_PHULUCII_TO1_EXCEL = "rptNS_DuToan_PAPB_MauSo1_PhuLucII_DonViNgang_Excel.xlsx";
        public const string RPT_NS_DUTOAN_PAPB_MAUSO1_PHULUCII_TO1_THU_EXCEL = "rptNS_DuToan_PAPB_MauSo1_PhuLucII_DonViNgang_Thu_Excel.xlsx";
        public const string RPT_NS_DUTOAN_PAPB_MAUSO1_PHULUCII_TO1_CHI_EXCEL = "rptNS_DuToan_PAPB_MauSo1_PhuLucII_DonViNgang_Chi_Excel.xlsx";
        public const string RPT_NS_DUTOAN_PAPB_MAUSO2_KETQUA = "rptNS_DuToan_PAPB_MauSo2_KetQua.xlsx";

        //Du toan dau nam
        public const string RPT_NS_DUTOANDAUNAM_SOSANHDONVI = "rptNS_DuToanDauNam_SoSanhDonVi.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_CHINGANSACH = "rptNS_DuToanDauNam_ChiNganSach.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_TONGHOPDUTOAN = "rptNS_DuToanDauNam_TongHopDuToan.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_TONGHOPDUTOAN_PHULUC = "rptNS_DuToanDauNam_TongHopDuToan_PhuLuc.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_TONGHOPDUTOAN_TO2 = "rptNS_DuToanDauNam_TongHopDuToan_To2.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_CHITIETDONVI_PHULUC = "rptNS_DuToanDauNam_ChiTietDonVi_PhuLuc.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_CHITIETDONVI = "rptNS_DuToanDauNam_ChiTietDonVi.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_DUTOANNAM = "rptNS_DuToanDauNam_DuToanNam.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_DUTOANNAMCHITIET = "rptNS_DuToanDauNam_DuToanNamChiTiet.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_TONGHOPDUTOANUOCTHUCHIEN = "rptNS_DuToanDauNam_TongHopDuToanUocThucHien.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_TONGHOPDUTOANUOCTHUCHIEN_PHULUC = "rptNS_DuToanDauNam_TongHopDuToanUocThucHien_PhuLuc.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_TONGHOPDUTOANUOCTHUCHIEN_TO2 = "rptNS_DuToanDauNam_TongHopDuToanUocThucHien_To2.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_DUTOANNAM_All = "rptNS_DuToanDauNam_DuToanNam_All.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_TONGHOP_DACTHU = "rptNS_DuToanDauNam_TongHopDuToanDacThu.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_DUTOANNAMCHITIET_All = "rptNS_DuToanDauNam_DuToanNamChiTiet_All.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_CHITIETDONVI_ALL = "rptNS_DuToanDauNam_ChiTietDonVi_All.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_CHITIETDONVI_ALL_TONGHOP = "rptNS_DuToanDauNam_ChiTietDonVi_All_TongHop.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_CHITIETDONVI_PHULUC_ALL = "rptNS_DuToanDauNam_ChiTietDonVi_PhuLuc_All.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_CHITIETDONVI_PHULUC_ALL_TONGHOP = "rptNS_DuToanDauNam_ChiTietDonVi_PhuLuc_All_TongHop.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_TONGHOPDUTOAN_ALL = "rptNS_DuToanDauNam_TongHopDuToan_All.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_TONGHOPDUTOAN_PHULUC_ALL = "rptNS_DuToanDauNam_TongHopDuToan_PhuLuc_All.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_TONGHOPDUTOAN_PHULUC_ALL_EXCEL = "rptNS_DuToanDauNam_TongHopDuToan_PhuLuc_All_Excel.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_TONGHOPDUTOAN_TO2_ALL = "rptNS_DuToanDauNam_TongHopDuToan_To2_All.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_TONGHOPDUTOAN_PHULUC_ALL_TUCHI_UOCTHUCHIEN = "rptNS_DuToanDauNam_TongHopDuToan_PhuLuc_All_TuChi_UocThucHien.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_TONGHOPDUTOAN_PHULUC_ALL_TUCHI_UOCTHUCHIEN_TO2 = "rptNS_DuToanDauNam_TongHopDuToan_PhuLuc_All_TuChi_UocThucHien_To2.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_TONGHOPDUTOAN_PHULUC_ALL_TUCHI_UOCTHUCHIEN_EXCEL = "rptNS_DuToanDauNam_TongHopDuToan_PhuLuc_All_TuChi_UocThucHien_Excel.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_CHIMUAHANGTAPTRUNG_CAPHIENVAT = "rptNS_DuToanDauNam_ChiMuaHangTapTrungCapHienVat.xlsx";

        public const string EPT_NS_DUTOANDAUNAM_CHUNGTU_TONGHOP_NSSD = "rptNS_DuToanDauNam_ChungTu_TongHop_NSSD.xlsx";
        public const string EPT_NS_DUTOANDAUNAM_CHUNGTU_TONGHOP_NSBD = "rptNS_DuToanDauNam_ChungTu_TongHop_NSBD.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_NGANSACHDACTHU_PHANCAP = "rptNS_DuToanDauNam_NganSachDacThu_PhanCap.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_NGANSACHDACTHU_PHANCAP_ONEPAGE = "rptNS_DuToanDauNam_NganSachDacThu_PhanCap_OnePage_Excel.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_NGANSACHDACTHU_DONVI_DOC_TRANG1 = "rptNS_DuToanDauNam_NganSachDacThu_DonVi_Doc_Trang1.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_NGANSACHDACTHU_DONVI_DOC_TRANG1_EMPTY = "rptNS_DuToanDauNam_NganSachDacThu_DonVi_Doc_Trang1_Empty.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_NGANSACHDACTHU_DONVI_DOC_TRANG = "rptNS_DuToanDauNam_NganSachDacThu_DonVi_Doc_Trang.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_NGANSACHDACTHU_EMPTY = "rptNS_DuToanDauNam_NganSachDacThu_Empty.xlsx";

        public const string RPT_NS_DUTOANDAUNAM_THEONGANH_PHULUC_DONVI_DOC_TO1 = "rptNS_DuToanDauNam_TheoNganh_PhuLuc_DonVi_Doc_Trang1.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_THEONGANH_PHULUC_DONVI_DOC_TO2 = "rptNS_DuToanDauNam_TheoNganh_PhuLuc_DonVi_Doc_Trang.xlsx";

        public const string RPT_NS_DUTOANDAUNAM_THEONGANH_PHULUC_TO1 = "rptNS_DuToanDauNam_TheoNganh_PhuLuc_Trang1.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_THEONGANH_PHULUC_TO2 = "rptNS_DuToanDauNam_TheoNganh_PhuLuc_Trang.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_THEONGANH_PHULUC_EMPTY = "rptNS_DuToanDauNam_TheoNganh_PhuLuc_Empty.xlsx";


        public const string RPT_NS_DUTOANDAUNAM_THEODONVI = "rptNS_TN_DTDN_TongHop_DonVi.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_ONEPAGE = "rptNS_TN_DTDN_TongHop_OnePage.xlsx";
        public const string RPT_NS_DUTOANDAUNAM_MULTIPAGE = "rptNS_TN_DTDN_TongHop_MultiPage.xlsx";
        public const string RPT_NS_DUTOAN_PHUONG_AN_ONEPAGE = "rptNS_DuToan_Thu_Nop_Hd4554_OnePage.xlsx";
        public const string RPT_NS_THUNOP_NGANSACH_PHANBO_DUTOAN_DONVI = "rptNS_ThuNop_NganSach_PhanBo_DuToan.xlsx";

        //Nganh tham dinh
        public const string RPT_NS_NGANHTHAMDINH_CHUNGTU_TONGHOP = "rptNS_NganhThamDinh_ChungTu_TongHop.xlsx";
        public const string RPT_NS_NGANHTHAMDINH_CHUNGTU_TONGHOP_NSBD = "rptNS_NganhThamDinh_ChungTu_TongHop_NSBD.xlsx";
        public const string RPT_NS_NGANHTHAMDINH_CHUNGTU_TONGHOP_NTD = "rptNS_NganhThamDinh_ChungTu_TongHop_NTD.xlsx";
        public const string RPT_NS_NGANHTHAMDINH_CHUNGTU_TONGHOP_NSBD_NTD = "rptNS_NganhThamDinh_ChungTu_TongHop_NSBD_NTD.xlsx";
        public const string RPT_NS_NGANHTHAMDINH_CHUNGTUCTC = "rptNS_NganhThamDinh_ChungTuCTC.xlsx";
        public const string RPT_NS_NGANHTHAMDINH_CHUNGTUCTC_DONVI = "rptNS_NganhThamDinh_ChungTuCTC_DonVi.xlsx";
        public const string RPT_NS_NGANHTHAMDINH_CHUNGTUCTC_NSBD_DONVI = "rptNS_NganhThamDinh_ChungTuCTC_NSBD_DonVi.xlsx";
        public const string RPT_NS_NGANHTHAMDINHDATAKIEMTRA_TRANG1 = "rptNS_NganhThamDinhDataKiemTra_Trang1.xlsx";
        public const string RPT_NS_NGANHTHAMDINHDATAKIEMTRA_TRANG = "rptNS_NganhThamDinhDataKiemTra_Trang.xlsx";
        public const string RPT_NS_NGANHTHAMDINHDATAKIEMTRA_TRANG2 = "rptNS_NganhThamDinhDataKiemTra_Trang_.xlsx";
        public const string RPT_NS_NGANHTHAMDINH_CHUNGTUNTD_NSBD = "rptNS_NganhThamDinh_ChungTuNTD_NSBD.xlsx";
        public const string RPT_NS_NGANHTHAMDINH_CTC_DATAKIEMTRA_TRANG1 = "rptNS_NganhThamDinh_CTC_DataKiemTra_Trang1.xlsx";
        public const string RPT_NS_NGANHTHAMDINH_CTC_DATAKIEMTRA_TRANG = "rptNS_NganhThamDinh_CTC_DataKiemTra_Trang.xlsx";
        //Phan cap ngan sach nganh
        public const string EPT_NS_PHANCAPNGANSACHNGANH_CHUNGTU = "rptNS_PhanCapNganSachNganh_ChungTu.xlsx";

        //Cap phat
        public const string RPT_CAPPHAT_CHUNGTU_EXPORT = "rptNS_CapPhat_ChungTu.xlsx";
        public const string RPT_NS_CAPPHAT_SOSANH_CHITIET_DONVI = "rptNS_CapPhat_SoSanh_ChiTiet_DonVi";
        public const string RPT_NS_CAPPHAT_SOSANH_TONGHOP_DONVI = "rptNS_CapPhat_SoSanh_TongHop_DonVi";
        public const string RPT_NS_CAPPHAT_SOSANH_LNS = "rptNS_CapPhat_SoSanh_LNS";
        public const string RPT_NS_CAPPHAT_REQUEST_DONVI = "rptNS_CapPhat_Request_DonVi";
        public const string RPT_NS_CAPPHAT_REQUEST_LNS = "rptNS_CapPhat_Request_LNS";
        public const string RPT_NS_CAPPHAT_THONGTRI_MACDINH = "rptNS_CapPhat_ThongTri_MacDinh.xlsx";
        public const string RPT_NS_CAPPHAT_THONGTRI_NHIEUDONVI = "rptNS_CapPhat_ThongTri_NhieuDonVi.xlsx";
        public const string RPT_NS_CAPPHAT_LOAICAP_CHITIET_DONVI = "rptNS_CapPhat_LoaiCap_ChiTiet_DonVi";
        public const string RPT_NS_CAPPHAT_LOAICAP_TONGHOP_DONVI = "rptNS_CapPhat_LoaiCap_TongHop_DonVi";
        public const string RPT_NS_CAPPHAT_LOAICAP_LNS = "rptNS_CapPhat_LoaiCap_LNS";
        public const string RPT_NS_CAPPHAT_DONVI_TO1 = "rptNS_CapPhat_DonVi";
        public const string RPT_NS_CAPPHAT_DONVI_TO2 = "rptNS_CapPhat_DonVi_To2";
        public const string RPT_NS_CAPPHAT_DONVI_EMPTY = "rptNS_CapPhat_DonVi_Empty";
        public const string RPT_NS_CAPPHAT_DONVI_EXCEL = "rptNS_CapPhat_DonVi_Excel";

        // xuat excel import
        // so nhu cau
        public const string RPT_NS_SOKIEMTRA_CHUNGTU_TONGHOP_NSSD = "rptNS_SoKiemTra_ChungTu_TongHop_NSSD.xlsx";
        public const string RPT_NS_SOKIEMTRA_CHUNGTU_TONGHOP_NSBD = "rptNS_SoKiemTra_ChungTu_TongHop_NSBD.xlsx";
        public const string RPT_NS_SOKIEMTRA_CHUNGTU_PHANBO_NSSD = "rptNS_SoKiemTra_ChungTu_PhanBo_NSSD.xlsx";
        public const string RPT_NS_SOKIEMTRA_CHUNGTU_PHANBO_NSBD = "rptNS_SoKiemTra_ChungTu_PhanBo_NSBD.xlsx";

        //VDT
        // Ke hoach chi
        public const string RPT_VDT_NC_NHUCAUCHI = "rpt_BaoCaoNhucauKeHoachChiQuy.xlsx";

        //Quyet Toan
        public const string RPT_NS_QUYETTOAN_TOBIA = "rptNS_QuyetToan_ToBia.xlsx";
        public const string RPT_NS_QUYETTOAN_CHUNGTU = "rptNS_QuyetToan_ChungTu.xlsx";
        public const string RPT_NS_QUYETTOAN_CHUNGTU_GIAITHICH_LOI = "rptNS_QuyetToan_ChungTu_GiaiThich_Loi.xlsx";
        public const string RPT_NS_QUYETTOAN_CHUNGTU_GIAITHICH_SO = "rptNS_QuyetToan_ChungTu_GiaiThich_So.xlsx";

        public const string RPT_NS_QUYETTOAN_THONGTRI_LNS = "rptNS_QuyetToan_ThongTri_LNS.xlsx";
        public const string RPT_NS_QUYETTOAN_THONGTRI_LNS_LNS = "rptNS_QuyetToan_ThongTri_LNS_lns.xlsx";
        public const string RPT_NS_QUYETTOAN_THONGTRI_LNS_M = "rptNS_QuyetToan_ThongTri_LNS_m.xlsx";
        public const string RPT_NS_QUYETTOAN_THONGTRI_LNS_TM = "rptNS_QuyetToan_ThongTri_LNS_tm.xlsx";

        public const string RPT_NS_QUYETTOAN_THONGTRI_DONVI = "rptNS_QuyetToan_ThongTri_DonVi.xlsx";

        public const string RPT_NS_QUYETTOAN_THUONGXUYEN_TONGHOP = "rptNS_QuyetToan_ThuongXuyen_TongHop.xlsx";
        public const string RPT_NS_QUYETTOAN_THUONGXUYEN_TONGHOP_GIAITHICH_LOI = "rptNS_QuyetToan_ThuongXuyen_TongHop_GiaiThich_Loi.xlsx";
        public const string RPT_NS_QUYETTOAN_THUONGXUYEN_TONGHOP_GIAITHICH_SO = "rptNS_QuyetToan_ThuongXuyen_TongHop_GiaiThich_So.xlsx";

        public const string RPT_NS_QUYETTOAN_QUOCPHONG_TONGHOP = "rptNS_QuyetToan_QuocPhong_TongHop.xlsx";
        public const string RPT_NS_QUYETTOAN_QUOCPHONG_TONGHOP_TATCA = "rptNS_QuyetToan_QuocPhong_TongHop_TatCa.xlsx";
        public const string RPT_NS_QUYETTOAN_QUOCPHONG_TONGHOP_TATCA_UPDATE = "rptNS_QuyetToan_QuocPhong_TongHop_TatCa_Update.xlsx";
        public const string RPT_NS_QUYETTOAN_QUOCPHONG_TONGHOP_NGAYNGUOI = "rptNS_QuyetToan_QuocPhong_TongHop_NgayNguoi.xlsx";
        public const string RPT_NS_QUYETTOAN_QUOCPHONG_TONGHOP_GIAITHICH_LOI = "rptNS_QuyetToan_QuocPhong_TongHop_GiaiThich_Loi.xlsx";
        public const string RPT_NS_QUYETTOAN_QUOCPHONG_TONGHOP_SONGUOI_SONGAY = "rptNS_QuyetToan_QuocPhong_TongHop_SoNgay_SoNguoi.xlsx";
        public const string RPT_NS_QUYETTOAN_QUOCPHONG_TONGHOP_SONGUOI_SONGAY_TATCA = "rptNS_QuyetToan_QuocPhong_TongHop_SoNgay_SoNguoi_TatCa.xlsx";

        public const string RPT_NS_QUYETTOAN_DUTOAN = "rptNS_QuyetToan_DuToan.xlsx";
        public const string RPT_NS_QUYETTOAN_DUTOAN_THANG = "rptNS_QuyetToan_DuToan_Thang.xlsx";
        public const string RPT_NS_QUYETTOAN_DUTOAN_QUY = "rptNS_QuyetToan_DuToan_Quy.xlsx";
        public const string RPT_NS_QUYETTOAN_DUTOAN_TONGHOP_THANG = "rptNS_QuyetToan_DuToan_TongHop_Thang.xlsx";

        public const string RPT_NS_QUYETTOAN_NAM_LNS = "rptNS_QuyetToan_Nam_LNS.xlsx";
        public const string RPT_NS_QUYETTOAN_TONGHOP_8063 = "rptNS_QuyetToan_TongHop_8063.xlsx";
        public const string RPT_NS_QUYETTOAN_TONGHOP_6789 = "rptNS_QuyetToan_TongHop_6789.xlsx";
        public const string RPT_NS_QUYETTOAN_TONGHOP_8568 = "rptNS_QuyetToan_TongHop_8568.xlsx";
        public const string RPT_NS_QUYETTOAN_NAM_LNS_TNG = "rptNS_QuyetToan_Nam_LNS_TNG.xlsx";
        public const string RPT_NS_QUYETTOAN_NAM_LNS_UPDATE = "rptNS_QuyetToan_Nam_LNS_Update.xlsx";
        public const string RPT_NS_QUYETTOAN_NHAN_KINHPHI = "rptNS_Nhan_QuyetToan_KinhPhi.xlsx";
        public const string RPT_NS_QUYETTOAN_NAM_LNS_UPDATE1 = "rptNS_QuyetToan_Nam_LNS_Update1.xlsx";
        public const string RPT_NS_QUYETTOAN_NAM_LNS_UPDATE2 = "rptNS_QuyetToan_Nam_LNS_Update2.xlsx";
        public const string RPT_NS_QUYETTOAN_NAM_LNS_UPDATE1_EMPTY = "rptNS_QuyetToan_Nam_LNS_Update1_Empty.xlsx";

        public const string RPT_NS_QUYETTOAN_CHUNGTU_TONGHOP = "rptNS_QuyetToan_ChungTu_TongHop.xlsx";
        public const string RPT_NS_QUYETTOAN_TONGHOP_DONVI = "rptNS_QuyetToan_TongHop_DonVi";
        public const string RPT_NS_QUYETTOAN_TONGHOP_DONVI_EXCEL = "rptNS_QuyetToan_TongHop_DonVi_Excel";
        public const string RPT_NS_QUYETTOAN_TONGHOP_DONVI_ONEPAPER = "rptNS_QuyetToan_TongHop_DonVi_OnePaper";
        public const string RPT_NS_QUYETTOAN_TONGHOP_DONVI_TRANG = "rptNS_QuyetToan_TongHop_DonVi_Trang";
        public const string RPT_NS_QUYETTOAN_TONGHOP_DONVI_TRANG_ONEPAPER = "rptNS_QuyetToan_TongHop_DonVi_Trang_OnePaper";
        public const string RPT_NS_QUYET_TOAN_QD_CONGKHAINGANSACH = "rptNS_QuyetToan_QdCongKhaiNganSach_To1.xlsx";
        public const string RPT_NS_QUYET_TOAN_QD_CONGKHAINGANSACH_To2 = "rptNS_QuyetToan_QdCongKhaiNganSach_To2.xlsx";
        public const string RPT_NS_QUYET_TOAN_CONGKHAI_THUCHI = "rptNS_QuyetToan_CongKhai_ThuChi.xlsx";
        public const string RPT_NS_QUYET_TOAN_CONGKHAI_THUCHI_04a = "rptNS_QuyetToan_CongKhai_ThuChi_04a.xlsx";
        public const string RPT_NS_QUYET_TOAN_CONGKHAI_THUCHI_04a_EXCEL = "rptNS_QuyetToan_CongKhai_ThuChi_04a_Excel.xlsx";
        public const string RPT_NS_QUYET_TOAN_CONGKHAI_THUCHI_04b = "rptNS_QuyetToan_CongKhai_ThuChi_04b.xlsx";

        public const string RPT_NS_BANGKE_CHUNGTU = "rptNS_BangKe_ChungTu.xlsx";

        public const string RPT_NS_BANGKE_TONGHOP = "rptNS_BangKe_TongHop.xlsx";
        public const string RPT_NS_BANGKE_TONGHOP_DONVI = "rptNS_BangKe_TongHop_DonVi.xlsx";
        public const string RPT_NS_BANGKE_TONGHOP_TUCHI = "rptNS_BangKe_TongHop_TuChi.xlsx";
        public const string RPT_NS_BANGKE_TONGHOP_HIENVAT = "rptNS_BangKe_TongHop_HienVat.xlsx";

        public const string RPT_NS_QUANSO_BINHQUAN_A3 = "rptNS_QuanSo_BinhQuan_A3.xlsx";
        public const string RPT_NS_QUANSO_BINHQUAN_A4 = "rptNS_QuanSo_BinhQuan_A4.xlsx";
        public const string RPT_NS_QUANSO_BINHQUAN_TONGHOP_A3 = "rptNS_QuanSo_BinhQuan_TongHop_A3.xlsx";
        public const string RPT_NS_QUANSO_BINHQUAN_TONGHOP_A4 = "rptNS_QuanSo_BinhQuan_TongHop_A4.xlsx";
        public const string RPT_NS_QUANSO_DONVI = "rptNS_QuanSo_DonVi.xlsx";
        public const string RPT_NS_QUANSO_DONVI_2 = "rptNS_QuanSo_DonVi_2.xlsx";
        public const string RPT_NS_QUANSO_DONVI_3 = "rptNS_QuanSo_DonVi_3.xlsx";
        public const string RPT_NS_QUANSO_DONVI_CHITIET = "rptNS_QuanSo_DonVi_ChiTiet.xlsx";
        public const string RPT_NS_QUANSO_DONVI_CHITIET_2 = "rptNS_QuanSo_DonVi_ChiTiet_2.xlsx";
        public const string RPT_NS_QUANSO_DONVI_QUY = "rptNS_QuanSo_DonVi_Quy.xlsx";
        public const string RPT_NS_QUANSO_DONVI_QUY_2 = "rptNS_QuanSo_DonVi_Quy_2.xlsx";
        public const string RPT_NS_QUANSO_DONVI_NAM = "rptNS_QuanSo_DonVi_Nam.xlsx";
        public const string RPT_NS_QUANSO_DONVI_NAM_2 = "rptNS_QuanSo_DonVi_Nam_2.xlsx";
        public const string RPT_NS_QUANSO_DONVI_TONGHOP = "rptNS_QuanSo_DonVi_TongHop.xlsx";
        public const string RPT_NS_QUANSO_DONVI_TONGHOP_2 = "rptNS_QuanSo_DonVi_TongHop_2.xlsx";
        public const string RPT_NS_QUANSO_DONVI_TONGHOP_LIENTHAM = "rptNS_QuanSo_DonVi_TongHop_LienTham.xlsx";
        public const string RPT_NS_QUANSO_DONVI_TONGHOP_LIENTHAM_2 = "rptNS_QuanSo_DonVi_TongHop_LienTham_2.xlsx";
        public const string RPT_NS_QUANSO_LIENTHAM = "rptNS_QuanSo_LienTham.xlsx";
        public const string RPT_NS_QUANSO_RAQUAN = "rptNS_QuanSo_RaQuan.xlsx";
        public const string RPT_NS_QUANSO_TANGGIAM = "rptNS_QuanSo_TangGiam.xlsx";
        public const string RPT_NS_QUANSO_TANGGIAM_TONGHOP_DONVI = "rptNS_QuanSo_TangGiam_TongHop_DonVi.xlsx";
        public const string RPT_NS_QUANSO_THUONGXUYEN = "rptNS_QuanSo_ThuongXuyen.xlsx";

        //Thuc hien du an
        public const string RPT_VDT_TINHHINHTHUCHIENDUAN = "rptVDT_TinhHinhThucHienDuAn.xlsx";
        public const string RPT_NH_DANHMUC_HOPDONG = "TemplateHopDongTrongNuoc.xlsx";
        public const string RPT_NH_DANHMUC_HOPDONG_DUAN = "TemplateHopDongTrongNuoc_DuAn.xlsx";
        public const string RPT_GOITHAU = "TemplateGoiThau.xlsx";
        public const string RPT_NGUONVON = "TemplateNguonVon.xlsx";
        public const string RPT_HANGMUC = "TemplateHangMuc.xlsx";
        public const string RPT_NH_DANHMUC_HOPDONGNT = "TemplateHopDongNgoaiThuong.xlsx";

        public const string RPT_VDT_DA_THONGTINDUAN = "tmp_Vdt_Da_ThongTinDuAn.xlsx";

        public const string RPT_IMPORT_GOITHAU_DUAN = "Import-Goithau-DuAn.xlsx";
        public const string RPT_IMPORT_HOPDONG_DUAN = "Import-HopDong-DuAn.xlsx";

        //Cap Phat Thanh Toan
        public const string RPT_VDT_CAPPHATTHANHTOANTHANHTOAN = "rptVDT_CapPhatThanhToanThanhToanNew.xlsx";
        public const string RPT_VDT_CAPPHATTHANHTOAN = "rptVDT_CapPhatThanhToan.xlsx";
        public const string RPT_VDTQUANLYCAPPHATTHANHTOANTHANHTOAN = "rptVDT_QuanLyCapPhatThanhToanThanhToan.xlsx";
        public const string RPT_VDTQUANLYCAPPHATTHANHTOANTHANHTOAN_NODATA = "rptVDT_QuanLyCapPhatThanhToanThanhToanNodata.xlsx";
        public const string RPT_VDT_CAPPHATTHANHTOAN_NODATA = "rptVDT_QuanLyCapPhatThanhToanNodata.xlsx";
        public const string RPT_VDT_QUANLYCAPPHATTHANHTOAN = "rptVDT_QuanLyCapPhatThanhToan.xlsx";
        public const string RPT_VDT_DNTHANHTOAN_RUTDUTOAN = "rpt_vdt_ThanhToan_GiayRutDuToan.xls";

        //Thuc Hien dau tu
        public const string RPT_VDT_KETQUA_GIAINGAN_CHIKHINHPHIDAUTU = "rptVDT_KetQua_GiaiNgan_ChiKinhPhiDauTu.xlsx";

        //Ket thuc dau tu
        public const string RPT_VDT_QUYETTOANHOANTHANH = "rptVDT_QuyetToanHoanThanh.xlsx";
        public const string RPT_VDT_TONGHOPQUYETTOANDUANHOANTHANH = "rptVDT_TongHopQuyetToanDuAnHoanThanh.xlsx";
        public const string EPT_VDT_TONGHOPDENGHIQUYETTOAN = "eptVDT_TongHopDeNghiQuyetToan.xlsx";
        public const string EPT_VDT_TONGHOPPHEDUYETQUYETTOAN = "eptVDT_TongHopPheDuyetQuyetToan.xlsx";

        public const string RPT_VDT_TONGHOPQUYETTOANDUANHOANTHANH_PHULUC = "rptVDT_TongHopQuyetToanDuAnHoanThanhPhuLuc.xlsx";

        //Phe duyet thanh toan
        public const string TMP_VDT_PHEDUYETTHANHTOANCHITIET = "tmp_vdt_PheDuyetThanhToanChiTiet.xlsx";
        public const string TMP_VDT_DENGHITHANHTOAN = "tmp_vdt_DeNghiThanhToan.xlsx";
        public const string tmp_vdt_DeNghiThanhToanTongHop_NSQP = "tmp_vdt_DeNghiThanhToanTongHop_NSQP.xlsx";

        // Thong tri cap phat
        public const string RPT_VDT_THONGTRI_THANHTOAN = "rpt_vdt_thongtri_thanhtoan_02.xlsx";
        public const string RPT_VDT_THONGTRI_THUHOIUNG = "rpt_vdt_thongtri_thuhoitamung.xlsx";
        public const string RPT_VDT_THONGTRI_TAMUNG = "rpt_vdt_thongtri_tamung_02.xlsx";
        public const string RPT_VDT_THONGTRI_HOPTHUC = "rpt_vdt_thongtri_hopthuc_02.xlsx";
        public const string RPT_VDT_THONGTRI_KINHPHI = "rpt_vdt_thongtri_kinhphi_02.xlsx";

        // VDT - Quyet Toan
        public const string RPT_VDT_QUYETTOANNIENDO_VONNAM = "rpt_vdt_quyettoanniendo_vonnam.xlsx";
        public const string RPT_VDT_QUYETTOANNIENDO_VONNAM_NSQP = "rpt_vdt_quyettoanniendo_vonnam_nsqp.xlsx";
        public const string RPT_VDT_QUYETTOANNIENDO_VONNAM_PHANTICH = "rpt_vdt_quyettoanniendo_vonnam_phantich.xlsx";
        public const string RPT_VDT_QUYETTOANNIENDO_VONUNG = "rpt_vdt_quyettoanniendo_vonung.xlsx";

        public const string EPT_VDT_TONGHOPKHOITAO = "rptVDT_TongHopKhoiTao.xlsx";

        public const string RPT_VDT_THONGTRI_QUYETTOAN = "rptVdt_ThongTriQuyetToan.xlsx";

        public const string RPT_DM_MLNS = "rpt_dm_mlns.xlsx";

        // Ngoai Hoi 
        public const string TEMPLATE_IMPORT_KHOITAO = "TemplateImportKhoiTaoChiTiet.xlsx";
        public const string RPT_NH_CAPPHAT_DENGHITHANHTOAN = "rptNH_CapPhat_DeNghiThanhToan.xlsx";
        public const string RPT_NH_CAPPHAT_PHEDUYETTHANHTOAN = "rptNH_CapPhat_PheDuyetThanhToan.xlsx";
        public const string RPT_NGOAIHOI_THONGTRI_CAPPHAT = "rptNgoaiHoi_ThongTri_CapPhat.xlsx";
        public const string RPT_NGOAIHOI_PHEDUYET_QUYETTOAN_DAHT = "rptNgoaiHoi_PheDuyet_QuyetToan_Daht.xlsx";
        public const string RPT_NGOAIHOI_PHEDUYET_QUYETTOAN_DAHTTO2 = "rptNgoaiHoi_PheDuyet_QuyetToan_DahtTo2.xlsx";
        public const string RPT_BAOCAO_KETLUAN_QUYETTOAN = "rptBaoCaoKetLuanQuyetToan.xlsx";
        public const string RPT_BAOCAO_KETLUAN_QUYETTOANTO2 = "rptBaoCaoKetLuanQuyetToanTo2.xlsx";

        public const string RPT_NGOAIHOI_THUCHIEN_NGANSACH = "rptNgoaiHoi_ThucHienNganSach.xlsx";
        public const string RPT_NGOAIHOI_THUCHIEN_NGANSACH_GIAIDOAN = "rptNgoaiHoi_ThucHienNganSachGiaiDoan.xlsx";
        public const string RPT_NGOAIHOI_THUCHIEN_NGANSACH_GIAIDOAN_TO = "rptNgoaiHoi_ThucHienNganSachGiaiDoanTo.xlsx";
        public const string RPT_NGOAIHOI_THUCHIEN_NGANSACH_GIAIDOAN_TO1 = "rptNgoaiHoi_ThucHienNganSachGiaiDoanTo1.xlsx";
        public const string RPT_NGOAIHOI_THUCHIEN_NGANSACH_GIAIDOAN_TO2 = "rptNgoaiHoi_ThucHienNganSachGiaiDoanTo2.xlsx";
        public const string RPT_NH_TINHHINHTHUCHIENDUAN = "rptNH_TinhHinhThucHienDuAn.xlsx";
        public const string RPT_NH_TONGHOP_TT_DUAN = "rptNHTongHopThongTinDuAn.xlsx";
        public const string RPT_NH_NHUCAU_CHIQUY = "rpt_Nhucauchiquy_BaoCao.xlsx";
        public const string TEMPATE_NH_DANHMUC_NHATHAU = "TemplateNH_DanhMuc_NhaThau.xlsx";
        public const string RPT_NH_TONGHOPQUYETTOANDUANHOANTHANH = "rptNH_TongHopQuyetToanDuAnHoanThanh.xlsx";
        public const string RPT_NH_TONGHOPQUYETTOANDUANHOANTHANH_PHULUC = "rptNH_TongHopQuyetToanDuAnHoanThanhPhuLuc.xlsx";
        public const string TEMPATE_NH_THONGTIN_DUAN = "tmp_NH_Da_ThongTinDuAn.xlsx";
        public const string TEMPATE_NH_THONGTIN_DUAN_CTC = "tmp_NH_Da_ThongTinDuAn_CTC.xlsx";
        public const string PRT_NH_CHENHLECH_TIGIAHOIDOAI = "rptNH_ChenhLechTiGiaHoiDoai.xlsx";
        public const string PRT_NH_HOPDONGTRONGNUOC_KHONGGOITHAU = "Template_NH_HD_HopDongTrongNuoc_KhongGoiThau.xlsx";
        public const string EPT_NH_KHOITAOCAPPHAT = "eptNH_KhoiTaoCapPhat.xlsx";
        public const string PRT_NH_BAOCAOTAISAN = "rptNH_BaoCaoTaiSan.xlsx";


        // Quyết toán
        public const string RPT_NH_QUYETTOAN_QUYETTOAN_NIENDO_QUY = "rptNH_QuyetToan_NienDo_Quy.xlsx";
        public const string RPT_NH_QUYETTOAN_QUYETTOAN_NIENDO_NAM = "rptNH_QuyetToan_NienDo_Nam.xlsx";
        public const string RPT_NH_QUYETTOAN_QUYETTOAN_NIENDO_GIAIDOAN = "rptNH_QuyetToan_NienDo_GiaiDoan.xlsx";
        public const string EPT_NH_DANHMUC_NHATHAU = "TemplateNH_DanhMuc_NhaThau.xlsx";
        public const string RPT_NH_THONG_TRI_QUYET_TOAN = "rptNH_ThongTriQuyetToan.xlsx";

        // Hợp đồng ngoại thương
        public const string RPT_FOREX_CONTRACT = "rpt_Forex_Contract.xlsx";
        public const string RPT_Hop_Dong_Ngoai_Thuong_CucTaiChinh = "HopDongNgoaiThuong_CucTaiChinh.xlsx";

        // Bảo hiểm
        public const string RPT_BH_KHT_CHUNGTU_CHITIET_BHXH = "rptBH_KHT_ChungTu_ChiTiet_BHXH.xlsx";
        public const string RPT_BH_KHC_CHUNGTU_CHITIET_BHXH = "rptBH_KHC_ChungTu_ChiTiet_BHXH.xlsx";
        public const string PRT_BH_KHT_ChungTu_ChiTiet_PhuLuc_KHT_BHXH_Doc = "rptBH_KHT_ChungTu_ChiTiet_PhuLuc_KHT_BHXH.xlsx";
        public const string PRT_BH_KHT_DU_TOAN_THU_BHXH = "rptBH_KHT_Du_Toan_Thu_BHXH.xlsx";
        public const string PRT_BH_KHT_DU_TOAN_THU_BHTN = "rptBH_KHT_Du_Toan_Thu_BHTN.xlsx";
        public const string PRT_BH_KHT_DU_TOAN_THU_BHYT_QUAN_NHAN = "rptBH_KHT_Du_Toan_Thu_BHYT_Quan_Nhan.xlsx";
        public const string PRT_BH_KHT_DU_TOAN_THU_BHYT_NLD = "rptBH_KHT_Du_Toan_Thu_BHYT_NLD.xlsx";
        public const string IMPORT_BH_KHC_CHUNGTU_CHITIET_BHXH = "importBH_KHC_ChungTu_ChiTiet_BHXH.xlsx";
        public const string PRT_BH_KHC_CHUNGTU_CHITIET_PHULUC_KCT_BHXH_DOC = "rptBH_KHC_ChungTu_ChiTiet_PhuLuc_KHC_BHXH";
        public const string PRT_BH_KHC_CHUNGTU_TONGHOP_PHULUC_KCT_BHXH_DOC = "rptBH_KHC_ChungTu_TongHop_PhuLuc_KHC_BHXH";
        public const string RPT_BH_NDT_THONGBAOCAPCHITIEUNGANSACH_DOC = "rptBH_NDT_ThongBaoCapChiTieuNganSach.xlsx";
        public const string RPT_BH_THONGTRICAPTAMUNGKINHPHIKCBBHYT_DOC = "rptBH_ThongTriCapTamUngKinhPhiKCBBHYT_Doc.xlsx";
        public const string RPT_BH_TONGHOPTHONGTRICAPTAMUNGKINHPHIKCBBHYT_DOC = "rptBH_TongHopThongTriCapTamUngKinhPhiKCBBHYT_Doc.xlsx";
        public const string RPT_BH_KEHOACHCAPTAMUNGQNKCHBHYT_DOC = "rptBH_KeHoachCapTamUngQNKCBBHYT_Doc.xlsx";
        public const string RPT_BH_KEHOACHCAPTAMUNGQNNLDKCHBHYT_DOC = "rptBH_KeHoachCapTamUngQNNLDKCBBHYT_Doc.xlsx";
        public const string RP_BH_EXPORT_CAPPHATTAMUNGKCBHUYT = "rpt_BH_CapPhatTamUngKCBBHYT.xlsx";
        public const string RP_BH_EXPORT_CAPPHATTAMUNGKCBBHYT = "rpt_BH_CapPhatTamUngKCBBHYT_export.xlsx";
        public const string RP_BH_EXPORT_QUYETTOANNAM_BAOCAOQUYETTOANCHIBHXH = "rpt_BH_BaoCaoQuyetToanChiBHXH_Doc.xlsx";
        public const string RP_BH_EXPORT_QUYETTOANNAM_QUYETTOANCHIBHXH = "rpt_BH_QuyetToanChiBHXH_Doc.xlsx";
        public const string RPT_BHXH_DU_TOAN_THU_BHXH_BHYT_BHTN = "rptBHXH_DU_TOAN_THU_BHXH_BHYT_BHTN.xlsx";
        public const string RPT_BHXH_PHUONGAN_DU_TOAN_CHI_GOP_KQPL = "rptBHXH_PhuongAn_DuToan_GopKPQL.xlsx";
        public const string RPT_BHXH_PHUONGAN_DU_TOAN_CHI_TACH_KQPL = "rptBHXH_PhuongAn_DuToan_TachKPQL.xlsx";
        public const string RPT_BHXH_CHITIET_NOIDUNG_KPQL = "rptBHXH_DTPBC_NoiDung_KPQL.xlsx";
        public const string RPT_BH_QTC_NHAN_VA_QTKP = "rptBH_QTC_Nhan_QT_KinhPhi";


        public const string RP_BH_EXPORT_QUYETTOANQUY_BAOCAOQUYETTOANCHIBHXH = "rpt_BH_BaoCaoQuyetToanChiQuyBHXH_Doc.xlsx";
        public const string RP_BH_EXPORT_QUYETTOANQUY_BAOCAOQUYETTOANCHIBHXHSOLIEU = "rpt_BH_BaoCaoQuyetToanChiQuyBHXHSolieu_Doc.xlsx";
        public const string RP_BH_EXPORT_QUYETTOANQUY_GIAITHICHTROCAPOMDAU04A = "rpt_BH_BaoCaoQuyetToanGiaiThichTroCapOmDau_Doc.xlsx";
        public const string RP_BH_EXPORT_QUYETTOANQUY_GIAITHICHTROCAPTHAISAN04B = "rpt_BH_BaoCaoQuyetToanGiaiThichTroCapThaiSan_Doc.xlsx";
        public const string RP_BH_EXPORT_QUYETTOANQUY_GIAITHICHTROCAPTAINANNGHENGHIEP = "rpt_BH_BaoCaoQuyetToanGiaiThichTroCapTaiNan_Doc.xlsx";
        public const string RP_BH_EXPORT_QUYETTOANQUY_GIAITHICHTROCAPHUUTRIPHUCVU = "rpt_BH_BaoCaoQuyetToanGiaiThichTroCapHTPVXNTVTT_Doc.xlsx";
        public const string RP_BH_EXPORT_QUYETTOANQUY_THONGTRIXACNHANQUYETTOANQUYBHXH = "rpt_BH_BaoCaoQuyetToanThongTriQuyBHXH_Doc.xlsx";
        public const string RP_BH_EXPORT_QUYETTOANQUY_THONGTRIXACNHANQUYETTOANQUYTHEOLOAICHIBHXH = "rpt_BH_BaoCaoQuyetToanThongTriQuyTheoLoaiChiBHXH_Doc.xlsx";
        public const string RP_BH_EXPORT_QUYETTOANQUY_DANH_SACH_NLD_NGHI_VIEC = "rptBH_BaoCaoQuyetToanCheDoOmDau.xlsx";

        public const string RP_BH_EXPORT_QUYETTOANQUYKCB_BAOCAOTHONGTRITHEODONVIKCBQUANY = "rptBH_QTC_QKCB_Thongtri_DonVi_Doc.xlsx";
        public const string RP_BH_EXPORT_QUYETTOANQUYKCB_BAOCAOQUYETTOANCHIKCBQUANY = "rpt_BH_BaoCaoQuyetToanChiKInhPhiKCBQuanY_Doc.xlsx";
        public const string RP_BH_EXPORT_QUYETTOANQUYKCB_BAOCAOQUYETTOANCHIKCBQUANYSOLIEU = "rpt_BH_BaoCaoQuyetToanChiKInhPhiKCBQuanYSoLieu_Doc.xlsx";
        public const string RP_BH_EXPORT_QUYETTOANQUY_THONGTRIXACNHANQUYETTOANQUYKCBQUANY = "rpt_BH_BaoCaoQuyetToanThongTriQuyKCBQuanY_Doc.xlsx";
        public const string RP_BH_EXPORT_QUYETTOANNAM_BAOCAOQUYETTOANCHIKCBQUANYDONVI = "rpt_BH_BaoCaoQuyetToanNamKCBQuanYDonVi_Doc.xlsx";
        public const string RP_BH_EXPORT_QUYETTOANNAM_PHHULUCBAOCAOQUYETTOANNAMKCBQUANYDONVI = "rpt_BH_PhuLucBaoCaoQuyetToanNamKCBQuanYDonVi_Doc.xlsx";
        public const string RP_BH_EXPORT_QUYETTOANQUYKCB_BAOCAOTHONGTRITHEODONVIBHXH = "rptBH_QTC_QBHXH_Thongtri_DonVi_Doc.xlsx";
        public const string RP_BH_EXPORT_QUYETTOANQUYKCB_BAOCAOTHONGTRITHEOLOAICHIBHXH = "rptBH_QTC_QBHXH_Thongtri_LoaiChi.xlsx";
        public const string RP_BH_EXPORT_QUYETTOANQUYKCB_BAOCAOQUYETTOANCHIKCBQUANYTHC = "rpt_BH_QuyetToan_Qkcb_TongHopChi_Doc.xlsx";

        public const string RPT_BH_LUONG_TRO_CAP_OM_DAU = "rptBH_Luong_Tro_Cap_Om_Dau.xlsx";
        public const string RPT_BH_LUONG_TRO_CAP_OM_DAU_IN_SO_THUC_NHAN = "rptBH_Luong_Tro_Cap_Om_Dau_InSoThucNhan.xlsx";
        public const string RPT_BH_LUONG_TRO_CAP_OM_DAU_DON_VI = "rptBH_Luong_Tro_Cap_Om_Dau_DonVi.xlsx";
        public const string RPT_BH_LUONG_TRO_CAP_OM_DAU_GIAI_THICH_DON_VI = "rptBH_Luong_Tro_Cap_Om_Dau_DonVi_GiaiThich_DonVi.xlsx";
        public const string RPT_BH_LUONG_TRO_CAP_OM_DAU_GIAI_THICH = "rptBH_Luong_Tro_Cap_Om_Dau_GiaiThich.xlsx";
        public const string RPT_BH_LUONG_TRO_CAP_OM_DAU_DON_VI_IN_SO_THUC_NHAN = "rptBH_Luong_Tro_Cap_Om_Dau_DonVi_InSoThucNhan.xlsx";
        public const string RPT_BH_LUONG_TRO_CAP_THAI_SAN = "rptBH_Luong_Tro_Cap_Thai_San.xlsx";
        public const string RPT_BH_LUONG_TRO_CAP_THAI_SAN_DON_VI = "rptBH_Luong_Tro_Cap_Thai_San_DonVi.xlsx";
        public const string RPT_BH_LUONG_TRO_CAP_TNLD = "rptBH_Luong_Tro_Cap_TNLD.xlsx";
        public const string RPT_BH_LUONG_TRO_CAP_TNLD_DON_VI = "rptBH_Luong_Tro_Cap_TNLD_DonVi.xlsx";
        public const string RPT_BH_LUONG_TRO_CAP_HT_PV_TV_TT = "rptBH_Luong_Tro_Cap_HT_PV_TV_TT.xlsx";
        public const string RPT_BH_LUONG_TRO_CAP_HT_PV_TV_TT_DON_VI = "rptBH_Luong_Tro_Cap_HT_PV_TV_TT_DonVi.xlsx";
        public const string RPT_BH_LUONG_TRO_CAP_XUAT_NGU = "rptBH_Luong_Tro_Cap_Xuat_Ngu.xlsx";
        public const string RPT_BH_LUONG_EXPORT_DATA = "rptBH_Luong_Tro_Cap_Xuat_Ngu.xlsx";
        public const string RPT_BH_LUONG_TRO_CAP_XUAT_NGU_DON_VI = "rptBH_Luong_Tro_Cap_Xuat_Ngu_Don_Vi.xlsx";

        public const string RPT_BH_KHTM_BHYT_CHUNGTU_CHITIET = "rptBH_KHTM_BHYT_ChungTu_ChiTiet.xlsx";
        public const string IMPORT_BH_KHTM_BHYT_CHUNGTU_CHITIET = "importBH_KHTM_BHYT_ChungTu_ChiTiet.xlsx";
        public const string PRT_BH_KHTM_BHYT_ChungTu_ChiTiet_BC = "rptBH_ChungTu_ChiTiet_KHTM_BHYT.xlsx";
        public const string PRT_BH_KHTM_Du_Toan_Thu_BHYT_Than_Nhan = "rptBH_KHTM_Du_Toan_Thu_BHYT_Than_Nhan.xlsx";
        public const string PRT_BH_KHTM_Du_Toan_Thu_BHYT_HSSV = "rptBH_KHTM_Du_Toan_Thu_BHYT_HSSV.xlsx";

        public const string RPT_BH_KHC_KPQL_CHUNGTU_CHITIET_BHXH = "rptBH_KHC_QLKP_ChungTu_ChiTiet.xlsx";
        public const string IMPORT_BH_KHC_QLKP_CHUNGTU_CHITIET_BHXH = "importBH_KHC_QLKP_ChungTu_ChiTiet.xlsx";
        public const string PRT_BH_KHC_QLKP_CHUNGTU_CHITIET_PHULUC_DOC = "rptBH_KHC_QLKP_ChungTu_ChiTiet_PhuLuc";
        public const string PRT_BH_KHC_QLKP_CHUNGTU_TONGHOP_DONVI_PHULUC_DOC = "rptBH_KHC_QLKP_ChungTu_TongHop_DonVi_PhuLuc";

        public const string RPT_BH_KHC_KCB_CHUNGTU_CHITIET_BHXH = "rptBH_KHC_KCB_ChungTu_ChiTiet.xlsx";
        public const string IMPORT_BH_KHC_KCB_CHUNGTU_CHITIET_BHXH = "importBH_KHC_KCB_ChungTu_ChiTiet.xlsx";
        public const string PRT_BH_KHC_KCB_CHUNGTU_CHITIET_PHULUC_DOC = "rptBH_KHC_KCB_ChungTu_ChiTiet_PhuLuc";
        public const string PRT_BH_KHC_KCB_CHUNGTU_TONGHOP_PHULUC_DOC = "rptBH_KHC_KCB_ChungTu_TongHop_DonVi_PhuLuc";

        public const string IMPORT_BH_DTT_BHXH_CHUNGTU_CHITIET = "import_BH_DTT_BHXH_ChungTu_ChiTiet.xlsx";
        public const string EXPORT_BH_DTT_BHXH_CHUNGTU_CHITIET = "rpt_DTT_BHXH_ChungTu.xlsx";

        public const string RPT_DM_MLNS_BHXH = "rpt_dm_mlns_bhxh.xlsx";
        public const string RPT_DM_MLNS_BHXH_IMPORT = "rpt_dm_mlns_bhxh_import.xlsx";

        public const string RPT_BH_DT_DCDT_CHUNGTU_CHITIET_BHXH = "rptBH_DT_DCDT_ChungTu_ChiTiet.xlsx";
        public const string IMPORT_BH_DT_DCDT_CHUNGTU_CHITIET_BHXH = "importBH_DT_DCDT_ChungTu_ChiTiet.xlsx";

        public const string RPT_BHXH_DT_DCDT_CHIBHXH_CHITIET = "rptBHXH_DT_DCDT_CHI_BHXH_ChiTiet";
        public const string RPT_BHXH_DT_DCDT_CHIKPQL_CHITIET = "rptBHXH_DT_DCDT_CHI_KPQL_ChiTiet";
        public const string RPT_BHXH_DT_DCDT_CHIKPKCB_QUANY_CHITIET = "rptBHXH_DT_DCDT_CHI_KPKCB_QuanY_ChiTiet";
        public const string RPT_BHXH_DT_DCDT_CHIKPKCB_TS_CHITIET = "rptBHXH_DT_DCDT_CHI_KPKCB_TruongSa_ChiTiet";
        public const string RPT_BHXH_DT_DCDT_CHIKPCSSK_HSSV_NLD_CHITIET = "rptBHXH_DT_DCDT_CHI_KPCSSK_HSSV_NLD_ChiTiet";
        public const string RPT_BHXH_DT_DCDT_CHITNKDQ_KCBBHYT_QUANNHAN_CHITIET = "rptBHXH_DT_DCDT_CHI_KetDu_KCBBHYT_QuanNhan_ChiTiet";
        public const string RPT_BHXH_DT_DCDT_BHTN_CHITIET = "rptBHXH_DT_DCDT_CHI_Hotro_BHTN_ChiTiet";
        public const string RPT_BHXH_DT_DCDT_MSTTBYT_CHITIET = "rptBHXH_DT_DCDT_CHI_MSTTBYT_ChiTiet";
        // Du toan dieu chinh  tong hop chi tiet don vi
        public const string RPT_BHXH_DT_DCDT_CHIBHXH_TONGHOP_CHITIETDONVI = "rptBHXH_DT_DCDT_CHI_BHXH_TongHop_ChiTietDonVi";
        public const string RPT_BHXH_DT_DCDT_CHIKPQL_TONGHOP_CHITIETDONVI = "rptBHXH_DT_DCDT_CHI_KPQL_TongHop_ChiTietDonVi";
        public const string RPT_BHXH_DT_DCDT_CHIKPKCB_QUANY_TONGHOP_CHITIETDONVI = "rptBHXH_DT_DCDT_CHI_KPKCB_QuanY_TongHop_ChiTietDonVi";
        public const string RPT_BHXH_DT_DCDT_CHIKPKCB_TS_TONGHOP_CHITIETDONVI = "rptBHXH_DT_DCDT_CHI_KPKCB_TruongSa_TongHop_ChiTietDonVi";
        public const string RPT_BHXH_DT_DCDT_CHIKPCSSK_HSSV_NLD_TONGHOP_CHITIETDONVI = "rptBHXH_DT_DCDT_CHI_KPCSSK_HSSV_NLD_TongHop_ChiTietDonVi";
        //public const string RPT_BHXH_DT_DCDT_CHIKPCSSK_NLD_TONGHOP_CHITIETDONVI = "rptBHXH_DT_DCDT_CHI_KPCSSK_NLD_TongHop_ChiTietDonVi";
        public const string RPT_BHXH_DT_DCDT_CHITNKDQ_KCBBHYT_QUANNHAN_TONGHOP_CHITIETDONVI = "rptBHXH_DT_DCDT_CHI_KetDu_KCBBHYT_QuanNhan_TongHop_ChiTietDonVi";
        public const string RPT_BHXH_DT_DCDT_BHTN_TONGHOP_CHITIETDONVI = "rptBHXH_DT_DCDT_CHI_Hotro_BHTN_TongHop_ChiTietDonVi";
        public const string RPT_BHXH_DT_DCDT_MSTTBYT_TONGHOP_CHITIETDONVI = "rptBHXH_DT_DCDT_CHI_MuaSam_TTBYT_TongHop_ChiTietDonVi";

        public const string RPT_BHXH_DT_PBC_CHIBHXH_TONGHOP_DONVI_PHULUC = "rptBHXH_DTPBC_BHXH_TongHop_DonVi_PhuLuc";
        public const string RPT_BHXH_DT_PBC_CHIKPQL_TONGHOP_DONVI_PHULUC = "rptBHXH_DTPBC_KPQL_TongHop_DonVi_PhuLuc";
        public const string RPT_BHXH_DT_PBC_CHIKPCBQY_TONGHOP_DONVI_PHULUC = "rptBHXH_DTPBC_KCB_QY_TongHop_DonVi_PhuLuc";
        public const string RPT_BHXH_DT_PBC_CHIKPCBTS_TONGHOP_DONVI_PHULUC = "rptBHXH_DTPBC_KCB_TS_TongHop_DonVi_PhuLuc";
        public const string RPT_BHXH_DT_PBC_CHIKPCSSK_HSSV_NLS_TONGHOP_DONVI_PHULUC = "rptBHXH_DTPBC_CSSK_HSSV_NLD_TongHop_DonVi_PhuLuc";
        public const string RPT_BHXH_DT_PBC_CHITNKDQ_KCBBHYT_QUANNHAN_TONGHOP_DONVI_PHULUC = "rptBHXH_DTPBC_KD_KCBBHYT_QN_TongHop_DonVi_PhuLuc";
        public const string RPT_BHXH_DT_PBC_CHI_BHTN_TONGHOP_DONVI_PHULUC = "rptBHXH_DTPBC_HT_BHTN_TongHop_DonVi_PhuLuc";
        public const string RPT_BHXH_DT_PBC_CHI_MSTTBYT_TONGHOP_DONVI_PHULUC = "rptBHXH_DTPBC_MS_TTBYT_TongHop_DonVi_PhuLuc";

        public const string RPT_BHXH_DT_PBC_BS_CHIBHXH_TONGHOP_DONVI_PHULUC = "rptBHXH_DTPBC_BHXH_TongHop_DonVi_PhuLuc";
        public const string RPT_BHXH_DT_PBC_BS_CHIKPQL_TONGHOP_DONVI_PHULUC = "rptBHXH_DTPBC_BS_KPQL_TongHop_DonVi_PhuLuc";
        public const string RPT_BHXH_DT_PBC_BS_CHIKPCBQY_TONGHOP_DONVI_PHULUC = "rptBHXH_DTPBC_BS_KCBQY_TongHop_DonVi_PhuLuc";
        public const string RPT_BHXH_DT_PBC_BS_CHIKPCBTS_TONGHOP_DONVI_PHULUC = "rptBHXH_DTPBC_BS_KCBTS_TongHop_DonVi_PhuLuc";
        public const string RPT_BHXH_DT_PBC_BS_CHIKPCSSK_HSSV_NLS_TONGHOP_DONVI_PHULUC = "rptBHXH_DTPBC_BS_CSSKHSSVNLD_TongHop_DonVi_PhuLuc";
        public const string RPT_BHXH_DT_PBC_BS_CHITNKDQ_KCBBHYT_QUANNHAN_TONGHOP_DONVI_PHULUC = "rptBHXH_DTPBC_BS_KDKCBBHYTQN_TongHop_DonVi_PhuLuc";
        public const string RPT_BHXH_DT_PBC_BS_CHI_BHTN_TONGHOP_DONVI_PHULUC = "rptBHXH_DTPBC_BS_HT_BHTN_TongHop_DonVi_PhuLuc";
        public const string RPT_BHXH_DT_PBC_BS_CHI_MSTTBYT_TONGHOP_DONVI_PHULUC = "rptBHXH_DTPBC_BS_MS_TTBYT_TongHop_DonVi_PhuLuc";

        public const string RPT_BH_DC_DTT_CHUNG_TU_CHITIET = "rptBH_DC_DTT_Chung_Tu_ChiTiet.xlsx";
        public const string RPT_BH_CPBS_CHUNG_TU_CHITIET = "rpt_BH_CPBS_Chung_Tu_ChiTiet.xlsx";

        public const string IMPORT_BH_DTTM_BHYT_CHUNGTU_CHITIET = "import_BH_DTTM_BHYT_ChungTu_ChiTiet.xlsx";
        public const string RPT_BH_DC_DU_TOAN_THU_DOC = "rptBH_DC_Du_Toan_Thu.xlsx";

        public const string RPT_BH_KE_HOACH_CAP_TAM_UNG_TNQN_DOC = "rpt_BH_Ke_Hoach_Cap_Bo_Sung_TNQN_Doc.xlsx";
        public const string RPT_BH_TONG_HOP_CAP_TAM_UNG_TNQN_NLD_DOC = "rpt_BH_Ke_Hoach_Cap_Bo_Sung_TNQN_NLD_Doc.xlsx";
        public const string RPT_BH_TONG_HOP_THONG_TRI_CBS_DOC = "rpt_BH_Tong_Hop_Thong_Tri_CBS_Doc.xlsx";
        public const string RPT_BH_THONG_TRI_CBS_DOC = "rpt_BH_Thong_Tri_CBS_Doc.xlsx";
        public const string RP_BH_EXPORT_CAPPHAT = "rptBH_CapPhat_ChiTiet.xlsx";
        public const string IMPORT_BH_CP_CHUNGTU_CHITIET = "import_BH_CapPhat_ChungTu_ChiTiet.xlsx";

        public const string RPT_BH_CAPPHAT_THONGTRI_MACDINH = "rptBH_CapPhat_ThongTri_MacDinh.xlsx";
        public const string RPT_BH_CAPPHAT_THONGTRI_MACDINH_DONVI = "rptBH_CapPhat_ThongTri_MacDinh_DonVi.xlsx";
        public const string RPT_BH_CAPPHAT_THONGTRI_MACDINH_TONGHOP = "rptBH_CapPhat_ThongTri_MacDinh_TongHop.xlsx";

        // bao thong tri tong hop don vi
        public const string RPT_BH_CHI_KINH_PHI_BHXH_CAPPHAT_DONVI = "rptBH_CKP_BHXH_CapPhat_DonVi";
        public const string RPT_BH_CHI_KINH_PHI_QL_CAPPHAT_DONVI = "rptBH_CKP_QL_CapPhat_DonVi";
        public const string RPT_BH_CHI_KINH_PHI_KCB_QYDV_CAPPHAT_DONVI = "rptBH_CKP_KCB_QYDV_CapPhat_DonVi";
        public const string RPT_BH_CHI_KINH_PHI_KCB_TS_CAPPHAT_DONVI = "rptBH_CKP_KCB_TS_CapPhat_DonVi";
        public const string RPT_BH_CHI_KINH_PHI_CHAM_SSK_BANDAU_HSSV_CAPPHAT_DONVI = "rptBH_CKP_CSSKBD_HSSV_CapPhat_DonVi";
        public const string RPT_BH_CHI_CHI_KINH_PHI_CHAM_SSK_BANDAU_NLD_CAPPHAT_DONVI = "rptBH_CKP_CSSKBD_NLD_CapPhat_DonVi";
        public const string RPT_BH_CHI_KINH_PHI_KCB_BHYT_QN_CAPPHAT_DONVI = "rptBH_CKP_TKCB_BHYT_QN_CapPhat_DonVi";
        public const string RPT_BH_CHI_KINH_PHI_MS_TTB_YTE_CAPPHAT_DONVI = "rptBH_CKP_Muasam_TTBYte_CapPhat_DonVi";
        public const string RPT_BH_CHI_KINH_PHI_HT_BHTN_CAPPHAT_DONVI = "rptBH_CKP_Hotro_BHTN_CapPhat_DonVi";

        public const string RPT_BH_CP_KHCKP_CAPKINHPHI_CACCHEDO_BHXH = "rptBH_CP_KHCKP_CapKinhPhi_CacCheDo_BHXH.xlsx";
        public const string RPT_BH_CP_KHCKP_CAPKINHPHI_QUANLY_BHXH_BHYT = "rptBH_CP_KHCKP_CapKinhPhi_Quanly_BHXH_BHYT.xlsx";
        public const string RPT_BH_CP_KHCKP_CAPKINHPHI_KCB_QUANY = "rptBH_CP_KHCKP_CapKinhPhi_KCB_QuanY.xlsx";
        public const string RPT_BH_CP_KHCKP_CAPKINHPHI_KCB_TRUONGSA = "rptBH_CP_KHCKP_CapKinhPhi_KCB_TruongSa.xlsx";
        public const string RPT_BH_CP_KHCKP_CAPKINHPHI_CSSK_HSSV = "rptBH_CP_KHCKP_CapKinhPhi_CSSK_HSSV.xlsx";
        public const string RPT_BH_CP_KHCKP_CAPKINHPHI_CSSK_NLD = "rptBH_CP_KHCKP_CapKinhPhi_CSSK_NLD.xlsx";
        public const string RPT_BH_CP_KHCKP_CAPKINHPHI_HTT_BHTN = "rptBH_CP_KHCKP_CapKinhPhi_HTT_BHTN.xlsx";
        public const string RPT_BH_CP_KHCKP_CAPKINHPHI_MSTTBYT = "rptBH_CP_KHCKP_CapKinhPhi_MSTTBYT.xlsx";
        public const string RPT_BH_CP_KHCKP_CAPKINHPHI_KCB_BHYT = "rptBH_CP_KHCKP_CapKinhPhi_KCB_BHYT.xlsx";

        public const string RPT_BH_CHI_BHXH_CAPPHAT_LNS = "rptBH_CHI_BHXH_CapPhat_Lns";
        public const string RPT_BH_CHI_KINH_PHI_QL_CAPPHAT_LNS = "rptBH_CHI_KINH_PHI_QL_CapPhat_Lns";
        public const string RPT_BH_CHI_KINH_PHI_KCBQY_CAPPHAT_LNS = "rptBH_CHI_KINH_PHI_KCBQY_CapPhat_Lns";
        public const string RPT_BH_CHI_KINH_PHI_KCBTS_CAPPHAT_LNS = "rptBH_CHI_KINH_PHI_KCBTS_CapPhat_Lns";
        public const string RPT_BH_CHI_KINH_PHI_CSSK_HSSV_CAPPHAT_LNS = "rptBH_CHI_KINH_PHI_CSSK_HSSV_CapPhat_Lns";
        public const string RPT_BH_CHI_KINH_PHI_CSSK_NLD_CAPPHAT_LNS = "rptBH_CHI_KINH_PHI_CSSK_NLD_CapPhat_Lns";
        public const string RPT_BH_CHI_KINH_PHI_TNKDQ_KCBBHYT_CAPPHAT_LNS = "rptBH_CHI_KINH_PHI_KCBBHYT_QuanNhan_CapPhat_Lns";
        public const string RPT_BH_CHI_KINH_PHI_MSTTBY_CAPPHAT_LNS = "rptBH_CHI_KINH_PHI_Muasam_TTBYte_CapPhat_Lns";
        public const string RPT_BH_CHI_KINH_PHI_HTBHTN_CAPPHAT_LNS = "rptBH_CHI_KINH_PHI_HoTro_BHTN_CapPhat_Lns";
        public const string RPT_BH_CHI_BHXH_CAPPHAT_THONGTRI_TYPES = "rptBH_CHI_BHXH_CapPhat_Thong_Tri_Type";


        public const string RPT_BH_QTCN_CHUNGTU_CHITIET_BHXH = "rpt_BH_QTCN_Chung_Tu_ChiTiet.xlsx";
        public const string RPT_BH_QTT_BHXH_CHUNGTU_CHITIET = "rpt_BH_QTT_BHXH_ChungTu_ChiTiet.xlsx";
        public const string RPT_BH_QTT_BHXH_TEMPLATE_IMPORT = "rpt_BH_QTT_Template_Import.xlsx";
        public const string RPT_BH_QTCQ_CHUNGTU_CHITIET_BHXH = "rpt_BH_QTCQ_Chung_Tu_ChiTiet.xlsx";
        public const string RPT_BH_QTTM_BHYT_CHUNGTU_CHITIET = "rpt_BH_QTTM_BHYT_ChungTu_ChiTiet.xlsx";
        public const string RPT_BH_QTC_KPQL_CHUNGTU_CHITIET_BHXH = "rptBH_QTC_QLKP_ChungTu_ChiTiet.xlsx";
        public const string IMPORT_BH_QTC_KPQL_CHUNGTU_CHITIET = "import_BH_QTC_QCKPQL_ChungTu_ChiTiet.xlsx";
        public const string RPT_BH_QTC_QKPQL_TOBIA = "rptBH_QTC_QKPQL_ToBia.xlsx";
        public const string RPT_BH_QTC_QKPQL_THONGTRILOAI2 = "rptBH_QTC_QKPQL_Thongtri_Loai2";
        public const string RPT_BH_QTC_QKPQL_THONGTRILOAI1 = "rptBH_QTC_QKPQL_Thongtri_Loai1";
        public const string RPT_BH_QTC_QKPQL_CHITIET = "rptBH_QTC_QKPQL_ChungTu_ChiTiet";
        public const string RPT_BH_QTC_QKPQL_CHITIET_ISSOLIEU = "rptBH_QTC_QKPQL_ChungTu_ChiTiet_SoLieu";
        public const string RPT_BH_THONG_TRI_QUYET_TOAN_THU = "rpt_BH_Thong_Tri_Quyet_Toan_Thu.xlsx";
        public const string RPT_BH_THONG_TRI_QUYET_TOAN_THU_TONG_HOP = "rpt_BH_Thong_Tri_Quyet_Toan_Thu_Tong_Hop.xlsx";
        public const string RPT_BH_QUYET_TOAN_THU_NOP_ALL_QUY = "rpt_BH_Quyet_Toan_Thu_Nop_All_Quy.xlsx";
        public const string RPT_BH_QUYET_TOAN_THU_NOP_BHXH_QUY = "rpt_BH_Quyet_Toan_Thu_Nop_BHXH_Quy.xlsx";
        public const string RPT_BH_QUYET_TOAN_THU_NOP_BHYT_QUY = "rpt_BH_Quyet_Toan_Thu_Nop_BHYT_Quy.xlsx";
        public const string RPT_BH_QUYET_TOAN_THU_NOP_BHTN_QUY = "rpt_BH_Quyet_Toan_Thu_Nop_BHTN_Quy.xlsx";
        public const string RPT_BH_GIAI_THICH_LOI_QUY = "rpt_BH_Giai_Thich_Loi_Quy.xlsx";
        public const string RPT_BH_QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_NAM = "rpt_BH_Quyet_Toan_Thu_Nop_BHXH_BHYT_BHTN_Nam.xlsx";
        public const string RPT_BH_QUYET_TOAN_THU_BHXH = "rpt_BH_Quyet_Toan_Thu_BHXH.xlsx";
        public const string RPT_BH_QUYET_TOAN_THU_BHTN = "rpt_BH_Quyet_Toan_Thu_BHTN.xlsx";
        public const string RPT_BH_QUYET_TOAN_THU_BHYT_QUAN_NHAN = "rpt_BH_Quyet_Toan_Thu_BHYT_Quan_nhan.xlsx";
        public const string RPT_BH_GIAI_THICH_SO_LIEU_NAM = "rpt_BH_Giai_Thich_So_Lieu_Nam.xlsx";
        public const string RPT_BH_QUYET_TOAN_THU_BHYT_NLD = "rpt_BH_Quyet_Toan_Thu_BHYT_NLD.xlsx";
        public const string RPT_BH_QUYET_TOAN_TONG_HOP_NAM = "rptBH_KHT_Quyet_Toan_Tong_Hop_Nam.xlsx";
        public const string RPT_BH_QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN_NLD = "rpt_BH_Quyet_Toan_Thu_Mua_BHYT_Than_Nhan_NLD.xlsx";
        public const string RPT_BH_QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN_NLD_NAM = "rpt_BH_Quyet_Toan_Thu_Mua_BHYT_Than_Nhan_NLD_Nam.xlsx";
        public const string RPT_BH_QUYET_TOAN_TONG_HOP_THU_CHI = "rpt_BH_Quyet_Toan_Tong_Hop_Thu_Chi.xlsx";
        public const string RPT_BH_THAM_DINH_QUYET_TOAN_CHI_TIET = "rpt_BH_ThamDinhQuyetToan_ChiTiet.xlsx";
        public const string RPT_BH_THAM_DINH_QUYET_TOAN_THU_CHI = "rpt_BH_ThamDinhQuyetToanThuChi.xlsx";
        public const string RPT_BH_THAM_DINH_QUYET_TOAN_CHI_CHE_DO_BHXH = "rpt_BH_ThamDinhQuyetToan_ChiCheDoBHXH.xlsx";
        public const string RPT_BH_THAM_DINH_QUYET_TOAN_CHI_CSSK_HSSV_NLD = "rpt_BH_ThamDinhQuyetToan_ChiCSSK_HSSV_NLD.xlsx";
        public const string RPT_BH_THAM_DINH_QUYET_TOAN_CHI_KCB_QYDV = "rpt_BH_ThamDinhQuyetToan_ChiKCB_QuanYDonVi.xlsx";
        public const string RPT_BH_THAM_DINH_QUYET_TOAN_CHI_MUA_SAM_TTBYT = "rpt_BH_ThamDinhQuyetToan_ChiMuaSamTTBYT.xlsx";
        public const string RPT_BH_THAM_DINH_QUYET_TOAN_CHI_KPQL = "rptBH_ThamDinhQuyetToan_ChiKPQL.xlsx";
        public const string RPT_BH_THAM_DINH_QUYET_TOAN_CHI_KPK_TRUONGSADK = "rptBH_ThamDinhQuyetToan_ChiKPK_TruongSaDK.xlsx";
        public const string RPT_BH_THAM_DINH_QUYET_TOAN_THU_BHTN = "rpt_BH_ThamDinhQuyetToan_Thu_BHTN.xlsx";
        public const string RPT_BH_THAM_DINH_QUYET_TOAN_THU_BHXH = "rpt_BH_ThamDinhQuyetToan_Thu_BHXH.xlsx";
        public const string RPT_BH_THAM_DINH_QUYET_TOAN_THU_BHYT_NLD = "rpt_BH_ThamDinhQuyetToan_Thu_BHYT_NLD.xlsx";
        public const string RPT_BH_THAM_DINH_QUYET_TOAN_THU_BHYT_QUANNHAN = "rpt_BH_ThamDinhQuyetToan_Thu_BHYT_Quan_nhan.xlsx";
        public const string RPT_BH_THAM_DINH_QUYET_TOAN_THU_BHYT_HSSV_HVQS_SQDB = "rpt_BH_ThamDinhQuyetToan_Thu_Mua_BHYT_HSSV_HVQS_SQDB.xlsx";
        public const string RPT_BH_THAM_DINH_QUYET_TOAN_THU_BHYT_THANNHAN = "rpt_BH_ThamDinhQuyetToan_Thu_Mua_BHYT_Than_Nhan.xlsx";
        public const string RPT_BH_THAM_DINH_QUYET_TOAN_CCTQBHXHBHYT = "rpt_BH_ThamDinhQuyetToan_CanCuTrichQuyBhxhSangBhyt.xlsx";
        public const string RPT_BH_QTCQ_CHUNGTU_TRO_CAP_GIAI_THICH = "rpt_BH_QTCQ_Chung_Tu_Tro_Cap_GiaiThich.xlsx";
        public const string RPT_BH_THONG_TRI_QUYET_TOAN_THU_ALL_THC = "rptBH_ThongTriTongHop_Thu_All_TongHopChung.xlsx";
        public const string RPT_BH_GIAI_SO_LIEU_THANG_QUY = "rpt_BH_Quyet_Toan_Thu_Giai_Thich_So_Lieu.xlsx";

        public const string RPT_BH_QQUYET_TOAN_THU_MUA_BHYT_THAN_NHAN = "rpt_BH_Quyet_Toan_Thu_Mua_BHYT_Than_Nhan.xlsx";
        public const string RPT_BH_QQUYET_TOAN_THU_MUA_BHYT_BHYT_HSSV_HVQS_SQDB = "rpt_BH_Quyet_Toan_Thu_Mua_BHYT_HSSV_HVQS_SQDB.xlsx";
        public const string RPT_BH_QTCQ_CHUNGTU_CHITIET_KCB = "rpt_BH_QTCQ_Chung_Tu_ChiTiet_KCB.xlsx";
        public const string RPT_BH_QTC_NKPQL_CHUNGTU_CHITIET_BHXH = "rptBH_QTC_NQLKP_ChungTu_ChiTiet.xlsx";
        public const string IMPORT_BH_QTC_NKPQL_CHUNGTU_CHITIET = "import_BH_QTC_NCKPQL_ChungTu_ChiTiet.xlsx";
        public const string RPT_BH_QTC_NKPQL_CHUNGTU_DONVI_PHULUC_BHXH = "rptBH_QTC_NQLKP_ChungTu_DonVi_PhuLuc_Doc.xlsx";
        public const string RPT_BH_QTC_NKPQL_CHITIET = "rptBH_QTC_NKPQL_ChungTu_ChiTiet_Doc.xlsx";
        public const string RPT_BH_QTC_QKPK_CHUNGTU_CHITIET_BHXH = "rptBH_QTC_QKPK_ChungTu_ChiTiet.xlsx";
        public const string IMPORT_BH_QTC_QKPK_CHUNGTU_CHITIET = "import_BH_QTC_QKPK_ChungTu_ChiTiet.xlsx";
        public const string RPT_BH_DU_TOAN_TONG_HOP_THU_CHI = "rptBH_Du_Toan_Tong_Hop_Thu_Chi.xlsx";
        public const string RPT_BH_DU_TOAN_TONG_HOP_THU_CHI_BHXH_BHYT_BHTN = "rpt_BH_Du_Toan_Tong_Hop_Thu_Chi_BHXH_BHYT_BHTN.xlsx";
        public const string RPT_BH_DU_TOAN_TONG_HOP_THU_CHI_NGANG = "rpt_BH_Du_Toan_Tong_Hop_Thu_Chi_Ngang.xlsx";
        public const string RPT_BH_DIEU_CHINH_DU_TOAN_TONG_HOP = "rptBH_Dieu_Chinh_Du_Toan_Tong_Hop.xlsx";
        public const string RPT_BH_DU_TOAN_TONG_HOP_THU_CHI_PLI = "rpt_BH_Du_Toan_Tong_Hop_Thu_Chi.xlsx";
        public const string RPT_BH_DU_TOAN_THU_BHXH_CHUNGTU = "rptBH_DTT_BHXH_ChungTu.xlsx";
        public const string RPT_THONG_BAO_PHE_DUYET_QUYET_TOAN_NAM_TONG_HOP_THU_CHI = "rptBH_Tham_Dinh_Quyet_Toan_Tong_Hop_Thu_Chi.xlsx";
        public const string RPT_BAO_CAO_TONG_HOP_QUYET_TOAN_THU_CHI_BHXH_BHYT_BHTN = "rptBH_Tong_Hop_Quyet_Toan_Thu_Chi_BHXH_BHYT_BHTN.xlsx";
        public const string RPT_BH_THAM_DINH_DU_TOAN_KINH_PHI_CHUYEN_NAM_SAU = "rpt_BH_ThamDinhQuyetToan_DuToanKinhPhi_Chuyen_Nam_Sau.xlsx";

        public const string RPT_BH_QTC_QKPK_TSDK_THONGTRILOAI1 = "rptBH_QTC_QKPK_TSDK_Thongtri_Loai1";
        public const string RPT_BH_QTC_QKPK_TSDK_THONGTRILOAI2 = "rptBH_QTC_QKPK_TSDK_Thongtri_Loai2";

        public const string RPT_BH_QTC_QKPK_KCBBHYTQN_BHTN_MSTTBYT_THONGTRILOAI1 = "rptBH_QTC_QKPK_KCBBHYTQN_BHTN_MSTTBYT_Thongtri_Loai1";
        public const string RPT_BH_QTC_QKPK_KCBBHYTQN_BHTN_MSTTBYT_THONGTRILOAI2 = "rptBH_QTC_QKPK_KCBBHYTQN_BHTN_MSTTBYT_Thongtri_Loai2";


        public const string RPT_BH_QTC_QKPK_QYDV_THONGTRILOAI1 = "rptBH_QTC_QKPK_QYDV_Thongtri_Loai1";
        public const string RPT_BH_QTC_QKPK_QYDV_THONGTRILOAI2 = "rptBH_QTC_QKPK_QYDV_Thongtri_Loai2";
        public const string RPT_BH_QTC_QKPK_HSSV_THONGTRILOAI1 = "rptBH_QTC_QKPK_HSSV_Thongtri_Loai1";
        public const string RPT_BH_QTC_QKPK_HSSV_THONGTRILOAI2 = "rptBH_QTC_QKPK_HSSV_Thongtri_Loai2";
        public const string RPT_BH_QTC_QKPK_NLD_THONGTRILOAI1 = "rptBH_QTC_QKPK_NLD_Thongtri_Loai1";
        public const string RPT_BH_QTC_QKPK_NLD_THONGTRILOAI2 = "rptBH_QTC_QKPK_NLD_Thongtri_Loai2";
        public const string RPT_BH_QTC_QKPK_NLD_KEHOACH = "rptBH_QTC_QKPK_NLD_KeHoach";
        public const string RPT_BH_QTC_QKPK_HSSV_KEHOACH = "rptBH_QTC_QKPK_HSSV_KeHoach";
        public const string RPT_BH_QTC_QKPK_MACDINH_KEHOACH = "rptBH_QTC_QKPK_MacDinh_KeHoach";
        public const string RPT_BH_QTC_QKPK_QYDV_KEHOACH = "rptBH_QTC_QKPK_QYDN_KeHoach";
        public const string RPT_BH_QTC_QKPK_TOBIA = "rptBH_QTC_QKPK_ToBia.xlsx";
        public const string RPT_BH_QTC_QKPK_MSTTBYT_KEHOACH = "rptBH_QTC_QKPK_MSTTBYT_KeHoach";
        public const string RPT_BH_QTC_QKPK_KCBBHYT_KEHOACH = "rptBH_QTC_QKPK_KCBBHYT_KeHoach";
        public const string RPT_BH_QTC_QKPK_BHTN_KEHOACH = "rptBH_QTC_QKPK_BHTN_KeHoach";

        public const string RPT_BH_QTC_QKPK_NLD_KEHOACH_ISSOLIEU = "rptBH_QTC_QKPK_NLD_KeHoach_SoLieu";
        public const string RPT_BH_QTC_QKPK_HSSV_KEHOACH_ISSOLIEU = "rptBH_QTC_QKPK_HSSV_KeHoach_SoLieu";
        public const string RPT_BH_QTC_QKPK_MACDINH_KEHOACH_ISSOLIEU = "rptBH_QTC_QKPK_MacDinh_KeHoach_SoLieu";
        public const string RPT_BH_QTC_QKPK_QYDV_KEHOACH_ISSOLIEU = "rptBH_QTC_QKPK_QYDN_KeHoach_SoLieu";

        public const string RPT_BH_QTC_NKPK_CHUNGTU_CHITIET_BHXH = "rptBH_QTC_NKPK_ChungTu_ChiTiet.xlsx";
        public const string IMPORT_BH_QTC_NKPK_CHUNGTU_CHITIET = "import_BH_QTC_NKPK_ChungTu_ChiTiet.xlsx";
        public const string RPT_BH_QTC_NKPK_HSSV_CHUNGTU_DONVI_PHULUC_BHXH = "rptBH_QTC_NKPK_HSSV_ChungTu_DonVi_PhuLuc";
        public const string RPT_BH_QTC_NKPK_NLD_CHUNGTU_DONVI_PHULUC_BHXH = "rptBH_QTC_NKPK_NLD_ChungTu_DonVi_PhuLuc";
        public const string RPT_BH_QTC_NKPK_CHUNGTU_DONVI_PHULUC_BHXH = "rptBH_QTC_NKPK_ChungTu_DonVi_PhuLuc";
        public const string RPT_BH_QTC_NKPK_CHITIET = "rptBH_QTC_NKPK_ChungTu_ChiTiet";
        public const string RPT_BH_QTC_NKPKBHTN_CHITIET = "rptBH_QTC_NKPK_BHTN_ChungTu_ChiTiet";
        public const string RPT_BH_QTC_NKPK_HSSV_NLD_CHITIET = "rptBH_QTC_NKPK_HSSV_NLD_ChungTu_ChiTiet";
        public const string RPT_BH_QTC_NKPK_KCB_BHYT_CHITIET = "rptBH_QTC_NKPK_KCB_BHYT_ChungTu_ChiTiet";
        public const string RPT_BH_QTC_NKPK_MSTTBYT_CHITIET = "rptBH_QTC_NKPK_MSTTBYT_ChungTu_ChiTiet";

        public const string RPT_BH_KHC_K_CHUNGTU_CHITIET = "rptBH_KHC_K_ChungTu_ChiTiet.xlsx";
        public const string IMPORT_BH_KHC_K_CHUNGTU_CHITIET = "importBH_KHC_K_ChungTu_ChiTiet.xlsx";
        public const string PRT_BH_KHC_K_CHUNGTU_CHITIET_PHULUC_DOC = "rptBH_KHC_K_ChungTu_ChiTiet_PhuLuc";
        public const string PRT_BH_KHC_K_CHUNGTU_TONGHOP_PHULUC_DOC = "rptBH_KHC_K_ChungTu_TongHop_DonVi_PhuLuc";
        public const string RPT_BH_QTCN_CHUNGTU_CHITIET_KCB = "rpt_BH_QTCN_Chung_Tu_ChiTiet_KCB.xlsx";
        public const string RPT_BH_QTC_KPKCB_CHUNG_TU_CHITIET = "rpt_BH_QTC_KPKCB_Chung_Tu_Chi_Tiet.xlsx";
        public const string RPT_BH_QTC_KPKCB_CHUNG_TU_CHITIET_NAM = "rpt_BH_QTC_KPKCB_Chung_Tu_Chi_Tiet_Nam.xlsx";
        public const string RPT_BH_QTC_QBHXH_TOBIA = "rptBH_QTC_QBHXH_ToBia.xlsx";
        public const string RPT_BH_QTC_QKCB_TOBIA = "rptBH_QTC_QKCB_ToBia.xlsx";

        public const string RPT_BH_QT_KCB_BHYT_CHITIET = "rptBH_QT_KCB_BHYT_ChiTiet_Doc.xlsx";
        public const string RPT_BH_QT_KCB_BHYT_TONGHOP = "rptBH_QT_KCB_BHYT_TongHop_Doc.xlsx";

        public const string IMPORT_BH_CPTU_CHUNGTU_CHITIET = "KeHoachCapTamUng_Template_Import.xlsx";
    }

    public struct TypeChuKy
    {
        //Ngan Sach
        //So nhu cau
        public const string RPT_NS_SNC_DONVI = "rptNS_DonVi";
        public const string RPT_NS_SNC_TONGHOP = "rptNS_SNC_TongHop";
        public const string RPT_NS_SNC_TONGHOP_NSSD = "rptNS_SNC_TongHop_NSSD";
        public const string RPT_NS_SNC_TONGHOP_NSBD_DT = "rptNS_SNC_TongHop_NSBD_DT";
        public const string RPT_NS_SNC_TONGHOP_NSBD_MHHV = "rptNS_SNC_TongHop_NSBD_MHHV";
        public const string RPT_NS_SNC_CHITIET = "rptNS_SNC_ChiTiet";
        public const string RPT_NS_SNC_CHITIET_NSSD = "rptNS_SNC_ChiTiet_NSSD";
        public const string RPT_NS_SNC_CHITIET_NSBD_DT = "rptNS_SNC_ChiTiet_NSBD_DT";
        public const string RPT_NS_SNC_CHITIET_NSBD_MHHV = "rptNS_SNC_ChiTiet_NSBD_MHHV";
        public const string RPT_NS_SNC3Y_CHITIET = "rptNS_SNC3Y_ChiTiet";
        public const string RPT_NS_SNC3Y_TONGHOP = "rptNS_SNC3Y_TongHop";
        //So Kiem Tra
        public const string RPT_NS_SKT_NHANSOKIEMTRA = "rptNS_SKT_NhanSoKiemTra";
        public const string RPT_NS_SKT_NHANSOKIEMTRA_NSSD = "rptNS_SKT_NhanSoKiemTra_NSSD";
        public const string RPT_NS_SKT_NHANSOKIEMTRA_NSDTN = "rptNS_SKT_NhanSoKiemTra_NSDTN";
        public const string RPT_NS_SKT_NHANSOKIEMTRA_NSSD_TONGHOP = "rptNS_SKT_NhanSoKiemTra_NSSD_TONGHOP";
        public const string RPT_NS_SKT_NHANSOKIEMTRA_NSDTN_TONGHOP = "rptNS_SKT_NhanSoKiemTra_NSDTN_TONGHOP";
        public const string RPT_NS_SKT_TONGHOP_BENHVIENTUCHU = "rpt_SKT_TongHopBenhVienTuChu";
        //Phan bo so kiem tra
        public const string RPT_NS_PHANBO_SOKIEMTRA_DONVI = "rptNS_PhanBo_SoKiemTra_DonVi";
        public const string RPT_NS_PHANBO_SOKIEMTRA_DONVI_NSDTN = "rptNS_PhanBo_SoKiemTra_DonVi_NSDTN";
        public const string RPT_NS_PHANBO_SOKIEMTRA_DONVI_NSSD = "rptNS_PhanBo_SoKiemTra_DonVi_NSSD";
        public const string RPT_NS_TONGHOP_PHANBO_SOKIEMTRA = "rptNS_TongHop_PhanBo_SoKiemTra";
        public const string RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_THEONGANH = "rptNS_PhanBo_SoKiemTra_TheoNganh";
        public const string RPT_NS_NGANHTHAMDINH_CTC = "rptNS_NganhThamDinh_Ctc";
        public const string RPT_NS_NGANHTHAMDINH_SKT = "rptNS_NganhThamDinh_Skt";
        public const string RPT_NS_SO_NHU_CAU_THEONGANH = "rptNS_SoNhuCau_TheoNganh";
        public const string RPT_NS_NHAN_SO_KIEM_TRA_THEONGANH = "rptNS_Nhan_SoKiemTra_TheoNganh";
        public const string RPT_NS_PHUONGAN_PHANBO_SOKIEMTRA = "rptNS_PhuongAn_PhanBo_SoKiemTra";
        public const string RPT_NS_PHUONGAN_PHANBO_SOKIEMTRA_02A = "rptNS_PhuongAn_PhanBo_SoKiemTra_02a";
        public const string RPT_NS_PHUONGAN_PHANBO_SOKIEMTRA_02B = "rptNS_PhuongAn_PhanBo_SoKiemTra_02b";
        public const string RPT_NS_SO_SANH_PHAN_BO_SKT_NAM_TRUOC_NAM_NAY = "rptNS_Nhan_SoKiemTra_TheoNganh";

        // xuat excel import
        // so nhu cau
        public const string RPT_NS_SOKIEMTRA_CHUNGTU_TONGHOP = "rptNS_SoKiemTra_ChungTu_TongHop";
        public const string RPT_NS_PLAN_DUTOAN_NGANSACH_CHITIET_DONVI_SD = "rptNS_Plan_DuToan_NganSach_ChiTiet_DonVi_NSSD";
        public const string RPT_NS_PLAN_DUTOAN_NGANSACH_CHITIET_DONVI_DT = "rptNS_Plan_DuToan_NganSach_ChiTiet_DonVi_NSDT";
        public const string RPT_NS_PLAN_DUTOAN_NGANSACH_CHITIET_DONVI_SD_Ngang = "rptNS_Plan_DuToan_NganSach_ChiTietDonVi_NSSD_Ngang";
        public const string RPT_NS_PLAN_DUTOAN_NGANSACH_CHITIET_DONVI_DT_Ngang = "rptNS_Plan_DuToan_NganSach_ChiTietDonVi_NSDT_Ngang";

        public const string RPT_NS_PLAN_SOSANH_SKTDTDN_SD = "rptNS_Plan_SoSanh_SKTDTDN_NSSD";
        public const string RPT_NS_PLAN_SOSANH_SKTDTDN_DT = "rptNS_Plan_SoSanh_SKTDTDN_NSDT";

        //Quyet toan
        public const string RPT_NS_QUYETTOAN_CHUNGTU_THUONGXUYEN = "rptNS_QuyetToan_ChungTu_ThuongXuyen";
        public const string RPT_NS_QUYETTOAN_CHUNGTU_QUOCPHONG = "rptNS_QuyetToan_ChungTu_QuocPhong";
        public const string RPT_NS_QUYETTOAN_CHUNGTU_NHANUOC = "rptNS_QuyetToan_ChungTu_NhaNuoc";
        public const string RPT_NS_QUYETTOAN_CHUNGTU_NGOAIHOI = "rptNS_QuyetToan_ChungTu_NgoaiHoi";
        public const string RPT_NS_QUYETTOAN_CHUNGTU_KHAC = "rptNS_QuyetToan_ChungTu_Khac";
        public const string RPT_NS_QUYETTOAN_THONGTRI_LNS = "rptNS_QuyetToan_ThongTri_LNS";
        public const string RPT_NS_QUYETTOAN_THONGTRI_DONVI = "rptNS_QuyetToan_ThongTri_DonVi";
        public const string RPT_NS_QUYETTOAN_THUONGXUYEN_TONGHOP = "rptNS_QuyetToan_ThuongXuyen_TongHop";
        public const string RPT_NS_QUYETTOAN_QUOCPHONG_TONGHOP = "rptNS_QuyetToan_QuocPhong_TongHop";
        public const string RPT_NS_QUYETTOAN_NHANUOC_TONGHOP = "rptNS_QuyetToan_NhaNuoc_TongHop";
        public const string RPT_NS_QUYETTOAN_NGOAIHOI_TONGHOP = "rptNS_QuyetToan_NgoaiHoi_TongHop";
        public const string RPT_NS_QUYETTOAN_KINHPHIKHAC_TONGHOP = "rptNS_QuyetToan_KinhPhiKhac_TongHop";
        public const string RPT_NS_QUYETTOAN_TATCA_TONGHOP = "rptNS_QuyetToan_TatCa_TongHop";
        public const string RPT_NS_QUYETTOAN_DUTOAN = "rptNS_QuyetToan_DuToan";
        public const string RPT_NS_QUYETTOAN_NAM_LNS = "rptNS_QuyetToan_Nam_LNS";
        public const string RPT_NS_QUYETTOAN_NHAN_KINHPHI = "rptNS_Nhan_QuyetToan_KinhPhi";
        public const string RPT_NS_QUYETTOAN_TONGHOP_DONVI = "rptNS_QuyetToan_TongHop_DonVi";
        public const string RPT_BH_THAM_DINH_QUYET_TOAN_THU_CHI = "rpt_BH_ThamDinhQuyetToanThuChi";
        public const string RPT_BH_CAN_CU_TRICH_QUY_BHXH_SANG_DONG_BHYT = "rpt_BH_ThamDinhQuyetToan_TrichQuyBhxhSangBhyt";

        public const string RPT_NS_BANGKE_CHUNGTU = "rptNS_BangKe_ChungTu";
        public const string RPT_NS_BANGKE_TONGHOP = "rptNS_BangKe_TongHop";

        public const string RPT_NS_QUANSO_BINHQUAN = "rptNS_QuanSo_BinhQuan";
        public const string RPT_NS_QUANSO_LIENTHAM = "rptNS_QuanSo_LienTham";
        public const string RPT_NS_QUANSO_RAQUAN = "rptNS_QuanSo_RaQuan";
        public const string RPT_NS_QUANSO_TANGGIAM = "rptNS_QuanSo_TangGiam";
        public const string RPT_NS_QUANSO_TANGGIAM_TONGHOP_DONVI = "rptNS_QuanSo_TangGiam_TongHop_DonVi";
        public const string RPT_NS_QUANSO_DONVI = "rptNS_QuanSo_DonVi";
        public const string RPT_NS_QUANSO_THUONGXUYEN = "rptNS_QuanSo_ThuongXuyen";

        public const string RPT_NS_QUYETTOANNAM = "rptNS_QuyetToanNam";
        public const string RPT_NS_QUYETTOANNAM_TONGHOP = "rptNS_QuyetToanNam_TongHop";
        public const string RPT_NS_QUYETTOANNAM_XETDUYET = "rptNS_QuyetToanNam_XetDuyetQuyetToanNam";

        //Du Toan
        public const string RPT_NS_DUTOAN_THEODOT = "rptNS_DuToan_TheoDot";
        public const string RPT_NS_DUTOAN_LUYKEDENDOT = "rptNS_DuToan_LuyKeDenDot";
        public const string RPT_NS_DUTOAN_THEONGANH = "rptNS_DuToan_TheoNganh";
        public const string RPT_NS_DUTOAN_TONGHOP_TUCHI = "rptNS_DuToan_TongHopTuChi";
        public const string RPT_NS_DUTOAN_TONGHOP_HIENVAT = "rptNS_DuToan_TongHopHienVat";
        public const string RPT_NS_DUTOAN_TONGHOP_CHUNG = "rptNS_DuToan_TongHopChung";
        public const string RPT_NS_DUTOAN_TOBIA = "rptNS_DuToan_ToBia";
        public const string RPT_NS_DUTOAN_CHITIET_DONVI = "rptNS_DuToan_ChiTiet_DonVi";
        public const string RPT_NS_DUTOAN_TONGHOP_DONVI = "rptNS_DuToan_TongHop_DonVi";
        public const string RPT_NS_DUTOAN_PHUONGAN_THU_CHI = "rptNS_DuToan_Thu_Nop_Hd4554";
        public const string RPT_NS_DUTOAN_CHITIET_NGANH = "rptNS_DuToan_ChiTiet_Nganh";
        public const string RPT_NS_DUTOAN_TONGHOP_PHANBO = "rptNS_DuToan_TongHop_PhanBo";
        public const string RPT_NS_DUTOAN_TONGHOP_THEODOT = "rptNS_DuToan_TongHop_TheoDot";
        public const string RPT_NS_DUTOAN_TONGHOP_LNS = "rptNS_DuToan_TongHop_Lns";
        public const string RPT_NS_DUTOAN_TONGHOP_NGANH = "rptNS_DuToan_TongHop_Nganh";
        public const string RPT_NS_DUTOAN_CHITIEU_LNS = "rptNS_DuToan_ChiTieu_Lns";
        public const string RPT_NS_DUTOAN_SOPHANBO_HIENTAI = "rptNS_DuToan_SoPhanBo_HienTai";
        public const string RPT_NS_DUTOAN_DAUNAM = "rptNS_DuToan_DauNam";
        public const string RPT_NS_DUTOAN_DAUNAM_CHITHUONGXUYEN = "rptNS_DuToan_DauNam_ChiThuongXuyen";
        public const string RPT_NS_DUTOAN_DAUNAM_CHITHUONGXUYEN_DACTHU_DOC = "rptNS_DuToan_DauNam_ChiThuongXuyen_DacThu_Doc";
        public const string RPT_NS_DUTOAN_DAUNAM_CHITHUONGXUYEN_DACTHU_NGANG = "rptNS_DuToan_DauNam_ChiThuongXuyen_DacThu_Ngang";
        public const string RPT_NS_DUTOAN_DAUNAM_CHITHUONGXUYEN_MHHV = "rptNS_DuToan_DauNam_ChiThuongXuyen_MHHV";
        public const string RPT_NS_DUTOAN_DAUNAM_THUNOP_NGANSACH = "rptNS_TN_DuToanDauNam_ThuNopNS";
        public const string RPT_NS_DUTOAN_DAUNAM_THUNOP_NGANSACH_TONGHOP = "rptNS_TN_DuToanDauNam_TongHop_ThuNopNS";
        public const string RPT_NS_TONG_HOP_DU_TOAN_NGAN_SACH_DAC_THU_NGANH = "rptNS_TongHop_DuToan_NSDT";
        public const string RPT_NS_DUTOAN_SOSANH_SOKIEMTRA = "rptNS_DuToan_SoSanh_SoKiemTra";
        public const string RPT_NS_DUTOAN_THONGKE_THEO_SOQUYETDINH = "rptNS_DuToan_ThongKe_Theo_SoQuyetDinh";
        public const string RPT_NS_DUTOAN_PHANBO_TONGHOP_DONVI = "rptNS_DuToan_PhanBo_TongHop_DonVi";
        public const string RPT_NS_DUTOAN_PHANCAP = "rptNS_DuToan_PhapCap";
        public const string RPT_NS_DUTOAN_DIEUCHINH = "rptNS_DuToan_DieuChinh";
        public const string RPT_NS_DUTOAN_DIEUCHINH_DONVI = "rptNS_DuToan_DieuChinh_DonVi";
        public const string RPT_NS_DUTOAN_DIEUCHINH_TONGHOP2 = "rptNS_DuToan_DieuChinh_TONGHOP2";
        public const string RPT_NS_DUTOAN_DIEUCHINH_TONGHOP = "rptNS_DuToan_DieuChinhDuToan";
        public const string RPT_NS_PHANBODUTOAN_CONGKHAITAICHINH = "rptDuToan_TongHop_CongKhai";
        public const string RPT_NS_DUTOANDAUNAM_CHUYENNGANH = "rptDuToan_DauNam_TheoChuyenNganh";
        public const string RPT_NS_DUTOAN_QD_CONGKHAINGANSACH = "rptNS_DuToan_QdCongKhaiNganSach";
        public const string RPT_NS_DUTOAN_CONGKHAI_02CKNS = "rptNS_DuToan_QdCongKhaiNganSach_02CKNS";
        public const string RPT_NS_DUTOAN_CONGKHAI_06CKNS = "rptNS_DuToan_QdCongKhaiNganSach_06CKNS";
        public const string RPT_NS_DUTOAN_PAPB_MAUSO1_TOBAOCAO = "rptNS_DuToan_PAPB_MauSo1_ToBaoCao";
        public const string RPT_NS_DUTOAN_PAPB_MAUSO1_PHULUCI = "rptNS_DuToan_PAPB_MauSo1_PhuLucI";
        public const string RPT_NS_DUTOAN_PAPB_MAUSO1_PHULUCII = "rptNS_DuToan_PAPB_MauSo1_PhuLucII";
        public const string RPT_NS_DUTOAN_PAPB_MAUSO2_KETQUA = "rptNS_DuToan__PAPB_MauSo2_KetQua";

        public const string RPT_NS_QUYET_TOAN_CONGKHAI_THUCHI_04a = "rptNS_QuyetToan_CongKhai_ThuChi_04a";
        public const string RPT_NS_QUYET_TOAN_CONGKHAI_THUCHI_04b = "rptNS_QuyetToan_CongKhai_ThuChi_04b";

        //Cap Phat
        public const string RPT_NS_CAPPHAT_LNS = "rptNS_CapPhat_Lns";
        public const string RPT_NS_CAPPHAT_DONVI = "rptNS_CapPhat_DonVi";
        public const string RPT_NS_CAPPHAT_DENGHI = "rptNS_CapPhat_DeNghi";
        public const string RPT_NS_CAPPHAT_LOAICAP = "rptNS_CapPhat_LoaiCap";
        public const string RPT_NS_CAPPHAT_SOSANH = "rptNS_CapPhat_SoSanh";
        public const string RPT_NS_CAPPHAT_MLNS_DONVINGANG = "rptNS_CapPhat_Mlns_DonVi";

        //Thu nộp ngân sách
        public const string RPT_NS_THUNOPNGANSACH_LAPDUTOAN = "rptDuToan_ThuNop";
        public static string RPT_NS_THUNOP_NGANSACH_NHANHUOC_THANG = "rpt_ThuNop_NganSach_NhaNuoc_Thang";
        public static string RPT_NS_THUNOP_NGANSACH_NHANHUOC_QUY = "rpt_ThuNop_NganSach_NhaNuoc_Quy";
        public static string RPT_NS_THUNOP_NGANSACH_NHANHUOC_NAM = "rpt_ThuNop_NganSach_NhaNuoc_Nam";
        public static string RPT_NS_THUNOP_NGANSACH_QUOCPHONG_THANG = "rpt_ThuNop_QuocPhong_Thang";
        public static string RPT_NS_THUNOP_NGANSACH_QUOCPHONG_NAM = "rpt_ThuNop_QuocPhong_Nam";
        public static string RPT_NS_THUNOP_NGANSACH_QUOCPHONG_QUY = "rpt_ThuNop_QuocPhong_Quy";
        public static string RPT_NS_THUNOP_NGANSACH_PHANBO_DUTOAN_DONVI = "rpt_ThuNop_PhanBo_DuToan_DonVi";
        //Luong
        public const string RPT_TL_LUONG_THANG = "rptLuong_BangLuong_Thang";
        public const string RPT_TL_GIAY_GIOI_THIEU_TAI_CHINH = "rptLuong_Giay_Gioi_Thieu_Tai_Chinh";
        public const string RPT_TL_LUONG_THANG_BIEN_PHONG = "rptLuong_BangLuong_Thang_Bien_Phong";
        public const string RPT_TL_LUONG_TRUY_LINH = "rptLuong_BangLuong_TruyLinh";
        public const string RPT_TL_LUONG_TRUY_THU = "rptLuong_BangLuong_TruyThu";
        public const string RPT_TL_LUONG_BAO_HIEM = "rptLuong_BangLuong_BaoHiem";
        public const string RPT_TL_QUYET_TOAN_TTNCN = "rptLuong_QuyetToan_Nam_ThueTncn";
        public const string RPT_TL_DS_TRA_NGAN_HANG = "rptLuong_DanhSach_ChiTra_NganHang";
        public const string RPT_TL_BANGKE_TRICHTHUETNCN = "rptLuong_BangKe_TrichThueTNCN";
        public const string RPT_TL_DANHSACH_CHITRA_LUONGCN = "rptLuong_DanhSach_ChiTra_LuongCN";
        public const string RPT_TL_TONGHOP_LUONG_PHUCAP_DONVI = "rptLuong_TongHop_Luong_PhuCap_DonVi";
        public const string RPT_TL_TONGHOP_LUONG_NGACHDONVI = "rptLuong_TongHop_Luong_NgachDonVi";
        public const string RPT_TL_QUYETTOAN_QUANSO = "rptLuong_QuyetToan_QuanSo";
        public const string RPT_TL_GIAITHICH_CHITIET_PHUCAPTNVKTHD = "rptLuong_GiaiThich_ChiTiet_PhuCapTNVKTHD";
        public const string RPT_TL_GIAY_GT_TC = "rptLuong_Giay_GioiThieu_TaiChinh";
        public const string RPT_TL_TONG_HOP_LUONG_PHU_CAP_THEO_NGACH = "rptLuong_Tong_Hop_Luong_Theo_Ngach";
        public const string RPT_TL_GIAI_THICH_CHI_TIET_LUONG = "rptLuong_Giai_Thich_Luong_Chi_Tiet";
        public const string RPT_TL_GIAITHICH_CHITIET_PHUCAP_KHAC = "rptLuong_GiaiThich_ChiTiet_PhuCap_Kha";
        public const string RPT_TL_LUONGNAM_KEHOACH = "rptLuong_NamKeHoach";
        public const string RPT_TL_DIEUCHINH_QUANSO_KEHOACH = "rptLuong_DieuChinh_QuanSo_KeHoach";
        public const string RPT_TL_CHITIET_QS_RAQUAN = "rptLuong_ChiTiet_QS_RaQuan_KeHoach";
        public const string RPT_TL_CHITIET_QS_NGHIHUU = "rptLuong_ChiTiet_QS_NghiHuu_KeHoach";
        public const string RPT_TL_DS_CAPPHAT_PHUCAP = "rptLuong_DS_CapPhat_PhuCap";
        public const string RPT_TL_BIENDONG_QUANHAM_KEHOACH = "rptLuong_ChiTiet_BienDong_QuanHam_KeHoach";
        public const string RPT_TL_QUYETTOAN_LUONG_NAM_KH = "rptLuong_QuyetToan_Luong_Nam_KeHoach";
        public const string RPT_TL_TRUYLINH_CHUYENCHEDO_SQ = "rptLuong_TruyLinhChuyenCheDo";
        public const string RPT_TL_TRUYLINH_CHUYENCHEDO_QNCN = "rptLuong_TruyLinhChuyenCheDo_Qncn";
        public const string RPT_TL_BAOCAO_TIENAN = "rptLuong_BaoCao_TienAn";
        public const string RPT_TL_QUYETTOAN_THUONGXUYEN = "rptNS_QuyetToan_ThuongXuyen";
        public const string RPT_TL_GIAITHICH_CHITIET_RAQUAN_XUATNGU = "rptLuong_DS_RaQuan";
        public const string RPT_TL_CHITRA_NGANHANG_THUNHAPKHAC = "rptLuong_BaoCao_TriTraNganHang_ThuNhapKhac";
        public const string RPT_TL_BANG_TRUYLINH_NHIEU_QD = "rptLuong_BangLuong_TruyLinh_NhieuNgay_QuyetDinh";
        public const string RPT_TL_CHITIET_QUANSO_TANGGIAM = "rptLuong_ChiTiet_QuanSo_TangGiam";
        public const string RPT_TL_QS_CHUNGTU = "rptLuong_QS_ChungTu";
        public const string RPT_TL_TONGHOP_LUONG_PHUCAP_BIENPHONG = "rptLuong_Tong_Hop_Luong_PhuCap_BienPhong";
        public const string RPT_TL_GIAITHICH_PHUCAP_THEONGAY = "rptLuong_Giai_Thich_Cac_Phu_Cap_Theo_Ngay";

        //Luong new
        public const string RPT_TL_LUONG_THANG_NEW = "rptLuong_BangLuong_Thang";
        public const string RPT_TL_GIAY_GIOI_THIEU_TAI_CHINH_NEW = "rptLuong_Giay_Gioi_Thieu_Tai_Chinh";
        public const string RPT_TL_LUONG_THANG_BIEN_PHONG_NEW = "rptLuong_BangLuong_Thang_Bien_Phong";
        public const string RPT_TL_LUONG_TRUY_LINH_NEW = "rptLuong_BangLuong_TruyLinh";
        public const string RPT_TL_LUONG_TRUY_THU_NEW = "rptLuong_BangLuong_TruyThu";
        public const string RPT_TL_LUONG_BAO_HIEM_NEW = "rptLuong_BangLuong_BaoHiem";
        public const string RPT_TL_QUYET_TOAN_TTNCN_NEW = "rptLuong_QuyetToan_Nam_ThueTncn";
        public const string RPT_TL_DS_TRA_NGAN_HANG_NEW = "rptLuong_DanhSach_ChiTra_NganHang";
        public const string RPT_TL_BANGKE_TRICHTHUETNCN_NEW = "rptLuong_BangKe_TrichThueTNCN";
        public const string RPT_TL_DANHSACH_CHITRA_LUONGCN_NEW = "rptLuong_DanhSach_ChiTra_LuongCN";
        public const string RPT_TL_TONGHOP_LUONG_PHUCAP_DONVI_NEW = "rptLuong_TongHop_Luong_PhuCap_DonVi";
        public const string RPT_TL_TONGHOP_LUONG_NGACHDONVI_NEW = "rptLuong_TongHop_Luong_NgachDonVi";
        public const string RPT_TL_QUYETTOAN_QUANSO_NEW = "rptLuong_QuyetToan_QuanSo";
        public const string RPT_TL_GIAITHICH_CHITIET_PHUCAPTNVKTHD_NEW = "rptLuong_GiaiThich_ChiTiet_PhuCapTNVKTHD";
        public const string RPT_TL_GIAY_GT_TC_NEW = "rptLuong_Giay_GioiThieu_TaiChinh";
        public const string RPT_TL_TONG_HOP_LUONG_PHU_CAP_THEO_NGACH_NEW = "rptLuong_Tong_Hop_Luong_Theo_Ngach";
        public const string RPT_TL_GIAI_THICH_CHI_TIET_LUONG_NEW = "rptLuong_Giai_Thich_Luong_Chi_Tiet";
        public const string RPT_TL_GIAITHICH_CHITIET_PHUCAP_KHAC_NEW = "rptLuong_GiaiThich_ChiTiet_PhuCap_Kha";
        public const string RPT_TL_LUONGNAM_KEHOACH_NEW = "rptLuong_NamKeHoach";
        public const string RPT_TL_DIEUCHINH_QUANSO_KEHOACH_NEW = "rptLuong_DieuChinh_QuanSo_KeHoach";
        public const string RPT_TL_CHITIET_QS_RAQUAN_NEW = "rptLuong_ChiTiet_QS_RaQuan_KeHoach";
        public const string RPT_TL_CHITIET_QS_NGHIHUU_NEW = "rptLuong_ChiTiet_QS_NghiHuu_KeHoach";
        public const string RPT_TL_DS_CAPPHAT_PHUCAP_NEW = "rptLuong_DS_CapPhat_PhuCap";
        public const string RPT_TL_BIENDONG_QUANHAM_KEHOACH_NEW = "rptLuong_ChiTiet_BienDong_QuanHam_KeHoach";
        public const string RPT_TL_QUYETTOAN_LUONG_NAM_KH_NEW = "rptLuong_QuyetToan_Luong_Nam_KeHoach";
        public const string RPT_TL_TRUYLINH_CHUYENCHEDO_SQ_NEW = "rptLuong_TruyLinhChuyenCheDo";
        public const string RPT_TL_TRUYLINH_CHUYENCHEDO_QNCN_NEW = "rptLuong_TruyLinhChuyenCheDo_Qncn";
        public const string RPT_TL_BAOCAO_TIENAN_NEW = "rptLuong_BaoCao_TienAn";
        public const string RPT_TL_QUYETTOAN_THUONGXUYEN_NEW = "rptNS_QuyetToan_ThuongXuyen";
        public const string RPT_TL_GIAITHICH_CHITIET_RAQUAN_XUATNGU_NEW = "rptLuong_DS_RaQuan";
        public const string RPT_TL_CHITRA_NGANHANG_THUNHAPKHAC_NEW = "rptLuong_BaoCao_TriTraNganHang_ThuNhapKhac";
        public const string RPT_TL_BANG_TRUYLINH_NHIEU_QD_NEW = "rptLuong_BangLuong_TruyLinh_NhieuNgay_QuyetDinh";
        public const string RPT_TL_CHITIET_QUANSO_TANGGIAM_NEW = "rptLuong_ChiTiet_QuanSo_TangGiam";
        public const string RPT_TL_QS_CHUNGTU_NEW = "rptLuong_QS_ChungTu";
        public const string RPT_TL_TONGHOP_LUONG_PHUCAP_BIENPHONG_NEW = "rptLuong_Tong_Hop_Luong_PhuCap_BienPhong";
        public const string RPT_TL_GIAITHICH_PHUCAP_THEONGAY_NEW = "rptLuong_Giai_Thich_Cac_Phu_Cap_Theo_Ngay";
        // Von Dau Tu
        public const string RPT_VDT_KEHOACHTRUNGHAN_DEXUAT_MO_MOI_GOC = "rptKeHoachTrungHan_DeXuat_Report";
        public const string RPT_VDT_KEHOACHTRUNGHAN_DEXUAT_MO_MOI_DIEUCHINH = "rpt_KHTH_DeXuat_Dieu_Chinh";
        public const string RPT_VDT_KEHOACHTRUNGHAN_DEXUAT_CHUYENTIEP = "rptKeHoachTrungHan_DeXuat_ChuyenTiep_Report";

        public const string RPT_VDT_KEHOACHTRUNGHAN_DUOCDUYET_MO_MOI = "rptKeHoachTrungHanReport";
        public const string RPT_VDT_KEHOACHTRUNGHAN_DUOCDUYET_CHUYENTIEP = "rptKeHoachTrungHan_ChuyenTiep_Report";

        public const string RPT_VDT_KEHOACHVONNAM_DEXUAT_GOC = "rpt_BaoCao_KHNam_DonVi";
        public const string RPT_VDT_KEHOACHVONNAM_DEXUAT_DIEUCHINH = "rpt_BaoCao_KHNam_DonVi_DieuChinh";
        public const string RPT_VDT_KEHOACHVONNAM_NGUON_VON_KHAC_DIEUCHINH = "rpt_BaoCao_KHNam_DeXuat_DieuChinh_NguonVonKhac";

        public const string RPT_VDT_KEHOACHVONNAM_DUOCDUYET_GOC = "rptKeHoachVonNamDuocDuyet";
        public const string RPT_VDT_QUYETTOANNIENDO_VONUNG = "rpt_vdt_quyettoanniendo_vonung";
        public const string RPT_VDT_QUYETTOANNIENDO_VONNAM = "rpt_vdt_quyettoanniendo_vonnam";
        public const string RPT_VDT_THUCHIENDAUTU_DENGHITHANHTOAN = "rptDeNghiThanhToan";
        public const string RPT_VDT_THUCHIENDAUTU_THONGTRITHANHTOAN = "rptThongTriThanhToan";
        public const string RPT_VDT_THUCHIENDAUTU_PHEDUYETTHANHTOAN = "rptPheDuyetThanhToan";
        public const string RPT_VDT_THUCHIENDAUTU_KEHOACHCHIQUY = "rptKeHoachChiQuy";
        public const string RPT_VDT_THUCHIENDAUTU_QTND = "rptQuyetToanNienDo";
        public const string RPT_VDT_THONGTRI_QUYETTOAN = "rptVdt_ThongTriQuyetToan";

        public const string RPT_VDTG_TONG_HOP_QUYETTOAN = "rptvdtg_tong_hop_quyettoan";

        // Ngoai Hoi
        public const string RPT_NH_CAPPHAT_DENGHITHANHTOAN = "rptNHCapPhatDeNghiThanhToan";
        public const string RPT_NGOAIHOI_THONGTRI_CAPPHAT = "rptNgoaiHoi_ThongTri_CapPhat";
        public const string RPT_NGOAIHOI_THUCHIEN_NGANSACH = "rptNgoaiHoi_ThucHienNganSach";
        public const string RPT_NGOAIHOI_PHEDUYET_QUYETTOANDAHT = "rptNgoaiHoi_PheDuyet_QuyetToanDAHT";
        public const string RPT_BAOCAO_KETLUAN_QUYETTOAN = "rptBaoCao_KetLuan_QuyetToan";

        public const string RPT_NGOAIHOI_NHUCAU_CHIQUY = "rptNgoaiHoi_NhuCau_ChiQuy";
        public const string RPT_NH_DENGHI_QUYETTOAN_NIENDO = "rptNHDeNghiQuyetToanNienDo";
        public const string RPT_NH_TONG_HOP_QUYETTOAN = "rpt_nh_th_qt_tt";
        public const string RPT_NH_TONG_HOP_QUYETTOAN_TOTRINH = "rpt_nh_th_qt_tt";
        public const string RPT_NH_TONG_HOP_QUYETTOAN_PHULUC = "rpt_nh_th_qt_pl";
        public const string RPT_NH_CAPPHAT_PHEDUYETTHANHTOAN = "rptNHCapPhatPheDuyetThanhToan";
        public const string RPT_NH_THONG_TRI_QUYET_TOAN = "rptNHThongTriQuyetToan";
        public const string RPT_NH_CHENHLECH_TIGIA_HOIDOAI = "rptNH_ChenhLechTiGiaHoiDoai";

        // Bao Hiem - Ke Hoach Thu
        public const string RPT_BHXH_KHT_CHITIET = "rptBHXH_KHT_ChiTiet";
        public const string RPT_BHXH_KHTC_TONG_HOP = "rptBHXH_KHTC_TongHop";
        public const string RPT_BHXH_DU_TOAN_THU_BHXH = "rptBHXH_DU_TOAN_THU_BHXH";
        public const string RPT_BHXH_DU_TOAN_THU_BHTN = "rptBHXH_DU_TOAN_THU_BHTN";
        public const string RPT_BHXH_DU_TOAN_THU_BHYT_QUAN_NHAN = "rptBHXH_DU_TOAN_THU_BHYT_QUAN_NHAN";
        public const string RPT_BHXH_DU_TOAN_THU_BHYT_NLD = "rptBHXH_DU_TOAN_THU_BHYT_NLD";
        public const string RPT_BHXH_DU_TOAN_THU_BHXH_BHYT_BHTN = "rptBHXH_DU_TOAN_THU_BHXH_BHYT_BHTN";
        public const string RPT_BHXH_PHUONGAN_DU_TOAN_CHI_GOP_KQPL = "rptBHXH_PhuongAn_DuToan_GopKPQL";
        public const string RPT_BHXH_PHUONGAN_DU_TOAN_CHI_TACH_KQPL = "rptBHXH_PhuongAn_DuToan_TachKPQL";

        // Bao Hiem - Ke Hoach Thu Mua BHYT
        public const string RPT_BHYT_KHTM_CHITIET = "rptBHYT_KHTM_ChiTiet";
        public const string RPT_BHYT_KHTM_THAN_NHAN = "rptBHYT_KHTM_DuToanThu_Than_nhan";
        public const string RPT_BHYT_KHTM_HSSV = "rptBHYT_KHTM_DuToanThu_HSSV";

        // Bảo hiểm kế hoạch chi
        public const string RPT_BHXH_KHC_CHITIET = "rptBHXH_KHC_ChiTiet";
        public const string RPT_BHXH_KHC_TONGHOP = "rptBHXH_KHC_TongHop";

        // Bao hiem ke hoach kinh phi chi
        public const string RPT_BHXH_KHC_QLKP_CHITIET = "rptBHXH_KHC_QLKP_ChiTiet";
        public const string RPT_BHXH_KHC_QLKP_TONGHOP = "rptBHXH_KHC_QLKP_TongHop";
        // Bao hiem ke hoach khac
        public const string RPT_BHXH_KHC_K_TSDK_CHITIET = "rptBHXH_KHC_K_TSDK_ChiTiet";
        public const string RPT_BHXH_KHC_K_HSSV_NLD_CHITIET = "rptBHXH_KHC_K_HSSV_NLD_ChiTiet";
        public const string RPT_BHXH_KHC_K_MSTTBYT_CHITIET = "rptBHXH_KHC_K_MSTTBYT_ChiTiet";
        public const string RPT_BHXH_KHC_K_BHYTTN_CHITIET = "rptBHXH_KHC_K_BHYTTN_ChiTiet";
        public const string RPT_BHXH_KHC_K_BHTN_CHITIET = "rptBHXH_KHC_K_BHTN_ChiTiet";
        public const string RPT_BHXH_KHC_K_TSDK_PHUlUC = "rptBHXH_KHC_K_TSDK_PhucLuc";
        public const string RPT_BHXH_KHC_K_HSSV_NLD_PHUlUC = "rptBHXH_KHC_K_HSSV_NLD_PhucLuc";
        public const string RPT_BHXH_DUTOAN_DIEUCHINH = "rptBHXH_DuToan_DieuChinh";

        // Du toan dieu chinh chi tiết
        public const string RPT_BHXH_DT_DCDT_CHIBHXH_CHITIET = "rptBHXH_DT_DCDT_CHI_BHXH_ChiTiet";
        public const string RPT_BHXH_DT_DCDT_CHIKPQL_CHITIET = "rptBHXH_DT_DCDT_CHI_KPQL_ChiTiet";
        public const string RPT_BHXH_DT_DCDT_CHIKPKCB_QUANY_CHITIET = "rptBHXH_DT_DCDT_CHI_KPKCB_QuanY_ChiTiet";
        public const string RPT_BHXH_DT_DCDT_CHIKPKCB_TS_CHITIET = "rptBHXH_DT_DCDT_CHI_KPKCB_TruongSa_ChiTiet";
        public const string RPT_BHXH_DT_DCDT_CHIKPCSSK_HSSV_NLD_CHITIET = "rptBHXH_DT_DCDT_CHI_KPCSSK_HSSV_NLD_ChiTiet";
        public const string RPT_BHXH_DT_DCDT_CHITNKDQ_KCBBHYT_QUANNHAN_CHITIET = "rptBHXH_DT_DCDT_CHI_KetDu_KCBBHYT_QuanNhan_ChiTiet";
        public const string RPT_BHXH_DT_DCDT_BHTN_CHITIET = "rptBHXH_DT_DCDT_CHI_Hotro_BHTN_ChiTiet";
        public const string RPT_BHXH_DT_DCDT_MSTTBYT_CHITIET = "rptBHXH_DT_DCDT_CHI_MSTTBYT_ChiTiet";
        // Du toan dieu chinh  tong hop chi tiet don vi
        public const string RPT_BHXH_DT_DCDT_CHIBHXH_TONGHOP_CHITIETDONVI = "rptBHXH_DT_DCDT_CHI_BHXH_TongHop_ChiTietDonVi";
        public const string RPT_BHXH_DT_DCDT_CHIKPQL_TONGHOP_CHITIETDONVI = "rptBHXH_DT_DCDT_CHI_KPQL_TongHop_ChiTietDonVi";
        public const string RPT_BHXH_DT_DCDT_CHIKPKCB_QUANY_TONGHOP_CHITIETDONVI = "rptBHXH_DT_DCDT_CHI_KPKCB_QuanY_TongHop_ChiTietDonVi";
        public const string RPT_BHXH_DT_DCDT_CHIKPKCB_TS_TONGHOP_CHITIETDONVI = "rptBHXH_DT_DCDT_CHI_KPKCB_TruongSa_TongHop_ChiTietDonVi";
        public const string RPT_BHXH_DT_DCDT_CHIKPCSSK_HSSV_NLD_TONGHOP_CHITIETDONVI = "rptBHXH_DT_DCDT_CHI_KPCSSK_HSSV_NLD_TongHop_ChiTietDonVi";
        //public const string RPT_BHXH_DT_DCDT_CHIKPCSSK_NLD_TONGHOP_CHITIETDONVI = "rptBHXH_DT_DCDT_CHI_KPCSSK_NLD_TongHop_ChiTietDonVi";
        public const string RPT_BHXH_DT_DCDT_CHITNKDQ_KCBBHYT_QUANNHAN_TONGHOP_CHITIETDONVI = "rptBHXH_DT_DCDT_CHI_KetDu_KCBBHYT_QuanNhan_TongHop_ChiTietDonVi";
        public const string RPT_BHXH_DT_DCDT_BHTN_TONGHOP_CHITIETDONVI = "rptBHXH_DT_DCDT_CHI_Hotro_BHTN_TongHop_ChiTietDonVi";
        public const string RPT_BHXH_DT_DCDT_MSTTBYT_TONGHOP_CHITIETDONVI = "rptBHXH_DT_DCDT_CHI_MuaSam_TTBYT_TongHop_ChiTietDonVi";


        // Du toan phân bổ chi
        public const string RPT_BHXH_DT_PBC_CHIBHXH_TONGHOP_DONVI_PHULUC = "rptBHXH_DTPBC_BHXH_TongHop_DonVi_PhuLuc";
        public const string RPT_BHXH_DT_PBC_CHIKPQL_TONGHOP_DONVI_PHULUC = "rptBHXH_DTPBC_KPQL_TongHop_DonVi_PhuLuc";
        public const string RPT_BHXH_DT_PBC_CHIKPCBQY_TONGHOP_DONVI_PHULUC = "rptBHXH_DTPBC_KCBQY_TongHop_DonVi_PhuLuc";
        public const string RPT_BHXH_DT_PBC_CHIKPCBTS_TONGHOP_DONVI_PHULUC = "rptBHXH_DTPBC_KCBTS_TongHop_DonVi_PhuLuc";
        public const string RPT_BHXH_DT_PBC_CHIKPCSSK_HSSV_NLS_TONGHOP_DONVI_PHULUC = "rptBHXH_DTPBC_CSSK_HSSV_NLD_TongHop_DonVi_PhuLuc";
        public const string RPT_BHXH_DT_PBC_CHITNKDQ_KCBBHYT_QUANNHAN_TONGHOP_DONVI_PHULUC = "rptBHXH_DTPBC_KD_KCBBHYT_QN_TongHop_DonVi_PhuLuc";
        public const string RPT_BHXH_DT_PBC_CHI_BHTN_TONGHOP_DONVI_PHULUC = "rptBHXH_DTPBC_HT_BHTN_TongHop_DonVi_PhuLuc";
        public const string RPT_BHXH_DT_PBC_CHI_MSTTBYT_TONGHOP_DONVI_PHULUC = "rptBHXH_DTPBC_MS_TTBYT_TongHop_DonVi_PhuLuc";
        public const string RPT_BHXH_DT_PBC_CHI_BHXH_TONGHOP_DONVI_PHULUC = "rptBHXH_DT_PBC_CHI_BHXH_TongHop_DonVi_PhuLuc";

        public const string RPT_BHXH_DT_PBC_BS_CHIBHXH_TONGHOP_DONVI_PHULUC = "rptBHXH_DTPBC_BS_BHXH_TongHop_DonVi_PhuLuc";
        public const string RPT_BHXH_DT_PBC_BS_CHIKPQL_TONGHOP_DONVI_PHULUC = "rptBHXH_DTPBC_BS_KPQL_TongHop_DonVi_PhuLuc";
        public const string RPT_BHXH_DT_PBC_BS_CHIKPCBQY_TONGHOP_DONVI_PHULUC = "rptBHXH_DTPBC_BS_KCBQY_TongHop_DonVi_PhuLuc";
        public const string RPT_BHXH_DT_PBC_BS_CHIKPCBTS_TONGHOP_DONVI_PHULUC = "rptBHXH_DTPBC_BS_KCBTS_TongHop_DonVi_PhuLuc";
        public const string RPT_BHXH_DT_PBC_BS_CHIKPCSSK_HSSV_NLS_TONGHOP_DONVI_PHULUC = "rptBHXH_DTPBC_BS_CSSKHSSVNLD_TongHop_DonVi_PhuLuc";
        public const string RPT_BHXH_DT_PBC_BS_CHITNKDQ_KCBBHYT_QUANNHAN_TONGHOP_DONVI_PHULUC = "rptBHXH_DTPBC_BS_KCBBHYTQN_TongHop_DonVi_PhuLuc";
        public const string RPT_BHXH_DT_PBC_BS_CHI_BHTN_TONGHOP_DONVI_PHULUC = "rptBHXH_DTPBC_BS_HTBHTN_TongHop_DonVi_PhuLuc";
        public const string RPT_BHXH_DT_PBC_BS_CHI_MSTTBYT_TONGHOP_DONVI_PHULUC = "rptBHXH_DTPBC_BS_MSTTBYT_TongHop_DonVi_PhuLuc";


        public const string RPT_BHXH_DT_DC_DTT_CHITIET = "rptBHXH_DTT_DC_DTT_ChiTiet";
        public const string RPT_BHXH_CPBS_QN = "rpt_BHXH_CPBS_QN";
        public const string RPT_BHXH_CPBS_TNQN_NLD = "rpt_BHXH_CPBS_TNQN_NLD";
        public const string RPT_BHXH_CPBS_TONG_HOP_THONG_TRI_TNQN = "rpt_BHXH_CPBS_TONG_HOP_THONG_TRI";
        public const string RPT_BHXH_CPBS_TONG_HOP_THONG_TRI_TNQN_NLD = "rpt_BHXH_CPBS_TONG_HOP_THONG_TRI_TNQN_NLD";
        public const string RPT_BHXH_CPBS_THONG_TRI_TNQN = "rpt_BHXH_CPBS_THONG_TRI";
        public const string RPT_BHXH_CPBS_THONG_TRI_TNQN_NLD = "rpt_BHXH_CPBS_THONG_TRI_TNQN_NLD";
        public const string RPT_BHXH_CHITIET_NOIDUNG_KPQL = "rptBHXH_DTPBC_NoiDung_KPQL";
        // Bh Cap Phat chung tu
        public const string RPT_BH_CHI_KINH_PHI_BHXH_CAPPHAT_DONVI = "rptBH_CKP_BHXH_CapPhat_DonVi";
        public const string RPT_BH_CHI_KINH_PHI_QL_CAPPHAT_DONVI = "rptBH_CKP_QL_CapPhat_DonVi";
        public const string RPT_BH_CHI_KINH_PHI_KCB_QYDV_CAPPHAT_DONVI = "rptBH_CKP_KCB_QYDV_CapPhat_DonVi";
        public const string RPT_BH_CHI_KINH_PHI_KCB_TS_CAPPHAT_DONVI = "rptBH_CKP_KCB_TS_CapPhat_DonVi";
        public const string RPT_BH_CHI_KINH_PHI_CHAM_SSK_BANDAU_HSSV_CAPPHAT_DONVI = "rptBH_CKP_CSSKBD_HSSV_CapPhat_DonVi";
        public const string RPT_BH_CHI_CHI_KINH_PHI_CHAM_SSK_BANDAU_NLD_CAPPHAT_DONVI = "rptBH_CKP_CSSKBD_NLD_CapPhat_DonVi";
        public const string RPT_BH_CHI_KINH_PHI_KCB_BHYT_QN_CAPPHAT_DONVI = "rptBH_CKP_TKCB_BHYT_QN_CapPhat_DonVi";
        public const string RPT_BH_CHI_KINH_PHI_MS_TTB_YTE_CAPPHAT_DONVI = "rptBH_CKP_Muasam_TTBYte_CapPhat_DonVi";
        public const string RPT_BH_CHI_KINH_PHI_HT_BHTN_CAPPHAT_DONVI = "rptBH_CKP_Hotro_BHTN_CapPhat_DonVi";

        public const string RPT_BH_CHI_BHXH_CAPPHAT_LNS = "rptBH_CHI_BHXH_CapPhat_Lns";
        public const string RPT_BH_CHI_KINH_PHI_QL_CAPPHAT_LNS = "rptBH_CHI_KINH_PHI_QL_CapPhat_Lns";
        public const string RPT_BH_CHI_KINH_PHI_KCBQY_CAPPHAT_LNS = "rptBH_CHI_KINH_PHI_KCBQY_CapPhat_Lns";
        public const string RPT_BH_CHI_KINH_PHI_KCBTS_CAPPHAT_LNS = "rptBH_CHI_KINH_PHI_KCBTS_CapPhat_Lns";
        public const string RPT_BH_CHI_KINH_PHI_CSSK_HSSV_CAPPHAT_LNS = "rptBH_CHI_KINH_PHI_CSSK_HSSV_CapPhat_Lns";
        public const string RPT_BH_CHI_KINH_PHI_CSSK_NLD_CAPPHAT_LNS = "rptBH_CHI_KINH_PHI_CSSK_NLD_CapPhat_Lns";
        public const string RPT_BH_CHI_KINH_PHI_TNKDQ_KCBBHYT_CAPPHAT_LNS = "rptBH_CHI_KINH_PHI_KCBBHYT_QuanNhan_CapPhat_Lns";
        public const string RPT_BH_CHI_KINH_PHI_MSTTBY_CAPPHAT_LNS = "rptBH_CHI_KINH_PHI_Muasam_TTBYte_CapPhat_Lns";
        public const string RPT_BH_CHI_KINH_PHI_HTBHTN_CAPPHAT_LNS = "rptBH_CHI_KINH_PHI_HoTro_BHTN_CapPhat_Lns";
        public const string RPT_BH_CHI_BHXH_CAPPHAT_THONGTRI_TYPES = "rptBH_CHI_BHXH_CapPhat_Thong_Tri_Type";


        public const string RPT_BH_CP_KH_CBHXH = "rptBH_CP_KHCKP_CapKinhPhi_CacCheDo_BHXH";
        public const string RPT_BH_CP_KH_CKPQl_BHXH_BHYT = "rptBH_CP_KHCKP_CapKinhPhi_Quanly_BHXH_BHYT";
        public const string RPT_BH_CP_KH_CKP_KCB_QYDV = "rptBH_CP_KHCKP_CapKinhPhi_KCB_QuanY";
        public const string RPT_BH_CP_KH_CKP_KCB_TS = "rptBH_CP_KHCKP_CapKinhPhi_KCB_TruongSa";
        public const string RPT_BH_CP_KH_CKP_CSSKBD_HSSV = "rptBH_CP_KHCKP_CapKinhPhi_CSSK_HSSV";
        public const string RPT_BH_CP_KH_CKP_CSSKBD_NLD = "rptBH_CP_KHCKP_CapKinhPhi_CSSK_NLD";
        public const string RPT_BH_CP_KH_CKP_HTBHTN = "rptBH_CP_KHCKP_CapKinhPhi_HT_BHTN";
        public const string RPT_BH_CP_KH_CKP_MuaSam_TTBYTe = "rptBH_CP_KHCKP_CapKinhPhi_MuaSam_TTBYTe";
        public const string RPT_BH_CP_KH_CKP_KCBBHYT = "rptBH_CP_KHCKP_CapKinhPhi_KCBBHYT_QuanNhan";

        public const string RPT_BH_CAPPHAT_DENGHI = "rptBH_CapPhat_DeNghi";
        public const string RPT_BH_CAPPHAT_LOAICAP = "rptBH_CapPhat_LoaiCap";
        public const string RPT_BH_CAPPHAT_SOSANH = "rptBH_CapPhat_SoSanh";
        public const string RPT_BH_CAPPHAT_MLNS_DONVINGANG = "rptBH_CapPhat_Mlns_DonVi";

        public const string RPT_BH_DUTOAN_THONGBAOCAPCHITIEUNGANSACH_BHXH = "rptBH_DuToan_ThongBaoCapChiTieuNganSach_BHXH";
        public const string RPT_BH_DUTOAN_THONGBAOCAPCHITIEUNGANSACH_KPQL = "rptBH_DuToan_ThongBaoCapChiTieuNganSach_KPQL";
        public const string RPT_BH_DUTOAN_THONGBAOCAPCHITIEUNGANSACH_KCB_QY = "rptBH_DuToan_ThongBaoCapChiTieuNganSach_KCB_QY";
        public const string RPT_BH_DUTOAN_THONGBAOCAPCHITIEUNGANSACH_KCB_TS = "rptBH_DuToan_ThongBaoCapChiTieuNganSach_KCB_TS";
        public const string RPT_BH_DUTOAN_THONGBAOCAPCHITIEUNGANSACH_KCB_BHYT_QN = "rptBH_DuToan_ThongBaoCapChiTieuNganSach_KCB_BHYT_QN";
        public const string RPT_BH_DUTOAN_THONGBAOCAPCHITIEUNGANSACH_KCB_MSTTB_YT = "rptBH_DuToan_ThongBaoCapChiTieuNganSach_KCB_MSTTB_YT";
        public const string RPT_BH_DUTOAN_THONGBAOCAPCHITIEUNGANSACH_HSSV_NLD = "rptBH_DuToan_ThongBaoCapChiTieuNganSach_HSSV_NLD";
        public const string RPT_BH_DUTOAN_THONGBAOCAPCHITIEUNGANSACH_HT_BHTN = "rptBH_DuToan_ThongBaoCapChiTieuNganSach_HT_BHTN";

        //Bh quyết toán
        public const string RPT_BH_QUYETTOANQUY_BAOCAOQUYETTOANCHIBHXH = "rptBH_QuyetToanQuy_BaoCaoQuyetToanChiCacCheDoBHXH";
        public const string RPT_BH_QUYETTOANQUY_GIAITHICHTROCAPOMDAU = "rptBH_QT_BaoCaoQTChiGiaiThichTroCapOmDauBHXH";
        public const string RPT_BH_QUYETTOANQUY_GIAITHICHTROCAPTHAISAN = "rptBH_QT_BaoCaoQTChiGiaiThichTroCapThaiSanBHXH";
        public const string RPT_BH_QUYETTOANQUY_GIAITHICHTAINHANLAODONGNGHENGHIEP = "rptBH_QT_BaoCaoQTChiGiaiThichTNLDNNBHXH";
        public const string RPT_BH_QUYETTOANQUY_GIAITHICHTROCAPHUUTRIXUATNGU = "rptBH_QT_BaoCaoQTChiGiaiThichTCHTXNBHXH";
        public const string RPT_BH_QUYETTOANQUY_THONGTRIXACNHANQUYETTOANBHXH = "rptBH_QuyetToan_BaoCaoQuyetToanThongChiBHXH";
        public const string PRINT_DANHSACHNLDNGHIVIEC = "rptBH_QuyetToan_DanhSachNguoiLaoDongNghiViec";
        public const string RPT_BH_QTC_QKPK_MACDINH_KEHOACH = "rptBH_QTC_QKPK_MacDinh_KeHoach";

        public const string RPT_BH_QUYETTOAN_BAOCAOQUYETTOANCHIBHXH = "rptBH_QuyetToan_BaoCaoQuyetToanChiCacCheDoBHXH";
        public const string RPT_BH_QUYETTOAN_QUYETTOANCHIBHXH = "rptBH_QuyetToan_QuyetToanChiCacCheDoBHXH";
        public const string RPT_BH_QUYETTOAN_BAOCAOQUYETTOANCHIBHXH_KCB = "rptBH_QuyetToan_BaoCaoQuyetToanChiKCBCacCheDoBHXH";
        public const string RPT_BH_QUYETTOAN_QUYETTOANCHIBHXH_KCB = "rptBH_QuyetToan_QuyetToanChiKCBCacCheDoBHXH";

        public const string RPT_BH_QUYETTOAN_BAOCAOQUYETTOANCHIKCB = "rptBH_QuyetToan_BaoCaoQuyetToanChiKCB";
        public const string RPT_BH_QUYETTOAN_QUYETTOANCHIKCB = "rptBH_QuyetToan_QuyetToanChiKCB";
        public const string RPT_BH_CAPPHAT_KEHOACH_TNQN = "rptBH_CapPhat_KeHoach_TNQN";
        public const string RPT_BH_CAPPHAT_KEHOACH_TNQN_NLD = "rptBH_CapPhat_KeHoach_TNQN_NLD";
        public const string RPT_BH_CAPPHAT_TONGHOP_TNQN = "rptBH_CapPhat_TongHop_TNQN";
        public const string RPT_BH_CAPPHAT_TONGHOP_TNQN_NLD = "rptBH_CapPhat_TongHop_TNQN_NLD";
        public const string RPT_BH_CAPPHAT_THONGTRI_TNQN = "rptBH_CapPhat_ThongTri_TNQN";
        public const string RPT_BH_CAPPHAT_THONGTRI_TNQN_NLD = "rptBH_CapPhat_ThongTri_TNQN_NLD";
        public const string RPT_BH_QTC_QKPQL_THONGTRILOAI1 = "rptBH_QTC_QKPQL_Thongtri_Loai1";
        public const string RPT_BH_QTC_QKPQL_THONGTRILOAI2 = "rptBH_QTC_QKPQL_Thongtri_Loai2";
        public const string RPT_BH_QTC_QKPQL_CHITIET = "rptBH_QTC_QKPQL_ChiTiet";
        public const string RPT_BH_QT_THONGTRI_KPKCB_BHYT = "rptBH_QuyetToan_ThongTri_KPKCB_HBHYT";

        public const string QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_QUY = "rpt_BH_Quyet_Toan_Thu_Nop_BHXH_BHYT_BHTN_Quy";
        public const string QUYET_TOAN_THU_NOP_BHXH_QUY = "rpt_BH_Quyet_Toan_Thu_Nop_BHXH_Quy";
        public const string QUYET_TOAN_THU_NOP_BHYT_QUY = "rpt_BH_Quyet_Toan_Thu_Nop_BHYT_Quy";
        public const string QUYET_TOAN_THU_NOP_BHTN_QUY = "rpt_BH_Quyet_Toan_Thu_Nop_BHTN_Quy";
        public const string QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_NAM = "rpt_BH_Quyet_Toan_Thu_Nop_BHXH_BHYT_BHTN_Nam";
        public const string QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_THEO_THOI_GIAN = "rpt_BH_Quyet_Toan_Thu_Nop_BHXH_BHYT_BHTN_Theo_Thoi_Gian";
        public const string QUYET_TOAN_THU_NOP_BHXH_THEO_THOI_GIAN = "rpt_BH_Quyet_Toan_Thu_Nop_BHXH_Theo_Thoi_Gian";
        public const string QUYET_TOAN_THU_NOP_BHYT_THEO_THOI_GIAN = "rpt_BH_Quyet_Toan_Thu_Nop_BHYT_Theo_Thoi_Gian";
        public const string QUYET_TOAN_THU_NOP_BHTN_THEO_THOI_GIAN = "rpt_BH_Quyet_Toan_Thu_Nop_BHTN_Theo_Thoi_Gian";
        public const string QUYET_TOAN_THU_BHXH = "rpt_BH_Quyet_Toan_Thu_BHXH";
        public const string QUYET_TOAN_THU_BHTN = "rpt_BH_Quyet_Toan_Thu_BHTN";
        public const string QUYET_TOAN_THU_BHYT_QUAN_NHAN = "rpt_BH_Quyet_Toan_Thu_BHYT_Quan_Nhan";
        public const string QUYET_TOAN_THU_BHYT_NLD = "rpt_BH_Quyet_Toan_Thu_BHYT_NLD";
        public const string QUYET_TOAN_THU_CHI_TONG_HOP = "rpt_BH_Quyet_Toan_Thu_Chi_Tong_Hop";
        public const string QUYET_TOAN_TONG_HOP_NAM = "rpt_BH_Quyet_Toan_Tong_Hop_Nam";
        public const string DU_TOAN_TONG_HOP_THU_CHI = "rpt_BH_Du_Toan_Tong_Hop_Thu_Chi";
        public const string GIAO_DU_TOAN_TONG_HOP_THU_CHI = "rpt_BH_Giao_Du_Toan_Tong_Hop_Thu_Chi";
        public const string DU_TOAN_DIEU_CHINH_BO_SUNG_THU_CHI = "rptBH_Dieu_Chinh_Du_Toan_Tong_Hop";
        public const string RPT_THONG_BAO_PHE_DUYET_QUYET_TOAN_NAM_TONG_HOP_THU_CHI = "rpt_BH_Phe_Duyet_Quyet_Toan_Tong_Hop_Thu_Chi";
        public const string RPT_Bao_Cao_TH_Quyet_Toan_Thu_Chi_BHXH_BHYT_BHTN = "rpt_BH_TH_Quyet_Toan_Thu_Chi_BHXH_BHYT_BHTN";
        public const string RPT_BH_DU_TOAN_KINH_PHI_CHUYEN_NAM_SAU = "rpt_BH_DuToanKinhPhiChuyenNamSau";

        //Thông tri bảo hiểm xã hội
        public const string QUYET_TOAN_THONG_TRI_TONG_HOP_THU_ALL = "rptBH_ThongTriTongHop_Thu_All";
        public const string QUYET_TOAN_THONG_TRI_XAC_NHAN_THU_ALL_NLD = "rptBH_ThongTriXacNhan_Thu_All_NLD";
        public const string QUYET_TOAN_THONG_TRI_XAC_NHAN_THU_ALL_NSD = "rptBH_ThongTriXacNhan_Thu_All_NSD";
        public const string QUYET_TOAN_THONG_TRI_TONG_HOP_THU_BHXH = "rptBH_ThongTriTongHop_Thu_BHXH";
        public const string QUYET_TOAN_THONG_TRI_XAC_NHAN_THU_BHXH_NLD = "rptBH_ThongTriXacNhan_Thu_BHXH_NLD";
        public const string QUYET_TOAN_THONG_TRI_XAC_NHAN_THU_BHXH_NSD = "rptBH_ThongTriXacNhan_Thu_BHXH_NSD";
        public const string QUYET_TOAN_THONG_TRI_TONG_HOP_THU_BHYT = "rptBH_ThongTriTongHop_Thu_BHYT";
        public const string QUYET_TOAN_THONG_TRI_XAC_NHAN_THU_BHYT_NLD = "rptBH_ThongTriXacNhan_Thu_BHYT_NLD";
        public const string QUYET_TOAN_THONG_TRI_XAC_NHAN_THU_BHYT_NSD = "rptBH_ThongTriXacNhan_Thu_BHYT_NSD";
        public const string QUYET_TOAN_THONG_TRI_TONG_HOP_THU_BHTN = "rptBH_ThongTriTongHop_Thu_BHTN";
        public const string QUYET_TOAN_THONG_TRI_XAC_NHAN_THU_BHTN_NLD = "rptBH_ThongTriXacNhan_Thu_BHTN_NLD";
        public const string QUYET_TOAN_THONG_TRI_XAC_NHAN_THU_BHTN_NSD = "rptBH_ThongTriXacNhan_Thu_BHTN_NSD";
        public const string QUYET_TOAN_THONG_TRI_TONG_HOP_THU_ALL_THC = "rptBH_ThongTriTongHop_Thu_All_TongHopChung";
        public const string QUYET_TOAN_THONG_TRI_TONG_HOP_THU_ALL_THC_NLD = "rptBH_ThongTriTongHop_Thu_All_TongHopChung_NLD";
        public const string QUYET_TOAN_THONG_TRI_TONG_HOP_THU_ALL_THC_NSD = "rptBH_ThongTriTongHop_Thu_All_TongHopChung_NSD";

        public const string QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN_NLD = "rpt_BH_Quyet_Toan_Thu_Mua_BHYT_Than_Nhan_NLD";
        public const string QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN = "rpt_BH_Quyet_Toan_Thu_Mua_BHYT_Than_Nhan";
        public const string QUYET_TOAN_THU_MUA_BHYT_BHYT_HSSV_HVQS_SQDB = "rpt_BH_Quyet_Toan_Thu_Mua_BHYT_HSSV_HVQS_SQDB";

        public const string RPT_BH_QTC_QKPK_TNKDQKCBBHYTQNTT_THONGTRILOAI1 = "rptBH_QTC_QKPK_TNKDQKCBBHYTQNTT_Thongtri_Loai1";
        public const string RPT_BH_QTC_QKPK_TNKDQKCBBHYTQNTT_THONGTRILOAI2 = "rptBH_QTC_QKPK_TNKDQKCBBHYTQNTT_Thongtri_Loai2";
        public const string RPT_BH_QTC_QKPK_MSTT_THONGTRILOAI1 = "rptBH_QTC_QKPK_MSTT_Thongtri_Loai1";
        public const string RPT_BH_QTC_QKPK_MSTT_THONGTRILOAI2 = "rptBH_QTC_QKPK_MSTT_Thongtri_Loai2";
        public const string RPT_BH_QTC_QKPK_HTBHTNTT_THONGTRILOAI1 = "rptBH_QTC_QKPK_HTBHTNTT_Thongtri_Loai1";
        public const string RPT_BH_QTC_QKPK_HTBHTNTT_THONGTRILOAI2 = "rptBH_QTC_QKPK_HTBHTNTT_Thongtri_Loai2";

        public const string RPT_BH_QTC_QKPK_TSDK_THONGTRILOAI1 = "rptBH_QTC_QKPK_TSDK_Thongtri_Loai1";
        public const string RPT_BH_QTC_QKPK_TSDK_THONGTRILOAI2 = "rptBH_QTC_QKPK_TSDK_Thongtri_Loai2";
        public const string RPT_BH_QTC_QKPK_HSSV_THONGTRILOAI1 = "rptBH_QTC_QKPK_HSSV_Thongtri_Loai1";
        public const string RPT_BH_QTC_QKPK_HSSV_THONGTRILOAI2 = "rptBH_QTC_QKPK_HSSV_Thongtri_Loai2";
        public const string RPT_BH_QTC_QKPK_NLD_THONGTRILOAI1 = "rptBH_QTC_QKPK_NLD_Thongtri_Loai1";
        public const string RPT_BH_QTC_QKPK_NLD_THONGTRILOAI2 = "rptBH_QTC_QKPK_NLD_Thongtri_Loai2";
        public const string RPT_BH_QTC_QKPK_NLD_KEHOACH = "rptBH_QTC_QKPK_NLD_KeHoach";
        public const string RPT_BH_QTC_QKPK_HSSV_NLD_KEHOACH = "rptBH_QTC_QKPK_HSSV_NLD_KeHoach";
        public const string RPT_BH_QTC_QKPK_TSDK_KEHOACH = "rptBH_QTC_QKPK_TSDK_KeHoach";
        public const string RPT_BH_QTC_NHAN_VA_QTKP = "rptBH_QTC_NHAN_QT_KinhPhi";

        public const string RPT_BH_QTC_QKPK_TNKDQKCBBHYTQNTT_KEHOACH = "rptBH_QTC_QKPK_TNKDQKCBBHYTQNTT_KeHoach";
        public const string RPT_BH_QTC_QKPK_MSTT_KEHOACH = "rptBH_QTC_QKPK_MSTT_KeHoach";

        public const string RPT_BH_QTC_QKPK_HTBHTNTT_KEHOACH = "rptBH_QTC_QKPK_HTBHTNTT_KeHoach";

        public const string RPT_BH_QTC_NKPQL_CHITIET = "rptBH_QTC_NKPQL_ChungTu_ChiTiet";
        public const string RPT_BH_QTC_NKPQL_CHITIET_PHULUC = "rptBH_QTC_NKPQL_ChungTu_ChiTiet_PhuLuc";

        public const string RPT_BH_QTC_NKPK_HSSV_NLD_CHITIET = "rptBH_QTC_NKPK_HSSV_NLD_ChungTu_ChiTiet";
        public const string RPT_BH_QTC_NKPK_TSDK_CHITIET = "rptBH_QTC_NKPK_TSDK_ChungTu_ChiTiet";
        public const string RPT_BH_QTC_NKPK_HSSV_DONVI_PHULUC = "rptBH_QTC_NKPK_HSSV_DonVi_PhuLuc";
        public const string RPT_BH_QTC_NKPK_NLD_DONVI_PHULUC = "rptBH_QTC_NKPK_NLD_DonVi_PhucLuc";
        public const string RPT_BH_QTC_NKPK_TSDK_DONVI_PHULUC = "rptBH_QTC_NKPK_TSDK_DonVi_PhucLuc";
        public const string RPT_BH_QTC_QKPK_QYDV_KEHOACH = "rptBH_QTC_QKPK_QYDN_KeHoach";
        public const string RPT_BH_QTC_KCB_QUANY_DONVI = "rptBH_QTC_KCB_QuanY_DonVi";
        public const string RPT_BH_QTC_KCB_TONGHOPCACDONVICHIPHIKCB = "rptBH_QTC_KCB_TongHopCacDonviChiPhiKCB";
        public const string RPT_BH_QTC_KCB_THONGTRIQUYETTOANCHIKCB = "rptBH_QTC_KCB_ThongTriQuyetToanChiKCB";
        public const string RPT_BH_QTC_KCB_THONGTRIQUYETTOANCHIKCBTHC = "rpt_BH_QuyetToan_Qkcb_TongHopChi";
        public const string RPT_BH_QTC_NKPK_KCBBHYTQN_DONVI_PHULUC = "rptBH_QTC_NKPK_KCBBHYTQN_DonVi_PhucLuc";
        public const string RPT_BH_QTC_NKPK_MSTTBYT_DONVI_PHULUC = "rptBH_QTC_NKPK_MSTTBYT_DonVi_PhucLuc";
        public const string RPT_BH_QTC_NKPK_CHTBHTN_DONVI_PHULUC = "rptBH_QTC_NKPK_CHTBHTN_DonVi_PhucLuc";
        public const string RPT_BH_QTC_NKPK_KCBBHYTQN_CHITIET = "rptBH_QTC_NKPK_KCBBHYTQN_ChungTu_ChiTiet";
        public const string RPT_BH_QTC_NKPK_STTBYT_CHITIET = "rptBH_QTC_NKPK_STTBYT_ChungTu_ChiTiet";
        public const string RPT_BH_QTC_NKPK_CHTBHTN_CHITIET = "rptBH_QTC_NKPK_CHTBHTN_ChungTu_ChiTiet";

        public const string RPT_BHXH_KHC_KCB_QYDV_CHITIET = "rptBHXH_KHC_KCB_QYDV_ChiTiet";
        public const string RPT_BHXH_KHC_KCB_QYDV_PHUlUC = "rptBHXH_KHC_KCB_QYDV_PhucLuc";
        // Bao Hiem - Luong
        public const string RPT_LUONG_BHXH_TRO_CAP_OM_DAU = "RPT_LUONG_BHXH_TRO_CAP_OM_DAU";
        public const string RPT_LUONG_BHXH_TRO_CAP_THAI_SAN = "RPT_LUONG_BHXH_TRO_CAP_THAI_SAN";
        public const string RPT_LUONG_BHXH_TRO_CAP_TNLD = "RPT_LUONG_BHXH_TRO_CAP_TNLD";
        public const string RPT_LUONG_BHXH_TRO_CAP_HUU_TRI = "RPT_LUONG_BHXH_TRO_CAP_HUU_TRI";
        public const string RPT_LUONG_BHXH_TRO_CAP_XUAT_NGU = "RPT_LUONG_BHXH_TRO_CAP_XUATNGU";

        public const string BH_THAM_DINH_QUYET_TOAN_CHI_CHE_DO_BHXH = "rpt_BH_ThamDinhQuyetToan_ChiCheDoBHXH";
        public const string BH_THAM_DINH_QUYET_TOAN_CHI_CSSK_HSSV = "rpt_BH_ThamDinhQuyetToan_ChiCSSK_HSSV";
        public const string BH_THAM_DINH_QUYET_TOAN_CHI_CSSK_NLD = "rpt_BH_ThamDinhQuyetToan_ChiCSSK_NLD";
        public const string BH_THAM_DINH_QUYET_TOAN_CHI_KCB_QYDV = "rpt_BH_ThamDinhQuyetToan_ChiKCB_QuanYDonVi";
        public const string BH_THAM_DINH_QUYET_TOAN_CHI_MUA_SAM_TTBYT = "rpt_BH_ThamDinhQuyetToan_ChiMuaSamTTBYT";
    }

    public struct PROJECT_STATUS
    {
        public static string KHOI_TAO = "KhoiTao";
        public static string KE_HOACH_TRUNG_HAN = "KHTH";
        public static string CHU_TRUONG_DAU_TU = "CTDauTu";
        public static string PHE_DUYET_DU_AN = "PDDA";
        public static string THIET_KE_THI_CONG_TDT = "TKTC";
        public static string KH_LUA_CHON_NHA_THAU = "KHLCNT";
        public static string QD_LUA_CHON_NHA_THAU = "QDLCNT";
        public static string THUC_HIEN = "THUC_HIEN";
        public static string KET_THUC = "KET_THUC";
    }

    public struct VDT_INITIAL_NAME
    {
        public static string CHU_TRUONG_DAU_TU = "CTĐT";
        public static string PHE_DUYET_DU_AN = "PDDA";
        public static string THIET_KE_THI_CONG_TDT = "TKTC";
        public static string KH_LUA_CHON_NHA_THAU = "KHLCNT";
        public static string QD_LUA_CHON_NHA_THAU = "QDLCNT";
    }

    public class NAM_NGAN_SACH
    {
        public const int TONG_HOP = 0;
        public const int NAM_NAY = 2;
        public const int NAM_TRUOC_CHUYEN_SANG_DA_CAP = 1;
        public const int NAM_TRUOC_CHUYEN_SANG = 3;
        public const int NAM_TRUOC_CHUYEN_SANG_CHUA_CAP = 4;
        public const int NAM_KHAC = 5;
    }

    public struct ExportColumnHeader
    {
        public const string STT = "STT";
        public const string NOIDUNG = "NOIDUNG";
        public const string STT1 = "STT1";
        public const string STT2 = "STT2";
        public const string MA_CBO = "MA_CBO";
        public const string MA_CAN_BO = "MaCanBo";
        public const string SO_CAN_BO = "SoCanBo";
        public const string TEN_CAN_BO = "TenCanBo";
        public const string TEN_CANBO = "Ten_CanBo";
        public const string CAP_BAC = "CapBac";
        public const string SO_TAI_KHOAN = "SoTaiKhoan";
        public const string NGAN_HANG = "NganHang";
        public const string NOI_DUNG = "NoiDung";
        public const string NAM_THAM_NIEN = "NamThamNien";
        public const string NGAY_NHAP_NGU = "NgayNhapNgu";
        public const string NGAY_XUAT_NGU = "NgayXuatNgu";
        public const string NGAY_TAI_NGU = "NgayTaiNgu";
        public const string DOI_TUONG = "DoiTuong";
        public const string SO_NGUOI = "SoNguoi";
        public const string IS_PARENT = "IsParent";
        public const string DON_VI = "DonVi";
        public const string TEN_NGACH = "TenNgach";
        public const string DEM = "Dem";
        public const string MA_HIEU = "MaHieuCanBo";
        public const string MA_CAP_BAC = "MaCapBac";
        public const string MA_CAP_BAC_PARENT = "MaCapBacParent";
        public const string MA_CHUCVU = "MaChucVu";
        public const string HS_CHUCVU = "HSChucVu";
        public const string SOLUONG = "SoSoLuong";
        public const string STK = "Stk";
        public const string STK1 = "STK1";
        public const string STK2 = "STK2";
        public const string PARENT = "Parent";
        public const string MA_DONVI = "MaDonVi";
        public const string LUONG_CO_BAN = "LuongCoBan";
        public const string HS_LUONG = "HSLUONG";
        public const string IS_HEADER = "IsHeader";
        public const string IS_TOTAL = "IsTotal";
        public const string IS_FIRST_ROW = "IsFirstRow";
        public const string GIA_TRI = "GiaTri";
        public const string THANH_TIEN = "THANHTIEN";
        public const string THANH_TIEN1 = "THANHTIEN1";
        public const string THANH_TIEN2 = "THANHTIEN2";
        public const string GTPT_DG_SN = "GTPT_DG_SN";
        public const string THANH_TIEN_TAI_DV = "THANH_TIEN_TAI_DV";
        public const string THANH_TIEN_QUA_TK = "THANH_TIEN_QUA_TK";
        public const string THANG = "Thang";
        public const string GIAM_TRU_PHU_THUOC = "GIAM_TRU_PHU_THUOC";  //GTPT_SN * GTPT_DG
        public const string NOTE = "Note";
        public const string TIEN_AN = "TienAn";
        public const string DINH_MUC = "DinhMuc";
        public const string SO_NGAY = "SoNgay";
        public const string DV_TINH = "Dv_Tinh";
        public const string NHAN = "Nhan";
        public const string BANG = "Bang";
        public const string LUONG_THANG_SUM = "LUONGTHANG_SUM";
        public const string THUONG_TT = "THUONG_TT";
        public const string THUNHAPKHAC_TT = "THUNHAPKHAC_TT";
        public const string BHCN_TT = "BHCN_TT";
        public const string PCCT_TT = "PCCT_TT";
        public const string TINHTHUE = "TINHTHUE";
        public const string MADONVI = "MADONVI";
        public const string TA_TRUCQY_DG1_TT = "TA_TRUCQY_DG1_TT";
        public const string TA_TRUCQY_DG2_TT = "TA_TRUCQY_DG2_TT";
        public const string TA_TRUCQY_DG3_TT = "TA_TRUCQY_DG3_TT";
        public const string TA_TRUCQY_DG4_TT = "TA_TRUCQY_DG4_TT";
        public const string TA_TRUCTRAI_DG_TT = "TA_TRUCTRAI_DG_TT";
        public const string TA_DOCHAI_DG_TT = "TA_DOCHAI_DG_TT";
        public const string TA_BB_DG_TT = "TA_BB_DG_TT";
        public const string TA_OM_DG_TT = "TA_OM_DG_TT";
        public const string TNN = "Tnn";
        public const string XAU_NOI_MA = "XauNoiMa";
        public const string NamTN = "NamTN";
        public const string SUM_TL_BHXH = "SUM_TL_BHXH";
        public const string SUM_BHXH = "SUM_BHXH";
        public const string SUM_BHYT = "SUM_BHYT";
        public const string SUM_BHTN = "SUM_BHTN";
        public const string SUM_ROW = "SUM_ROW";

    }

    public class XAU_NOI_MA
    {
        public const string TIEN_LUONG = "1010000-010-011-6000";
        public const string PHU_CAP_LUONG = "1010000-010-011-6100";
    }

    public class MA_CB
    {
        public const string SQ = "1";
        public const string QNCN = "2";
        public const string CNVC = "3.3";
        public const string LDHD = "4";
        public const string HCY = "5";
        public const string CMKTCY = "6";
        public const string HDLD = "3.4";
    }

    public struct MA_CAP_BAC
    {
        public const string THIEU_UY = "111";
        public const string TRUNG_UY = "112";
        public const string THUONG_UY = "113";
        public const string DAI_UY = "114";
        public const string THIEU_TA = "121";
        public const string TRUNG_TA = "122";
        public const string THUONG_TA = "123";
        public const string DAI_TA = "124";
        public const string TUONG = "13";
        public const string BINH_NHI = "01";
        public const string BINH_NHAT = "02";
        public const string HA_SI = "03";
        public const string TRUNG_SI = "04";
        public const string THUONG_SI = "05";
        public const string QNCN1 = "2";
        public const string VCQP = "3.3";
        public const string CNQP = "3.1";
        public const string CCQP = "3.2";
        public const string CNVC = "3";
        public const string SI_QUAN = "1";
        public const string LDHD = "43";
        public const string THIEU_UY_CN = "211";
        public const string TRUNG_UY_CN = "212";
        public const string THUONG_UY_CN = "213";
        public const string DAI_UY_CN = "214";
        public const string THIEU_TA_CN = "221";
        public const string TRUNG_TA_CN = "222";
        public const string THUONG_TA_CN = "223";
        public const string HCY_BAC1 = "511";
        public const string HCY_BAC2 = "512";
        public const string HCY_BAC3 = "513";
        public const string HCY_BAC4 = "514";
        public const string HCY_BAC5 = "521";
        public const string HCY_BAC6 = "522";
        public const string HCY_BAC7 = "523";
        public const string HCY_BAC8 = "524";
        public const string HCY_BAC9 = "53";
        public const string HCY_BAC10 = "532";
        public const string CMKYCY_THIEUUY = "611";
        public const string CMKYCY_TRUNGUY = "612";
        public const string CMKYCY_THUONGUY = "613";
        public const string CMKYCY_DAIUY = "614";
        public const string CMKYCY_THIEUTA = "621";
        public const string CMKYCY_TRUNGTA = "622";
        public const string CMKYCY_THUONGTA = "623";
    }

    public struct TEN_CAP_BAC
    {
        public const string THIEU_UY = "ThieuUy";
        public const string TRUNG_UY = "TrungUy";
        public const string THUONG_UY = "ThuongUy";
        public const string DAI_UY = "DaiUy";
        public const string THIEU_TA = "ThieuTa";
        public const string TRUNG_TA = "TrungTa";
        public const string THUONG_TA = "ThuongTa";
        public const string DAI_TA = "DaiTa";
        public const string TUONG = "Tuong";
        public const string BINH_NHI = "BinhNhi";
        public const string BINH_NHAT = "BinhNhat";
        public const string HA_SI = "HaSi";
        public const string TRUNG_SI = "TrungSi";
        public const string THUONG_SI = "ThuongSi";
        public const string QNCN1 = "QNCN";
        public const string VCQP = "VCQP";
        public const string CNQP = "CNQP";
        public const string CCQP = "CCQP";
        public const string LDHD = "LDHD";
        public const string THIEU_UY_CN = "ThuongUyCn";
        public const string TRUNG_UY_CN = "TrungUyCn";
        public const string THUONG_UY_CN = "ThuongUyCn";
        public const string DAI_UY_CN = "DaiUyCn";
        public const string THIEU_TA_CN = "ThieuTaCn";
        public const string TRUNG_TA_CN = "TrungTaCn";
        public const string THUONG_TA_CN = "ThuongTaCn";
    }

    public struct MA_TANG_GIAM
    {
        public const string QUAN_SO_THANG_TRUOC = "100";
        public const string QUAN_SO_QT_THANG_NAY = "700";
        public const string THANG = "Thang";
        public const string NAM = "Nam";
        public const string MA_CAN_BO = "MaCbo";
        public const string MA_HIEU_CAN_BO = "MaHieuCanBo";
        public const string QUAN_SO_QT_TRONG_THANG = "400";
        public const string QUAN_SO_TANG_TRONG_THANG = "2";
        public const string QUAN_SO_GIAM_TRONG_THANG = "3";
        public const string QUAN_SO_BO_SUNG_THANG_TRUOC = "500";
        public const string QUAN_SO_CHUA_QT_THANG_NAY = "600";
        public const string TANG_QUAN_HAM_MA_250 = "250";
        public const string TANG_QUAN_HAM_MA_350 = "350";
        public const string CHUYEN_CHE_DO_280 = "280";
        public const string CHUYEN_CHE_DO_380 = "380";
        public const string GIAM_NOI_BO = "390";
    }

    public struct LOAI_CHUNG_TU
    {
        public static string CHU_DAU_TU = "000";
        public static string NAM_TRUOC_CHUYEN_SANG = "NAM_TRUOC_CHUYEN";

        public static string KE_HOACH_VON_NAM = "KHVN";
        public static string KHVN_KHOBAC = "101";
        public static string KHVN_LENHCHI = "102";
        public static string KHVN_TONKHOANTAIDONVI = "103";

        public static string KE_HOACH_VON_UNG = "KHVU";
        public static string KHVU_KHOBAC = "121a";
        public static string KHVU_LENHCHI = "122a";
        public static string KHVU_TONKHOANTAIDONVI = "123a";

        public static string CAP_THANH_TOAN = "THANH_TOAN";
        public static string TT_THANHTOAN_KHOBAC = "201";
        public static string TT_THANHTOAN_LENHCHI = "202";
        public static string TT_THANHTOAN_TONKHOANDONVI = "203";
        public static string TT_UNG_KHOBAC = "211a";
        public static string TT_UNG_LENHCHI = "212a";
        public static string TT_UNG_TONKHOANDONVI = "213a";

        public static string TT_THUHOI_KHOBAC_NAMTRUOC = "211b1";
        public static string TT_THUHOI_LENHCHI_NAMTRUOC = "212b1";
        public static string TT_THUHOI_TONKHOANDONVI_NAMTRUOC = "213b1";
        public static string TT_THUHOI_KHOBAC_NAMNAY = "211b2";
        public static string TT_THUHOI_LENHCHI_NAMNAY = "212b2";
        public static string TT_THUHOI_TONKHOANDONVI_NAMNAY = "213b2";

        public static string QUYET_TOAN = "QUYET_TOAN";
        public static string QT_KHOBAC_CHUYENNAMTRUOC = "111";
        public static string QT_LENHCHI_CHUYENNAMTRUOC = "112";
        public static string QT_TONKHOANDONVI_CHUYENNAMTRUOC = "113";

        public static string QT_UNG_KHOBAC_CHUYENNAMTRUOC = "131";
        public static string QT_UNG_LENHCHI_CHUYENNAMTRUOC = "132";
        public static string QT_UNG_TONKHOANDONVI_CHUYENNAMTRUOC = "133";

        public static string QT_KHOBAC_DIEUCHINHGIAM = "211c";
        public static string QT_LENHCHI_DIEUCHINHGIAM = "212c";
        public static string QT_TONKHOANDONVI_DIEUCHINHGIAM = "213c";

        public static string QT_LUYKE_KHVN_KHOBAC = "301";
        public static string QT_LUYKE_KHVN_LENHCHI = "302";
        public static string QT_LUYKE_KHVN_TONKHOANDONVI = "303";

        public static string QT_LUYKE_TT_KHVN_KHOBAC = "403";
        public static string QT_LUYKE_TT_KHVN_LENHCHI = "404";
        public static string QT_LUYKE_TT_KHVN_TONKHOANDONVI = "405";

        public static string QT_LUYKE_TU_CHUATH_KHOBAC = "413a";
        public static string QT_LUYKE_TU_CHUATH_LENHCHI = "414a";
        public static string QT_LUYKE_TU_CHUATH_TONKHOANDONVI = "415a";

        public static string QT_LUYKE_KHVU_KHOBAC = "321a";
        public static string QT_LUYKE_KHVU_LENHCHI = "322a";
        public static string QT_LUYKE_KHVU_TONKHOANDONVI = "323a";

        public static string QT_LUYKE_TT_KHVU_KHOBAC = "403";
        public static string QT_LUYKE_TT_KHVU_LENHCHI = "404";
        public static string QT_LUYKE_TT_KHVU_TONKHOANDONVI = "405";

        public static string QT_LUYKE_UNGTRUOC_CHUATHUHOI_KHOBAC = "321b";
        public static string QT_LUYKE_UNGTRUOC_CHUATHUHOI_LENHCHI = "322b";
        public static string QT_LUYKE_UNGTRUOC_CHUATHUHOI_TONKHOANDONVI = "323b";

        public static string QT_LUYKE_TTKLHT_CHUA_PHANBO_KHOBAC = "403a";
        public static string QT_LUYKE_TTKLHT_CHUA_PHANBO_LENHCHI = "404a";
        public static string QT_LUYKE_TTKLHT_CHUA_PHANBO_TONKHOANDONVI = "405a";
    }

    public enum DtDuLieuNhan
    {
        Normal = 0,
        Import = 1,
        Input = 2
    }

    public enum LoaiSave
    {
        SAVE_AND_CLEAR = 1,
        SAVE_AND_CLOSE = 2,
        SAVE_AND_COPY = 3
    }

    public class STORAGE_CONFIG
    {
        public const string STORAGE_TYPE = "STORAGE_TYPE";
        public const string FTP_HOST = "FTP_HOST";
        public const string FTP_USER = "FTP_USER";
        public const string FTP_PASSWORD = "FTP_PASSWORD";
        public const string FTP_PORT = "FTP_PORT";
    }

    public enum ConfigurationUploadFile
    {
        HTTP_HOST,
        HTTP_USER,
        HTTP_PASSWORD,
        HTTP_PORT,
        FTP_HOST,
        FTP_USER,
        FTP_PASSWORD,
        FTP_PORT,
    }

    public struct TrangThaiBangLuongThang
    {
        public static string SU_DUNG = "Đang sử dụng";
        public static string KHONG_SU_DUNG = "Không sử dụng";
    }

    public enum LoaiKeHoachVon
    {
        KHVN = 1,
        KHVU = 2,
        KHVN_NAMTRUOC = 3,
        KHVU_NAMTRUOC = 4
    }

    public struct OldColumnName
    {
        public const string SSL = "SSL";
        public const string MADT = "MADT";
        public const string DVI = "DVI";
        public const string MADV = "MADV";
        public const string SOTK = "SOTK";
        public const string CBAC = "CBAC";
        public const string HOTEN = "HOTEN";
        public const string NHAPNGU = "NHAPNGU";
        public const string XUATNGU = "XUATNGU";
        public const string TAINGU = "TAINGU";
        public const string HSLG = "HSLG";
        public const string HSBL = "HSBL";
        public const string LCB = "LCB";
        public const string LCV = "LCV";
        public const string HSCV = "HSCV";
        public const string VK = "VK";
        public const string KV = "KV";
        public const string TC1 = "TC1";
        public const string TC2 = "TC2";
        public const string TC3 = "TC3";
        public const string TC4 = "TC4";
        public const string TC5 = "TC5";
        public const string AN1N = "AN1N";
        public const string THD = "THD";
        public const string NU = "NU";
        public const string TRUKHAC = "TRUKHAC";
        public const string TG = "TG";
        public const string THNHAPTT = "THNHAPTT";
        public const string DUOCNHAN = "DUOCNHAN";
        public const string COBHTN = "COBHTN";
        public const string COTN = "COTN";
        public const string TRICHLG = "TRICHLG";
        public const string BHTN = "BHTN";
        public const string BHYT = "BHYT";
        public const string BHXH = "BHXH";
        public const string GIAMBTHAN = "GIAMBTHAN";
        public const string GIAMPT = "GIAMPT";
        public const string CONPHAINOP = "CONPHAINOP";
        public const string THUONG = "THUONG";
        public const string THULAOKHAC = "THULAOKHAC";
        public const string GIAMKHAC = "GIAMKHAC";
        public const string GIAMTHUE = "GIAMTHUE";
        public const string THUNGOAI = "THUNGOAI";
        public const string KQTINHTHUE = "KQTINHTHUE";
        public const string THUDAUVAO = "THUDAUVAO";
        public const string NOPDAUVAO = "NOPDAUVAO";
        public const string DCGIAMTRU = "DCGIAMTRU";
        public const string DCTHUNHAP = "DCTHUNHAP";
        public const string DANOP = "DANOP";
        public const string PHAINOP = "PHAINOP";
        public const string THUNHAPTT = "THUNHAPTT";
        public const string SONGUOIPT = "SONGUOIPT";
        public const string LUONG = "LUONG";
        public const string TONGTNCT = "TONGTNCT";
        public const string TONGGIAM = "TONGGIAM";
        public const string TRN = "TRN";
        public const string TVK = "TVK";
        public const string TBL = "TBL";
        public const string TTN = "TTN";
        public const string TDB = "TDB";
        public const string TKV = "TKV";
        public const string TCV = "TCV";
        public const string TTCK = "TTCK";
        public const string TRUAN = "TRUAN";
        public const string DUOCNHANTK = "DUOCNHANTK";
        public const string DUOCNHANDV = "DUOCNHANDV";
        public const string SSS = "SSS";
        public const string STT = "STT";
        public const string MARK = "MARK";
        public const string KCONGVU = "KCONGVU";
        public const string TTT = "TTT";
        public const string NDTHUKHAC = "NDTHUKHAC";
        public const string NDNOPTHUE = "NDNOPTHUE";
        public const string TAMHUY = "TAMHUY";
        public const string SOTNNGHE = "SOTNNGHE";
        public const string LUUTK = "LUUTK";
        public const string DCTHUENOP = "DCTHUENOP";
    }

    public enum SalaryPrintType
    {
        BC_CHI_TIET_QUYET_TOAN_TX_SO_LIEU = 0,
        BC_CHI_TIET_QUYET_TOAN_TX_THEO_COT = 1,
        BC_CHI_TIET_QUYET_TOAN_TX_THEO_CACH_TINH_LUONG = 2,
        BC_QUYET_TOAN_LUONG_PHU_CAP = 3,
        BC_QUYET_TOAN_TIEN_AN = 4,
        BC_QUYET_TOAN_BAO_HIEM = 5,
        BC_QUYET_TOAN_RA_QUAN = 6,
    }

    public enum DuLieuDuToan
    {
        TuChi = 1,
        HienVat = 2,
        DuPhong = 3,
        HangNhap = 4,
        HangMua = 5,
        PhanCap = 6
    }

    public enum DuToanRowType
    {
        RowCha = 0,
        RowNhanPhanBoOrTong = 1,
        RowConLai = 2,
        RowChiTiet = 3
    }

    public struct NhomQncn
    {
        public const string SOCAPN1 = "Sơ cấp (Nhóm 1)";
        public const string SOCAPN2 = "Sơ cấp (Nhóm 2)";
        public const string TRUNGCAPN1 = "Trung cấp (Nhóm 1)";
        public const string TRUNGCAPN2 = "Trung cấp (Nhóm 2)";
        public const string CAOCAPN1 = "Cao cấp (Nhóm 1)";
        public const string CAOCAPN2 = "Cao cấp (Nhóm 2)";
    }

    public struct LoaiCanBoKehoach
    {
        public const string THAYDOIQH_NANGLUONG = "Thay đổi quân hàm, nâng lương";
        public const string NGHIHUU = "Nghỉ hưu";
        public const string RAQUAN_XUATNGU = "Ra quân, xuất ngũ";
        public const string TANG_THAMNIEN = "Tăng thâm niên";
    }

    public struct NgachLuongBhxh
    {
        public const string SQ = "SQ";
        public const string SQ2 = "Sĩ Quan";
        public const string QNCN = "QNCN";
        public const string HSQ_BS = "HSQ_BS";
        public const string VCQP = "VCQP";
        public const string LDHD = "LDHD";
        public const string VCQP2 = "CNVCQP";
        public const string LDHD2 = "HĐLĐ";
    }

    public class LoaiTongHopBhxh
    {
        public const int CTTL_DOI_TUONG = 1;
        public const int CTTL_DON_VI = 2;
        public const string TEN_CTTL_DOI_TUONG = "Chi tiết theo Loại đối tượng";
        public const string TEN_CTTL_DON_VI = "Chi tiết theo đơn vị";

        public static string GetNameLoai(int loai)
        {
            switch (loai)
            {
                case CTTL_DOI_TUONG:
                    return TEN_CTTL_DOI_TUONG;
                case CTTL_DON_VI:
                    return TEN_CTTL_DON_VI;
                default:
                    return string.Empty;
            }

        }
    }


    public class CoNamLamViec
    {
        public const int HAS_YEAR = 1;
        public const int HAS_NO_YEAR = 2;
    }

    public struct EstimateColumn
    {
        public const string FTUCHI = "FTuChi";
        public const string FRUTKBNN = "FRutKBNN";
        public const string FCAPBANGTIEN = "FCapBangTien";
        public const string FHIENVAT = "FHienVat";
        public const string FPHANCAP = "FPhanCap";
        public const string FHANGNHAP = "FHangNhap";
        public const string FHANGMUA = "FHangMua";
        public const string FDUPHONG = "FDuPhong";
        public const string FTONKHO = "FTonKho";
    }

    public struct BooleanString
    {
        public const string TRUE = "TRUE";
        public const string FALSE = "FALSE";
    }

    public struct LOAI_PHU_CAP
    {
        public static string TONG_CONG = "TONG";
        public static string PHAI_TRU = "PHAITRU";
    }

    public struct LoaiPhuCapLuong
    {
        public const int QUYET_TOAN = 1;
        public const int NGHIEP_VU = 2;
    }

    public struct LoaiPhuCapLuongString
    {
        public const string QUYET_TOAN = "Phụ cấp Quyết toán";
        public const string NGHIEP_VU = "Phụ cấp Nghiệp vụ";
    }

    public static class SO_CU_TRUC_TIEP
    {
        public struct TypeName
        {
            public const string DU_TOAN = "Dự toán đặt hàng/mua sắm được duyệt";
            public const string QD_DAU_TU = "Quyết định đầu tư";
        }

        public enum TypeValue
        {
            DU_TOAN = 1,
            QD_DAU_TU = 2
        }

        // Feature Phương án nhập khẩu
        public const string CHUONG_TRINH = "CHUONG_TRINH"; // Chương trình
        public const string THONG_TIN_DU_AN = "THONG_TIN_DU_AN"; // Thông tin dự án
        public const string CHU_CHUONG_DAU_TU = "CHU_CHUONG_DAU_TU"; // Chủ trương đầu tư
        public const string QUYET_DINH_DAU_TU = "QUYET_DINH_DAU_TU"; // Quyết định đầu tư
    }

    public enum LoaiChungTu
    {
        TONG_HOP = 0,
        THUONG = 1
    }

    public enum LoaiNSBD
    {
        DAC_THU = 0,
        MHHV = 1
    }

    public class IMPORT_TABLE_NAME
    {
        public const string BK_CHUNGTU = "BK_ChungTu";
        public const string BK_CHUNGTUCHITIET = "BK_ChungTuChiTiet";
        public const string CP_CHUNGTU = "CP_ChungTu";
        public const string CP_CHUNGTUCHITIET = "CP_ChungTuChiTiet";
        public const string DANHMUC = "DanhMuc";
        public const string DM_CHUKY = "DM_ChuKy";
        public const string DT_CHUNGTU = "DT_ChungTu";
        public const string DT_CHUNGTUCHITIET = "DT_ChungTuChiTiet";
        public const string NS_DONVI = "NS_DonVi";
        public const string NS_MUCLUCNGANSACH = "NS_MucLucNganSach";
        public const string QS_CHUNGTU = "QS_ChungTu";
        public const string QS_CHUNGTUCHITIET = "QS_ChungTuChiTiet";
        public const string QS_MUCLUC = "QS_MucLuc";
        public const string QT_CHUNGTU = "QT_ChungTu";
        public const string QT_CHUNGTUCHITIET = "QT_ChungTuChiTiet";
        public const string QT_CHUNGTUCHITIET_GIAITHICH = "QT_ChungTuChiTiet_GiaiThich";
        public const string QT_CHUNGTUCHITIET_GIAITHICH_LUONGTRU = "QT_ChungTuChiTiet_GiaiThich_LuongTru";
        public const string SKT_CHUNGTU = "SKT_CHUNGTU";
        public const string SKT_CHUNGTU_CHITIET = "SKT_CHUNGTU_CHITIET";
        public const string SKT_MUCLUC = "SKT_MUCLUC";
        public const string SKT_MUCLUC_MAP = "SKT_MUCLUC_MAP";
        public const string SKT_SO_LIEU_CHITIET = "SKT_SO_LIEU_CHITIET";
    }

    public static class ATTACH_TYPE
    {
        public struct TypeName
        {
            public const string BANG_KHOI_LUONG = "Bảng khối lượng";
            public const string BIEN_BAN_NT = "Biên bản NT";
            public const string KHAC = "Khác";
        }

        public enum TypeValue
        {
            BANG_KHOI_LUONG = 1,
            BIEN_BAN_NT = 2,
            KHAC = 3
        }
    }

    public struct DinhDangPhuCapLuong
    {
        public const int TIEN = 0;
        public const int HE_SO = 1;
        public const int KHAC = 2;
    }

    public struct DinhDangPhuCapLuongString
    {
        public const string TIEN = "Tiền";
        public const string HE_SO = "Hệ số";
        public const string KHAC = "Khác";
    }

    public static class LOAI_KHV
    {
        public struct TypeName
        {
            public const string KE_HOACH_VON_NAM = "Kế hoạch vốn năm";
            public const string KE_HOACH_VON_UNG = "Kế hoạch vốn ứng";
            public const string KE_HOACH_VON_NAM_CHUYEN_SANG = "Kế hoạch năm trước chuyển sang";
            public const string KE_HOACH_VON_UNG_CHUYEN_SANG = "Kế hoạch ứng trước năm trước chuyển sang";
        }

        public enum Type
        {
            KE_HOACH_VON_NAM = 1,
            KE_HOACH_VON_UNG = 2,
            KE_HOACH_VON_NAM_CHUYEN_SANG = 3,
            KE_HOACH_VON_UNG_CHUYEN_SANG = 4
        }
    }

    public static class Loai_KHTT
    {
        public const int GIAIDOAN = 1;
        public const int NAM = 2;
    }

    public static class LOAI_QD_PDDA
    {
        public struct TypeName
        {
            public const string PHE_DUYET_DU_AN = "Phê duyệt dự án";
            public const string BC_KINH_TE_KY_THUAT = "Báo cáo kinh tế kỹ thuật";
        }

        public enum Type
        {
            PHE_DUYET_DU_AN = 1,
            BC_KINH_TE_KY_THUAT = 2
        }
    }

    public enum LOAI_BC_QUYET_TOAN
    {
        TONG_HOP_SO_LIEU = 1,
        PHAN_TICH_SO_LIEU = 2
    }

    public enum LOAI_TAB_QUYETTOAN
    {
        TONG_HOP_SO_LIEU = 0,
        PHAN_TICH_SO_LIEU = 1
    }

    public static class LOAI_QUYETTOAN_DUAN_HOANTHANH
    {
        public struct TypeName
        {
            public const string THEO_HANGMUC = "1";
            public const string THEO_GOITHAU = "2";

        }
    }

    public static class NHDAQDDauTuChiPhiHangMucModel_Loai
    {
        public const int CP = 1;
        public const int HM = 2;
    }

    public class RegexExpression
    {
        public const string REGEX_ALPHANUMERIC = @"^[a-zA-Z0-9]*$";
        public const string REGEX_EMAIL = @"^[a-zA-Z0-9]+(([-._]{1}[a-zA-Z0-9]+)*)@(([a-zA-Z0-9\-]+\.)+[a-zA-Z0-9]{2,})$";
        public const string REGEX_PHONE_FAX = @"^(([+]?([\s]?[0-9]+))|([(][+]?([\s]?[0-9]+)[)]))?([\s]?[(]?[0-9]+[)])?([-]?[\s]?[0-9])+$";
    }

    public static class THOIGIAN_CONGKHAI
    {
        public const int DAU_NAM = 0;
        public const int QUY_I = 3;
        public const int SAU_THANG = 6;
        public const int CHIN_THANG = 9;
        public const int CA_NAM = 12;
    }

    public static class LOAI_BAOCAO_CONGKHAI
    {
        public const int BIEUSO_01_QĐ_CKNS = 1;
        public const int BIEUSO_01_CKNS = 2;
        public const int BIEUSO_02_CKNS = 3;
        public const int BIEUSO_05_CKNS = 4;
        public const int BIEUSO_06_CKNS = 5;

    }

    public static class LOAI_BAOCAO_CONGKHAI_QUYETTOAN
    {
        public const int BIEUSO_01_QĐ_CKNS = 1;
        public const int BIEUSO_03_CKNS = 2;
        public const int BIEUSO_04a_CKNS = 30;
        public const int BIEUSO_04b_CKNS = 40;
        public const int BIEUSO_07_CKNS = 5;
        public const int BIEUSO_08a_CKNS = 6;
        public const int BIEUSO_08b_CKNS = 7;

    }

    public static class LOAI_BAOCAO_PAPB
    {
        public const int MAUSO1_TOBAOCAO = 1;
        public const int MAUSO1_PHULUCI = 2;
        public const int MAUSO1_PHULUCII = 3;
        public const int MAUSO2_KETQUA = 4;

    }

    public static class LOAI_DU_TOAN_FOREX
    {
        public struct TypeName
        {
            public const string LOAI_DU_TOAN_MUA_SAM = "Dự toán mua sắm được duyệt";
            public const string LOAI_DU_TOAN_DAT_HANG = "Dự toán đặt hàng được duyệt";
        }
        public enum Type
        {
            LOAI_DU_TOAN_MUA_SAM = 1,
            LOAI_DU_TOAN_DAT_HANG = 2
        }

        public struct GiaTri_DTCTN_PheDuyet
        {
            public const string MUA_SAM = "GIÁ TRỊ DTMS PHÊ DUYỆT";
            public const string DAT_HANG = "GIÁ TRỊ DTĐH PHÊ DUYỆT";
            public const string DEFAULT = "GIÁ TRỊ DTCTN PHÊ DUYỆT";
        }
    }

    public enum ReportCPTUKCBBHYT
    {
        KEHOACH_TNQN = 1,
        KEHOACH_TNQN_NLD = 2,
        TONGHOP_TNQN = 3,
        TONGHOP_TNQN_NLD = 4,
        THONGTRI_TNQN = 5,
        THONGTRI_TNQN_NLD = 6

    }

    public enum ReportDTDauNamTongHopDV
    {
        TATCA = 1,
        DTNAMKEHOACH = 2
    }

    public enum GetFileType
    {
        GetChildren,
        GetParent,
        GetSelf
    }
    public class InsuranceAllocationReportType
    {
        public static Dictionary<ReportCPTUKCBBHYT, string> AllocationReportTypeName = new Dictionary<ReportCPTUKCBBHYT, string>
        {
            {ReportCPTUKCBBHYT.KEHOACH_TNQN, "Kinh phí KCB BHYT quân nhân"},
            {ReportCPTUKCBBHYT.KEHOACH_TNQN_NLD, "Kinh phí KCB BHYT TNQN và NLĐ"},
            {ReportCPTUKCBBHYT.TONGHOP_TNQN, "Kinh phí KCB BHYT quân nhân"},
            {ReportCPTUKCBBHYT.TONGHOP_TNQN_NLD, "Kinh phí KCB BHYT TNQN và NLĐ"},
            {ReportCPTUKCBBHYT.THONGTRI_TNQN, "Kinh phí KCB BHYT quân nhân"},
            {ReportCPTUKCBBHYT.THONGTRI_TNQN_NLD, "Kinh phí KCB BHYT TNQN và NLĐ"}
        };
        public static Dictionary<string, ReportCPTUKCBBHYT> AllocationType = new Dictionary<string, ReportCPTUKCBBHYT>
        {
            {"1",ReportCPTUKCBBHYT.KEHOACH_TNQN},
            {"2",ReportCPTUKCBBHYT.KEHOACH_TNQN_NLD},
            {"3",ReportCPTUKCBBHYT.TONGHOP_TNQN},
            {"4",ReportCPTUKCBBHYT.TONGHOP_TNQN_NLD },
            {"5",ReportCPTUKCBBHYT.THONGTRI_TNQN},
            {"6",ReportCPTUKCBBHYT.THONGTRI_TNQN_NLD}
        };
        public static Dictionary<string, string> AllocationReportTitle2 = new Dictionary<string, string>
        {
            {"1","Cấp tạm ứng phí KCB BHYT Quân nhân quý {0} năm {1}"},
            {"2","Cấp tạm ứng kinh phí KCB BHYT TNQN và NLĐ quý {0} năm {1}"},
            {"3","Cấp tạm ứng kinh phí KCB BHYT Quân nhân"},
            {"4","Cấp tạm ứng kinh phí KCB BHYT TNQN và NLĐ"},
            {"5","Cấp tạm ứng kinh phí KCB BHYT Quân nhân"},
            {"6","Cấp tạm ứng kinh phí KCB BHYT TNQN và NLĐ"}
        };

    }

    public struct DefaultCPTUBHYTReportTitle
    {
        public const string THONG_TRI_TITLE_1 = "THÔNG TRI";
        public const string THONG_TRI_QN_TITLE_2 = "Cấp tạm ứng kinh phí KCB BHYT Quân nhân";
        public const string THONG_TRI_TNQN_TITLE_2 = "Cấp tạm ứng kinh phí KCB BHYT TNQN và NLĐ";
        public const string THONG_TRI_QN_TITLE_3 = "";
        public const string THONG_TRI_TNQN_TITLE_3 = "";

        public const string TONG_HOP_TITLE_1 = "THÔNG TRI TỔNG HỢP";
        public const string TONG_HOP_QN_TITLE_2 = "Cấp tạm ứng kinh phí KCB BHYT Quân nhân";
        public const string TONG_HOP_TNQN_TITLE_2 = "Cấp tạm ứng kinh phí KCB BHYT TNQN và NLĐ";
        public const string TONG_HOP_QN_TITLE_3 = "";
        public const string TONG_HOP_TNQN_TITLE_3 = "";

        public const string KE_HOACH_TITLE_1 = "KẾ HOẠCH";
        public const string KE_HOACH_QN_TITLE_2 = "Cấp tạm ứng kinh phí KCB BHYT Quân nhân";
        public const string KE_HOACH_TNQN_TITLE_2 = "Cấp tạm ứng kinh phí KCB BHYT TNQN và NLĐ";
        public const string KE_HOACH_QN_TITLE_3 = "";
        public const string KE_HOACH_TNQN_TITLE_3 = "";
    }

    public enum AllocationFunction
    {
        CAP_KINH_PHI = 1,
        CAP_TAM_UNG = 2,
        CAP_BO_SUNG = 3

    }

    public static class TargetAgencyHD4554
    {
        public const string DU_TOAN_PHANBO = "1";
        public const string THU_NOP_PHANBO = "2";
        public const string ALL = "3";
    }

}
