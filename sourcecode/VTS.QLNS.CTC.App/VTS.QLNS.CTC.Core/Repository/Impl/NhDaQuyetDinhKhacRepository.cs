using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhDaQuyetDinhKhacRepository : Repository<NhDaQuyetDinhKhac>, INhDaQuyetDinhKhacRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDaQuyetDinhKhacRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public IEnumerable<NhDaQuyetDinhKhac> FindAll(AuthenticationInfo authenticationInfo)
        {
            return FindAll();
        }

        public IEnumerable<NhDaQuyetDinhKhacQuery> FindIndex(int iThuocMenu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_get_nh_da_quyetdinhkhac_index @iThuocMenu";
                var parameters = new[] {
                    new SqlParameter("@iThuocMenu", iThuocMenu)
                };
                return ctx.FromSqlRaw<NhDaQuyetDinhKhacQuery>(sql, parameters).ToList();
            }
        }
        public bool CheckDuplicateSoQD(string soQuyetDinh, Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var listChiPhiKhac = ctx.NhDaQuyetDinhKhacs.Where(x => x.SSoQuyetDinh == soQuyetDinh && x.Id != id).ToList();

                if (listChiPhiKhac.Count > 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
