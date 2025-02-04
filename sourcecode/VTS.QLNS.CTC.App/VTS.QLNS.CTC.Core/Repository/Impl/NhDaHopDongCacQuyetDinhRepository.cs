using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhDaHopDongCacQuyetDinhRepository : Repository<NhDaHopDongCacQuyetDinh>, INhDaHopDongCacQuyetDinhRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDaHopDongCacQuyetDinhRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public IEnumerable<NhDaHopDongCacQuyetDinh> FindByHopDongIdQuyetDinhId(Guid? idHopDong, Guid? idQuyetDinh)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NhDaHopDongCacQuyetDinhs.Where(x => x.IIdHopDongId == idHopDong && x.IIdCacQuyetDinhId == idQuyetDinh).ToList();
            }
        }
        public IEnumerable<NhDaHopDongCacQuyetDinh> FindByIdHopDong(Guid? idHopDong)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NhDaHopDongCacQuyetDinhs.Where(x => x.IIdHopDongId == idHopDong).ToList();
            }
        }
        public void DeleteQuyetDinh(Guid? idHopDong, Guid? idQuyetDinh)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_nh_da_delete_quyetdinhhopdong @idHopDong, @idQuyetDinh";
                var parameters = new[]
                {
                    new SqlParameter("@idHopDong", idHopDong),
                    new SqlParameter("@idQuyetDinh", idQuyetDinh.IsNullOrEmpty() ? DBNull.Value : (object)idQuyetDinh)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }
    }
}
