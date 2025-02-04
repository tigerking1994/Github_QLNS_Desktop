using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDmPhuCapNq104Repository : Repository<Domain.TlDmPhuCapNq104>, ITlDmPhuCapNq104Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDmPhuCapNq104Repository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<Domain.TlDmPhuCapNq104> FindByCondition()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                StringBuilder query = new StringBuilder("SELECT DISTINCT * FROM TL_DM_PhuCap_Nq104 ");
                query.Append("WHERE Chon = 1 AND ");
                query.Append("Is_Formula = 0 AND ");
                query.Append("Is_Readonly = 0 AND ");
                query.Append("Parent IN (SELECT Ma_PhuCap FROM TL_DM_PhuCap_Nq104 WHERE Parent = '' AND Chon = 1) ");
                query.Append("ORDER BY Parent ");
                return ctx.TlDmPhuCapsNq104.FromSql(query.ToString()).ToList();
            }
        }

        public IEnumerable<TlDmPhuCapNq104> FindByHeThong()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                StringBuilder query = new StringBuilder("SELECT DISTINCT * FROM TL_DM_PhuCap_Nq104 ");
                query.Append("Where Ma_phuCap like 'HETHONG' or Parent like 'HETHONG' ");
                return ctx.TlDmPhuCapsNq104.FromSql(query.ToString()).ToList();
            }
        }

        public IEnumerable<TlDmPhuCapNq104> FindAllPhuCapHeThong()
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_get_dm_phucap_hethong_nq104"; 

                return ctx.FromSqlRaw<TlDmPhuCapNq104>(sql);
            }
        }

        public TlDmPhuCapNq104 FindByMaPhuCap(string maPhuCap)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmPhuCapsNq104.Where(X => X.MaPhuCap == maPhuCap).FirstOrDefault();
            }
        }

        public IEnumerable<TlDmPhuCapNq104> GetDmPhuCapInDcTapTheCanBo()
        {
            List<Guid> lstId = new List<Guid>();
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var data = ctx.FromSqlRaw<TlDmPhuCapNq104>("exec sp_luong_get_dmphucap_dctapthecanbo_nq104").ToList();
                if(data != null)
                {
                    lstId = data.Select(n => n.Id).ToList();
                }
            }
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmPhuCapsNq104.Where(n => lstId.Contains(n.Id)).ToList();
            }
            return new List<TlDmPhuCapNq104>();
        }

        public IEnumerable<TlDmPhuCapNq104> FindHasDataBangLuong(int nam, int thang, string maCachTl)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                StringBuilder query = new StringBuilder("");
                query.Append("SELECT * FROM TL_DM_PhuCap_Nq104 ");
                query.Append("WHERE ");
                query.Append("  iDinhDang = 0 ");
                query.Append("  AND Ma_PhuCap IN ( ");
                query.Append("      SELECT DISTINCT bangLuong.MA_PHU_CAP ");
                query.Append("      FROM TL_BangLuong_Thang_Bridge_Nq104 bangLuong ");
                query.Append("      JOIN TL_DS_CapNhap_BangLuong_Nq104 dsCapNhapBangLuong ");
                query.Append("          ON bangLuong.parent = dsCapNhapBangLuong.Id ");
                query.Append("      WHERE ");
                query.Append("          dsCapNhapBangLuong.Ma_CachTL=@MaCachTl ");
                query.Append("          AND dsCapNhapBangLuong.Thang=@Thang ");
                query.Append("          AND dsCapNhapBangLuong.Nam = @Nam ");
                query.Append("          AND dsCapNhapBangLuong.Status = 1 ");
                query.Append("  )");
                var parameters = new[]
                {
                    new SqlParameter("@MaCachTl", maCachTl),
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam),
                };
                return ctx.TlDmPhuCapsNq104.FromSql(query.ToString(), parameters).ToList();
            }
        }

        public IEnumerable<TlDmPhuCapNq104> FindByIdThuNopBhxh(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var sql = @"SELECT * FROM TL_DM_PhuCap_Nq104 " +
                          "WHERE Ma_PhuCap IN (SELECT sMa_PhuCap FROM TL_QuanLyThuNop_BHXH_ChiTiet WHERE iId_ParentId = @idParent)";
                var param = new[]
                {
                   new SqlParameter("@idParent", id),
                };
                return ctx.TlDmPhuCapsNq104.FromSql(sql, param).ToList();

            }
        }

        public void UpdateCanBoPhuCapWhenChangePhuCap(int iThang, int iNam, List<Guid> lstIdPhuCap, bool bIsDelete)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeQuery = "EXECUTE sp_tl_update_dmphucap_canbo_nq104 @nam, @thang, @bIsDelete, @phucapIds";

                DataTable dt = DBExtension.ConvertDataToGuidTable(lstIdPhuCap);
                SqlParameter dtDetailParam = new SqlParameter("phucapIds", SqlDbType.Structured);
                dtDetailParam.TypeName = "t_tbl_uniqueidentifier";
                dtDetailParam.Value = dt;
                var parameters = new[]
                {
                    new SqlParameter("nam", iNam),
                    new SqlParameter("thang", iThang),
                    new SqlParameter("bIsDelete", bIsDelete),
                    dtDetailParam
                };

                ctx.Database.ExecuteSqlCommand(executeQuery, parameters);
            }
        }

        public bool CheckPhuCapExist(string maPhuCap, Guid iId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmPhuCapsNq104.Where(n => n.Id != iId && n.MaPhuCap.Equals(maPhuCap)).Any();
            }
        }

        public IEnumerable<TlPhuCapNq104Query> FindAllPhuCapVaCheDoBHXH()
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_find_all_phu_cap_va_che_do_bhxh_nq104";

                return ctx.FromSqlRaw<TlPhuCapNq104Query>(sql);
            }
        }
    }
}
