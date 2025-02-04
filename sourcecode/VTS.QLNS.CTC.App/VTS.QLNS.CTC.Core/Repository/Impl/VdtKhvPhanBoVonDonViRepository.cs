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
    public class VdtKhvPhanBoVonDonViRepository : Repository<VdtKhvPhanBoVonDonVi>, IVdtKhvPhanBoVonDonViRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtKhvPhanBoVonDonViRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<VdtKhvPhanBoVonDonViQuery> GetDataPhanBoVonDonViIndexView()
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtKhvPhanBoVonDonViQuery>("EXECUTE dbo.sp_vdt_phanbovon_donvi_index").ToList();
            }
        }

        public bool ExistPhanBoVonBySoQuyetDinhAndDonVi(VdtKhvPhanBoVonDonVi objPhanBoVon)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtKhvPhanBoVonDonVis.Any(n => n.IIdMaDonViQuanLy == objPhanBoVon.IIdMaDonViQuanLy
                    && n.SSoQuyetDinh == objPhanBoVon.SSoQuyetDinh
                    && (objPhanBoVon.Id == Guid.Empty || n.Id != objPhanBoVon.Id));
            }
        }

        public bool ExistPhanBoVonByNamKeHoachAndDonViQuanLy(VdtKhvPhanBoVonDonVi objPhanBoVon)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtKhvPhanBoVonDonVis.Any(n => n.IIdMaDonViQuanLy == objPhanBoVon.IIdMaDonViQuanLy
                   && n.INamKeHoach == objPhanBoVon.INamKeHoach
                   && n.IIdNguonVonId == objPhanBoVon.IIdNguonVonId
                   && (n.BActive ?? false) == true);
            }
        }

        public IEnumerable<VdtKhvPhanBoVonDonVi> GetPhanBoVonByListId(List<Guid> lstId, int yearPlan)
        {
            using(ApplicationDbContext ctxt = _contextFactory.CreateDbContext())
            {
                return ctxt.VdtKhvPhanBoVonDonVis.Where(x => lstId.Contains(x.Id) && x.INamKeHoach < yearPlan).ToList();
            }    
        }

        public VdtKhvPhanBoVonDonVi FindAggregateVoucher(string sTongHop)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtKhvPhanBoVonDonVis.Where(x => x.STongHop == sTongHop).FirstOrDefault();
            }
        }
    }
}
