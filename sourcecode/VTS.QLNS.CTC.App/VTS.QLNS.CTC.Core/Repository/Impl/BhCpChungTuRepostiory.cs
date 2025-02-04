using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class BhCpChungTuRepostiory : Repository<BhCpChungTu>, IBhCpChungTuRepostiory
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public BhCpChungTuRepostiory(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public List<DonVi> FindByDonViForNamLamViec(int yearOfWork, int iQuy, Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", yearOfWork);
                SqlParameter searchQuyParam = new SqlParameter("@Quy", iQuy);
                SqlParameter searchLoaiChiParam = new SqlParameter("@LoaiChi", id);
                return ctx.FromSqlRaw<DonVi>("EXECUTE sp_cp_rpt_get_donvi_lns_BH @NamLamViec, @Quy, @LoaiChi", iNamLamViecParam, searchQuyParam, searchLoaiChiParam).ToList();
            }
        }

        public IEnumerable<BhCpChungTuQuery> FindIndex(int iNamChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_cp_chungtu_index_bh @YearOfWork";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", iNamChungTu)
                };
                return ctx.FromSqlRaw<BhCpChungTuQuery>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<ReportBHChungTuCapPhatKeHoachQuery> GetDataReportCapPhatKeHoach(string lstMaDonVi, int iQuy, int yearOfWork, string principal, int donViTinh, Guid idLoaiCap, string sMaLoaiChi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_cp_rpt_kehoach_bh @IdMaDonVi, @IQuy, @NamLamViec, @UserName, @Donvitinh,@iIdLoaiCap,@MaLoaiChi";
                var parameters = new[]
                {
                    new SqlParameter("@IdMaDonVi", lstMaDonVi),
                    new SqlParameter("@IQuy", iQuy),
                    new SqlParameter("@NamLamViec", yearOfWork),
                    new SqlParameter("@UserName", principal),
                    new SqlParameter("@Donvitinh", donViTinh),
                    new SqlParameter("@iIdLoaiCap", idLoaiCap),
                    new SqlParameter("@MaLoaiChi",sMaLoaiChi)
                    
                };
                return ctx.FromSqlRaw<ReportBHChungTuCapPhatKeHoachQuery>(executeSql, parameters).ToList();
            }
        }


        public IEnumerable<ReportBHChungTuCapPhatKeHoachQuery> GetDataReportCapPhatKeHoachCsskHssvNld(int yearOfWork, Guid iDLoaiCap, string lstDonVi, string sLNS, string sNguoiTao, int donViTinh, int iQuy, string sMaLoaiChi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_cp_rpt_kehoach_cho_cssk_hssv_nld_bh @NamLamViec, @IDLoaiCap, @IdDonVi, @LNS, @UserName, @Dvt, @Quy,@MaLoaiChi";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", yearOfWork),
                    new SqlParameter("@IDLoaiCap", iDLoaiCap),
                    new SqlParameter("@IdDonvi", lstDonVi),
                    new SqlParameter("@LNS", sLNS),
                    new SqlParameter("@UserName", sNguoiTao),
                    new SqlParameter("@Dvt", donViTinh),
                    new SqlParameter("@Quy", iQuy),
                    new SqlParameter("@MaLoaiChi",sMaLoaiChi)
                };
                return ctx.FromSqlRaw<ReportBHChungTuCapPhatKeHoachQuery>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<ReportBHChungTuCapPhatThongTriQuery> GetDataReportCapPhatThongTri(int yearOfWork, Guid idLoaiChi, string maDonVi, string sLNS, string sNguoiTao, int donViTinh, int iQuy)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_cp_lns_rpt_thongtri_lns_bh @NamLamViec, @IDLoaiChi, @IdDonVi, @LNS, @UserName, @Dvt, @Quy";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", yearOfWork),
                    new SqlParameter("@IDLoaiChi", idLoaiChi),
                    new SqlParameter("@IdDonvi", maDonVi),
                    new SqlParameter("@LNS", sLNS),
                    new SqlParameter("@UserName", sNguoiTao),
                    new SqlParameter("@Dvt", donViTinh),
                    new SqlParameter("@Quy", iQuy)
                };
                return ctx.FromSqlRaw<ReportBHChungTuCapPhatThongTriQuery>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<ReportBHChungTuCapPhatKeHoachQuery> GetDataReportCapPhatThongTriForDonVi(string lstMaDonVi, int iQuy, int yearOfWork, string principal, int donViTinh, Guid idLoaiCap)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_cp_rpt_thongtri_donvi_bh @IdMaDonVi, @IQuy, @NamLamViec, @UserName, @Donvitinh,@iIdLoaiCap";
                var parameters = new[]
                {
                    new SqlParameter("@IdMaDonVi", lstMaDonVi),
                    new SqlParameter("@IQuy", iQuy),
                    new SqlParameter("@NamLamViec", yearOfWork),
                    new SqlParameter("@UserName", principal),
                    new SqlParameter("@Donvitinh", donViTinh),
                    new SqlParameter("@iIdLoaiCap", idLoaiCap)
                };
                return ctx.FromSqlRaw<ReportBHChungTuCapPhatKeHoachQuery>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<ReportBHChungTuCapPhatKeHoachQuery> GetDataReportCapPhatThongTriForDonViCsskHssvNld(int yearOfWork, Guid idLoaiCap
                                                                , string lstMaDonVi, string sLNS,
                                                                string principal, int donViTinh, int iQuy)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_cp_rpt_thongtri_theodonvi_cssk_hssv_nld_bh @NamLamViec, @IDLoaiCap, @IdDonVi, @LNS, @UserName, @Dvt, @Quy";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", yearOfWork),
                    new SqlParameter("@IDLoaiCap", idLoaiCap),
                    new SqlParameter("@IdDonvi", lstMaDonVi),
                    new SqlParameter("@LNS", sLNS),
                    new SqlParameter("@UserName", principal),
                    new SqlParameter("@Dvt", donViTinh),
                    new SqlParameter("@Quy", iQuy),
                };
                return ctx.FromSqlRaw<ReportBHChungTuCapPhatKeHoachQuery>(executeSql, parameters).ToList();
            }
        }

        public int GetSoChungTuIndexByCondition(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.BhCpChungTus.Where(x=>x.INamChungTu== namLamViec).OrderByDescending(x => x.SSoChungTu).Select(x => x.SSoChungTu).ToList();
                if (result.Count <= 0) return 1;
                try
                {
                    var indexString = result.FirstOrDefault().Substring(3, 3);
                    var index = int.Parse(indexString) + 1;
                    return index;
                }
                catch (Exception)
                {
                    return result.Count + 1;
                }
            }
        }
    }
}
