using Microsoft.EntityFrameworkCore;
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
using static VTS.QLNS.CTC.Utility.Enum.BaoHiemDuToanTypeEnum;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhKtKhoiTaoCapPhatRepository : Repository<NhKtKhoiTaoCapPhat>, INhKtKhoiTaoCapPhatRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public NhKtKhoiTaoCapPhatRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void DeleteKhoiTaoTheoQuyetDinh(Guid id, int Type)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.Database.ExecuteSqlCommand("EXECUTE sp_nh_delete_duan_qddt_hd_qdk_by_idKhoiTao @iIdKhoiTaoQdk, @type",
                     new SqlParameter("@iIdKhoiTaoQdk", id),
                     new SqlParameter("@type", Type));
            }
        }

        public IEnumerable<NhKtKhoiTaoCapPhatQuery> GetAll(int INamLamViecDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_nh_kt_khoi_tao_cap_phat @INamLamViec";
                var parameters = new[]
                {
                    new SqlParameter("@INamLamViec", INamLamViecDonVi),
                };
                return ctx.FromSqlRaw<NhKtKhoiTaoCapPhatQuery>(executeSql, parameters).ToList();
            }
        }
    }
}
