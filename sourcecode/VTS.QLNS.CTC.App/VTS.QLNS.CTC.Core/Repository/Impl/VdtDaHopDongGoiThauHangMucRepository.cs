using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using Dapper;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtDaHopDongGoiThauHangMucRepository : Repository<VdtDaHopDongGoiThauHangMuc>, IVdtDaHopDongGoiThauHangMucRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtDaHopDongGoiThauHangMucRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<HopDongHangMucQuery> GetPhuLucHangMucByGoiThau(Guid iIdGoiThauId, Guid iIdHopDongId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<HopDongHangMucQuery>("EXECUTE dbo.sp_vdt_get_hangmuc_goithau_hopdong @iIdGoiThau, @iIdHopDong",
                    new SqlParameter("@iIdGoiThau", iIdGoiThauId),
                    new SqlParameter("@iIdHopDong", iIdHopDongId)).ToList();
            }
        }

        public IEnumerable<HopDongHangMucQuery> GetPhuLucChiPhiByGoiThau(Guid iIdGoiThauId, Guid iIdHopDongId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<HopDongHangMucQuery>("EXECUTE dbo.sp_vdt_get_chiphi_goithau_hopdong_by_goithau @iIdGoiThau, @iIdHopDong",
                    new SqlParameter("@iIdGoiThau", iIdGoiThauId),
                    new SqlParameter("@iIdHopDong", iIdHopDongId)).ToList();
            }
        }

        public IEnumerable<HopDongHangMucQuery> GetGoiThauChiPhiByHopDong(Guid iIdHopDongId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<HopDongHangMucQuery>("EXECUTE dbo.sp_vdt_get_chiphi_goithau_hopdong_by_hopdong @iIdHopDong",
                    new SqlParameter("@iIdHopDong", iIdHopDongId)).ToList();
            }
        }

        public IEnumerable<HopDongHangMucQuery> GetAllHangMucByGoiThau(Guid iIdGoiThauId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<HopDongHangMucQuery>("EXECUTE dbo.sp_vdt_get_hangmuc_goithau_hopdong_by_goithau @iIdGoiThau",
                    new SqlParameter("@iIdGoiThau", iIdGoiThauId)).ToList();
            }
        }

        public IEnumerable<HopDongHangMucQuery> GetAllHangMucByListGoiThauHopDongAdd(Guid iIdHopDongId, List<Guid> listGoiThauId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var executeQuery = "EXECUTE dbo.sp_vdt_get_hangmuc_goithau_hopdong_by_goithaus_hopdong_add @iIdHopDongId, @lstId";
                DataTable dt = DBExtension.ConvertDataToGuidTable(listGoiThauId);
                var parameters = new[]
                {
                    new SqlParameter("@iIdHopDongId", iIdHopDongId),
                    new SqlParameter("@lstId", dt.AsTableValuedParameter("t_tbl_uniqueidentifier"))
                };
                return ctx.FromSqlRaw<HopDongHangMucQuery>(executeQuery, parameters).ToList().OrderBy(t => t.MaOrDer);
            }
        }

        public IEnumerable<HopDongHangMucQuery> GetAllHangMucByListGoiThau(List<Guid> listGoiThauId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var executeQuery = "EXECUTE dbo.sp_vdt_get_hangmuc_goithau_hopdong_by_goithaus @lstId";
                DataTable dt = DBExtension.ConvertDataToGuidTable(listGoiThauId);
                var parameters = new[]
                {
                    new SqlParameter("@lstId", dt.AsTableValuedParameter("t_tbl_uniqueidentifier"))
                };
                return ctx.FromSqlRaw<HopDongHangMucQuery>(executeQuery, parameters).ToList();
            }
        }

        public IEnumerable<HopDongHangMucQuery> GetAllHangMucByHopDong(Guid iIdHopDongId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<HopDongHangMucQuery>("EXECUTE dbo.sp_vdt_get_hangmuc_goithau_hopdong_by_hopdong  @iIdHopDong",
                    new SqlParameter("@iIdHopDong", iIdHopDongId)).ToList();
            }
        }

        public IEnumerable<HopDongHangMucQuery> GetAllHangMucByHopDongDieuChinh(Guid iIdHopDongId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<HopDongHangMucQuery>("EXECUTE dbo.sp_vdt_get_hangmuc_goithau_hopdong_by_hopdong_dieuchinh  @iIdHopDong",
                    new SqlParameter("@iIdHopDong", iIdHopDongId)).ToList();
            }
        }

        public IEnumerable<VdtDmLoaiHopDong> GetAllLoaiHopDong()
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtDmLoaiHopDong>("SELECT * FROM VDT_DM_LoaiHopDong").ToList();
            }
        }
    }
}
