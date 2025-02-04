using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtDuAnHangMucRepository : Repository<VdtDaDuAnHangMuc>, IVdtDuAnHangMucRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtDuAnHangMucRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void DeleteByDuAnId(Guid duanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                List<VdtDaDuAnHangMuc> entitys = ctx.VdtDaDuAnHangMucs.Where(x => x.IIdDuAnId == duanId).AsTracking().ToList();
                RemoveRange(entitys);
            }
        }

        public IEnumerable<VdtDaDuAnHangMuc> FindByDuAnHangMuc(Guid idDuAn, int? nguonVon, Guid? idLoaiCongTrinh)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDaDuAnHangMucs.Where(x => x.IIdDuAnId == idDuAn && x.iID_NguonVonID == nguonVon && x.IdLoaiCongTrinh == idLoaiCongTrinh).AsTracking().ToList();
            }
        }

        public IEnumerable<VdtDaDuAnHangMuc> FindByIdDuAn(Guid idDuAn)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDaDuAnHangMucs.Where(x => x.IIdDuAnId == idDuAn).AsTracking().ToList();
            }
        }

        public int FindNextSoChungTuIndex()
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var currentIndex = ctx.VdtDaDuAnHangMucs
               .Max(x => x.indexMaHangMuc);
                return currentIndex != null ? (int)currentIndex + 1 : 1;
            }
        }
    }
}
