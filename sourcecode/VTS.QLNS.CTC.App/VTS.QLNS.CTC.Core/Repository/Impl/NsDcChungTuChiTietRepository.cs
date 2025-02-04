using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NsDcChungTuChiTietRepository : Repository<NsDcChungTuChiTiet>, INsDcChungTuChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NsDcChungTuChiTietRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void AddAggregateVoucherDetail(EstimationVoucherDetailCriteria creation)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_dc_chungtu_chitiet_tao_tonghop @VoucherIds, @VoucherId, @YearOfBudget, @BudgetSource, @YearOfWork, @AgencyId, @UserName";
                var parameters = new[]
                {
                    new SqlParameter("@VoucherIds", creation.VoucherIds),
                    new SqlParameter("@VoucherId", creation.VoucherId),
                    new SqlParameter("@YearOfBudget", creation.YearOfBudget),
                    new SqlParameter("@BudgetSource", creation.BudgetSource),
                    new SqlParameter("@YearOfWork", creation.YearOfWork),
                    new SqlParameter("@AgencyId", creation.IdDonVi),
                    new SqlParameter("@UserName", creation.UserName)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public void DeleteByIdChungTu(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = $"DELETE FROM NS_DC_ChungTuChiTiet WHERE iID_DcChungTu = @idChungTuParam";
                var parameters = new[]
                {
                    new SqlParameter("@idChungTuParam", id.ToString())
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public void DeleteByIds(IEnumerable<string> ids)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                if (ids == null || !ids.Any())
                {
                    return;
                }

                foreach (var id in ids)
                {
                    string sql = $"DELETE FROM NS_DC_ChungTuChiTiet WHERE iID_DCCTChiTiet = @id";
                    var parameters = new[]
                    {
                        new SqlParameter("@id", id)
                    };
                    ctx.Database.ExecuteSqlCommand(sql, parameters);
                }
            }
        }

        public NsDcChungTuChiTiet FindByChungTuIdAndMlnsId(Guid voucherId, Guid mlnsId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDcChungTuChiTiets.Where(x => x.IIdDcchungTu == voucherId && x.IIdMlns == mlnsId).FirstOrDefault();
            }
        }

        public IEnumerable<NsDcChungTuChiTietQuery> FindAllNSDCChungTuByCondition(EstimationVoucherDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.Database.SetCommandTimeout(300);
                string sql = string.Format("EXECUTE dbo.sp_dc_chungtu_chitiet_all @chungTuIds, @namLamViec, @namNganSach, @nguonNganSach, @lns, @donVi, @loaiDuKien, @userName");
                var parameters = new[]
                {
                    new SqlParameter("@chungTuIds", searchCondition.VoucherIds.ToString()),
                    new SqlParameter("@namLamViec", searchCondition.YearOfWork),
                    new SqlParameter("@namNganSach", searchCondition.YearOfBudget),
                    new SqlParameter("@nguonNganSach", searchCondition.BudgetSource),
                    new SqlParameter("@lns", searchCondition.LNS),
                    new SqlParameter("@donVi", searchCondition.IdDonVi),
                    new SqlParameter("@loaiDuKien", searchCondition.LoaiDuKien),
                    //new SqlParameter("@loaiChungTu", searchCondition.LoaiChungTu),
                    //new SqlParameter("@ngayChungTu", searchCondition.VoucherDate),
                    new SqlParameter("@userName", searchCondition.UserName)
                };
                return ctx.FromSqlRaw<NsDcChungTuChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<NsDcChungTuChiTietQuery> FindByCondition(EstimationVoucherDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = string.Format("EXECUTE dbo.sp_dc_chungtu_chitiet @chungTuId, @namLamViec, @namNganSach, @nguonNganSach, @lns, @donVi, @loaiDuKien, @loaiChungTu, @ngayChungTu, @userName");
                var parameters = new[]
                {
                    new SqlParameter("@chungTuId", searchCondition.VoucherId.ToString()),
                    new SqlParameter("@namLamViec", searchCondition.YearOfWork),
                    new SqlParameter("@namNganSach", searchCondition.YearOfBudget),
                    new SqlParameter("@nguonNganSach", searchCondition.BudgetSource),
                    new SqlParameter("@lns", searchCondition.LNS),
                    new SqlParameter("@donVi", searchCondition.IdDonVi),
                    new SqlParameter("@loaiDuKien", searchCondition.LoaiDuKien),
                    new SqlParameter("@loaiChungTu", searchCondition.LoaiChungTu),
                    new SqlParameter("@ngayChungTu", searchCondition.VoucherDate),
                    new SqlParameter("@userName", searchCondition.UserName)
                };
                return ctx.FromSqlRaw<NsDcChungTuChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<NsDcChungTuChiTietQuery> FindDuToanByCondition(EstimationVoucherDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = string.Format("EXECUTE dbo.sp_dc_dutoan_chitiet @chungTuId, @namLamViec, @namNganSach, @nguonNganSach, @lns, @donVi, @loaiDuKien, @loaiChungTu, @ngayChungTu, @userName");
                var parameters = new[]
                {
                    new SqlParameter("@chungTuId", searchCondition.VoucherId.ToString()),
                    new SqlParameter("@namLamViec", searchCondition.YearOfWork),
                    new SqlParameter("@namNganSach", searchCondition.YearOfBudget),
                    new SqlParameter("@nguonNganSach", searchCondition.BudgetSource),
                    new SqlParameter("@lns", searchCondition.LNS),
                    new SqlParameter("@donVi", searchCondition.IdDonVi),
                    new SqlParameter("@loaiDuKien", searchCondition.LoaiDuKien),
                    new SqlParameter("@loaiChungTu", searchCondition.LoaiChungTu),
                    new SqlParameter("@ngayChungTu", searchCondition.VoucherDate),
                    new SqlParameter("@userName", searchCondition.UserName)
                };
                return ctx.FromSqlRaw<NsDcChungTuChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<NsDcChungTuChiTietQuery> FindByConditionTongSo(EstimationVoucherDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = string.Format("EXECUTE dbo.sp_dc_chungtu_chitiet_tongso @chungTuId, @namLamViec, @namNganSach, @nguonNganSach, @lns, @donVi, @loaiDuKien, @loaiChungTu, @ngayChungTu, @userName");
                var parameters = new[]
                {
                    new SqlParameter("@chungTuId", searchCondition.VoucherId.ToString()),
                    new SqlParameter("@namLamViec", searchCondition.YearOfWork),
                    new SqlParameter("@namNganSach", searchCondition.YearOfBudget),
                    new SqlParameter("@nguonNganSach", searchCondition.BudgetSource),
                    new SqlParameter("@lns", searchCondition.LNS),
                    new SqlParameter("@donVi", searchCondition.IdDonVi),
                    new SqlParameter("@loaiDuKien", searchCondition.LoaiDuKien),
                    new SqlParameter("@loaiChungTu", searchCondition.LoaiChungTu),
                    new SqlParameter("@ngayChungTu", searchCondition.VoucherDate),
                    new SqlParameter("@userName", searchCondition.UserName),
                };
                return ctx.FromSqlRaw<NsDcChungTuChiTietQuery>(sql, parameters).ToList();
            }
        }


        public IEnumerable<NsDcChungTuChiTietQuery> FindChungTuChiTietForDcDuToanTongHopReport(EstimationVoucherDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = string.Format("EXECUTE dbo.sp_dc_chungtu_chitiet_2 @namLamViec, @namNganSach, @nguonNganSach, @userName");
                var parameters = new[]
                {
                    new SqlParameter("@namLamViec", searchCondition.YearOfWork),
                    new SqlParameter("@namNganSach", searchCondition.YearOfBudget),
                    new SqlParameter("@nguonNganSach", searchCondition.BudgetSource),
                    new SqlParameter("@userName", searchCondition.UserName)
                };
                return ctx.FromSqlRaw<NsDcChungTuChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<DataDieuChinhQuery> FindDataDieuChinh(EstimationVoucherDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = string.Format("EXECUTE dbo.sp_dc_chungtu_lay_dieuchinh @namLamViec, @namNganSach, @nguonNganSach, @lns, @donVi, @loaiChungTu, @ngayChungTu, @userName");
                var parameters = new[]
                {
                    new SqlParameter("@namLamViec", searchCondition.YearOfWork),
                    new SqlParameter("@namNganSach", searchCondition.YearOfBudget),
                    new SqlParameter("@nguonNganSach", searchCondition.BudgetSource),
                    new SqlParameter("@lns", searchCondition.LNS),
                    new SqlParameter("@donVi", searchCondition.IdDonVi),
                    new SqlParameter("@loaiChungTu", searchCondition.LoaiChungTu),
                    new SqlParameter("@ngayChungTu", searchCondition.VoucherDate),
                    new SqlParameter("@userName", searchCondition.UserName)
                };
                return ctx.FromSqlRaw<DataDieuChinhQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<NsDcChungTuChiTietQuery> FindByVoucherID(Guid voucherID)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = string.Format("EXECUTE dbo.sp_ns_dieuchinh_get_by_chungtu @VoucherID");
                var parameters = new[]
                {
                    new SqlParameter("@VoucherID", voucherID)
                };
                return ctx.FromSqlRaw<NsDcChungTuChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<NsDcChungTuChiTietQuery> FindByUnits(string maDonVi, int namLamViec, string iidChungTuNhan)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = string.Format("EXECUTE dbo.sp_ns_phanbo_dutoan_get_dulieu_dieuchinh @MaDonVi, @NamLamViec, @IidChungTuNhan");
                var parameters = new[]
                {
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@IidChungTuNhan", iidChungTuNhan)
                };
                return ctx.FromSqlRaw<NsDcChungTuChiTietQuery>(sql, parameters).ToList();
            }
        }
    }
}
