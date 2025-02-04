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
using Dapper;
using System.Collections.ObjectModel;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtDaGoiThauRepository : Repository<VdtDaGoiThau>, IVdtDaGoiThauRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtDaGoiThauRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<VdtDaGoiThauQuery> FindByCondition(int namLamViec)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                return ctx.FromSqlRaw<VdtDaGoiThauQuery>("EXECUTE dbo.sp_vdt_getall_goithau @YearOfWork", yearOfWorkParam).ToList();
            }
        }

        public void DeleteGoiThauChiTiet(Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter goiThauIdParam = new SqlParameter("@id", id);
                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_vdt_delete_goithauchitiet @id", goiThauIdParam);
            }
        }

        public IEnumerable<VdtDmNhaThau> GetAllNhaThau()
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDmNhaThaus.ToList();
            }
        }

        public IEnumerable<VdtDaGoiThauDetailQuery> FindListDetail(Guid goiThauId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter goiThauIdParam = new SqlParameter("@goiThauId", goiThauId);
                return ctx.FromSqlRaw<VdtDaGoiThauDetailQuery>("EXECUTE dbo.sp_vdt_getall_goithauchitiet @goiThauId", goiThauIdParam).ToList();
            }
        }

        public IEnumerable<VdtDaGoiThauDetailQuery> FindListDieuChinhDetail(Guid goiThauId, Guid goiThauGocId, DateTime dngayLap)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter goiThauIdParam = new SqlParameter("@goiThauId", goiThauId);
                SqlParameter goiThauGocIdParam = new SqlParameter("@goiThauGocId", goiThauGocId);
                SqlParameter ngayLapParam = new SqlParameter("@dNgayLap", dngayLap);
                return ctx.FromSqlRaw<VdtDaGoiThauDetailQuery>("EXECUTE dbo.sp_vdt_getall_goithaudieuchinhchitiet @goiThauId,@goiThauGocId,@dNgayLap", goiThauIdParam, goiThauGocIdParam, ngayLapParam).ToList();
            }
        }

        public IEnumerable<VdtDmChiPhi> GetListChiPhiByDuAn(Guid idDuAn, DateTime ngayLap)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter duAnIdParam = new SqlParameter("@duAnId", idDuAn);
                SqlParameter ngayLapParam = new SqlParameter("@ngayLap", ngayLap);
                return ctx.Set<VdtDmChiPhi>().FromSql("EXECUTE dbo.sp_vdt_getlist_chiphi_by_duan @duAnId,@ngayLap", duAnIdParam, ngayLapParam).ToList();
            }
        }

        public IEnumerable<NsNguonNganSach> GetListNguonVonByDuAn(Guid idDuAn, DateTime ngayLap)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter duAnIdParam = new SqlParameter("@duAnId", idDuAn);
                SqlParameter ngayLapParam = new SqlParameter("@ngayLap", ngayLap);
                return ctx.Set<NsNguonNganSach>().FromSql("EXECUTE dbo.sp_vdt_getlist_nguonvon_by_duan @duAnId,@ngayLap", duAnIdParam, ngayLapParam).ToList();
            }
        }

        public IEnumerable<VdtDaDuAnHangMuc> GetListHangMucByDuAn(Guid idDuAn, DateTime ngayLap)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter duAnIdParam = new SqlParameter("@duAnId", idDuAn);
                SqlParameter ngayLapParam = new SqlParameter("@ngayLap", ngayLap);
                return ctx.FromSqlRaw<VdtDaDuAnHangMuc>("EXECUTE dbo.sp_vdt_getlist_hangmuc_by_duan @duAnId,@ngayLap", duAnIdParam, ngayLapParam).ToList();
            }
        }

        public double? GetTongMucDTChiPhi(Guid idChiPhi, Guid idDuAn, DateTime dNgayLap)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter chiPhiIdParam = new SqlParameter("@chiPhiId", idChiPhi);
                SqlParameter duAnIdParam = new SqlParameter("@duAnId", idDuAn);
                SqlParameter ngayLapParam = new SqlParameter("@ngayLap", dNgayLap);
                double tongMucDT = 0;
                SqlParameter tongMucDTParam = new SqlParameter("@tongMucDT", tongMucDT) { Direction = ParameterDirection.Output };
                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_vdt_get_tongmucdautuchiphi @chiPhiId, @duAnId,@ngayLap,@tongMucDT OUT", chiPhiIdParam, duAnIdParam, ngayLapParam, tongMucDTParam);
                return Convert.ToDouble(tongMucDTParam.Value);
            }
        }

        public double? GetTongMucDTNguonVon(int idNguonVon, Guid idDuAn, DateTime dNgayLap)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter nguonVonIdParam = new SqlParameter("@nguonVonId", idNguonVon);
                SqlParameter duAnIdParam = new SqlParameter("@duAnId", idDuAn);
                SqlParameter ngayLapParam = new SqlParameter("@ngayLap", dNgayLap);
                double tongMucDT = 0;
                SqlParameter tongMucDTParam = new SqlParameter("@tongMucDT", tongMucDT) { Direction = ParameterDirection.Output };
                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_vdt_get_tongmucdautunguonvon @nguonVonId, @duAnId,@ngayLap,@tongMucDT OUT", nguonVonIdParam, duAnIdParam, ngayLapParam, tongMucDTParam);
                return Convert.ToDouble(tongMucDTParam.Value);
            }
        }

        public double? GetTongMucDTHangMuc(Guid idHangMuc, Guid idDuAn, DateTime dNgayLap)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter hangMucIdParam = new SqlParameter("@hangMucId", idHangMuc);
                SqlParameter duAnIdParam = new SqlParameter("@duAnId", idDuAn);
                SqlParameter ngayLapParam = new SqlParameter("@ngayLap", dNgayLap);
                double tongMucDT = 0;
                SqlParameter tongMucDTParam = new SqlParameter("@tongMucDT", tongMucDT) { Direction = ParameterDirection.Output };
                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_vdt_get_tongmucdautuhangmuc @hangMucId, @duAnId,@ngayLap,@tongMucDT OUT", hangMucIdParam, duAnIdParam, ngayLapParam, tongMucDTParam);
                return Convert.ToDouble(tongMucDTParam.Value);
            }
        }

        public void DeleteGoiThauChiPhi(Guid idGoiThau)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter goiThauIdParam = new SqlParameter("@id", idGoiThau);
                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_vdt_delete_goithauchiphi @id", goiThauIdParam);
            }
        }
        public void DeleteGoiThauNguonVon(Guid idGoiThau)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter goiThauIdParam = new SqlParameter("@id", idGoiThau);
                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_vdt_delete_goithaunguonvon @id", goiThauIdParam);
            }
        }
        public void DeleteGoiThauHangMuc(Guid idGoiThau)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter goiThauIdParam = new SqlParameter("@id", idGoiThau);
                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_vdt_delete_goithauhangmuc @id", goiThauIdParam);
            }
        }

        public IEnumerable<VdtDaDuAn> FindDuAnByDonViGoiThau(string donviUserId, int namLamViec)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                SqlParameter donViIdParam = new SqlParameter("@donViQLId", donviUserId);
                return ctx.Set<VdtDaDuAn>().FromSql("EXECUTE dbo.sp_vdt_get_list_duan_by_donvi_goithau @YearOfWork, @donViQLId", yearOfWorkParam, donViIdParam).ToList();
            }
        }

        public IEnumerable<GoiThauChiPhiQuery> FindListGoiThauChiPhi(Guid goiThauId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter goiThauIdParam = new SqlParameter("@goiThauId", goiThauId);
                return ctx.FromSqlRaw<GoiThauChiPhiQuery>("EXECUTE dbo.sp_vdt_get_list_goithauchiphi @goiThauId", goiThauIdParam).ToList();
            }
        }

        public IEnumerable<GoiThauNguonVonQuery> FindListGoiThauNguonVon(Guid goiThauId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter goiThauIdParam = new SqlParameter("@goiThauId", goiThauId);
                return ctx.FromSqlRaw<GoiThauNguonVonQuery>("EXECUTE dbo.sp_vdt_get_list_goithaunguonvon @goiThauId", goiThauIdParam).ToList();
            }
        }

        public IEnumerable<GoiThauHangMucQuery> FindListGoiThauHangMuc(Guid goiThauId, Guid chiPhiDuAnId, bool isDuToan)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter goiThauIdParam = new SqlParameter("@goiThauId", goiThauId);
                SqlParameter chiPhiDuAnIdParam = new SqlParameter("@chiphiDuAnId", chiPhiDuAnId);
                if (isDuToan)
                {
                    return ctx.FromSqlRaw<GoiThauHangMucQuery>("EXECUTE dbo.sp_vdt_get_list_goithauhangmuc @goiThauId,@chiphiDuAnId", goiThauIdParam, chiPhiDuAnIdParam).ToList();
                }
                else
                {
                    return ctx.FromSqlRaw<GoiThauHangMucQuery>("EXECUTE dbo.sp_vdt_get_list_goithauhangmuc_qddautu @goiThauId,@chiphiDuAnId", goiThauIdParam, chiPhiDuAnIdParam).ToList();
                }
            }
        }

        public VdtDaGoiThau FindGoiThauDieuChinhByGoiThauGocId(Guid goiThauGocId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDaGoiThaus.Where(x => x.IIdGoiThauGocId == goiThauGocId && x.BActive == true).FirstOrDefault();
            }
        }

        public IEnumerable<NhaThauHopDongQuery> FindListNhaThauHopDongByGoiThau(Guid goiThauId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter goiThauIdParam = new SqlParameter("@goiThauId", goiThauId);
                return ctx.FromSqlRaw<NhaThauHopDongQuery>("EXECUTE dbo.sp_vdt_get_nhathau_hopdong_by_goithau @goiThauId", goiThauIdParam).ToList();
            }
        }

        public IEnumerable<VdtDaGoiThauQuery> FindByKhlcNhaThauId(Guid iIdKhlcNhaThauId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeQuery = "EXECUTE sp_vdt_goithau_get_by_khlcnhathau @iIdKhlcNhaThau";
                var parameters = new[]
                {
                    new SqlParameter("@iIdKhlcNhaThau", iIdKhlcNhaThauId)
                };
                return ctx.FromSqlRaw<VdtDaGoiThauQuery>(executeQuery, parameters).ToList();
            }
        }

        public IEnumerable<VdtKhlcNhaThauGoiThauDetailQuery> GetGoiThauNguonVonByChungTu(Guid iIdChungTu, string sLoaiChungTu)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeQuery = "EXECUTE sp_vdt_khlcnt_get_nguonvon_by_chungtu @iIdChungTu, @sLoaiChungTu";
                var parameters = new[]
                {
                    new SqlParameter("@iIdChungTu", iIdChungTu),
                    new SqlParameter("@sLoaiChungTu", sLoaiChungTu)
                };
                return ctx.FromSqlRaw<VdtKhlcNhaThauGoiThauDetailQuery>(executeQuery, parameters).ToList();
            }
        }

        public IEnumerable<VdtKhlcNhaThauGoiThauDetailQuery> GetGoiThauChiPhiByChungTu(Guid iIdChungTu, string sLoaiChungTu, bool bIsAdd)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeQuery = "EXECUTE sp_vdt_khlcnt_get_chiphi_by_chungtu @iIdChungTu, @sLoaiChungTu, @bIsAdd";
                var parameters = new[]
                {
                    new SqlParameter("@iIdChungTu", iIdChungTu),
                    new SqlParameter("@sLoaiChungTu", sLoaiChungTu),
                    new SqlParameter("@bIsAdd", bIsAdd)
                };
                return ctx.FromSqlRaw<VdtKhlcNhaThauGoiThauDetailQuery>(executeQuery, parameters).ToList();
            }
        }

        public IEnumerable<VdtKhlcNhaThauGoiThauDetailQuery> GetGoiThauChiPhiByChungTuCtdt_KhlcntEdit(Guid iIdKhlcnt)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeQuery = "EXECUTE sp_vdt_khlcnt_get_chiphi_by_chungtu_chutruongdautu @iIdKhlcntId";
                var parameters = new[]
                {
                    new SqlParameter("@iIdKhlcntId", iIdKhlcnt)
                };
                return ctx.FromSqlRaw<VdtKhlcNhaThauGoiThauDetailQuery>(executeQuery, parameters).ToList();
            }
        }

        public IEnumerable<VdtKhlcNhaThauGoiThauDetailQuery> GetGoiThauHangMucByChungTu(Guid iIdChungTu, string sLoaiChungTu)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeQuery = "EXECUTE sp_vdt_khlcnt_get_hangmuc_by_chungtu @iIdChungTu, @sLoaiChungTu";
                var parameters = new[]
                {
                    new SqlParameter("@iIdChungTu", iIdChungTu),
                    new SqlParameter("@sLoaiChungTu", sLoaiChungTu)
                };
                return ctx.FromSqlRaw<VdtKhlcNhaThauGoiThauDetailQuery>(executeQuery, parameters).ToList();
            }
        }

        public void DeleteGoiThauDetail(List<Guid> iIdGoiThaus)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeQuery = "EXECUTE sp_vdt_khlcnt_delete_goithau_detail @iIdGoiThaus";
                DataTable dt = DBExtension.ConvertDataToGuidTable(iIdGoiThaus);
                SqlParameter dtDetailParam = new SqlParameter("iIdGoiThaus", SqlDbType.Structured);
                dtDetailParam.TypeName = "t_tbl_uniqueidentifier";
                dtDetailParam.Value = dt;
                var parameters = new[]
                {
                    dtDetailParam
                };

                ctx.Database.ExecuteSqlCommand(executeQuery, parameters);
            }
        }

        public void DeleteListGoiThau(List<Guid> iIdGoiThaus)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeQuery = "EXECUTE sp_vdt_khlcnt_delete_goithau @iIdGoiThaus";
                DataTable dt = DBExtension.ConvertDataToGuidTable(iIdGoiThaus);
                var parameters = new[]
                {
                    new SqlParameter("@iIdGoiThaus", dt.AsTableValuedParameter("t_tbl_uniqueidentifier")),
                };
                ctx.Database.ExecuteSqlCommand(executeQuery, parameters);
            }
        }

        public IEnumerable<VdtKhlcNhaThauGoiThauDetailQuery> GetGoiThauNguonVonByKhlcNhaThauId(Guid iIdKhlcNhaThau)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeQuery = "EXECUTE sp_vdt_khlcnt_get_nguonvon_by_khlcntid @iIdKhlcnt";
                var parameters = new[]
                {
                    new SqlParameter("@iIdKhlcnt", iIdKhlcNhaThau)
                };
                return ctx.FromSqlRaw<VdtKhlcNhaThauGoiThauDetailQuery>(executeQuery, parameters).ToList();
            }
        }

        public IEnumerable<VdtKhlcNhaThauGoiThauDetailQuery> GetGoiThauChiPhiByKhlcNhaThauId(Guid iIdKhlcNhaThau)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeQuery = "EXECUTE sp_vdt_khlcnt_get_chiphi_by_khlcntid @iIdKhlcnt";
                var parameters = new[]
                {
                    new SqlParameter("@iIdKhlcnt", iIdKhlcNhaThau)
                };
                return ctx.FromSqlRaw<VdtKhlcNhaThauGoiThauDetailQuery>(executeQuery, parameters).ToList();
            }
        }

        public IEnumerable<VdtKhlcNhaThauGoiThauDetailQuery> GetGoiThauHangMucByKhlcNhaThauId(Guid iIdKhlcNhaThau)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeQuery = "EXECUTE sp_vdt_khlcnt_get_hangmuc_by_khlcntid @iIdKhlcnt";
                var parameters = new[]
                {
                    new SqlParameter("@iIdKhlcnt", iIdKhlcNhaThau)
                };
                return ctx.FromSqlRaw<VdtKhlcNhaThauGoiThauDetailQuery>(executeQuery, parameters).ToList();
            }
        }

        public IEnumerable<VdtKhlcntChiPhiNguonVonCanCuQuery> GetAllNguonVonByLoaiCanCuInKhlcntScreen(List<Guid> iIdCanCuIds, string sLoaiCanCu)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeQuery = "EXECUTE sp_vdt_khlcnt_get_all_nguonvon_by_loaicancu @iIds, @sLoaiCanCu";
                DataTable dt = DBExtension.ConvertDataToGuidTable(iIdCanCuIds);
                var parameters = new[]
                {
                    new SqlParameter("@iIds", dt.AsTableValuedParameter("t_tbl_uniqueidentifier")),
                    new SqlParameter("@sLoaiCanCu", sLoaiCanCu)
                };
                return ctx.FromSqlRaw<VdtKhlcntChiPhiNguonVonCanCuQuery>(executeQuery, parameters).ToList();
            }
        }

        public IEnumerable<VdtKhlcntChiPhiNguonVonCanCuQuery> GetAllChiPhiByLoaiCanCuInKhlcntScreen(List<Guid> iIdCanCuIds, string sLoaiCanCu)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeQuery = "EXECUTE sp_vdt_khlcnt_get_all_chiphi_by_loaicancu @iIds, @sLoaiCanCu";
                DataTable dt = DBExtension.ConvertDataToGuidTable(iIdCanCuIds);
                var parameters = new[]
                {
                    new SqlParameter("@iIds", dt.AsTableValuedParameter("t_tbl_uniqueidentifier")),
                    new SqlParameter("@sLoaiCanCu", sLoaiCanCu)
                };
                return ctx.FromSqlRaw<VdtKhlcntChiPhiNguonVonCanCuQuery>(executeQuery, parameters).ToList();
            }
        }

        public void ReActiveGoiThauByKhlcntId(Guid iIdKhlcnt)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var lstGoiThau = ctx.VdtDaGoiThaus.Where(n => n.IIdKhlcnhaThau == iIdKhlcnt).ToList();
                if (lstGoiThau == null || !lstGoiThau.Any()) return;
                lstGoiThau = lstGoiThau.Select(n => { n.BActive = true; return n; }).ToList();
                ctx.UpdateRange(lstGoiThau);
                ctx.SaveChanges();
            }
        }

        public IEnumerable<HopDongGoiThauQuery> FindGoiThauByDuAn(Guid duanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeQuery = @"EXECUTE sp_vdt_hopdong_find_goithau_by_du_an @DuAnId";
                var parameters = new[]
                {
                    new SqlParameter("@DuAnId", duanId)
                };
                var result = ctx.FromSqlRaw<HopDongGoiThauQuery>(executeQuery, parameters).ToList();
                foreach (var goithau in result)
                {
                    goithau.IdHopDongGoiThauNhaThau = Guid.NewGuid();
                    IEnumerable<HopDongChiPhiQuery> chiphis = FindChiPhiByGoiThau(goithau.GoiThauId);
                    goithau.ListChiPhi = new ObservableCollection<HopDongChiPhiQuery>(chiphis);
                    // load hang muc theo chi phi
                    foreach (var chiphi in goithau.ListChiPhi)
                    {
                        chiphi.IdHopDongGoiThauNhaThau = goithau.IdHopDongGoiThauNhaThau;
                        IEnumerable<HopDongChiPhiHangMucQuery> hangmucs = FindHangMucByChiPhi(chiphi.IdChiPhi.Value, chiphi.IdGoiThau.Value);
                        chiphi.ListHangMuc = new ObservableCollection<HopDongChiPhiHangMucQuery>(hangmucs);
                        foreach (var hm in chiphi.ListHangMuc)
                        {
                            hm.IdHopDongGoiThauNhaThau = goithau.IdHopDongGoiThauNhaThau;
                        }
                    }
                }
                return result;
            }
        }

        public IEnumerable<HopDongGoiThauQuery> FindGoiThauByHopDong(Guid hopdongId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeQuery = @"Execute sp_vdt_hopdong_find_goithau_by_hopdong @IdHopDong";
                var parameters = new[]
                    {
                    new SqlParameter("@IdHopDong", hopdongId)
                };
                var result = ctx.FromSqlRaw<HopDongGoiThauQuery>(executeQuery, parameters).ToList();
                foreach (var goithau in result)
                {
                    IEnumerable<HopDongChiPhiQuery> chiphis = FindChiPhiByGoiThau(goithau.GoiThauId, hopdongId, goithau.IdHopDongGoiThauNhaThau);
                    goithau.ListChiPhi = new ObservableCollection<HopDongChiPhiQuery>(chiphis);
                    // load hang muc theo chi phi
                    foreach (var chiphi in goithau.ListChiPhi)
                    {
                        chiphi.IdHopDongGoiThauNhaThau = goithau.IdHopDongGoiThauNhaThau;
                        IEnumerable<HopDongChiPhiHangMucQuery> hangmucs = FindHangMucByChiPhi(chiphi.IdChiPhi.Value, chiphi.IdGoiThau.Value, hopdongId, goithau.IdHopDongGoiThauNhaThau);
                        chiphi.ListHangMuc = new ObservableCollection<HopDongChiPhiHangMucQuery>(hangmucs);
                        foreach (var hm in chiphi.ListHangMuc)
                        {
                            hm.IdHopDongGoiThauNhaThau = goithau.IdHopDongGoiThauNhaThau;
                        }
                    }
                }
                return result;
            }
        }

        private IEnumerable<HopDongChiPhiQuery> FindChiPhiByGoiThau(Guid goithauID, Guid? idHopDong = null, Guid? idHopDongGoiThauNhaThau = null)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                // trong trường hợp sửa thì lấy danh sách các chi phí và hạng mục đã lưu, còn trong trường hợp thêm mới thì lấy trong KHLC nhà thấu
                string executeQuery = @"Execute sp_vdt_hopdong_find_chiphi_by_goithau @GoiThauId, @IdHopDong, @IdHopDongGoiThauNhaThau";
                var parameters = new[]
                {
                    new SqlParameter("@GoiThauId", goithauID),
                    //new SqlParameter("@IdHopDong", idHopDong.HasValue ? idHopDong.Value : Guid.Empty),
                    //new SqlParameter("@IdHopDongGoiThauNhaThau", idHopDongGoiThauNhaThau.HasValue ? idHopDongGoiThauNhaThau.Value : Guid.Empty),
                    new SqlParameter("@IdHopDong", idHopDong ?? null),
                    new SqlParameter("@IdHopDongGoiThauNhaThau", idHopDongGoiThauNhaThau ?? null),
                };
                var rs = ctx.FromSqlRaw<HopDongChiPhiQuery>(executeQuery, parameters);
                return OrderHopDongChiPhi(rs);
            }
        }

        private IEnumerable<HopDongChiPhiHangMucQuery> FindHangMucByChiPhi(Guid chiphiID, Guid goithauID, Guid? idHopDong = null, Guid? idHopDongGoiThauNhaThau = null)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string getHangMucQuery = @"Execute sp_vdt_hopdong_find_hangmuc_by_chiphi @ChiPhiId, @GoiThauId, @IdHopDong, @IdHopDongGoiThauNhaThau";
                var p = new[]
                {
                    new SqlParameter("@ChiPhiId", chiphiID),
                    new SqlParameter("@GoiThauId", goithauID),
                    new SqlParameter("@IdHopDong", idHopDong.HasValue ? idHopDong.Value : Guid.Empty),
                    new SqlParameter("@IdHopDongGoiThauNhaThau", idHopDongGoiThauNhaThau.HasValue ? idHopDongGoiThauNhaThau.Value : Guid.Empty),
                };
                return ctx.FromSqlRaw<HopDongChiPhiHangMucQuery>(getHangMucQuery, p);
            }
        }

        #region hop dong dieu chinh
        public IEnumerable<HopDongGoiThauQuery> DCFindGoiThauByHopDong(Guid hopDongGocId, Guid? hopdongDCId = null)
        {
            string executeQuery = @"Execute sp_vdt_hopdong_dieuchinh_find_goithau_by_hopdong @hopDongGocId, @hopdongDCId";
            object hddcID = DBNull.Value;
            if (hopdongDCId != null)
            {
                hddcID = hopdongDCId;
            }
            var parameters = new[]
            {
                new SqlParameter("@hopDongGocId", hopDongGocId),
                new SqlParameter("@hopdongDCId", hddcID)
            };
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.FromSqlRaw<HopDongGoiThauQuery>(executeQuery, parameters).ToList();
                foreach (var gt in result)
                {
                    gt.ListChiPhi = new ObservableCollection<HopDongChiPhiQuery>();
                    if (hopdongDCId == null)
                    {
                        gt.ListChiPhi = new ObservableCollection<HopDongChiPhiQuery>(DCFindChiPhiByGoiThau(gt.IdHopDongGoiThauNhaThau, gt.GoiThauId, hopDongGocId));
                    }
                    else
                    {
                        gt.ListChiPhi = new ObservableCollection<HopDongChiPhiQuery>(DCFindChiPhiByGoiThau(gt.OldIdHopDongGoiThauNhaThau.Value, gt.GoiThauId, hopDongGocId, gt.IdHopDongGoiThauNhaThau));
                    }
                    foreach (var cp in gt.ListChiPhi)
                    {
                        if (hopdongDCId == null)
                            cp.ListHangMuc = new ObservableCollection<HopDongChiPhiHangMucQuery>(DCFindHangMucByChiPhi1(cp.IdChiPhi.Value, cp.IdHopDongGoiThauNhaThau).OrderBy(t => t.MaOrDer));
                        else
                            cp.ListHangMuc = new ObservableCollection<HopDongChiPhiHangMucQuery>(DCFindHangMucByChiPhi2(cp.IdChiPhi.Value, cp.IdHopDongGoiThauNhaThau).OrderBy(t => t.MaOrDer));
                    }
                }
                return result;
            }
        }

        private IEnumerable<HopDongChiPhiQuery> DCFindChiPhiByGoiThau(Guid idHopDongGoiThauNhaThau, Guid idGoiThau, Guid hopdongId, Guid? idHopDongGoiThauNhaThauDC = null)
        {
            string executeQuery = "Execute sp_vdt_hopdong_dieuchinh_find_chiphi_by_goithau @IdHopDong, @idGoiThau, @idHopDongGoiThauNhaThau, @dcIdHopDongGoiThauNhaThau";
            object dieuchinhId = DBNull.Value;
            if (idHopDongGoiThauNhaThauDC != null)
            {
                dieuchinhId = idHopDongGoiThauNhaThauDC;
            }
            var parameters = new[]
            {
                new SqlParameter("@IdHopDong", hopdongId),
                new SqlParameter("@idGoiThau", idGoiThau),
                new SqlParameter("@idHopDongGoiThauNhaThau", idHopDongGoiThauNhaThau),
                new SqlParameter("@dcIdHopDongGoiThauNhaThau", dieuchinhId)
            };
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.FromSqlRaw<HopDongChiPhiQuery>(executeQuery, parameters).ToList();
                return OrderHopDongChiPhi(result);
            }
        }

        private List<HopDongChiPhiQuery> OrderHopDongChiPhi(IEnumerable<HopDongChiPhiQuery> datas)
        {
            List<HopDongChiPhiQuery> results = new List<HopDongChiPhiQuery>();
            if (datas == null) return results;
            foreach(var item in datas.Where(n => !n.IdChiPhiParent.HasValue))
            {
                results.AddRange(RecusiveChiPhi(item, datas.ToList()));
            }
            return results;
        }

        private List<HopDongChiPhiQuery> RecusiveChiPhi(HopDongChiPhiQuery item, List<HopDongChiPhiQuery> datas)
        {
            List<HopDongChiPhiQuery> results = new List<HopDongChiPhiQuery>();
            results.Add(item);
            foreach(var child in datas.Where(n => n.IdChiPhiParent == item.IdChiPhi))
            {
                results.AddRange(RecusiveChiPhi(child, datas));
            }
            return results;
        }

        // dieu chinh tu 1 hop dong goc
        private IEnumerable<HopDongChiPhiHangMucQuery> DCFindHangMucByChiPhi1(Guid chiphiID, Guid idHopDongGoiThauNhaThau)
        {
            string executeQuery = "Execute sp_vdt_hopdong_dieuchinh_find_hangmuc_by_chiphi_hopdonggoc @chiphiID, @idHopDongGoiThauNhaThau";
            var parameters = new[]
            {
                new SqlParameter("@chiphiID", chiphiID),
                new SqlParameter("@idHopDongGoiThauNhaThau", idHopDongGoiThauNhaThau)
            };
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.FromSqlRaw<HopDongChiPhiHangMucQuery>(executeQuery, parameters).ToList();
                return result;
            }
        }

        // update hop dong dieu chinh
        private IEnumerable<HopDongChiPhiHangMucQuery> DCFindHangMucByChiPhi2(Guid chiphiID, Guid idHopDongGoiThauNhaThau)
        {
            string executeQuery = "Execute sp_vdt_hopdong_dieuchinh_find_hangmuc_by_chiphi_hopdongdieuchinh @chiphiID, @idHopDongGoiThauNhaThau";
            var parameters = new[]
            {
                new SqlParameter("@chiphiID", chiphiID),
                new SqlParameter("@idHopDongGoiThauNhaThau", idHopDongGoiThauNhaThau)
            };
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.FromSqlRaw<HopDongChiPhiHangMucQuery>(executeQuery, parameters).ToList();
                return result;
            }
        }

        public string GetTypeOfGoiThau(Guid goithauId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var sql = from vdtDaGoiThau in ctx.VdtDaGoiThaus
                          join vdtQDDT in ctx.VdtQddtKhlcnhaThaus on vdtDaGoiThau.IIdKhlcnhaThau equals vdtQDDT.Id
                          where vdtDaGoiThau.Id == goithauId
                          select new { vdtQDDT.IIdDuToanId, vdtQDDT.IIdChuTruongDauTuID, vdtQDDT.IIdQDDauTuID };
                var rs = sql.FirstOrDefault();
                if (rs != null)
                {
                    if (rs.IIdDuToanId != null)
                        return VDT_INITIAL_NAME.THIET_KE_THI_CONG_TDT;
                    if (rs.IIdChuTruongDauTuID != null)
                        return VDT_INITIAL_NAME.CHU_TRUONG_DAU_TU;
                    if (rs.IIdQDDauTuID != null)
                        return VDT_INITIAL_NAME.PHE_DUYET_DU_AN;
                }
                return string.Empty;
            }
        }
        #endregion
    }
}
