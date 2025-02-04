using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using Dapper;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtKhvPhanBoVonRepository : Repository<VdtKhvPhanBoVon>, IVdtKhvPhanBoVonRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtKhvPhanBoVonRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<PhanBoVonQuery> GetDataPhanBoVonInIndexView(int iLoaiKeHoachVon)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<PhanBoVonQuery>("EXECUTE dbo.sp_vdt_phanbovon_index @iLoaiKeHoachVon",
                new SqlParameter("@iLoaiKeHoachVon", iLoaiKeHoachVon)).ToList();
            }
        }

        public int RemovePhanBoVon(VdtKhvPhanBoVon data)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                ctx.VdtKhvPhanBoVons.Remove(data);
                return ctx.SaveChanges();
            }
        }

        public bool ExistPhanBoVonByNamKeHoachAndDonViQuanLy(VdtKhvPhanBoVon objPhanBoVon, int iLoai)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtKhvPhanBoVons.Any(n => n.IIDMaDonViQuanLy == objPhanBoVon.IIDMaDonViQuanLy
                    && n.INamKeHoach == objPhanBoVon.INamKeHoach
                    && n.ILoai == iLoai
                    && n.IIdNguonVonId == objPhanBoVon.IIdNguonVonId
                    && n.IIdLoaiNguonVonId == objPhanBoVon.IIdLoaiNguonVonId
                    && !n.DDateDelete.HasValue && string.IsNullOrEmpty(n.SUserDelete));
            }
        }

        public bool ExistPhanBoVonBySoQuyetDinhAndDonVi(VdtKhvPhanBoVon objPhanBoVon, int iLoai)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtKhvPhanBoVons.Any(n => n.IIDMaDonViQuanLy == objPhanBoVon.IIDMaDonViQuanLy
                    && n.ILoai == objPhanBoVon.ILoai
                    && n.SSoQuyetDinh == objPhanBoVon.SSoQuyetDinh
                    && (objPhanBoVon.Id == Guid.Empty || n.Id != objPhanBoVon.Id));
            }
        }

        public List<RptAnnualBudgetAllocationQuery> GetDataRptAnnualBudgetAllocation(int iNamKeHoach, DateTime dDenNgay, int iIdnguonVon, string sUserLogin)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<RptAnnualBudgetAllocationQuery>("EXECUTE dbo.sp_TongHopTinhHinhThucHienNganSachNam_DonViQuanLyID @iNamKeHoach, @dDenNgay, @iIdNguonVonId, @sUserLogin",
                    new SqlParameter("@iNamKeHoach", iNamKeHoach),
                    new SqlParameter("@dDenNgay", dDenNgay),
                    new SqlParameter("@iIdNguonVonId", iIdnguonVon),
                    new SqlParameter("@sUserLogin", sUserLogin)).ToList();
            }
        }

        public List<KeHoachVonQuery> GetKeHoachVonCapPhatThanhToan(string duAnId, int nguonVonId, DateTime dNgayDeNghi, int namKeHoach, int iCoQuanThanhToan, Guid iIdPheDuyet)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<KeHoachVonQuery>("EXECUTE dbo.sp_vdt_get_kehoachvon_capphatthanhtoan @DuAnId, @NguonVonId, @dNgayDeNghi, @NamKeHoach, @iCoQuanThanhToan, @iIdPheDuyet",
                    new SqlParameter("@DuAnId", duAnId),
                    new SqlParameter("@NguonVonId", nguonVonId),
                    new SqlParameter("@dNgayDeNghi", dNgayDeNghi),
                    new SqlParameter("@NamKeHoach", namKeHoach),
                    new SqlParameter("@iCoQuanThanhToan", iCoQuanThanhToan),
                    new SqlParameter("@iIdPheDuyet", iIdPheDuyet)).ToList();
            }
        }

        public List<KeHoachVonQuery> GetDeNghiTamUngCapPhatThanhToan(string duAnId, int nguonVonId, DateTime dNgayDeNghi, int namKeHoach, int iCoQuanThanhToan, Guid iIdPheDuyet)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<KeHoachVonQuery>("EXECUTE dbo.sp_vdt_get_denghitamung_capphatthanhtoan @DuAnId, @NguonVonId, @dNgayDeNghi, @NamKeHoach, @iCoQuanThanhToan, @iIdPheDuyet",
                    new SqlParameter("@DuAnId", duAnId),
                    new SqlParameter("@NguonVonId", nguonVonId),
                    new SqlParameter("@dNgayDeNghi", dNgayDeNghi),
                    new SqlParameter("@NamKeHoach", namKeHoach),
                    new SqlParameter("@iCoQuanThanhToan", iCoQuanThanhToan),
                    new SqlParameter("@iIdPheDuyet", iIdPheDuyet)).ToList();
            }
        }

        public IEnumerable<VdtKhvPhanBoVonChiTiet> GetPhanBoVonByIdPhanBoVon(Guid idPhanBoVon)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtKhvPhanBoVonChiTiets.Where(x => x.IIdPhanBoVonId == idPhanBoVon).ToList();
            }
        }

        public IEnumerable<ChungTuThanhToanQuery> GetKeHoachVonByThanhToanUngIds(List<Guid> lstid)
        {

            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var executeQuery = "EXECUTE dbo.sp_vdt_get_kehoachvon_by_tamung_thanhtoan @lstId";
                DataTable dt = DBExtension.ConvertDataToGuidTable(lstid);
                var parameters = new[]
                {
                    new SqlParameter("@lstId", dt.AsTableValuedParameter("t_tbl_uniqueidentifier"))
                };
                return ctx.FromSqlRaw<ChungTuThanhToanQuery>(executeQuery, parameters).ToList();
            }
        }

        public IEnumerable<BcDuToanTheoLoaiCongTrinhQuery> GetBcDuToanTheoLoaiCongTrinh(int iLoaiChungTu, int iNamKeHoach, double fDonViTinh, List<string> lstDonViId)
        {

            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var executeQuery = "EXECUTE dbo.sp_vdt_bcdutoan_loaicongtrinh @iLoaiChungTu, @iNamKeHoach, @fDonViTinh, @DonViIds";
                DataTable dt = DBExtension.ConvertDataToStringTable(lstDonViId);
                var parameters = new[]
                {
                    new SqlParameter("@iLoaiChungTu",iLoaiChungTu),
                    new SqlParameter("@iNamKeHoach",iNamKeHoach),
                    new SqlParameter("@fDonViTinh",fDonViTinh),
                    new SqlParameter("@DonViIds", dt.AsTableValuedParameter("t_tbl_string"))
                };
                return ctx.FromSqlRaw<BcDuToanTheoLoaiCongTrinhQuery>(executeQuery, parameters).ToList();
            }
        }
    }
}
