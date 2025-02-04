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

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtKtKhoiTaoDuLieuChiTietThanhToanRepository:Repository<VdtKtKhoiTaoDuLieuChiTietThanhToan>, IVdtKtKhoiTaoDuLieuChiTietThanhToanRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtKtKhoiTaoDuLieuChiTietThanhToanRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void DeleteByKhoiTaoDuLieuId(Guid iId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
				string sql = "";
				sql += "DELETE chld FROM VDT_KT_KhoiTao_DuLieu as tbl ";
                sql += "INNER JOIN VDT_KT_KhoiTao_DuLieu_ChiTiet as dt on tbl.Id = dt.iID_KhoiTaoDuLieuID "; 
                sql += "INNER JOIN VDT_KT_KhoiTao_DuLieu_ChiTiet_ThanhToan as chld on dt.Id = chld.iId_KhoiTaoDuLieuChiTietId ";
                sql += "WHERE tbl.Id = @Id";
				var parameters = new[]
                {
                    new SqlParameter("@Id", iId)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);

                //insert bản ghi revert, và đổi bIsLog bản ghi trong bảng Tổng hơp
                ctx.Database.ExecuteSqlCommand("EXECUTE sp_delete_tonghopkhoitaothongtinduan @iId",
                  new SqlParameter("@iId", iId));
            }
        }

        public IEnumerable<VdtKtKhoiTaoDuLieuChiTietThanhToanQuery> GetDetailByKTDLId(Guid iId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
				string sql = "EXECUTE dbo.sp_kt_khoitaodulieuchitietthanhtoan_byktdl_id @iId";
				var parameters = new[]
                {
                    new SqlParameter("@iId", iId)
                };
                return ctx.FromSqlRaw<VdtKtKhoiTaoDuLieuChiTietThanhToanQuery>(sql, parameters).ToList();
            }
        }
    }
}
