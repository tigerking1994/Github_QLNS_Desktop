using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using Dapper;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhDaGoiThauRepository : Repository<NhDaGoiThau>, INhDaGoiThauRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDaGoiThauRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhDaGoiThau> FindByIidKhlcNhaThau(Guid iIdKhlcNhaThau)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NhDaGoiThaus.Where(n => n.IIdKhlcnhaThau == iIdKhlcNhaThau).ToList();
            }
        }
        public IEnumerable<NhDaGoiThau> FindByIidKhlcNhaThauID(Guid iIdKhlcNhaThau)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NhDaGoiThaus.Where(n => n.Id== iIdKhlcNhaThau).ToList();
            }
        }

        public void DeleteByIidKhlcNhaThau(Guid iIdKhlcNhaThau)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var lstData = ctx.NhDaGoiThaus.Where(n => n.IIdKhlcnhaThau == iIdKhlcNhaThau);
                if (lstData == null) return;
                ctx.RemoveRange(lstData);
                ctx.SaveChanges();
            }
        }

        public IEnumerable<NhDaGoiThauQuery> GetAll()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_nh_thongtin_goithau_index";
                return ctx.FromSqlRaw<NhDaGoiThauQuery>(executeSql).ToList();
            }
        }

        public IEnumerable<NhDaGoiThauTrongNuocQuery> GetAllGoiThauTrongNuoc(int ILoai, int IThuocMenu)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_nh_goithau_trongnuoc_index @ILoai, @IThuocMenu";
                var parameters = new[]
                {
                    new SqlParameter("ILoai", ILoai),
                    new SqlParameter("IThuocMenu", IThuocMenu)
                };
                return ctx.FromSqlRaw<NhDaGoiThauTrongNuocQuery>(executeSql, parameters).ToList();
            }
        }
         public IEnumerable<NhDaGoiThauTrongNuocQuery> GetAllGoiThauTrongNuocByILoai(int ILoai, int IThuocMenu)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_nh_goithau_trongnuoc_index2 @ILoai, @IThuocMenu";
                var parameters = new[]
                {
                    new SqlParameter("ILoai", ILoai),
                    new SqlParameter("IThuocMenu", IThuocMenu)
                };
                return ctx.FromSqlRaw<NhDaGoiThauTrongNuocQuery>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<NhDaThongTinNhaThauHopDongQuery> GetThongTinHopDongByIdGoiThau(Guid idGoiThau)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter idGoiThauParam = new SqlParameter("@idGoiThau", idGoiThau);
                return ctx.FromSqlRaw<NhDaThongTinNhaThauHopDongQuery>("EXECUTE dbo.sp_nh_gethangmuc_hopdongtrongnuoc_byidgoithau @idGoiThau", idGoiThauParam).ToList();
            }
        }

        public IEnumerable<NhDaGoiThauDetailQuery> FindGoiThauDetail()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<NhDaGoiThauDetailQuery>("EXECUTE dbo.sp_nh_goithau_detail").ToList();
            }
        }

        public void DeleteGoiThauDetail(List<Guid> iIdGoiThaus)
        {
            if(iIdGoiThaus.Count > 0)
            {
                using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
                {
                    string executeQuery = "EXECUTE sp_nh_khlcnt_delete_goithau_detail @iIdGoiThaus";
                    DataTable dt = DBExtension.ConvertDataToGuidTable(iIdGoiThaus);
                    var parameters = new[]
                    {
                    new SqlParameter("@iIdGoiThaus", dt.AsTableValuedParameter("t_tbl_uniqueidentifier"))
                };
                    ctx.FromSqlRaw<Guid>(executeQuery, parameters);
                }
            }
        }
        public void DeleteListGoiThau(List<Guid> iIdGoiThaus)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeQuery = "EXECUTE sp_nh_khlcnt_delete_goithau @iIdGoiThaus";
                DataTable dt = DBExtension.ConvertDataToGuidTable(iIdGoiThaus);
                var parameters = new[]
                {
                    new SqlParameter("@iIdGoiThaus", dt.AsTableValuedParameter("t_tbl_uniqueidentifier"))
                };
                ctx.FromSqlRaw<Guid>(executeQuery, parameters);
            }
        }
    }
}
