using System.Data.SqlClient;
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
    public class VdtQtQuyetToanRepository : Repository<VdtQtQuyetToan>, IVdtQtQuyetToanRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtQtQuyetToanRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<VdtQtQuyetToanQuery> FindAllPheDuyetQuyetToan(int yearOfWork, string userName)
        {
            try
            {
                using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
                {
                    SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", yearOfWork);
                    SqlParameter userNameParam = new SqlParameter("@UserName", userName);
                    return ctx.FromSqlRaw<VdtQtQuyetToanQuery>("EXECUTE dbo.sp_vdt_get_phe_duyet_quyet_toan @YearOfWork, @UserName", yearOfWorkParam, userNameParam).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<VdtQtQuyetToanQuery>();
            }
        }

        public List<NguonVonQuyetToanQuery> GetNguonVonByDuToanIdDeNghiQuyetToanId(string duToanId, string deNghiQuyetToanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter duToandParam = new SqlParameter("@duToanId", duToanId);
                SqlParameter quyetToanParam = new SqlParameter("@quyetToanId", deNghiQuyetToanId);
                return ctx.FromSqlRaw<NguonVonQuyetToanQuery>("EXECUTE dbo.sp_vdt_get_all_dutoan_nguonvon_by_list_dutoan_id_quyettoan_id @duToanId, @quyetToanId",
                    duToandParam, quyetToanParam).ToList();
            }
        }

        public void UpdateTienQuyetToan(string quyetToanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter quyetToanIdParam = new SqlParameter("@IdQuyetToan", quyetToanId);
                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_vdt_update_tien_quyet_toan_qt_quyettoan @IdQuyetToan", quyetToanIdParam);
            }
        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                VdtQtQuyetToan entity = ctx.VdtQtQuyetToans.Find(id);
                entity.BKhoa = lockStatus;
                ctx.Entry(entity).State = EntityState.Modified;
                return ctx.SaveChanges();
            }
        }

        public bool IsExistSoQuyetDinh(string soQuyetDinh, Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtQtQuyetToans.Any(n =>
                        n.SSoQuyetDinh == soQuyetDinh
                    && (id == Guid.Empty || n.Id != id));
            }
        }
    }
}