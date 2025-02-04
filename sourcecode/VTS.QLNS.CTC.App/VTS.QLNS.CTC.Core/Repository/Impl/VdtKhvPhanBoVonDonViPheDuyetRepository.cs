using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtKhvPhanBoVonDonViPheDuyetRepository : Repository<VdtKhvPhanBoVonDonViPheDuyet>, IVdtKhvPhanBoVonDonViPheDuyetRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtKhvPhanBoVonDonViPheDuyetRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<PhanBoVonDonViPheDuyetQuery> GetDataPhanBoVonInIndexView(int iLoaiKeHoachVon)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<PhanBoVonDonViPheDuyetQuery>("EXECUTE dbo.sp_vdt_phanbovon_donvi_pheduyet_index @iLoaiKeHoachVon",
                new SqlParameter("@iLoaiKeHoachVon", iLoaiKeHoachVon)).ToList();
            }
        }

        public int RemovePhanBoVon(VdtKhvPhanBoVonDonViPheDuyet data)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                ctx.VdtKhvPhanBoVonDonViPheDuyets.Remove(data);
                return ctx.SaveChanges();
            }
        }

        public bool ExistPhanBoVonByNamKeHoachAndDonViQuanLy(VdtKhvPhanBoVonDonViPheDuyet objPhanBoVon, int iLoai)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtKhvPhanBoVonDonViPheDuyets.Any(n => n.IIdMaDonViQuanLy == objPhanBoVon.IIdMaDonViQuanLy
                    && n.INamKeHoach == objPhanBoVon.INamKeHoach
                    && n.ILoai == iLoai
                    && n.IIdNguonVonId == objPhanBoVon.IIdNguonVonId
                    && n.IIdLoaiNguonVonId == objPhanBoVon.IIdLoaiNguonVonId
                    && !n.DDateDelete.HasValue && string.IsNullOrEmpty(n.SUserDelete));
            }
        }

        public bool ExistPhanBoVonBySoQuyetDinhAndDonVi(VdtKhvPhanBoVonDonViPheDuyet objPhanBoVon, int iLoai)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtKhvPhanBoVonDonViPheDuyets.Any(n => n.IIdMaDonViQuanLy == objPhanBoVon.IIdMaDonViQuanLy
                    && n.ILoai == objPhanBoVon.ILoai
                    && n.SSoQuyetDinh == objPhanBoVon.SSoQuyetDinh
                    && (objPhanBoVon.Id == Guid.Empty || n.Id != objPhanBoVon.Id));
            }
        }

        //public List<RptAnnualBudgetAllocationQuery> GetDataRptAnnualBudgetAllocation(int iNamKeHoach, DateTime dDenNgay, int iIdnguonVon, string sUserLogin)
        //{
        //    using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
        //    {
        //        return ctx.FromSqlRaw<RptAnnualBudgetAllocationQuery>("EXECUTE dbo.sp_TongHopTinhHinhThucHienNganSachNam_DonViQuanLyID @iNamKeHoach, @dDenNgay, @iIdNguonVonId, @sUserLogin",
        //            new SqlParameter("@iNamKeHoach", iNamKeHoach),
        //            new SqlParameter("@dDenNgay", dDenNgay),
        //            new SqlParameter("@iIdNguonVonId", iIdnguonVon),
        //            new SqlParameter("@sUserLogin", sUserLogin)).ToList();
        //    }
        //}

        //public List<KeHoachVonQuery> GetKeHoachVonCapPhatThanhToan(string duAnId, int nguonVonId, DateTime dNgayDeNghi, int namKeHoach, int iCoQuanThanhToan, Guid iIdPheDuyet)
        //{
        //    using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
        //    {
        //        return ctx.FromSqlRaw<KeHoachVonQuery>("EXECUTE dbo.sp_vdt_get_kehoachvon_capphatthanhtoan @DuAnId, @NguonVonId, @dNgayDeNghi, @NamKeHoach, @iCoQuanThanhToan, @iIdPheDuyet",
        //            new SqlParameter("@DuAnId", duAnId),
        //            new SqlParameter("@NguonVonId", nguonVonId),
        //            new SqlParameter("@dNgayDeNghi", dNgayDeNghi),
        //            new SqlParameter("@NamKeHoach", namKeHoach),
        //            new SqlParameter("@iCoQuanThanhToan", iCoQuanThanhToan),
        //            new SqlParameter("@iIdPheDuyet", iIdPheDuyet)).ToList();
        //    }
        //}

        //public List<KeHoachVonQuery> GetDeNghiTamUngCapPhatThanhToan(string duAnId, int nguonVonId, DateTime dNgayDeNghi, int namKeHoach, int iCoQuanThanhToan, Guid iIdPheDuyet)
        //{
        //    using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
        //    {
        //        return ctx.FromSqlRaw<KeHoachVonQuery>("EXECUTE dbo.sp_vdt_get_denghitamung_capphatthanhtoan @DuAnId, @NguonVonId, @dNgayDeNghi, @NamKeHoach, @iCoQuanThanhToan, @iIdPheDuyet",
        //            new SqlParameter("@DuAnId", duAnId),
        //            new SqlParameter("@NguonVonId", nguonVonId),
        //            new SqlParameter("@dNgayDeNghi", dNgayDeNghi),
        //            new SqlParameter("@NamKeHoach", namKeHoach),
        //            new SqlParameter("@iCoQuanThanhToan", iCoQuanThanhToan),
        //            new SqlParameter("@iIdPheDuyet", iIdPheDuyet)).ToList();
        //    }
        //}

        public IEnumerable<VdtKhvPhanBoVonDonViChiTietPheDuyet> GetPhanBoVonByIdPhanBoVon(Guid idPhanBoVon)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtKhvPhanBoVonDonViChiTietPheDuyets.Where(x => x.IIdPhanBoVonDonViPheDuyetId == idPhanBoVon).ToList();
            }
        }

        //public IEnumerable<ChungTuThanhToanQuery> GetKeHoachVonByThanhToanUngIds(List<Guid> lstid)
        //{

        //    using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
        //    {
        //        var executeQuery = "EXECUTE dbo.sp_vdt_get_kehoachvon_by_tamung_thanhtoan @lstId";
        //        DataTable dt = DBExtension.ConvertDataToGuidTable(lstid);
        //        var parameters = new[]
        //        {
        //            new SqlParameter("@lstId", dt.AsTableValuedParameter("t_tbl_uniqueidentifier"))
        //        };
        //        return ctx.FromSqlRaw<ChungTuThanhToanQuery>(executeQuery, parameters).ToList();
        //    }
        //}
    }
}
