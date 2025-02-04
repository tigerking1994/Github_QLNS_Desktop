using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class BhQtcNamKinhPhiQuanLyRepostiory : Repository<BhQtcNamKinhPhiQuanLy>, IBhQtcNamKinhPhiQuanLyRepostiory
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public BhQtcNamKinhPhiQuanLyRepostiory(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public List<BhQtcNamKinhPhiQuanLy> FindByYear(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcNamKinhPhiQuanLys.Where(x => x.INamLamViec == namLamViec).ToList();
            }
        }

        public IEnumerable<BhQtcNamKinhPhiQuanLyQuery> FindIndex(int iNamChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_qtc_namkinhphi_quanly_chungtu_index_bh @YearOfWork";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", iNamChungTu)
                };
                return ctx.FromSqlRaw<BhQtcNamKinhPhiQuanLyQuery>(executeSql, parameters).ToList();
            }
        }

        public void CreateQTCNamKPQLFor4Quy(Guid idChungTu, string idDonVi, int iNamLamViec, string user)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_create_quyet_toan_chinamKPQL_chitiet_theo4quy @IdChungTu, @IdMaDonVi, @INamLamViec, @User";
                var parameters = new[]
                {
                    new SqlParameter("@IdChungTu",idChungTu),
                    new SqlParameter("@IdMaDonVi", idDonVi),
                    new SqlParameter("@INamLamViec", iNamLamViec),
                    new SqlParameter("@User", user)

                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public int GetSoChungTuIndexByCondition(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.BhQtcNamKinhPhiQuanLys.Where(x => x.INamLamViec == namLamViec).OrderByDescending(x => x.SSoChungTu).Select(x => x.SSoChungTu).ToList();
                if (result.Count <= 0) return 1;
                try
                {
                    var indexString = result.FirstOrDefault().Substring(4, 3);
                    var index = int.Parse(indexString) + 1;
                    return index;
                }
                catch (Exception)
                {
                    return result.Count + 1;
                }
            }
        }

        public bool IsExistChungTuTongHop(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcNamKinhPhiQuanLys.Any(t => t.INamLamViec == namLamViec && t.ILoaiTongHop == SettlementTypeLoaiChungTu.ChungTuTongHop);
            }
        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                BhQtcNamKinhPhiQuanLy entity = ctx.BhQtcNamKinhPhiQuanLys.Find(id);
                entity.BIsKhoa = lockStatus;
                ctx.Entry(entity).State = EntityState.Modified;
                return ctx.SaveChanges();
            }
        }

        public List<DonVi> FindByDonViForNamLamViec(int yearOfWork, int chungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", yearOfWork);
                SqlParameter searchLoaiCtParam = new SqlParameter("@LoaiChungTu", chungTu);
                return ctx.FromSqlRaw<DonVi>("EXECUTE sp_qtc_rpt_get_donvi_nKPQL_lns @NamLamViec, @LoaiChungTu", iNamLamViecParam, searchLoaiCtParam).ToList();
            }
        }

        public IEnumerable<BhQtcNamKinhPhiQuanLy> FindByAggregateVoucher(List<string> voucherNos, int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcNamKinhPhiQuanLys.Where(x => voucherNos.Contains(x.SSoChungTu) && x.INamLamViec == yearOfWork).ToList();
            }
        }
    }
}
