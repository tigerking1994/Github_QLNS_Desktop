using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class QttBHXHChiTietGiaiThichRepository : Repository<BhQttBHXHChiTietGiaiThich>, IQttBHXHChiTietGiaiThichRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public QttBHXHChiTietGiaiThichRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public BhQttBHXHChiTietGiaiThich FindByCondition(BhQttBHXHChiTietGiaiThichCriteria condition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQttBHXHChiTietGiaiThichs.Where(x => x.QttBHXHId == condition.VoucherId && x.IIDMaDonVi == condition.AgencyId
                                                           && x.INamLamViec == condition.YearOfWork && x.ILoaiGiaiThich == condition.ExplainType
                                                           && x.SLoaiThu == condition.CollectionType).FirstOrDefault();
            }
        }

        public IEnumerable<BhQttBHXHChiTietGiaiThichQuery> GetChiTietGiaiThichTruyThu(BhQttBHXHChiTietGiaiThichCriteria condition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", condition.YearOfWork);
                SqlParameter voucherIDParam = new SqlParameter("@VoucherID", condition.VoucherId);
                SqlParameter maDonViParam = new SqlParameter("@MaDonVi", condition.AgencyId);
                SqlParameter voucherType = new SqlParameter("@VoucherType", condition.VoucherType);
                return ctx.FromSqlRaw<BhQttBHXHChiTietGiaiThichQuery>("EXECUTE sp_bh_qtt_get_giai_thich_truy_thu " +
                    "@NamLamViec, @VoucherID, @MaDonVi, @VoucherType", iNamLamViecParam, voucherIDParam, maDonViParam, voucherType).ToList();
            }
        }

        public IEnumerable<BhQttBHXHChiTietGiaiThich> GetChiTietGiaiThichTongHopSoSanh(BhQttBHXHChiTietGiaiThichCriteria condition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", condition.YearOfWork);
                SqlParameter voucherIDParam = new SqlParameter("@VoucherID", condition.VoucherId);
                SqlParameter maDonViParam = new SqlParameter("@MaDonVi", condition.AgencyId);
                SqlParameter loaiGiaiThichParam = new SqlParameter("@LoaiGiaiThich", condition.ExplainType);
                return ctx.FromSqlRaw<BhQttBHXHChiTietGiaiThich>("EXECUTE sp_bh_qtt_get_giai_thich_tong_hop_so_sanh " +
                    "@NamLamViec, @VoucherID, @MaDonVi, @LoaiGiaiThich", iNamLamViecParam, voucherIDParam, maDonViParam, loaiGiaiThichParam).ToList();
            }
        }

        public IEnumerable<BhQttBHXHChiTietGiaiThich> GetChiTietGiaiThichTongHopSoSanhTonTai(BhQttBHXHChiTietGiaiThichCriteria condition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQttBHXHChiTietGiaiThichs.Where(x => x.QttBHXHId == condition.VoucherId && x.IIDMaDonVi == condition.AgencyId
                                                           && x.INamLamViec == condition.YearOfWork && x.ILoaiGiaiThich == condition.ExplainType).ToList();
            }
        }

        public IEnumerable<BhQttBHXHChiTietGiaiThich> FindByQttId(Guid iDQTT)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQttBHXHChiTietGiaiThichs.Where(x => x.QttBHXHId == iDQTT).ToList();
            }
        }

        public IEnumerable<BhQttBHXHChiTietGiaiThichQuery> ExportGiaiThichTruyThu(int namLamViec, string maDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter maDonViParam = new SqlParameter("@MaDonVi", maDonVi);
                return ctx.FromSqlRaw<BhQttBHXHChiTietGiaiThichQuery>("EXECUTE sp_bh_qtt_rpt_giai_thich_truy_thu " +
                    "@NamLamViec, @MaDonVi", iNamLamViecParam, maDonViParam).ToList();
            }
        }

        public IEnumerable<BhQttBHXHChiTietGiaiThichQuery> ExportGiaiThichTruyThuTongHopDonVi(int namLamViec, string maDonVis)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter maDonViParam = new SqlParameter("@IdDonVis", maDonVis);
                return ctx.FromSqlRaw<BhQttBHXHChiTietGiaiThichQuery>("EXECUTE sp_bh_qtt_rpt_giai_thich_truy_thu_tong_hop_don_vi " +
                    "@NamLamViec, @IdDonVis", iNamLamViecParam, maDonViParam).ToList();
            }
        }

        public IEnumerable<BhQttBHXHChiTietGiaiThichQuery> ExportGiaiThichTongHopSoSanh(int namLamViec, string maDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter maDonViParam = new SqlParameter("@MaDonVi", maDonVi);
                return ctx.FromSqlRaw<BhQttBHXHChiTietGiaiThichQuery>("EXECUTE sp_bh_qtt_rpt_giai_thich_tong_hop_so_sanh " +
                    "@NamLamViec, @MaDonVi", iNamLamViecParam, maDonViParam).ToList();
            }
        }

        public BhQttBHXHChiTietGiaiThichQuery ExportGiaiThichBangLoi(int namLamViec, int quy, int loaiQuy, string maDonVi, string loaiThu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter iQuyParam = new SqlParameter("@Quy", quy);
                SqlParameter iLoaiQuyParam = new SqlParameter("@LoaiQuy", loaiQuy);
                SqlParameter maDonViParam = new SqlParameter("@MaDonVi", maDonVi);
                SqlParameter loaiThuParam = new SqlParameter("@LoaiThu", loaiThu);

                return ctx.FromSqlRaw<BhQttBHXHChiTietGiaiThichQuery>("EXECUTE sp_bh_qtt_rpt_giai_thich_bang_loi " +
                    "@NamLamViec, @Quy, @LoaiQuy, @MaDonVi, @LoaiThu", iNamLamViecParam, iQuyParam, iLoaiQuyParam, maDonViParam, loaiThuParam).FirstOrDefault();
            }
        }

        public BhQttBHXHChiTietGiaiThichQuery ExportGiaiThichBangLoiTongHopDonVi(int namLamViec, int quy, int loaiQuy, string maDonVis, string loaiThu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter iQuyParam = new SqlParameter("@Quy", quy);
                SqlParameter iLoaiQuyParam = new SqlParameter("@LoaiQuy", loaiQuy);
                SqlParameter maDonViParam = new SqlParameter("@MaDonVis", maDonVis);
                SqlParameter loaiThuParam = new SqlParameter("@LoaiThu", loaiThu);

                return ctx.FromSqlRaw<BhQttBHXHChiTietGiaiThichQuery>("EXECUTE sp_bh_qtt_rpt_giai_thich_bang_loi_tong_hop_don_vi_quy " +
                    "@NamLamViec, @Quy, @LoaiQuy, @MaDonVis, @LoaiThu", iNamLamViecParam, iQuyParam, iLoaiQuyParam, maDonViParam, loaiThuParam).FirstOrDefault();
            }
        }

        public IEnumerable<BhQttBHXHChiTietGiaiThichQuery> ExportGiaiThichGiamDong(int namLamViec, string maDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter maDonViParam = new SqlParameter("@MaDonVi", maDonVi);
                return ctx.FromSqlRaw<BhQttBHXHChiTietGiaiThichQuery>("EXECUTE sp_bh_qtt_rpt_giai_thich_giam_dong " +
                    "@NamLamViec, @MaDonVi", iNamLamViecParam, maDonViParam).ToList();
            }
        }

        public IEnumerable<BhQttBHXHChiTietGiaiThichQuery> ExportGiaiThichTongHopSoSanhDonVi(int namLamViec, string maDonVis)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter maDonViParam = new SqlParameter("@MaDonVi", maDonVis);
                return ctx.FromSqlRaw<BhQttBHXHChiTietGiaiThichQuery>("EXECUTE sp_bh_qtt_rpt_giai_thich_tong_hop_so_sanh_don_vi " +
                    "@NamLamViec, @MaDonVi", iNamLamViecParam, maDonViParam).ToList();
            }
        }

        public IEnumerable<BhQttBHXHChiTietGiaiThichQuery> ExportGiaiThichGiamDongTongHopDonVi(int namLamViec, string maDonVis)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter maDonViParam = new SqlParameter("@MaDonVis", maDonVis);
                return ctx.FromSqlRaw<BhQttBHXHChiTietGiaiThichQuery>("EXECUTE sp_bh_qtt_rpt_giai_thich_giam_dong_tong_hop_don_vi " +
                    "@NamLamViec, @MaDonVis", iNamLamViecParam, maDonViParam).ToList();
            }
        }

        public IEnumerable<BhQttBHXHChiTietGiaiThich> FindByVouCherId(Guid voucherID)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQttBHXHChiTietGiaiThichs.Where(x => x.QttBHXHId == voucherID).ToList();
            }
        }

        public IEnumerable<BhQttBHXHChiTietGiaiThichBangLoiQuery> GetGiaiThichBangLoi(BhQttBHXHChiTietGiaiThichCriteria condition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter voucherIDParam = new SqlParameter("@VoucherID", condition.VoucherId);
                return ctx.FromSqlRaw<BhQttBHXHChiTietGiaiThichBangLoiQuery>("EXECUTE sp_bh_qtt_get_giai_thich_bang_loi " + "@VoucherID", voucherIDParam).ToList();
            }
        }

        public IEnumerable<BhQttBHXHChiTietGiaiThichQuery> ExportGiaiThichSoLieuThangQuy(int namLamViec, int quy, int loaiQuy, string maDonVi, int dvt, bool isLuyKe)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter iQuyParam = new SqlParameter("@Quy", quy);
                SqlParameter iLoaiQuyParam = new SqlParameter("@LoaiQuy", loaiQuy);
                SqlParameter maDonViParam = new SqlParameter("@MaDonVi", maDonVi);
                SqlParameter dvtParam = new SqlParameter("@Donvitinh", dvt);
                SqlParameter isLuyKeParam = new SqlParameter("@IsLuyKe", isLuyKe);

                return ctx.FromSqlRaw<BhQttBHXHChiTietGiaiThichQuery>("EXECUTE sp_bh_qtt_rpt_giai_thich_so_lieu_thang_quy " +
                    "@NamLamViec, @Quy, @LoaiQuy, @MaDonVi, @Donvitinh, @IsLuyKe", iNamLamViecParam, iQuyParam, iLoaiQuyParam, maDonViParam, dvtParam, isLuyKeParam).ToList();
            }
        }

        public bool HasMonthlyExplains(int iNamLamViec, int iQuy, int iLoai, bool isLuyKe, string sMaDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var arrMaDonVi = sMaDonVi.Split(",");
                if (iQuy == 3 && iLoai == 1)
                {
                    string[] months = { "1", "2", "3" };
                    return ctx.BhQttBHXHChiTietGiaiThichs.Any(t => t.INamLamViec == iNamLamViec && t.IQuyNamLoai == 0 && months.Contains(t.IQuyNam.ToString()) && arrMaDonVi.Contains(t.IIDMaDonVi) && t.ILoaiGiaiThich == (int)ExplainType.GIAITHICH_TRUYTHU);
                }
                else if (iQuy == 6 && iLoai == 1 && !isLuyKe)
                {
                    string[] months = { "4", "5", "6" };
                    return ctx.BhQttBHXHChiTietGiaiThichs.Any(t => t.INamLamViec == iNamLamViec && t.IQuyNamLoai == 0 && months.Contains(t.IQuyNam.ToString()) && arrMaDonVi.Contains(t.IIDMaDonVi) && t.ILoaiGiaiThich == (int)ExplainType.GIAITHICH_TRUYTHU);
                }
                else if (iQuy == 9 && iLoai == 1 && !isLuyKe)
                {
                    string[] months = { "7", "8", "9" };
                    return ctx.BhQttBHXHChiTietGiaiThichs.Any(t => t.INamLamViec == iNamLamViec && t.IQuyNamLoai == 0 && months.Contains(t.IQuyNam.ToString()) && arrMaDonVi.Contains(t.IIDMaDonVi) && t.ILoaiGiaiThich == (int)ExplainType.GIAITHICH_TRUYTHU);
                }
                else if (iQuy == 12 && iLoai == 1 && !isLuyKe)
                {
                    string[] months = { "10", "11", "12" };
                    return ctx.BhQttBHXHChiTietGiaiThichs.Any(t => t.INamLamViec == iNamLamViec && t.IQuyNamLoai == 0 && months.Contains(t.IQuyNam.ToString()) && arrMaDonVi.Contains(t.IIDMaDonVi) && t.ILoaiGiaiThich == (int)ExplainType.GIAITHICH_TRUYTHU);
                }

                else if (iQuy == 6 && iLoai == 1 && isLuyKe)
                {
                    string[] months = { "1", "2", "3", "4", "5", "6" };
                    return ctx.BhQttBHXHChiTietGiaiThichs.Any(t => t.INamLamViec == iNamLamViec && t.IQuyNamLoai == 0 && months.Contains(t.IQuyNam.ToString()) && arrMaDonVi.Contains(t.IIDMaDonVi) && t.ILoaiGiaiThich == (int)ExplainType.GIAITHICH_TRUYTHU);
                }

                else if (iQuy == 9 && iLoai == 1 && isLuyKe)
                {
                    string[] months = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                    return ctx.BhQttBHXHChiTietGiaiThichs.Any(t => t.INamLamViec == iNamLamViec && t.IQuyNamLoai == 0 && months.Contains(t.IQuyNam.ToString()) && arrMaDonVi.Contains(t.IIDMaDonVi) && t.ILoaiGiaiThich == (int)ExplainType.GIAITHICH_TRUYTHU);
                }
                else if (iQuy == 12 && iLoai == 1 && isLuyKe)
                {
                    string[] months = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
                    return ctx.BhQttBHXHChiTietGiaiThichs.Any(t => t.INamLamViec == iNamLamViec && t.IQuyNamLoai == 0 && months.Contains(t.IQuyNam.ToString()) && arrMaDonVi.Contains(t.IIDMaDonVi) && t.ILoaiGiaiThich == (int)ExplainType.GIAITHICH_TRUYTHU);
                }
                else return false;
            }
        }

        public IEnumerable<BhQttBHXHChiTietGiaiThichQuery> ExportGiaiThichSoLieuThang(int namLamViec, int quy, int loaiQuy, string maDonVi, int dvt, bool isLuyKe)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter iQuyParam = new SqlParameter("@Quy", quy);
                SqlParameter iLoaiQuyParam = new SqlParameter("@LoaiQuy", loaiQuy);
                SqlParameter maDonViParam = new SqlParameter("@MaDonVi", maDonVi);
                SqlParameter dvtParam = new SqlParameter("@Donvitinh", dvt);
                SqlParameter isLuyKeParam = new SqlParameter("@IsLuyKe", isLuyKe);

                return ctx.FromSqlRaw<BhQttBHXHChiTietGiaiThichQuery>("EXECUTE sp_bh_qtt_rpt_giai_thich_so_lieu_thang " +
                    "@NamLamViec, @Quy, @LoaiQuy, @MaDonVi, @Donvitinh, @IsLuyKe", iNamLamViecParam, iQuyParam, iLoaiQuyParam, maDonViParam, dvtParam, isLuyKeParam).ToList();
            }
        }

        public IEnumerable<BhQttBHXHChiTietGiaiThichQuery> GetGiaiThichTruyThu(int namLamViec, string maDonVi, int quy, int loaiQuy)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter maDonViParam = new SqlParameter("@IdDonVis", maDonVi);
                SqlParameter iQuyParam = new SqlParameter("@IQuy", quy);
                SqlParameter iLoaiQuyParam = new SqlParameter("@ILoaiQuy", loaiQuy);

                return ctx.FromSqlRaw<BhQttBHXHChiTietGiaiThichQuery>("EXECUTE sp_bh_rpt_qtt_thong_tri_giai_thich_truy_thu " +
                    "@NamLamViec, @IdDonVis, @IQuy, @ILoaiQuy", iNamLamViecParam, maDonViParam, iQuyParam, iLoaiQuyParam).ToList();
            }
        }

        public IEnumerable<BhQttBHXHChiTietGiaiThichQuery> GetGiaiThichTruyThuDonVi(int namLamViec, string maDonVi, int quy, int loaiQuy)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter maDonViParam = new SqlParameter("@IdDonVis", maDonVi);
                SqlParameter iQuyParam = new SqlParameter("@IQuy", quy);
                SqlParameter iLoaiQuyParam = new SqlParameter("@ILoaiQuy", loaiQuy);

                return ctx.FromSqlRaw<BhQttBHXHChiTietGiaiThichQuery>("EXECUTE sp_bh_rpt_qtt_thong_tri_giai_thich_truy_thu_donvi " +
                    "@NamLamViec, @IdDonVis, @IQuy, @ILoaiQuy", iNamLamViecParam, maDonViParam, iQuyParam, iLoaiQuyParam).ToList();
            }
        }
    }
}
