using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtDmDonViThucHienDuAnRepository : Repository<VdtDmDonViThucHienDuAn>, IVdtDmDonViThucHienDuAnRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public VdtDmDonViThucHienDuAnRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<VdtDmDonViThucHienDuAn> FindAllWithOrder()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var AllDonViNS = ctx.NsDonVis.ToList();
                var AllDonViThucHienDÂ = ctx.Set<VdtDmDonViThucHienDuAn>().ToList();
                var result = ctx.Set<VdtDmDonViThucHienDuAn>().FromSql("EXECUTE dbo.sp_vdt_dm_DonViThucHienDuAn").ToList();
                foreach (var r in result)
                {
                    r.TenDonViCha = AllDonViThucHienDÂ.FirstOrDefault(d => d.IIdDonVi.Equals(r.IIdDonViCha))?.STenDonVi;
                    r.TenDonViNS = AllDonViNS.FirstOrDefault(d => d.IIDMaDonVi.Equals(r.IIDMaDonViNS))?.TenDonVi;
                }
                return result;
            }
        }

        public int AddOrUpdateRange(IEnumerable<VdtDmDonViThucHienDuAn> entities, int iNamLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                foreach (var entity in entities)
                {
                    var tracked = ctx.VdtDmDonViThucHienDuAns.FirstOrDefault(i => i.IIdDonVi.Equals(entity.IIdDonVi));
                    if (entity.IsDeleted)
                    {
                        if (tracked != null)
                        {
                            // remove map mlns_mlskt
                            ctx.Remove(tracked);
                        }
                    }
                    else if (entity.IsModified)
                    {
                        if (tracked != null)
                        {
                            ctx.Entry(tracked).CurrentValues.SetValues(entity);
                        }
                        else
                        {
                            ctx.Set<VdtDmDonViThucHienDuAn>().Add(entity);
                        }
                    }
                }
                return ctx.SaveChanges();
            }
        }

        public VdtDmDonViThucHienDuAn FindByMaDonVi(string sMaDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDmDonViThucHienDuAns.Where(x => x.IIdMaDonVi == sMaDonVi).FirstOrDefault();
            }
        }

        public IEnumerable<NSDonViThucHienDuAnExportQuery> GetDonViThucHienDuAnExport()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<NSDonViThucHienDuAnExportQuery>("EXECUTE sp_ns_get_donvi_thuchienduan_export").ToList();
            }
        }
    }
}
