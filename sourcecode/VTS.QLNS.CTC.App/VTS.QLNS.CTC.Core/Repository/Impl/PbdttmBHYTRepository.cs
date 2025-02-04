using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class PbdttmBHYTRepository : Repository<BhPbdttmBHYT>, IPbdttmBHYTRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public PbdttmBHYTRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhPbdttmBHYT> FindByCondition(Expression<Func<BhPbdttmBHYT, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhPbdttmBHYTs.Where(predicate).ToList();
            }
        }

        public int GetSoChungTuIndexByCondition(int iNamLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.BhPbdttmBHYTs.Where(x=> x.INamLamViec == iNamLamViec).ToList();
                if (result.Count <= 0) return 1;
                try
                {
                    var sSoChungTuMax = result.OrderByDescending(x => x.SSoChungTu).FirstOrDefault().SSoChungTu;
                    var indexString = sSoChungTuMax.Substring(4, sSoChungTuMax.Length - 4);
                    var index = int.Parse(indexString) + 1;
                    return index;
                }
                catch (Exception)
                {
                    return result.Count + 1;
                }
            }
        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                BhPbdttmBHYT entity = ctx.BhPbdttmBHYTs.Find(id);
                if (entity != null) entity.BIsKhoa = lockStatus;
                return Update(entity);
            }
        }

        public IEnumerable<BhPbdttmBHYT> FindDotNhanByChungTuPhanBo(Guid idPhanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter idPhanBoParam = new SqlParameter("@IdPhanBo", idPhanBo.ToString());
                return ctx.BhPbdttmBHYTs.FromSql("EXECUTE sp_bh_dttm_danhsach_dotnhan @IdPhanBo", idPhanBoParam).ToList();
            }
        }

        public IEnumerable<BhPbdttmBHYT> FindBySoQuyetDinh(string soQuyetDinh, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhPbdttmBHYTs.Where(y => y.INamLamViec == nam && y.SSoQuyetDinh == soQuyetDinh).ToList();
            }
        }

        public IEnumerable<BhPbdttmBHYT> FindByLuyKeDot(DateTime? ngayQuyetDinh, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhPbdttmBHYTs.Where(y => y.INamLamViec == nam && y.DNgayQuyetDinh < ngayQuyetDinh).ToList();
            }
        }

        public IEnumerable<BhPbdttmBHYT> FindBySoChungTu(string soChungTu, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhPbdttmBHYTs.Where(y => y.INamLamViec == nam && y.SSoChungTu == soChungTu).ToList();
            }
        }

        public IEnumerable<BhPbdttmBHYTQuery> FindBySoQuyetDinhLuyKe(string soQuyetDinh, string ngayQuyetDinh, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var yearOfWorkParam = new SqlParameter("@NamLamViec", nam);
                var soQuyetDinhParam = new SqlParameter("@SoQuyetDinh", soQuyetDinh);
                var ngayQDParam = new SqlParameter("@NgayQuyetDinh", ngayQuyetDinh);
                return ctx.FromSqlRaw<BhPbdttmBHYTQuery>("EXECUTE dbo.sp_bhxh_get_chung_tu_duoc_xem_luy_ke_dttm @NamLamViec, @SoQuyetDinh, @NgayQuyetDinh",
                    yearOfWorkParam, soQuyetDinhParam, ngayQDParam).ToList();
            }
        }

        public IEnumerable<BhDuToanThuChiQuery> GetSoQuyetDinhDTTM(int year, bool isInTheoChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var yearOfWorkParam = new SqlParameter("@NamLamViec", year);
                if (isInTheoChungTu)
                {
                    return ctx.FromSqlRaw<BhDuToanThuChiQuery>("EXECUTE dbo.sp_bhxh_get_so_quyet_dinh_dttm_theo_ct @NamLamViec", yearOfWorkParam).ToList();
                }
                else
                {
                    return ctx.FromSqlRaw<BhDuToanThuChiQuery>("EXECUTE dbo.sp_bhxh_get_so_quyet_dinh_dttm @NamLamViec", yearOfWorkParam).ToList();
                }
            }
        }

        public IEnumerable<BhPbdttmBHYT> FindByIdNhanDuToan(string id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhPbdttmBHYTs.Where(y => y.SDS_DotNhan.Contains(id)).ToList();
            }
        }
    }
}
