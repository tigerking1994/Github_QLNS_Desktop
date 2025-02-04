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
    public class VdtDaDuToanRepository : Repository<VdtDaDuToan>, IVdtDaDuToanRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtDaDuToanRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public bool CheckDuplicateSoQD(string soQuyetDinh, Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var listDuToan = ctx.VdtDaDuToans.Where(x => x.SSoQuyetDinh == soQuyetDinh && x.Id != id).ToList();

                if (listDuToan.Count > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public bool checkExistLoaiQuyetDinh(bool bLaTongDuToan, Guid? iIdDuAnId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var listDuToan = ctx.VdtDaDuToans.Where(x => x.BLaTongDuToan != bLaTongDuToan && x.IIdDuAnId == iIdDuAnId).ToList();


                if (listDuToan.Count > 0)
                {
                    return true;
                }
                return false;
            }
        }


        public double TinhTongPheDuyetDuAn(Guid? iIdDuAnId, Guid? idDuToanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                //return (double)ctx.VdtDaDuToans.Where(x => x.IIdDuAnId == iIdDuAnId).Sum(x => x.FTongDuToanPheDuyet);
                List<VdtDaDuToan> lstDaDuToan = ctx.VdtDaDuToans.Where(x => x.IIdDuAnId == iIdDuAnId && x.BActive).ToList();

                if (idDuToanId.HasValue)
                {
                    lstDaDuToan = lstDaDuToan.Where(x => x.Id != idDuToanId).ToList();
                }

                double sum = lstDaDuToan.Sum(x => x.FTongDuToanPheDuyet ?? 0);

                return sum;
            }
        }

        public void DeleteDuToanChiTiet(Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter duToanIdParam = new SqlParameter("@id", id);
                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_vdt_delete_dutoanchitiet @id", duToanIdParam);
            }
        }

        public IEnumerable<VdtDaDuToanQuery> FindByCondition(int namLamViec)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                return ctx.FromSqlRaw<VdtDaDuToanQuery>("EXECUTE dbo.sp_vdt_getall_tktcvatongdutoan @YearOfWork", yearOfWorkParam).ToList();
            }
        }

        public VdtDaDuToan FindByDuAnId(Guid duanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDaDuToans.Where(x => x.IIdDuAnId == duanId && x.BActive == true).FirstOrDefault();
            }
        }

        public List<VdtDaDuToan> FindListByDuAnId(Guid duanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDaDuToans.Where(x => x.IIdDuAnId == duanId && x.BActive == true).ToList();
            }
        }

        public IEnumerable<VdtDaDuAn> FindDuAnByDonViAndLoaiQD(string donviQLId, string loaiQD)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                //SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                SqlParameter donViIdParam = new SqlParameter("@donViQLId", donviQLId);
                SqlParameter loaiQDParam = new SqlParameter("@loaiQD", loaiQD);
                return ctx.Set<VdtDaDuAn>().FromSql("EXECUTE dbo.sp_vdt_get_list_duan_by_donvi_dutoan @donViQLId,@loaiQD", donViIdParam, loaiQDParam).ToList();
            }
        }

        public IEnumerable<DuToanDetailQuery> FindListDetail(Guid duToanId, Guid? duAnChiPhiId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_getall_dutoanchitiet @duToanId,@duAnChiPhiId";
                var parameters = new[]
                {
                    new SqlParameter("@duToanId", duToanId),
                    new SqlParameter("@duAnChiPhiId", duAnChiPhiId)
                };
                return ctx.FromSqlRaw<DuToanDetailQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<DuToanDetailQuery> FindListHangMucDieuChinhAdd(Guid duToanId, Guid? duAnChiPhiId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_getall_hangmuc_dieuchinh_add @duToanId,@duAnChiPhiId";
                var parameters = new[]
                {
                    new SqlParameter("@duToanId", duToanId),
                    new SqlParameter("@duAnChiPhiId", duAnChiPhiId)
                };
                return ctx.FromSqlRaw<DuToanDetailQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<DuToanDetailQuery> FindListHangMucDieuChinhUpdate(Guid duToanId, Guid? duAnChiPhiId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_getall_hangmuc_dieuchinh_update @duToanId,@duAnChiPhiId";
                var parameters = new[]
                {
                    new SqlParameter("@duToanId", duToanId),
                    new SqlParameter("@duAnChiPhiId", duAnChiPhiId)
                };
                return ctx.FromSqlRaw<DuToanDetailQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<VdtDaDuToanChiPhiQuery> FindListDuToanChiPhiByDuAn(Guid duAnId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter duAnIdParam = new SqlParameter("@duAnId", duAnId);
                return ctx.FromSqlRaw<VdtDaDuToanChiPhiQuery>("EXECUTE dbo.sp_vdt_get_all_dutoan_chiphi_by_duan @duAnId", duAnIdParam).ToList();
            }
        }

        public IEnumerable<VdtDaDuToanChiPhiQuery> FindListDuToanChiPhiByDuToanId(Guid duToanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter duToanIdParam = new SqlParameter("@duToanId", duToanId);
                return ctx.FromSqlRaw<VdtDaDuToanChiPhiQuery>("EXECUTE dbo.sp_vdt_get_all_dutoan_chiphi_by_dutoanId @duToanId", duToanIdParam).ToList();
            }
        }

        public IEnumerable<VdtDaDuToanChiPhiQuery> FindListDuToanChiPhiDieuChinhAdd(Guid duToanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter duToanIdParam = new SqlParameter("@duToanId", duToanId);
                return ctx.FromSqlRaw<VdtDaDuToanChiPhiQuery>("EXECUTE dbo.sp_vdt_get_all_dutoan_chiphi_dieuchinh_add @duToanId", duToanIdParam).ToList();
            }
        }

        public IEnumerable<VdtDaDuToanChiPhiQuery> FindListDuToanChiPhiDieuChinhUpdate(Guid duToanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter duToanIdParam = new SqlParameter("@duToanId", duToanId);
                return ctx.FromSqlRaw<VdtDaDuToanChiPhiQuery>("EXECUTE dbo.sp_vdt_get_all_dutoan_chiphi_dieuchinh_update @duToanId", duToanIdParam).ToList();
            }
        }

        public IEnumerable<VdtDaDuToanNguonVonQuery> FindListDuToanNguonVonByDuAn(Guid duAnId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter duAnIdParam = new SqlParameter("@duAnId", duAnId);
                return ctx.FromSqlRaw<VdtDaDuToanNguonVonQuery>("EXECUTE dbo.sp_vdt_get_all_dutoan_nguonvon_by_duan @duAnId", duAnIdParam).ToList();
            }
        }

        public IEnumerable<VdtDaDuToanNguonVonQuery> FindListDuToanNguonVonByDuToanId(Guid duToanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter duToandParam = new SqlParameter("@duToanId", duToanId);
                return ctx.FromSqlRaw<VdtDaDuToanNguonVonQuery>("EXECUTE dbo.sp_vdt_get_all_dutoan_nguonvon @duToanId", duToandParam).ToList();
            }
        }

        public IEnumerable<VdtDaDuToanNguonVonQuery> FindListDuToanNguonVonDieuChinhAdd(Guid duToanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter duToandParam = new SqlParameter("@duToanId", duToanId);
                return ctx.FromSqlRaw<VdtDaDuToanNguonVonQuery>("EXECUTE dbo.sp_vdt_get_all_dutoan_nguonvon_dieuchinh_add @duToanId", duToandParam).ToList();
            }
        }

        public IEnumerable<VdtDaDuToanNguonVonQuery> FindListDuToanNguonVonDieuChinhUpdate(Guid duToanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter duToandParam = new SqlParameter("@duToanId", duToanId);
                return ctx.FromSqlRaw<VdtDaDuToanNguonVonQuery>("EXECUTE dbo.sp_vdt_get_all_dutoan_nguonvon_dieuchinh_update @duToanId", duToandParam).ToList();
            }
        }

        public void AddDuToanHangMuc(Guid duToanId, Guid qdDauTuId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter duToanIdParam = new SqlParameter("@duToan", duToanId);
                SqlParameter qdDauTuIdParam = new SqlParameter("@qdDauTuId", qdDauTuId);
                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_vdt_add_dutoanhangmuc @duToan,@qdDauTuId", duToanIdParam, qdDauTuIdParam);
            }
        }

        public IEnumerable<VdtDmChiPhi> GetListQDChiPhi(Guid duToanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter duToanIdParam = new SqlParameter("@duToanId", duToanId);
                return ctx.Set<VdtDmChiPhi>().FromSql("EXECUTE dbo.sp_vdt_getlist_dmchiphi_by_dutoan @duToanId", duToanIdParam).ToList();
            }
        }

        public void AddDuToanHangMucDetail(Guid duToanId, Guid qdDauTuId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter duToanIdParam = new SqlParameter("@duToanId", duToanId);
                SqlParameter qdDauTuIdParam = new SqlParameter("@qdDauTuId", qdDauTuId);
                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_vdt_add_dutoanhangmuc_detail @duToanId,@qdDauTuId", duToanIdParam, qdDauTuIdParam);
            }
        }

        public IEnumerable<ChiPhiHangMucQuery> GetDetailByDuAnId(Guid iIdDuAn, Guid? iIdChiPhi)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_dutoan_get_detail_by_duanid @iIdDuAn, @iIdChiPhiId";
                var parameters = new[]
                {
                    new SqlParameter("@iIdChiPhiId", iIdChiPhi),
                    new SqlParameter("@iIdDuAn", iIdDuAn)
                };
                return ctx.FromSqlRaw<ChiPhiHangMucQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<ChiPhiHangMucQuery> GetDetailByDuToanId(Guid iIdDuToan, Guid? iIdChiPhi)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_dutoan_get_detaildutoan @iIdDuToan, @iIdChiPhiId";
                var parameters = new[]
                {
                    new SqlParameter("@iIdChiPhiId", iIdChiPhi),
                    new SqlParameter("@iIdDuToan", iIdDuToan)
                };
                return ctx.FromSqlRaw<ChiPhiHangMucQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<ChiPhiHangMucQuery> GetDetailByQDDauTuId(Guid iIdQDDauTu, Guid? iIdChiPhi)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_dutoan_get_detail_qddautu @iIdQDDauTu,@iIdChiPhiId";
                var parameters = new[]
                {
                    new SqlParameter("@iIdQDDauTu", iIdQDDauTu),
                    new SqlParameter("@iIdChiPhiId", iIdChiPhi)
                };
                return ctx.FromSqlRaw<ChiPhiHangMucQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<ChiPhiHangMucQuery> GetDetailByChuTruongDauTuId(Guid iIdChuTruongDauTu)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_vdt_khlcnt_get_detail_chutruongdautu_by_id @iIdChuTruongDauTu";
                var parameters = new[]
                {
                    new SqlParameter("@iIdChuTruongDauTu", iIdChuTruongDauTu)
                };
                return ctx.FromSqlRaw<ChiPhiHangMucQuery>(sql, parameters).ToList();
            }
        }

        public void InsertDuToanChiPhi(IEnumerable<VdtDaDuToanChiPhi> datas)
        {
            //TODO: TungNH Move logic to VdtDaDuToanChiPhis
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                ctx.VdtDaDuToanChiPhis.AddRange(datas);
                ctx.SaveChanges();
            }
        }

        public void UpdateDuToanChiPhi(Guid iIdDuToan, IEnumerable<VdtDaDuToanChiPhi> datas)
        {
            //TODO: TungNH Move logic to VdtDaDuToanChiPhis
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var dicUpdate = datas.ToDictionary(n => n.IIdDuAnChiPhi, n => n);
                var lstKey = dicUpdate.Keys;
                var lstDataUpdate = ctx.VdtDaDuToanChiPhis.Where(n => n.IIdDuToanId == iIdDuToan && lstKey.Contains(n.IIdDuAnChiPhi)).ToList();
                lstDataUpdate = lstDataUpdate.Select(n =>
                {
                    n.FTienPheDuyet = dicUpdate[n.IIdDuAnChiPhi].FTienPheDuyet;
                    return n;
                }).ToList();
                ctx.VdtDaDuToanChiPhis.UpdateRange(lstDataUpdate);
                ctx.SaveChanges();
            }
        }

        public void DeleteDuToanChiPhi(Guid iIdDuToan, IEnumerable<VdtDaDuToanChiPhi> datas)
        {
            //TODO: TungNH Move logic to VdtDaDuToanChiPhis
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var dicUpdate = datas.ToDictionary(n => n.IIdDuAnChiPhi, n => n);
                var lstKey = dicUpdate.Keys;
                var lstDataDelete = ctx.VdtDaDuToanChiPhis.Where(n => n.IIdDuToanId == iIdDuToan && lstKey.Contains(n.IIdDuAnChiPhi));
                ctx.VdtDaDuToanChiPhis.RemoveRange(lstDataDelete);
                ctx.SaveChanges();
            }
        }

        public void InsertDuToanNguonVon(IEnumerable<VdtDaDuToanNguonvon> datas)
        {
            //TODO: TungNH Move logic to VdtDaDuToanNguonvons
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                ctx.VdtDaDuToanNguonvons.AddRange(datas);
                ctx.SaveChanges();
            }
        }

        public void UpdateDuToanNguonVon(Guid iIdDuToan, IEnumerable<VdtDaDuToanNguonvon> datas)
        {
            //TODO: TungNH Move logic to VdtDaDuToanNguonvons
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var dicUpdate = datas.ToDictionary(n => n.IIdNguonVonId.Value, n => n);
                var lstKey = dicUpdate.Keys;
                var lstNguonVon = ctx.VdtDaDuToanNguonvons.Where(n => n.IIdDuToanId == iIdDuToan);
                if (lstNguonVon == null || !lstNguonVon.Any()) return;
                var lstNguonVonUpdate = lstNguonVon.Where(n => lstKey.Contains(n.IIdNguonVonId.Value)).ToList()
                    .Select(n =>
                    {
                        n.FTienPheDuyet = dicUpdate[n.IIdNguonVonId.Value].FTienPheDuyet;
                        return n;
                    });
                ctx.VdtDaDuToanNguonvons.UpdateRange(lstNguonVonUpdate);
                ctx.SaveChanges();
            }
        }

        public void DeleteDuToanNguonVon(Guid iIdDuToan, IEnumerable<VdtDaDuToanNguonvon> datas)
        {
            //TODO: TungNH Move logic to VdtDaDuToanNguonvons
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var lstUpdate = datas.Select(n => n.IIdNguonVonId.Value);
                var lstNguonVon = ctx.VdtDaDuToanNguonvons.Where(n => n.IIdDuToanId == iIdDuToan);
                if (lstNguonVon == null || !lstNguonVon.Any()) return;
                var lstNguonVonUpdate = lstNguonVon.Where(n => lstUpdate.Contains(n.IIdNguonVonId.Value));
                ctx.VdtDaDuToanNguonvons.RemoveRange(lstNguonVonUpdate);
                ctx.SaveChanges();
            }
        }

        public void InsertDmDuToanHangMuc(IEnumerable<VdtDaDuToanDmHangMuc> datas)
        {
            //TODO: TungNH Move logic to VdtDaDuToanNguonvons
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                ctx.VdtDaDuToanDmHangMucs.AddRange(datas);
                ctx.SaveChanges();
            }
        }

        public void UpdateDmDuToanHangMuc(IEnumerable<VdtDaDuToanDmHangMuc> datas)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var dicUpdate = datas.ToDictionary(n => n.Id, n => n);
                var lstKey = dicUpdate.Keys;
                var lstDataUpdate = ctx.VdtDaDuToanDmHangMucs.Where(n => lstKey.Contains(n.Id)).ToList();
                lstDataUpdate = lstDataUpdate.Select(n =>
                {
                    n.STenHangMuc = dicUpdate[n.Id].STenHangMuc;
                    return n;
                }).ToList();
                ctx.VdtDaDuToanDmHangMucs.UpdateRange(lstDataUpdate);
                ctx.SaveChanges();
            }
        }

        public void DeleteDmDuToanHangMuc(IEnumerable<VdtDaDuToanDmHangMuc> datas)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var dicUpdate = datas.ToDictionary(n => n.Id, n => n);
                var lstKey = dicUpdate.Keys;
                var lstDataDelete = ctx.VdtDaDuToanDmHangMucs.Where(n => lstKey.Contains(n.Id));
                ctx.VdtDaDuToanDmHangMucs.RemoveRange(lstDataDelete);
                ctx.SaveChanges();
            }
        }

        public void InsertDuToanHangMuc(IEnumerable<VdtDaDuToanHangMuc> datas)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                ctx.VdtDaDuToanHangMucs.AddRange(datas);
                ctx.SaveChanges();
            }
        }

        public void UpdateDuToanHangMuc(Guid iIdDuToan, IEnumerable<VdtDaDuToanHangMuc> datas)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var dicUpdate = datas.ToDictionary(n => n.IIdHangMucId, n => n);
                var lstKey = dicUpdate.Keys;
                var lstDataUpdate = ctx.VdtDaDuToanHangMucs.Where(n => n.IIdDuToanId == iIdDuToan && lstKey.Contains(n.IIdHangMucId)).ToList();
                lstDataUpdate = lstDataUpdate.Select(n =>
                {
                    n.FTienPheDuyet = dicUpdate[n.IIdHangMucId].FTienPheDuyet;
                    return n;
                }).ToList();
                ctx.VdtDaDuToanHangMucs.UpdateRange(lstDataUpdate);
                ctx.SaveChanges();
            }
        }

        public void DeleteDuToanHangMuc(Guid iIdDuToan, IEnumerable<VdtDaDuToanHangMuc> datas)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var dicUpdate = datas.ToDictionary(n => n.IIdHangMucId, n => n);
                var lstKey = dicUpdate.Keys;
                var lstDataDelete = ctx.VdtDaDuToanHangMucs.Where(n => n.IIdDuToanId == iIdDuToan && lstKey.Contains(n.IIdHangMucId));
                ctx.VdtDaDuToanHangMucs.RemoveRange(lstDataDelete);
                ctx.SaveChanges();
            }
        }
        public bool CheckExistInDuToanHangMuc(Guid duToanId, Guid danhMucChiPhiDuAnId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                VdtDaDuToanHangMuc duToanHangMuc = ctx.VdtDaDuToanHangMucs.Where(x => x.IIdDuToanId == duToanId && x.IIdDuAnChiPhi == danhMucChiPhiDuAnId).FirstOrDefault();
                if (duToanHangMuc != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool CheckExistInDuToanHangMuc(string listDuToanId, Guid danhMucChiPhiDuAnId)
        {
            if (string.IsNullOrEmpty(listDuToanId))
            {
                return false;
            }
            List<string> listDuToan = listDuToanId.Split(',').ToList();

            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                VdtDaDuToanHangMuc duToanHangMuc = ctx.VdtDaDuToanHangMucs.Where(x => listDuToan.Contains(x.IIdDuToanId.ToString()) && x.IIdDuAnChiPhi == danhMucChiPhiDuAnId).FirstOrDefault();
                if (duToanHangMuc != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public IEnumerable<DuToanDetailQuery> ListHangMucInitial(Guid qdDauTuId, Guid danhMucDuAnChiPhiId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter qdDauTuIdParam = new SqlParameter("@qdDauTuId", qdDauTuId);
                SqlParameter duAnChiPhiIdParam = new SqlParameter("@danhMucDuAnChiPhiId", danhMucDuAnChiPhiId);
                return ctx.FromSqlRaw<DuToanDetailQuery>("EXECUTE dbo.sp_vdt_getall_dutoanchitiet_hangmuc_initial @qdDauTuId, @danhMucDuAnChiPhiId", qdDauTuIdParam, duAnChiPhiIdParam).ToList();
            }
        }

        public IEnumerable<DuToanDetailQuery> ListHangMucByQDDauTu(Guid qdDauTuId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter qdDauTuIdParam = new SqlParameter("@qdDauTuId", qdDauTuId);
                return ctx.FromSqlRaw<DuToanDetailQuery>("EXECUTE dbo.sp_vdt_getall_dutoan_hangmuc_by_qddautu @qdDauTuId", qdDauTuIdParam).ToList();
            }
        }

        public IEnumerable<DuToanDetailQuery> ListHangMucByDuToan(Guid duToanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter duToanIdParam = new SqlParameter("@duToanId", duToanId);
                return ctx.FromSqlRaw<DuToanDetailQuery>("EXECUTE dbo.sp_vdt_getall_dutoan_hangmuc_by_dutoan @duToanId", duToanIdParam).ToList();
            }
        }

        public IEnumerable<VdtDaDuToanQuery> GetDuToanByDuAnId(Guid iIdDuAn)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtDaDuToanQuery>("EXECUTE dbo.sp_get_dutoan_by_duanid @iIdDuAn", new SqlParameter("@iIdDuAn", iIdDuAn)).ToList();
            }
        }

        public IEnumerable<VdtDaDuToanQuery> GetDuToanByDuAnIdAndActive(Guid iIdDuAn, int bActive)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtDaDuToanQuery>("EXECUTE dbo.sp_get_dutoan_by_duanid_and_active @iIdDuAn, @bActive", new SqlParameter("@iIdDuAn", iIdDuAn), new SqlParameter("@bActive", bActive)).ToList();
            }
        }

        public IEnumerable<VdtDaDuToanQuery> GetQDDauTuByDuAnIdAndNgayLap(Guid iIdDuAn, DateTime ngayLap)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter duAnIdParam = new SqlParameter("@iIdDuAn", iIdDuAn);
                SqlParameter ngayLapParam = new SqlParameter("@ngayLap", ngayLap);
                return ctx.FromSqlRaw<VdtDaDuToanQuery>("EXECUTE dbo.sp_get_qddautu_by_duanid_khlcnt @iIdDuAn, @ngayLap", duAnIdParam, ngayLapParam).ToList();
            }
        }

        public IEnumerable<VdtDaDuToanQuery> GetDuToanByKHLCNhaThauId(Guid duToanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtDaDuToanQuery>("EXECUTE dbo.sp_get_dutoan_by_khlcnhathauid @IdDuToan", new SqlParameter("@IdDuToan", duToanId)).ToList();
            }
        }

        public IEnumerable<VdtDaDuToanQuery> GetQDDauTuByKHLCNhaThauId(Guid qdDauTuId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtDaDuToanQuery>("EXECUTE dbo.sp_get_qddautu_by_khlcnhathauid @IdQDDauTu", new SqlParameter("@IdQDDauTu", qdDauTuId)).ToList();
            }
        }

        public IEnumerable<DuToanDetailQuery> FindListHangMucAllDetail(Guid duToanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter duToanIdParam = new SqlParameter("@duToanId", duToanId);
                return ctx.FromSqlRaw<DuToanDetailQuery>("EXECUTE dbo.sp_vdt_getall_dutoanchitiet_hangmuc_viewdetail @duToanId", duToanIdParam).ToList();
            }
        }

        public IEnumerable<DuToanDetailQuery> FindListDetail(string duToanId, Guid? duAnChiPhiId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_getall_dutoanchitiet_danhsach @duToanId,@duAnChiPhiId";
                var parameters = new[]
                {
                    new SqlParameter("@duToanId", duToanId),
                    new SqlParameter("@duAnChiPhiId", duAnChiPhiId)
                };
                return ctx.FromSqlRaw<DuToanDetailQuery>(sql, parameters).ToList();
            }
        }

        public VdtDaDuToan FindDuToanByDuToanGocId(Guid id, Guid duToanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDaDuToans.Where(x => x.IIdDuToanGocId == id && x.BActive == true && x.Id != duToanId).FirstOrDefault();
            }
        }

        public string GetDuToanIdByDuAnId(Guid duAnId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                List<VdtDaDuToan> list = ctx.VdtDaDuToans.Where(x => x.IIdDuAnId == duAnId && x.BActive == true).ToList();
                if (list != null && list.Count > 0)
                {
                    return string.Join(",", list.Select(n => n.Id.ToString()).ToList());
                }
                else
                {
                    VdtDaDuToan itemTongDuToan = ctx.VdtDaDuToans.Where(x => x.IIdDuAnId == duAnId && x.BLaTongDuToan && x.BActive == true).FirstOrDefault();
                    if (itemTongDuToan != null)
                    {
                        return itemTongDuToan.Id.ToString();
                    }
                }
                return string.Empty;
            }
        }

        public double GetGiaTriDuToanIdByDuAnId(Guid duAnId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                List<VdtDaDuToan> list = ctx.VdtDaDuToans.Where(x => x.IIdDuAnId == duAnId && x.BActive == true).ToList();
                if (list != null && list.Count > 0)
                {
                    return list.Sum(n => n.FTongDuToanPheDuyet.HasValue ? n.FTongDuToanPheDuyet.Value : 0);
                }
                else
                {
                    VdtDaDuToan itemTongDuToan = ctx.VdtDaDuToans.Where(x => x.IIdDuAnId == duAnId && x.BLaTongDuToan && x.BActive == true).FirstOrDefault();
                    if (itemTongDuToan != null)
                    {
                        return itemTongDuToan.FTongDuToanPheDuyet.HasValue ? itemTongDuToan.FTongDuToanPheDuyet.Value : 0;
                    }
                }
                return 0;
            }
        }

        public bool CheckQDDTExistTKTCTDT(Guid qdDtId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var lstDuToan = ctx.VdtDaDuToans.Where(x => x.IIdQddauTuId == qdDtId).ToList();
                if (lstDuToan.Count > 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
