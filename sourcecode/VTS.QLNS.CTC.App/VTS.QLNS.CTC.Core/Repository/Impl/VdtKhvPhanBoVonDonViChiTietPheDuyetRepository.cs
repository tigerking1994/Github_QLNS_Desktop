using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility.Criteria;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtKhvPhanBoVonDonViChiTietPheDuyetRepository : Repository<VdtKhvPhanBoVonDonViChiTietPheDuyet>, IVdtKhvPhanBoVonDonViChiTietPheDuyetRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtKhvPhanBoVonDonViChiTietPheDuyetRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<PhanBoVonDonViChiTietPheDuyetQuery> GetAllDuAnInPhanBoVon(string idPhanBoVonDeXuat, int iNguonVonId)
        {
            try
            {
                using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
                {
                    return ctx.FromSqlRaw<PhanBoVonDonViChiTietPheDuyetQuery>("EXECUTE dbo.sp_vdt_get_duan_in_phanbovon_donvi_pheduyet @idPhanBoVonDeXuat, @nguonVonID",
                        new SqlParameter("@idPhanBoVonDeXuat", idPhanBoVonDeXuat),
                        new SqlParameter("@nguonVonID", iNguonVonId)).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<PhanBoVonDonViChiTietPheDuyetQuery>();
            }
        }

        public List<PhanBoVonDonViChiTietPheDuyetQuery> GetPhanBoVonChiTietByParentId(Guid iIdPhanBoVonChiTiet, DateTime? dNgayQuyetDinh)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_find_phanbovondonvichitietpheduyet @iIdPhanBoVon, @dNgayLap";
                var parameters = new[]
                {
                        new SqlParameter("@iIdPhanBoVon", iIdPhanBoVonChiTiet),
                        new SqlParameter("@dNgayLap", dNgayQuyetDinh)
                    };
                return ctx.FromSqlRaw<PhanBoVonDonViChiTietPheDuyetQuery>(sql, parameters).ToList();
            }
        }
        
        public List<PhanBoVonDonViChiTietPheDuyetQuery> GetPhanBoVonChiTietByParentIdClone(Guid iIdPhanBoVonChiTiet, DateTime? dNgayQuyetDinh)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_find_phanbovondonvichitietpheduyet_clone @iIdPhanBoVon, @dNgayLap";
                var parameters = new[]
                {
                        new SqlParameter("@iIdPhanBoVon", iIdPhanBoVonChiTiet),
                        new SqlParameter("@dNgayLap", dNgayQuyetDinh)
                    };
                return ctx.FromSqlRaw<PhanBoVonDonViChiTietPheDuyetQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<VdtKhvPhanBoVonDonViChiTietPheDuyet> GetPhanBoVonChiTietByIidPhanBoVonID(Guid iIdPhanBoVonId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtKhvPhanBoVonDonViChiTietPheDuyets.Where(n => n.IIdPhanBoVonDonViPheDuyetId == iIdPhanBoVonId).ToList();
            }
        }

        public bool CreatePhanBoVonChiTiet(int iLoaiKeHoach, DataTable dt, string sUserLogin, bool bIsEdit)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var executeQuery = "EXEC sp_vdt_insert_phanbovondonvichitietpheduyet2 @bIsEdit, @sUserLogin, @tbl_PhanBoVonChiTiet, @sTypeError OUT";
                SqlParameter iErrorTypeParam = new SqlParameter("sTypeError", SqlDbType.Int);
                iErrorTypeParam.Direction = ParameterDirection.Output;

                SqlParameter dtDetailParam = new SqlParameter("tbl_PhanBoVonChiTiet", SqlDbType.Structured);
                dtDetailParam.TypeName = "t_tbl_phanbovonchitiet6";
                dtDetailParam.Value = dt;

                var parameters = new[]
                {
                    new SqlParameter("bIsEdit", bIsEdit),
                    new SqlParameter("sUserLogin", sUserLogin),
                    dtDetailParam,
                    iErrorTypeParam
                };
                ctx.Database.ExecuteSqlCommand(executeQuery, parameters);
                return (int)iErrorTypeParam.Value == 0;
            }
        }

        public int RemovePhanBoVonChiTiet(VdtKhvPhanBoVonDonViChiTietPheDuyet data)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                ctx.VdtKhvPhanBoVonDonViChiTietPheDuyets.Remove(data);
                return ctx.SaveChanges();
            }
        }

        public int Update(IEnumerable<VdtKhvPhanBoVonDonViChiTietPheDuyet> datas)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                foreach (var item in datas)
                {
                    ctx.VdtKhvPhanBoVonDonViChiTietPheDuyets.Update(item);
                }
                return ctx.SaveChanges();
            }
        }

        public int RemovePhanBoVonChiTiet(IEnumerable<VdtKhvPhanBoVonDonViChiTietPheDuyet> datas)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                ctx.VdtKhvPhanBoVonDonViChiTietPheDuyets.RemoveRange(datas);
                return ctx.SaveChanges();
            }
        }

        public IEnumerable<PhanBoVonDonViPheDuyetReportQuery> GetPhanBoVonDonViPheDuyetReport(string lstId, string lstLct, int yearPlan, int type, string lstDonVi, double donViTienTe)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<PhanBoVonDonViPheDuyetReportQuery>("EXECUTE dbo.sp_vdt_get_phan_bo_von_don_vi_phe_duyet_report @lstId, @lstLct,@YearPlan, @type, @lstDonVi, @DonViTienTe",
                    new SqlParameter("@lstId", lstId),
                    new SqlParameter("@lstLct", lstLct),
                    new SqlParameter("@YearPlan", yearPlan),
                    new SqlParameter("@type", type),
                    new SqlParameter("@lstDonVi", lstDonVi),
                    new SqlParameter("@DonViTienTe", donViTienTe)).ToList();
            }
        }

        public IEnumerable<long> GetVonBoTri5Nam(string lstId, int yearPlan)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<long>("EXECUTE dbo.sp_vdt_get_von_bo_tri_5_nam @lstId, @YearPlan",
                    new SqlParameter("@lstId", lstId),
                    new SqlParameter("@YearPlan", yearPlan)).ToList();
            }
        }

        public IEnumerable<PhanBoVonDonViPheDuyetDieuChinhReportQuery> GetPhanBoVonDonViPheDuyetDieuChinhReport(string lstId, string lstLct, int yearPlan, int type, string lstDonVi, double donViTienTe)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<PhanBoVonDonViPheDuyetDieuChinhReportQuery>("EXECUTE dbo.sp_vdt_get_phan_bo_von_don_vi_phe_duyet_dieu_chinh_report @lstId, @lstLct,@YearPlan, @type, @lstDonVi, @DonViTienTe",
                    new SqlParameter("@lstId", lstId),
                    new SqlParameter("@lstLct", lstLct),
                    new SqlParameter("@YearPlan", yearPlan),
                    new SqlParameter("@type", type),
                    new SqlParameter("@lstDonVi", lstDonVi),
                    new SqlParameter("@DonViTienTe", donViTienTe)).ToList();
            }
        }

        public IEnumerable<VdtKhvVonNamDonViPheDuyetDuocDuyetQuery> GetKeHoachVonNamDuocDuyetReport(string listId, string lct, int type, int loaiDuAn, string lstDonVi, double donViTinh)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtKhvVonNamDonViPheDuyetDuocDuyetQuery>("EXECUTE sp_vdt_khv_kehoach_von_nam_duoc_duyet_export @lstId, @lct, @type,@loaiDuAn,@lstDonVi,@MenhGiaTienTe",
                                        new SqlParameter("lstId", listId),
                                        new SqlParameter("lct", lct),
                                        new SqlParameter("type", type),
                                        new SqlParameter("loaiDuAn", loaiDuAn),
                                        new SqlParameter("lstDonVi", lstDonVi),
                                        new SqlParameter("MenhGiaTienTe", donViTinh)).ToList();
            }
        }

        public IEnumerable<PhanBoVonDonViPheDuyetDuocDuyetChiTietQuery> GetPhanBoVonChiTietDieuChinhByParentId(Guid idPhanBoVon)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<PhanBoVonDonViPheDuyetDuocDuyetChiTietQuery>("EXECUTE sp_vdt_find_kehoachvonnam_donvi_pheduyet_duocduyet_dieuchinh_chitiet @iIdPhanBoVon",
                    new SqlParameter("iIdPhanBoVon", idPhanBoVon)).ToList();
            }
        }

        public IEnumerable<VdtKhvPhanBoVonDonViChiTietPheDuyet> GetKeHoachVonNamDuocDuyet(YearPlanCriteria condition)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtKhvPhanBoVonDonViChiTietPheDuyets.Where(x => x.IIdDuAnId == condition.IIdDuAn).ToList();
            }
        }

        public IEnumerable<VdtKhvVonNamDonViPheDuyetNguonVonQuery> GetPhanBoVonDonViPheDuyetNguonVon(int type, string lstId, string lstLct, string lstNguonVon, string lstDonVi, double donViTienTe)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtKhvVonNamDonViPheDuyetNguonVonQuery>("EXECUTE dbo.sp_vdt_get_phan_bo_von_don_vi_phe_duyet_nguonvon_report @type, @lstId,@lstLct, @lstNguonVon,@lstDonVi, @DonViTienTe",
                    new SqlParameter("@type", type),
                    new SqlParameter("@lstId", lstId),
                    new SqlParameter("@lstLct", lstLct),
                    new SqlParameter("@lstNguonVon", lstNguonVon),
                    new SqlParameter("@lstDonVi", lstDonVi),
                    new SqlParameter("@DonViTienTe", donViTienTe)).ToList();
            }
        }

        public IEnumerable<VdtKhvVonNamDonViPheDuyetNguonVonDieuChinhQuery> GetPhanBoVonDonViPheDuyetNguonVonDieuChinh(int type, string lstId, string lstLct, string lstNguonVon, string lstDonVi, double donViTienTe)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtKhvVonNamDonViPheDuyetNguonVonDieuChinhQuery>("EXECUTE dbo.sp_vdt_get_phan_bo_von_don_vi_phe_duyet_nguonvon_dieu_chinh_report @type, @lstId,@lstLct, @lstNguonVon, @lstDonVi, @DonViTienTe",
                    new SqlParameter("@type", type),
                    new SqlParameter("@lstId", lstId),
                    new SqlParameter("@lstLct", lstLct),
                    new SqlParameter("@lstNguonVon", lstNguonVon),
                    new SqlParameter("@lstDonVi", lstDonVi),
                    new SqlParameter("@DonViTienTe", donViTienTe)).ToList();
            }
        }
    }
}
