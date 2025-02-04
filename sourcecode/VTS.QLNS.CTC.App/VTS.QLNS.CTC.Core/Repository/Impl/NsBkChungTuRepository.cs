using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NsBkChungTuRepository : Repository<NsBkChungTu>, INsBkChungTuRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NsBkChungTuRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void LockOrUnlockMultiple(List<NsBkChungTu> chungTus, bool isLock)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = $"UPDATE NS_BK_ChungTu SET bKhoa = @LockParam WHERE iID_BKChungTu IN (SELECT * FROM f_split(@VoucherIds))";
                var parameters = new[]
                {
                    new SqlParameter("@VoucherIds", string.Join(",", chungTus.Select(x => x.Id))),
                    new SqlParameter("@LockParam", isLock ? 1 : 0)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }
    }
}
