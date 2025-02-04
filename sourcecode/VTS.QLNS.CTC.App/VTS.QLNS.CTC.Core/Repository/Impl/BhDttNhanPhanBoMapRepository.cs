using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class BhDttNhanPhanBoMapRepository : Repository<BhDttNhanPhanBoMap>, IBhDttNhanPhanBoMapRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public BhDttNhanPhanBoMapRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void DeleteByIdPhanBoDuToan(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter idPhanBoDuToanParam = new SqlParameter("@idPhanBoDuToanParam", id.ToString());
                ctx.Database.ExecuteSqlCommand($"DELETE FROM BH_DTT_Nhan_Phan_Bo_Map WHERE iID_CTDuToan_PhanBo = @idPhanBoDuToanParam", idPhanBoDuToanParam);
            }
        }

        public IEnumerable<BhDttNhanPhanBoMap> FindByIdNhanDuToan(Guid idNhanDuToan)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var a = ctx.BhDttNhanPhanBoMaps.ToList();
                return ctx.BhDttNhanPhanBoMaps.Where(n => n.IIdCtduToanNhan == idNhanDuToan).ToList();
            }
        }

        public IEnumerable<BhDttNhanPhanBoMap> FindByIdPhanBoDuToan(string idPhanBoDuToan)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhDttNhanPhanBoMaps.Where(n => n.IIdCtduToanPhanBo.ToString() == idPhanBoDuToan).ToList();
            }
        }

        public IEnumerable<BhDttNhanPhanBoMap> FindByListIdNhanDuToan(IEnumerable<string> listIdNhanDuToan)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhDttNhanPhanBoMaps.Where(n => listIdNhanDuToan.Contains(n.IIdCtduToanNhan.ToString())).ToList();
            }
        }

        public IEnumerable<BhDttNhanPhanBoMap> FindByListIdPhanBo(IEnumerable<string> listIdPhanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhDttNhanPhanBoMaps.Where(n => listIdPhanBo.Contains(n.IIdCtduToanPhanBo.ToString())).ToList();
            }
        }
    }
}
