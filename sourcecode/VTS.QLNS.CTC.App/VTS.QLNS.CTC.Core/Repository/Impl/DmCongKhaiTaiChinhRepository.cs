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
    public class DmCongKhaiTaiChinhRepository : Repository<NsDanhMucCongKhai>, IDmCongKhaiTaiChinhRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public DmCongKhaiTaiChinhRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void AddOrUpdateRange(IEnumerable<NsDanhMucCongKhai> entities, AuthenticationInfo authenticationInfo)
        {
            var time = DateTime.Now;
            if (entities.Any(n => n.IsDeleted))
            {
                ClearMlnsByiIdDmCongKhai(entities.Where(n => n.IsDeleted).Select(n => n.Id).ToList());
            }
            using (var ctx = _contextFactory.CreateDbContext())
            {
                foreach (var entity in entities)
                {
                    var current = ctx.NsDanhMucCongKhais.AsNoTracking().FirstOrDefault(i => i.Id.Equals(entity.Id));
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
                            entity.dNgayTao = time;
                            entity.sNguoiTao = authenticationInfo.Principal;
                            ctx.Add(entity);
                        }
                    }
                }
                ctx.SaveChanges();
            }
        }

        public void AddRangeMlns(IEnumerable<NsDmCongKhaiMlns> entities, AuthenticationInfo authenticationInfo)
        {
            var time = DateTime.Now;
            var ids = entities.Where(n => !string.IsNullOrEmpty(n.sNS_XauNoiMa)).Select(c => c.iID_DMCongKhai).Distinct();
            using (var ctx = _contextFactory.CreateDbContext())
            { 
                //var deleteds = ctx.NsDmCongKhaiMlns.Where(c => ids.Contains(c.iID_DMCongKhai));
                //ctx.NsDmCongKhaiMlns.RemoveRange(deleteds);
                //ctx.NsDmCongKhaiMlns.AddRange(entities);
                foreach(var entity in entities)
                {
                    if (entity.IsModified && !string.IsNullOrEmpty(entity.sNS_XauNoiMa))
                    {
                        ctx.NsDmCongKhaiMlns.Add(entity);
                    }
                }
                ctx.SaveChanges();
            }
        }

        public override IEnumerable<NsDanhMucCongKhai> FindAll()
        {
            using(var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDanhMucCongKhais.ToList();
            }
        }

        public IEnumerable<NsDanhMucCongKhai> FindByCondition(Expression<Func<NsDanhMucCongKhai, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDanhMucCongKhais.Where(predicate).ToList();
            }
        }

        public IEnumerable<NsDmCongKhaiMlns> FindByCondition(Expression<Func<NsDmCongKhaiMlns, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDmCongKhaiMlns.Where(predicate).ToList();
            }
        }


        public IEnumerable<NsDmCongKhaiMlns> GetAllMlns(IEnumerable<Guid> ids)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDmCongKhaiMlns.Where(c => ids.Contains(c.iID_DMCongKhai))
                    .ToList();
            }
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

        public void ClearMlnsByiIdDmCongKhai(List<Guid> lstIdDmCongKhai)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "DELETE Ns_DmCongKhai_Mlns WHERE iID_DMCongKhai in (SELECT * FROM f_split(@lstId))";
                var parameters = new[]
                {
                    new SqlParameter("@lstId", string.Join(",", lstIdDmCongKhai))
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

        public IEnumerable<NsDanhMucCongKhai> GetByXauNoiMaMlns(string xauNoiMa, int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_dt_PAPBDT_m02_get_dm_congkhai @XauNoiMa, @NamLamViec";
                var parameters = new[]
                {
                    new SqlParameter("@XauNoiMa", xauNoiMa),
                    new SqlParameter("@NamLamViec",yearOfWork)
                };
                var result = ctx.FromSqlRaw<NsDanhMucCongKhai>(sql, parameters).ToList();

                return result;
            }
        }
    }
}
