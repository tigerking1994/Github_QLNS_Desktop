using log4net;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
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
    public class TlQuanLyThuNopBhxhRepository : Repository<TlQuanLyThuNopBhxh>, ITlQuanLyThuNopBhxhRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        private readonly ILog _logger;


        public TlQuanLyThuNopBhxhRepository(ApplicationDbContextFactory contextFactory, ILog log) : base(contextFactory)
        {
            _contextFactory = contextFactory;
            _logger = log;

        }

        public int DeleteDetail(string siIdDetail)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_tl_delete_QlThuNop @idDsbangLuong";
                var parameters = new[]
                {
                    new SqlParameter("@siIdDetail", siIdDetail)
                };
                return ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public int DeleteModelAndDetail(int thang, int nam, string maDonVi, string maCachTl, Guid? idTongHop = null, bool isTongHop = false)
        {
            try
            {
                using (var ctx = _contextFactory.CreateDbContext())
                {
                    string sql = "EXECUTE sp_tl_delete_tl_quan_ly_thu_nop_bhxh @thang, @nam, @maDonVi, @maCachTl,@IsAggregate, @IdAggregate";
                    var parameters = new[]
                    {
                    new SqlParameter("@thang", thang),
                    new SqlParameter("@nam", nam),
                    new SqlParameter("@maDonVi", maDonVi),
                    new SqlParameter("@maCachTl", maCachTl),
                    new SqlParameter("@IsAggregate", isTongHop),
                    new SqlParameter("@IdAggregate", idTongHop?? Guid.Empty)
                };
                    return ctx.Database.ExecuteSqlCommand(sql, parameters);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }

        }

        public TlQuanLyThuNopBhxh FindByCondition(string maCachTinhLuong, string maDonVi, int thang, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlQuanLyThuNopBhxhs.Where(x => x.SMaCachTl == maCachTinhLuong && x.IIdMaDonVi == maDonVi && x.IThang == thang && x.INam == nam).FirstOrDefault();
            }
        }

        public TlQuanLyThuNopBhxh FindByConditionStatus(string maCachTinhLuong, string maDonVi, int thang, int nam, bool status)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlQuanLyThuNopBhxhs.FirstOrDefault(x => x.SMaCachTl == maCachTinhLuong && x.IIdMaDonVi == maDonVi && x.IThang == thang && x.INam == nam && x.Status == status);
            }
        }

        public IEnumerable<TlQuanLyThuNopBhxh> FindByMaCachTinhluong(string maCachTinhLuong)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlQuanLyThuNopBhxhs.Where(x => x.SMaCachTl == maCachTinhLuong).ToList();
            }
        }

        public IEnumerable<TlQuanLyThuNopBhxh> FindByMonth(int month)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlQuanLyThuNopBhxhs.Where(x => x.IThang == month).ToList();
            }
        }

        public IEnumerable<TlQuanLyThuNopBhxhQuery> FindByThangByNam(int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var data = from tlquanLy in ctx.TlQuanLyThuNopBhxhs.Where(x => x.INam == nam && x.SMaCachTl.Equals(CachTinhLuong.CACH0))
                           join tlDonVi in ctx.TlDmDonVis on tlquanLy.IIdMaDonVi equals tlDonVi.MaDonVi into tblDv
                           from subData in tblDv.DefaultIfEmpty()
                           orderby tlquanLy.IIdMaDonVi
                           select new TlQuanLyThuNopBhxhQuery
                           {
                               Id = tlquanLy.Id,
                               STen = tlquanLy.STen,
                               ILoai = tlquanLy.ILoai,
                               DTuNgay = tlquanLy.DTuNgay,
                               DDenNgay = tlquanLy.DDenNgay,
                               SMaPban = tlquanLy.SMaPban,
                               IIdMaDonVi = tlquanLy.IIdMaDonVi,
                               IThang = tlquanLy.IThang,
                               INam = tlquanLy.INam,
                               ISoTt = tlquanLy.ISoTt,
                               SMaCachTl = tlquanLy.SMaCachTl,
                               Status = tlquanLy.Status,
                               DNgayTao = tlquanLy.DNgayTao,
                               SNguoiTao = tlquanLy.SNguoiTao,
                               SMoTa = tlquanLy.SMoTa,
                               BIsKhoa = tlquanLy.BIsKhoa,
                               IsTongHop = tlquanLy.IsTongHop,
                               STongHop = tlquanLy.STongHop,
                               STenDonVi = subData == null ? tlquanLy.IIdMaDonVi : $"{subData.MaDonVi}-{subData.TenDonVi}",
                           };


                return data.ToList();
            }
        }

        public void UpdateDetailBhxhTheoCapBac(int iThang, int iNam, List<string> lstMaDonVi)
        {
            try
            {
                using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
                {
                    var executeQuery = "EXECUTE dbo.sp_tl_update_QLThuNop_bhxh_hsqcs @iThang, @inam, @iIdMaDonVi";
                    DataTable dt = DBExtension.ConvertDataToStringTable(lstMaDonVi);
                    SqlParameter dtDetailParam = new SqlParameter("iIdMaDonVi", SqlDbType.Structured)
                    {
                        TypeName = "t_tbl_string",
                        Value = dt
                    };
                    var parameters = new[]
                    {
                    new SqlParameter("@iThang",iThang),
                    new SqlParameter("@inam",iNam),
                    dtDetailParam
                };
                    ctx.Database.ExecuteSqlCommand(executeQuery, parameters);
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }
    }
}
