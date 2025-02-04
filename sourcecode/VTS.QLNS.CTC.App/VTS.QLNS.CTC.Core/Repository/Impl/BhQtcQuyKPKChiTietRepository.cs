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
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using static Dapper.SqlMapper;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class BhQtcQuyKPKChiTietRepository : Repository<BhQtcQuyKPKChiTiet>, IBhQtcQuyKPKChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public BhQtcQuyKPKChiTietRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void AddAggregate(QtcQuyKCBCriteria criteria)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter listIdChungTuTongHop = new SqlParameter("@ListIdChungTuTongHop", criteria.LstSoChungTu);
                SqlParameter nguoiTao = new SqlParameter("@Nguoitao", criteria.SNguoiTao);
                SqlParameter idChungTu = new SqlParameter("@IdChungTu", criteria.ID);
                SqlParameter namLamViec = new SqlParameter("@NamLamViec", criteria.NamLamViec);
                SqlParameter sMaDonVi = new SqlParameter("@SMaDonVi", criteria.IDMaDonVi);
                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_qtc_quykinhphi_khac_chungtu_chitiet_tao_tonghop @ListIdChungTuTongHop,@Nguoitao ,@IdChungTu, @NamLamViec,@SMaDonVi",
                    listIdChungTuTongHop, nguoiTao, idChungTu, namLamViec, sMaDonVi);
            }
        }

        public void CreateVoudcherForQuaterBefore(BhQtcQuyKPK entity)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_qtcqKPk_create_datafor_quaterbefore @IdChungTu, @Username, @NamLamViec, @Quy, @LoaiChi, @MaDonVi";
                var parameters = new[]
                {
                    new SqlParameter("@IdChungTu", entity.Id),
                    new SqlParameter("@Username", entity.SNguoiTao),
                    new SqlParameter("@NamLamViec", entity.INamChungTu),
                    new SqlParameter("@Quy", entity.IQuyChungTu),
                    new SqlParameter("@LoaiChi", entity.IID_LoaiChi),
                    new SqlParameter("@MaDonVi", entity.IID_MaDonVi),
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public void DeleteByIdChungTu(Guid idChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = @"DELETE  BH_QTC_Quy_KPK_CHiTiet WHERE iID_QTC_Quy_KPK = @IdChungTu";
                var parameter = new SqlParameter("@IdChungTu", idChungTu);
                ctx.Database.ExecuteSqlCommand(sql, parameter);
            }
        }

        public bool ExitChungTuChiTiet(QtcQuyKCBCriteria searchCondition)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var lstChungTu = ctx.BhQtcQuyKPKChiTiets.Where(x => x.IID_QTC_Quy_KPK.Equals(searchCondition.ID) && x.FTien_DuToanGiaoNamNay.GetValueOrDefault(0) != 0);
                if (lstChungTu.Count()>0)
                    return true;
                return false;
            }
        }

        public BhQtcQuyKPKChiTiet FindById(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcQuyKPKChiTiets.Find(id);
            }
        }

        public IEnumerable<BhQtcQuyKPKChiTiet> FindByIdChiTiet(Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcQuyKPKChiTiets.Where(x => x.IID_QTC_Quy_KPK == id).ToList();
            }
        }

        public List<BhQtcQuyKPKChiTietQuery> FindChungTuChiTiet(QtcQuyKCBCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@VoucherId",searchCondition.ID),
                    new SqlParameter("@LNS",searchCondition.SLNS),
                    new SqlParameter("@YearOfWork", searchCondition.NamLamViec),
                    new SqlParameter("@AgencyId", searchCondition.IDMaDonVi),
                    new SqlParameter("@VoucherDate", searchCondition.DNgayChungTu),
                    new SqlParameter("@iID_LoaiDanhMucChi", searchCondition.IDLoaiChi),
                    new SqlParameter("@iQuyChungTu", searchCondition.IQuyChungTu),
                    new SqlParameter("@loai", searchCondition.LoaiChungTu)
                };

                string executeSql = "EXECUTE dbo.sp_qtc_quykinhphi_khac_chungtu_chi_tiet @VoucherId, @LNS,@YearOfWork,@AgencyId,@VoucherDate,@iID_LoaiDanhMucChi,@iQuyChungTu,@loai";
                return ctx.FromSqlRaw<BhQtcQuyKPKChiTietQuery>(executeSql, parameters).ToList();
            }
        }

        public List<BhQtcQuyKPKChiTietQuery> FindSoTienQuyetToanDaDuocDuyetTheoQuy(QtcQuyKCBCriteria searchCondition)
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

                string executeSql = "EXECUTE dbo.sp_qtc_quykinhphi_khacget_tienquyet_toandaduyet @YearOfWork,@AgencyId,@VoucherDate,@iQuyChungTu";
                return ctx.FromSqlRaw<BhQtcQuyKPKChiTietQuery>(executeSql, parameters).ToList();
            }
        }

        public List<ReportBHQTCQKPKThongTriQuery> GetDataReportKeHoach(QtcQuyKCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", searchCondition.NamLamViec),
                    new SqlParameter("@iQuy", searchCondition.IQuyChungTu),
                    new SqlParameter("@IdDonVi", searchCondition.IDMaDonVi),
                    new SqlParameter("@LNS", searchCondition.SLNS),
                    new SqlParameter("@UserName", searchCondition.SNguoiTao),
                    new SqlParameter("@IdLoaiChi", searchCondition.IDLoaiChi),
                    new SqlParameter("@LoaiTongHop", searchCondition.LoaiChungTu),
                    new SqlParameter("@Dvt", searchCondition.DonViTinh)
                };

                string executeSql = "EXECUTE dbo.sp_rpt_qtc_qkpk_kehoach_bh @NamLamViec,@iQuy,@IdDonVi,@LNS,@UserName,@IdLoaiChi,@LoaiTongHop,@Dvt";
                return ctx.FromSqlRaw<ReportBHQTCQKPKThongTriQuery>(executeSql, parameters).ToList();
            }
        }

        public List<ReportBHQTCQKPKThongTriQuery> GetDataReportQtcQuyKPKThongTri1(int yearOfWork, string squy, string lstDonViChecked, string principal, int iLoaiChungTu, Guid iDLoaichi, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", yearOfWork),
                    new SqlParameter("@iQuy", squy),
                    new SqlParameter("@IdDonVi", lstDonViChecked),
                    new SqlParameter("@UserName", principal),
                    new SqlParameter("@iLoaiTongHop", iLoaiChungTu),
                    new SqlParameter("@IDLoaichi",iDLoaichi),
                    new SqlParameter("@Dvt", donViTinh)
                };

                string executeSql = "EXECUTE dbo.sp_rpt_qtc_qkpk_thongtriloai1_bh @NamLamViec,@iQuy,@IdDonVi,@UserName,@iLoaiTongHop,@IDLoaichi,@Dvt";
                return ctx.FromSqlRaw<ReportBHQTCQKPKThongTriQuery>(executeSql, parameters).ToList();
            }
        }

        public List<ReportBHQTCQKPKThongTriQuery> GetDataReportQtcQuyKPKThongTri2(int yearOfWork, string quy, string donVi, string sLNS, string principal, Guid IdLoaiChi, int iLoaiTongHop, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", yearOfWork),
                    new SqlParameter("@iQuy", quy),
                    new SqlParameter("@IdDonVi", donVi),
                    new SqlParameter("@LNS", sLNS),
                    new SqlParameter("@UserName", principal),
                    new SqlParameter("@IdLoaiChi", IdLoaiChi),
                    new SqlParameter("@LoaiTongHop", iLoaiTongHop),
                    new SqlParameter("@Dvt", donViTinh)
                };

                string executeSql = "EXECUTE dbo.sp_rpt_qtc_qkpk_thongtriloai2_bh @NamLamViec,@iQuy,@IdDonVi,@LNS,@UserName,@IdLoaiChi,@LoaiTongHop,@Dvt";
                return ctx.FromSqlRaw<ReportBHQTCQKPKThongTriQuery>(executeSql, parameters).ToList();
            }
        }

        public List<BhQtcQuyKPKChiTietQuery> GetDataTienDuToanPhanBoChi(QtcQuyKCBCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", searchCondition.NamLamViec),
                    new SqlParameter("@LNS", searchCondition.SLNS),
                    new SqlParameter("@AgencyId", searchCondition.IDMaDonVi),
                    new SqlParameter("@VoucherDate", searchCondition.DNgayChungTu),
                    new SqlParameter("@iID_LoaiDanhMucChi", searchCondition.IDLoaiChi),
                    new SqlParameter("@Quy", searchCondition.IQuyChungTu)
                };

                string executeSql = "EXECUTE dbo.sp_qtc_quyKhac_getTienDuToanDuocGiao_chi_tiet @YearOfWork,@LNS,@AgencyId,@VoucherDate,@iID_LoaiDanhMucChi,@Quy";
                return ctx.FromSqlRaw<BhQtcQuyKPKChiTietQuery>(executeSql, parameters).ToList();
            }
        }
    }
}
