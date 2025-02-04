using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class BhThamDinhQuyetToanChiTietRepository : Repository<BhThamDinhQuyetToanChiTiet>, IBhThamDinhQuyetToanChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public BhThamDinhQuyetToanChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhThamDinhQuyetToanChiTiet> FindAllOfLastYear(int yearOfWork, string iIDMaDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhThamDinhQuyetToanChiTiets.Where(n => n.INamLamViec == yearOfWork && n.IID_MaDonVi == iIDMaDonVi).ToList();
            }
        }

        public IEnumerable<BhThamDinhQuyetToanChiTietQuery> FindAll(Guid iIDChungTu, int yearOfWork, string iIDMaDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter idChungTuParam = new SqlParameter("@IdChungTu", iIDChungTu);
                SqlParameter iNamLamViecParam = new SqlParameter("@INamLamViec ", yearOfWork);
                SqlParameter iMaDonViParam = new SqlParameter("@IdDonVi ", iIDMaDonVi);

                return ctx.FromSqlRaw<BhThamDinhQuyetToanChiTietQuery>("EXECUTE sp_bh_thamdinhquyettoan_chitiet @IdChungTu, @INamLamViec, @IdDonVi ",
                    idChungTuParam, iNamLamViecParam, iMaDonViParam).ToList();
            }
        }

        public IEnumerable<BhThamDinhQuyetToanChiTietQuery> FindAll(int yearOfWork, string iIDMaDonVi, int dvt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@INamLamViec ", yearOfWork);
                SqlParameter iMaDonViParam = new SqlParameter("@IdDonVi ", iIDMaDonVi);
                SqlParameter iDonViTinh = new SqlParameter("@DonViTinh ", dvt);

                return ctx.FromSqlRaw<BhThamDinhQuyetToanChiTietQuery>("EXECUTE sp_rpt_bh_thamdinhquyettoan_chitiet @INamLamViec, @IdDonVi, @DonViTinh ",
                     iNamLamViecParam, iMaDonViParam, iDonViTinh).ToList();
            }
        }

        public IEnumerable<BhThamDinhQuyetToanChiTTBYTQuery> GetChiKinhPhiTTBYT(int yearOfWork, string iIDMaDonVi, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@INamLamViec ", yearOfWork);
                SqlParameter iMaDonViParam = new SqlParameter("@IdDonVi ", iIDMaDonVi);
                SqlParameter iDonViTinhParam = new SqlParameter("@DonViTinh ", donViTinh);

                return ctx.FromSqlRaw<BhThamDinhQuyetToanChiTTBYTQuery>("EXECUTE sp_rpt_bh_thamdinhquyettoan_kinhphi_ttbyt @INamLamViec, @IdDonVi,@DonViTinh ",
                     iNamLamViecParam, iMaDonViParam, iDonViTinhParam).ToList();
            }
        }

        public IEnumerable<BhThamDinhQuyetToanChiCheDoBHXHQuery> GetChiKinhPhiCheDoBHXH(int yearOfWork, string iIDMaDonVi, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@INamLamViec ", yearOfWork);
                SqlParameter iMaDonViParam = new SqlParameter("@IdDonVi ", iIDMaDonVi);
                SqlParameter iDonViTinhParam = new SqlParameter("@DonViTinh ", donViTinh);
                return ctx.FromSqlRaw<BhThamDinhQuyetToanChiCheDoBHXHQuery>("EXECUTE sp_rpt_bh_thamdinhquyettoan_kinhphi_chedo_bhxh @INamLamViec, @IdDonVi,@DonViTinh ",
                     iNamLamViecParam, iMaDonViParam, iDonViTinhParam).ToList();
            }
        }

        public IEnumerable<BhThamDinhQuyetToanChiKCBHSSVNLDQuery> GetChiKinhPhiCSSKHSSVNLD(int yearOfWork, string iIDMaDonVi, int iLoai, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@INamLamViec ", yearOfWork);
                SqlParameter iMaDonViParam = new SqlParameter("@IdDonVi ", iIDMaDonVi);
                SqlParameter iLoaiParam = new SqlParameter("@Loai ", iLoai);
                SqlParameter iDonViTinhParam = new SqlParameter("@DonViTinh ", donViTinh);

                return ctx.FromSqlRaw<BhThamDinhQuyetToanChiKCBHSSVNLDQuery>("EXECUTE sp_rpt_bh_thamdinhquyettoan_kinhphi_cssk_hssv_nld @INamLamViec, @IdDonVi, @Loai,@DonViTinh ",
                     iNamLamViecParam, iMaDonViParam, iLoaiParam, iDonViTinhParam).ToList();
            }
        }

        public IEnumerable<BhThamDinhQuyetToanChiKCBQuanYDonViQuery> GetChiKinhPhiKCBQuanYDonVi(int yearOfWork, string iIDMaDonVi, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@INamLamViec ", yearOfWork);
                SqlParameter iMaDonViParam = new SqlParameter("@IdDonVi ", iIDMaDonVi);
                SqlParameter iDonViTinhParam = new SqlParameter("@DonViTinh ", donViTinh);

                return ctx.FromSqlRaw<BhThamDinhQuyetToanChiKCBQuanYDonViQuery>("EXECUTE sp_rpt_bh_thamdinhquyettoan_kinhphi_kcb_quanydonvi @INamLamViec, @IdDonVi,@DonViTinh ",
                     iNamLamViecParam, iMaDonViParam, iDonViTinhParam).ToList();
            }
        }

        public void CreateVoudcherSummary(string idChungTu, string nguoiTao, int namLamViec, string idChungTuSummary)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_create_summary_tham_dinh_quyet_toan @IdChungTu, @NguoiTao, @YearOfWork, @IdChungTuSummary";
                var parameters = new[]
                {
                    new SqlParameter("@IdChungTu", idChungTu),
                    new SqlParameter("@NguoiTao", nguoiTao),
                    new SqlParameter("@YearOfWork", namLamViec),
                    new SqlParameter("@IdChungTuSummary", idChungTuSummary)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQuyetToanThuBhxhBhtn(int namLamViec, string lstDonvi, int dvt, bool isBHXH)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec ", namLamViec);
                SqlParameter lstDonViParam = new SqlParameter("@LstSelectedUnit ", lstDonvi);
                SqlParameter dvtParam = new SqlParameter("@Dvt ", dvt);
                SqlParameter isBHXHParam = new SqlParameter("@IsBHXH ", isBHXH);

                return ctx.FromSqlRaw<BhQttBHXHChiTietQuery>("EXECUTE sp_bh_rpt_thamdinh_quyet_toan_thu_bhxh_bhtn @NamLamViec, @LstSelectedUnit, @Dvt, @IsBHXH ", iNamLamViecParam, lstDonViParam, dvtParam, isBHXHParam).ToList();
            }
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQuyetToanThuBhyt(int namLamViec, string lstDonvi, int dvt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec ", namLamViec);
                SqlParameter lstDonViParam = new SqlParameter("@LstSelectedUnit ", lstDonvi);
                SqlParameter dvtParam = new SqlParameter("@Dvt ", dvt);

                return ctx.FromSqlRaw<BhQttBHXHChiTietQuery>("EXECUTE sp_bh_rpt_thamdinh_quyet_toan_thu_bhyt_nld @NamLamViec, @LstSelectedUnit, @Dvt", iNamLamViecParam, lstDonViParam, dvtParam).ToList();
            }
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQuyetToanThuBhytQuanNhan(int namLamViec, string lstDonvi, int dvt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec ", namLamViec);
                SqlParameter lstDonViParam = new SqlParameter("@LstSelectedUnit ", lstDonvi);
                SqlParameter dvtParam = new SqlParameter("@Dvt ", dvt);

                return ctx.FromSqlRaw<BhQttBHXHChiTietQuery>("EXECUTE sp_bh_rpt_thamdinh_quyet_toan_thu_bhyt_quannhan @NamLamViec, @LstSelectedUnit, @Dvt", iNamLamViecParam, lstDonViParam, dvtParam).ToList();
            }
        }

        public IEnumerable<BhQttmBHYTChiTietQuery> ExportQuyetToanThuBhytThanNhan(int namLamViec, string lstDonvi, int dvt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec ", namLamViec);
                SqlParameter lstDonViParam = new SqlParameter("@LstSelectedUnit ", lstDonvi);
                SqlParameter dvtParam = new SqlParameter("@Dvt ", dvt);

                return ctx.FromSqlRaw<BhQttmBHYTChiTietQuery>("EXECUTE sp_bh_rpt_thamdinh_quyet_toan_thu_bhyt_thannhan @NamLamViec, @LstSelectedUnit, @Dvt", iNamLamViecParam, lstDonViParam, dvtParam).ToList();
            }
        }

        public IEnumerable<BhQttmBHYTChiTietQuery> ExportQuyetToanThuBhytHssvHvqs(int namLamViec, string lstDonvi, int dvt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec ", namLamViec);
                SqlParameter lstDonViParam = new SqlParameter("@LstSelectedUnit ", lstDonvi);
                SqlParameter dvtParam = new SqlParameter("@Dvt ", dvt);

                return ctx.FromSqlRaw<BhQttmBHYTChiTietQuery>("EXECUTE sp_bh_rpt_thamdinh_quyet_toan_thu_bhyt_hssv_hvqs_sqdb @NamLamViec, @LstSelectedUnit, @Dvt", iNamLamViecParam, lstDonViParam, dvtParam).ToList();
            }
        }

        public IEnumerable<BhReportQttBHXHChiTietQuery> ExportThongBaoPheDuyetThuChi(int iNamLamViec, string sIdDonVis, int donViTinh, int type)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter sIdDonViParam = new SqlParameter("@LstMaDonVi", sIdDonVis);
                SqlParameter donViTinhParam = new SqlParameter("@DVT", donViTinh);
                SqlParameter iType = new SqlParameter("@Type ", type);
                return ctx.FromSqlRaw<BhReportQttBHXHChiTietQuery>("EXECUTE dbo.sp_bh_report_tham_dinh_quyet_toan_tong_hop_thu_chi "
                    + "@NamLamViec, @LstMaDonVi, @Type, @DVT", iNamLamViecParam, sIdDonViParam, iType, donViTinhParam).ToList();
            }
        }

        public IEnumerable<BhReportQttBHXHChiTietQuery> ExportTongHopQuyetToanThuChi(int iNamLamViec, string sIdDonVis, int donViTinh, bool isTongHop)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter sIdDonViParam = new SqlParameter("@LstMaDonVi", sIdDonVis);
                SqlParameter donViTinhParam = new SqlParameter("@DVT", donViTinh);
                SqlParameter isTongHopParam = new SqlParameter("@IsTongHop", isTongHop);
                return ctx.FromSqlRaw<BhReportQttBHXHChiTietQuery>("EXECUTE dbo.sp_bh_report_tong_hop_quyet_toan_thu_chi_bhxh_bhyt_bhtn "
                    + "@NamLamViec, @LstMaDonVi, @DVT, @IsTongHop", iNamLamViecParam, sIdDonViParam, donViTinhParam, isTongHopParam).ToList();
            }
        }

        public IEnumerable<BhThamDinhQuyetToanChiTietQuery> ExportDuToanKinhPhiChuyenNamSau(int iNamLamViec, string sIdDonVis, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@INamLamViec", iNamLamViec);
                SqlParameter lstDonViParam = new SqlParameter("@IdDonVi", sIdDonVis);
                SqlParameter dvtParam = new SqlParameter("@DonViTinh", donViTinh);

                return ctx.FromSqlRaw<BhThamDinhQuyetToanChiTietQuery>("EXECUTE sp_rpt_bh_thamdinh_du_toan_chuyen_nam_sau @INamLamViec, @IdDonVi, @DonViTinh",
                    iNamLamViecParam, lstDonViParam, dvtParam).ToList();
            }
        }

        public IEnumerable<BhThamDinhQuyetToanChiTietQuery> ExportCanCuTrichQuyBhxhSangBhyt(int yearOfWork, string iIDMaDonVi, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec ", yearOfWork);
                SqlParameter iMaDonViParam = new SqlParameter("@MaDonVi ", iIDMaDonVi);
                SqlParameter iDonViTinh = new SqlParameter("@Dvt ", donViTinh);

                return ctx.FromSqlRaw<BhThamDinhQuyetToanChiTietQuery>("EXECUTE sp_bh_rpt_thamdinhquyettoan_can_cu_bhxh_sang_bhyt @NamLamViec, @MaDonVi, @Dvt ",
                     iNamLamViecParam, iMaDonViParam, iDonViTinh).ToList();
            }
        }
    }
}
