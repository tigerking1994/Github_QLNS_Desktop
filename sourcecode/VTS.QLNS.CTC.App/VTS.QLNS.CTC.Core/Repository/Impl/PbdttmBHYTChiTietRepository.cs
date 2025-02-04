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

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class PbdttmBHYTChiTietRepository : Repository<BhPbdttmBHYTChiTiet>, IPbdttmBHYTChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public PbdttmBHYTChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhPbdttmBHYTChiTiet> FindByCondition(Expression<Func<BhPbdttmBHYTChiTiet, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhPbdttmBHYTChiTiets.Where(predicate).ToList();
            }
        }

        public IEnumerable<BhPbdttmBHYTChiTietQuery> FindChungTuChiTiet(Guid chungTuId, string sLNS, string sIDDonVi, int iNamLamViec, string userName)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter chungTuIdParam = new SqlParameter("@ChungTuId", chungTuId);
                SqlParameter sLNSParam = new SqlParameter("@LNS", sLNS);
                SqlParameter sIDDonViParam = new SqlParameter("@IdDonVi", sIDDonVi);
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter userNameParam = new SqlParameter("@UserName", userName);
                return ctx.FromSqlRaw<BhPbdttmBHYTChiTietQuery>("EXECUTE dbo.sp_bh_dttm_danhsach_pbdttm_chitiet @ChungTuId," +
                    "@LNS, @IdDonVi,@NamLamViec,@UserName ", chungTuIdParam, sLNSParam, sIDDonViParam, iNamLamViecParam, userNameParam).ToList();
            }
        }
        
        public IEnumerable<BhPbdttmBHYTChiTietQuery> FindChungTuChiTietDieuChinh(Guid chungTuId, string sLNS, int iNamLamViec, string userName)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter chungTuIdParam = new SqlParameter("@ChungTuId", chungTuId);
                SqlParameter sLNSParam = new SqlParameter("@LNS", sLNS);
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter userNameParam = new SqlParameter("@UserName", userName);
                return ctx.FromSqlRaw<BhPbdttmBHYTChiTietQuery>("EXECUTE dbo.sp_bh_dttm_danhsach_pbdttm_chitiet_dieuchinh @ChungTuId," +
                    "@LNS,@NamLamViec,@UserName ", chungTuIdParam, sLNSParam, iNamLamViecParam, userNameParam).ToList();
            }

        }

        public IEnumerable<BhPbdttmBHYTChiTietQuery> ExportExcelPhanBoDuToanChi(Guid chungTuId, string sLNS, int iNamLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter chungTuIdParam = new SqlParameter("@ChungTuId", chungTuId);
                SqlParameter sLNSParam = new SqlParameter("@LNS", sLNS);
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);

                return ctx.FromSqlRaw<BhPbdttmBHYTChiTietQuery>("EXECUTE sp_bh_export_phan_bo_du_toan_thumua_BHYT_chi_tiet @ChungTuId, @LNS, @NamLamViec",
                              chungTuIdParam, sLNSParam, iNamLamViecParam).ToList();
            }
        }

        public IEnumerable<ReportKhtmDuToanBHYTQuery> ExportDuToanThuBhytThanNhan(int namLamViec, string lstDonvi, string thanNhanQuanNhan, string thanNhanCNVQP
            , string smDuToan, string smHachToan, string soQuyetDinh, string ngayQuyetDinh, int dvt, bool isMillionRound)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_khtm_du_toan_thu_bhyt_than_nhan @namLamViec, @lstSelectedUnit, @thanNhanQuanNhan, @thanNhanCNVQP, @smDuToan, @smHachToan, @soQuyetDinh, @ngayQuyetDinh, @dvt, @IsMillionRound";
                var parameters = new[]
                {
                    new SqlParameter("@namLamViec", namLamViec),
                    new SqlParameter("@lstSelectedUnit", lstDonvi),
                    new SqlParameter("@thanNhanQuanNhan", thanNhanQuanNhan),
                    new SqlParameter("@thanNhanCNVQP", thanNhanCNVQP),
                    new SqlParameter("@smDuToan", smDuToan),
                    new SqlParameter("@smHachToan", smHachToan),
                    new SqlParameter("@soQuyetDinh", soQuyetDinh),
                    new SqlParameter("@ngayQuyetDinh", ngayQuyetDinh),
                    new SqlParameter("@dvt", dvt),
                    new SqlParameter("@IsMillionRound", isMillionRound)
                };
                return ctx.FromSqlRaw<ReportKhtmDuToanBHYTQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<ReportKhtmDuToanBHYTQuery> ExportDuToanThuBhytHSSV(int namLamViec, string lstDonvi, string hSSV, string luuHS, string hVSQ, string sQDuBi, string soQuyetDinh, string ngayQuyetDinh, int dvt, bool isMillionRound)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_khtm_du_toan_thu_bhyt_hssv_hvqs @namLamViec, @lstSelectedUnit, @hSSV, @luuHS, @hVSQ, @sQDuBi, @soQuyetDinh, @ngayQuyetDinh, @dvt, @IsMillionRound";
                var parameters = new[]
                {
                    new SqlParameter("@namLamViec", namLamViec),
                    new SqlParameter("@lstSelectedUnit", lstDonvi),
                    new SqlParameter("@hSSV", hSSV),
                    new SqlParameter("@luuHS", luuHS),
                    new SqlParameter("@hVSQ", hVSQ),
                    new SqlParameter("@sQDuBi", sQDuBi),
                    new SqlParameter("@soQuyetDinh", soQuyetDinh),
                    new SqlParameter("@ngayQuyetDinh", ngayQuyetDinh),
                    new SqlParameter("@dvt", dvt),
                    new SqlParameter("@IsMillionRound", isMillionRound)
                };
                return ctx.FromSqlRaw<ReportKhtmDuToanBHYTQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<BhPbdttmBHYTChiTiet> FindByXauNoiMaVaSoQuyetDinh(string sSoQuyetDinh, List<string> sLNS, int iNamLamViec, bool isContains)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhPbdttmBHYTChiTiets.Where(x => x.SSoQuyetDinh == sSoQuyetDinh && (isContains ? sLNS.Contains(x.SLNS) : !sLNS.Contains(x.SLNS)) && x.INamLamViec == iNamLamViec).ToList();
            }
        }
    }
}
