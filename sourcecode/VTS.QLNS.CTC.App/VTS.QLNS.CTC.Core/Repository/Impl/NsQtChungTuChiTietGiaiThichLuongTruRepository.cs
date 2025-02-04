using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NsQtChungTuChiTietGiaiThichLuongTruRepository : Repository<NsQtChungTuChiTietGiaiThichLuongTru>, INsQtChungTuChiTietiGiaiThichLuongTruRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NsQtChungTuChiTietGiaiThichLuongTruRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void DeleteByVoucherId(Guid voucherId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = $"DELETE FROM NS_QT_ChungTuChiTiet_GiaiThich_LuongTru WHERE iID_QTChungTu = @VoucherId";
                var parameters = new[]
                {
                    new SqlParameter("@VoucherId", voucherId.ToString())
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }
    }
}
