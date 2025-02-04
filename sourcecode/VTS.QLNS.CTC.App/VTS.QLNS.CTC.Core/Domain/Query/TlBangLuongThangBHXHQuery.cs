using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class TlBangLuongThangBHXHQuery
    {
        public string SLoaiTC { get; set; }
        public Guid Id { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public string MaCbo { get; set; }
        public string TenCbo { get; set; }
        public string MaDonVi { get; set; }
        public Guid? IIDMaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public decimal? GiaTri { get; set; }
        public string UserName { get; set; }
        public DateTime? NgayHt { get; set; }
        public string MaCachTl { get; set; }
        public string TenCachTl { get; set; }
        public string STT { get; set; }
        public int? LoaiBl { get; set; }
        public Guid? Parent { get; set; }
        public string MaCb { get; set; }
        public string TenCapBac { get; set; }
        public string MaCheDo { get; set; }
        public double? SoNgayBenhDaiNgayD14Ngay { get; set; }
        public double? SoNgayBenhDaiNgayT14Ngay { get; set; }
        public double? SoNgayOmKhacD14Ngay { get; set; }
        public double? SoNgayOmKhacT14Ngay { get; set; }
        public double? SoNgayConOm { get; set; }
        public double? SoNgayDuongSuc { get; set; }
        public decimal? FLuongCanCu { get; set; }
        public decimal? FBenhDaiNgayD14Ngay { get; set; }
        public decimal? FBenhDauNgayT14Ngay { get; set; }
        public decimal? FOmKhacD14Ngay { get; set; }
        public decimal? FOmKhacT14Ngay { get; set; }
        public decimal? FConOm { get; set; }
        public decimal? FDuongSucPHSK { get; set; }
        public decimal? FTongSoTien { get; set; }
        public string MaHieuCanBo { get; set; }
        public bool? BNuocNgoai { get; set; }
        public decimal? HuongPcSn { get; set; }
        public string CongThuc { get; set; }
        public bool IsCalculated { get; set; }
        public bool IsCapNhat { get; set; }
        public Guid IID_CanBoPhuCap { get; set; }
        public string DoiTuong { get; set; }
        public string LoaiDoiTuong { get; set; }

        public double? SoNgaySinhConNuoiCon { get; set; }
        public double? SoNgayTroCap1Lan { get; set; }
        public double? SoNgayKhamThai { get; set; }
        public double? SoNgayDuongSucPHSKThaiSan { get; set; }
        public decimal? FSinhConNuoiCon { get; set; }
        public decimal? FTroCap1Lan { get; set; }
        public decimal? FKhamThai { get; set; }
        public decimal? FDuongSucPHSKThaiSan { get; set; }

        public double? SoNgayDuongSucTNLD { get; set; }
        public decimal? FChiGiamDinh { get; set; }
        public decimal? FTroCapTheoPhieuTruyTra { get; set; }
        public decimal? FTroCapHangThang { get; set; }
        public decimal? FTroCapPHCN { get; set; }
        public decimal? FTroCapChetDoTNLD { get; set; }
        public decimal? FDuongSucTNLD { get; set; }
        public decimal? FTroCapKhuVuc { get; set; }
        public decimal? FTroCapMaiTang { get; set; }
        public decimal? FTongSoTienThangNay { get; set; }

        public decimal? FTroCapMaiTangTruyLinh { get; set; }
        public decimal? FTroCap1LanTruyLinh { get; set; }
        public decimal? FTroCapKhuVucTruyLinh { get; set; }
        public decimal? FTongSoTienTruyLinh { get; set; }
        public decimal? FChiGiamDinhTruyLinh { get; set; }
        public decimal? FHoTroCdnnTruyLinh { get; set; }
        public decimal? FTroCapHangThangTruyLinh { get; set; }
        public decimal? FTroCapPHCNTruyLinh { get; set; }
        public decimal? FTroCapChetDoTNLDTruyLinh { get; set; }
        public decimal? FDuongSucTNLDTruyLinh { get; set; }
        public decimal? SoNgayDuongSucTNLDTruyLinh { get; set; }
        public decimal? FHoTroCdnn { get; set; }
        public decimal? FHoTroPhongNgua { get; set; }
        public decimal? FHoTroPhongNguaTruyLinh { get; set; }

        public bool? IsHangCha { get; set; }
        public bool IsHasData { get; set; }
        public bool IsAgency { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public string SNgayQuyetDinh => DNgayQuyetDinh?.ToString("dd/MM/yyyy");
        public int? SoNguoiSQ { get; set; }
        public decimal? SoTienSQ { get; set; }
        public decimal? SoTienQNCN { get; set; }
        public int? SoNguoiQNCN { get; set; }
        public decimal? SoTienHSQ { get; set; }
        public int? SoNguoiHSQ { get; set; }
        public decimal? SoTienVCQP { get; set; }
        public int? SoNguoiVCQP { get; set; }
        public decimal? SoTienHDLD { get; set; }
        public int? SoNguoiHDLD { get; set; }
        public string SXauNoiMaMlnsBHXH { get; set; }
        public int SoCanBo { get; set; }
        public bool IsDeleteParent { get; set; }
        public decimal? FSoPhaiTruBHXH { get; set; }
        public decimal? FSoPhaiTruBHYT { get; set; }
        public decimal? FTongPhaiTru { get; set; }
        public decimal? FDuocNhan { get; set; }
        public int OrderNganh
        {
            get
            {
                switch (LoaiDoiTuong)
                {
                    case NgachLuongBhxh.SQ:
                    case NgachLuongBhxh.SQ2:
                        return 1;
                    case NgachLuongBhxh.QNCN:
                        return 2;
                    case NgachLuongBhxh.HSQ_BS:
                        return 3;
                    case NgachLuongBhxh.VCQP:
                    case NgachLuongBhxh.VCQP2:
                        return 4;
                    case NgachLuongBhxh.LDHD:
                    case NgachLuongBhxh.LDHD2:
                        return 5;
                    default:
                        return 0;
                }
            }
        }
        public int? SoTt { get; set; }

        public bool HasDataPrint => FTroCap1Lan.GetValueOrDefault() != 0 || FTroCapKhuVuc.GetValueOrDefault() != 0 || FTroCapMaiTang.GetValueOrDefault() != 0 || FTroCap1LanTruyLinh.GetValueOrDefault() != 0 || FTroCapKhuVucTruyLinh.GetValueOrDefault() != 0 || FTroCapMaiTangTruyLinh.GetValueOrDefault() != 0;
    }

    public class TlBangLuongThangBHXHReportQuery
    {
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public string STT { get; set; }
        public string LoaiDoiTuong { get; set; }
        public string TenChiTieu { get; set; }
        public string MaCapBac { get; set; }
        public string MaCanBo { get; set; }
        public string TenCanBo { get; set; }
        public int? Level { get; set; }
        public decimal? LBH_TT { get; set; }
        public decimal? PCCVBH_TT { get; set; }
        public decimal? PCTNBH_TT { get; set; }
        public decimal? PCTNVKBH_TT { get; set; }
        public decimal? HSBLBH_TT { get; set; }
        public decimal? Total { get; set; }
        public bool IsHangCha => Level.GetValueOrDefault() < 2;
        public bool IsAgency => Level.GetValueOrDefault() == 0;
        public int SoCanBo { get; set; }


    }
}
