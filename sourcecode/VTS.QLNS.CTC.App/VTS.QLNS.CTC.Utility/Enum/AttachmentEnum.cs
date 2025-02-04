using System;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public class AttachmentEnum
    {
        public enum Type
        {
            // Ngân sách: 1 + XX (XX tăng dần)
            NS_MUCLUC_NGANSACH = 100,
            NS_DUTOAN = 101,

            // Vốn đầu tư: 2 + XX (XX tăng dần)
            VDT_KHTH_DEXUAT = 201,
            VDT_KHTH_DUOCDUYET = 202,
            VDT_CHUTRUONG_DAUTU = 203,
            VDT_THONGTIN_DUAN = 204,
            VDT_PHEDUYET_DUAN = 205,
            VDT_TKTC_TONGDUTOAN = 206,
            VDT_KH_LUACHON_NHATHAU = 207,
            VDT_THONGTIN_HOPDONG = 208,
            VDT_DENGHI_THANHTOAN = 209,
            VDT_PHEDUYET_THANHTOAN = 210,
            // Lương: 3 + XX (XX tăng dần)
            LUONG_CANBO = 301,

            // Ngoại hối
            NH_KH_CHITIET = 401,
            NH_KH_TONGTHE = 402,
            NH_HDNK_CACQUYETDINH = 403,
            NH_CHUTRUONG_DAUTU = 404,
            NH_QUYETDINH_DAUTU = 405,
            NH_DA_HOPDONG = 406,
            NH_TKKT_TONGDUTOAN = 407,
			NH_DA_KHLCNhaThau = 408,
            NH_DA_THONGTIN_DUAN = 409,
            NH_DA_GOITHAU_NGOAITHUONG = 410,
            NH_DE_NGHI_THANH_TOAN = 411,
            NH_NHUCAU_CHIQUY = 413,
            NH_PHE_DUYET_THANH_TOAN = 412,
        }

        public struct TypeValue
        {
            public const string NS_MUCLUC_NGANSACH = @"NS\MucLucNganSach";
            public const string NS_DUTOAN = @"NS\DuToan";
            public const string VDT_KHTH_DEXUAT = @"VDT\KHTHDeXuat";
            public const string VDT_KHTH_DUOCDUYET = @"VDT\KHTHDuocDuyet";
            public const string VDT_CHUTRUONG_DAUTU = @"VDT\ChuTruongDauTu";
            public const string VDT_THONGTIN_DUAN = @"VDT\ThongTinDuAn";
            public const string VDT_DENGHI_THANHTOAN = @"VDT\DeNghiThanhToan";
            public const string VDT_PHEDUYET_THANHTOAN = @"VDT\PheDuyetThanhToan";
            public const string VDT_PHEDUYET_DUAN = @"VDT\PheDuyetDuAn";
            public const string VDT_TKTC_TONGDUTOAN = @"VDT\TKTCTongDuToan";
            public const string VDT_KH_LUACHON_NHATHAU = @"VDT\KHLuaChonNhaThau";
            public const string VDT_THONGTIN_HOPDONG = @"VDT\ThongTinHopDong";
            public const string NH_KH_CHITIET = @"NH\KHChiTiet";
            public const string NH_KH_TONGTHE = @"NH\KHTongThe";
            public const string NH_HDNK_CACQUYETDINH = @"NH\NHHdnkCacQuyetDinh";
            public const string NH_DA_HOPDONG = @"NH\DaHopDong";
            public const string NH_CHUTRUONG_DAUTU = @"NH\ChuTruongDauTu";
            public const string NH_TKKT_TONGDUTOAN = @"NH\DuToan";
            public const string NH_QUYETDINH_DAUTU = @"NH\QuyetDinhDauTu";
            public const string NH_DE_NGHI_THANH_TOAN = @"NH\DeNghiThanhToan";
            public const string NH_DA_THONGTIN_DUAN = @"NH\ThongTinDuAn";
            public const string NH_DA_KHLCNhaThau = @"NH\KeHoachLuaChonNhaThau";
            public const string NH_DA_GOITHAU_NGOAITHUONG = @"NH\GoiThauNgoaiThuong";
            public const string NH_NHUCAU_CHIQUY = @"NH\NhuCauChiQuy";
            public const string NH_PHE_DUYET_THANH_TOAN = @"NH\PheDuyetThanhToan";
        }

        public static string ValueOf(Type type)
        {
            switch (type)
            {
                case Type.NS_MUCLUC_NGANSACH: return TypeValue.NS_MUCLUC_NGANSACH;
                case Type.NS_DUTOAN: return TypeValue.NS_DUTOAN;
                case Type.VDT_KHTH_DEXUAT: return TypeValue.VDT_KHTH_DEXUAT;
                case Type.VDT_KHTH_DUOCDUYET: return TypeValue.VDT_KHTH_DUOCDUYET;
                case Type.VDT_CHUTRUONG_DAUTU: return TypeValue.VDT_CHUTRUONG_DAUTU;
                case Type.VDT_THONGTIN_DUAN: return TypeValue.VDT_THONGTIN_DUAN;
                case Type.VDT_PHEDUYET_DUAN: return TypeValue.VDT_PHEDUYET_DUAN;
                case Type.VDT_TKTC_TONGDUTOAN: return TypeValue.VDT_TKTC_TONGDUTOAN;
                case Type.VDT_KH_LUACHON_NHATHAU: return TypeValue.VDT_KH_LUACHON_NHATHAU;
                case Type.VDT_THONGTIN_HOPDONG: return TypeValue.VDT_THONGTIN_HOPDONG;
                case Type.VDT_DENGHI_THANHTOAN: return TypeValue.VDT_DENGHI_THANHTOAN;
                case Type.VDT_PHEDUYET_THANHTOAN: return TypeValue.VDT_PHEDUYET_THANHTOAN;
                case Type.NH_DA_HOPDONG: return TypeValue.NH_DA_HOPDONG;
                case Type.NH_HDNK_CACQUYETDINH: return TypeValue.NH_HDNK_CACQUYETDINH; 
                case Type.NH_KH_CHITIET: return TypeValue.NH_KH_CHITIET;
                case Type.NH_KH_TONGTHE: return TypeValue.NH_KH_TONGTHE;
                case Type.NH_CHUTRUONG_DAUTU: return TypeValue.NH_CHUTRUONG_DAUTU;
                case Type.NH_TKKT_TONGDUTOAN: return TypeValue.NH_TKKT_TONGDUTOAN;
                case Type.NH_QUYETDINH_DAUTU: return TypeValue.NH_QUYETDINH_DAUTU;
                case Type.NH_DE_NGHI_THANH_TOAN: return TypeValue.NH_DE_NGHI_THANH_TOAN;
                case Type.NH_DA_THONGTIN_DUAN: return TypeValue.NH_DA_THONGTIN_DUAN;
                case Type.NH_DA_KHLCNhaThau: return TypeValue.NH_DA_KHLCNhaThau;
                case Type.NH_DA_GOITHAU_NGOAITHUONG: return TypeValue.NH_DA_GOITHAU_NGOAITHUONG;
                case Type.NH_NHUCAU_CHIQUY: return TypeValue.NH_NHUCAU_CHIQUY;
                case Type.NH_PHE_DUYET_THANH_TOAN: return TypeValue.NH_PHE_DUYET_THANH_TOAN;
                default:
                    throw new Exception("Not supported.");
            }
        }

        public static Type TypeOf(int type)
        {
            switch (type)
            {
                case (int)Type.NS_MUCLUC_NGANSACH: return Type.NS_MUCLUC_NGANSACH;
                case (int)Type.NS_DUTOAN: return Type.NS_DUTOAN;
                case (int)Type.VDT_KHTH_DEXUAT: return Type.VDT_KHTH_DEXUAT;
                case (int)Type.VDT_KHTH_DUOCDUYET: return Type.VDT_KHTH_DUOCDUYET;
                case (int)Type.VDT_CHUTRUONG_DAUTU: return Type.VDT_CHUTRUONG_DAUTU;
                case (int)Type.VDT_THONGTIN_DUAN: return Type.VDT_THONGTIN_DUAN;
                case (int)Type.VDT_PHEDUYET_DUAN: return Type.VDT_PHEDUYET_DUAN;
                case (int)Type.VDT_TKTC_TONGDUTOAN: return Type.VDT_TKTC_TONGDUTOAN;
                case (int)Type.VDT_KH_LUACHON_NHATHAU: return Type.VDT_KH_LUACHON_NHATHAU;
                case (int)Type.VDT_THONGTIN_HOPDONG: return Type.VDT_THONGTIN_HOPDONG;
                case (int)Type.VDT_DENGHI_THANHTOAN: return Type.VDT_DENGHI_THANHTOAN;
                case (int)Type.VDT_PHEDUYET_THANHTOAN: return Type.VDT_PHEDUYET_THANHTOAN;
                case (int)Type.NH_KH_CHITIET: return Type.NH_KH_CHITIET;
                case (int)Type.NH_KH_TONGTHE: return Type.NH_KH_TONGTHE;
                case (int)Type.NH_HDNK_CACQUYETDINH: return Type.NH_HDNK_CACQUYETDINH;
                case (int)Type.NH_DA_HOPDONG: return Type.NH_DA_HOPDONG;
                case (int)Type.NH_CHUTRUONG_DAUTU: return Type.NH_CHUTRUONG_DAUTU;
                case (int)Type.NH_TKKT_TONGDUTOAN: return Type.NH_TKKT_TONGDUTOAN;
                case (int)Type.NH_QUYETDINH_DAUTU: return Type.NH_QUYETDINH_DAUTU;
                case (int)Type.NH_DE_NGHI_THANH_TOAN: return Type.NH_DE_NGHI_THANH_TOAN;
                case (int)Type.NH_DA_THONGTIN_DUAN: return Type.NH_DA_THONGTIN_DUAN;
                case (int)Type.NH_DA_KHLCNhaThau: return Type.NH_DA_KHLCNhaThau;
                case (int)Type.NH_DA_GOITHAU_NGOAITHUONG: return Type.NH_DA_GOITHAU_NGOAITHUONG;
                case (int)Type.NH_NHUCAU_CHIQUY: return Type.NH_NHUCAU_CHIQUY;
                default:
                    throw new Exception("Not supported.");
            }
        }
    }
}
