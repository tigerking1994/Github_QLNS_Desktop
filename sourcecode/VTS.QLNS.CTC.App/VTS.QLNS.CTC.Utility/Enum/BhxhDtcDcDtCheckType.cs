using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public struct DtDcDtBhxhLoaiChungTu
    {
        public const int BhxhChungTu = 1;
        public const int BhxhChungTuTongHop = 2;
    }

    public enum ReportDtDcDtType
    {
        DTDCCT = 1,
        DTDCTheoDonVi = 2,
        DTDCTheoDonViTongHop = 3,
    }

    public enum GetQuater
    {
        QuyI = 1,
        QuyII = 2,
        QuyIII = 3,
        QuyIV = 4
    }

    public struct TypeName
    {
        public const string QuyI = "Quý I";
        public const string QuyII = "Quý II";
        public const string QuyIII = "Quý III";
        public const string QuyIV = "Quý IV";
    }

    public enum DtDcDtCheckPrintType
    {
        DTDCCT = 1,
        DTDCTheoDonVi = 2,
        DTDCTheoDonViTongHop = 3,
    }
    public struct DtDcDtBhXhMLNS
    {
        public const string CHI_BHXH_KHOI_DU_TOAN = "9010001";
        public const string CHI_BHXH_KHOI_HACH_TOAN = "9010002";
        public const string CHI_KINH_PHI_QUAN_LY_BHXH = "9010003";
        public const string CHI_KINH_PHI_QUAN_Y_DON_VI_KHOI_DU_TOAN = "9010004";
        public const string CHI_KINH_PHI_QUAN_Y_DON_VI_KHOI_HACH_TOAN = "9010005";
        public const string CHI_KINH_PHI_TRUONG_SA_DK_KHOI_DU_TOAN = "9010006";
        public const string CHI_KINH_PHI_TRUONG_SA_DK_KHOI_HACH_TOAN = "9010007";
        public const string CHI_KINH_PHI_CHAM_SOC_SUC_KHOE_HSSV = "9050002";
        public const string CHI_KINH_PHI_CHAM_SOC_SUC_KHOE_NLD = "9050001";
        public const string CHI_TU_NGUON_KET_DU_QUY_KCB_BHYT_QN = "9040001";
    }

    public enum IDanhMucLoaiChi
    {
        // Chi các chế độ BHXH
        CHI_BHXH = 0,
        // Chi kinh phí quản lý BHXH, BHYT
        CHI_KINH_PHI_QUAN_LY = 1,
        // Chi kinh phí KCB tại quân y đơn vị 
        CHI_KINH_PHI_QUAN_Y = 2,
        // Chi kinh phí KCB tại Trường Sa
        CHI_KINH_PHI_TRUONG_SA = 3,
        // Chi kinh phí chăm sóc sức khỏe ban đầu HSSV 
        CHI_KINH_PHI_CHAM_SOC_HSSV = 4,
        // Chi kinh phí chăm sóc sức khỏe ban đầu NLĐ 
        CHI_KINH_PHI_CHAM_SOC_NLD = 5,
        // Chi từ nguồn kết dư Quỹ KCB BHYT quân nhân 
        CHI_TU_NGUON_KET_DU_QUY_KCB_BHYT_QUAN_NHAN = 6,
    }

    public struct DanhMucLoaiChiSLNS
    {
        public const string CHI_BHXH_KHOI_DU_TOAN = "9010001";
        public const string CHI_BHXH_KHOI_HACH_TOAN = "9010002";
        public const string CHI_BHXH_SLNS = "9010001,9010002";
        public const string CHI_BHXH__SLNS = "9010001,9010002";
        public const string CHI_KINH_PHI_QUAN_LY_SLNS = "9010003";
        public const string CHI_KINH_PHI_QUAN_Y_KHOI_DU_TOAN_SLNS = "9010004";
        public const string CHI_KINH_PHI_QUAN_Y_KHOI_HACH_TOAN_SLNS = "9010005";
        public const string CHI_KINH_PHI_QUAN_Y_SLNS = "9010004,9010005";
        public const string CHI_KINH_PHI_TRUONG_SA_KHOI_DU_TOAN_SLNS = "9010006";
        public const string CHI_KINH_PHI_TRUONG_SA_KHOI_HACH_TOAN_SLNS = "9010007";
        public const string CHI_KINH_PHI_TRUONG_SA_SLNS = "9010006,9010007";
        public const string CHI_KINH_PHI_CHAM_SOC_HSSV_NLD_SLNS = "9050001,9050002";
        public const string CHI_KINH_PHI_CHAM_SOC_NLD_SLNS = "9050001";
        public const string CHI_KINH_PHI_CHAM_SOC_HSSV_SLNS = "9050002";
        public const string CHI_TU_NGUON_KET_DU_QUY_KCB_BHYT_QUAN_NHAN_SLNS = "9040001";
    }

    public struct MaDanhMucLoaiChi
    {
        public const string CHI_BHXH = "01";
        // Chi kinh phí quản lý BHXH, BHYT
        public const string CHI_KINH_PHI_QUAN_LY = "02";
        // Chi kinh phí KCB tại quân y đơn vị 
        public const string CHI_KINH_PHI_QUAN_Y_DON_VI = "03";
        // Chi kinh phí KCB tại Trường Sa
        public const string CHI_KINH_PHI_TRUONG_SA = "04";
        // Chi từ nguồn kết dư Quỹ KCB BHYT quân nhân 
        public const string CHI_TU_NGUON_KET_DU_QUY_KCB_BHYT_QUAN_NHAN = "05";
        // Kinh phí mua sắm trang thiết bị y tế 
        public const string CHI_KINH_PHI_TRANG_THIET_BI_Y_TE = "06";
        // Chi kinh phí chăm sóc sức khỏe ban đầu HSSV & NLĐ
        public const string CHI_KINH_PHI_CHAM_SOC_HSSV_NLD = "07";
        //Chi hỗ trợ BHTN
        public const string CHI_TU_NGUON_HTBNTC = "08";
    }

    public enum TypeCapKinhPhiBHYT
    {
        //Chi nguồn kết dư Qũy KCB BHYT quân nhân 
        KINH_PHI_KCB_BHYT_QUAN_NHAN = 1,
        //Chi kinh phí chăm sóc sức khỏe ban đầu HSSV & NLĐ
        KINH_PHI_KCB_BHYT_QN_NLD = 2,
    }

    public enum TypeImport
    {
        //Chi nguồn kết dư Qũy KCB BHYT quân nhân 
        ImportTemplate = 1,
        //Chi kinh phí chăm sóc sức khỏe ban đầu HSSV & NLĐ
        ImportFileExport = 2,
    }

    public struct TypeImportName
    {
        //Kinh phí KCB BHYT quân nhân 
        public const string ImportTemplate = "Import bằng file template";
        //Kinh phí KCB BHYT thân nhân quân nhân và người lao động
        public const string ImportFileExport = "Import bằng file export";
    }

    public struct CapKinhPhiBHYT
    {
        //Kinh phí KCB BHYT quân nhân 
        public const string KINH_PHI_KCB_BHYT_QUAN_NHAN = "Kinh phí KCB BHYT quân nhân";
        //Kinh phí KCB BHYT thân nhân quân nhân và người lao động
        public const string KINH_PHI_KCB_BHYT_QN_NLD = "Kinh phí KCB BHYT thân nhân quân nhân và người lao động";
    }

    public enum BhDttThagQuyValue
    {
        Thang1 = 1,
        Thang2 = 2,
        Thang3 = 3,
        Thang4 = 4,
        Thang5 = 5,
        Thang6 = 6,
        Thang7 = 7,
        Thang8 = 8,
        Thang9 = 9,
        Thang10 = 10,
        Thang11 = 11,
        Thang12 = 12,
        QuyI = 13,
        QuyII = 14,
        QuyIII = 15,
        QuyIV = 16
    }

    public static class BhDttThagQuy
    {
        public enum Type
        {
            Thang1 = 1,
            Thang2 = 2,
            Thang3 = 3,
            Thang4 = 4,
            Thang5 = 5,
            Thang6 = 6,
            Thang7 = 7,
            Thang8 = 8,
            Thang9 = 9,
            Thang10 = 10,
            Thang11 = 11,
            Thang12 = 12,
            QuyI = 13,
            QuyII = 14,
            QuyIII = 15,
            QuyIV = 16
        }

        public struct TypeName
        {
            public const string Thang1 = "Tháng 1";
            public const string Thang2 = "Tháng 2";
            public const string Thang3 = "Tháng 3";
            public const string Thang4 = "Tháng 4";
            public const string Thang5 = "Tháng 5";
            public const string Thang6 = "Tháng 6";
            public const string Thang7 = "Tháng 7";
            public const string Thang8 = "Tháng 8";
            public const string Thang9 = "Tháng 9";
            public const string Thang10 = "Tháng 10";
            public const string Thang11 = "Tháng 11";
            public const string Thang12 = "Tháng 12";
            public const string QuyI = "Quý I";
            public const string QuyII = "Quý II";
            public const string QuyIII = "Quý III";
            public const string QuyIV = "Quý IV";
        }

        public static string Get(int? type)
        {
            switch (type)
            {
                case (int)Type.Thang1:
                    return TypeName.Thang1;
                case (int)Type.Thang2:
                    return TypeName.Thang2;
                case (int)Type.Thang3:
                    return TypeName.Thang3;
                case (int)Type.Thang4:
                    return TypeName.Thang4;
                case (int)Type.Thang5:
                    return TypeName.Thang5;
                case (int)Type.Thang6:
                    return TypeName.Thang6;
                case (int)Type.Thang7:
                    return TypeName.Thang7;
                case (int)Type.Thang8:
                    return TypeName.Thang8;
                case (int)Type.Thang9:
                    return TypeName.Thang9;
                case (int)Type.Thang10:
                    return TypeName.Thang10;
                case (int)Type.Thang11:
                    return TypeName.Thang11;
                case (int)Type.Thang12:
                    return TypeName.Thang12;
                case (int)Type.QuyI:
                    return TypeName.QuyI;
                case (int)Type.QuyII:
                    return TypeName.QuyII;
                case (int)Type.QuyIII:
                    return TypeName.QuyIII;
                case (int)Type.QuyIV:
                    return TypeName.QuyIV;
            }
            return string.Empty;
        }

        public static string GetDTC(int type)
        {
            switch (type)
            {
                case (int)GetQuater.QuyI:
                    return TypeName.QuyI;
                case (int)GetQuater.QuyII:
                    return TypeName.QuyII;
                case (int)GetQuater.QuyIII:
                    return TypeName.QuyIII;
                case (int)GetQuater.QuyIV:
                    return TypeName.QuyIV;
            }
            return string.Empty;
        }

    }

}
