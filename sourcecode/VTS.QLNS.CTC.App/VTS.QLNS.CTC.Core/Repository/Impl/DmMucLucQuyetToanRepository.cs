using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;
using static Dapper.SqlMapper;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class DmMucLucQuyetToanRepository : Repository<NsMucLucQuyetToanNam>, IDmMucLucQuyetToanRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public DmMucLucQuyetToanRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void AddOrUpdateRange(IEnumerable<NsMucLucQuyetToanNam> entities, AuthenticationInfo authenticationInfo)
        {
            var time = DateTime.Now;
            if (entities.Any(n => n.IsDeleted))
            {
                ClearMlnsByIdMlqt(entities.Where(n => n.IsDeleted).Select(n => n.Ma).ToList());
            }
            using (var ctx = _contextFactory.CreateDbContext())
            {
                foreach (var entity in entities)
                {
                    var current = ctx.NsMucLucQuyetToanNams.AsNoTracking().FirstOrDefault(i => i.Id.Equals(entity.Id));
                    if (entity.IsDeleted)
                    {
                        if (!entity.Id.Equals(Guid.Empty))
                        {
                            ctx.Remove(entity);
                        }
                    }
                    else if (entity.IsModified)
                    {
                        if (!entity.Id.Equals(Guid.Empty) && current != null)
                        {
                            ctx.Update(entity);
                        }
                        else
                        {
                            entity.DNgayTao = time;
                            entity.SNguoiTao = authenticationInfo.Principal;
                            ctx.Add(entity);
                        }
                    }
                }
                ctx.SaveChanges();
            }
        }

        public void AddRangeMlns(IEnumerable<NsMucLucQuyetToanNamMLNS> entities, AuthenticationInfo authenticationInfo)
        {
            var time = DateTime.Now;
            var ids = entities.Where(n => !string.IsNullOrEmpty(n.XauNoiMa)).Select(c => c.Id).Distinct();
            using (var ctx = _contextFactory.CreateDbContext())
            { 
                foreach(var entity in entities)
                {
                    if (entity.IsModified && !string.IsNullOrEmpty(entity.XauNoiMa))
                    {
                        ctx.NsMucLucQuyetToanNamMLNSs.Add(entity);
                    }
                }
                ctx.SaveChanges();
            }
        }

        public override IEnumerable<NsMucLucQuyetToanNam> FindAll()
        {
            using(var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsMucLucQuyetToanNams.ToList();
            }
        }

        public IEnumerable<NsMucLucQuyetToanNam> FindByCondition(Expression<Func<NsMucLucQuyetToanNam, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsMucLucQuyetToanNams.Where(predicate).ToList();
            }
        }

        public IEnumerable<NsMucLucQuyetToanNamMLNS> FindByCondition(Expression<Func<NsMucLucQuyetToanNamMLNS, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsMucLucQuyetToanNamMLNSs.Where(predicate).ToList();
            }
        }


        public IEnumerable<NsMucLucQuyetToanNamMLNS> GetAllMlns(IEnumerable<string> ids, int yearOfWork)
        {
            using var ctx = _contextFactory.CreateDbContext();
            return ctx.NsMucLucQuyetToanNamMLNSs.Where(c => ids.Contains(c.MaMLQT) && c.NamLamViec == yearOfWork).ToList();
        }

        public IEnumerable<PrintPublicFinanceQuery> ReportPublicFinance(int yearOfWork, string ids, int yearOfBudget, int budgetSource)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.ns_rpt_congkhaitaichinh @NamLamViec, @idDanhMuc, @NamNganSach, @MaNguonNganSach";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", yearOfWork),
                    new SqlParameter("@idDanhMuc",ids),
                    new SqlParameter("@NamNganSach",yearOfBudget),
                    new SqlParameter("@MaNguonNganSach",budgetSource)
                };
                var result = ctx.FromSqlRaw<PrintPublicFinanceQuery>(sql, parameters).ToList();

                return result;
            }
        }

        public void ClearMlnsByIdMlqt(List<string> ids)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "DELETE NS_MucLucQuyetToanNam_MLNS WHERE sMaMLQT in (SELECT * FROM f_split(@lstId))";
                var parameters = new[]
                {
                    new SqlParameter("@lstId", string.Join(",", ids))
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public void UpdateIsHangCha()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = @"UPDATE tbl
                                SET bHangCha = 0
                                FROM NS_DanhMucCongKhai as tbl
                                LEFT JOIN NS_DanhMucCongKhai as dt on tbl.Id = dt.iID_DMCongKhai_Cha
                                WHERE tbl.bHangCha = 1 AND dt.iID_DMCongKhai_Cha IS NULL";
                ctx.Database.ExecuteSqlCommand(sql);
            }
        }
    }
}
