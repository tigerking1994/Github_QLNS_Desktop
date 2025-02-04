using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;


namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class QtcqBHXHChiTietRepository : Repository<BhQtcqBHXHChiTiet>, IQtcqBHXHChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public QtcqBHXHChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhQtcqBHXHChiTiet> FindByCondition(Expression<Func<BhQtcqBHXHChiTiet, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcqBHXHChiTiets.Where(predicate).ToList();
            }
        }
        public void CreateVoudcherSummary(string idChungTu, string nguoiTao, int namLamViec, string idChungTuSummary, string sMaDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_qtcqBHXH_create_data_summary @IdChungTu, @NguoiTao, @YearOfWork, @IdChungTuSummary,@MaDonVi";
                var parameters = new[]
                {
                    new SqlParameter("@IdChungTu", idChungTu),
                    new SqlParameter("@NguoiTao", nguoiTao),
                    new SqlParameter("@YearOfWork", namLamViec),
                    new SqlParameter("@IdChungTuSummary", idChungTuSummary),
                    new SqlParameter("@MaDonVi", sMaDonVi)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public IEnumerable<BhQtcqBHXHChiTietQuery> GetChiTietQuyetToanChiQuyBHXH(Guid idChungTu, string sSLNS, int iNamLamViec, string iIDMaDonVi, bool isPhanBo, DateTime? dNgayChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter idChungTuParam = new SqlParameter("@IdChungTu", idChungTu);
                SqlParameter sSLNSParam = new SqlParameter("@SLNS", sSLNS);
                SqlParameter iNamLamViecParam = new SqlParameter("@INamLamViec ", iNamLamViec);
                SqlParameter maDonViParam = new SqlParameter("@MaDonVi ", iIDMaDonVi);
                SqlParameter loaiParam = new SqlParameter("@Loai ", isPhanBo);
                return ctx.FromSqlRaw<BhQtcqBHXHChiTietQuery>("EXECUTE sp_bh_quyet_toan_chiquybhxh_chitiet @IdChungTu, @SLNS, @INamLamViec, @MaDonVi, @Loai ",
                    idChungTuParam, sSLNSParam, iNamLamViecParam, maDonViParam, loaiParam).ToList();
            }
        }

        public IEnumerable<BhQtcqBHXHChiTietQuery> BaoCaoQuyetToanChiQuyBHXH(int iNamLamViec, string idMaDonVi, string sLNS, bool isTongHop, int iQuy, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@INamLamViec", iNamLamViec);
                SqlParameter idMaDonViParam = new SqlParameter("@IdMaDonVi ", idMaDonVi);
                SqlParameter sLNSParam = new SqlParameter("@SLN ", sLNS);
                SqlParameter isTongHopParam = new SqlParameter("@IsTongHop  ", isTongHop);
                SqlParameter iQuyParam = new SqlParameter("@IQuy", iQuy);
                SqlParameter donViTinhParam = new SqlParameter("@Donvitinh", donViTinh);

                return ctx.FromSqlRaw<BhQtcqBHXHChiTietQuery>("EXECUTE sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh @INamLamViec, @IdMaDonVi,@SLN, @IsTongHop,@IQuy ,@Donvitinh  ",
                    iNamLamViecParam, idMaDonViParam, sLNSParam, isTongHopParam, iQuyParam, donViTinhParam).ToList();
            }
        }

        public IEnumerable<BhBaoCaoQuyetToanChiQuyQuery> BaoCaoGiaiThichTroCapOmDau(int iNamLamViec, string idMaDonVi, string sLNS, bool isTongHop, int iQuy, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@INamLamViec", iNamLamViec);
                SqlParameter idMaDonViParam = new SqlParameter("@IdMaDonVi ", idMaDonVi);
                SqlParameter sLNSParam = new SqlParameter("@SLN ", sLNS);
                SqlParameter isTongHopParam = new SqlParameter("@IsTongHop  ", isTongHop);
                SqlParameter iQuyParam = new SqlParameter("@IQuy", iQuy);
                SqlParameter donViTinhParam = new SqlParameter("@Donvitinh", donViTinh);

                return ctx.FromSqlRaw<BhBaoCaoQuyetToanChiQuyQuery>("EXECUTE sp_bh_quyet_toan_giaithichtrocapomdau_bhxh @INamLamViec, @IdMaDonVi,@SLN, @IsTongHop,@IQuy ,@Donvitinh  ",
                    iNamLamViecParam, idMaDonViParam, sLNSParam, isTongHopParam, iQuyParam, donViTinhParam).ToList();
            }
        }

        public IEnumerable<BhBaoCaoQuyetToanChiQuyQuery> BaoCaoGiaiThichTroCapOmDauKhoi(int yearOfWork, string donVi, string lNS_9010001_9010002, bool isTongHop, int iQuy, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@INamLamViec", yearOfWork);
                SqlParameter idMaDonViParam = new SqlParameter("@IdMaDonVi ", donVi);
                SqlParameter sLNSParam = new SqlParameter("@SLN ", lNS_9010001_9010002);
                SqlParameter isTongHopParam = new SqlParameter("@IsTongHop  ", isTongHop);
                SqlParameter iQuyParam = new SqlParameter("@IQuy", iQuy);
                SqlParameter donViTinhParam = new SqlParameter("@Donvitinh", donViTinh);

                return ctx.FromSqlRaw<BhBaoCaoQuyetToanChiQuyQuery>("EXECUTE sp_bh_quyet_toan_giaithichtrocapomdau_khoi @INamLamViec, @IdMaDonVi,@SLN, @IsTongHop,@IQuy ,@Donvitinh  ",
                    iNamLamViecParam, idMaDonViParam, sLNSParam, isTongHopParam, iQuyParam, donViTinhParam).ToList();
            }
        }

        public IEnumerable<BhBaoCaoQuyetToanChiQuyQuery> BaoCaoGiaiThichTroCapOmDauKhoiNhomDT(int yearOfWork, string donVi, string lNS_9010001_9010002, bool isTongHop, int iQuy, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@INamLamViec", yearOfWork);
                SqlParameter idMaDonViParam = new SqlParameter("@IdMaDonVi ", donVi);
                SqlParameter sLNSParam = new SqlParameter("@SLN ", lNS_9010001_9010002);
                SqlParameter isTongHopParam = new SqlParameter("@IsTongHop  ", isTongHop);
                SqlParameter iQuyParam = new SqlParameter("@IQuy", iQuy);
                SqlParameter donViTinhParam = new SqlParameter("@Donvitinh", donViTinh);

                return ctx.FromSqlRaw<BhBaoCaoQuyetToanChiQuyQuery>("EXECUTE sp_bh_quyet_toan_giaithichtrocapomdau_khoi_nhomdt @INamLamViec, @IdMaDonVi,@SLN, @IsTongHop,@IQuy ,@Donvitinh  ",
                    iNamLamViecParam, idMaDonViParam, sLNSParam, isTongHopParam, iQuyParam, donViTinhParam).ToList();
            }
        }


        public IEnumerable<BhBaoCaoQuyetToanChiQuyQuery> BaoCaoGiaiThichTroCapThaiSan(int iNamLamViec, string idMaDonVi, string sLNS, bool isTongHop, int iQuy, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@INamLamViec", iNamLamViec);
                SqlParameter idMaDonViParam = new SqlParameter("@IdMaDonVi ", idMaDonVi);
                SqlParameter sLNSParam = new SqlParameter("@SLN ", sLNS);
                SqlParameter isTongHopParam = new SqlParameter("@IsTongHop  ", isTongHop);
                SqlParameter iQuyParam = new SqlParameter("@IQuy", iQuy);
                SqlParameter donViTinhParam = new SqlParameter("@Donvitinh", donViTinh);

                return ctx.FromSqlRaw<BhBaoCaoQuyetToanChiQuyQuery>("EXECUTE sp_bh_quyet_toan_giaithichtrocapthaisan_bhxh @INamLamViec, @IdMaDonVi,@SLN, @IsTongHop,@IQuy ,@Donvitinh  ",
                    iNamLamViecParam, idMaDonViParam, sLNSParam, isTongHopParam, iQuyParam, donViTinhParam).ToList();
            }
        }

        public IEnumerable<BhQtcqBHXHChiTietQuery> FindSoTienQuyetToanDaDuocDuyetTheoQuy(string idMaDonVi, string sLNS, int quyChungTu, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter idMaDonViParam = new SqlParameter("@MaDonVi", idMaDonVi);
                SqlParameter sLnsParam = new SqlParameter("@SLNS ", sLNS);
                SqlParameter iQuyParam = new SqlParameter("@Quy ", quyChungTu);
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec ", namLamViec);

                return ctx.FromSqlRaw<BhQtcqBHXHChiTietQuery>("EXECUTE sp_bh_quyet_toan_chiquytruocbhxh_chitiet @MaDonVi, @SLNS, @Quy, @NamLamViec ",
                    idMaDonViParam, sLnsParam, iQuyParam, iNamLamViecParam).ToList();
            }
        }

        public void CreateVoudcherForQuaterBefore(BhQtcqBHXH entity)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_qtcqBHXH_create_datafor_quaterbefore @IdChungTu, @Username, @NamLamViec, @Quy, @MaDonVi";
                var parameters = new[]
                {
                    new SqlParameter("@IdChungTu", entity.Id),
                    new SqlParameter("@Username", entity.SNguoiTao),
                    new SqlParameter("@NamLamViec", entity.INamChungTu),
                    new SqlParameter("@Quy", entity.IQuyChungTu),
                    new SqlParameter("@MaDonVi", entity.IIdMaDonVi),
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public List<ReportBHQTCQBHXHThongTriQuery> GetDataThongTriForDonVi(int yearOfWork, string quy, string donVi, string principal, int iLoaiChungTu, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", yearOfWork),
                    new SqlParameter("@iQuy", quy),
                    new SqlParameter("@IdDonVi", donVi),
                    new SqlParameter("@UserName", principal),
                    new SqlParameter("@iLoaiTongHop", iLoaiChungTu),
                    new SqlParameter("@Dvt", donViTinh)
                };

                string executeSql = "EXECUTE dbo.sp_rpt_qtc_qbhxh_thongtriloai1_bh @NamLamViec,@iQuy,@IdDonVi,@UserName,@iLoaiTongHop,@Dvt";
                return ctx.FromSqlRaw<ReportBHQTCQBHXHThongTriQuery>(executeSql, parameters).ToList();
            }
        }

        public bool ExistVoucherDetail(Guid voucherID)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var lstQtcQuy = ctx.BhQtcqBHXHChiTiets.Where(t => t.IdQTCQuyCheDoBHXH == voucherID && t.FTienDuToanDuyet.GetValueOrDefault(0) != 0).Select(x => x.FTienDuToanDuyet).ToList();
                if (lstQtcQuy.Count() > 0)
                    return true;
                return false;
            }
        }

        public IEnumerable<BhQtcqBHXHChiTiet> FindByVoucherID(Guid voucherID)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcqBHXHChiTiets.Where(x => x.IdQTCQuyCheDoBHXH == voucherID).ToList();
            }
        }

        public List<BhQtcqGiaiThichTroCapQuery> ExportDanhSachNguoiLaoDongNghiViec(int yearOfWork, int donViTinh, int quy, string donVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", yearOfWork),
                    new SqlParameter("@DVT", donViTinh),
                    new SqlParameter("@Quy", quy),
                    new SqlParameter("@DonVi", donVi)
                };

                string executeSql = "EXECUTE dbo.sp_rpt_bhxh_qtcq_giai_thich_che_do_om_dau @NamLamViec, @DVT, @Quy, @DonVi";
                return ctx.FromSqlRaw<BhQtcqGiaiThichTroCapQuery>(executeSql, parameters).ToList();
            }
        }

        public List<BhQtcqGiaiThichTroCapTaiNanQuery> ExportDanhSachHuongTroCapTaiNan(int yearOfWork, int donViTinh, int quy, string donVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@INamLamViec", yearOfWork),
                    new SqlParameter("@IdMaDonVi", donVi),
                    new SqlParameter("@IQuy", quy),
                    new SqlParameter("@Donvitinh", donViTinh)
                };

                string executeSql = "EXECUTE dbo.sp_bh_quyet_toan_giaithichttainannghenghiep_bhxh @INamLamViec, @IdMaDonVi, @IQuy, @Donvitinh";
                return ctx.FromSqlRaw<BhQtcqGiaiThichTroCapTaiNanQuery>(executeSql, parameters).ToList();
            }
        }

        public List<BhQtcqGiaiThichTroCapTaiNanQuery> ExportDanhSachHuongTroCapTaiNanTruyLinh(int yearOfWork, int donViTinh, int iQuy, string donVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@INamLamViec", yearOfWork),
                    new SqlParameter("@IdMaDonVi", donVi),
                    new SqlParameter("@IQuy", iQuy),
                    new SqlParameter("@Donvitinh", donViTinh)
                };

                string executeSql = "EXECUTE dbo.sp_bh_quyet_toan_giaithichttainannghenghiep_chuatonghopquytruoc_bhxh @INamLamViec, @IdMaDonVi, @IQuy, @Donvitinh";
                return ctx.FromSqlRaw<BhQtcqGiaiThichTroCapTaiNanQuery>(executeSql, parameters).ToList();
            }
        }

        public List<BhQtcqGiaiThichTroCapHTPVXNTVTTQuery> ExportDanhSachHuongTroCapHTPVXNTVTT(int yearOfWork, int donViTinh, int iQuy, string donVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@INamLamViec", yearOfWork),
                    new SqlParameter("@IdMaDonVi", donVi),
                    new SqlParameter("@IQuy", iQuy),
                    new SqlParameter("@Donvitinh", donViTinh)
                };

                string executeSql = "EXECUTE dbo.sp_rpt_bhxh_qtcq_gttrocap_huutri_phucvien_xuatngu_thoiviec_tutuat @INamLamViec, @IdMaDonVi, @IQuy, @Donvitinh";
                return ctx.FromSqlRaw<BhQtcqGiaiThichTroCapHTPVXNTVTTQuery>(executeSql, parameters).ToList();
            }
        }

        public List<BhQtcqBHXHChiTietQuery> GetTienPhanBoChiTietDuToanChi(int iNamLamViec, string sMaLoaiChi, Guid id, string sMaDonVi, string sLNS, DateTime dNgayChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@INamLamViec ", iNamLamViec);
                SqlParameter sMaLoaiChiParam = new SqlParameter("@SMaLoaiChi", sMaLoaiChi);
                SqlParameter idLoaiChiParam = new SqlParameter("@IDLoaiChi", id);
                SqlParameter sMaDonViParam = new SqlParameter("@SMaDonVi", sMaDonVi);
                SqlParameter sLNSParam = new SqlParameter("@SLNS", sLNS);
                SqlParameter dNgayChungTuParam = new SqlParameter("@DNgayChungTu ", dNgayChungTu);

                return ctx.FromSqlRaw<BhQtcqBHXHChiTietQuery>("EXECUTE sp_bh_quyet_toan_get_fTienDuToanDuyet_for_pbdtchi  @INamLamViec,@SMaLoaiChi,@IDLoaiChi ,@SMaDonVi,@SLNS,@DNgayChungTu",
                    iNamLamViecParam, sMaLoaiChiParam, idLoaiChiParam, sMaDonViParam, sLNSParam, dNgayChungTuParam).ToList();

            }
        }

        public List<BhQtcqBHXHChiTietQuery> GetDataSummaryBefore(BhQtcqBHXH entity)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@YearOfWork", entity.INamChungTu);
                SqlParameter iQuyParam = new SqlParameter("@Quy ", entity.IQuyChungTu);
                SqlParameter sDonViParam = new SqlParameter("@DonVi", entity.IIdMaDonVi);

                return ctx.FromSqlRaw<BhQtcqBHXHChiTietQuery>("EXECUTE sp_bh_qtcqBHXH_data_summary_before  @YearOfWork,@Quy,@DonVi",
                    iNamLamViecParam, iQuyParam, sDonViParam).ToList();

            }
        }

        public List<BhQtcqGiaiThichTroCapTaiNanQuery> ExportDanhSachHuongTroCapTaiNanNNTheoKhoi(int yearOfWork, int donViTinh, int iQuy, string donVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@INamLamViec", yearOfWork),
                    new SqlParameter("@IdMaDonVi", donVi),
                    new SqlParameter("@IQuy", iQuy),
                    new SqlParameter("@Donvitinh", donViTinh)
                };

                string executeSql = "EXECUTE dbo.sp_bh_quyet_toan_giaithichttainannghenghiep_khoi @INamLamViec, @IdMaDonVi, @IQuy, @Donvitinh";
                return ctx.FromSqlRaw<BhQtcqGiaiThichTroCapTaiNanQuery>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<BhBaoCaoQuyetToanChiQuyQuery> BaoCaoGiaiThichTroCapThaiSanFollowKhoi(int yearOfWork, string donVi, int iQuy, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@INamLamViec", yearOfWork),
                    new SqlParameter("@IdMaDonVi", donVi),
                    new SqlParameter("@IQuy", iQuy),
                    new SqlParameter("@Donvitinh", donViTinh)
                };

                string executeSql = "EXECUTE dbo.sp_bh_quyet_toan_giaithichtrocapthaisan_khoi @INamLamViec, @IdMaDonVi, @IQuy, @Donvitinh";
                return ctx.FromSqlRaw<BhBaoCaoQuyetToanChiQuyQuery>(executeSql, parameters).ToList();
            }
        }

        public List<BhQtcqGiaiThichTroCapHTPVXNTVTTQuery> ExportDanhSachHuongTroCapHTPVXNTVTTDetailXN(int yearOfWork, int donViTinh, int iQuy, string donVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@INamLamViec", yearOfWork),
                    new SqlParameter("@IdMaDonVi", donVi),
                    new SqlParameter("@IQuy", iQuy),
                    new SqlParameter("@Donvitinh", donViTinh)
                };

                string executeSql = "EXECUTE dbo.sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT @INamLamViec, @IdMaDonVi, @IQuy, @Donvitinh";
                return ctx.FromSqlRaw<BhQtcqGiaiThichTroCapHTPVXNTVTTQuery>(executeSql, parameters).ToList();
            }
        }

        public List<BhQtcqGiaiThichTroCapHTPVXNTVTTQuery> ExportDanhSachHuongTroCapHTPVXNTVTTKhoi(int yearOfWork, int donViTinh, int iQuy, string donVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@INamLamViec", yearOfWork),
                    new SqlParameter("@IdMaDonVi", donVi),
                    new SqlParameter("@IQuy", iQuy),
                    new SqlParameter("@Donvitinh", donViTinh)
                };

                string executeSql = "EXECUTE dbo.sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi @INamLamViec, @IdMaDonVi, @IQuy, @Donvitinh";
                return ctx.FromSqlRaw<BhQtcqGiaiThichTroCapHTPVXNTVTTQuery>(executeSql, parameters).ToList();
            }
        }

        public List<BhQtcqGiaiThichTroCapHTPVXNTVTTQuery> ExportDanhSachHuongTroCapHTPVXNTVTTKhoiDetailXN(int yearOfWork, int donViTinh, int iQuy, string donVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@INamLamViec", yearOfWork),
                    new SqlParameter("@IdMaDonVi", donVi),
                    new SqlParameter("@IQuy", iQuy),
                    new SqlParameter("@Donvitinh", donViTinh)
                };

                string executeSql = "EXECUTE dbo.sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Khoi_Detail @INamLamViec, @IdMaDonVi, @IQuy, @Donvitinh";
                return ctx.FromSqlRaw<BhQtcqGiaiThichTroCapHTPVXNTVTTQuery>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<BhBaoCaoQuyetToanChiQuyQuery> BaoCaoGiaiThichTroCapThaiSanFollowKhoiNhomDT(int yearOfWork, string donVi, int iQuy, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@INamLamViec", yearOfWork),
                    new SqlParameter("@IdMaDonVi", donVi),
                    new SqlParameter("@IQuy", iQuy),
                    new SqlParameter("@Donvitinh", donViTinh)
                };

                string executeSql = "EXECUTE dbo.sp_bh_quyet_toan_giaithichtrocapthaisan_khoi_Nhomdt @INamLamViec, @IdMaDonVi, @IQuy, @Donvitinh";
                return ctx.FromSqlRaw<BhBaoCaoQuyetToanChiQuyQuery>(executeSql, parameters).ToList();
            }
        }

        public List<BhQtcqGiaiThichTroCapTaiNanQuery> ExportDanhSachHuongTroCapTaiNanNNTheoKhoiNhomDT(int yearOfWork, int donViTinh, int iQuy, string donVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@INamLamViec", yearOfWork),
                    new SqlParameter("@IdMaDonVi", donVi),
                    new SqlParameter("@IQuy", iQuy),
                    new SqlParameter("@Donvitinh", donViTinh)
                };

                string executeSql = "EXECUTE dbo.sp_bh_quyet_toan_giaithichttainannghenghiep_khoi_Nhomdt @INamLamViec, @IdMaDonVi, @IQuy, @Donvitinh";
                return ctx.FromSqlRaw<BhQtcqGiaiThichTroCapTaiNanQuery>(executeSql, parameters).ToList();
            }
        }

        public List<BhQtcqGiaiThichTroCapHTPVXNTVTTQuery> ExportDanhSachHuongTroCapHTPVXNTVTTKhoiNhomDTDetail(int yearOfWork, int donViTinh, int iQuy, string donVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@INamLamViec", yearOfWork),
                    new SqlParameter("@IdMaDonVi", donVi),
                    new SqlParameter("@IQuy", iQuy),
                    new SqlParameter("@Donvitinh", donViTinh)
                };

                string executeSql = "EXECUTE dbo.sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Nhomdt_Detail @INamLamViec, @IdMaDonVi, @IQuy, @Donvitinh";
                return ctx.FromSqlRaw<BhQtcqGiaiThichTroCapHTPVXNTVTTQuery>(executeSql, parameters).ToList();
            }
        }

        public List<BhQtcqGiaiThichTroCapHTPVXNTVTTQuery> ExportDanhSachHuongTroCapHTPVXNTVTTKhoiNhomDT(int yearOfWork, int donViTinh, int iQuy, string donVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@INamLamViec", yearOfWork),
                    new SqlParameter("@IdMaDonVi", donVi),
                    new SqlParameter("@IQuy", iQuy),
                    new SqlParameter("@Donvitinh", donViTinh)
                };

                string executeSql = "EXECUTE dbo.sp_rpt_bhxh_qtcq_gttrocap_HT_PV_XN_TV_TT_Nhomdt @INamLamViec, @IdMaDonVi, @IQuy, @Donvitinh";
                return ctx.FromSqlRaw<BhQtcqGiaiThichTroCapHTPVXNTVTTQuery>(executeSql, parameters).ToList();
            }
        }
    }
}
