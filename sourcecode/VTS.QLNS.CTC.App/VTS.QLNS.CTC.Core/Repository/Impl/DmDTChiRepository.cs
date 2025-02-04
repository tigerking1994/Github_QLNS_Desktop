using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class DmDTChiRepository : Repository<VdtDmDuToanChi>, IDmDTChiRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public DmDTChiRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<VdtDmDuToanChi> FindAllDTChi()
        {
            using(var ctx = _contextFactory.CreateDbContext())
            {
                var rs = ctx.VdtDmDuToanChi.ToList();
                foreach (var item in rs)
                {
                    item.DuToanChiParent = rs.FirstOrDefault(t => t.IIdDuToanChi.Equals(item.IIdDuToanChiParent))?.STenDuToanChi;
                }
                return OrderHopDongChiPhi(rs);
            }
        }

        private List<VdtDmDuToanChi> OrderHopDongChiPhi(IEnumerable<VdtDmDuToanChi> datas)
        {
            List<VdtDmDuToanChi> results = new List<VdtDmDuToanChi>();
            if (datas == null) return results;
            foreach (var item in datas.Where(n => !n.IIdDuToanChiParent.HasValue))
            {
                results.AddRange(RecusiveChiPhi(item, datas.ToList()));
            }
            return results;
        }

        private List<VdtDmDuToanChi> RecusiveChiPhi(VdtDmDuToanChi item, List<VdtDmDuToanChi> datas)
        {
            List<VdtDmDuToanChi> results = new List<VdtDmDuToanChi>();
            results.Add(item);
            foreach (var child in datas.Where(n => n.IIdDuToanChiParent == item.IIdDuToanChi))
            {
                results.AddRange(RecusiveChiPhi(child, datas));
            }
            return results;
        }

        public new void AddOrUpdateRange(IEnumerable<VdtDmDuToanChi> entities)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                foreach (var entity in entities)
                {
                    if (entity.IsDeleted)
                    {
                        if (!entity.IIdDuToanChi.Equals(Guid.Empty))
                        {
                            ctx.Remove(entity);
                        }
                    }
                    else if (entity.IsModified)
                    {
                        if (!entity.IIdDuToanChi.Equals(Guid.Empty))
                        {
                            ctx.Update(entity);
                        }
                        else
                        {
                            ctx.Add(entity);
                        }
                    }
                }
                ctx.SaveChanges();
            }
        }
    }
}
