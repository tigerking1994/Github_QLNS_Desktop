using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhTtThanhToanRepository : Repository<NhTtThanhToan>, INhTtThanhToanRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhTtThanhToanRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public List<NhTtThanhToan> FindDeNghiTongHop()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NhTtThanhToan.Where(t => t.BTongHop.HasValue && t.BTongHop.Value).ToList();
            }
        }

        public IEnumerable<NhTtThanhToanQuery> FindIndex(int yearOfWork, int iTrangThai, bool bIsDeNghi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_nh_thongtinthanhtoan_index @YearOfWork, @iTrangThai, @bIsDeNghi";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", yearOfWork),
                    new SqlParameter("@iTrangThai", iTrangThai),
                    new SqlParameter("@bIsDeNghi", bIsDeNghi),
                };
                return ctx.FromSqlRaw<NhTtThanhToanQuery>(executeSql, parameters).ToList();
            }
        }

        public void TongHopDeNghiThanhToan(NhTtThanhToan NHTtDeNghiThanhToan, List<Guid> childrenIds)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var exist = ctx.NhTtThanhToan.Any(t => t.Id.Equals(NHTtDeNghiThanhToan.Id));
                if (exist)
                    ctx.Update(NHTtDeNghiThanhToan);
                else
                    ctx.Add(NHTtDeNghiThanhToan);
                var children = ctx.NhTtThanhToan.Where(t => childrenIds.Contains(t.Id)).ToList();
                children.ForEach(t => t.ParentId = NHTtDeNghiThanhToan.Id);
                ctx.SaveChanges();
            }
        }

        public void RemoveParentIdOfChildren(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var children = ctx.NhTtThanhToan.Where(t => id.Equals(t.ParentId)).ToList();
                foreach (var item in children)
                {
                    item.ParentId = null;
                }
                ctx.SaveChanges();
            }
        }
    }
}
