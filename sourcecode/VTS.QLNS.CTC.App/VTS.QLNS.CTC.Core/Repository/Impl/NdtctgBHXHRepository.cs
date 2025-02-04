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

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NdtctgBHXHRepository : Repository<BhDtctgBHXH>, INdtctgBHXHRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NdtctgBHXHRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhDtctgBHXH> FindByCondition(Expression<Func<BhDtctgBHXH, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhDtctgBHXHs.Where(predicate).ToList();
            }
        }

        public IEnumerable<BhDtctgBHXHQuery> FindByYear(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                return ctx.FromSqlRaw<BhDtctgBHXHQuery>("EXECUTE dbo.sp_bhxh_nhandutoanchitrengiao_index_clone @YearOfWork", yearOfWorkParam).ToList();
            }
        }

        public int GetSoChungTuIndexByCondition(int iNamLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.BhDtctgBHXHs.Where(x=> x.INamLamViec == iNamLamViec).ToList();
                if (result.Count <= 0) return 1;
                try
                {
                    var sSoChungTuMax = result.OrderByDescending(x => x.SSoChungTu).FirstOrDefault().SSoChungTu;
                    var indexString = sSoChungTuMax.Substring(4, 3);
                    var index = int.Parse(indexString) + 1;
                    return index;
                }
                catch (Exception)
                {
                    return result.Count + 1;
                }
            }
        }

        public IEnumerable<BhDtctgBHXHQuery> GetDuToanDanhSachDotNhanPhanBo(int iNamLamViec, DateTime date, int iLoaiDuToanNhan)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", iNamLamViec);
                SqlParameter dateParam = new SqlParameter("@Date", date);
                SqlParameter iLoaiDuToanNhanParam = new SqlParameter("@LoaiDuToanNhan", iLoaiDuToanNhan);
                return ctx.FromSqlRaw<BhDtctgBHXHQuery>("EXECUTE dbo.sp_bh_dutoan_dotnhan_phanbo_find_all_clone @YearOfWork,@Date,@LoaiDuToanNhan ",
                    yearOfWorkParam, dateParam, iLoaiDuToanNhanParam).ToList();
            }
        }


        public bool IsExitsDuToanDaDuocPhanBo(Guid iDuToanNhan, Guid iDuToanPhanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhdtcnpbMapBHXHs.Any(t => t.iID_BHDTC_NhanPhanBo == iDuToanNhan && t.iID_BHDTC_PhanBo == iDuToanPhanBo);
            }
        }

        
    }

}
