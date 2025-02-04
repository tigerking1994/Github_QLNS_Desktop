using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtKhvKeHoach5NamRepository : Repository<VdtKhvKeHoach5Nam>, IVdtKhvKeHoach5NamRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtKhvKeHoach5NamRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<VdtKhvKeHoach5Nam> FindByDonViQuanLy(MediumTermPlanIndexSearch condition)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtKhvKeHoach5Nams.Where(x => x.IIdMaDonViQuanLy == condition.idMaDonViQuanLy
                    && x.IGiaiDoanTu == condition.iGiaiDoanTu
                    && x.IGiaiDoanDen == condition.iGiaiDoanDen
                    && x.NamLamViec == condition.iNamLamViec
                    && x.ILoai == condition.iLoai).ToList();
            }
        }

        //public IEnumerable<VdtKhvKeHoach5Nam> FindByIdDonVi(Guid id)
        //{
        //    using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
        //    {
        //        return ctx.VdtKhvKeHoach5Nams.Where(x => x.IIdDonViQuanLyId.Equals(id)).ToList();
        //    }
        //}

        public IEnumerable<VdtKhvKeHoach5Nam> FindByIdDonViParent(string idDonVi, int type, int iGiaiDoanTu, int iGiaiDoanDen)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtKhvKeHoach5Nams.Where(x => x.IIdMaDonViQuanLy.Equals(idDonVi) && x.ILoai.Equals(type) && x.IGiaiDoanTu == iGiaiDoanTu && x.IGiaiDoanDen == iGiaiDoanDen).ToList();
            }
        }

        public IEnumerable<VdtKhvKeHoach5NamQuery> FindConditionIndex(int yearOfWork)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtKhvKeHoach5NamQuery>("EXECUTE dbo.sp_vdt_kehoach5nam_index @yearOfWork",
                    new SqlParameter("@yearOfWork", yearOfWork)).ToList();
            }
        }

        public Guid? FindIdKHTHByID(Guid? id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<Guid>("EXECUTE dbo.sp_khv_khth_find_id_khth @Id",
                    new SqlParameter("@Id", id)).ToList().FirstOrDefault();
            }
        }

        public bool IsExistSoQuyetDinh(string soQuyetDinh, Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtKhvKeHoach5Nams.Any(n =>
                        n.SSoQuyetDinh == soQuyetDinh
                    && (id == Guid.Empty || n.Id != id));
            }
        }

        IEnumerable<VdtKhvKeHoach5NamQuery> IVdtKhvKeHoach5NamRepository.FindAllDuocDuyet()
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtKhvKeHoach5NamQuery>("EXECUTE dbo.sp_vdt_kehoach5nam_index_find_all").ToList();
                    
            }
        }
    }
}
