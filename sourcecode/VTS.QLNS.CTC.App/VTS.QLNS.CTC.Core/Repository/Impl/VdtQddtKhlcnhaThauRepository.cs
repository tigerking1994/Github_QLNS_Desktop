using VTS.QLNS.CTC.Core.Extensions;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility.Enum;
using Dapper;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtQddtKhlcnhaThauRepository : Repository<VdtQddtKhlcnhaThau>, IVdtQddtKhlcnhaThauRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtQddtKhlcnhaThauRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<KHLCNhaThauQuery> GetDataIndex()
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<KHLCNhaThauQuery>("EXECUTE dbo.sp_qddt_khlcnhathau_index").ToList();
            }
        }

        public IEnumerable<KHLCNhaThauQuery> GetKHLCNhaThauByIdDuAn(Guid idDuAn)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<KHLCNhaThauQuery>("EXECUTE dbo.sp_get_khluachonnhathau_byduanid @iId",
                    new SqlParameter("@iId", idDuAn)).ToList();
    }
}


public IEnumerable<ChiPhiHangMucQuery> GetKHLCNTDetailByListGoiThau(List<Guid> iIdGoiThaus, Guid iIdDuToan)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var executeQuery = "EXECUTE dbo.sp_khluachonnhathau_get_listgoithau_detail @iIdDuToan, @iIdGoiThauIds";
                DataTable dt = DBExtension.ConvertDataToGuidTable(iIdGoiThaus);
                var parameters = new[]
                {
                    new SqlParameter("@iIdDuToan", iIdDuToan),
                    new SqlParameter("@iIdGoiThauIds", dt.AsTableValuedParameter("t_tbl_uniqueidentifier"))
                };
                return ctx.FromSqlRaw<ChiPhiHangMucQuery>(executeQuery, parameters).ToList();
            }
        }

        public IEnumerable<ChiPhiHangMucQuery> GetKHLCNTDetailQDDauTuByListGoiThau(List<Guid> iIdGoiThaus, Guid iIdQDDauTu)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var executeQuery = "EXECUTE dbo.sp_khluachonnhathau_get_listgoithau_by_qddautu_detail @iIdQDDauTu, @iIdGoiThauIds";
                DataTable dt = DBExtension.ConvertDataToGuidTable(iIdGoiThaus);
                var parameters = new[]
                {
                    new SqlParameter("@iIdQDDauTu", iIdQDDauTu),
                    new SqlParameter("@iIdGoiThauIds", dt.AsTableValuedParameter("t_tbl_uniqueidentifier"))
                };
                return ctx.FromSqlRaw<ChiPhiHangMucQuery>(executeQuery, parameters).ToList();
            }
        }

        public IEnumerable<VdtDaDuAn> GetDuAnByDonViQuanLy(string iIdMaDonViQuanLy)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.Set<VdtDaDuAn>().FromSql("EXECUTE dbo.sp_get_duan_in_khluachonnhathau @iIdMaDonVi",
                    new SqlParameter("@iIdMaDonVi", iIdMaDonViQuanLy)).ToList();
            }
        }

        public IEnumerable<KHLCNhaThauDetailQuery> GetGoiThauByParentid(Guid iIdParentid)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<KHLCNhaThauDetailQuery>("EXECUTE dbo.sp_get_goithau_khluachonnhathau @iId",
                    new SqlParameter("@iId", iIdParentid)).ToList();
            }
        }

        public IEnumerable<ChiPhiHangMucQuery> GetChiPhiHangMucByDuAn(Guid iIdDuAn)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<ChiPhiHangMucQuery>("EXECUTE dbo.sp_khluachonnhathau_get_chiphihangmuc @iIDDuAnId",
                    new SqlParameter("@iIDDuAnId", iIdDuAn)).ToList();
            }
        }

        public IEnumerable<ChiPhiHangMucQuery> GetChiPhiHangMucByGoiThau(Guid iIdGoiThau, Guid iIdDuToanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<ChiPhiHangMucQuery>("EXECUTE dbo.sp_khluachonnhathau_get_goithau_detail @iIdGoiThauId, @iIdDuToanId",
                    new SqlParameter("@iIdGoiThauId", iIdGoiThau),
                    new SqlParameter("@iIdDuToanId", iIdDuToanId)).ToList();
            }

        }

        public IEnumerable<ChiPhiHangMucQuery> GetChiPhiHangMucGoiThauByQDDauTu(Guid iIdGoiThau, Guid iIdQDDauTuId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<ChiPhiHangMucQuery>("EXECUTE dbo.sp_khlcnt_get_nguonvon_chiphi_hm_goithau_by_qddautu @iIdGoiThauId, @iIdQDDauTuId",
                    new SqlParameter("@iIdGoiThauId", iIdGoiThau),
                    new SqlParameter("@iIdQDDauTuId", iIdQDDauTuId)).ToList();
            }

        }

        public bool CheckExistKHLCNhaThau(VdtQddtKhlcnhaThau data)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtQddtKhlcnhaThaus.Any(n => n.IIdDuAnId == data.IIdDuAnId && n.Id != data.Id);
            }
        }

        public void InsertGoiThauChiPhi(List<VdtDaGoiThauChiPhi> datas)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                //var lstDelete = ctx.VdtDaGoiThauChiPhis.Where(n => n.IIdGoiThauId == iIdGoiThau && n.IIDDuToanID == iIdDuToan);
                //var lstDelete = ctx.VdtDaGoiThauChiPhis.Where(n => n.IIdGoiThauId == iIdGoiThau);
                //ctx.VdtDaGoiThauChiPhis.RemoveRange(lstDelete);
                ctx.VdtDaGoiThauChiPhis.AddRange(datas);
                ctx.SaveChanges();
            }
        }

        public void InsertGoiThauHangMuc(List<VdtDaGoiThauHangMuc> datas)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                //var lstDelete = ctx.VdtDaGoiThauHangMucs.Where(n => n.IIdGoiThauId == iIdGoiThau && n.IIDDuToanID == iIdDuToan);
                //var lstDelete = ctx.VdtDaGoiThauHangMucs.Where(n => n.IIdGoiThauId == iIdGoiThau);
                //ctx.VdtDaGoiThauHangMucs.RemoveRange(lstDelete);
                ctx.VdtDaGoiThauHangMucs.AddRange(datas);
                ctx.SaveChanges();
            }
        }

        public void InsertGoiThauNguonVon(List<VdtDaGoiThauNguonVon> datas)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                //var lstDelete = ctx.VdtDaGoiThauNguonVons.Where(n => n.IIdGoiThauId == iIdGoiThau);
                //ctx.VdtDaGoiThauNguonVons.RemoveRange(lstDelete);
                ctx.VdtDaGoiThauNguonVons.AddRange(datas);
                ctx.SaveChanges();
            }
        }

        public void DeleteGoiThauDetailWhenChangeDuToan(Guid iIdKHLCNT, Guid iIdDuToan)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                ctx.Database.ExecuteSqlCommand("EXECUTE sp_khluachonnhathau_delete_detail_notby_dutoan @iIDKeHoachLuaChonNhaThau, @iIdDuToan",
                new SqlParameter("@iIDKeHoachLuaChonNhaThau", iIdKHLCNT),
                new SqlParameter("@iIdDuToan", iIdDuToan));
            }
        }

        public double GetTotalNguonVonByGoiThau(Guid iIdGoiThau)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                if (!ctx.VdtDaGoiThauNguonVons.Any(n => n.IIdGoiThauId == iIdGoiThau)) return 0;
                return ctx.VdtDaGoiThauNguonVons
                    .Where(n => n.IIdGoiThauId == iIdGoiThau).Sum(n => (n.FTienGoiThau ?? 0));
            }
        }

        public IEnumerable<VdtDaGoiThau> ListGoiThauByKHLCNhaThauId(Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDaGoiThaus.Where(x => x.IIdKhlcnhaThau == id).ToList();
            }
        }

        public void DeleteGoiThauByKHLCNTId(Guid idKHLCNT)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter khlcntIdParam = new SqlParameter("@id", idKHLCNT);
                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_vdt_delete_goithau_by_kehoachluachonnhathauid @id", khlcntIdParam);
            }
        }

        public double GetTotalNguonVonGoiThauDC(Guid iIdGoiThau)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                //if (!ctx.VdtDaGoiThauNguonVons.Any(n => n.IIdGoiThauId == iIdGoiThau)) return 0;
                return ctx.VdtDaGoiThauNguonVons
                    .Where(n => n.IIdGoiThauId == iIdGoiThau).Sum(n => (n.FTienGoiThau ?? 0));
            }
        }

        public void UpdateGoiThauByLCNT(Guid lcntID)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter khlcntIdParam = new SqlParameter("@id", lcntID);
                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_vdt_update_goithau_by_kehoachluachonnhathauid @id", khlcntIdParam);
            }
        }

        public bool CheckDuAnkExistKHLCNT(Guid duToanId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var lstDuToan = ctx.VdtQddtKhlcnhaThaus.Where(x => x.IIdDuToanId == duToanId).ToList();
                if (lstDuToan.Count > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public IEnumerable<ChiPhiHangMucQuery> GetNguonVonByDuAnLCNTAdd(Guid id, string sLoaiChungTu)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter isDuToanParam = new SqlParameter("@isDuToan", sLoaiChungTu);
                SqlParameter idParam = new SqlParameter("@id", id);
                return ctx.FromSqlRaw<ChiPhiHangMucQuery>("EXECUTE dbo.sp_khluachonnhathau_get_nguonvon_by_dutoan @id, @isDuToan", idParam, isDuToanParam).ToList();
            }
        }

        public IEnumerable<ChiPhiHangMucQuery> GetNguonVonByKHLCNTUpdate(Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                //SqlParameter isDuToanParam = new SqlParameter("@isDuToan", isDuToan);
                SqlParameter idParam = new SqlParameter("@id", id);
                return ctx.FromSqlRaw<ChiPhiHangMucQuery>("EXECUTE dbo.sp_khluachonnhathau_get_nguonvon_by_lcnt_update @id", idParam).ToList();
            }
        }

        public IEnumerable<ChiPhiHangMucQuery> GetChiPhiByDuAnLCNTAdd(Guid id, string sLoaiChungTu)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter isDuToanParam = new SqlParameter("@sLoaiChungTu", sLoaiChungTu);
                SqlParameter idParam = new SqlParameter("@id", id);
                return ctx.FromSqlRaw<ChiPhiHangMucQuery>("EXECUTE dbo.sp_khluachonnhathau_get_chiphi_by_dutoan @id, @sLoaiChungTu", idParam, isDuToanParam).ToList();
            }
        }

        public IEnumerable<ChiPhiHangMucQuery> GetChiPhiByKHLCNTUpdate(Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                //SqlParameter isDuToanParam = new SqlParameter("@isDuToan", isDuToan);
                SqlParameter idParam = new SqlParameter("@id", id);
                return ctx.FromSqlRaw<ChiPhiHangMucQuery>("EXECUTE dbo.sp_khluachonnhathau_get_chiphi_by_lcnt_update @id", idParam).ToList();
            }
        }

        public IEnumerable<ChiPhiHangMucQuery> GetHangMucByDuAnLCNTAdd(Guid id, string sLoaiChungTu)
        {
            if (sLoaiChungTu == LoaiKHLCNTType.CHU_TRUONG_DAU_TU)
                return new List<ChiPhiHangMucQuery>();
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter isDuToanParam = new SqlParameter("@sLoaiChungTu", sLoaiChungTu);
                SqlParameter idParam = new SqlParameter("@id", id);
                return ctx.FromSqlRaw<ChiPhiHangMucQuery>("EXECUTE dbo.sp_khluachonnhathau_get_hangmuc_by_dutoan @id, @sLoaiChungTu", idParam, isDuToanParam).ToList();
            }
        }

        public IEnumerable<ChiPhiHangMucQuery> GetHangMucByLCNTUpdate(Guid id, bool isDuToan)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter isDuToanParam = new SqlParameter("@isDuToan", isDuToan);
                SqlParameter idParam = new SqlParameter("@id", id);
                return ctx.FromSqlRaw<ChiPhiHangMucQuery>("EXECUTE dbo.sp_khluachonnhathau_get_hangmuc_by_lcnt_update @id, @isDuToan", idParam, isDuToanParam).ToList();
            }
        }

        public bool IsExistSoQuyetDinh(string soQuyetDinh, Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtQddtKhlcnhaThaus.Any(n =>
                        n.SSoQuyetDinh == soQuyetDinh
                    && (id == Guid.Empty || n.Id != id));
            }
        }
    }
}
