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
using static VTS.QLNS.CTC.Utility.Enum.BaoHiemDuToanTypeEnum;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class BhQtcQuyKinhPhiQuanLyChiTietRepostiory : Repository<BhQtcQuyKinhPhiQuanLyChiTiet>, IBhQtcQuyKinhPhiQuanLyChiTietRepostiory
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public BhQtcQuyKinhPhiQuanLyChiTietRepostiory(ApplicationDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            _contextFactory = dbContextFactory;
        }

        public void AddAggregate(QtcQuyKinhPhiQuanLyCriteria criteria)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter listIdChungTuTongHop = new SqlParameter("@ListIdChungTuTongHop", criteria.LstSoChungTu);
                SqlParameter nguoiTao = new SqlParameter("@Nguoitao", criteria.SNguoiTao);
                SqlParameter idChungTu = new SqlParameter("@IdChungTu", criteria.ID);
                SqlParameter namLamViec = new SqlParameter("@NamLamViec", criteria.NamLamViec);
                SqlParameter sMaDonVo = new SqlParameter("@SMaDonVi", criteria.IDMaDonVi);
                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_qtc_quykinhphi_quanly_chungtu_chitiet_tao_tonghop @ListIdChungTuTongHop,@Nguoitao ,@IdChungTu, @NamLamViec,@SMaDonVi",
                    listIdChungTuTongHop, nguoiTao, idChungTu, namLamViec,sMaDonVo);
            }
        }

        public void CreateVoudcherForQuaterBefore(BhQtcQuyKinhPhiQuanLy entity)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_qtcqKPQL_create_datafor_quaterbefore @IdChungTu, @Username, @NamLamViec, @Quy, @MaDonVi";
                var parameters = new[]
                {
                    new SqlParameter("@IdChungTu", entity.Id),
                    new SqlParameter("@Username", entity.SNguoiTao),
                    new SqlParameter("@NamLamViec", entity.INamChungTu),
                    new SqlParameter("@Quy", entity.IQuyChungTu),
                    new SqlParameter("@MaDonVi", entity.IID_MaDonVi),
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public bool ExitChungTuChiTiet(QtcQuyKinhPhiQuanLyCriteria searchCondition)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var lstChungTu = ctx.BhQtcQuyKinhPhiQuanLyChiTiets.Where(x => x.IID_QTC_Quy_KinhPhiQuanLy.Equals(searchCondition.ID) && x.FTienDuToanDuocGiao.GetValueOrDefault(0) != 0).ToList();
                if (lstChungTu.Count > 0)
                    return true;
                return false;
            }
        }

        public BhQtcQuyKinhPhiQuanLyChiTiet FindById(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcQuyKinhPhiQuanLyChiTiets.Find(id);
            }
        }

        public IEnumerable<BhQtcQuyKinhPhiQuanLyChiTiet> FindByIdChiTiet(Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcQuyKinhPhiQuanLyChiTiets.Where(x => x.IID_QTC_Quy_KinhPhiQuanLy == id).ToList();
            }
        }

        public List<BhQtcQuyKinhPhiQuanLyChiTietQuery> FindChungTuChiTiet(QtcQuyKinhPhiQuanLyCriteria searchCondition)
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
                    new SqlParameter("@Loai", searchCondition.LoaiChungTu)
                };

                string executeSql = "EXECUTE dbo.sp_qtc_quykinhphi_quanly_chungtu_chi_tiet @VoucherId, @LNS,@YearOfWork,@AgencyId,@VoucherDate,@iID_LoaiDanhMucChi,@iQuyChungTu,@Loai";
                return ctx.FromSqlRaw<BhQtcQuyKinhPhiQuanLyChiTietQuery>(executeSql, parameters).ToList();
            }
        }

        public List<BhQtcQuyKinhPhiQuanLyChiTietQuery> FindSoTienQuyetToanDaDuocDuyetTheoQuy(QtcQuyKinhPhiQuanLyCriteria searchCondition)
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

                string executeSql = "EXECUTE dbo.sp_qtc_quykinhphi_quanly_gettienquyet_toandaduyet @YearOfWork,@AgencyId,@VoucherDate,@iQuyChungTu";
                return ctx.FromSqlRaw<BhQtcQuyKinhPhiQuanLyChiTietQuery>(executeSql, parameters).ToList();
            }
        }

        public List<ReportBHQTCQKPQuanLyThongTriQuery> GetDataReportKeHoach(QtcQuyKinhPhiQuanLyCriteria searchCondition)
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
                    new SqlParameter("@LoaiTongHop",searchCondition.LoaiChungTu),
                    new SqlParameter("@Dvt", searchCondition.DonViTinh)
                };

                string executeSql = "EXECUTE dbo.sp_rpt_qtc_qkp_ql_chungtu_chi_tiet @NamLamViec,@iQuy,@IdDonVi,@LNS,@UserName,@LoaiTongHop,@Dvt";
                return ctx.FromSqlRaw<ReportBHQTCQKPQuanLyThongTriQuery>(executeSql, parameters).ToList();
            }
        }

        public List<ReportBHQTCQKPQuanLyThongTriQuery> GetDataReportQtcQuyKPQlThongTri1(int yearOfWork, string quy, string donVi, string principal, int iLoaiChungTu, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", yearOfWork),
                    new SqlParameter("@iQuy", quy),
                    new SqlParameter("@IdDonVi", donVi),
                    new SqlParameter("@UserName", principal),
                    new SqlParameter("@iLoaiTongHop", iLoaiChungTu),
                    new SqlParameter("@Dvt", donViTinh)
                };

                string executeSql = "EXECUTE dbo.sp_rpt_qtc_qkp_ql_thongtriloai1_bh @NamLamViec,@iQuy,@IdDonVi,@UserName,@iLoaiTongHop,@Dvt";
                return ctx.FromSqlRaw<ReportBHQTCQKPQuanLyThongTriQuery>(executeSql, parameters).ToList();
            }
        }

        public List<ReportBHQTCQKPQuanLyThongTriQuery> GetDataReportQtcQuyKPQlThongTri2(int yearOfWork, string quy, string donVi, string sLNS, string principal, int iLoaiChungTu, int donViTinh)
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
                    new SqlParameter("@LoaiTongHop",iLoaiChungTu),
                    new SqlParameter("@Dvt", donViTinh)
                };

                string executeSql = "EXECUTE dbo.sp_rpt_qtc_qkp_ql_thongtriloai2_bh @NamLamViec,@iQuy,@IdDonVi,@LNS,@UserName,@LoaiTongHop,@Dvt";
                return ctx.FromSqlRaw<ReportBHQTCQKPQuanLyThongTriQuery>(executeSql, parameters).ToList();
            }
        }

        public List<BhQtcQuyKinhPhiQuanLyChiTietQuery> GetDataTienDuToanPhanBoChi(QtcQuyKinhPhiQuanLyCriteria searchCondition)
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

                };

                string executeSql = "EXECUTE dbo.sp_qtc_quykinhphi_quanly_getTienDuToanDuocGiao_chi_tiet @YearOfWork,@LNS,@AgencyId,@VoucherDate,@iID_LoaiDanhMucChi";
                return ctx.FromSqlRaw<BhQtcQuyKinhPhiQuanLyChiTietQuery>(executeSql, parameters).ToList();
            }
        }
    }
}
