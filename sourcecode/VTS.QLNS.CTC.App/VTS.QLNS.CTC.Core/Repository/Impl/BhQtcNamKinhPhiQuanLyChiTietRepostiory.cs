using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class BhQtcNamKinhPhiQuanLyChiTietRepostiory : Repository<BhQtcNamKinhPhiQuanLyChiTiet>, IBhQtcNamKinhPhiQuanLyChiTietRepostiory
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public BhQtcNamKinhPhiQuanLyChiTietRepostiory(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void AddAggregate(QtcNamKinhPhiQuanLyCriteria criteria)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_qtc_qkpql_create_data_summary_bh @IdChungTu,@IDMaDonVi, @NguoiTao, @YearOfWork, @LstIdChungTuSummary";
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

        public void CreateChungTuChiTietTheoQuy(Guid id, string idMaDonVi, int? namChungTu, string sNguoiTao)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_bh_create_quyet_toan_chinamKPQL_chitiet_theo4quy @idChungTu, @idMaDonVi, @iNamLamViec, @user";
                var parameters = new[]
                {
                    new SqlParameter("@idChungTu", id),
                    new SqlParameter("@IdMaDonVi", idMaDonVi),
                    new SqlParameter("@INamLamViec", namChungTu),
                    new SqlParameter("@User", sNguoiTao)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public bool ExitChungTuChiTiet(QtcNamKinhPhiQuanLyCriteria searchCondition)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcNamKinhPhiQuanLyChiTiets.Any(x => x.IID_QTC_Nam_KinhPhiQuanLy.Equals(searchCondition.ID));
            }
        }

        public BhQtcNamKinhPhiQuanLyChiTiet FindById(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcNamKinhPhiQuanLyChiTiets.Find(id);
            }
        }

        public IEnumerable<BhQtcNamKinhPhiQuanLyChiTiet> FindByIdChiTiet(Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcNamKinhPhiQuanLyChiTiets.Where(x => x.IID_QTC_Nam_KinhPhiQuanLy == id).ToList();
            }
        }

        public List<BhQtcNamKinhPhiQuanLyChiTietQuery> FindChungTuChiTiet(QtcNamKinhPhiQuanLyCriteria searchCondition)
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

                string executeSql = "EXECUTE dbo.sp_bh_qtc_nkp_ql_chitiet @NamLamViec, @iD,@IdDonVi,@LNS,@Loai";
                return ctx.FromSqlRaw<BhQtcNamKinhPhiQuanLyChiTietQuery>(executeSql, parameters).ToList();
            }
        }

        public List<BhQtcNamKinhPhiQuanLyChiTietQuery> FindGetReportKeHoach(QtcNamKinhPhiQuanLyCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", searchCondition.NamLamViec),
                    new SqlParameter("@iD",searchCondition.ID),
                    new SqlParameter("@IdDonVi", searchCondition.IDMaDonVi),
                    new SqlParameter("@LNS", searchCondition.SLNS),
                    new SqlParameter("@DonViTnh",searchCondition.DonViTinh),
                    new SqlParameter("@Loai",searchCondition.LoaiChungTu)
                };

                string executeSql = "EXECUTE dbo.sp_rpt_qtc_nqlkp_kehoach_bh @NamLamViec, @iD, @IdDonVi, @LNS, @DonViTnh,@Loai";
                return ctx.FromSqlRaw<BhQtcNamKinhPhiQuanLyChiTietQuery>(executeSql, parameters).ToList();
            }
        }

        public List<ReportBHQTCNKPQuanLyPhuLucQuery> FindGetReportPhuLuc(int iNamLamViec, string listTenDonVi, int dvt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@listTenDonVi", listTenDonVi),
                    new SqlParameter("@namLamViec", iNamLamViec),
                    new SqlParameter("@dvt", dvt)
                };

                string executeSql = "EXECUTE dbo.sp_rpt_qtc_nqlkp_phucluc_bh @listTenDonVi, @namLamViec,@dvt";
                return ctx.FromSqlRaw<ReportBHQTCNKPQuanLyPhuLucQuery>(executeSql, parameters).ToList();
            }
        }

        public List<BhQtcNamKinhPhiQuanLyChiTietQuery> FindTienThuChiForChungTu(QtcNamKinhPhiQuanLyCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", searchCondition.NamLamViec),
                    new SqlParameter("@AgencyId", searchCondition.IDMaDonVi),
                    new SqlParameter("@VoucherDate", searchCondition.DNgayChungTu),
                    new SqlParameter("@iQuyChungTu", searchCondition.IQuyChungTu)
                };

                string executeSql = "EXECUTE dbo.sp_qtc_namkinhphi_quanly_gettien_thuchi @YearOfWork,@AgencyId,@VoucherDate,@iQuyChungTu";
                return ctx.FromSqlRaw<BhQtcNamKinhPhiQuanLyChiTietQuery>(executeSql, parameters).ToList();
            }
        }

        public List<BhQtcNamKinhPhiQuanLyChiTietQuery> GetTienPhanBoChiTietDuToanChi(QtcNamKinhPhiQuanLyCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@INamLamViec ", searchCondition.NamLamViec);
                SqlParameter sMaLoaiChiParam = new SqlParameter("@SMaLoaiChi", searchCondition.SMaLoaiChi);
                SqlParameter idLoaiChiParam = new SqlParameter("@IDLoaiChi", searchCondition.IDLoaiChi);
                SqlParameter sMaDonViParam = new SqlParameter("@SMaDonVi", searchCondition.IDMaDonVi);
                SqlParameter sLNSParam = new SqlParameter("@SLNS", searchCondition.SLNS);
                SqlParameter dNgayChungTuParam = new SqlParameter("@DNgayChungTu ", searchCondition.DNgayChungTu);

                return ctx.FromSqlRaw<BhQtcNamKinhPhiQuanLyChiTietQuery>("EXECUTE sp_bh_quyet_toan_get_fTienDuToanGiaoNamNay_nkpql_for_pbdtchi  @INamLamViec,@SMaLoaiChi,@IDLoaiChi ,@SMaDonVi,@SLNS,@DNgayChungTu",
                    iNamLamViecParam, sMaLoaiChiParam, idLoaiChiParam, sMaDonViParam, sLNSParam, dNgayChungTuParam).ToList();

            }
        }

        public List<ReportBHQTCNKPQuanLyPhuLucQuery> FindGetReportQTKPQL_KPKTSDK(int iNamLamViec, string listTenDonVi, int typeValue)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@LstSelectedUnit", listTenDonVi),
                    new SqlParameter("@NamLamViec", iNamLamViec),
                    new SqlParameter("@TypeValue", typeValue)
                };

                string executeSql = "EXECUTE dbo.sp_bh_rpt_thamdinh_quyet_toan_kpql_kpktsdk @NamLamViec, @LstSelectedUnit, @TypeValue";
                return ctx.FromSqlRaw<ReportBHQTCNKPQuanLyPhuLucQuery>(executeSql, parameters).ToList();
            }
        }
    }
}
