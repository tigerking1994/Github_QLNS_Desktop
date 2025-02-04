using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtKtKhoiTaoDuLieuRepository : Repository<VdtKtKhoiTaoDuLieu>, IVdtKtKhoiTaoDuLieuRepository
    {
        private ApplicationDbContextFactory _contextFactory;
        public VdtKtKhoiTaoDuLieuRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<KhoiTaoDuLieuQuery> FindByCondition(int namLamViec)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                return ctx.FromSqlRaw<KhoiTaoDuLieuQuery>("EXECUTE dbo.sp_vdt_khoitao_dulieu_index @YearOfWork", yearOfWorkParam).ToList();
            }
        }
    }
}
