using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;


namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class QtcnKCBChiTietRepository : Repository<BhQtcnKCBChiTiet>, IQtcnKCBChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public QtcnKCBChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhQtcnKCBChiTiet> FindByCondition(Expression<Func<BhQtcnKCBChiTiet, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcnKCBChiTiets.Where(predicate).ToList();
            }
        }

        public IEnumerable<BhQtcnKCBChiTietQuery> GetChiTietQuyetToanChiNamKCB(Guid idChungTu, string sLNS, int iNamLamViec, bool isTongHop4Quy, string maDonVi, bool loai)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter idChungTuParam = new SqlParameter("@IdChungTu", idChungTu);
                SqlParameter sLnsParam = new SqlParameter("@Lns ", sLNS);
                SqlParameter iNamLamViecParam = new SqlParameter("@INamLamViec ", iNamLamViec);
                SqlParameter isTongHop4QuyParam = new SqlParameter("@IsTongHop4Quy ", isTongHop4Quy);
                SqlParameter maDonViParam = new SqlParameter("@MaDonVi", maDonVi);
                SqlParameter loaiParam = new SqlParameter("@Loai ", loai);

                return ctx.FromSqlRaw<BhQtcnKCBChiTietQuery>("EXECUTE sp_bh_quyet_toan_chinamKCB_chitiet @IdChungTu, @Lns, @INamLamViec, @IsTongHop4Quy, @MaDonVi, @Loai",
                    idChungTuParam, sLnsParam, iNamLamViecParam, isTongHop4QuyParam, maDonViParam, loaiParam).ToList();
            }
        }

        public void CreateVoudcherSummary(string idChungTu, string idMaDonVi, string nguoiTao, int namLamViec, string idChungTuSummary)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_qtcnKCB_create_data_summary @IdChungTu,@IDMaDonVi, @NguoiTao, @YearOfWork, @IdChungTuSummary";
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
                string sql = "EXECUTE sp_bh_create_quyet_toan_chinamkcb_chitiet_theo4quy @idChungTu, @idMaDonVi, @iNamLamViec, @user,@IsTongHop ";
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

        public IEnumerable<BhQtcnKCBChiTietQuery> ExportBaoCaoQuyetToanKhamChuaBenhTaiQuanYDonVi(int iNamLamViec, string sIdDonVi, string sLns, int donViTinh, bool isTongHop)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@INamLamViec", iNamLamViec);
                SqlParameter sIdDonViParam = new SqlParameter("@IdDonVi ", sIdDonVi);
                SqlParameter sLnsParam = new SqlParameter("@Lns ", sLns);
                SqlParameter donViTinhParam = new SqlParameter("@Donvitinh ", donViTinh);
                SqlParameter isTongHopParam = new SqlParameter("@IsTongHop ", isTongHop);

                return ctx.FromSqlRaw<BhQtcnKCBChiTietQuery>("EXECUTE sp_bh_quyet_toan_baocaoquyettoanchicacchedo_KCB @INamLamViec, @IdDonVi,@Lns, @Donvitinh,  @IsTongHop",
                    iNamLamViecParam, sIdDonViParam, sLnsParam, donViTinhParam, isTongHopParam).ToList();
            }
        }

        public IEnumerable<BhQtcnKCBChiTietQuery> ExportPhuLucQuyetToanKhamChuaBenhTaiQuanYDonVi(int iNamLamViec, string sIdDonVi, string sLns, int donViTinh, bool isTongHop)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@INamLamViec", iNamLamViec);
                SqlParameter sIdDonViParam = new SqlParameter("@IdDonVi ", sIdDonVi);
                SqlParameter sLnsParam = new SqlParameter("@Lns ", sLns);
                SqlParameter donViTinhParam = new SqlParameter("@Donvitinh ", donViTinh);
                SqlParameter isTongHopParam = new SqlParameter("@IsTongHop ", isTongHop);

                return ctx.FromSqlRaw<BhQtcnKCBChiTietQuery>("EXECUTE sp_bh_quyet_toan_phulucquyettoannam_KCB @INamLamViec, @IdDonVi,@Lns, @Donvitinh,  @IsTongHop",
                    iNamLamViecParam, sIdDonViParam, sLnsParam, donViTinhParam, isTongHopParam).ToList();
            }
        }

        public bool ExistVoucherDetail(Guid id, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcnKCBChiTiets.Where(x => x.IIdQTCNamKCBQuanYDonVi == id && x.INamLamViec == namLamViec).ToList().Count > 0 ? true : false;
            }
        }

        public List<BhQtcnKCBChiTietQuery> GetTienPhanBoChiTietDuToanChi(int iNamLamViec, string sMaLoaiChi, Guid id, string idMaDonVi, string sDSLNS, DateTime dNgayChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@INamLamViec ", iNamLamViec);
                SqlParameter sMaLoaiChiParam = new SqlParameter("@SMaLoaiChi", sMaLoaiChi);
                SqlParameter idLoaiChiParam = new SqlParameter("@IDLoaiChi", id);
                SqlParameter sMaDonViParam = new SqlParameter("@SMaDonVi", idMaDonVi);
                SqlParameter sLNSParam = new SqlParameter("@SLNS", sDSLNS);
                SqlParameter dNgayChungTuParam = new SqlParameter("@DNgayChungTu ", dNgayChungTu);

                return ctx.FromSqlRaw<BhQtcnKCBChiTietQuery>("EXECUTE sp_bh_quyet_toan_get_fTienDuToanGiaoNamNay_nkcb_for_pbdtchi  @INamLamViec,@SMaLoaiChi,@IDLoaiChi ,@SMaDonVi,@SLNS,@DNgayChungTu",
                    iNamLamViecParam, sMaLoaiChiParam, idLoaiChiParam, sMaDonViParam, sLNSParam, dNgayChungTuParam).ToList();

            }
        }
    }
}
