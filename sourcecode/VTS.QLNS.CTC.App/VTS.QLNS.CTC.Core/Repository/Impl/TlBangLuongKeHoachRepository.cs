using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Query.Shared;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlBangLuongKeHoachRepository : Repository<TlBangLuongKeHoach>, ITlBangLuongKeHoachRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlBangLuongKeHoachRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int DeleteByParentId(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.TlBangLuongKeHoaches.RemoveRange(ctx.TlBangLuongKeHoaches.Where(x => x.Parent == id));
                return ctx.SaveChanges();
            }
        }

        public IEnumerable<TlDmCanBoKeHoach> FindCanBo(decimal? thang, decimal? nam, string maDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCanBoKeHoaches.Include(x => x.TlDmCapBac).Where(x => x.Thang == thang && x.Nam == nam && x.Parent == maDonVi && x.IsDelete == true).ToList();
            }
        }

        public IEnumerable<TlDmThueThuNhapCaNhan> FindThue()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmThueThuNhapCaNhans.Where(x => x.LoaiThue.StartsWith("B")).ToList();
            }
        }

        public DataTable GetDataBangLuong(string maDonVi, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {

                DataTable dataTable = new DataTable();

                var connection = ctx.Database.GetDbConnection();
                DbProviderFactory dbProviderFactory = DbProviderFactories.GetFactory(connection);
                var cmd = connection.CreateCommand();
                cmd.CommandText = "dbo.sp_tl_baocao_luong_nam_ke_hoach";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@spMaDonVi", maDonVi));
                cmd.Parameters.Add(new SqlParameter("@spNam", nam));

                using (DbDataAdapter adapter = dbProviderFactory.CreateDataAdapter())
                {
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dataTable);
                }

                return dataTable;
            }
        }

        public IEnumerable<TlBangLuongKeHoachExportQuery> ExportQuyLuongCanCu(int iNamLamViec, List<string> lstMaPhuCap, string lstIdChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter PiNamLamViec = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter PLuongChinh = new SqlParameter("@LuongChinh", lstMaPhuCap[0]);
                SqlParameter PPhuCapCV = new SqlParameter("@PhuCapCV", lstMaPhuCap[1]);
                SqlParameter PPhuCapTNN = new SqlParameter("@PhuCapTNN", lstMaPhuCap[2]);
                SqlParameter PPhuCapTNVK = new SqlParameter("@PhuCapTNVK", lstMaPhuCap[3]);
                SqlParameter PPhuCapHSBL = new SqlParameter("@PhuCapHSBL", lstMaPhuCap[4]);
                SqlParameter PLstIdChungTu = new SqlParameter("@lstIdChungTu", lstIdChungTu);
                return ctx.FromSqlRaw<TlBangLuongKeHoachExportQuery>("EXECUTE dbo.sp_tl_export_quy_luong_can_cu "
                    + "@NamLamViec, @LuongChinh, @PhuCapCV, @PhuCapTNN, @PhuCapTNVK, @PhuCapHSBL, @lstIdChungTu",
                    PiNamLamViec, PLuongChinh, PPhuCapCV, PPhuCapTNN, PPhuCapTNVK, PPhuCapHSBL, PLstIdChungTu).ToList();
            }
        }
    }
}
