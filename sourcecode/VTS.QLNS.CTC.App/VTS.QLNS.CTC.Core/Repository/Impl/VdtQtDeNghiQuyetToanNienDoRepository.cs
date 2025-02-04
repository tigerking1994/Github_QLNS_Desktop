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
    public class VdtQtDeNghiQuyetToanNienDoRepository : Repository<VdtQtDeNghiQuyetToanNienDo>, IVdtQtDeNghiQuyetToanNienDoRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtQtDeNghiQuyetToanNienDoRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public List<VdtQtDenghiQuyetToanNienDoQuery> GetDeNghiQuyetToanNienDoIndex()
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtQtDenghiQuyetToanNienDoQuery>("EXECUTE dbo.sp_qt_quyettoanniendo_index").ToList();
            }
        }

        public bool CheckExistDeNghiQuyetToanNienDo(Guid iIdDonVi, int iNamKeHoach, int iNguonVon, Guid iIdLoaiNguonvon)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtQtDeNghiQuyetToanNienDos.Any(n => !n.DDateDelete.HasValue && n.IIdDonViDeNghiId == iIdDonVi && n.INamKeHoach == iNamKeHoach && n.IIdNguonVonId == iNguonVon && n.IIdLoaiNguonVonId == iIdLoaiNguonvon);
            }
        }
    }
}
