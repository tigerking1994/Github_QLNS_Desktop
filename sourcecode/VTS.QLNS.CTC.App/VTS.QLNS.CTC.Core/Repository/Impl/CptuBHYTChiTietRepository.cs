using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class CptuBHYTChiTietRepository : Repository<BhCptuBHYTChiTiet>, ICptuBHYTChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public CptuBHYTChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhCptuBHYTChiTiet> FindByCondition(Expression<Func<BhCptuBHYTChiTiet, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhCptuBHYTChiTiets.Where(predicate).ToList();
            }
        }

        public void CreateVoudcherSummary(string idChungTu, string nguoiTao, int namLamViec, string idChungTuSummary)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_cptubhyt_create_data_summary @IdChungTu, @NguoiTao, @YearOfWork, @IdChungTuSummary";
                var parameters = new[]
                {
                    new SqlParameter("@IdChungTu", idChungTu),
                    new SqlParameter("@NguoiTao", nguoiTao),
                    new SqlParameter("@YearOfWork", namLamViec),
                    new SqlParameter("@IdChungTuSummary", idChungTuSummary)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public IEnumerable<BhCptuBHYTChiTietQuery> FinChungTuChiTiet(Guid idChungTu, string sLNS, string idCsYTe, int iNamLamViec, int iQuyKyTruoc, int iNamKyTruoc, string userName)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter chungTuPhaBoIdParam = new SqlParameter("@ChungTuId", idChungTu);
                SqlParameter sLNSParam = new SqlParameter("@LNS", sLNS);
                SqlParameter sIdCsYTeParam = new SqlParameter("@IdCsYTe", idCsYTe);
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter userNameParam = new SqlParameter("@UserName", userName);
                SqlParameter iNamKyTruocParam = new SqlParameter("@INamKyTruoc", iNamKyTruoc);
                SqlParameter iQuyKyTruocParam = new SqlParameter("@IQuyKyTruoc", iQuyKyTruoc);

                return ctx.FromSqlRaw<BhCptuBHYTChiTietQuery>("EXECUTE sp_bh_dt_danhsach_cptubhyt_chitiet @ChungTuId, @LNS, @IdCsYTe, @NamLamViec, @INamKyTruoc, @IQuyKyTruoc, @UserName",
                              chungTuPhaBoIdParam, sLNSParam, sIdCsYTeParam, iNamLamViecParam, iQuyKyTruocParam, iNamKyTruocParam, userNameParam).ToList();
            }
        }

        public IEnumerable<BhCptuBHYTChiTietQuery> FindChungTuImport(int iQuy, string sLNS, string idCsYTe, int iNamLamViec, int iQuyKyTruoc, int iNamKyTruoc, string userName)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iQuyParam = new SqlParameter("@iQuy", iQuy);
                SqlParameter sLNSParam = new SqlParameter("@LNS", sLNS);
                SqlParameter sIdCsYTeParam = new SqlParameter("@IdCsYTe", idCsYTe);
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter userNameParam = new SqlParameter("@UserName", userName);
                SqlParameter iNamKyTruocParam = new SqlParameter("@INamKyTruoc", iNamKyTruoc);
                SqlParameter iQuyKyTruocParam = new SqlParameter("@IQuyKyTruoc", iQuyKyTruoc);

                return ctx.FromSqlRaw<BhCptuBHYTChiTietQuery>("EXECUTE sp_bh_dt_danhsach_cptubhyt_import @iQuy, @LNS, @IdCsYTe, @NamLamViec, @INamKyTruoc, @IQuyKyTruoc, @UserName",
                              iQuyParam, sLNSParam, sIdCsYTeParam, iNamLamViecParam, iQuyKyTruocParam, iNamKyTruocParam, userNameParam).ToList();
            }
        }

        public IEnumerable<BhCptuBHYTChiTietQuery> ExportKeHoachCapPhatTamUngKCBBHYT(string sIdCsYTe, int iLoai, int iQuy, int iNamLamViec, string userName, int donViTinh, string sLns, bool isRoundMillion)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter sIdCsYTeParam = new SqlParameter("@IdCsYTe", sIdCsYTe);
                SqlParameter iLoaiParam  = new SqlParameter("@ILoai", iLoai);
                SqlParameter iQuyParam = new SqlParameter("@IQuy", iQuy);
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter userNameParam = new SqlParameter("@UserName", userName);
                SqlParameter donViTinhParam = new SqlParameter("@Donvitinh", donViTinh);
                SqlParameter sLnsParam = new SqlParameter("@Lns", sLns);
                SqlParameter isRoundMillionParam = new SqlParameter("@IsRoundMillion", isRoundMillion);

                return ctx.FromSqlRaw<BhCptuBHYTChiTietQuery>("EXECUTE sp_bh_export_kehoachcaptamung_kcbbhyt @IdCsYTe, @ILoai, @IQuy, @NamLamViec, @UserName,@Donvitinh, @Lns, @IsRoundMillion",
                              sIdCsYTeParam, iLoaiParam, iQuyParam, iNamLamViecParam, userNameParam, donViTinhParam, sLnsParam, isRoundMillionParam).ToList();
            }
        }

        public IEnumerable<BhCptuBHYTChiTietQuery> ExportTongHopCapPhatTamUngKCBBHYT(string sIdCsYTe, int iLoai, int iQuy, int iNamLamViec, string userName, int donViTinh, string sLns, bool isRoundMillion)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter sIdCsYTeParam = new SqlParameter("@IdCsYTe", sIdCsYTe);
                SqlParameter iLoaiParam = new SqlParameter("@ILoai", iLoai);
                SqlParameter iQuyParam = new SqlParameter("@IQuy", iQuy);
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter userNameParam = new SqlParameter("@UserName", userName);
                SqlParameter sLnsParam = new SqlParameter("@LNS", sLns);
                SqlParameter donViTinhParam = new SqlParameter("@Donvitinh", donViTinh);
                SqlParameter isRoundMillionParam = new SqlParameter("@IsRoundMillion", isRoundMillion);

                return ctx.FromSqlRaw<BhCptuBHYTChiTietQuery>("EXECUTE sp_bh_export_tonghopcaptamung_kcbbhyt @IdCsYTe, @ILoai, @IQuy, @NamLamViec, @UserName, @LNS, @Donvitinh, @IsRoundMillion",
                              sIdCsYTeParam, iLoaiParam, iQuyParam, iNamLamViecParam, userNameParam, sLnsParam, donViTinhParam, isRoundMillionParam).ToList();
            }
        }

        public IEnumerable<BhCptuBHYTChiTietQuery> ExportThongTriCapPhatTamUngKCBBHYT(string sIdCsYTe, int iLoai, string sLns, int iQuy, int iNamLamViec, string userName, int donViTinh, bool isRoundMillion)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter sIdCsYTeParam = new SqlParameter("@IdCsYTe", sIdCsYTe);
                SqlParameter iLoaiParam = new SqlParameter("@ILoai", iLoai);
                SqlParameter sLnsParam = new SqlParameter("@LNS", sLns);
                SqlParameter iQuyParam = new SqlParameter("@IQuy", iQuy);
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter userNameParam = new SqlParameter("@UserName", userName);
                SqlParameter donViTinhParam = new SqlParameter("@Donvitinh", donViTinh);
                SqlParameter isRoundMillionParam = new SqlParameter("@IsRoundMillion", isRoundMillion);

                return ctx.FromSqlRaw<BhCptuBHYTChiTietQuery>("EXECUTE sp_bh_export_thongtricaptamung_kcbbhyt @IdCsYTe, @ILoai,@LNS, @IQuy, @NamLamViec, @UserName,@Donvitinh,@IsRoundMillion",
                              sIdCsYTeParam, iLoaiParam, sLnsParam, iQuyParam, iNamLamViecParam, userNameParam, donViTinhParam, isRoundMillionParam).ToList();
            }
        }

        public IEnumerable<ReportQuyetToanKCBBHYTQuery> GetDataReportQuyetToanKPKCBBHYTChiTiet(int quy, int namlamviec, string lns, int donvitinh, string idMaCoSoYTe)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter quyParam = new SqlParameter("@Quy", quy);
                SqlParameter namlamviecParam = new SqlParameter("@NamLamViec", namlamviec);
                SqlParameter lnsParam = new SqlParameter("@LNS", lns);
                SqlParameter donvitinhParam = new SqlParameter("@DonViTinh", donvitinh);
                SqlParameter idMaCoSoYTeParam = new SqlParameter("@IdMaCoSoYTe", idMaCoSoYTe);

                return ctx.FromSqlRaw<ReportQuyetToanKCBBHYTQuery>("EXECUTE sp_bh_qt_report_thongtriquyettoanKCBBHHY_chitiet @Quy, @NamLamViec, @LNS, @DonViTinh, @IdMaCoSoYTe",
                              quyParam, namlamviecParam, lnsParam, donvitinhParam, idMaCoSoYTeParam).ToList();
            }
        }

        public IEnumerable<ReportQuyetToanKCBBHYTQuery> GetDataReportQuyetToanKPKCBBHYTTongHop(int quy, int namlamviec, string lns, int donvitinh, string idMaCoSoYTe)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter quyParam = new SqlParameter("@Quy", quy);
                SqlParameter namlamviecParam = new SqlParameter("@NamLamViec", namlamviec);
                SqlParameter lnsParam = new SqlParameter("@LNS", lns);
                SqlParameter donvitinhParam = new SqlParameter("@DonViTinh", donvitinh);
                SqlParameter idMaCoSoYTeParam = new SqlParameter("@IdMaCoSoYTe", idMaCoSoYTe);

                return ctx.FromSqlRaw<ReportQuyetToanKCBBHYTQuery>("EXECUTE sp_bh_qt_report_thongtriquyettoanKCBBHHY_tonghop @Quy, @NamLamViec, @LNS, @DonViTinh, @IdMaCoSoYTe",
                              quyParam, namlamviecParam, lnsParam, donvitinhParam, idMaCoSoYTeParam).ToList();
            }
        }

        public IEnumerable<BhCptuBHYTChiTiet> FindChungTuChiTietByChungTuId(BhCpTUChungTuChiTietCriteria searchModel)
        {
            Guid chungTuId = searchModel.IIDCTCapPhatTU;
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhCptuBHYTChiTiets.Where(x => x.IID_BH_CP_CapTamUng_KCB_BHYT == chungTuId).ToList();
            }
        }
    }
}
