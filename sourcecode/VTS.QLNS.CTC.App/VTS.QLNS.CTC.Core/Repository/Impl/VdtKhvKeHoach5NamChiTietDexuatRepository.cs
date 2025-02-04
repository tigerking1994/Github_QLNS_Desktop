using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtKhvKeHoach5NamChiTietDeXuatRepository : Repository<VdtKhvKeHoach5NamDeXuatChiTiet>, IVdtKhvKeHoach5NamDeXuatChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtKhvKeHoach5NamChiTietDeXuatRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public VdtKhvKeHoach5NamDeXuatChiTiet FindById(Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtKhvKeHoach5NamDeXuatChiTiets.Where(x => x.Id == id).FirstOrDefault();
            }
        }

        public IEnumerable<VdtKhvKeHoach5NamDeXuatExportQuery> GetDataExportKeHoachTrungHanDeXuat(Guid iID)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtKhvKeHoach5NamDeXuatExportQuery>("EXECUTE dbo.sp_vdt_kehoachtrunghan_dexuat_export @iId", new SqlParameter("@iId", iID)).ToList();
            }
        }

        public IEnumerable<VdtKhvKeHoach5NamDeXuatChiTiet> FindByLevel(int level, Guid id, Guid? idParent)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtKhvKeHoach5NamDeXuatChiTiets.Where(x => x.Level == level && x.IIdKeHoach5NamId == id && x.IdParent == idParent).ToList();
            }
        }

        public int FindNextSoChungTuIndex(Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var currentIndex = ctx.VdtKhvKeHoach5NamDeXuatChiTiets.Where(x => x.IIdKeHoach5NamId == id)
                    .Max(x => x.IndexCode);
                return currentIndex != null ? (int)currentIndex + 1 : 1;
            }
        }

        public IEnumerable<VdtKhvKeHoach5NamDeXuatChiTiet> FindBySMaOrder(string sMaOrder)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtKhvKeHoach5NamDeXuatChiTiets.Where(x => x.SMaOrder == sMaOrder).ToList();
            }
        }

        public int FindByMaxStt(int level, Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var currentIndex = ctx.VdtKhvKeHoach5NamDeXuatChiTiets.Where(x => x.Level == level && x.IIdKeHoach5NamId == id)
              .Max(x => x.STT);

                return !string.IsNullOrEmpty(currentIndex) ? int.Parse(currentIndex) + 1 : 1;
            }
        }

        public IEnumerable<DuAnHangMucQuery> FindListDuAnHangMuc(string lstId, string listIdDuAn)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<DuAnHangMucQuery>("EXECUTE dbo.sp_vdt_du_an_hang_muc @lstId,@lstIdDuAn", new SqlParameter("@lstId", lstId), new SqlParameter("@lstIdDuAn", listIdDuAn)).ToList();
            }
        }

        public IEnumerable<DuAnNguonVonQuery> FindListNguonVon(string lstId, string lstIdDuAn)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<DuAnNguonVonQuery>("EXECUTE dbo.sp_vdt_du_an_nguon_von @lstId,@lstIdDuAn", new SqlParameter("@lstId", lstId), new SqlParameter("@lstIdDuAn", lstIdDuAn)).ToList();
            }
        }

        public IEnumerable<DuAnQuery> FindListDuAn(string lstId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<DuAnQuery>("EXECUTE dbo.sp_vdt_du_an_by_listId @lstId", new SqlParameter("@lstId", lstId)).ToList();
            }
        }

        public IEnumerable<VdtKhvKeHoach5NamDeXuatReportQuery> FindByReportKeHoachTrungHanDeXuat(string id, string lct, string lstNguonVon, int type, double menhGiaTienTe, int iNamLamViec)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_khv_kehoach_5_nam_de_xuat_export @Id, @lct, @IdNguonVon, @type, @MenhGiaTienTe, @iNamLamViec";
                var parameters = new[]
                {
                    new SqlParameter("@Id", id),
                    new SqlParameter("@lct", lct),
                    new SqlParameter("@IdNguonVon", lstNguonVon),
                    new SqlParameter("@type", type),
                    new SqlParameter("@MenhGiaTienTe", menhGiaTienTe),
                    new SqlParameter("@iNamLamViec", iNamLamViec)
                };
                return ctx.FromSqlRaw<VdtKhvKeHoach5NamDeXuatReportQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<VdtKhvKeHoach5NamDeXuatChiTiet> FindByIdKeHoach5Nam(Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtKhvKeHoach5NamDeXuatChiTiets.Where(x => x.IIdKeHoach5NamId == id).ToList();
            }
        }

        public IEnumerable<VdtKhvKeHoach5NamDeXuatChiTietQuery> FindConditionIndex(string voucherId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_kehoach5nam_dexuat_chitiet @VoucherId";
                var parameters = new[]
                {
                    new SqlParameter("@VoucherId", voucherId)
                };
                return ctx.FromSqlRaw<VdtKhvKeHoach5NamDeXuatChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<VdtKhvKeHoach5NamDeXuatChiTietQuery> FindConditionModifiedIndex(string voucherId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_kehoach5nam_dexuat_chitiet_dieuchinh @VoucherId";
                var parameters = new[]
                {
                    new SqlParameter("@VoucherId", voucherId)
                };
                return ctx.FromSqlRaw<VdtKhvKeHoach5NamDeXuatChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<VdtKhvKeHoach5NamDeXuatChiTietQuery> FindListVoucherDetailsModified(Guid idKhth)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_khv_khth_list_dexuat_chitiet @VoucherId";
                var parameters = new[]
                {
                    new SqlParameter("@VoucherId", idKhth.ToString())
                };
                return ctx.FromSqlRaw<VdtKhvKeHoach5NamDeXuatChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<VdtKhvKeHoach5NamDeXuatChiTietQuery> FindListKH5NamDeXuatDieuChinhChiTiet(Guid idKhth)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_khv_khth_list_dexuat_dieuchinh_chitiet @VoucherId";
                var parameters = new[]
                {
                    new SqlParameter("@VoucherId", idKhth.ToString())
                };
                return ctx.FromSqlRaw<VdtKhvKeHoach5NamDeXuatChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<VdtKhvKeHoach5NamDeXuatDieuChinhReportQuery> FindSuggestionReport(int type, string lstId, string lstDonVi, double menhGiaTienTe, string lstNgVon)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_khv_khth_dexuat_dieuchinh_report @type, @VoucherId, @lstDonVi, @MenhGiaTienTe, @IdNguonVon";
                var parameters = new[]
                {
                    new SqlParameter("@type", type),
                    new SqlParameter("@VoucherId", lstId),
                    new SqlParameter("@lstDonVi", lstDonVi),
                    new SqlParameter("@MenhGiaTienTe", menhGiaTienTe),
                    new SqlParameter("@IdNguonVon", lstNgVon)
                };
                return ctx.FromSqlRaw<VdtKhvKeHoach5NamDeXuatDieuChinhReportQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<VdtKhvKeHoach5NamDeXuatChiTiet> FindByListId(List<Guid> lstId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtKhvKeHoach5NamDeXuatChiTiets.Where(x => lstId.Contains(x.Id)).ToList();
            }
        }

        public IEnumerable<VdtKhvKeHoach5NamDeXuatChiTietQuery> FindChiTietDuAnChuyenTiep(Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtKhvKeHoach5NamDeXuatChiTietQuery>("EXECUTE dbo.sp_vdt_kehoach5nam_dexuat_chitiet_chuyentiep @id",
                                                                    new SqlParameter("@id", id.ToString())).ToList();
            }
        }

        public IEnumerable<VdtKhvKeHoach5NamDeXuatReportQuery> FindByReportKeHoachTrungHanDeXuatChuyenTiep(string lstId, string lstBudget, string lstLoaiCongTrinh, string lstUnit, int type, double donViTinh)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtKhvKeHoach5NamDeXuatReportQuery>("EXECUTE dbo.sp_vdt_kehoach5nam_dexuat_chitiet_chuyentiep_report @lstId, @lstBudget, @lstLoaiCongTrinh, @lstUnit, @type, @DonViTienTe",
                                                                    new SqlParameter("@lstId", lstId),
                                                                    new SqlParameter("@lstBudget", lstBudget),
                                                                    new SqlParameter("@lstLoaiCongTrinh", lstLoaiCongTrinh),
                                                                    new SqlParameter("@lstUnit", lstUnit),
                                                                    new SqlParameter("@type", type),
                                                                    new SqlParameter("@DonViTienTe", donViTinh)).ToList();
            }
        }

        public IEnumerable<DuAnTrungHanDeXuatQuery> FindAllDuAnChuyenTiep(string idDonVi)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<DuAnTrungHanDeXuatQuery>("EXECUTE dbo.sp_get_all_du_an_kehoachtrunghan_dexuat_chuyentiep @IdDonVi",
                                                                    new SqlParameter("@IdDonVi", idDonVi.ToString())).ToList();
            }
        }

        public IEnumerable<DuAnTrungHanDeXuatQuery> FindAllDuAnChuyenTiepDieuChinh(string iIdDonVi)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var executeQuery = "EXECUTE dbo.sp_get_all_du_an_kehoachtrunghan_dexuat_chuyentiep_dieuchinh @IdDonVi";
                var parameters = new[]
                {
                    new SqlParameter("@IdDonVi", iIdDonVi)
                };
                return ctx.FromSqlRaw<DuAnTrungHanDeXuatQuery>(executeQuery, parameters).ToList();
            }
        }
    }
}
