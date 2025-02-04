using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NsQtChungTuChiTietGiaiThichRepository : Repository<NsQtChungTuChiTietGiaiThich>, INsQtChungTuChiTietGiaiThichRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NsQtChungTuChiTietGiaiThichRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public NsQtChungTuChiTietGiaiThich FindByCondition(SettlementVoucherDetailExplainCriteria condition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsQtChungTuChiTietGiaiThiches.Where(x => x.IIdQtchungTu == condition.VoucherId && x.IIdGiaiThich == condition.ExplainId && x.IIdMaDonVi == condition.AgencyId
                                                              && x.INamLamViec == condition.YearOfWork).FirstOrDefault();
            }
        }

        public NsQtChungTuChiTietGiaiThich FindByCondition(Expression<Func<NsQtChungTuChiTietGiaiThich, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsQtChungTuChiTietGiaiThiches.Where(predicate).FirstOrDefault();
            }
        }

        public IEnumerable<NsQtChungTuChiTietGiaiThich> GetDataChungTuGiaiTichBangSo(string sLoai, string iID_MaDonVi, int iThang, int iQuy, int iNamLamViec, int iNamNganSach, int iID_NguonNganSach, int isTongHop, string explainId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_ns_rpt_quyettoan_giaithichbangso @sLoai, @iID_MaDonVi, @iThang, @iQuy,  @iNamLamViec, @iNamNganSach, @iID_NguonNganSach, @isTongHop, @explainId";
                var parameters = new[]
                {
                    new SqlParameter("@sLoai", sLoai),
                    new SqlParameter("@iID_MaDonVi", iID_MaDonVi),
                    new SqlParameter("@iThang", iThang),
                    new SqlParameter("@iQuy", iQuy),
                    new SqlParameter("@iNamLamViec", iNamLamViec),
                    new SqlParameter("@iNamNganSach", iNamNganSach),
                    new SqlParameter("@iID_NguonNganSach",iID_NguonNganSach),
                    new SqlParameter("@isTongHop",isTongHop),
                    new SqlParameter("@explainId",explainId)
                };

                return ctx.FromSqlRaw<NsQtChungTuChiTietGiaiThich>(sql, parameters);
            }
        }

        public IEnumerable<NsQtChungTuChiTietGiaiThich> FindListCondition(SettlementVoucherDetailExplainCriteria condition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.Set<NsQtChungTuChiTietGiaiThich>().Where(x => x.IIdQtchungTu == condition.VoucherId && x.IIdGiaiThich == condition.ExplainId && x.IIdMaDonVi == condition.AgencyId
                                                              && x.INamLamViec == condition.YearOfWork).ToList();
            }
        }

        public IEnumerable<NsQtChungTuChiTietGiaiThich> GetDataChungTuGiaiTichBangLoi(string sLoai, string iID_MaDonVi, int iThang, int iQuy, int iNamLamViec, int iNamNganSach, int iID_NguonNganSach, int isTongHop, string explainId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_ns_rpt_quyettoan_giaithichbangloi @sLoai, @iID_MaDonVi, @iThang, @iQuy,  @iNamLamViec, @iNamNganSach, @iID_NguonNganSach, @isTongHop, @explainId";
                var parameters = new[]
                {
                    new SqlParameter("@sLoai", sLoai),
                    new SqlParameter("@iID_MaDonVi", iID_MaDonVi),
                    new SqlParameter("@iThang", iThang),
                    new SqlParameter("@iQuy", iQuy),
                    new SqlParameter("@iNamLamViec", iNamLamViec),
                    new SqlParameter("@iNamNganSach", iNamNganSach),
                    new SqlParameter("@iID_NguonNganSach",iID_NguonNganSach),
                    new SqlParameter("@isTongHop",isTongHop),
                    new SqlParameter("@explainId",explainId)
                };

                return ctx.FromSqlRaw<NsQtChungTuChiTietGiaiThich>(sql, parameters);
            }
        }
    }
}
