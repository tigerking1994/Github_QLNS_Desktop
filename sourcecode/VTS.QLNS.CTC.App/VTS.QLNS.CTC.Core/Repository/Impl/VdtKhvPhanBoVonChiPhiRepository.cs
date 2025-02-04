using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtKhvPhanBoVonChiPhiRepository : Repository<VdtKhvPhanBoVonChiPhi>, IVdtKhvPhanBoVonChiPhiRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtKhvPhanBoVonChiPhiRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public List<VdtKhvPhanBoVonChiPhi> GetVdtKhvPhanBoVonChiPhiInThanhToanChiPhiDialog(string sMaDonVi, Guid iIdDuAnId, int iNamKeHoach)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtKhvPhanBoVonChiPhis.Where(n => 
                    (n.BActive ?? false)
                    && n.IIdMaDonVi == sMaDonVi
                    && n.INamKeHoach == iNamKeHoach
                ).ToList();
            }
        }

        public IEnumerable<VdtKhvPhanBoVonChiPhiQuery> FindGiaoDuToanIndex()
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE sp_nh_quanli_giao_duan_index"; 
                var result = ctx.FromSqlRaw<VdtKhvPhanBoVonChiPhiQuery>(executeSql).ToList();
                return result;
            }
        }

        public bool IsExistSoQuyetDinh(string soQuyetDinh, Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtKhvPhanBoVonChiPhis.Any(n =>
                        n.SSoQuyetDinh == soQuyetDinh
                    && (id == Guid.Empty || n.Id != id));
            }
        }
    }
}
