using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhDaChuTruongDauTuRepository : Repository<NhDaChuTruongDauTu>, INhDaChuTruongDauTuRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDaChuTruongDauTuRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhDaChuTruongDauTuQuery> FindIndex(int yearOfWork, int ILoai)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_nh_chutruongdautu_index @YearOfWork, @iLoai";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", yearOfWork),
                    new SqlParameter("@iLoai", ILoai)
                };
                return ctx.FromSqlRaw<NhDaChuTruongDauTuQuery>(executeSql, parameters).ToList();
            }
        }
    }
}
