using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtKhvKeHoach5NamDeXuatRepository : Repository<VdtKhvKeHoach5NamDeXuat>, IVdtKhvKeHoach5NamDeXuatRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtKhvKeHoach5NamDeXuatRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public VdtKhvKeHoach5NamDeXuat FindAggregateVoucher(string sTongHop)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtKhvKeHoach5NamDeXuats.Where(x => x.STongHop == sTongHop).FirstOrDefault();
            }
        }

        public IEnumerable<VdtKhvKeHoach5NamDeXuat> FindByIdDonViParent(string idDonVi, int type, int iGiaiDoanTu, int iGiaiDoanDen)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtKhvKeHoach5NamDeXuats.Where(x =>x.IIdMaDonViQuanLy.Equals(idDonVi) && x.ILoai.Equals(type) && x.IGiaiDoanTu == iGiaiDoanTu && x.IGiaiDoanDen == x.IGiaiDoanDen && !string.IsNullOrEmpty(x.STongHop)).ToList();
            }
        }

        public IEnumerable<VdtKhvKeHoach5NamDeXuatQuery> FindConditionIndex(int yearOfWork)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtKhvKeHoach5NamDeXuatQuery>("EXECUTE dbo.sp_vdt_kehoach5nam_dexuat_index @yearOfWork",
                    new SqlParameter("@yearOfWork", yearOfWork)).ToList();
            }
        }

        public IEnumerable<VdtKhvKeHoach5NamDeXuatQuery> FindConditionAll()
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtKhvKeHoach5NamDeXuatQuery>("EXECUTE dbo.sp_vdt_kehoach5nam_dexuat_index_find_all").ToList();
            }
        }

        public bool IsExistSoQuyetDinh(string soQuyetDinh, Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtKhvKeHoach5NamDeXuats.Any(n =>
                        n.SSoQuyetDinh == soQuyetDinh
                    && (id == Guid.Empty || n.Id != id));
            }
        }
    }
}
