using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtQtXuLySoLieuRepository : Repository<VdtQtXuLySoLieu>, IVdtQtXuLySoLieuRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtQtXuLySoLieuRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public bool DeleteAllXuLySoLieuByParent(VdtQtDeNghiQuyetToanNienDo objNienDo)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                List<VdtQtXuLySoLieu> lstData = ctx.VdtQtXuLySoLieus.Where(n => n.DNgayLap == objNienDo.DNgayDeNghi && n.IIdDonViQuanLyId == objNienDo.IIdDonViDeNghiId
                    && n.IIdLoaiNguonVonId == objNienDo.IIdLoaiNguonVonId && n.IIdNguonVonId == objNienDo.IIdNguonVonId && n.INamKeHoach == objNienDo.INamKeHoach).ToList();
                ctx.RemoveRange(lstData);
                return ctx.SaveChanges() != 0;
            }
        }
    }
}
