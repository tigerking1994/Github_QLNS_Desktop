using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Extensions;
using Dapper;
using VTS.QLNS.CTC.Core.Domain.Query;
using System.Collections.Generic;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhThTongHopRepository : Repository<NHTHTongHop>, INhThTongHopRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhThTongHopRepository(ApplicationDbContextFactory context)
            : base(context)
        {
            _contextFactory = context;
        }
        public void InsertNHTongHop_Tang(string sLoai, int iTypeExecute, Guid iIdQuyetDinh, Guid iIDQuyetDinhOld)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                ctx.Database.ExecuteSqlCommand("EXECUTE sp_insert_nh_tonghop_tang @sLoai, @iTypeExecute, @uIdQuyetDinh, @iIDQuyetDinhOld",
                   new SqlParameter("@sLoai", sLoai),
                   new SqlParameter("@iTypeExecute", iTypeExecute),
                   new SqlParameter("@uIdQuyetDinh", iIdQuyetDinh),
                   new SqlParameter("@iIDQuyetDinhOld", iIDQuyetDinhOld));
            }
        }
       
        public void InsertNHTongHop_New(string sLoai, int iTypeExecute, Guid iIdQuyetDinh, Guid iIDQuyetDinhOld)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                ctx.Database.ExecuteSqlCommand("EXECUTE sp_insert_NH_TH_NguonNgoaiHoi_tang @sLoai, @iTypeExecute, @uIdQuyetDinh, @iIDQuyetDinhOld",
                   new SqlParameter("@sLoai", sLoai),
                   new SqlParameter("@iTypeExecute", iTypeExecute),
                   new SqlParameter("@uIdQuyetDinh", iIdQuyetDinh),
                   new SqlParameter("@iIDQuyetDinhOld", iIDQuyetDinhOld));
            }
        }

        public void InsertNHTongHop_Giam(string sLoai, int iTypeExecute, Guid iIdQuyetDinh, Guid iIDQuyetDinhOld)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                ctx.Database.ExecuteSqlCommand("EXECUTE sp_insert_nh_tonghop_giam @sLoai, @iTypeExecute, @uIdQuyetDinh, @iIDQuyetDinhOld",
                   new SqlParameter("@sLoai", sLoai),
                   new SqlParameter("@iTypeExecute", iTypeExecute),
                   new SqlParameter("@uIdQuyetDinh", iIdQuyetDinh),
                   new SqlParameter("@iIDQuyetDinhOld", iIDQuyetDinhOld));
            }
        }
        public void DeleteNHTongHop_Giam(string sLoai, Guid iIdQuyetDinh)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                ctx.Database.ExecuteSqlCommand("EXECUTE sp_delete_nh_tonghop_giam @sLoai, @uIdQuyetDinh",
                   new SqlParameter("@sLoai", sLoai),
                   new SqlParameter("@uIdQuyetDinh", iIdQuyetDinh));
            }
        }
        public void InsertNHTongHop(Guid iIDChungTu, string sLoai, List<NHTHTongHopQuery> lstData)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var executeQuery = "EXECUTE sp_insert_nhtonghop @iIDChungTu, @sLoai, @data";
                var data = DBExtension.ConvertDataToTableDefined("t_tbl_nh_tonghop", lstData);
                var parameters = new[]
                {
                    new SqlParameter("iIDChungTu", iIDChungTu),
                    new SqlParameter("sLoai", sLoai),
                    new SqlParameter("@data", data.AsTableValuedParameter("t_tbl_nh_tonghop"))
                };
                ctx.FromSqlRaw<NHTHTongHopQuery>(executeQuery, parameters);
            }
        }
    }
}
