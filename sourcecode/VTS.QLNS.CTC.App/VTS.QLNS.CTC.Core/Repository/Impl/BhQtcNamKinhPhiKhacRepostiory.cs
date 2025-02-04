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
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class BhQtcNamKinhPhiKhacRepostiory : Repository<BhQtcNamKinhPhiKhac>, IBhQtcNamKinhPhiKhacRepostiory
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public BhQtcNamKinhPhiKhacRepostiory(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void CreateQTCNamKPKFor4Quy(Guid idChungTu, string idDonVi, int iNamLamViec, string user, Guid iDLoaiCap)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_create_quyet_toan_chinamKPK_chitiet_theo4quy @IdChungTu, @IdMaDonVi, @INamLamViec, @User,@IDLoaiCap";
                var parameters = new[]
                {
                    new SqlParameter("@IdChungTu",idChungTu),
                    new SqlParameter("@IdMaDonVi", idDonVi),
                    new SqlParameter("@INamLamViec", iNamLamViec),
                    new SqlParameter("@User", user),
                    new SqlParameter("@IDLoaiCap", iDLoaiCap)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public List<DonVi> FindByDonViForNamLamViec(int yearOfWork, int chungTu, Guid idLoaiChi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", yearOfWork);
                SqlParameter searchLoaiCtParam = new SqlParameter("@LoaiChungTu", chungTu);
                SqlParameter searchloaiChiQTParam = new SqlParameter("@LoaiChi", idLoaiChi);
                return ctx.FromSqlRaw<DonVi>("EXECUTE sp_qtc_rpt_get_donvi_nKPK_lns @NamLamViec, @LoaiChungTu, @LoaiChi", iNamLamViecParam, searchLoaiCtParam, searchloaiChiQTParam).ToList();
            }
        }

        public List<BhQtcNamKinhPhiKhac> FindByYear(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcNamKinhPhiKhacs.Where(x => x.INamLamViec == namLamViec).ToList();
            }
        }

        public IEnumerable<BhQtcNamKinhPhiKhacQuery> FindIndex(int iNamChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_qtc_namkinh_phikhac_chungtu_index_bh @YearOfWork";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", iNamChungTu)
                };
                return ctx.FromSqlRaw<BhQtcNamKinhPhiKhacQuery>(executeSql, parameters).ToList();
            }
        }

        public int GetSoChungTuIndexByCondition(int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.BhQtcNamKinhPhiKhacs.Where(x => x.INamLamViec == yearOfWork).OrderByDescending(x => x.SSoChungTu).Select(x => x.SSoChungTu).ToList();
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
                return ctx.BhQtcNamKinhPhiKhacs.Any(t => t.INamLamViec == namLamViec && t.ILoaiTongHop == SettlementTypeLoaiChungTu.ChungTuTongHop);
            }
        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                BhQtcNamKinhPhiKhac entity = ctx.BhQtcNamKinhPhiKhacs.Find(id);
                entity.BIsKhoa = lockStatus;
                ctx.Entry(entity).State = EntityState.Modified;
                return ctx.SaveChanges();
            }
        }
    }
}
