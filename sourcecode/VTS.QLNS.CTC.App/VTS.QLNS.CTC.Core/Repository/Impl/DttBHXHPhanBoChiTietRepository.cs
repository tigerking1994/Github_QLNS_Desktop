using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class DttBHXHPhanBoChiTietRepository : Repository<BhDtPhanBoChungTuChiTiet>, IDttBHXHPhanBoChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public DttBHXHPhanBoChiTietRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void DeleteByIdChungTu(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var idChungTuParam = new SqlParameter("@idChungTuParam", id.ToString());
                ctx.Database.ExecuteSqlCommand($"DELETE FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet WHERE iID_DTT_BHXH_ChungTu = @idChungTuParam", idChungTuParam);
            }
        }

        public void DeleteByIdChungTuDuToanNhan(Guid id, string idDuToanNhan)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_bhxh_delete_dtt_phan_bo_chung_tu_chi_tiet_by_dotnhan @iID_DTChungTu, @iID_CTDuToan_Nhan";
                var parameters = new[]
                {
                    new SqlParameter("@iID_DTChungTu", id.ToString()),
                    new SqlParameter("@iID_CTDuToan_Nhan", idDuToanNhan),
                };
                ctx.FromSqlRaw<NsDtChungTuChiTietQuery>(sql, parameters).ToList();
            }
        }

        public void DeleteByIds(IEnumerable<string> ids)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                if (ids == null || !ids.Any())
                {
                    return;
                }

                foreach (var id in ids)
                {
                    var idParam = new SqlParameter("@id", id);
                    ctx.Database.ExecuteSqlCommand($"DELETE FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet WHERE iID_DTT_BHXH_ChungTu_ChiTiet = @id", idParam);
                }
            }
        }

        public IEnumerable<BhDtPhanBoChungTuChiTietQuery> FindByCondition(DuToanThuChungTuChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var chungTuIdParam = new SqlParameter("@ChungTuId", Guid.Empty.Equals(searchCondition.VoucherId) ? searchCondition.ChungTuId : searchCondition.VoucherId.ToString());
                var lnsParam = new SqlParameter("@LNS", searchCondition.LNS);
                var yearOfWorkParam = new SqlParameter("@YearOfWork", searchCondition.YearOfWork);
                return ctx.FromSqlRaw<BhDtPhanBoChungTuChiTietQuery>("EXECUTE dbo.sp_dtt_bhxh_export_phan_bo_du_toan_chi_tiet @ChungTuId, @LNS, @YearOfWork",
                    chungTuIdParam, lnsParam, yearOfWorkParam).ToList();
            }
        }

        public IEnumerable<BhDtPhanBoChungTuChiTiet> FindByCondition(Expression<Func<BhDtPhanBoChungTuChiTiet, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhDtPhanBoChungTuChiTiets.Where(predicate).ToList();
            }
        }

        public IEnumerable<BhDtPhanBoChungTuChiTiet> FindByIdChungTu(string idChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                List<string> listId = idChungTu.Split(',').ToList();
                return ctx.BhDtPhanBoChungTuChiTiets.Where(n => listId.Contains(n.IIdDttBHXH.ToString())).ToList();
            }
        }

        public IEnumerable<BhDtPhanBoChungTuChiTietQuery> FindChungTuChiTiet(Guid chungTuPhanBoId, string sLNS, string sIdDonVi, int iNamLamViec, string userName)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter chungTuPhaBoIdParam = new SqlParameter("@ChungTuId", chungTuPhanBoId);
                SqlParameter sLNSParam = new SqlParameter("@LNS", sLNS);
                SqlParameter sIdDonViParam = new SqlParameter("@IdDonVi", sIdDonVi);
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter userNameParam = new SqlParameter("@UserName", userName);

                return ctx.FromSqlRaw<BhDtPhanBoChungTuChiTietQuery>("EXECUTE sp_bh_danh_sach_phan_bo_dtt_chitiet @ChungTuId, @LNS, @IdDonVi, @NamLamViec, @UserName",
                              chungTuPhaBoIdParam, sLNSParam, sIdDonViParam, iNamLamViecParam, userNameParam).ToList();
            }
        }

        public IEnumerable<BhDtPhanBoChungTuChiTietQuery> FindChungTuChiTietDieuChinh(Guid chungTuPhanBoId, string sLNS, int iNamLamViec, string userName)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter chungTuPhaBoIdParam = new SqlParameter("@ChungTuId", chungTuPhanBoId);
                SqlParameter sLNSParam = new SqlParameter("@LNS", sLNS);
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter userNameParam = new SqlParameter("@UserName", userName);

                return ctx.FromSqlRaw<BhDtPhanBoChungTuChiTietQuery>("EXECUTE sp_bh_danh_sach_phan_bo_dtt_chitiet_dieu_chinh @ChungTuId, @LNS, @NamLamViec, @UserName",
                              chungTuPhaBoIdParam, sLNSParam, iNamLamViecParam, userNameParam).ToList();
            }
        }

        public bool IsExistEstimate(Guid id, Guid estimateId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhDtPhanBoChungTuChiTiets.Any(c => c.IIdDttBHXH.Value == id && c.IIdCtduToanNhan.Equals(estimateId));
            }
        }

        public IEnumerable<ReportKhtDuToanBHXHQuery> ExportKhtDuToanBHXH(int namLamViec, string lstDonvi, string khoiDuToan, string khoiHachToan, string soQuyetDinh, string ngayQuyetDinh, int dvt, bool isMillionRound, bool IsKhoi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = string.Empty;
                if (!IsKhoi)
                    sql = "EXECUTE dbo.sp_rpt_kht_du_toan_thu_bhxh @NamLamViec, @LstSelectedUnit, @KhoiDuToan, @KhoiHachToan, @SoQuyetDinh, @NgayQuyetDinh, @DVT, @IsMillionRound";
                else sql = "EXECUTE dbo.sp_rpt_kht_du_toan_thu_theokhoi_bhxh @NamLamViec, @LstSelectedUnit, @KhoiDuToan, @KhoiHachToan, @SoQuyetDinh, @NgayQuyetDinh, @DVT, @IsMillionRound";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@LstSelectedUnit", lstDonvi),
                    new SqlParameter("@KhoiDuToan", khoiDuToan),
                    new SqlParameter("@KhoiHachToan", khoiHachToan),
                    new SqlParameter("@SoQuyetDinh", soQuyetDinh),
                    new SqlParameter("@NgayQuyetDinh", ngayQuyetDinh),
                    new SqlParameter("@DVT", dvt),
                    new SqlParameter("@IsMillionRound", isMillionRound)
                };
                return ctx.FromSqlRaw<ReportKhtDuToanBHXHQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<ReportKhtDuToanBHXHQuery> ExportKhtDuToanBHYT(int namLamViec, string lstDonvi, string khoiDuToan, string khoiHachToan, string sm, string soQuyetDinh, string ngayQuyetDinh, int dvt, bool isMillionRound, bool IsKhoi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = string.Empty;
                if (!IsKhoi)
                    sql = "EXECUTE dbo.sp_rpt_kht_du_toan_thu_bhyt @NamLamViec, @LstSelectedUnit, @KhoiDuToan, @KhoiHachToan, @SM, @SoQuyetDinh, @NgayQuyetDinh, @DVT, @IsMillionRound";
                else sql = "EXECUTE dbo.sp_rpt_kht_du_toan_thu_theokhoi_bhyt @NamLamViec, @LstSelectedUnit, @KhoiDuToan, @KhoiHachToan, @SM, @SoQuyetDinh, @NgayQuyetDinh, @DVT, @IsMillionRound";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@LstSelectedUnit", lstDonvi),
                    new SqlParameter("@KhoiDuToan", khoiDuToan),
                    new SqlParameter("@KhoiHachToan", khoiHachToan),
                    new SqlParameter("@SM", sm),
                    new SqlParameter("@SoQuyetDinh", soQuyetDinh),
                    new SqlParameter("@NgayQuyetDinh", ngayQuyetDinh),
                    new SqlParameter("@DVT", dvt),
                    new SqlParameter("@IsMillionRound", isMillionRound)
                };
                return ctx.FromSqlRaw<ReportKhtDuToanBHXHQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<BhReportQttBHXHChiTietQuery> ExportTongHopDuToanThuChi(int iNamLamViec, string sIdDonVis, string soQuyetDinh, string ngayQuyetDinh, int donViTinh, bool isMillionRound)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter sIdDonViParam = new SqlParameter("@LstMaDonVi", sIdDonVis);
                SqlParameter sSoQuyetDinh = new SqlParameter("@SoQuyetDinh ", soQuyetDinh);
                SqlParameter sNgayQuyetDinh = new SqlParameter("@NgayQuyetDinh ", ngayQuyetDinh);
                SqlParameter donViTinhParam = new SqlParameter("@DVT", donViTinh);
                SqlParameter isMillionRoundParam = new SqlParameter("@IsMillionRound", isMillionRound);
                return ctx.FromSqlRaw<BhReportQttBHXHChiTietQuery>("EXECUTE dbo.sp_bh_report_tong_hop_du_toan_thu_chi_bhxh_bhyt_bhtn "
                    + "@NamLamViec, @LstMaDonVi, @SoQuyetDinh, @NgayQuyetDinh, @DVT, @IsMillionRound", iNamLamViecParam, sIdDonViParam, sSoQuyetDinh, sNgayQuyetDinh, donViTinhParam, isMillionRoundParam).ToList();
            }
        }

        public IEnumerable<ReportKhtDuToanBHXHBHYTBHTNQuery> ExportKhtDuToanBHXHBHYTBHTN(int yearOfWork, string selectedUnits, string soQuyetDinh, string ngayQuyetDinh, int donViTinh, bool isMillionRound)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", yearOfWork);
                SqlParameter sIdDonViParam = new SqlParameter("@LstSelectedUnit", selectedUnits);
                SqlParameter sSoQuyetDinh = new SqlParameter("@SoQuyetDinh ", soQuyetDinh);
                SqlParameter sNgayQuyetDinh = new SqlParameter("@NgayQuyetDinh ", ngayQuyetDinh);
                SqlParameter donViTinhParam = new SqlParameter("@DVT", donViTinh);
                SqlParameter isMillionRoundParam = new SqlParameter("@IsMillionRound", isMillionRound);
                return ctx.FromSqlRaw<ReportKhtDuToanBHXHBHYTBHTNQuery>("EXECUTE dbo.sp_rpt_kht_du_toan_thu_bhxh_bhyt_bhtn "
                    + "@NamLamViec, @LstSelectedUnit, @SoQuyetDinh, @NgayQuyetDinh, @DVT, @IsMillionRound", iNamLamViecParam, sIdDonViParam, sSoQuyetDinh, sNgayQuyetDinh, donViTinhParam, isMillionRoundParam).ToList();
            }
        }
    }
}
