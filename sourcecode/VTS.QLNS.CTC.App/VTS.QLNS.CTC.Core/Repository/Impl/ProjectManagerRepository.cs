using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class ProjectManagerRepository : Repository<VdtDaDuAn>, IProjectManagerRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public ProjectManagerRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        #region NamNV
        public IEnumerable<ProjectManagerQuery> FindByCondition()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<ProjectManagerQuery>("EXECUTE dbo.sp_vdt_thongtinduan_index").ToList();
            }
        }

        public ProjectManagerQuery FindDuAnById(Guid duAnId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter duAnIdParam = new SqlParameter("@id", duAnId);
                return ctx.FromSqlRaw<ProjectManagerQuery>("EXECUTE dbo.sp_vdt_thongtinduan_by_id @id", duAnIdParam).ToList().FirstOrDefault();
            }
        }

        public IEnumerable<VdtDmLoaiCongTrinh> GetAllDMLoaiCongTrinh()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDmLoaiCongTrinhs.ToList();
            }
        }

        public bool CheckExitsQdDauTuByDuAnId(Guid duAnId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                int count = ctx.VdtDaQddauTus.Where(x => x.IIdDuAnId == duAnId).ToList().Count;
                return count > 0;
            }
        }

        public bool CheckExitsChuTruongDauTuByDuAnId(Guid duAnId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                int count = ctx.VdtDaChuTruongDauTus.Where(x => x.IIdDuAnId == duAnId).ToList().Count;
                return count > 0;
            }
        }

        public IEnumerable<VdtDmPhanCapDuAn> GetAllPhanCapDuAn()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDmPhanCapDuAns.Where(phanCap => phanCap.STen != "Cục duyệt").ToList();
            }
        }

        //public IEnumerable<VdtDmLoaiCongTrinh> GetAllLoaiCongTrinh()
        //{
        //    using (var ctx = _contextFactory.CreateDbContext())
        //    {
        //        return ctx.VdtDmLoaiCongTrinhs.ToList();
        //    }
        //}

        public bool CheckDuplicateMaDuAn(string maDuAn, Guid duAnId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                List<VdtDaDuAn> listDuAn = new List<VdtDaDuAn>();
                if (duAnId != Guid.Empty)
                {
                    listDuAn = ctx.VdtDaDuAns.Where(x => x.SMaDuAn == maDuAn && x.Id != duAnId).ToList();
                }
                else
                {
                    listDuAn = ctx.VdtDaDuAns.Where(x => x.SMaDuAn == maDuAn).ToList();
                }

                if (listDuAn.Count > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public IEnumerable<ProjectManagerDetailQuery> FindListProjectDetail(Guid duAnId, Guid quyetDinhId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter duAnIdParam = new SqlParameter("@duAnId", duAnId);
                SqlParameter quyetDinhIdParam = new SqlParameter("@IdQdDauTu", quyetDinhId);
                return ctx.FromSqlRaw<ProjectManagerDetailQuery>("EXECUTE dbo.sp_vdt_getall_duanchitiet @duAnId, @IdQdDauTu", duAnIdParam, quyetDinhIdParam).ToList();
            }
        }

        public IEnumerable<ReportTongHopThongTinDuAnQuery> FindByAggregateProjectInformationReport(AggregateProjectInformationReportCriteria condition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<ReportTongHopThongTinDuAnQuery>("EXECUTE dbo.sp_vdt_bctonghopthongtinduan @iIDMaDonVi,@iNamKeHoach",
                                                                            new SqlParameter("@iIDMaDonVi", condition.IdMaDonViQuanLy),
                                                                            new SqlParameter("@iNamKeHoach", condition.NamKeHoach)).ToList();
            }
        }

        public IEnumerable<ReportChiTieuCapPhatDuAnQuery> FindByConditionProjectAllocationReport(ReportChiTieuCapPhatDuAnCriteria condition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<ReportChiTieuCapPhatDuAnQuery>("EXECUTE dbo.sp_rpt_vdt_theochitieu_capphatduan @donViID,@namThucHien,@nguonVonId",
                                                                            new SqlParameter("@donViID", condition.IdMaDonViQuanLy),
                                                                            new SqlParameter("@namThucHien", condition.NamThucHien),
                                                                            new SqlParameter("@nguonVonId", condition.NguonVonId)).ToList();
            }

        }

        public IEnumerable<ReportChiTieuCapPhatDuAnQuery> FindListParentByConditionProjectAllocationReport(ReportChiTieuCapPhatDuAnCriteria condition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<ReportChiTieuCapPhatDuAnQuery>("EXECUTE dbo.sp_rpt_vdt_theochitieu_capphatduan_parent @donViID,@namKhoiTao,@namKeHoach",
                                                                            new SqlParameter("@donViID", condition.IdMaDonViQuanLy),
                                                                            new SqlParameter("@namKhoiTao", condition.NamKhoiTao),
                                                                            new SqlParameter("@namKeHoach", condition.NamThucHien)).ToList();
            }
        }

        public IEnumerable<ReportDuToanNSQPNamQuery> FindByConditionDuToanNSQPNamReport(DuToanNSQPNamReportSearch condition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<ReportDuToanNSQPNamQuery>("EXECUTE dbo.sp_rpt_vdt_dutoan_nsqp_nam @donViID,@namKeHoach",
                                                                            new SqlParameter("@donViID", condition.MaDonViQuanLy),
                                                                            new SqlParameter("@namKeHoach", condition.NamThucHien)).ToList();
            }
        }

        public IEnumerable<ReportDuToanNSQPNamQuery> FindListParentDuToanNSQPNamReport(DuToanNSQPNamReportSearch condition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<ReportDuToanNSQPNamQuery>("EXECUTE dbo.sp_rpt_vdt_dutoan_nsqp_nam_parent @donViID,@namKeHoach",
                                                                            new SqlParameter("@donViID", condition.MaDonViQuanLy),
                                                                            new SqlParameter("@namKeHoach", condition.NamThucHien)).ToList();
            }
        }
        #endregion

        #region LongDT
        public IEnumerable<ChiPhiHangMucQuery> GetDetailData(Guid iIdParent)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<ChiPhiHangMucQuery>("EXECUTE dbo.sp_da_get_detailduan @iIdDuAn", new SqlParameter("@iIdDuAn", iIdParent)).ToList();
            }
        }

        public IEnumerable<ChiPhiHangMucQuery> GetChiPhiByDuAnId(Guid iIdDuAn)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<ChiPhiHangMucQuery>("EXECUTE dbo.sp_get_chiphiinthongtinduanscreen @iIdDuAnID", new SqlParameter("@iIdDuAnID", iIdDuAn)).ToList();
            }
        }

        public IEnumerable<ChiPhiHangMucQuery> GetDetailByDuAnId(Guid iIdDuAn)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<ChiPhiHangMucQuery>("EXECUTE dbo.sp_da_get_detail_by_duanid @iIdDuAn", new SqlParameter("@iIdDuAn", iIdDuAn)).ToList();
            }
        }

        public void InsertDuAnHangMuc(IEnumerable<VdtDaDuAnHangMuc> datas)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.VdtDaDuAnHangMucs.AddRange(datas);
                ctx.SaveChanges();
            }
        }

        public void UpdateDuAnHangMuc(IEnumerable<VdtDaDuAnHangMuc> datas)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var dicUpdate = datas.ToDictionary(n => n.Id, n => n);
                var lstKey = dicUpdate.Keys;
                var lstDataUpdate = ctx.VdtDaDuAnHangMucs.Where(n => lstKey.Contains(n.Id)).ToList();
                lstDataUpdate = lstDataUpdate.Select(n =>
                {
                    n.STenHangMuc = dicUpdate[n.Id].STenHangMuc;
                    n.fHanMucDauTu = dicUpdate[n.Id].fHanMucDauTu;
                    n.IIdParentId = dicUpdate[n.Id].IIdParentId;
                    n.IdLoaiCongTrinh = dicUpdate[n.Id].IdLoaiCongTrinh;
                    return n;
                }).ToList();
                ctx.VdtDaDuAnHangMucs.UpdateRange(lstDataUpdate);
                ctx.SaveChanges();
            }
        }

        public void DeleteDuAnHangMuc(IEnumerable<VdtDaDuAnHangMuc> datas)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var dicUpdate = datas.ToDictionary(n => n.Id, n => n);
                var lstKey = dicUpdate.Keys;
                var lstDataDelete = ctx.VdtDaDuAnHangMucs.Where(n => lstKey.Contains(n.Id));
                ctx.VdtDaDuAnHangMucs.RemoveRange(lstDataDelete);
                ctx.SaveChanges();
            }
        }

        public void InsertDuAnNguonVon(IEnumerable<VdtDaNguonVon> datas)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.VdtDaNguonVons.AddRange(datas);
                ctx.SaveChanges();
            }
        }

        public void UpdateDuAnNguonVon(Guid iIdDuAn, IEnumerable<VdtDaNguonVon> datas)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var dicUpdate = datas.ToDictionary(n => n.IIdNguonVonId.Value, n => n);
                var lstKey = dicUpdate.Keys;
                var lstNguonVon = ctx.VdtDaNguonVons.Where(n => n.IIdDuAn == iIdDuAn);
                if (lstNguonVon == null || !lstNguonVon.Any()) return;
                var lstNguonVonUpdate = lstNguonVon.Where(n => lstKey.Contains(n.IIdNguonVonId.Value)).ToList()
                    .Select(n =>
                    {
                        n.FThanhTien = dicUpdate[n.IIdNguonVonId.Value].FThanhTien;
                        return n;
                    });
                ctx.VdtDaNguonVons.UpdateRange(lstNguonVonUpdate);
                ctx.SaveChanges();
            }
        }

        public void DeleteDuAnNguonVon(Guid iIdDuAn, IEnumerable<VdtDaNguonVon> datas)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var lstUpdate = datas.Select(n => n.IIdNguonVonId.Value);
                var lstNguonVon = ctx.VdtDaNguonVons.Where(n => n.IIdDuAn == iIdDuAn);
                if (lstNguonVon == null || !lstNguonVon.Any()) return;
                var lstNguonVonUpdate = lstNguonVon.Where(n => lstUpdate.Contains(n.IIdNguonVonId.Value));
                ctx.VdtDaNguonVons.RemoveRange(lstNguonVonUpdate);
                ctx.SaveChanges();
            }
        }

        public void InsertDmDuAnChiPhi(IEnumerable<VdtDmDuAnChiPhi> datas)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.VdtDmDuAnChiPhis.AddRange(datas);
                ctx.SaveChanges();
            }
        }

        public void UpdateDmDuAnChiPhi(IEnumerable<VdtDmDuAnChiPhi> datas)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var dicUpdate = datas.ToDictionary(n => n.Id, n => n);
                var lstKey = dicUpdate.Keys;
                var lstDataUpdate = ctx.VdtDmDuAnChiPhis.Where(n => lstKey.Contains(n.Id)).ToList();
                lstDataUpdate = lstDataUpdate.Select(n =>
                {
                    n.STenChiPhi = dicUpdate[n.Id].STenChiPhi;
                    return n;
                }).ToList();
                ctx.VdtDmDuAnChiPhis.UpdateRange(lstDataUpdate);
                ctx.SaveChanges();
            }
        }

        public void DeleteDmDuAnChiPhi(IEnumerable<VdtDmDuAnChiPhi> datas)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var dicUpdate = datas.ToDictionary(n => n.Id, n => n);
                var lstKey = dicUpdate.Keys;
                var lstDataDelete = ctx.VdtDmDuAnChiPhis.Where(n => lstKey.Contains(n.Id));
                ctx.VdtDmDuAnChiPhis.RemoveRange(lstDataDelete);
                ctx.SaveChanges();
            }
        }

        public void InsertDuAnChiPhi(IEnumerable<VdtDaDuAnChiPhi> datas)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.VdtDaDuAnChiPhis.AddRange(datas);
                ctx.SaveChanges();
            }
        }

        public void UpdateDuAnChiPhi(Guid iIdDuAn, IEnumerable<VdtDaDuAnChiPhi> datas)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var dicUpdate = datas.ToDictionary(n => n.IIdDmduAnChiPhi, n => n);
                var lstKey = dicUpdate.Keys;
                var lstDataUpdate = ctx.VdtDaDuAnChiPhis.Where(n => n.IIdDuAnId == iIdDuAn && lstKey.Contains(n.IIdDmduAnChiPhi)).ToList();
                lstDataUpdate = lstDataUpdate.Select(n =>
                {
                    n.FTienPheDuyet = dicUpdate[n.IIdDmduAnChiPhi].FTienPheDuyet;
                    return n;
                }).ToList();
                ctx.VdtDaDuAnChiPhis.UpdateRange(lstDataUpdate);
                ctx.SaveChanges();
            }
        }

        public void DeleteDuAnChiPhi(Guid iIdDuAn, IEnumerable<VdtDaDuAnChiPhi> datas)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var dicUpdate = datas.ToDictionary(n => n.IIdDmduAnChiPhi, n => n);
                var lstKey = dicUpdate.Keys;
                var lstDataDelete = ctx.VdtDaDuAnChiPhis.Where(n => n.IIdDuAnId == iIdDuAn && lstKey.Contains(n.IIdDmduAnChiPhi));
                ctx.VdtDaDuAnChiPhis.RemoveRange(lstDataDelete);
                ctx.SaveChanges();
            }
        }

        public VdtDaDuAn InsertAutoCode(VdtDaDuAn entity)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                //int iMaxId = (ctx.VdtDaDuAns.Where(n => (n.IMaDuAnIndex ?? 0) != 0).OrderByDescending(n => n.IMaDuAnIndex).FirstOrDefault().IMaDuAnIndex ?? 0) + 1;
                int iMaxId = (ctx.VdtDaDuAns.Where(n => (n.IMaDuAnIndex ?? 0) != 0)?.Max(n => n.IMaDuAnIndex) ?? 0) + 1;

                //entity.SMaDuAn += "-" + iMaxId.ToString("0000");
                entity.IMaDuAnIndex = iMaxId;
                entity.STrangThaiDuAn = PROJECT_STATUS.KHOI_TAO;
                ctx.VdtDaDuAns.Add(entity);
                ctx.SaveChanges();
                return entity;
            }
        }

        public void UpdateDataDuAn(VdtDaDuAn entity)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var objUpdate = ctx.VdtDaDuAns.Find(entity.Id);
                if (objUpdate == null) return;
                objUpdate.IIdDonViQuanLyId = entity.IIdDonViQuanLyId;
                objUpdate.IIdMaDonViQuanLy = entity.IIdMaDonViQuanLy;
                objUpdate.IIdDonViThucHienDuAnId = entity.IIdDonViThucHienDuAnId;
                objUpdate.IIdMaDonViThucHienDuAn = entity.IIdMaDonViThucHienDuAn;
                objUpdate.STenDuAn = entity.STenDuAn;
                objUpdate.IIdChuDauTuId = entity.IIdChuDauTuId;
                objUpdate.IIdMaChuDauTuId = entity.IIdMaChuDauTuId;
                objUpdate.IIdCapPheDuyetId = entity.IIdCapPheDuyetId;
                objUpdate.IIdHinhThucQuanLyId = entity.IIdHinhThucQuanLyId;
                objUpdate.SKhoiCong = entity.SKhoiCong;
                objUpdate.SKetThuc = entity.SKetThuc;
                objUpdate.IIdNhomDuAnId = entity.IIdNhomDuAnId;
                objUpdate.FHanMucDauTu = entity.FHanMucDauTu;
                objUpdate.BIsDuPhong = entity.BIsDuPhong;
                objUpdate.SDiaDiem = entity.SDiaDiem;
                objUpdate.SMucTieu = entity.SMucTieu;
                objUpdate.SQuyMo = entity.SQuyMo;
                objUpdate.DDateUpdate = entity.DDateUpdate;
                objUpdate.SUserUpdate = entity.SUserUpdate;
                objUpdate.SBanQuanLyDuAn = entity.SBanQuanLyDuAn;
                ctx.VdtDaDuAns.Update(objUpdate);
                ctx.SaveChanges();
            }
        }

        public IEnumerable<VDTDaNguonVonQuery> FindListNguonVonByDuan(Guid duAnId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter duAnIdParam = new SqlParameter("@duAnId", duAnId);
                return ctx.FromSqlRaw<VDTDaNguonVonQuery>("EXECUTE dbo.sp_vdt_getall_nguonvon_by_duan_detail @duAnId", duAnIdParam).ToList();
            }
        }

        public IEnumerable<VdtDaHangMucQuery> FindListHangMucByDuan(Guid duAnId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter duAnIdParam = new SqlParameter("@duAnId", duAnId);
                return ctx.FromSqlRaw<VdtDaHangMucQuery>("EXECUTE dbo.sp_vdt_getall_hangmuc_by_duan_detail @duAnId", duAnIdParam).ToList();
            }
        }

        public IEnumerable<VdtDaDuAn> FindListDuAnByDonViKHLCNT(string donviId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter donViIdParam = new SqlParameter("@donviId", donviId);
                return ctx.Set<VdtDaDuAn>().FromSql("EXECUTE dbo.sp_vdt_getlist_duan_by_donvi_kehoachlcnt @donviId", donViIdParam).ToList();
            }
        }

        public IEnumerable<VdtDaDuAn> FindListDuAnByDonViChuTruongDauTu(string donviId, Guid iIDKhlcNhaThau)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_vdt_khlcnt_get_duan_by_donvi_chutruongdautu @donviId,@iIDKhlcNhaThau";
                var parameters = new[] {
                    new SqlParameter("@donviId", donviId),
                    new SqlParameter("@iIDKhlcNhaThau", iIDKhlcNhaThau)
                };
                return ctx.Set<VdtDaDuAn>().FromSql(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<VdtDaDuAn> FindListDuAnByDonViQDDauTu(string donviId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter donViIdParam = new SqlParameter("@donviId", donviId);
                return ctx.Set<VdtDaDuAn>().FromSql("EXECUTE dbo.sp_vdt_getlist_duan_by_donvi_kehoachlcnt_qddautu @donviId", donViIdParam).ToList();
            }
        }

        public bool CheckDuAnQuyetToanHoanThanh(Guid duAnId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var listQuyetToan = ctx.VdtQtQuyetToans.Where(x => x.IIdDuAnId == duAnId).ToList();
                if (listQuyetToan != null && listQuyetToan.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public IEnumerable<VdtDaDuAn> FindDuAnByLoaiChungTu(string sLoaiChungTu, string sMaDonViThucHienDuAn, bool isAdd)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeQuery = "EXECUTE dbo.sp_vdt_da_duan_get_by_loaichungtu @sLoaiChungTu, @sMaDonViThucHienDuAn, @IsAdd";
                var parameters = new[]
                {
                    new SqlParameter("@sLoaiChungTu", sLoaiChungTu),
                    new SqlParameter("@sMaDonViThucHienDuAn", sMaDonViThucHienDuAn),
                    new SqlParameter("@IsAdd", isAdd),
                };
                //return ctx.Set<VdtDaDuAn>().FromSql(executeQuery, parameters).ToList();
                return ctx.FromSqlRaw<VdtDaDuAn>(executeQuery, parameters).ToList();
            }
        }

        public IEnumerable<VdtKhlcNhaThauCanCuQuery> FindChungTuInKeHoachLuaChonNhaThauScreen(Guid iIdKhLcNhaThau, Guid iIdDuAn, string sLoaiChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeQuery = "EXECUTE dbo.sp_vdt_khlcnt_chungtu_by_loaichungtu @iIdKhlcntId, @iIdDuAnId, @sLoaiChungTu";
                var parameters = new[]
                {
                    new SqlParameter("@iIdKhlcntId", iIdKhLcNhaThau),
                    new SqlParameter("@iIdDuAnId", iIdDuAn),
                    new SqlParameter("@sLoaiChungTu", sLoaiChungTu),
                };
                return ctx.FromSqlRaw<VdtKhlcNhaThauCanCuQuery>(executeQuery, parameters).ToList();
            }
        }

        public IEnumerable<VdtKhlcNhaThauCanCuQuery> FindChungTuInKeHoachLuaChonNhaThauDieuChinh(Guid iIdKhLcNhaThau, string sLoaiChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeQuery = "EXECUTE dbo.sp_vdt_khlcnt_get_chungtu_dieuchinh @iIdKhlcntId, @sLoaiChungTu";
                var parameters = new[]
                {
                    new SqlParameter("@iIdKhlcntId", iIdKhLcNhaThau),
                    new SqlParameter("@sLoaiChungTu", sLoaiChungTu)
                };
                return ctx.FromSqlRaw<VdtKhlcNhaThauCanCuQuery>(executeQuery, parameters).ToList();
            }
        }
        #endregion
    }
}
