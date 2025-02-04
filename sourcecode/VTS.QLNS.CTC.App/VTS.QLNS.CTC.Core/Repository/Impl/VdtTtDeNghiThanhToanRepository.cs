using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtTtDeNghiThanhToanRepository : Repository<VdtTtDeNghiThanhToan>, IVdtTtDeNghiThanhToanRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtTtDeNghiThanhToanRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<DuAnDeNghiThanhToanQuery> GetDuAnByDeNghiThanhToan(string iIdDonViQuanLy, int iNguonVonId, Guid iIdVon, string type, DateTime dNgayLap, int iNamKeHoach)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<DuAnDeNghiThanhToanQuery>("EXECUTE dbo.sp_vdt_get_duan_by_thanhtoanscreen_update @donViQuanLyId, @nguonVonID, @keHoachVonId, @ngayLap, @year, @typeGet",
                    new SqlParameter("@donViQuanLyId", iIdDonViQuanLy),
                    new SqlParameter("@nguonVonID", iNguonVonId),
                    new SqlParameter("@keHoachVonId", iIdVon),
                    new SqlParameter("@ngayLap", dNgayLap),
                    new SqlParameter("@year", iNamKeHoach),
                    new SqlParameter("@typeGet", type)).ToList();
            }
        }

        public IEnumerable<DuAnDeNghiThanhToanQuery> GetDetailDuAnByDeNghiThanhToan(string iIdDonViQuanLy, int iNguonVonId, Guid iIdLoaiNguonVon, DateTime dNgayLap, int iNamKeHoach)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<DuAnDeNghiThanhToanQuery>("EXECUTE dbo.sp_vdt_get_duan_by_thanhtoandetailscreen @donViQuanLyId, @nguonVonID, @loaiNguonVonID, @ngayLap, @year",
                    new SqlParameter("@donViQuanLyId", iIdDonViQuanLy),
                    new SqlParameter("@nguonVonID", iNguonVonId),
                    new SqlParameter("@loaiNguonVonID", iIdLoaiNguonVon),
                    new SqlParameter("@ngayLap", dNgayLap),
                    new SqlParameter("@year", iNamKeHoach)).ToList();
            }
        }

        public IEnumerable<VdtTtDeNghiThanhToanQuery> GetDataDeNghiThanhToanIndex(int namLamViec, string userName)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtTtDeNghiThanhToanQuery>("EXECUTE dbo.sp_vdt_denghithanhtoan_index_2 @YearOfWork, @UserName",
                    new SqlParameter("@YearOfWork", namLamViec),
                    new SqlParameter("@UserName", userName)).ToList();
            }
        }

        public IEnumerable<VdtTtDeNghiThanhToanChiPhiQuery> GetChiPhiInDenghiThanhToanScreen(DateTime dNgayDeNghi, Guid iIdDuAnId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var executeQuery = "EXECUTE dbo.sp_vdt_tt_getchiphi_in_denghithanhtoan_screen @dNgayDeNghi, @iIdDuAnId";
                var parameters = new[]
                {
                    new SqlParameter("@dNgayDeNghi", dNgayDeNghi),
                    new SqlParameter("@iIdDuAnId", iIdDuAnId)
                };
                return ctx.FromSqlRaw<VdtTtDeNghiThanhToanChiPhiQuery>(executeQuery, parameters).ToList();
            }
        }

        public void LoadGiaTriThanhToan(int iCoQuanThanhToan, DateTime ngayDeNghi, bool bThanhToanTheoHopDong, string iIdChungTu, int nguonVonId, int namKeHoach, Guid? thanhToanId, int? loaiCoQuanTaiChinh, ref double thanhToanTN, ref double thanhToanNN, ref double tamUngTN, ref double tamUngNN, ref double luyKeTUUngTruocTN, ref double luyKeTUUngTruocNN, ref double sumTN, ref double sumNN, int? loaiKeHoachVon)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "";
                //if (thanhToanId != null)
                //{
                //    sql = "EXECUTE dbo.sp_vdt_lay_gia_tri_denghi_thanh_toan_edit_2 @bThanhToanTheoHopDong, @iIdChungTu, @NgayDeNghi, @NguonVonId, @NamKeHoach, @iCoQuanThanhToan";
                //}
                //else
                //{
                //    sql = "EXECUTE dbo.sp_vdt_lay_gia_tri_denghi_thanh_toan @bThanhToanTheoHopDong, @iIdChungTu, @NgayDeNghi, @NguonVonId, @NamKeHoach, @iCoQuanThanhToan";
                //}
                sql = "EXECUTE dbo.sp_vdt_lay_gia_tri_denghi_thanh_toan_edit_3 @bThanhToanTheoHopDong, @iIdChungTu, @NgayDeNghi, @NguonVonId, @NamKeHoach, @iCoQuanThanhToan, @loaiCoQuanTaiChinh, @loaiKhv";
                var parameters = new[]
                {
                    new SqlParameter("@bThanhToanTheoHopDong", bThanhToanTheoHopDong),
                    new SqlParameter("@iIdChungTu", iIdChungTu),
                    new SqlParameter("@NgayDeNghi", ngayDeNghi),
                    new SqlParameter("@NguonVonId", nguonVonId),
                    new SqlParameter("@NamKeHoach", namKeHoach),
                    new SqlParameter("@iCoQuanThanhToan", iCoQuanThanhToan),
                    new SqlParameter("@loaiCoQuanTaiChinh", loaiCoQuanTaiChinh == null ? 0 : loaiCoQuanTaiChinh),
                    new SqlParameter("@loaiKhv", loaiKeHoachVon == null ? 0 : loaiKeHoachVon)
                };
                List<DeNghiThanhToanValueQuery> list = ctx.FromSqlRaw<DeNghiThanhToanValueQuery>(sql, parameters).ToList();
                DeNghiThanhToanValueQuery item = list != null && list.Count > 0 ? list.FirstOrDefault() : null;
                if (item != null)
                {
                    thanhToanTN = item.ThanhToanTN;
                    sumTN = item.fLyKeTN;
                    thanhToanNN = item.ThanhToanNN;
                    sumNN = item.fLuyKeNN;
                    tamUngTN = item.TamUngTN - item.ThuHoiUngTN;
                    tamUngNN = item.TamUngNN - item.ThuHoiUngNN;
                    luyKeTUUngTruocTN = item.TamUngUngTruocTN - item.ThuHoiUngUngTruocTN;
                    luyKeTUUngTruocNN = item.TamUngUngTruocNN - item.ThuHoiUngUngTruocNN;
                }
                else
                {
                    thanhToanTN = 0;
                    thanhToanNN = 0;
                    tamUngTN = 0;
                    tamUngNN = 0;
                    luyKeTUUngTruocTN = 0;
                    luyKeTUUngTruocNN = 0;
                }
            }
        }

        public void LoadGiaTriThanhToanBaoCao(int iCoQuanThanhToan, DateTime ngayDeNghi, bool bThanhToanTheoHopDong, string iIdChungTu, int nguonVonId, int namKeHoach, Guid? thanhToanId, int? loaiCoQuanTaiChinh, ref double thanhToanTN, ref double thanhToanNN, ref double tamUngTN, ref double tamUngNN, ref double luyKeTUUngTruocTN, ref double luyKeTUUngTruocNN, ref double sumTN, ref double sumNN, ref double sumThuHoiTN, ref double sumThuHoiNN, int? loaiKeHoachVon)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "";
                sql = "EXECUTE dbo.sp_vdt_lay_gia_tri_denghi_thanh_toan_bao_cao @bThanhToanTheoHopDong, @iIdChungTu, @NgayDeNghi, @NguonVonId, @NamKeHoach, @iCoQuanThanhToan, @loaiCoQuanTaiChinh, @loaiKhv";
                var parameters = new[]
                {
                    new SqlParameter("@bThanhToanTheoHopDong", bThanhToanTheoHopDong),
                    new SqlParameter("@iIdChungTu", iIdChungTu),
                    new SqlParameter("@NgayDeNghi", ngayDeNghi),
                    new SqlParameter("@NguonVonId", nguonVonId),
                    new SqlParameter("@NamKeHoach", namKeHoach),
                    new SqlParameter("@iCoQuanThanhToan", iCoQuanThanhToan),
                    new SqlParameter("@loaiCoQuanTaiChinh", loaiCoQuanTaiChinh == null ? 0 : loaiCoQuanTaiChinh),
                    new SqlParameter("@loaiKhv", loaiKeHoachVon == null ? 0 : loaiKeHoachVon)
                };
                List<DeNghiThanhToanValueQuery> list = ctx.FromSqlRaw<DeNghiThanhToanValueQuery>(sql, parameters).ToList();
                DeNghiThanhToanValueQuery item = list != null && list.Count > 0 ? list.FirstOrDefault() : null;
                if (item != null)
                {
                    thanhToanTN = item.ThanhToanTN;
                    sumTN = item.fLyKeTN;
                    thanhToanNN = item.ThanhToanNN;
                    sumNN = item.fLuyKeNN;
                    tamUngTN = item.TamUngTN - item.ThuHoiUngTN;
                    tamUngNN = item.TamUngNN - item.ThuHoiUngNN;
                    luyKeTUUngTruocTN = item.TamUngUngTruocTN - item.ThuHoiUngUngTruocTN;
                    luyKeTUUngTruocNN = item.TamUngUngTruocNN - item.ThuHoiUngUngTruocNN;
                    sumThuHoiTN = item.ThuHoiTN;
                    sumThuHoiNN = item.ThuHoiNN;
                }
                else
                {
                    thanhToanTN = 0;
                    thanhToanNN = 0;
                    tamUngTN = 0;
                    tamUngNN = 0;
                    luyKeTUUngTruocTN = 0;
                    luyKeTUUngTruocNN = 0;
                }
            }
        }

        public DeNghiThanhToanValueQuery LoadGiaTriPheDuyetThanhToanByParentId(Guid iIdPheDuyetThanhToanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tt_get_giatrithanhtoanthuctepheduyetthanhtoan_by_parentid @iIdPheDuyetId";
                var parameters = new[]
                {
                    new SqlParameter("@iIdPheDuyetId", iIdPheDuyetThanhToanId)
                };
                return ctx.FromSqlRaw<DeNghiThanhToanValueQuery>(sql, parameters).FirstOrDefault();
            }
        }

        public bool CheckExistSoQuyetDinh(Guid id, string soQuyetDinh)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {

                if (id == Guid.Empty)
                {
                    VdtTtDeNghiThanhToan item = ctx.VdtTtDeNghiThanhToans.Where(n => n.SSoDeNghi == soQuyetDinh).FirstOrDefault();
                    return item == null ? false : true;
                }
                else
                {
                    VdtTtDeNghiThanhToan item = ctx.VdtTtDeNghiThanhToans.Where(n => n.Id != id && n.SSoDeNghi == soQuyetDinh).FirstOrDefault();
                    return item == null ? false : true;
                }
            }
        }

        public List<CapPhatThanhToanReportQuery> GetDataReport(string id, int namLamViec, int donViTinh)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<CapPhatThanhToanReportQuery>("EXECUTE dbo.sp_vdt_report_cap_phat_thanh_toan_new @Id, @NamLamViec, @dvt",
                    new SqlParameter("@Id", id),
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@dvt", donViTinh)).ToList();                    
            }
        }

        public VdtTtDeNghiThanhToan FindByHopDongId(Guid hopdongId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtTtDeNghiThanhToans.FirstOrDefault(n => n.IIdHopDongId == hopdongId);
            }
        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                VdtTtDeNghiThanhToan entity = ctx.VdtTtDeNghiThanhToans.Find(id);
                entity.BKhoa = lockStatus;
                ctx.Entry(entity).State = EntityState.Modified;
                return ctx.SaveChanges();
            }
        }

        public void UpdateThongTriThanhToan(Guid iIdThongTriId, List<Guid> lstThanhToanId)
        {
            List<VdtTtDeNghiThanhToan> lstThanhToanUpdate = new List<VdtTtDeNghiThanhToan>();
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var lstThongTriOld = ctx.VdtTtDeNghiThanhToans.Where(n => n.IIdThongTriThanhToanId.HasValue && n.IIdThongTriThanhToanId == iIdThongTriId).ToList();
                if(lstThongTriOld != null)
                {
                    lstThongTriOld = lstThongTriOld.Select(n => { n.IIdThongTriThanhToanId = null; return n; }).ToList();
                    lstThanhToanUpdate.AddRange(lstThongTriOld);
                }
                if(lstThanhToanId != null)
                {
                    var lstThongTriNew = ctx.VdtTtDeNghiThanhToans.Where(n => lstThanhToanId.Contains(n.Id)).ToList();
                    if (lstThongTriNew != null)
                    {
                        lstThongTriNew = lstThongTriNew.Select(n => { n.IIdThongTriThanhToanId = iIdThongTriId; return n; }).ToList();
                        lstThanhToanUpdate.AddRange(lstThongTriNew);
                    }
                }
                ctx.UpdateRange(lstThanhToanUpdate);
                ctx.SaveChanges();
            }
            
        }

        public void TongHopDeNghiThanhToan(VdtTtDeNghiThanhToan vdtTtDeNghiThanhToan, List<Guid> childrenIds)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var exist = ctx.VdtTtDeNghiThanhToans.Any(t => t.Id.Equals(vdtTtDeNghiThanhToan.Id));
                if (exist)
                    ctx.Update(vdtTtDeNghiThanhToan);
                else
                    ctx.Add(vdtTtDeNghiThanhToan);
                var children = ctx.VdtTtDeNghiThanhToans.Where(t => childrenIds.Contains(t.Id)).ToList();
                children.ForEach(t => t.ParentId = vdtTtDeNghiThanhToan.Id);
                ctx.SaveChanges();
            }
        }

        public List<VdtTtDeNghiThanhToan> FindDeNghiTongHop()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtTtDeNghiThanhToans.Where(t => t.BTongHop.HasValue && t.BTongHop.Value).ToList();
            }
        }

        public VdtTtDeNghiThanhToan FindLastRowBySoDeNghi(int? namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var sql = $"select top 1 * from VDT_TT_DeNghiThanhToan where iNamKeHoach = {namLamViec} and bTongHop is null order by sSoDeNghi desc";
                return ctx.FromSqlRaw<VdtTtDeNghiThanhToan>(sql).ToList().FirstOrDefault();
            }
        }

        public List<VdtTtDeNghiThanhToanQuery> getListDeNghiThanhToanByThongtriId(Guid? id, int namLamViec, string userName)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                //var sql = $"select * from VDT_TT_DeNghiThanhToan where iID_ThongTriThanhToanID = '{id}' order by sSoDeNghi desc";
                //return ctx.FromSqlRaw<VdtTtDeNghiThanhToan>(sql).ToList();
                return ctx.FromSqlRaw<VdtTtDeNghiThanhToanQuery>("EXECUTE dbo.sp_vdt_denghithanhtoan_by_thongtri @YearOfWork, @UserName, @thongTriId",
                    new SqlParameter("@YearOfWork", namLamViec),
                    new SqlParameter("@UserName", userName),
                    new SqlParameter("@thongTriId", id)).ToList();
            }
        }
    }
}
