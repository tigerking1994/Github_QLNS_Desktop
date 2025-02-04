using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhQtChuyenQuyetToanChiTietRepository : Repository<NhQtChuyenQuyetToanChiTiet>, INhQtChuyenQuyetToanChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhQtChuyenQuyetToanChiTietRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void DeleteByChuyenQuyetToanId(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var detailList = FindAll().Where(x => x.iID_ChuyenQuyetToanID.HasValue && x.iID_ChuyenQuyetToanID.Value.Equals(id));
                if (detailList.Any())
                {
                    ctx.RemoveRange(detailList);
                    ctx.SaveChanges();
                }
            }
        }

        public void Save(NhQtChuyenQuyetToanChiTiet entity)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var exist = ctx.NhQtChuyenQuyetToanChiTiets.Any(t => t.Id.Equals(entity.Id));
                if (exist)
                    ctx.Update(entity);
                else
                    ctx.Add(entity);
                ctx.SaveChanges();
            }
        }
    }
}
