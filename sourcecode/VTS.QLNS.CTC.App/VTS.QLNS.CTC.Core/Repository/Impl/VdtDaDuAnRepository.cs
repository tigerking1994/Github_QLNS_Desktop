using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtDaDuAnRepository : Repository<VdtDaDuAn>, IVdtDaDuAnRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtDaDuAnRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<DeNghiQuyetToanQuery> FindAllDeNghiQuyetToan(int namLamViec, string userName)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter userNameParam = new SqlParameter("@UserName", userName);
                return ctx.FromSqlRaw<DeNghiQuyetToanQuery>("EXECUTE dbo.sp_vdt_dnqt_get_denghiquyetoan_2 @NamLamViec, @UserName", namLamViecParam, userNameParam).ToList();
            }
        }

        public IEnumerable<VdtDaDuAnQuery> FindByIdDonViAndNgayQuyetDinh(string idDonVi, DateTime ngayQuyetDinh)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter idDonViParam = new SqlParameter("@IdDonVi", idDonVi);
                SqlParameter ngayQuyetDinhParam = new SqlParameter("@NgayQuyetDinh", ngayQuyetDinh);
                return ctx.FromSqlRaw<VdtDaDuAnQuery>("EXECUTE dbo.sp_vdt_get_phe_duyet_quyet_toan_info @IdDonVi, @NgayQuyetDinh", idDonViParam, ngayQuyetDinhParam).ToList();
            }
        }

        public IEnumerable<VdtDaDuAnQuery> FindByIdDonVi(string idDonVi)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter idDonViParam = new SqlParameter("@IdDonVi", idDonVi);
                return ctx.FromSqlRaw<VdtDaDuAnQuery>("EXECUTE dbo.sp_vdt_get_du_an_info_hopdong @IdDonVi", idDonViParam).ToList();
            }
        }

        public IEnumerable<VdtDaDuAnReportQuery> FindDuAnInfoByIdDonVi(string idDonVi)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter idDonViParam = new SqlParameter("@IdDonVi", idDonVi);
                return ctx.FromSqlRaw<VdtDaDuAnReportQuery>("EXECUTE dbo.sp_vdt_get_du_an_info_report @IdDonVi", idDonViParam).ToList();
            }
        }

        public IEnumerable<NganSachDuAnInfoQuery> FindNganSachDuAnInfoByIdDuAn(string idDuAn)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter idDuAnParam = new SqlParameter("@IdDuAn", idDuAn);
                return ctx.FromSqlRaw<NganSachDuAnInfoQuery>("EXECUTE dbo.sp_vdt_get_du_an_info_ngansach @IdDuAn", idDuAnParam).ToList();
            }
        }

        public IEnumerable<VdtDaDuAn> FindByIdDonViQuanLy(string idDonVi)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDaDuAns.Where(n => n.IIdMaDonViThucHienDuAn == idDonVi).ToList();
            }
        }

        public List<DuAnKeHoachTrungHanQuery> GetDuAnChooseInKeHoachTrungHan()
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                //return ctx.FromSqlRaw<DuAnKeHoachTrungHanQuery>("EXECUTE dbo.sp_vdt_kehoach5nam_chitiet_chooseduan").ToList();
                return ctx.FromSqlRaw<DuAnKeHoachTrungHanQuery>("EXECUTE dbo.sp_vdt_kehoach5nam_chitiet_chooseduan_test").ToList();
            }
        }

        public IEnumerable<ReportTinhHinhDuAnQuery> GetDataReportTinhHinhDuAn(string idDuAn, DateTime ngayBaoCao)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter idDuAnParam = new SqlParameter("@IdDuAn", idDuAn);
                SqlParameter ngayBaoCaoParam = new SqlParameter("@NgayDeNghi", ngayBaoCao);
                return ctx.FromSqlRaw<ReportTinhHinhDuAnQuery>("EXECUTE dbo.sp_vdt_get_data_report_tinh_hinh_du_an @IdDuAn, @NgayDeNghi", idDuAnParam, ngayBaoCaoParam).ToList();
            }
        }

        public IEnumerable<ReportTinhHinhDuAnQuery> GetDataReportTinhHinhDuAnV1(string idDuAn, DateTime ngayBaoCao)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter idDuAnParam = new SqlParameter("@IdDuAn", idDuAn);
                SqlParameter ngayBaoCaoParam = new SqlParameter("@NgayDeNghi", ngayBaoCao);
                return ctx.FromSqlRaw<ReportTinhHinhDuAnQuery>("EXECUTE dbo.sp_vdt_baocaotinhhinhduan @IdDuAn, @NgayDeNghi", idDuAnParam, ngayBaoCaoParam).ToList();
            }
        }

        public int FindNextSoChungTuIndex()
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var currentIndex = ctx.VdtDaDuAns
               .Max(x => x.IMaDuAnIndex);
                return currentIndex != null ? (int)currentIndex + 1 : 1;
            }
        }

        public VdtDaDuAn FindById(Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDaDuAns.Where(x => x.Id == id).FirstOrDefault();
            }
        }

        public VdtDaDuAn FindByMaDuAn(string sMaDuAn)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDaDuAns.Where(x => x.SMaDuAn == sMaDuAn).FirstOrDefault();
            }
        }

        public IEnumerable<VdtDaDuAn> FindByIdDuAnKhthDeXuat(Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDaDuAns.Where(x => x.IdDuAnKhthDeXuat == id).ToList();
            }
        }

        public void CreateDuAn(string lstId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_vdt_duan_creation @lstId",
                                    new SqlParameter("@lstId", lstId));
            }
        }

        public List<VdtDaDuAn> FindByChuDauTuId(Guid chuDauTuId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDaDuAns.Where(x => x.IIdChuDauTuId == chuDauTuId).ToList();
            }
        }

        public List<VdtDaDuAn> FindByChuDauTuByMaChuDauTu(string maChuDauTu)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDaDuAns.Where(x => x.IIdMaChuDauTuId == maChuDauTu).ToList();
            }
        }

        public List<DuAnKeHoachTrungHanQuery> GetDuAnChooseInKeHoachTrungHanDeXuat(string iIdDuAn, int type)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<DuAnKeHoachTrungHanQuery>("EXECUTE dbo.sp_vdt_kehoach5nam_chitiet_chooseduandexuat @iID_DuAn,@type",
                    new SqlParameter("iID_DuAn", iIdDuAn),
                    new SqlParameter("type", type)).ToList();
            }
        }

        public IEnumerable<VdtDaDuAn> GetDuAnInQuyetToanDuAnHoanThanh(string iIdMaDonViQuanLy, Guid iIdQuyetToanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var executeQuery = "EXECUTE dbo.sp_vdt_quyettoanduanhoanthanh_get_duan @iIdMaDonViQuanLy, @iIdQuyetToanId";
                var parameters = new[]
                {
                    new SqlParameter("iIdMaDonViQuanLy", iIdMaDonViQuanLy),
                    new SqlParameter("iIdQuyetToanId", iIdQuyetToanId)
                };
                return ctx.Set<VdtDaDuAn>().FromSql(executeQuery, parameters).ToList();
            }
        }

        public IEnumerable<VdtDaDuAn> FindByDonvi(string maDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var executeQuery = "select * from VDT_DA_DuAn where iID_MaDonViThucHienDuAnID in ( select * from f_recursive_donvi(@iIdDonViQuanLy))";
                var parameters = new[]
                {
                    new SqlParameter("iIdDonViQuanLy", maDonVi),
                };
                return ctx.VdtDaDuAns.FromSql(executeQuery, parameters).ToList();
            }
        }
        
        public IEnumerable<VdtDaDuAn> FindDuanCreatedKHLCNT(string maDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var executeQuery = "select * from VDT_DA_DuAn where iID_MaDonViThucHienDuAnID in ( select * from f_recursive_donvi(@iIdDonViQuanLy)) and iID_DuAnID in (select iID_DuAnID from VDT_QDDT_KHLCNhaThau);";
                var parameters = new[]
                {
                    new SqlParameter("iIdDonViQuanLy", maDonVi),
                };
                return ctx.VdtDaDuAns.FromSql(executeQuery, parameters).ToList();
            }
        }
    }
}