
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility.Criteria;
using Dapper;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtKhvPhanBoVonChiTietRepository : Repository<VdtKhvPhanBoVonChiTiet>, IVdtKhvPhanBoVonChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtKhvPhanBoVonChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<PhanBoVonChiTietQuery> GetAllDuAnInPhanBoVon(string idPhanBoVonDeXuat, int iNguonVonId)
        {
            try
            {
                using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
                {
                    return ctx.FromSqlRaw<PhanBoVonChiTietQuery>("EXECUTE dbo.sp_vdt_get_duan_in_phanbovon_new @idPhanBoVonDeXuat, @nguonVonID",
                        new SqlParameter("@idPhanBoVonDeXuat", idPhanBoVonDeXuat),
                        new SqlParameter("@nguonVonID", iNguonVonId)).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<PhanBoVonChiTietQuery>();
            }
        }

        public IEnumerable<PhanBoVonChiTietQuery> GetAllDuAnInPhanBoVonByEdit(string idPhanBoVonDeXuat, int iNguonVonId)
        {
            try
            {
                using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
                {
                    return ctx.FromSqlRaw<PhanBoVonChiTietQuery>("EXECUTE dbo.sp_vdt_get_duan_in_phanbovon_new_2 @idPhanBoVonDeXuat, @nguonVonID",
                        new SqlParameter("@idPhanBoVonDeXuat", idPhanBoVonDeXuat),
                        new SqlParameter("@nguonVonID", iNguonVonId)).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<PhanBoVonChiTietQuery>();
            }
        }

        public IEnumerable<MucLucNganSachChungTuThanhToanQuery> GetAllMucLucNganSachByDuAnId(List<TongHopNguonNSDauTuQuery> lstData)
        {
            try
            {
                using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
                {
                    var executeQuery = "EXECUTE dbo.sp_pbv_get_muclucngansach_by_khv_thanhtoan @tblKHV";
                    var conditions = DBExtension.ConvertDataToTableDefined("t_tbl_tonghopdautu", lstData);
                    var parameters = new[]
                    {
                        new SqlParameter("@tblKHV", conditions.AsTableValuedParameter("t_tbl_tonghopdautu"))
                    };
                    return ctx.FromSqlRaw<MucLucNganSachChungTuThanhToanQuery>(executeQuery, parameters).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<PhanBoVonChiTietQuery> GetPhanBoVonChiTietByParentId(Guid iIdPhanBoVonChiTiet, DateTime? dNgayQuyetDinh)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_find_phanbovonchitiet @iIdPhanBoVon, @dNgayLap";
                var parameters = new[]
                {
                        new SqlParameter("@iIdPhanBoVon", iIdPhanBoVonChiTiet),
                        new SqlParameter("@dNgayLap", dNgayQuyetDinh)
                    };
                return ctx.FromSqlRaw<PhanBoVonChiTietQuery>(sql, parameters).ToList();
            }
        }

        public DuAnInfoQuery GetVonDaBoTriByDuAnIdAnMucLucNganSach(int iLoai, Guid iIdDuAn, string iIdDonViQuanLy, int iNamKeHoach, DateTime dNgayLap, Guid iIdLoaiNguonVon, int iIdNguonVon, string sL, string sK, string sM, string sTM, string sTTM, string sNG)
        {
            try
            {
                using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
                {
                    SqlParameter LParam = new SqlParameter("L", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(sL) ? string.Empty : sL };
                    SqlParameter KParam = new SqlParameter("K", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(sK) ? string.Empty : sK };
                    SqlParameter MParam = new SqlParameter("M", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(sM) ? string.Empty : sM };
                    SqlParameter TMParam = new SqlParameter("TM", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(sTM) ? string.Empty : sTM };
                    SqlParameter TTMParam = new SqlParameter("TTM", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(sTTM) ? string.Empty : sTTM };
                    SqlParameter MGParam = new SqlParameter("NG", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(sNG) ? string.Empty : sNG };

                    return ctx.FromSqlRaw<DuAnInfoQuery>("EXECUTE sp_vdt_phanbovon_get_info_duan_by_nganh @iLoai, @iNamKeHoach, @iDonViQuanLy, @dNgayLap, @iIdDuAn, @iLoaiNguonVon, @iNguonVon, @L, @K, @M, @TM, @TTM, @NG",
                        new SqlParameter("iLoai", iLoai),
                        new SqlParameter("iNamKeHoach", iNamKeHoach),
                        new SqlParameter("iDonViQuanLy", iIdDonViQuanLy),
                        new SqlParameter("dNgayLap", dNgayLap),
                        new SqlParameter("iIdDuAn", iIdDuAn),
                        new SqlParameter("iLoaiNguonVon", iIdLoaiNguonVon),
                        new SqlParameter("iNguonVon", iIdNguonVon),
                        LParam, KParam, MParam, TMParam, TTMParam, MGParam).ToList().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return new DuAnInfoQuery();
            }
        }

        public IEnumerable<VdtKhvPhanBoVonChiTiet> GetPhanBoVonChiTietByIidPhanBoVonID(Guid iIdPhanBoVonId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtKhvPhanBoVonChiTiets.Where(n => n.IIdPhanBoVonId == iIdPhanBoVonId).ToList();
            }
        }

        public bool CreatePhanBoVonChiTiet(int iLoaiKeHoach, DataTable dt, string sUserLogin, bool bIsEdit)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var executeQuery = "EXEC sp_vdt_insert_phanbovonchitiet @bIsEdit, @sUserLogin, @tbl_PhanBoVonChiTiet, @sTypeError OUT";
                SqlParameter iErrorTypeParam = new SqlParameter("sTypeError", SqlDbType.Int);
                iErrorTypeParam.Direction = ParameterDirection.Output;

                SqlParameter dtDetailParam = new SqlParameter("tbl_PhanBoVonChiTiet", SqlDbType.Structured);
                dtDetailParam.TypeName = "t_tbl_phanbovonchitiet5";
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

        public bool CreatePhanBoVonChiTietClone(int iLoaiKeHoach, DataTable dt, string sUserLogin, bool bIsEdit)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var executeQuery = "EXEC sp_vdt_insert_phanbovonchitiet_clone @bIsEdit, @sUserLogin, @tbl_PhanBoVonChiTiet, @sTypeError OUT";
                SqlParameter iErrorTypeParam = new SqlParameter("sTypeError", SqlDbType.Int);
                iErrorTypeParam.Direction = ParameterDirection.Output;

                SqlParameter dtDetailParam = new SqlParameter("tbl_PhanBoVonChiTiet", SqlDbType.Structured);
                dtDetailParam.TypeName = "t_tbl_phanbovonchitiet5";
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

        public bool CreatePhanBoVonChiTietNew(int iLoaiKeHoach, DataTable dt, string sUserLogin, bool bIsEdit)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var executeQuery = "EXEC sp_vdt_insert_phanbovonchitiet_new @bIsEdit, @sUserLogin, @tbl_PhanBoVonChiTiet, @sTypeError OUT";
                SqlParameter iErrorTypeParam = new SqlParameter("sTypeError", SqlDbType.Int);
                iErrorTypeParam.Direction = ParameterDirection.Output;

                SqlParameter dtDetailParam = new SqlParameter("tbl_PhanBoVonChiTiet", SqlDbType.Structured);
                dtDetailParam.TypeName = "t_tbl_phanbovonchitiet7";
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

        public int RemovePhanBoVonChiTiet(VdtKhvPhanBoVonChiTiet data)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                ctx.VdtKhvPhanBoVonChiTiets.Remove(data);
                return ctx.SaveChanges();
            }
        }

        public int Update(IEnumerable<VdtKhvPhanBoVonChiTiet> datas)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                foreach (var item in datas)
                {
                    ctx.VdtKhvPhanBoVonChiTiets.Update(item);
                }
                return ctx.SaveChanges();
            }
        }

        public int RemovePhanBoVonChiTiet(IEnumerable<VdtKhvPhanBoVonChiTiet> datas)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                ctx.VdtKhvPhanBoVonChiTiets.RemoveRange(datas);
                return ctx.SaveChanges();
            }
        }

        public IEnumerable<VdtKhvPhanBoVonChiTietProAdjustementReportQuery> FindByProjectAdjustementReport(ProjectAdjustementSearch condition)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtKhvPhanBoVonChiTietProAdjustementReportQuery>("EXECUTE dbo.sp_vdt_rpt_DieuChinhKeHoach_ChiTiet "
                    + "@NguonVonId, @NamKeHoach, @LoaiNguonVon, @userName",
                    new SqlParameter("@NguonVonId", condition.NguonVonId),
                    new SqlParameter("@NamKeHoach", condition.NamKeHoach),
                    new SqlParameter("@LoaiNguonVon", condition.LoaiNguonVon),
                    new SqlParameter("@userName", condition.UserName)).ToList();
            }
        }

        public IEnumerable<VdtKhvPhanBoVonChiTietProAdjustementReportQuery> FindByProjectAdjustementReportParent(ProjectAdjustementSearch condition)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtKhvPhanBoVonChiTietProAdjustementReportQuery>("EXECUTE dbo.sp_vdt_rpt_DieuChinhKeHoach_ChiTiet_Parent "
                    + "@namKeHoach, @nguonVonID, @loaiNguonVonID",
                    new SqlParameter("@namKeHoach", condition.NamKeHoach),
                    new SqlParameter("@nguonVonID", condition.NguonVonId),
                    new SqlParameter("@loaiNguonVonID", condition.LoaiNguonVon)).ToList();
            }
        }

        public IEnumerable<RptDieuChinhKeHoachQuery> GetRptDieuChinhKeHoach(int iIdNguonVonId, int iNamKeHoach, string sLNS, string sUserLogin)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<RptDieuChinhKeHoachQuery>("EXECUTE sp_vdt_baocao_dieuchinhkehoach @iIdNguonVonId, @iNamKeHoach, @sLNS, @userName",
                    new SqlParameter("iIdNguonVonId", iIdNguonVonId),
                    new SqlParameter("iNamKeHoach", iNamKeHoach),
                    new SqlParameter("sLNS", sLNS),
                    new SqlParameter("userName", sUserLogin)).ToList();
            }
        }

        public IEnumerable<KeHoachNamDuocDuyetDetailQuery> GetAllKeHoachVonNamDuocDuyetDetail(DateTime dNgayLap, string iIdMaDonViQuanLyId, int iNguonVonId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<KeHoachNamDuocDuyetDetailQuery>("EXECUTE sp_vdt_get_duan_in_kehoachvonnam_duocduyet @ngayLap, @maDonViQuanLyId, @nguonVonID",
                    new SqlParameter("ngayLap", dNgayLap),
                    new SqlParameter("maDonViQuanLyId", iIdMaDonViQuanLyId),
                    new SqlParameter("nguonVonID", iNguonVonId)).ToList();
            }

        }

        public IEnumerable<KeHoachNamDuocDuyetDetailQuery> GetKeHoachVonNamDuocDuyetByParentId(Guid iIdPhanBoVonChiTiet)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<KeHoachNamDuocDuyetDetailQuery>("EXECUTE sp_vdt_find_kehoachvonnamduocduyet_chitiet @iIdPhanBoVon",
                    new SqlParameter("iIdPhanBoVon", iIdPhanBoVonChiTiet)).ToList();
            }
        }

        public IEnumerable<VdtKhvVonNamDuocDuyetQuery> GetKeHoachVonNamDuocDuyetReport(string listId, string lct, int type, int loaiDuAn, string lstDonVi, double donViTinh)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtKhvVonNamDuocDuyetQuery>("EXECUTE sp_vdt_khv_kehoach_von_nam_duoc_duyet_export @lstId, @lct, @type,@loaiDuAn,@lstDonVi,@MenhGiaTienTe",
                                        new SqlParameter("lstId", listId),
                                        new SqlParameter("lct", lct),
                                        new SqlParameter("type", type),
                                        new SqlParameter("loaiDuAn", loaiDuAn),
                                        new SqlParameter("lstDonVi", lstDonVi),
                                        new SqlParameter("MenhGiaTienTe", donViTinh)).ToList();
            }
        }

        public void CreateSettlementVoucherApprovedDetail(MidiumTermPlanCriteria creation)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_vdt_create_phan_bo_von_duoc_duyet_chi_tiet @iID_KePhanBoVonDonViF, @iID_KePhanBoVonDonViL",
                    new SqlParameter("@iID_KePhanBoVonDonViL", creation.VocherIDL),
                    new SqlParameter("@iID_KePhanBoVonDonViF", creation.VocherIDF));
            }
        }

        public IEnumerable<PhanBoVonDuocDuyetChiTietQuery> GetPhanBoVonChiTietDieuChinhByParentId(Guid idPhanBoVon)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<PhanBoVonDuocDuyetChiTietQuery>("EXECUTE sp_vdt_find_kehoachvonnamduocduyet_dieuchinh_chitiet @iIdPhanBoVon",
                    new SqlParameter("iIdPhanBoVon", idPhanBoVon)).ToList();
            }
        }

        public IEnumerable<PhanBoVonDuocDuyetChiTietQuery> GetPhanBoVonChiTietParent(Guid idPhanBoVon)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<PhanBoVonDuocDuyetChiTietQuery>("EXECUTE sp_vdt_find_kehoachvonnamduocduyet_parent_chitiet @iIdPhanBoVon",
                    new SqlParameter("iIdPhanBoVon", idPhanBoVon)).ToList();
            }
        }

        public IEnumerable<VdtKhvPhanBoVonChiTiet> GetKeHoachVonNamDuocDuyet(YearPlanCriteria condition)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtKhvPhanBoVonChiTiets.Where(x => x.IIdDuAnId == condition.IIdDuAn
                                                            && x.Lns == condition.sLNS
                                                            && x.L == condition.sL
                                                            && x.K == condition.sK
                                                            && x.M == condition.sM
                                                            && x.Tm == condition.sTM
                                                            && x.Ttm == condition.sTTM
                                                            && x.Ng == condition.sNG).ToList();
            }
        }

        public IEnumerable<ChiTieuNganSachQuery> GetChiTieuNganSach(string idDuAn, DateTime dNgayQuyetDinh)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<ChiTieuNganSachQuery>("EXECUTE sp_vdt_chi_tieu_ngan_sach @idDuAn, @dNgayQuyetDinh",
                    new SqlParameter("@idDuAn", idDuAn),
                    new SqlParameter("@dNgayQuyetDinh", dNgayQuyetDinh)).ToList();
            }
        }
    }
}
