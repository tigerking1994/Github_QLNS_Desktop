using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class BhQtcqCtctGtTroCapRepository : Repository<BhQtcqCtctGtTroCap>, IBhQtcqCtctGtTroCapRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public BhQtcqCtctGtTroCapRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhQtcqCtctGtTroCap> FindByVoucherID(Guid voucherID)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcqCtctGtTroCaps.Where(x => x.IID_QTC_Quy_ChungTu == voucherID).ToList();
            }
        }

        public IEnumerable<BhSalaryDataQuery> FindSalaryDataByXauNoiMaAnQuarte(string sXauNoiMa, string sQuarte, string sMaDonVi, int namLamViec, string sLNS)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter sXauNoiMaParam = new SqlParameter("@sXauNoiMa", sXauNoiMa);
                SqlParameter sQuarteParam = new SqlParameter("@sQuarter", sQuarte);
                SqlParameter sMaDonViParam = new SqlParameter("@Donvi", sMaDonVi);
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter sLNSParam = new SqlParameter("@sLNS", sLNS);
                return ctx.FromSqlRaw<BhSalaryDataQuery>("EXECUTE dbo.sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh  @SXauNoiMa, @sQuarter,@Donvi,@NamLamViec,@sLNS", sXauNoiMaParam, sQuarteParam, sMaDonViParam, iNamLamViecParam, sLNSParam).ToList();
            }
        }

        public List<BhQtcqCtctGtTroCapQuery> GetDataExplainSubtracts(int yearWork, string idQTCQuyCheDoBHXH, string sXauNoiMa)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearWork", yearWork);
                SqlParameter IdVoucherParam = new SqlParameter("@IdQTCQuyCheDoBHXH", idQTCQuyCheDoBHXH);
                SqlParameter sXauNoiMaParam = new SqlParameter("@SXauNoiMa", sXauNoiMa);
                return ctx.FromSqlRaw<BhQtcqCtctGtTroCapQuery>("EXECUTE dbo.bh_qtcq_ctct_gttrocap_index @YearWork, @IdQTCQuyCheDoBHXH, @SXauNoiMa", yearOfWorkParam, IdVoucherParam, sXauNoiMaParam).ToList();
            }
        }

        public bool IsExistExplain(Guid voucherQTCQID)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcqCtctGtTroCaps.Any(t => t.IID_QTC_Quy_ChungTu.Equals(voucherQTCQID));
            }
        }

        public int DeleteDupItem(Guid voucherID)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_bhxh_giai_thich_tro_cap_delete_duplicate_item @VoucherID";
                var parameters = new[]
                {
                    new SqlParameter("@VoucherID", voucherID)
                };
                return ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public IEnumerable<BhQtcqBHXHChiTiet> FindDataBhxhByIdQtcqAndXauNoiMa(Guid voucherID, string sXauNoiMa, int iNamLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var sql = @"EXECUTE sp_bh_getDataQuyetToanGiaiThich @IdChungTu, @XauNoiMa, @NamLamViec";
                var parameters = new[]
                {
                    new SqlParameter("@IdChungTu",voucherID),
                    new SqlParameter("@XauNoiMa",sXauNoiMa),
                    new SqlParameter("@NamLamViec",iNamLamViec),
                };
                return ctx.FromSqlRaw<BhQtcqBHXHChiTiet>(sql, parameters);
            }
        }

        public List<BhQtcqCtctGtTroCap> FindByForIdChungTu(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcqCtctGtTroCaps.Where(x => x.IID_QTC_Quy_ChungTu == id).ToList();
            }
        }

        public List<BhQtcqCtctGtTroCap> FindExplainSubtracts (int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcqCtctGtTroCaps.Where(x => x.INamLamViec== namLamViec).ToList();
            }
        }
    }
}
