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
    public class TlDmPhuCapRepository : Repository<Domain.TlDmPhuCap>, ITlDmPhuCapRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDmPhuCapRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<Domain.TlDmPhuCap> FindByCondition()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                StringBuilder query = new StringBuilder("SELECT DISTINCT * FROM TL_DM_PhuCap ");
                query.Append("WHERE Chon = 1 AND ");
                query.Append("Is_Formula = 0 AND ");
                query.Append("Is_Readonly = 0 AND ");
                query.Append("Parent IN (SELECT Ma_PhuCap FROM TL_DM_PhuCap WHERE Parent = '' AND Chon = 1) ");
                query.Append("ORDER BY Parent ");
                return ctx.TlDmPhuCaps.FromSql(query.ToString()).ToList();
            }
        }

        public IEnumerable<TlDmPhuCap> FindByHeThong()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                StringBuilder query = new StringBuilder("SELECT DISTINCT * FROM TL_DM_PhuCap ");
                query.Append("Where Ma_phuCap like 'HETHONG' or Parent like 'HETHONG' ");
                return ctx.TlDmPhuCaps.FromSql(query.ToString()).ToList();
            }
        }

        public IEnumerable<TlDmPhuCap> FindAllPhuCapHeThong()
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_get_dm_phucap_hethong"; 

                return ctx.FromSqlRaw<TlDmPhuCap>(sql);
            }
        }

        public IEnumerable<TlDmPhuCap> FindByBHXHPhuCapNotIn(int namLamViec, string pcLoai, string pcBHXH, string pcChosen)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var mlpc = ctx.TlDmPhuCaps.Where(x => (x.MaPhuCap != "HETHONG" || x.Parent != "HETHONG") && !string.IsNullOrEmpty(x.Parent) && x.MaPhuCap.EndsWith("_TT")).ToList();
                var bhxhs = ctx.BhDmMucLucNganSachs.Where(t => t.INamLamViec == namLamViec && t.SXauNoiMa.StartsWith("902")).ToList();
                var pcList = pcChosen.Split(',');
                //var pcLC = "";
                //var pcPCCV = "";
                //var pcPCTN = "";
                //var pcPCTNVK = "";
                //if (pcLoai == "TenSLuongChinh")
                //{
                //    pcPCCV = string.Join(";", bhxhs.Where(t => !string.IsNullOrEmpty(t.sPCCV)).Select(t => t.sPCCV));
                //    pcPCTN = string.Join(";", bhxhs.Where(t => !string.IsNullOrEmpty(t.sPCTN)).Select(t => t.sPCTN));
                //    pcPCTNVK = string.Join(";", bhxhs.Where(t => !string.IsNullOrEmpty(t.sPCTNVK)).Select(t => t.sPCTNVK));
                //}
                //else if (pcLoai == "TenSPCCV")
                //{
                //    pcLC = string.Join(";", bhxhs.Where(t => !string.IsNullOrEmpty(t.sLuongChinh)).Select(t => t.sLuongChinh));
                //    pcPCTN = string.Join(";", bhxhs.Where(t => !string.IsNullOrEmpty(t.sPCTN)).Select(t => t.sPCTN));
                //    pcPCTNVK = string.Join(";", bhxhs.Where(t => !string.IsNullOrEmpty(t.sPCTNVK)).Select(t => t.sPCTNVK));
                //}
                //else if (pcLoai == "TenSPCTN")
                //{
                //    pcLC = string.Join(";", bhxhs.Where(t => !string.IsNullOrEmpty(t.sLuongChinh)).Select(t => t.sLuongChinh));
                //    pcPCCV = string.Join(";", bhxhs.Where(t => !string.IsNullOrEmpty(t.sPCCV)).Select(t => t.sPCCV));
                //    pcPCTNVK = string.Join(";", bhxhs.Where(t => !string.IsNullOrEmpty(t.sPCTNVK)).Select(t => t.sPCTNVK));
                //}
                //else if (pcLoai == "TenSPCTNVK")
                //{
                //    pcLC = string.Join(";", bhxhs.Where(t => !string.IsNullOrEmpty(t.sLuongChinh)).Select(t => t.sLuongChinh));
                //    pcPCCV = string.Join(";", bhxhs.Where(t => !string.IsNullOrEmpty(t.sPCCV)).Select(t => t.sPCCV));
                //    pcPCTN = string.Join(";", bhxhs.Where(t => !string.IsNullOrEmpty(t.sPCTN)).Select(t => t.sPCTN));
                //}
                
                var sql = from pc in mlpc
                          //where (!pcList.Contains(pc.MaPhuCap) && !pcLC.Contains(pc.MaPhuCap) && !pcPCCV.Contains(pc.MaPhuCap) && !pcPCTN.Contains(pc.MaPhuCap) && !pcPCTNVK.Contains(pc.MaPhuCap))
                          where !pcList.Contains(pc.MaPhuCap)
                          select new { pc = pc };
                var rs = sql.ToList().Select(t =>
                {
                    var pc = t.pc;
                    return pc;
                }).ToList();
                return rs;
            }
        }

        public TlDmPhuCap FindByMaPhuCap(string maPhuCap)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmPhuCaps.Where(X => X.MaPhuCap == maPhuCap).FirstOrDefault();
            }
        }

        public IEnumerable<TlDmPhuCap> GetDmPhuCapInDcTapTheCanBo()
        {
            List<Guid> lstId = new List<Guid>();
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var data = ctx.FromSqlRaw<TlDmPhuCap>("exec sp_luong_get_dmphucap_dctapthecanbo").ToList();
                if(data != null)
                {
                    lstId = data.Select(n => n.Id).ToList();
                }
            }
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmPhuCaps.Where(n => lstId.Contains(n.Id)).ToList();
            }
            return new List<TlDmPhuCap>();
        }

        public IEnumerable<TlDmPhuCap> FindHasDataBangLuong(int nam, int thang, string maCachTl)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                StringBuilder query = new StringBuilder("");
                query.Append("SELECT * FROM TL_DM_PhuCap ");
                query.Append("WHERE ");
                query.Append("  iDinhDang = 0 ");
                query.Append("  AND Ma_PhuCap IN ( ");
                query.Append("      SELECT DISTINCT bangLuong.MA_PHUCAP ");
                query.Append("      FROM TL_BangLuong_Thang bangLuong ");
                query.Append("      JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong ");
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
                return ctx.TlDmPhuCaps.FromSql(query.ToString(), parameters).ToList();
            }
        }

        public IEnumerable<TlDmPhuCap> FindByIdThuNopBhxh(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var sql = @"SELECT * FROM TL_DM_PhuCap " +
                          "WHERE Ma_PhuCap IN (SELECT sMa_PhuCap FROM TL_QuanLyThuNop_BHXH_ChiTiet WHERE iId_ParentId = @idParent)";
                var param = new[]
                {
                   new SqlParameter("@idParent", id),
                };
                return ctx.TlDmPhuCaps.FromSql(sql, param).ToList();

            }
        }

        public string FindByBHXHPhuCapIn(int namLamViec, string pcLoai, string pcBhxh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var bhxhs = ctx.BhDmMucLucNganSachs.Where(t => t.INamLamViec == namLamViec && t.SXauNoiMa == pcBhxh).FirstOrDefault();

                switch (pcLoai)
                {
                    case "TenSLuongChinh":
                        return bhxhs.SLuongChinh ?? string.Empty;
                    case "TenSPCCV":
                        return bhxhs.SPCCV ?? string.Empty;
                    case "TenSPCTN":
                        return bhxhs.SPCTN ?? string.Empty;
                    case "TenSPCTNVK":
                        return bhxhs.SPCTNVK ?? string.Empty;
                    default:
                        return string.Empty;
                }
            }
        }

        public void UpdateCanBoPhuCapWhenChangePhuCap(int iThang, int iNam, List<Guid> lstIdPhuCap, bool bIsDelete)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeQuery = "EXECUTE sp_tl_update_dmphucap_canbo @nam, @thang, @bIsDelete, @phucapIds";

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
                return ctx.TlDmPhuCaps.Where(n => n.Id != iId && n.MaPhuCap.Equals(maPhuCap)).Any();
            }
        }

        public IEnumerable<TlPhuCapQuery> FindAllPhuCapVaCheDoBHXH()
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_find_all_phu_cap_va_che_do_bhxh";

                return ctx.FromSqlRaw<TlPhuCapQuery>(sql);
            }
        }
    }
}
