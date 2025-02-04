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
using VTS.QLNS.CTC.Utility.Enum;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static VTS.QLNS.CTC.Utility.Enum.BaoHiemDuToanTypeEnum;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class QtcqKCBRepository : Repository<BhQtcqKCB>, IQtcqKCBRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public QtcqKCBRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhQtcqKCB> FindByCondition(Expression<Func<BhQtcqKCB, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcqKCBs.Where(predicate).ToList();
            }
        }


        public IEnumerable<BhQtcqKCB> FindByYear(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcqKCBs.Where(x => x.INamChungTu == namLamViec).ToList();
            }
        }

        public int GetSoChungTuIndexByCondition(int iNamLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.BhQtcqKCBs.Where(x => x.INamChungTu == iNamLamViec).ToList();
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
                BhQtcqKCB entity = ctx.BhQtcqKCBs.Find(id);
                entity.BIsKhoa = lockStatus;
                ctx.Entry(entity).State = EntityState.Modified;
                return ctx.SaveChanges();
            }
        }

        public bool IsExistChungTuTongHop(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcqKCBs.Any(t => t.INamChungTu == namLamViec && t.ILoaiTongHop == BhxhLoaiChungTu.BhxhChungTuTongHop);
            }
        }

        public IEnumerable<BhQtcqKCBQuery> GetDanhSachQuyetToanKCB(int iNamLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@INamLamViec", iNamLamViec);


                return ctx.FromSqlRaw<BhQtcqKCBQuery>("EXECUTE sp_bh_quyet_toan_danhsachquyettoanquyKCB_index @INamLamViec", iNamLamViecParam).ToList();
            }
        }

        public void UpdateTotalCPChungTu(string voucherId, string userModify)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_qtcqKCB_update_total @VoucherId, @UserModify";
                var parameters = new[]
                {
                    new SqlParameter("@VoucherId", voucherId),
                    new SqlParameter("@UserModify", userModify)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public List<DonVi> FindByDonViForNamLamViec(int yearOfWork, int iQuy, int chungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", yearOfWork);
                SqlParameter searchQuyParam = new SqlParameter("@Quy", iQuy);
                SqlParameter searchLoaiCtParam = new SqlParameter("@LoaiChungTu", chungTu);
                return ctx.FromSqlRaw<DonVi>("EXECUTE sp_cp_rpt_get_donvi_qkcb_lns @NamLamViec, @Quy, @LoaiChungTu", iNamLamViecParam, searchQuyParam, searchLoaiCtParam).ToList();
            }
        }

        public IEnumerable<BhQtcqKCB> FindByAggregateVoucher(List<string> voucherNos, int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcqKCBs.Where(x => voucherNos.Contains(x.SSoChungTu) && x.INamChungTu == yearOfWork).ToList();
            }
        }

        public List<DonVi> FindByDonViTongChiForNamLamViec(int yearOfWork, int iQuy)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", yearOfWork);
                SqlParameter searchQuyParam = new SqlParameter("@Quy", iQuy);
                return ctx.FromSqlRaw<DonVi>("EXECUTE sp_qt_rpt_get_donvi_qkcb @NamLamViec, @Quy", iNamLamViecParam, searchQuyParam).ToList();
            }
        }
    }
}
