using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility.Enum;


namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class QtcqKCBChiTietRepository : Repository<BhQtcqKCBChiTiet>, IQtcqKCBChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public QtcqKCBChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhQtcqKCBChiTiet> FindByCondition(Expression<Func<BhQtcqKCBChiTiet, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcqKCBChiTiets.Where(predicate).ToList();
            }
        }

        public void CreateVoudcherSummary(string idChungTu, string nguoiTao, int namLamViec, string idChungTuSummary, string sMaDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_qtcqKCB_create_data_summary @IdChungTu, @NguoiTao, @YearOfWork, @IdChungTuSummary,@MaDonVi";
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

        public IEnumerable<BhQtcqKCBChiTietQuery> GetChiTietQuyetToanChiQuyKCB(Guid idChungTu, Guid idLoaiChi, string sLNS, string sMaLoaiChi, string sMaDonVi, DateTime dNgaychungTu, int iQuy, int iNamLamViec, int loai)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter idChungTuParam = new SqlParameter("@IdChungTu", idChungTu);
                SqlParameter idLoaiChiParam = new SqlParameter("@IDLoaiChi", idLoaiChi);
                SqlParameter sLNSParam = new SqlParameter("@SLNS", sLNS);
                SqlParameter sMaLoaiChiParam = new SqlParameter("@SMaLoaiChi", sMaLoaiChi);
                SqlParameter iIdMaDonViParam = new SqlParameter("@IIdMaDonVi", sMaDonVi);
                SqlParameter dNgaychungTuParam = new SqlParameter("@DNgayChungTu", dNgaychungTu);
                SqlParameter IQuyChungTuParam = new SqlParameter("@IQuyChungTu", iQuy);
                SqlParameter iNamLamViecParam = new SqlParameter("@INamLamViec ", iNamLamViec);
                SqlParameter loaiViecParam = new SqlParameter("@Loai", loai);

                return ctx.FromSqlRaw<BhQtcqKCBChiTietQuery>("EXECUTE sp_bh_quyet_toan_chiquyKCB_chitiet_clone @IdChungTu,@IDLoaiChi,@SLNS,@SMaLoaiChi,@IIdMaDonVi,@DNgayChungTu,@IQuyChungTu, @INamLamViec,@Loai ",
                    idChungTuParam, idLoaiChiParam, sLNSParam, sMaLoaiChiParam, iIdMaDonViParam, dNgaychungTuParam, IQuyChungTuParam, iNamLamViecParam, iNamLamViecParam, loaiViecParam).ToList();
            }
        }

        public IEnumerable<BhQtcqKCBChiTietQuery> BaoCaoKCBQuanYDonVi(int iNamLamViec, string idMaDonVi, string sLNS, bool isTongHop, int iQuy, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@INamLamViec", iNamLamViec);
                SqlParameter idMaDonViParam = new SqlParameter("@IdMaDonVi ", idMaDonVi);
                SqlParameter sLNSParam = new SqlParameter("@SLN ", sLNS);
                SqlParameter isTongHopParam = new SqlParameter("@IsTongHop  ", isTongHop);
                SqlParameter iQuyParam = new SqlParameter("@IQuy", iQuy);
                SqlParameter donViTinhParam = new SqlParameter("@Donvitinh", donViTinh);

                return ctx.FromSqlRaw<BhQtcqKCBChiTietQuery>("EXECUTE sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb @INamLamViec, @IdMaDonVi,@SLN, @IsTongHop,@IQuy ,@Donvitinh  ",
                    iNamLamViecParam, idMaDonViParam, sLNSParam, isTongHopParam, iQuyParam, donViTinhParam).ToList();
            }
        }

        public List<ReportBHQTCQKCBThongTriQuery> GetDataThongTriDonVi(int yearOfWork, string quy, string donVi, string principal, int iLoaiChungTu, int donViTinh)
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

                string executeSql = "EXECUTE dbo.sp_rpt_qtc_qkcb_thongtriloai1_bh @NamLamViec,@iQuy,@IdDonVi,@UserName,@iLoaiTongHop,@Dvt";
                return ctx.FromSqlRaw<ReportBHQTCQKCBThongTriQuery>(executeSql, parameters).ToList();
            }
        }

        public void CreateVoudcherForQuaterBefore(BhQtcqKCB entity)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_qtcqKCB_create_datafor_quaterbefore @IdChungTu, @Username, @NamLamViec, @Quy, @MaDonVi";
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

        public List<BhQtcqKCBChiTietQuery> GetDataTienDuToanPhanBoChi(int namChungTu, string sDSLNS, string idMaDonVi, DateTime dNgayChungTu, Guid idLoaiChi, int quyChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", namChungTu),
                    new SqlParameter("@LNS", sDSLNS),
                    new SqlParameter("@AgencyId", idMaDonVi),
                    new SqlParameter("@VoucherDate", dNgayChungTu),
                    new SqlParameter("@iID_LoaiDanhMucChi", idLoaiChi),
                    new SqlParameter("@Quy", quyChungTu)
                };

                string executeSql = "EXECUTE dbo.sp_qtc_quyKCB_getTienDuToanDuocGiao_chi_tiet @YearOfWork,@LNS,@AgencyId,@VoucherDate,@iID_LoaiDanhMucChi,@Quy";
                return ctx.FromSqlRaw<BhQtcqKCBChiTietQuery>(executeSql, parameters).ToList();
            }
        }

        public bool ExitChungTuChiTiet(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var lstChungTu = ctx.BhQtcqKCBChiTiets.Where(x => x.IIdQTCQuyKCB == id && x.FTienDuToanGiaoNamNay.GetValueOrDefault(0) != 0).ToList();
                if (lstChungTu.Count > 0)
                    return true;
                return false;
            }
        }

        public List<ReportBhQtcQKCBTongHopChi> BaoCaoKCBQuanYDonViTongHopChi(int yearOfWork, int donViTinh, string lstIdDonVi, int iQuy)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", yearOfWork),
                    new SqlParameter("@DonViTinh", donViTinh),
                    new SqlParameter("@AgencyId", lstIdDonVi),
                    new SqlParameter("@Quy", iQuy)
                };

                string executeSql = "EXECUTE dbo.sp_rpt_qtc_qkcb_tonghopchi @YearOfWork,@DonViTinh,@AgencyId,@Quy";
                return ctx.FromSqlRaw<ReportBhQtcQKCBTongHopChi>(executeSql, parameters).ToList();
            }
        }
    }
}
