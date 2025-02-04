using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class BhQtcNamKinhPhiKhacChiTietRepostiory : Repository<BhQtcNamKinhPhiKhacChiTiet>, IBhQtcNamKinhPhiKhacChiTietRepostiory
    {

        private readonly ApplicationDbContextFactory _contextFactory;

        public BhQtcNamKinhPhiKhacChiTietRepostiory(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void AddAggregate(QtcNamKinhPhiKhacCriteria criteria)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_qtc_nkpk_create_data_summary_bh @IdChungTu,@IDMaDonVi, @NguoiTao, @YearOfWork, @LstIdChungTuSummary";
                var parameters = new[]
                {
                    new SqlParameter("@IdChungTu", criteria.ID),
                    new SqlParameter("@IDMaDonVi",criteria.IDMaDonVi),
                    new SqlParameter("@NguoiTao", criteria.SNguoiTao),
                    new SqlParameter("@YearOfWork", criteria.NamLamViec),
                    new SqlParameter("@LstIdChungTuSummary", criteria.LstSoChungTu),
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public bool ExitChungTuChiTiet(QtcNamKinhPhiKhacCriteria searchCondition)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {

                return ctx.BhQtcNamKinhPhiKhacChiTiets.Any(x => x.IID_QTC_Nam_KPK.Equals(searchCondition.ID) && x.FTien_DuToanGiaoNamNay.GetValueOrDefault(0) != 0);
            }
        }

        public BhQtcNamKinhPhiKhacChiTiet FindById(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcNamKinhPhiKhacChiTiets.Find(id);
            }
        }

        public IEnumerable<BhQtcNamKinhPhiKhacChiTiet> FindByIdChiTiet(Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcNamKinhPhiKhacChiTiets.Where(x => x.IID_QTC_Nam_KPK == id).ToList();
            }
        }

        public List<BhQtcNamKinhPhiKhacChiTietQuery> FindChungTuChiTiet(QtcNamKinhPhiKhacCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", searchCondition.NamLamViec),
                    new SqlParameter("@iD",searchCondition.ID),
                    new SqlParameter("@IdDonVi", searchCondition.IDMaDonVi),
                    new SqlParameter("@LNS", searchCondition.SLNS),
                    new SqlParameter("@Loai", searchCondition.LoaiChungTu)
                };

                string executeSql = "EXECUTE dbo.sp_qtc_nkqk_chitiet_bh @NamLamViec, @iD,@IdDonVi,@LNS,@Loai";
                return ctx.FromSqlRaw<BhQtcNamKinhPhiKhacChiTietQuery>(executeSql, parameters).ToList();
            }
        }

        public List<BhQtcNamKinhPhiKhacChiTietQuery> FindGetReportKeHoach(QtcNamKinhPhiKhacCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", searchCondition.NamLamViec),
                    new SqlParameter("@IdDonVi", searchCondition.IDMaDonVi),
                    new SqlParameter("@LNS", searchCondition.SLNS),
                    new SqlParameter("@LoaiChi",searchCondition.IDLoaiChi),
                    new SqlParameter("@Loai",searchCondition.LoaiChungTu),
                    new SqlParameter("@Dvt",searchCondition.DonViTinh)
                };

                string executeSql = "EXECUTE dbo.sp_rpt_qtc_nkpk_kehoach_bh @NamLamViec, @IdDonVi, @LNS, @LoaiChi,@Loai,@Dvt";
                return ctx.FromSqlRaw<BhQtcNamKinhPhiKhacChiTietQuery>(executeSql, parameters).ToList();
            }
        }

        public List<ReportBHQTCNKPKhacPhuLucQuery> FindGetReportPhuLuc(int iNamLamViec, string listTenDonVi, string sLNS)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@listTenDonVi", listTenDonVi),
                    new SqlParameter("@namLamViec", iNamLamViec),
                    new SqlParameter("@LNS", sLNS)

                };

                string executeSql = "EXECUTE dbo.sp_rpt_qtc_nkpk_phucluc_bh @listTenDonVi, @namLamViec,@LNS";
                return ctx.FromSqlRaw<ReportBHQTCNKPKhacPhuLucQuery>(executeSql, parameters).ToList();
            }
        }

        public List<BhQtcNamKinhPhiKhacChiTietQuery> FindTienThuChiForChungTu(QtcNamKinhPhiKhacCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", searchCondition.NamLamViec),
                    new SqlParameter("@AgencyId", searchCondition.IDMaDonVi),
                    new SqlParameter("@VoucherDate", searchCondition.DNgayChungTu),
                    new SqlParameter("@IQuyChungTu", searchCondition.IQuyChungTu),
                    new SqlParameter("@ILoaiChi",searchCondition.IDLoaiChi)
                };

                string executeSql = "EXECUTE dbo.sp_qtc_namkinh_phikhac_gettien_thuchi_bh @YearOfWork,@AgencyId,@VoucherDate,@IQuyChungTu,@ILoaiChi";
                return ctx.FromSqlRaw<BhQtcNamKinhPhiKhacChiTietQuery>(executeSql, parameters).ToList();
            }
        }

        public List<BhQtcNamKinhPhiKhacChiTietQuery> GetExcelData(QtcNamKinhPhiKhacCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", searchCondition.NamLamViec),
                    new SqlParameter("@iD",searchCondition.ID),
                    new SqlParameter("@IdDonVi", searchCondition.IDMaDonVi),
                    new SqlParameter("@LNS", searchCondition.SLNS)
                };

                string executeSql = "EXECUTE dbo.sp_qtc_nkqk_chitiet_export_excel_bh @NamLamViec, @iD,@IdDonVi,@LNS";
                return ctx.FromSqlRaw<BhQtcNamKinhPhiKhacChiTietQuery>(executeSql, parameters).ToList();
            }
        }

        public List<BhQtcNamKinhPhiKhacChiTietQuery> GetTienPhanBoChiTietDuToanChi(int iNamLamViec, Guid iID_LoaiChi, string iID_MaDonVi, string sDSLNS, DateTime? dNgayChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@INamLamViec ", iNamLamViec);
                SqlParameter idLoaiChiParam = new SqlParameter("@IDLoaiChi", iID_LoaiChi);
                SqlParameter sMaDonViParam = new SqlParameter("@SMaDonVi", iID_MaDonVi);
                SqlParameter sLNSParam = new SqlParameter("@SLNS", sDSLNS);
                SqlParameter dNgayChungTuParam = new SqlParameter("@DNgayChungTu ", dNgayChungTu);

                return ctx.FromSqlRaw<BhQtcNamKinhPhiKhacChiTietQuery>("EXECUTE [sp_bh_quyet_toan_get_fTienDuToanGiaoNamNay_nkpk_for_pbdtchi]  @INamLamViec,@IDLoaiChi ,@SMaDonVi,@SLNS,@DNgayChungTu",
                    iNamLamViecParam, idLoaiChiParam, sMaDonViParam, sLNSParam, dNgayChungTuParam).ToList();

            }
        }
    }
}
