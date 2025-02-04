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
    public class VdtKhvPhanBoVonDonViChiTietRepository : Repository<VdtKhvPhanBoVonDonViChiTiet>, IVdtKhvPhanBoVonDonViChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtKhvPhanBoVonDonViChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<VdtKhvPhanBoVonDonViChiTiet> GetPhanBoVonDonViChiTietByIidPhanBoVonID(Guid iIdParent)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtKhvPhanBoVonDonViChiTiets.Where(n => n.IIdPhanBoVonDonVi == iIdParent).ToList();
            }
        }

        public IEnumerable<VdtKhvPhanBoVonDonViChiTietQuery> GetAllDuAnInPhanBoVon(int iNamKeHoach, DateTime dNgayLap, string iIdMaDonViQuanLyId, int iNguonVonId, int? filterHasQDDT)
        {
            try
            {
                SqlParameter filterhasQDDTParam = new SqlParameter();
                filterhasQDDTParam.ParameterName = "@filterHasQDDT";
                if (filterHasQDDT == -1)
                {
                    filterhasQDDTParam.Value = DBNull.Value;
                }
                else
                {
                    filterhasQDDTParam.Value = filterHasQDDT;
                }
                using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
                {
                    //return ctx.FromSqlRaw<VdtKhvPhanBoVonDonViChiTietQuery>("EXECUTE dbo.sp_vdt_get_duan_in_phanbovon_donvi_in_kehoachtrunghan @iNamKeHoach, @ngayLap, @maDonViQuanLyId, @nguonVonID, @filterHasQDDT",
                    return ctx.FromSqlRaw<VdtKhvPhanBoVonDonViChiTietQuery>("EXECUTE dbo.sp_vdt_get_duan_in_phanbovon_donvi_in_kehoachtrunghan @iNamKeHoach, @maDonViQuanLyId, @nguonVonID, @filterHasQDDT",
                    new SqlParameter("@iNamKeHoach", iNamKeHoach),
                    //new SqlParameter("@ngayLap", dNgayLap),
                    new SqlParameter("@maDonViQuanLyId", iIdMaDonViQuanLyId),
                    new SqlParameter("@nguonVonID", iNguonVonId),
                    filterhasQDDTParam
                    ).ToList();                    
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<ExportVonNamDonViQuery> GetKeHoachVonNamDonViExport(List<YearPlanManagerExportCriteria> lstPhanboVon)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var executeQuery = "EXECUTE dbo.sp_export_kehoachvonnam_donvi @Ids";
                DataTable dt = DBExtension.ConvertDataToTableDefined("t_tbl_uniqueidentifier", lstPhanboVon);
                var parameters = new[]
                {
                    new SqlParameter("@Ids", dt.AsTableValuedParameter("t_tbl_uniqueidentifier"))
                };
                return ctx.FromSqlRaw<ExportVonNamDonViQuery>(executeQuery, parameters).ToList();
            }
        }

        public IEnumerable<VdtKhvVonNamDonViReportQuery> GetReportKeHoachVonNamDonVi(int type, string theLoaiCongTrinh, string lstId, string lstLct, double donViTinh)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtKhvVonNamDonViReportQuery>("EXECUTE dbo.sp_export_baocao_kehoachvonnam_donvi @type,@theLoaiCongTrinh,@lstId,@lstLct,@DonViTienTe",
                    new SqlParameter("type", type),
                    new SqlParameter("theLoaiCongTrinh", theLoaiCongTrinh),
                    new SqlParameter("lstId", lstId),
                    new SqlParameter("lstLct", lstLct),
                    new SqlParameter("DonViTienTe", donViTinh)).ToList();
            }
        }

        public IEnumerable<VdtKhvPhanBoVonDonViChiTietQuery> GetPhanBoVonChiTietByParentId(Guid iIdParent)
        {
            try
            {
                using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
                {
                    return ctx.FromSqlRaw<VdtKhvPhanBoVonDonViChiTietQuery>("EXECUTE dbo.sp_vdt_find_phanbovondonvichitiet @iIdPhanBoVon",
                    new SqlParameter("@iIdPhanBoVon", iIdParent)).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public int RemovePhanBoVonChiTiet(IEnumerable<VdtKhvPhanBoVonDonViChiTiet> datas)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                ctx.VdtKhvPhanBoVonDonViChiTiets.RemoveRange(datas);
                return ctx.SaveChanges();
            }
        }

        public IEnumerable<VdtKhvPhanBoVonDonViChiTietDieuChinhQuery> GetPhanBoVonChiTietDieuChinhByParentId(Guid iIdParent)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtKhvPhanBoVonDonViChiTietDieuChinhQuery>("EXECUTE dbo.sp_vdt_find_phanbovondonvichitiet_dieuchinh @iIdPhanBoVon",
                new SqlParameter("@iIdPhanBoVon", iIdParent)).ToList();
            }
        }

        public IEnumerable<PhanBoVonDonViDieuChinhReportQuery> GetPhanBoVonDieuChinhReport(string lstId, string lstLct, int yearPlan, int type, double donViTienTe)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<PhanBoVonDonViDieuChinhReportQuery>("EXECUTE dbo.sp_vdt_get_phan_bo_von_don_vi_dieu_chinh_report @lstId, @lstLct,@YearPlan, @type, @DonViTienTe",
                new SqlParameter("@lstId", lstId),
                new SqlParameter("@lstLct", lstLct),
                new SqlParameter("@YearPlan", yearPlan),
                new SqlParameter("@type", type),
                new SqlParameter("@DonViTienTe", donViTienTe)).ToList();
            }
        }

        public IEnumerable<PhanBoVonDonViGocReportQuery> GetPhanBoVonDonViGocReport(string lstId, string lstLct, int yearPlan, int type, double donViTienTe)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<PhanBoVonDonViGocReportQuery>("EXECUTE dbo.sp_vdt_get_phan_bo_von_don_vi_goc_report @lstId, @lstLct,@YearPlan, @type, @DonViTienTe",
                    new SqlParameter("@lstId", lstId),
                    new SqlParameter("@lstLct", lstLct),
                    new SqlParameter("@YearPlan", yearPlan),
                    new SqlParameter("@type", type),
                    new SqlParameter("@DonViTienTe", donViTienTe)).ToList();
            }
        }

        public IEnumerable<VdtKhvPhanBoVonDonViChiTiet> GetPhanBoVonChiTietByIdDuAn(Guid idDuAn)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtKhvPhanBoVonDonViChiTiets.Where(n => n.IIdDuAnId == idDuAn).ToList();
            }
        }

        public IEnumerable<VdtKhvPhanBoVonDonViChiTiet> GetPhanBoVonDonViByIdPhanBoVon(Guid idPhanBoVon)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtKhvPhanBoVonDonViChiTiets.Where(x => x.IIdPhanBoVonDonVi == idPhanBoVon).ToList();
            }
        }

        public IEnumerable<VdtKhvVonNamDeXuatDieuChinhNguonVonQuery> GetPhanBoVonDieuChinhNguonVon(int type, string lstId, string lstLct, string lstNguonVon, double donViTienTe)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtKhvVonNamDeXuatDieuChinhNguonVonQuery>("EXECUTE dbo.sp_vdt_get_phan_bo_von_don_vi_dieu_chinh_nguonvon_report @type, @lstId,@lstLct, @lstNguonVon,@DonViTienTe",
                new SqlParameter("@type", type),
                new SqlParameter("@lstId", lstId),
                new SqlParameter("@lstLct", lstLct),
                new SqlParameter("@lstNguonVon", lstNguonVon),
                new SqlParameter("@DonViTienTe", donViTienTe)).ToList();
            }
        }

        public IEnumerable<VdtKhvVonNamDeXuatGocNguonVonQuery> GetPhanBoVonDonViGocNguonVon(int type, string lstId, string lstLct, string lstNguonVon, double donViTienTe)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtKhvVonNamDeXuatGocNguonVonQuery>("EXECUTE dbo.sp_vdt_get_phan_bo_von_don_vi_goc_nguonvon_report @type, @lstId,@lstLct, @lstNguonVon,@DonViTienTe",
                    new SqlParameter("@type", type),
                    new SqlParameter("@lstId", lstId),
                    new SqlParameter("@lstLct", lstLct),
                    new SqlParameter("@lstNguonVon", lstNguonVon),
                    new SqlParameter("@DonViTienTe", donViTienTe)).ToList();
            }
        }

        public IEnumerable<PhanBoVonDonViQuery> GetPhanBoVonDonViDieuChinh(string idPhanBoVonDv)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<PhanBoVonDonViQuery>("EXECUTE dbo.sp_vdt_get_phan_bo_von_dieuchinh @idPhanBoVonDonVi",
                new SqlParameter("@idPhanBoVonDonVi", idPhanBoVonDv)).ToList();
            }
        }

        public IEnumerable<KeHoachVonDauTuTrungHan5NamQuery> GetVonBoTri5Nam(string lstId, int yearPlan)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<KeHoachVonDauTuTrungHan5NamQuery>("EXECUTE dbo.sp_vdt_get_von_bo_tri_5_nam @lstId, @YearPlan",
                    new SqlParameter("@lstId", lstId),
                    new SqlParameter("@YearPlan", yearPlan)).ToList();
            }
        }
    }
}
