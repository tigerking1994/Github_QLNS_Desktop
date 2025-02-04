using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI.WebControls;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;


namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class QtcnBHXHChiTietRepository : Repository<BhQtcnBHXHChiTiet>, IQtcnBHXHChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public QtcnBHXHChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhQtcnBHXHChiTiet> FindByCondition(Expression<Func<BhQtcnBHXHChiTiet, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcnBHXHChiTiets.Where(predicate).ToList();
            }
        }

        public IEnumerable<BhQtcnBHXHChiTietQuery> GetChiTietQuyetToanChiNamBHXH(Guid idChungTu, int iNamLamViec, bool isTongHop4Quy, int iLoaiTongHop, string sMaDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter idChungTuParam = new SqlParameter("@IdChungTu", idChungTu);
                SqlParameter iNamLamViecParam = new SqlParameter("@INamLamViec ", iNamLamViec);
                SqlParameter isTongHop4QuyParam = new SqlParameter("@IsTongHop4Quy ", isTongHop4Quy);
                SqlParameter isTongHopParam = new SqlParameter("@Loai ", iLoaiTongHop);
                SqlParameter sMaDonViParam = new SqlParameter("@MaDonVi ", sMaDonVi);
                return ctx.FromSqlRaw<BhQtcnBHXHChiTietQuery>("EXECUTE sp_bh_quyet_toan_chinambhxh_chitiet @IdChungTu, @INamLamViec,@IsTongHop4Quy,@Loai,@MaDonVi ",
                    idChungTuParam, iNamLamViecParam, isTongHop4QuyParam, isTongHopParam, sMaDonViParam).ToList();
            }
        }

        public void CreateVoudcherSummary(string idChungTu, string idMaDonVi, string nguoiTao, int namLamViec, string idChungTuSummary)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_qtcnBHXH_create_data_summary @IdChungTu,@IDMaDonVi, @NguoiTao, @YearOfWork, @IdChungTuSummary";
                var parameters = new[]
                {
                    new SqlParameter("@IdChungTu", idChungTu),
                    new SqlParameter("@IDMaDonVi",idMaDonVi),
                    new SqlParameter("@NguoiTao", nguoiTao),
                    new SqlParameter("@YearOfWork", namLamViec),
                    new SqlParameter("@IdChungTuSummary", idChungTuSummary)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }


        public void CreateChungTuChiTietTheoQuy(Guid idChungTu, string idMaDonVi, int iNamLamViec, string user, bool isTongHop)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_bh_create_quyet_toan_chinambhxh_chitiet_theo4quy @idChungTu, @idMaDonVi, @iNamLamViec, @user,@IsTongHop ";
                var parameters = new[]
                {
                    new SqlParameter("@IdChungTu", idChungTu),
                    new SqlParameter("@IdMaDonVi", idMaDonVi),
                    new SqlParameter("@INamLamViec", iNamLamViec),
                    new SqlParameter("@User", user),
                    new SqlParameter("@IsTongHop", isTongHop)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }


        public IEnumerable<BhQtcnBHXHChiTietQuery> ExportBaoCaoQuyetToanChiNamCacCheDoBHXH(int iNamLamViec, string sIdDonVi, string sLns, int donViTinh, bool isTongHop)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@INamLamViec", iNamLamViec);
                SqlParameter sIdDonViParam = new SqlParameter("@@IdMaDonVi ", sIdDonVi);
                SqlParameter sLnsParam = new SqlParameter("@Lns ", sLns);
                SqlParameter donViTinhParam = new SqlParameter("@Donvitinh ", donViTinh);
                SqlParameter isTongHopParam = new SqlParameter("@IsTongHop ", isTongHop);

                return ctx.FromSqlRaw<BhQtcnBHXHChiTietQuery>("EXECUTE sp_bh_quyet_toan_baocaoquyettoanchicacchedo_bhxh @INamLamViec, @IdMaDonVi,@Lns, @Donvitinh,  @IsTongHop",
                    iNamLamViecParam, sIdDonViParam, sLnsParam, donViTinhParam, isTongHopParam).ToList();
            }
        }


        public IEnumerable<BhBaoCaoQuyetToanChiNamQuery> ExportQuyetToanChiNamCacCheDoBHXH(int iNamLamViec, string sIdDonVi, string sLns, int donViTinh, bool isTongHop)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@INamLamViec", iNamLamViec);
                SqlParameter sIdDonViParam = new SqlParameter("@IdDonVi ", sIdDonVi);
                SqlParameter donViTinhParam = new SqlParameter("@DonViTinh", donViTinh);
                SqlParameter sLnsParam = new SqlParameter("@LNS ", sLns);
                SqlParameter isTongHopParam = new SqlParameter("@IsTongHop ", isTongHop);

                return ctx.FromSqlRaw<BhBaoCaoQuyetToanChiNamQuery>("EXECUTE sp_bh_quyet_toan_quyettoanchicacchedo_bhxh @INamLamViec, @IdDonVi,@DonViTinh, @LNS,  @IsTongHop",
                    iNamLamViecParam, sIdDonViParam, donViTinhParam, sLnsParam, isTongHopParam).ToList();
            }
        }

        public List<BhQtcnBHXHChiTietQuery> GetTienPhanBoChiTietDuToanChi(int iNamLamViec, string sMaLoaiChi,  string sMaDonVi, string sLNS, DateTime dNgayChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {

                SqlParameter iNamLamViecParam = new SqlParameter("@INamLamViec ", iNamLamViec);
                SqlParameter sMaLoaiChiParam = new SqlParameter("@SMaLoaiChi", sMaLoaiChi);
                SqlParameter sMaDonViParam = new SqlParameter("@SMaDonVi", sMaDonVi);
                SqlParameter sLNSParam = new SqlParameter("@SLNS", sLNS);
                SqlParameter dNgayChungTuParam = new SqlParameter("@DNgayChungTu ", dNgayChungTu);

                return ctx.FromSqlRaw<BhQtcnBHXHChiTietQuery>("EXECUTE sp_bh_quyet_toan_get_fTienDuToanDuyet_for_pbdtchi  @INamLamViec,@SMaLoaiChi,@SMaDonVi,@SLNS,@DNgayChungTu",
                    iNamLamViecParam, sMaLoaiChiParam,  sMaDonViParam, sLNSParam, dNgayChungTuParam).ToList();
            }
        }

        public void CreateVoudcherDetailSummary(int iNamLamViec, string sMaLoaiChi, Guid id, string sMaDonVi, string sLNS, DateTime dNgayChungTu, Guid idChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_bh_quyet_toan_Create_fTienDuToanDuyet_forQTCNBHXH_pbdtchi @INamLamViec,@SMaLoaiChi,@IDLoaiChi ,@SMaDonVi,@SLNS,@IDChungTu,@DNgayChungTu";
                var parameters = new[]
                {
                    new SqlParameter("@INamLamViec ", iNamLamViec),
                    new SqlParameter("@SMaLoaiChi", sMaLoaiChi),
                    new SqlParameter("@IDLoaiChi", id),
                    new SqlParameter("@SMaDonVi", sMaDonVi),
                    new SqlParameter("@SLNS", sLNS),
                    new SqlParameter("@IDChungTu",idChungTu),
                    new SqlParameter("@DNgayChungTu ", dNgayChungTu),
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }

        }

        public bool ExistVoucherDetail(Guid id, int? namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcnBHXHChiTiets.Where(x => x.IIdQTCNamCheDoBHXH == id && x.INamLamViec == namLamViec).ToList().Count > 0 ? true : false;
            }
        }
    }
}
