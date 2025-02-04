using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhDaQdDauTuRepository : Repository<NhDaQdDauTu>, INhDaQdDauTuRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDaQdDauTuRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public NhDaQdDauTu FindByDuAnId(Guid iIdDuAnId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NhDaQdDauTu.FirstOrDefault(n => n.IIdDuAnId == iIdDuAnId && (n.BIsActive ?? false));
                }
        }
        
        public IEnumerable<NhDaQdDauTuQuery> FindIndex(int yearOfWork, int iLoai)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_nh_qddautu_index @YearOfWork, @iLoai";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", yearOfWork),
                    new SqlParameter("@iLoai", iLoai)
                };
                return ctx.FromSqlRaw<NhDaQdDauTuQuery>(executeSql, parameters).ToList();
            }
        }
        public bool CheckDuplicateSoQD(string soQuyetDinh, Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var listQdDauTu = ctx.NhDaQdDauTu.Where(x => x.SSoQuyetDinh == soQuyetDinh && x.Id != id).ToList();

                return (listQdDauTu.Count > 0);
            }
        }
    }
}
