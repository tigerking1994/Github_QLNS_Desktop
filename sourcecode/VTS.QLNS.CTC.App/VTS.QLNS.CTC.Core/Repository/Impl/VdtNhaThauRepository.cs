using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtNhaThauRepository : Repository<VdtDmNhaThau>, IVdtNhaThauRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtNhaThauRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<string> GetListSTKByNhaThau(Guid iIdNhaThau)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "select * from VDT_DM_NhaThau where Id = @iIdNhaThau";
                var parameters = new[]
                {
                    new SqlParameter("@iIdNhaThau", iIdNhaThau)
                };
                VdtDmNhaThau nhaThau = ctx.Set<VdtDmNhaThau>().FromSql(sql, parameters).ToList().FirstOrDefault();
                List<string> listSTK = new List<string>();
                listSTK.Add(nhaThau.SSoTaiKhoan);
                listSTK.Add(nhaThau.SSoTaiKhoan2);
                listSTK.Add(nhaThau.SSoTaiKhoan3);
                return listSTK;
            }
        }

        public IEnumerable<VdtDmNhaThau> GetNhaThauByHopDong(Guid iIdHopDongId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_get_nhathau_by_hopdong @iIdHopDongId";
                var parameters = new[]
                {
                    new SqlParameter("@iIdHopDongId", iIdHopDongId)
                };
                return ctx.Set<VdtDmNhaThau>().FromSql(sql, parameters).ToList();
            }
        }
    }
}