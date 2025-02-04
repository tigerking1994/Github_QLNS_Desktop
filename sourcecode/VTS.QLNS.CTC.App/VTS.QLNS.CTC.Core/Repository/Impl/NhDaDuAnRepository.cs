using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using Microsoft.EntityFrameworkCore;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhDaDuAnRepository : Repository<NhDaDuAn>, INhDaDuAnRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDaDuAnRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhDaDuAn> GetDuAnHaveQDDauTu(Guid iIdKhlcntId, string sMaDonVi)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_nh_qddautu_get_duan_in_khlcnhathau @iId, @sMaDonVi";
                var parameters = new[]
                {
                    new SqlParameter("@iId", iIdKhlcntId),
                    new SqlParameter("@sMaDonVi", sMaDonVi)
                };
                return ctx.Set<NhDaDuAn>().FromSql(sql, parameters).ToList();
            }
        }
        public IEnumerable<NhDaDuAn> GetDuAnByDonVi(Guid iIdKhlcntId ,Guid Id, int iloai)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_nh_qddautu_get_duan_By_iID_DonViID @iIdKhlcntId, @iId,@iLoai";
                var parameters = new[]
                {
                     new SqlParameter("@iIdKhlcntId", iIdKhlcntId),
                    new SqlParameter("@iId", Id),
                    new SqlParameter("@iLoai", iloai),
                };
                return ctx.Set<NhDaDuAn>().FromSql(sql, parameters).ToList();
            }
        }

        public IEnumerable<NhDaDuAnQuery> FindIndex(int iLoai)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_nh_thongtinduan_index @iLoai"; 
                var parameters = new[] {
                    new SqlParameter("@iLoai", iLoai)
                };
                return ctx.FromSqlRaw<NhDaDuAnQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<NhDaDuAnQuery> FindFromChuTruongDauTu(int yearOfWork, string maDonVi, Guid? chuTruongDauTuId = null)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_nh_duan_find_from_chutruong_dautu @YearOfWork, @MaDonVi, @ChuTruongDauTuId";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", yearOfWork),
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@ChuTruongDauTuId", chuTruongDauTuId)
                };
                return ctx.FromSqlRaw<NhDaDuAnQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<NhDaDuAnQuery> FindFromQdDauTu(int yearOfWork, string maDonVi, int iLoai, Guid? qdDauTuId = null)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_nh_duan_find_from_qddautu @YearOfWork, @MaDonVi, @ILoai, @QdDauTuId";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", yearOfWork),
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@ILoai", iLoai),
                    new SqlParameter("@QdDauTuId", qdDauTuId)
                };
                return ctx.FromSqlRaw<NhDaDuAnQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<NhDaDuAnQuery> FindFromDuToan(int yearOfWork, string maDonVi, Guid? duToanId = null)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_nh_duan_find_from_dutoan @YearOfWork, @MaDonVi, @DuToanId";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", yearOfWork),
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@DuToanId", duToanId)
                };
                return ctx.FromSqlRaw<NhDaDuAnQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<NhDaDuAnTrongNuocQuery> FindDuAnTrongNuoc()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "";
                sql += "SELECT ";
                sql += "    duan.sTenDuAn AS STenDuAn, ";
                sql += "    duan.ID AS Id, ";
                sql += "    duan.iLoai as ILoai, ";
                sql += "    duan.iID_DonViQuanLyID as IIdDonViQuanLyId, ";
                sql += "    DM_ChuDauTu.iID_DonVi AS IIdChuDauTu, ";
                sql += "    DM_ChuDauTu.sTenDonVi AS STenChuDauTu, ";
                sql += "    DM_ChuDauTu.iID_MaDonVi AS SMaChudauTu, ";
                sql += "    SUM(CASE WHEN QDDauTu.fGiaTriUSD IS NOT NULL THEN QDDauTu.fGiaTriUSD ELSE DuToan.fGiaTriUSD END) AS TongMucDauTuUsd, ";
                sql += "    SUM(CASE WHEN QDDauTu.fGiaTriVND IS NOT NULL THEN QDDauTu.fGiaTriVND ELSE DuToan.fGiaTriVND END) AS TongMucDauTuVnd, ";
                sql += "    SUM(CASE WHEN QDDauTu.fGiaTriEUR IS NOT NULL THEN QDDauTu.fGiaTriEUR ELSE DuToan.fGiaTriEur END) AS TongMucDauTuEur, ";
                sql += "    SUM(CASE WHEN QDDauTu.fGiaTriNgoaiTeKhac IS NOT NULL THEN QDDauTu.fGiaTriNgoaiTeKhac ELSE DuToan.fGiaTriNgoaiTeKhac END) AS TongMucDauTuNgoaiTeKhac, ";
                sql += "    duan.sDiaDiem AS SDiaDiem, ";
                sql += "    duan.iID_KHTT_NhiemVuChiID AS IIdKHTTNhiemVuChiId, ";
                sql += "    CONCAT(DM_ChuDauTu.iID_MaDonVi, ' - ', DM_ChuDauTu.sTenDonVi) as TenChuDauTuDisplay ";
                sql += "FROM NH_DA_DuAn duan ";
                sql += "INNER JOIN NH_DA_KHLCNhaThau KHLCNhaThau ON duan.ID = KHLCNhaThau.iID_DuAnID ";
                sql += "INNER JOIN DM_ChuDauTu ON duan.iID_ChuDauTuID = DM_ChuDauTu.iID_DonVi ";
                sql += "LEFT JOIN NH_DA_QDDauTu QDDauTu  ON KHLCNhaThau.iID_QDDauTuID = QDDauTu.ID ";
                sql += "LEFT JOIN NH_DA_DuToan DuToan ON KHLCNhaThau.iID_DuToanID = DuToan.ID ";
                sql += "GROUP BY duan.ID, duan.sTenDuAn, DM_ChuDauTu.iID_DonVi, DM_ChuDauTu.sTenDonVi, DM_ChuDauTu.iID_MaDonVi, duan.sDiaDiem, duan.iID_KHTT_NhiemVuChiID, duan.iLoai, duan.iID_DonViQuanLyID";
                return ctx.FromSqlRaw<NhDaDuAnTrongNuocQuery>(sql).ToList();
            }
        }

        public IEnumerable<NhDaDuAnTinhHinhDuAnQuery> GetInfoDuAnTinhHinhDuAnReport(int yearOfWork, string maDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_nh_get_du_an_info_report @MaDonVi, @YearOfWork";
                var parameters = new[]
                {
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@YearOfWork", yearOfWork)
                };
                return ctx.FromSqlRaw<NhDaDuAnTinhHinhDuAnQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<NganSachNhDuAnInfoQuery> FindNganSachNgoaiHoiDuAnInfoByIdDuAn(string idDuAn)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_nh_get_du_an_info_ngansach @IdDuAn";
                var parameters = new[]
                {
                    new SqlParameter("@IdDuAn", idDuAn),
                };
                return ctx.FromSqlRaw<NganSachNhDuAnInfoQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<NhBaoCaoTinhHinhThucHienDuAnQuery> GetDataReportTinhHinhThucHienDuAn(string idDuAn, DateTime? ngayBatDau, DateTime? ngayKetThuc, string idHopDong, string idKhTongThe)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_nh_get_data_report_tinh_hinh_thuc_hien_du_an1 @IdDuAn, @NgayBatDau, @NgayKetThuc, @iID_HopDongID, @iID_KHTongTheID";
                var parameters = new[]
                {
                    new SqlParameter("@IdDuAn", idDuAn),
                    new SqlParameter("@NgayBatDau", ngayBatDau),
                    new SqlParameter("@NgayKetThuc", ngayKetThuc),
                    new SqlParameter("@iID_HopDongID", idHopDong),
                    new SqlParameter("@iID_KHTongTheID", idKhTongThe)
                };
                return ctx.FromSqlRaw<NhBaoCaoTinhHinhThucHienDuAnQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<ReportNHTongHopThongTinDuAnQuery> FindByAggregateProjectInformationReport(AggregateProjectInformationReportCriteria searchCondition)
        {
            
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = @"select DISTINCT
		                    t1.sTenDuAn as STenDuAn, 
		                    t1.iID_MaDonViQuanLy as SMaDonVi,
		                    t2.sSoQuyetDinh as SoQuyetDinhChuTruong, 
		                    t2.dNgayQuyetDinh as NgayQuyetDinhChuTruong, 
		                    t2.fGiaTriNgoaiTeKhac as FGiaTriKhacChuTruong, 
		                    t2.fGiaTriUSD as FGiaTriUSDChuTruong, 
		                    t2.fGiaTriEUR as FGiaTriEuroChuTruong, 
		                    t2.fGiaTriVND as FGiaTriVNDChuTruong,
		                    t3.sSoQuyetDinh as SoQuyetDinhDauTu,
		                    t3.dNgayQuyetDinh as NgayQuyetDinhDauTu, 
		                    t3.fGiaTriNgoaiTeKhac as FGiaTriKhacDauTu, 
		                    t3.fGiaTriUSD as FGiaTriUSDDauTu, 
		                    t3.fGiaTriEUR as FGiaTriEuroDauTu, 
		                    t3.fGiaTriVND as FGiaTriVNDDauTu,
		                    t4.sTenDonVi as TenDonVi
	                    from NH_DA_DuAn t1
	                    left join NH_DA_ChuTruongDauTu t2 on t1.ID = t2.iID_DuAnID
	                    left join NH_DA_QDDauTu t3 on t1.ID = t3.iID_DuAnID
	                    left join DonVi t4 on t4.iID_MaDonVi = t1.iID_MaDonViQuanLy";
                
                if(searchCondition == null)
                {
                    return ctx.FromSqlRaw<ReportNHTongHopThongTinDuAnQuery>(sql).ToList();
                } else
                {
                    string strWhere = "  and t4.iNamLamViec = " + searchCondition.NamKeHoach + " where t1.iID_MaDonViQuanLy " + (searchCondition.IdMaDonViQuanLy == null ? " is not null" : ("= " + searchCondition.IdMaDonViQuanLy));
                    return ctx.FromSqlRaw<ReportNHTongHopThongTinDuAnQuery>(sql + strWhere).ToList();
                }
            }
          
        }
        
        public IEnumerable<NhDaDuAn> FindAllDuAnByQDDT()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var QDDTDuAnIds = ctx.NhDaQdDauTu.Select(t => t.IIdDuAnId).ToList();
                return ctx.NhDaDuAn.Where(t => QDDTDuAnIds.Contains(t.Id)).ToList();
            }
        }

        public IEnumerable<NhDaDuAn> FindAll(AuthenticationInfo authenticationInfo)
        {
            return FindAll();
        }

        public void AddOrUpdateRange(IEnumerable<NhDaDuAn> entities, AuthenticationInfo authenticationInfo)
        {
            var time = DateTime.Now;
            using (var ctx = _contextFactory.CreateDbContext())
            {
                foreach (var entity in entities)
                {
                    var current = ctx.NhDaDuAn.AsNoTracking().FirstOrDefault(i => i.Id.Equals(entity.Id));
                    if (entity.IsDeleted)
                    {
                        if (!entity.Id.Equals(Guid.Empty))
                        {
                            ctx.Remove(entity);
                        }
                    }
                    else
                    {
                        if (!entity.Id.Equals(Guid.Empty) && current != null)
                        {
                            ctx.Update(entity);
                        }
                        else
                        {
                            entity.DNgayTao = time;
                            entity.SNguoiTao = authenticationInfo.Principal;
                            ctx.Add(entity);
                        }
                    }
                }
                ctx.SaveChanges();
            }
        }
        public IEnumerable<NhDaDuAnExportCTCQuery> GetDuAnExportCTC(int iLoai)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_nh_get_duan_export_ctc @iLoai";
                var parameters = new[] {
                    new SqlParameter("@iLoai", iLoai)
                };
                return ctx.FromSqlRaw<NhDaDuAnExportCTCQuery>(sql, parameters).ToList();
            }
        }
    }
}
