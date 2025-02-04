using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class ApproveProjectRepository : Repository<VdtDaQddauTu>, IApproveProjectRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public ApproveProjectRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<ApproveProjectQuery> FindByCondition(int namLamViec, int nguonNganSach, string donviUserId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                SqlParameter budgetSourceParam = new SqlParameter("@BudgetSource", nguonNganSach);
                SqlParameter agencyUserIdParam = new SqlParameter("@AgencyUserId", donviUserId);
                return ctx.FromSqlRaw<ApproveProjectQuery>("EXECUTE dbo.sp_vdt_getall_pheduyetduan @YearOfWork, @BudgetSource, @AgencyUserId", yearOfWorkParam, budgetSourceParam, agencyUserIdParam).ToList();
            }
        }

        public IEnumerable<VdtDaDuAn> FindDuAnByDonVi(string donviQLId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter donViIdParam = new SqlParameter("@donViQLId", donviQLId);
                return ctx.Set<VdtDaDuAn>().FromSql("EXECUTE dbo.sp_vdt_get_list_duan_by_donvi @donViQLId", donViIdParam).ToList();
            }
        }

        public IEnumerable<VdtDaDuAn> FindDuAnByMaDonVi(string sMaDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDaDuAns.Where(n => n.IIdMaDonViQuanLy == sMaDonVi).ToList();
            }
        }

        public IEnumerable<VdtDmNhomDuAn> GetAllNhomDuAn()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDmNhomDuAns.ToList();
            }
        }

        public IEnumerable<VdtDmHinhThucQuanLy> GetAllHinhThucQuanLy()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDmHinhThucQuanLies.ToList();
            }
        }

        public DonVi FindDonViByIdDonVi(string iDDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDonVis.FirstOrDefault(x => x.IIDMaDonVi == iDDonVi);
            }
        }

        public VdtDaDuAn FindDuAnById(Guid idDuAn)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDaDuAns.FirstOrDefault(x => x.Id == idDuAn);
            }
        }

        public VdtDaQddauTu FindQDDaTuDieuChinhByDuAn(Guid id, Guid duAnId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDaQddauTus.Where(x => x.IIdDuAnId == duAnId && x.BActive == true && x.Id != id).FirstOrDefault();
            }
        }

        public IEnumerable<VdtDmChiPhi> GetAllDmChiPhi()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDmChiPhis.ToList();
            }
        }

        public IEnumerable<NsNguonNganSach> GetAllNguonNS()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NguonNganSaches.Where(x => x.ITrangThai == 1).ToList();
            }
        }

        public IEnumerable<ApproveProjectDetailQuery> FindListDetail(Guid quyetDinhDauTuId, Guid duAnId, Guid? duAnChiPhiId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_getall_pheduyetduanchitiet_hangmuc @quyetDinhDauTuId, @duAnId,@duAnChiPhiId";
                var parameters = new[]
                {
                    new SqlParameter("@quyetDinhDauTuId", quyetDinhDauTuId),
                    new SqlParameter("@duAnId", duAnId),
                    new SqlParameter("@duAnChiPhiId", duAnChiPhiId)
                };
                return ctx.FromSqlRaw<ApproveProjectDetailQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<ApproveProjectDetailQuery> FindListHangMucByQDDauTu(Guid quyetDinhDauTuId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter quyetDinhDauTuIdParam = new SqlParameter("@quyetDinhDauTuId", quyetDinhDauTuId);
                
                return ctx.FromSqlRaw<ApproveProjectDetailQuery>("EXECUTE dbo.sp_vdt_getall_hangmuc_by_qddautuid @quyetDinhDauTuId", quyetDinhDauTuIdParam).ToList();
            }
        }

        public void DeleteQDDauTuChiTiet(Guid id, Guid? parentId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_delete_pheduyetduanchitiet @id, @parentId";
                var parameters = new[]
                {
                    new SqlParameter("@id", id),
                    new SqlParameter("@parentId", parentId.IsNullOrEmpty() ? DBNull.Value : (object)parentId)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public bool CheckDuplicateSoQD(string soQuyetDinh, Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                List<VdtDaQddauTu> listQDDauTu = new List<VdtDaQddauTu>();
                if (id != Guid.Empty)
                {
                    listQDDauTu = ctx.VdtDaQddauTus.Where(x => x.SSoQuyetDinh == soQuyetDinh && x.Id != id).ToList();
                }
                else
                {
                    listQDDauTu = ctx.VdtDaQddauTus.Where(x => x.SSoQuyetDinh == soQuyetDinh).ToList();
                }

                if (listQDDauTu.Count > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public IEnumerable<ApproveProjectDieuChinhDetailQuery> FindListDieuChinhDetail(Guid quyetDinhDauTuId, DateTime ngayQuyetDinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter quyetDinhDauTuIdParam = new SqlParameter("@quyetDinhDauTuId", quyetDinhDauTuId);
                SqlParameter ngayQuyetDinhParam = new SqlParameter("@ngayQuyetDinh", ngayQuyetDinh);
                return ctx.FromSqlRaw<ApproveProjectDieuChinhDetailQuery>("EXECUTE dbo.sp_vdt_getall_pheduyetduandieuchinhchitiet @quyetDinhDauTuId, @ngayQuyetDinh", quyetDinhDauTuIdParam, ngayQuyetDinhParam).ToList();
            }
        }

        public bool CheckExistsDuAnInQDDauTu(Guid duAnId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                List<VdtDaQddauTu> listQDDT = ctx.VdtDaQddauTus.Where(x => x.IIdDuAnId.Value == duAnId).ToList();
                if (listQDDT != null && listQDDT.Count > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public VdtDaQddauTu FindByDuAnId(Guid duAnId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDaQddauTus.Where(x => x.IIdDuAnId == duAnId && x.BActive == true).FirstOrDefault();
            }
        }

        public IEnumerable<QDDauTuChiPhiNguonVonDetailQuery> FindListChiPhiNguonVonDetail(Guid qDDauTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter quyetDinhDTParam = new SqlParameter("@qdDauTuId", qDDauTu);
                return ctx.FromSqlRaw<QDDauTuChiPhiNguonVonDetailQuery>("EXECUTE dbo.sp_vdt_get_qddautu_chiphi_nguonvon_chitiet @qdDauTuId", quyetDinhDTParam).ToList();
            }
        }

        public IEnumerable<ApproveProjectDetailQuery> FindListHangMucDetail(Guid quyetDinhDauTuId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter qdDauTuIdParam = new SqlParameter("@qdDauTuId", quyetDinhDauTuId);
                return ctx.FromSqlRaw<ApproveProjectDetailQuery>("EXECUTE dbo.sp_vdt_getall_pheduyetduanchitiet_hangmuc_parent @qdDauTuId", qdDauTuIdParam).ToList();
            }
        }

        public IEnumerable<VdtDmChiPhi> GetListQDChiPhi(Guid qdDauTuId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter qdDauTuIdParam = new SqlParameter("@quyetDinhDauTuId", qdDauTuId);
                return ctx.Set<VdtDmChiPhi>().FromSql("EXECUTE dbo.sp_vdt_getlist_dmchiphi_by_qddautu @quyetDinhDauTuId", qdDauTuIdParam).ToList();
            }
        }

        public IEnumerable<NsNguonNganSach> GetListNguonVonByQDDauTu(Guid qdDauTuId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                List<NsNguonNganSach> listAllNguonVon = ctx.NguonNganSaches.ToList();
                List<NsNguonNganSach> result = new List<NsNguonNganSach>();
                List<VdtDaQddauTuNguonVon> listQdNguonVon = ctx.VdtDaQddauTuNguonVons.Where(x => x.IIdQddauTuId == qdDauTuId).ToList();

                if (listQdNguonVon != null && listQdNguonVon.Count > 0)
                {
                    List<int> listIdNguonVon = listQdNguonVon.Select(x => x.IIdNguonVonId.Value).ToList();
                    result = listAllNguonVon.Where(x => listIdNguonVon.Contains(x.IIdMaNguonNganSach.Value)).ToList();
                }
                return result;
            }
        }

        public void UpdateQDDauTuIdToDuAnHangMuc(Guid idDuAn, Guid idQDDauTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter duAnIdParam = new SqlParameter("@duAnId", idDuAn);
                SqlParameter qDDauTuIdParam = new SqlParameter("@qdDauTuId", idQDDauTu);
                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_vdt_update_duanhangmuc @duAnId,@qdDauTuId", duAnIdParam, qDDauTuIdParam);
            }
        }

        public IEnumerable<VdtDaQDNguonVonQuery> FindListQDDauTuNguonVon(Guid qdDauTuId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter qdDauTuIdParam = new SqlParameter("@qdDauTuId", qdDauTuId);
                return ctx.FromSqlRaw<VdtDaQDNguonVonQuery>("EXECUTE dbo.sp_vdt_get_all_pheduyet_nguonvon @qdDauTuId", qdDauTuIdParam).ToList();
            }
        }

        public IEnumerable<VdtDaQddtChiPhiQuery> FindListQDDauTuChiPhi(Guid qdDauTuId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter qdDauTuIdParam = new SqlParameter("@qdDauTuId", qdDauTuId);
                return ctx.FromSqlRaw<VdtDaQddtChiPhiQuery>("EXECUTE dbo.sp_vdt_get_all_pheduyet_chiphi @qdDauTuId", qdDauTuIdParam).ToList();
            }
        }

        public IEnumerable<VdtDaQDNguonVonQuery> FindListQDDauTuNguonVonByDuAn(Guid duAnId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter duAnIdParam = new SqlParameter("@duAnId", duAnId);
                return ctx.FromSqlRaw<VdtDaQDNguonVonQuery>("EXECUTE dbo.sp_vdt_get_all_pheduyet_nguonvon_by_duan @duAnId", duAnIdParam).ToList();
            }
        }

        public IEnumerable<VdtDaQddtChiPhiQuery> FindListQDDauTuChiPhiParent(Guid qdDauTuId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter qdDauTuIdParam = new SqlParameter("@qdDauTuId", qdDauTuId);
                return ctx.FromSqlRaw<VdtDaQddtChiPhiQuery>("EXECUTE dbo.sp_vdt_get_all_pheduyet_chiphi @qdDauTuId", qdDauTuIdParam).ToList();
            }
        }

        public IEnumerable<VdtDaQddtChiPhiQuery> FindListAllLoaiChiPhi()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtDaQddtChiPhiQuery>("EXECUTE dbo.sp_vdt_get_all_pheduyet_loaichiphi").ToList();
            }
        }

        public bool CheckExistInQDHangMuc(Guid qdDauTuId, Guid danhMucDuAnChiPhiId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                VdtDaQddauTuHangMuc qDHangMuc = ctx.VdtDaQddauTuHangMucs.Where(x => x.IIdQddauTuId == qdDauTuId && x.IIdDuAnChiPhi == danhMucDuAnChiPhiId).FirstOrDefault();
                if (qDHangMuc != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public IEnumerable<ApproveProjectDetailQuery> FindListDetailBeforeSave(Guid duAnId, Guid quyetDinhDauTuId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter duAnIdParam = new SqlParameter("@duAnId", duAnId);
                SqlParameter qdDauTuIdParam = new SqlParameter("@qdDauTuId", quyetDinhDauTuId);
                if (quyetDinhDauTuId == Guid.Empty)
                    qdDauTuIdParam.SqlValue = DBNull.Value;
                return ctx.FromSqlRaw<ApproveProjectDetailQuery>("EXECUTE dbo.sp_vdt_getall_pheduyetduanchitiet_hangmuc_beforesave @duAnId, @qdDauTuId", duAnIdParam, qdDauTuIdParam).ToList();
            }
        }

        public IEnumerable<ApproveProjectDetailQuery> FindListHangMucDieuChinhAdd(Guid quyetDinhDauTuId, Guid duAnChiPhiId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_getall_pheduyetduanchitiet_hangmuc_dieuchinh_add @quyetDinhDauTuId,@duAnChiPhiId";
                var parameters = new[]
                {
                    new SqlParameter("@quyetDinhDauTuId", quyetDinhDauTuId),
                    new SqlParameter("@duAnChiPhiId", duAnChiPhiId)
                };
                return ctx.FromSqlRaw<ApproveProjectDetailQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<ApproveProjectDetailQuery> FindListHangMucDieuChinhByQDDauTuAdd(Guid quyetDinhDauTuId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_getall_pheduyetduan_hangmuc_dieuchinh_by_qddtid_add @quyetDinhDauTuId";
                var parameters = new[]
                {
                    new SqlParameter("@quyetDinhDauTuId", quyetDinhDauTuId)
                };
                return ctx.FromSqlRaw<ApproveProjectDetailQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<ApproveProjectDetailQuery> FindListHangMucDieuChinhByQDDauTuUpdate(Guid quyetDinhDauTuId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_getall_pheduyetduan_hangmuc_dieuchinh_by_qddtid_update @quyetDinhDauTuId";
                var parameters = new[]
                {
                    new SqlParameter("@quyetDinhDauTuId", quyetDinhDauTuId)
                };
                return ctx.FromSqlRaw<ApproveProjectDetailQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<VdtDaQDNguonVonQuery> FindListQDDauTuNguonVonDieuChinh(Guid qdDauTuId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter qdDauTuIdParam = new SqlParameter("@qdDauTuId", qdDauTuId);
                return ctx.FromSqlRaw<VdtDaQDNguonVonQuery>("EXECUTE dbo.sp_vdt_get_all_pheduyet_nguonvon_dieuchinh @qdDauTuId", qdDauTuIdParam).ToList();
            }
        }

        public IEnumerable<VdtDaQddtChiPhiQuery> FindListQDDauTuChiPhiDieuChinhDefault(Guid qdDauTuId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter qdDauTuIdParam = new SqlParameter("@qdDauTuId", qdDauTuId);
                return ctx.FromSqlRaw<VdtDaQddtChiPhiQuery>("EXECUTE dbo.sp_vdt_get_all_pheduyet_chiphi_dieuchinh_add @qdDauTuId", qdDauTuIdParam).ToList();
            }
        }

        public IEnumerable<ApproveProjectDetailQuery> FindListAllHangMucByQDDauTu(Guid quyetDinhDauTuId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter quyetDinhDauTuIdParam = new SqlParameter("@quyetDinhDauTuId", quyetDinhDauTuId);
                return ctx.FromSqlRaw<ApproveProjectDetailQuery>("EXECUTE dbo.sp_vdt_getall_pheduyetduanchitiet_hangmuc_viewall_detail @quyetDinhDauTuId", quyetDinhDauTuIdParam).ToList();
            }
        }

        public IEnumerable<ApproveProjectQuery> FindListQDDauTuByDuAnId(Guid duAnId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter duAnIdParam = new SqlParameter("@duAnId", duAnId);
                return ctx.FromSqlRaw<ApproveProjectQuery>("EXECUTE dbo.sp_vdt_getall_qddautu_by_duan @duAnId", duAnIdParam).ToList();
            }
        }

        public IEnumerable<VdtDaQDNguonVonQuery> FindListQDDauTuNguonVonDieuChinhUpdate(Guid qdDauTuId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter qdDauTuIdParam = new SqlParameter("@qdDauTuId", qdDauTuId);
                return ctx.FromSqlRaw<VdtDaQDNguonVonQuery>("EXECUTE dbo.sp_vdt_get_all_pheduyet_nguonvon_dieuchinh_update @qdDauTuId", qdDauTuIdParam).ToList();
            }
        }

        public IEnumerable<VdtDaQddtChiPhiQuery> FindListQDDauTuChiPhiDieuChinhUpdate(Guid qdDauTuId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter qdDauTuIdParam = new SqlParameter("@qdDauTuId", qdDauTuId);
                return ctx.FromSqlRaw<VdtDaQddtChiPhiQuery>("EXECUTE dbo.sp_vdt_get_all_pheduyet_chiphi_dieuchinh_update @qdDauTuId", qdDauTuIdParam).ToList();
            }
        }

        public bool CheckChiPhiCoHangMuc(Guid chiPhiId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var listHangMucByChiPhiId = ctx.VdtDaQddauTuHangMucs.Where(x => x.IIdDuAnChiPhi == chiPhiId).ToList();
                if (listHangMucByChiPhiId.Count > 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
