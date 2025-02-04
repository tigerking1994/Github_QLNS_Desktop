using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class SktChungTuChiTietRepository : Repository<NsSktChungTuChiTiet>, ISktChungTuChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        private const string _idDonViDuPhong = "999";

        public SktChungTuChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public NsSktChungTuChiTiet FindById(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsSktChungTuChiTiets.Find(id);
            }
        }

        public IEnumerable<NsSktChungTuChiTiet> FindByCondition(Expression<Func<NsSktChungTuChiTiet, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsSktChungTuChiTiets.Where(predicate).ToList();
            }
        }

        public IEnumerable<NsSktChungTuChiTiet> FindByConditionForChildUnit(SktChungTuChiTietCriteria searchCondition)
        {
            if (string.IsNullOrEmpty(searchCondition.IdDonViFilter) && searchCondition.IsViewDetailSummary == 1)
            {
                return FindByConditionForChildUnitDetail(searchCondition);
            }
            else
            {
                return FindByConditionForChildUnitSummary(searchCondition);
            }
        }

        public IEnumerable<NsSktChungTuChiTiet> FindByConditionForChildUnit_1(SktChungTuChiTietCriteria searchCondition)
        {
            if (string.IsNullOrEmpty(searchCondition.IdDonViFilter) && searchCondition.IsViewDetailSummary == 1)
            {
                return FindByConditionForChildUnitDetail(searchCondition);
            }
            else
            {
                return FindByConditionForChildUnitSummary_1(searchCondition);
            }
        }

        public IEnumerable<SktChungTuChiTietQuery> FindReportSoSanhSKT(SktChungTuChiTietCriteria searchCondition)
        {
            string sql = string.Empty;
            if (searchCondition.ILoaiNguonNganSach == 0)
                sql = "EXECUTE dbo.sp_skt_so_sanh_nam_truoc_nam_nay @Loai, @NamLamViec, @NamNganSach, @NguonNganSach, @LoaiChungTu, @IdDonVi, @NganSach, @UserName, @LoaiBaoCao, @KieuBaoCao, @DonViTinh";
            else
            {
                sql = "EXECUTE dbo.sp_skt_nhap_so_ktra_loainguon_so_sanh_nam_truoc_nam_nay @Loai, @NamLamViec, @NamNganSach, @NguonNganSach, @LoaiChungTu, @IdDonVi, @NganSach, @UserName, @LoaiBaoCao, @KieuBaoCao, @DonViTinh";
            }

            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@Loai", searchCondition.ILoai),
                    new SqlParameter("@NamLamViec", searchCondition.NamLamViec),
                    new SqlParameter("@NamNganSach", searchCondition.NamNganSach),
                    new SqlParameter("@NguonNganSach", searchCondition.NguonNganSach),
                    new SqlParameter("@LoaiChungTu", searchCondition.LoaiChungTu),
                    new SqlParameter("@IdDonVi", searchCondition.IdDonVi),
                    new SqlParameter("@NganSach", searchCondition.ILoaiNguonNganSach),
                    new SqlParameter("@UserName", searchCondition.UserName),
                    new SqlParameter("@LoaiBaoCao", searchCondition.LoaiBaoCao),
                    new SqlParameter("@KieuBaoCao", searchCondition.KieuBaoCao),
                    new SqlParameter("@DonViTinh", searchCondition.DonViTinh)
                };
                return ctx.FromSqlRaw<SktChungTuChiTietQuery>(sql, parameters).ToList();
            }
        }


        public IEnumerable<NsSktChungTuChiTiet> FindByConditionForChildUnitSummary(SktChungTuChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                int namLamViec = searchCondition.NamLamViec;
                int namNganSach = searchCondition.NamNganSach;
                int nguonNganSach = searchCondition.NguonNganSach;
                int iLoai = searchCondition.ILoai;
                int iLoaiNguonNganSach = searchCondition.ILoaiNguonNganSach;
                int iTrangThai = searchCondition.ITrangThai;
                string idDonVi = searchCondition.IdDonVi;
                int loaiChungTu = searchCondition.LoaiChungTu;
                Guid sktChungTuId = searchCondition.SktChungTuId;
                int hienThi = searchCondition.HienThi;
                Func<NsSktChungTuChiTiet, bool> defaultSktChungTuChiTietFilter = x => x.INamLamViec == namLamViec && x.INamNganSach == namNganSach && x.IIdMaNguonNganSach == nguonNganSach && x.ILoai == iLoai;
                Func<DonVi, bool> defaultNsDonViFilter = x => x.NamLamViec == namLamViec && x.ITrangThai == iTrangThai;
                Func<NsSktMucLuc, bool> defaultSktMucLucFilter = x => false;
                var nsDonVi = ctx.NsDonVis.Where(defaultNsDonViFilter)
                    .FirstOrDefault(x => x.IIDMaDonVi == idDonVi && x.NamLamViec == namLamViec);

                var sktChungTuChiTiets = ctx.NsSktChungTuChiTiets.AsNoTracking().AsEnumerable()
                    .Where(defaultSktChungTuChiTietFilter).Where(x => x.IIdMaDonVi == idDonVi && x.IIdCtsoKiemTra == sktChungTuId).ToList();

                sktChungTuChiTiets = sktChungTuChiTiets
                    .Where(x => x.FSoNganhPhanCap != 0 || x.FKhungNganSachDuocDuyet != 0 || x.FTuChi != 0 || x.FTuChiDeNghi != 0 || x.FHuyDongTonKho != 0 || x.FTonKhoDenNgay != 0 || x.FMuaHangCapHienVat != 0 || x.FPhanCap != 0 || x.FThongBaoDonVi != 0 || !string.IsNullOrEmpty(x.SGhiChu)).ToList();
                var lstIdMuclucDisplay = new List<string>();
                if (hienThi.Equals(DataStateValue.DA_NHAP_SKT))
                {
                    lstIdMuclucDisplay = sktChungTuChiTiets.Select(x => x.SKyHieu).Distinct().ToList();
                }
                var sktMucLucs = GetListMucLucSktByLoaiChungTu(iLoai, loaiChungTu, namLamViec, idDonVi, lstIdMuclucDisplay, hienThi, null);
                List<NsSktChungTuChiTiet> sktChungTuChiTietsSNC = new List<NsSktChungTuChiTiet>();
                if (DemandCheckType.CHECK.Equals(iLoai))
                {
                    sktChungTuChiTietsSNC = FindChungTuChiTietSkt(DemandCheckType.DEMAND, namLamViec, namNganSach, nguonNganSach, iLoaiNguonNganSach, loaiChungTu, idDonVi, 1).ToList();
                }

                if (DemandCheckType.DISTRIBUTION.Equals(iLoai) || DemandCheckType.CORPORATIZED_HOSPITAL.Equals(iLoai))
                {
                    sktChungTuChiTietsSNC = FindCanCuSoNhuCauPhanBoSKT(searchCondition);
                }

                var sktChungTuChiTietSKTNamTruoc = ctx.NsSktChungTuChiTiets.AsNoTracking()
                    .Where(x => x.INamLamViec == (namLamViec - 1) && x.INamNganSach == namNganSach && x.IIdMaNguonNganSach == nguonNganSach && (x.ILoai == DemandCheckType.CHECK || x.ILoai == DemandCheckType.DISTRIBUTION || x.ILoai == DemandCheckType.CORPORATIZED_HOSPITAL) && x.IIdMaDonVi == idDonVi && x.ILoaiChungTu == loaiChungTu)
                    .ToList();
                var sktChungTuSKTNamTruoc = ctx.NsSktChungTus.AsNoTracking().Where(x => x.INamLamViec == (namLamViec - 1) && x.INamNganSach == namNganSach && x.IIdMaNguonNganSach == nguonNganSach && x.BKhoa && x.ILoaiNguonNganSach == iLoaiNguonNganSach).ToList();

                var sktChungTuChiTietSKTNamTruocJoin = from chitiet in sktChungTuChiTietSKTNamTruoc
                                                       join chungtu in sktChungTuSKTNamTruoc on chitiet.IIdCtsoKiemTra equals chungtu.Id
                                                       where (chitiet.ILoai == 2 && chungtu.ILoai == 2) || chitiet.ILoai != 2
                                                       select chitiet;

                //group by theo mục lục ngân sách số chứng từ chi tiết năm trc - do nghiệp vụ thay đổi được tạo nhiều chứng từ
                var sktChungTuChiTietSKTNamTruocGroup = sktChungTuChiTietSKTNamTruocJoin.GroupBy(x => x.SKyHieu)
                                                         .Select(cl => new NsSktChungTuChiTiet
                                                         {
                                                             SoKiemTra = cl.Sum(x => x.FTuChi),
                                                             SoKiemTraMHHV = cl.Sum(x => x.FMuaHangCapHienVat),
                                                             SoKiemTraDT = cl.Sum(x => x.FPhanCap),
                                                             IIdMlskt = cl.First().IIdMlskt,
                                                             SKyHieu = cl.First().SKyHieu
                                                         }).ToList();


                var result = from sktMucLuc in sktMucLucs
                             join sktChungTuChiTiet in sktChungTuChiTiets on sktMucLuc.SKyHieu equals sktChungTuChiTiet.SKyHieu
                                         into gj
                             from sub in gj.DefaultIfEmpty()
                             join sktChungTuChiTietSNC in sktChungTuChiTietsSNC on sktMucLuc.SKyHieu equals sktChungTuChiTietSNC.SKyHieu
                             into snc
                             from sncResult in snc.DefaultIfEmpty()
                                 /*join sktNamTruoc in sktChungTuChiTietSKTNamTruoc on  sktMucLuc.IIDMLSKT equals sktNamTruoc.IIdMlskt
                                    into skt*/
                             join sktNamTruoc in sktChungTuChiTietSKTNamTruocGroup on sktMucLuc.SKyHieuCu equals sktNamTruoc.SKyHieu
                                 into skt
                             from sktResult in skt.DefaultIfEmpty()
                             orderby sktMucLuc.SKyHieu
                             select new NsSktChungTuChiTiet
                             {
                                 Id = sub == null ? Guid.NewGuid() : sub.Id == Guid.Empty ? Guid.NewGuid() : sub.Id,
                                 Stt = sktMucLuc.SSTT,
                                 SSttbc = sktMucLuc.SSttBC,
                                 SMoTa = sktMucLuc.SMoTa,
                                 SKyHieu = sktMucLuc.SKyHieu,
                                 SKyHieuCu = sktMucLuc.SKyHieuCu,
                                 SL = sktMucLuc.SL,
                                 SK = sktMucLuc.SK,
                                 SM = sktMucLuc.SM,
                                 Nganh = sktMucLuc.SNg,
                                 FHuyDongTonKho = sub == null ? 0 : sub.FHuyDongTonKho,
                                 FTuChi = sub == null ? 0 : sub.FTuChi,
                                 FTuChiDeNghi = sub == null ? 0 : sub.FTuChiDeNghi,
                                 INamLamViec = sub == null ? namLamViec : sub.INamLamViec,
                                 INamNganSach = namNganSach,
                                 IIdMaNguonNganSach = nguonNganSach,
                                 ILoaiChungTu = loaiChungTu,
                                 IIdCtsoKiemTra = sktChungTuId,
                                 IIdCtsoKiemTraChild = sub == null ? sktChungTuId : sub.IIdCtsoKiemTra,
                                 IsHangCha = sktMucLuc.BHangCha,
                                 IIdMaDonVi = nsDonVi == null ? string.Empty : nsDonVi.IIDMaDonVi,
                                 IdParent = sktMucLuc.IIDMLSKTCha,
                                 STenDonVi = nsDonVi == null ? string.Empty : nsDonVi.TenDonVi,
                                 IIdMlskt = sktMucLuc.IIDMLSKT,
                                 IsAdd = sub == null,
                                 SGhiChu = sub != null ? sub.SGhiChu : "",
                                 ILoai = iLoai,
                                 DNgayTao = null,
                                 SNguoiTao = null,
                                 DNgaySua = null,
                                 SNguoiSua = null,
                                 FHienVat = sub == null ? 0 : sub.FHienVat,
                                 FMuaHangCapHienVat = sub == null ? 0 : sub.FMuaHangCapHienVat,
                                 FPhanCap = sub == null ? 0 : sub.FPhanCap,
                                 FKhungNganSachDuocDuyet = sub == null ? 0 : sub.FKhungNganSachDuocDuyet,
                                 FSoNganhPhanCap = sub == null ? 0 : sub.FSoNganhPhanCap,
                                 FTonKhoDenNgay = sub == null ? 0 : sub.FTonKhoDenNgay,
                                 FThongBaoDonVi = sub == null ? 0 : sub.FThongBaoDonVi,
                                 SoNhuCau = sncResult == null ? 0 : sncResult.FTuChi,
                                 SoNhuCauMHHV = sncResult == null ? 0 : sncResult.FMuaHangCapHienVat,
                                 SoNhuCauDT = sncResult == null ? 0 : sncResult.FPhanCap,
                                 SoKiemTra = sktResult != null ? sktResult.SoKiemTra : 0,
                                 SoKiemTraMHHV = sktResult != null ? sktResult.SoKiemTraMHHV : 0,
                                 SoKiemTraDT = sktResult != null ? sktResult.SoKiemTraDT : 0
                             };
                var resultGroup = from p in result
                                  group p by new { p.SKyHieu, p.SKyHieuCu, p.IIdMaDonVi, p.SMoTa, p.STenDonVi } into g
                                  select new NsSktChungTuChiTiet
                                  {
                                      Id = g.First().Id,
                                      Stt = g.First().Stt,
                                      SSttbc = g.First().SSttbc,
                                      SMoTa = g.First().SMoTa,
                                      SKyHieu = g.First().SKyHieu,
                                      SKyHieuCu = g.First().SKyHieuCu,
                                      SL = g.First().SL,
                                      SK = g.First().SK,
                                      SM = g.First().SM,
                                      Nganh = g.First().Nganh,
                                      FHuyDongTonKho = g.First().FHuyDongTonKho,
                                      FTuChi = g.First().FTuChi,
                                      FTuChiDeNghi = g.First().FTuChiDeNghi,
                                      INamLamViec = g.First().INamLamViec,
                                      INamNganSach = g.First().INamNganSach,
                                      IIdMaNguonNganSach = g.First().IIdMaNguonNganSach,
                                      ILoaiChungTu = g.First().ILoaiChungTu,
                                      IIdCtsoKiemTra = g.First().IIdCtsoKiemTra,
                                      IIdCtsoKiemTraChild = g.First().IIdCtsoKiemTraChild,
                                      IsHangCha = g.First().IsHangCha,
                                      IIdMaDonVi = g.First().IIdMaDonVi,
                                      IdParent = g.First().IdParent,
                                      STenDonVi = g.First().STenDonVi,
                                      IIdMlskt = g.First().IIdMlskt,
                                      IsAdd = g.First().IsAdd,
                                      SGhiChu = g.First().SGhiChu,
                                      ILoai = g.First().ILoai,
                                      DNgayTao = null,
                                      SNguoiTao = null,
                                      DNgaySua = null,
                                      SNguoiSua = null,
                                      FHienVat = g.First().FHienVat,
                                      FMuaHangCapHienVat = g.First().FMuaHangCapHienVat,
                                      FPhanCap = g.First().FPhanCap,
                                      FKhungNganSachDuocDuyet = g.First().FKhungNganSachDuocDuyet,
                                      FSoNganhPhanCap = g.First().FSoNganhPhanCap,
                                      FTonKhoDenNgay = g.First().FTonKhoDenNgay,
                                      FThongBaoDonVi = g.First().FThongBaoDonVi,
                                      SoNhuCau = g.Sum(x => x.SoNhuCau),
                                      SoNhuCauMHHV = g.Sum(x => x.SoNhuCauMHHV),
                                      SoNhuCauDT = g.Sum(x => x.SoNhuCauDT),
                                      SoKiemTra = g.First().SoKiemTra,
                                      SoKiemTraMHHV = g.First().SoKiemTraMHHV,
                                      SoKiemTraDT = g.First().SoKiemTraDT
                                  };
                return resultGroup;
            }
        }


        public IEnumerable<NsSktChungTuChiTiet> FindByConditionForChildUnitSummary_1(SktChungTuChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                int namLamViec = searchCondition.NamLamViec;
                int namNganSach = searchCondition.NamNganSach;
                int nguonNganSach = searchCondition.NguonNganSach;
                int iLoai = searchCondition.ILoai;
                int iTrangThai = searchCondition.ITrangThai;
                string idDonVi = searchCondition.IdDonVi;
                int loaiChungTu = searchCondition.LoaiChungTu;
                int loaiNguonNganSach = searchCondition.ILoaiNguonNganSach;
                Guid sktChungTuId = searchCondition.SktChungTuId;
                int hienThi = searchCondition.HienThi;
                Func<NsSktChungTuChiTiet, bool> defaultSktChungTuChiTietFilter = x => x.INamLamViec == namLamViec && x.INamNganSach == namNganSach && x.IIdMaNguonNganSach == nguonNganSach && x.ILoai == iLoai;
                Func<DonVi, bool> defaultNsDonViFilter = x => x.NamLamViec == namLamViec && x.ITrangThai == iTrangThai;
                Func<NsSktMucLuc, bool> defaultSktMucLucFilter = x => false;
                var nsDonVi = ctx.NsDonVis.Where(defaultNsDonViFilter)
                    .FirstOrDefault(x => x.IIDMaDonVi == idDonVi && x.NamLamViec == namLamViec);

                var sktChungTuChiTiets = ctx.NsSktChungTuChiTiets.AsNoTracking().AsEnumerable()
                    .Where(defaultSktChungTuChiTietFilter).Where(x => x.IIdMaDonVi == idDonVi).ToList();

                sktChungTuChiTiets = sktChungTuChiTiets.Where(x => searchCondition.lstSktChungTuId.Contains(x.IIdCtsoKiemTra)).ToList();
                var lstKyHieu = sktChungTuChiTiets.Select(x => x.SKyHieu).Distinct().ToList();
                List<NsSktChungTuChiTiet> lstCtChiTiet = sktChungTuChiTiets.GroupBy(x => x.SKyHieu)
                                                         .Select(cl => new NsSktChungTuChiTiet
                                                         {
                                                             FHuyDongTonKho = cl.Sum(c => c.FHuyDongTonKho),
                                                             FTuChi = cl.Sum(c => c.FTuChi),
                                                             FTuChiDeNghi = cl.Sum(c => c.FTuChiDeNghi),
                                                             INamLamViec = cl.First().INamLamViec,
                                                             FHienVat = cl.Sum(c => c.FHienVat),
                                                             FMuaHangCapHienVat = cl.Sum(c => c.FMuaHangCapHienVat),
                                                             FPhanCap = cl.Sum(c => c.FPhanCap),
                                                             FSoNganhPhanCap = cl.Sum(c => c.FSoNganhPhanCap),
                                                             FKhungNganSachDuocDuyet = cl.Sum(c => c.FKhungNganSachDuocDuyet),
                                                             FTonKhoDenNgay = cl.Sum(c => c.FTonKhoDenNgay),
                                                             FThongBaoDonVi = cl.Sum(c => c.FThongBaoDonVi),
                                                             IIdMlskt = cl.First().IIdMlskt,
                                                             SKyHieu = cl.Key
                                                         }).ToList();


                lstCtChiTiet = lstCtChiTiet
                    .Where(x => x.FSoNganhPhanCap != 0 || x.FKhungNganSachDuocDuyet != 0 || x.FTuChi != 0 || x.FTuChiDeNghi != 0 || x.FHuyDongTonKho != 0 || x.FTonKhoDenNgay != 0 || x.FMuaHangCapHienVat != 0 || x.FPhanCap != 0 || x.FThongBaoDonVi != 0 || !string.IsNullOrEmpty(x.SGhiChu)).ToList();
                var lstIdMuclucDisplay = new List<string>();

                if (hienThi.Equals(DataStateValue.DA_NHAP_SKT))
                {
                    lstIdMuclucDisplay = lstCtChiTiet.Select(x => x.SKyHieu).Distinct().ToList();
                }
                var sktMucLucs = GetListMucLucSktByLoaiChungTu(iLoai, loaiChungTu, namLamViec, idDonVi, lstIdMuclucDisplay, hienThi, null);

                var result = from sktMucLuc in sktMucLucs
                             join sktChungTuChiTiet in lstCtChiTiet on sktMucLuc.SKyHieu equals sktChungTuChiTiet.SKyHieu
                                         into gj

                             from sub in gj.DefaultIfEmpty()
                             orderby sktMucLuc.SKyHieu
                             select new NsSktChungTuChiTiet
                             {
                                 Stt = sktMucLuc.SSTT,
                                 SSttbc = sktMucLuc.SSttBC,
                                 SMoTa = sktMucLuc.SMoTa,
                                 SKyHieu = sktMucLuc.SKyHieu,
                                 FHuyDongTonKho = sub == null ? 0 : sub.FHuyDongTonKho,
                                 FTuChi = sub == null ? 0 : sub.FTuChi,
                                 FTuChiDeNghi = sub == null ? 0 : sub.FTuChiDeNghi,
                                 INamLamViec = sub == null ? namLamViec : sub.INamLamViec,
                                 INamNganSach = namNganSach,
                                 IIdMaNguonNganSach = nguonNganSach,
                                 ILoaiChungTu = loaiChungTu,
                                 IsHangCha = sktMucLuc.BHangCha,
                                 IIdMaDonVi = nsDonVi == null ? string.Empty : nsDonVi.IIDMaDonVi,
                                 IdParent = sktMucLuc.IIDMLSKTCha,
                                 STenDonVi = nsDonVi == null ? string.Empty : nsDonVi.TenDonVi,
                                 IIdMlskt = sktMucLuc.IIDMLSKT,
                                 IsAdd = sub == null,
                                 ILoai = iLoai,
                                 DNgayTao = null,
                                 SNguoiTao = null,
                                 DNgaySua = null,
                                 SNguoiSua = null,
                                 FHienVat = sub == null ? 0 : sub.FHienVat,
                                 FMuaHangCapHienVat = sub == null ? 0 : sub.FMuaHangCapHienVat,
                                 FPhanCap = sub == null ? 0 : sub.FPhanCap,
                                 FKhungNganSachDuocDuyet = sub == null ? 0 : sub.FKhungNganSachDuocDuyet,
                                 FSoNganhPhanCap = sub == null ? 0 : sub.FSoNganhPhanCap,
                                 FTonKhoDenNgay = sub == null ? 0 : sub.FTonKhoDenNgay,
                                 FThongBaoDonVi = sub == null ? 0 : sub.FThongBaoDonVi,
                             };
                return result;
            }
        }

        public IEnumerable<NsSktChungTuChiTiet> FindByConditionForChildUnitDetail(SktChungTuChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                int namLamViec = searchCondition.NamLamViec;
                int namNganSach = searchCondition.NamNganSach;
                int nguonNganSach = searchCondition.NguonNganSach;
                int iLoai = searchCondition.ILoai;
                int iLoaiNguonNganSach = searchCondition.ILoaiNguonNganSach;
                int iTrangThai = searchCondition.ITrangThai;
                string idDonVi = searchCondition.IdDonVi;
                int loaiChungTu = searchCondition.LoaiChungTu;
                Guid sktChungTuId = searchCondition.SktChungTuId;
                int hienThi = searchCondition.HienThi;
                Func<NsSktChungTuChiTiet, bool> defaultSktChungTuChiTietFilter = x => x.INamLamViec == namLamViec && x.INamNganSach == namNganSach && x.IIdMaNguonNganSach == nguonNganSach && x.ILoai == iLoai;
                Func<NsSktChungTu, bool> defaultSktChungTuFilter = x => x.INamLamViec == namLamViec && x.INamNganSach == namNganSach && x.IIdMaNguonNganSach == nguonNganSach && x.ILoai == iLoai && x.ILoaiChungTu == loaiChungTu && x.ILoaiNguonNganSach == iLoaiNguonNganSach;
                Func<DonVi, bool> defaultNsDonViFilter = x => x.NamLamViec == namLamViec && x.ITrangThai == iTrangThai;
                var nsDonVi = ctx.NsDonVis.Where(defaultNsDonViFilter).FirstOrDefault(x => x.IIDMaDonVi == idDonVi);
                var sktChungTuSummary = ctx.NsSktChungTus.AsNoTracking().AsEnumerable().FirstOrDefault(x => x.Id == sktChungTuId);
                List<NsSktChungTu> lstChungTuChild = new List<NsSktChungTu>();
                if (sktChungTuSummary != null)
                {
                    lstChungTuChild = ctx.NsSktChungTus.AsNoTracking().AsEnumerable().Where(defaultSktChungTuFilter)
                        .Where(x => sktChungTuSummary.SDssoChungTuTongHop.Contains(x.SSoChungTu)).OrderByDescending(x => x.SSoChungTu).ToList();
                }

                var lstIdChungTuChild = lstChungTuChild.Select(x => x.Id).ToList();
                var lstIdDonViChild = lstChungTuChild.Select(x => x.IIdMaDonVi).ToList();
                var lstDonViChild = ctx.NsDonVis.Where(defaultNsDonViFilter).Where(x => lstIdDonViChild.Contains(x.IIDMaDonVi)).ToList();
                List<DonVi> lstDonViChildOrder = new List<DonVi>();
                foreach (var it in lstIdDonViChild)
                {
                    var dvOrder = lstDonViChild.FirstOrDefault(x => x.IIDMaDonVi.Equals(it));
                    if (dvOrder != null)
                    {
                        lstDonViChildOrder.Add(dvOrder);
                    }
                }
                lstDonViChildOrder = lstDonViChildOrder.OrderBy(x => x.IIDMaDonVi).ToList();
                var sktChungTuChiTiets = ctx.NsSktChungTuChiTiets.AsNoTracking().AsEnumerable()
                    .Where(defaultSktChungTuChiTietFilter).Where(x => x.IIdCtsoKiemTra == sktChungTuId || lstIdChungTuChild.Contains(x.IIdCtsoKiemTra)).ToList();
                sktChungTuChiTiets = sktChungTuChiTiets
                    .Where(x => x.FSoNganhPhanCap != 0 || x.FKhungNganSachDuocDuyet != 0 || x.FTuChi != 0 || x.FTuChiDeNghi != 0 || x.FHuyDongTonKho != 0 || x.FTonKhoDenNgay != 0 || x.FMuaHangCapHienVat != 0 || x.FPhanCap != 0 || !string.IsNullOrEmpty(x.SGhiChu)).ToList();
                var lstIdMuclucDisplay = new List<string>();
                if (hienThi.Equals(DataStateValue.DA_NHAP_SKT))
                {
                    lstIdMuclucDisplay = sktChungTuChiTiets.Select(x => x.SKyHieu).Distinct().ToList();
                }
                var sktMucLucs = GetListMucLucSktByLoaiChungTu(iLoai, loaiChungTu, namLamViec, idDonVi, lstIdMuclucDisplay, hienThi, null);
                var lstSktMlChild = sktMucLucs.Where(x => !x.BHangCha).ToList();

                foreach (var mucLuc in lstSktMlChild)
                {
                    var ml = sktMucLucs.FirstOrDefault(x => x.IIDMLSKT.Equals(mucLuc.IIDMLSKT));
                    if (ml != null)
                    {
                        ml.BHangCha = true;
                        ml.IdDonVi = nsDonVi.IIDMaDonVi;
                    }
                    foreach (var donVi in lstDonViChildOrder)
                    {
                        var childRow = mucLuc.Clone();
                        childRow.Id = Guid.NewGuid();
                        childRow.IdDonVi = donVi.IIDMaDonVi;
                        childRow.TenDonVi = donVi.TenDonVi;
                        childRow.IIDMLSKTCha = ml.IIDMLSKT;
                        childRow.SSTT = null;
                        childRow.SMoTa = donVi.TenDonVi;
                        childRow.BHangCha = false;
                        sktMucLucs.Add(childRow);
                    }
                }

                var result = from sktMucLuc in sktMucLucs
                             join chungtuChild in lstChungTuChild on sktMucLuc.IdDonVi equals chungtuChild.IIdMaDonVi into chungtus
                             from ct in chungtus.DefaultIfEmpty()
                             join sktChungTuChiTiet in sktChungTuChiTiets on new { SKyHieu = sktMucLuc.SKyHieu, sktMucLuc.IdDonVi } equals new
                             { SKyHieu = sktChungTuChiTiet.SKyHieu, IdDonVi = sktChungTuChiTiet.IIdMaDonVi } into gj
                             from sub in gj.DefaultIfEmpty()
                                 /*join cancu in CanCuData on new {chungtuId = ct == null ? Guid.Empty : ct.Id, iidmlskt = sktMucLuc.IIDMLSKT } equals new { chungtuId = cancu.IiIdCtsoKiemTra , iidmlskt = cancu.IIdMlskt} into cancuTbl
                                 from cc in cancuTbl.DefaultIfEmpty()*/
                             select new NsSktChungTuChiTiet
                             {
                                 Id = sub == null ? Guid.NewGuid() : sub.Id == Guid.Empty ? Guid.NewGuid() : sub.Id,
                                 Stt = sktMucLuc.SSTT,
                                 SSttbc = sktMucLuc.SSttBC,
                                 SMoTa = sktMucLuc.SMoTa,
                                 SKyHieu = sktMucLuc.SKyHieu,
                                 SKyHieuCu = sktMucLuc.SKyHieuCu,
                                 SL = sktMucLuc.SL,
                                 SK = sktMucLuc.SK,
                                 SM = sktMucLuc.SM,
                                 Nganh = sktMucLuc.SNg,
                                 FHuyDongTonKho = sub == null ? 0 : sub.FHuyDongTonKho,
                                 FTuChi = sub == null ? 0 : sub.FTuChi,
                                 FTuChiDeNghi = sub == null ? 0 : sub.FTuChiDeNghi,
                                 INamLamViec = sub == null ? namLamViec : sub.INamLamViec,
                                 INamNganSach = namNganSach,
                                 IIdMaNguonNganSach = nguonNganSach,
                                 ILoaiChungTu = loaiChungTu,
                                 IIdCtsoKiemTra = sktChungTuId,
                                 // khi view chung tu chi tiet cua chung tu tong hop, IIdCtsoKiemTraChild la id chung tu, IIdCtsoKiemTra la chung tu tong hop
                                 IIdCtsoKiemTraChild = sub == null ? (ct != null ? ct.Id : Guid.Empty) : sub.IIdCtsoKiemTra,
                                 IsHangCha = sktMucLuc.BHangCha,
                                 //IIdMaDonVi = sub == null ? string.Empty : sub.IIdMaDonVi,
                                 IIdMaDonVi = sktMucLuc.IdDonVi,
                                 IdParent = sktMucLuc.IIDMLSKTCha,
                                 STenDonVi = nsDonVi == null ? string.Empty : nsDonVi.TenDonVi,
                                 IIdMlskt = sktMucLuc.IIDMLSKT,
                                 IsAdd = sub == null,
                                 SGhiChu = sub != null ? sub.SGhiChu : "",
                                 ILoai = iLoai,
                                 DNgayTao = null,
                                 SNguoiTao = null,
                                 DNgaySua = null,
                                 SNguoiSua = null,
                                 FHienVat = sub == null ? 0 : sub.FHienVat,
                                 FMuaHangCapHienVat = sub == null ? 0 : sub.FMuaHangCapHienVat,
                                 FPhanCap = sub == null ? 0 : sub.FPhanCap,
                                 FSoNganhPhanCap = sub == null ? 0 : sub.FSoNganhPhanCap,
                                 FKhungNganSachDuocDuyet = sub == null ? 0 : sub.FKhungNganSachDuocDuyet,
                                 FTonKhoDenNgay = sub == null ? 0 : sub.FTonKhoDenNgay,
                                 FThongBaoDonVi = sub == null ? 0 : sub.FThongBaoDonVi,
                             };

                return result.OrderBy(sktMucLuc => sktMucLuc.SSttbc).ToList();
            }
        }

        public IEnumerable<NsSktChungTuChiTiet> FindByConditionForParentUnit(SktChungTuChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                int namLamViec = searchCondition.NamLamViec;
                int namNganSach = searchCondition.NamNganSach;
                int nguonNganSach = searchCondition.NguonNganSach;
                int iLoai = searchCondition.ILoai;
                int iTrangThai = searchCondition.ITrangThai;
                string idDonVi = searchCondition.IdDonVi;
                int loaiChungTu = searchCondition.LoaiChungTu;
                Guid sktChungTuId = searchCondition.SktChungTuId;
                int hienThi = searchCondition.HienThi;
                var donViSearch = searchCondition.IdDonViSearch;
                string chuyenNganh = searchCondition.ChuyenNganh;
                var nsDonVi = ctx.NsDonVis.FirstOrDefault(x => x.NamLamViec == namLamViec && x.ITrangThai == iTrangThai && x.IIDMaDonVi == idDonVi);
                var listNsDonViChild = GetListDonViByLoaiChungTu(loaiChungTu, namLamViec);
                var sktChungTuChiTiets = ctx.NsSktChungTuChiTiets.AsNoTracking()
                    .Where(x => x.IIdCtsoKiemTra == sktChungTuId)
                    .ToList();
                sktChungTuChiTiets = sktChungTuChiTiets
                    .Where(x => x.FTuChi != 0 || x.FMuaHangCapHienVat != 0 || x.FHuyDongTonKho != 0 || x.FPhanCap != 0 || x.FThongBaoDonVi != 0 || !string.IsNullOrEmpty(x.SGhiChu)).ToList();
                if (chuyenNganh != null)
                {
                    var lstMucLucNganh = ctx.NsSktMucLucs.AsNoTracking().Where(x => x.INamLamViec == namLamViec
                        && x.ITrangThai == StatusType.ACTIVE
                        && chuyenNganh.Contains(x.SNg)).ToList();
                    var lstIdMucLucNganh = lstMucLucNganh.Select(x => x.SKyHieu).ToList();
                    sktChungTuChiTiets = sktChungTuChiTiets.Where(x => lstIdMucLucNganh.Contains(x.SKyHieu)).ToList();
                }

                var lstIdMuclucDisplay = new List<string>();
                if (hienThi.Equals(DataStateValue.DA_NHAP_SKT))
                {
                    List<string> idDonViDisplay = sktChungTuChiTiets.Select(x => x.IIdMaDonVi).Distinct().ToList();
                    lstIdMuclucDisplay = sktChungTuChiTiets.Select(x => x.SKyHieu).Distinct().ToList();
                    listNsDonViChild = listNsDonViChild.Where(x => idDonViDisplay.Contains(x.IIDMaDonVi)).ToList();
                }
                if (hienThi.Equals(DataStateValue.DA_NHAN_SKT) || hienThi.Equals(DataStateValue.SO_CON_LAI_CHUA_PHAN_BO))
                {
                    lstIdMuclucDisplay = sktChungTuChiTiets.Where(x => DemandCheckType.CHECK.Equals(x.ILoai)).Select(x => x.SKyHieu).Distinct().ToList();
                }

                //if (!string.IsNullOrEmpty(donViSearch))
                //{
                //    listNsDonViChild = listNsDonViChild.Where(x => donViSearch.Equals(x.IIDMaDonVi)).ToList();
                //}

                List<NsSktChungTuChiTiet> sktChungTuChiTietSNC = FindCanCuSoNhuCauPhanBoSKT(searchCondition);
                var sktChungTuChiTietSKTNamTruoc = ctx.NsSktChungTuChiTiets.AsNoTracking()
                    .Where(x => x.INamLamViec == (namLamViec - 1) && x.INamNganSach == namNganSach && x.IIdMaNguonNganSach == nguonNganSach && (x.ILoai == DemandCheckType.CHECK || x.ILoai == DemandCheckType.DISTRIBUTION || x.ILoai == DemandCheckType.CORPORATIZED_HOSPITAL) && x.ILoaiChungTu == loaiChungTu)
                    .ToList();

                var sktChungTuSKTNamTruoc = ctx.NsSktChungTus.AsNoTracking().Where(x => x.INamLamViec == (namLamViec - 1) && x.INamNganSach == namNganSach && x.IIdMaNguonNganSach == nguonNganSach && x.BKhoa).ToList();

                var sktChungTuChiTietSKTNamTruocJoin = from chitiet in sktChungTuChiTietSKTNamTruoc
                                                       join chungtu in sktChungTuSKTNamTruoc on chitiet.IIdCtsoKiemTra equals chungtu.Id
                                                       where (chitiet.ILoai == 2 && chungtu.ILoai == 3) || chitiet.ILoai != 2
                                                       select chitiet;

                //group by theo mục lục ngân sách số chứng từ chi tiết năm trc - do nghiệp vụ thay đổi được tạo nhiều chứng từ
                var sktChungTuChiTietSKTNamTruocGroup = sktChungTuChiTietSKTNamTruocJoin.GroupBy(x => new { x.SKyHieu, x.IIdMaDonVi })
                                                         .Select(cl => new NsSktChungTuChiTiet
                                                         {
                                                             SoKiemTra = cl.Sum(x => x.FTuChi),
                                                             SoKiemTraMHHV = cl.Sum(x => x.FMuaHangCapHienVat),
                                                             SoKiemTraDT = cl.Sum(x => x.FPhanCap),
                                                             IIdMlskt = cl.First().IIdMlskt,
                                                             SKyHieu = cl.First().SKyHieu,
                                                             IIdMaDonVi = cl.First().IIdMaDonVi
                                                         }).ToList();


                var sktMucLucs = GetListMucLucSktByLoaiChungTu(iLoai, loaiChungTu, namLamViec, idDonVi, lstIdMuclucDisplay, hienThi, chuyenNganh);
                var listMLSKTChild = sktMucLucs.Where(x => x.INamLamViec == namLamViec && x.ITrangThai == iTrangThai && x.BHangCha == false).ToList();
                var listParent = sktMucLucs.Where(x => x.INamLamViec == namLamViec && x.ITrangThai == iTrangThai && x.BHangCha).ToList();
                var listMLSKT = listParent.Union(listMLSKTChild).ToList();

                Dictionary<string, List<NsSktMucLuc>> data = new Dictionary<string, List<NsSktMucLuc>>();
                foreach (var dv in listNsDonViChild)
                {
                    var lstSktMl = CheckSktMucLucByDonViQuanLy(loaiChungTu, namLamViec, dv.IIDMaDonVi);
                    data.Add(dv.IIDMaDonVi, lstSktMl);
                }

                listNsDonViChild = listNsDonViChild.OrderBy(x => x.IIDMaDonVi).ToList();
                foreach (var mucLuc in listMLSKTChild)
                {
                    var remainRow = mucLuc.Clone();
                    mucLuc.BHangCha = true;
                    mucLuc.IdDonVi = nsDonVi.IIDMaDonVi;
                    mucLuc.IsFirstParentRow = true;
                    remainRow.IIDMLSKT = Guid.NewGuid();
                    remainRow.IIDMLSKTCha = mucLuc.IIDMLSKT;
                    remainRow.SMoTa = "-- Số chưa phân bổ --";
                    remainRow.IsRemainRow = true;
                    remainRow.BHangCha = true;
                    remainRow.SSTT = null;
                    listMLSKT.Add(remainRow);
                    foreach (var donVi in listNsDonViChild)
                    {
                        var check = loaiChungTu == 1 || _idDonViDuPhong.Equals(donVi.IIDMaDonVi)
                                    || data[donVi.IIDMaDonVi].Exists(x => x.SKyHieu.Equals(mucLuc.SKyHieu));
                        if (check)
                        {
                            var childRow = mucLuc.Clone();
                            childRow.Id = Guid.NewGuid();
                            childRow.IdDonVi = donVi.IIDMaDonVi;
                            childRow.TenDonVi = donVi.TenDonVi;
                            childRow.SKhoiDonVi = donVi.Khoi;
                            childRow.IIDMLSKTCha = remainRow.IIDMLSKT;
                            childRow.SSTT = null;
                            childRow.BHangCha = false;
                            listMLSKT.Add(childRow);
                        }
                    }
                }

                var result = from sktMucLuc in listMLSKT
                             join sktChungTuChiTiet in sktChungTuChiTiets.GroupBy(x => new { x.IIdMaDonVi, x.SKyHieu }).Select(gr => gr.First()) on new { sktMucLuc.SKyHieu, sktMucLuc.IdDonVi } equals new
                             { SKyHieu = sktChungTuChiTiet.SKyHieu, IdDonVi = sktChungTuChiTiet.IIdMaDonVi }
                                          into gj
                             from sub in gj.DefaultIfEmpty()
                             join sktChungTuChiTietSnc in sktChungTuChiTietSNC on new { sktMucLuc.SKyHieu, sktMucLuc.IdDonVi } equals new
                             { SKyHieu = sktChungTuChiTietSnc.SKyHieu, IdDonVi = sktChungTuChiTietSnc.IIdMaDonVi }
                                 into snc
                             from sncResult in snc.DefaultIfEmpty()
                             join sktNamTruoc in sktChungTuChiTietSKTNamTruocGroup on new { SKyHieu = sktMucLuc.SKyHieuCu, sktMucLuc.IdDonVi } equals new
                             { SKyHieu = sktNamTruoc.SKyHieu, IdDonVi = sktNamTruoc.IIdMaDonVi }
                                 into skt
                             from sktResult in skt.DefaultIfEmpty()
                             orderby sktMucLuc.SKyHieu
                             select new NsSktChungTuChiTiet
                             {
                                 Id = sub == null ? Guid.NewGuid() : sub.Id == Guid.Empty ? Guid.NewGuid() : sub.Id,
                                 Stt = sktMucLuc.SSTT,
                                 SSttbc = sktMucLuc.SSttBC,
                                 SMoTa = sktMucLuc.SMoTa,
                                 SKyHieu = sktMucLuc.SKyHieu,
                                 SKyHieuCu = sktMucLuc.SKyHieuCu,
                                 SL = sktMucLuc.SL,
                                 SK = sktMucLuc.SK,
                                 SM = sktMucLuc.SM,
                                 Nganh = sktMucLuc.SNg,
                                 NganhParent = sktMucLuc.SNGCha,
                                 FHuyDongTonKho = sub == null ? 0 : sub.FHuyDongTonKho,
                                 FTuChi = sub == null ? 0 : sub.FTuChi,
                                 FTuChiDeNghi = sub == null ? 0 : sub.FTuChiDeNghi,
                                 INamLamViec = sub == null ? namLamViec : sub.INamLamViec,
                                 INamNganSach = namNganSach,
                                 IIdMaNguonNganSach = nguonNganSach,
                                 IIdCtsoKiemTra = sktChungTuId,
                                 IsHangCha = sktMucLuc.BHangCha,
                                 IIdMaDonVi = sktMucLuc.IdDonVi,
                                 SKhoiDonVi = sktMucLuc.SKhoiDonVi,
                                 IdParent = sktMucLuc.IIDMLSKTCha,
                                 ILoaiChungTu = loaiChungTu,
                                 IsFirstParentRow = sktMucLuc.IsFirstParentRow,
                                 IsRemainRow = sktMucLuc.IsRemainRow,
                                 STenDonVi = sktMucLuc.TenDonVi,
                                 IIdMlskt = sktMucLuc.IIDMLSKT,
                                 IsAdd = sub == null,
                                 SGhiChu = sub != null ? sub.SGhiChu : "",
                                 ILoai = iLoai,
                                 DNgayTao = null,
                                 SNguoiTao = null,
                                 DNgaySua = null,
                                 SNguoiSua = null,
                                 FHienVat = sub == null ? 0 : sub.FHienVat,
                                 FMuaHangCapHienVat = sub == null ? 0 : sub.FMuaHangCapHienVat,
                                 FPhanCap = sub == null ? 0 : sub.FPhanCap,
                                 FKhungNganSachDuocDuyet = sub == null ? 0 : sub.FKhungNganSachDuocDuyet,
                                 FSoNganhPhanCap = sub == null ? 0 : sub.FSoNganhPhanCap,
                                 FTonKhoDenNgay = sub == null ? 0 : sub.FTonKhoDenNgay,
                                 FThongBaoDonVi = sub == null ? 0 : sub.FThongBaoDonVi,
                                 SoNhuCau = sncResult == null ? 0 : sncResult.FTuChi,
                                 SoNhuCauMHHV = sncResult == null ? 0 : sncResult.FMuaHangCapHienVat,
                                 SoNhuCauDT = sncResult == null ? 0 : sncResult.FPhanCap,
                                 SoKiemTra = sktResult == null ? 0 : sktResult.SoKiemTra,
                                 SoKiemTraMHHV = sktResult == null ? 0 : sktResult.SoKiemTraMHHV,
                                 SoKiemTraDT = sktResult == null ? 0 : sktResult.SoKiemTraDT
                             };

                var resultGroup = from p in result
                                  group p by new { p.SKyHieu, p.IIdMaDonVi, p.SMoTa, p.STenDonVi } into g
                                  select new NsSktChungTuChiTiet()
                                  {
                                      Id = g.First().Id,
                                      Stt = g.First().Stt,
                                      SSttbc = g.First().SSttbc,
                                      SMoTa = g.First().SMoTa,
                                      SKyHieu = g.First().SKyHieu,
                                      SKyHieuCu = g.First().SKyHieuCu,
                                      SL = g.First().SL,
                                      SK = g.First().SK,
                                      SM = g.First().SM,
                                      Nganh = g.First().Nganh,
                                      NganhParent = g.First().NganhParent,
                                      FHuyDongTonKho = g.First().FHuyDongTonKho,
                                      FTuChi = g.First().FTuChi,
                                      FTuChiDeNghi = g.First().FTuChiDeNghi,
                                      INamLamViec = g.First().INamLamViec,
                                      INamNganSach = g.First().INamNganSach,
                                      IIdMaNguonNganSach = g.First().IIdMaNguonNganSach,
                                      IIdCtsoKiemTra = g.First().IIdCtsoKiemTra,
                                      IsHangCha = g.First().IsHangCha,
                                      IIdMaDonVi = g.First().IIdMaDonVi,
                                      SKhoiDonVi = g.First().SKhoiDonVi,
                                      IdParent = g.First().IdParent,
                                      ILoaiChungTu = g.First().ILoaiChungTu,
                                      IsFirstParentRow = g.First().IsFirstParentRow,
                                      IsRemainRow = g.First().IsRemainRow,
                                      STenDonVi = g.First().STenDonVi,
                                      IIdMlskt = g.First().IIdMlskt,
                                      IsAdd = g.First().IsAdd,
                                      SGhiChu = g.First().SGhiChu,
                                      ILoai = g.First().ILoai,
                                      DNgayTao = null,
                                      SNguoiTao = null,
                                      DNgaySua = null,
                                      SNguoiSua = null,
                                      FHienVat = g.First().FHienVat,
                                      FMuaHangCapHienVat = g.First().FMuaHangCapHienVat,
                                      FPhanCap = g.First().FPhanCap,
                                      FSoNganhPhanCap = g.First().FSoNganhPhanCap,
                                      FKhungNganSachDuocDuyet = g.First().FKhungNganSachDuocDuyet,
                                      FTonKhoDenNgay = g.First().FTonKhoDenNgay,
                                      FThongBaoDonVi = g.First().FThongBaoDonVi,
                                      SoNhuCau = g.Sum(x => x.SoNhuCau),
                                      SoNhuCauMHHV = g.Sum(x => x.SoNhuCauMHHV),
                                      SoNhuCauDT = g.Sum(x => x.SoNhuCauDT),
                                      SoKiemTra = g.First().SoKiemTra,
                                      SoKiemTraMHHV = g.First().SoKiemTraMHHV,
                                      SoKiemTraDT = g.First().SoKiemTraDT
                                  };

                if (hienThi.Equals(DataStateValue.DA_NHAP_SKT))
                {
                    result = result.Where(x =>
                        x.IsHangCha || x.FTuChi != 0 || x.FMuaHangCapHienVat != 0 || x.FPhanCap != 0);
                }
                return resultGroup.OrderBy(t => t.SSttbc).ToList();
            }
        }

        public IEnumerable<SoKyHieuMucLucNganSachQuery> FindSoKyHieus(int namLamViec, string iID_MaBQuanLy, int nguonNganSach)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter YearOfWork = new SqlParameter("@YearOfWork", namLamViec);
                SqlParameter MaBQuanLy = new SqlParameter("@MaBQuanLy", iID_MaBQuanLy);
                SqlParameter NguonNganSach = new SqlParameter("@NguonNganSach", nguonNganSach);
                //return ctx.FromSqlRaw<SoKyHieuMucLucNganSachQuery>("EXECUTE dbo.sp_ns_get_skt_chungtuchitiet @YearOfWork, @MaBQuanLy ",
                //    YearOfWork, MaBQuanLy).ToList();
                return ctx.FromSqlRaw<SoKyHieuMucLucNganSachQuery>("EXECUTE dbo.sp_ns_get_skt_chungtuchitiet1 @YearOfWork, @MaBQuanLy, @NguonNganSach ",
                    YearOfWork, MaBQuanLy, NguonNganSach).ToList();
            }
        }

        public IEnumerable<NsSktChungTuChiTiet> FindByConditionForParentUnitByIdMucLuc(SktChungTuChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                int namLamViec = searchCondition.NamLamViec;
                int namNganSach = searchCondition.NamNganSach;
                int nguonNganSach = searchCondition.NguonNganSach;
                int iLoai = searchCondition.ILoai;
                int iTrangThai = searchCondition.ITrangThai;
                string idDonVi = searchCondition.IdDonVi;
                int loaiChungTu = searchCondition.LoaiChungTu;
                Guid sktChungTuId = searchCondition.SktChungTuId;
                int hienThi = searchCondition.HienThi;
                Guid idMuLuc = searchCondition.IdMucLucSkt;
                var nsDonVi = ctx.NsDonVis.FirstOrDefault(x => x.NamLamViec == namLamViec && x.ITrangThai == iTrangThai && x.IIDMaDonVi == idDonVi);

                var listNsDonViChild = GetListDonViByLoaiChungTu(loaiChungTu, namLamViec);
                var sktChungTuChiTiets = ctx.NsSktChungTuChiTiets.AsNoTracking()
                    .Where(x => x.IIdCtsoKiemTra == sktChungTuId)
                    .ToList();
                List<NsSktChungTuChiTiet> sktChungTuChiTietSNC = FindCanCuSoNhuCauPhanBoSKT(searchCondition);
                var sktChungTuChiTietSKTNamTruoc = ctx.NsSktChungTuChiTiets.AsNoTracking()
                    .Where(x => x.INamLamViec == (namLamViec - 1) && x.INamNganSach == namNganSach && x.IIdMaNguonNganSach == nguonNganSach && x.ILoai == DemandCheckType.CHECK && x.ILoaiChungTu == loaiChungTu)
                    .ToList();
                var listMLById = FindMucLucSKTTheoNganh(idMuLuc.ToString(), 0, namLamViec).Select(item => item.IIdMlskt).ToList();
                var listMLSKTChild = ctx.NsSktMucLucs.AsNoTracking().Where(x => x.INamLamViec == namLamViec && x.ITrangThai == iTrangThai && listMLById.Contains(x.IIDMLSKT) && x.BHangCha == false).ToList();
                var listParent = ctx.NsSktMucLucs.AsNoTracking().Where(x => x.INamLamViec == namLamViec && x.ITrangThai == iTrangThai && listMLById.Contains(x.IIDMLSKT) && x.BHangCha == true).ToList();
                var listMLSKT = listParent.Union(listMLSKTChild).ToList();

                Dictionary<string, List<NsSktMucLuc>> data = new Dictionary<string, List<NsSktMucLuc>>();
                foreach (var dv in listNsDonViChild)
                {
                    var lstSktMl = CheckSktMucLucByDonViQuanLy(loaiChungTu, namLamViec, dv.IIDMaDonVi);
                    data.Add(dv.IIDMaDonVi, lstSktMl);
                }
                foreach (var mucLuc in listMLSKTChild)
                {
                    var remainRow = mucLuc.Clone();
                    mucLuc.BHangCha = true;
                    mucLuc.IdDonVi = nsDonVi.IIDMaDonVi;
                    mucLuc.IsFirstParentRow = true;
                    remainRow.IIDMLSKT = Guid.NewGuid();
                    remainRow.IIDMLSKTCha = mucLuc.IIDMLSKT;
                    remainRow.SMoTa = "-- Số chưa phân bổ --";
                    remainRow.IsRemainRow = true;
                    remainRow.BHangCha = true;
                    remainRow.SSTT = null;
                    listMLSKT.Add(remainRow);
                    foreach (var donVi in listNsDonViChild)
                    {
                        var check = loaiChungTu == 1
                                    || data[donVi.IIDMaDonVi].Exists(x => x.IIDMLSKT.Equals(mucLuc.IIDMLSKT));
                        if (check)
                        {
                            var childRow = mucLuc.Clone();
                            childRow.Id = Guid.NewGuid();
                            childRow.IdDonVi = donVi.IIDMaDonVi;
                            childRow.TenDonVi = donVi.TenDonVi;
                            childRow.SKhoiDonVi = donVi.Khoi;
                            childRow.IIDMLSKTCha = remainRow.IIDMLSKT;
                            childRow.SSTT = null;
                            childRow.BHangCha = false;
                            listMLSKT.Add(childRow);
                        }
                    }
                }
                var result = from sktMucLuc in listMLSKT
                             join sktChungTuChiTiet in sktChungTuChiTiets on new { sktMucLuc.IIDMLSKT, sktMucLuc.IdDonVi } equals new
                             {
                                 IIDMLSKT = sktChungTuChiTiet.IIdMlskt,
                                 IdDonVi = sktChungTuChiTiet.IIdMaDonVi
                             }
                                          into gj
                             from sub in gj.DefaultIfEmpty()
                             join sktChungTuChiTietSnc in sktChungTuChiTietSNC on new { sktMucLuc.IIDMLSKT, sktMucLuc.IdDonVi } equals new
                             {
                                 IIDMLSKT = sktChungTuChiTietSnc.IIdMlskt,
                                 IdDonVi = sktChungTuChiTietSnc.IIdMaDonVi
                             }
                                 into snc
                             from sncResult in snc.DefaultIfEmpty()
                             join sktNamTruoc in sktChungTuChiTietSKTNamTruoc on new { sktMucLuc.IIDMLSKT, sktMucLuc.IdDonVi } equals new
                             { IIDMLSKT = sktNamTruoc.IIdMlskt, IdDonVi = sktNamTruoc.IIdMaDonVi }
                                 into skt
                             from sktResult in skt.DefaultIfEmpty()
                             orderby sktMucLuc.SKyHieu
                             select new NsSktChungTuChiTiet
                             {
                                 Id = sub == null ? Guid.NewGuid() : sub.Id == Guid.Empty ? Guid.NewGuid() : sub.Id,
                                 Stt = sktMucLuc.SSTT,
                                 SSttbc = sktMucLuc.SSttBC,
                                 SMoTa = sktMucLuc.SMoTa,
                                 SKyHieu = sktMucLuc.SKyHieu,
                                 Nganh = sktMucLuc.SNg,
                                 NganhParent = sktMucLuc.SNGCha,
                                 FHuyDongTonKho = sub == null ? 0 : sub.FHuyDongTonKho,
                                 FTuChi = sub == null ? 0 : sub.FTuChi,
                                 FTuChiDeNghi = sub == null ? 0 : sub.FTuChiDeNghi,
                                 INamLamViec = namLamViec,
                                 INamNganSach = namNganSach,
                                 IIdMaNguonNganSach = nguonNganSach,
                                 IIdCtsoKiemTra = sktChungTuId,
                                 ILoaiChungTu = loaiChungTu,
                                 IsHangCha = sktMucLuc.BHangCha,
                                 IIdMaDonVi = sktMucLuc.IdDonVi,
                                 SKhoiDonVi = sktMucLuc.SKhoiDonVi,
                                 IdParent = sktMucLuc.IIDMLSKTCha,
                                 IsFirstParentRow = sktMucLuc.IsFirstParentRow,
                                 IsRemainRow = sktMucLuc.IsRemainRow,
                                 STenDonVi = sktMucLuc.TenDonVi,
                                 IIdMlskt = sktMucLuc.IIDMLSKT,
                                 IsAdd = sub == null,
                                 SGhiChu = sub != null ? sub.SGhiChu : "",
                                 ILoai = iLoai,
                                 DNgayTao = null,
                                 SNguoiTao = null,
                                 DNgaySua = null,
                                 SNguoiSua = null,
                                 FHienVat = sub == null ? 0 : sub.FHienVat,
                                 FMuaHangCapHienVat = sub == null ? 0 : sub.FMuaHangCapHienVat,
                                 FPhanCap = sub == null ? 0 : sub.FPhanCap,
                                 FKhungNganSachDuocDuyet = sub == null ? 0 : sub.FKhungNganSachDuocDuyet,
                                 FSoNganhPhanCap = sub == null ? 0 : sub.FSoNganhPhanCap,
                                 FTonKhoDenNgay = sub == null ? 0 : sub.FTonKhoDenNgay,
                                 SoNhuCau = sncResult == null ? 0 : sncResult.FTuChi,
                                 SoNhuCauMHHV = sncResult == null ? 0 : sncResult.FMuaHangCapHienVat,
                                 SoNhuCauDT = sncResult == null ? 0 : sncResult.FPhanCap,
                                 SoKiemTra = sktResult == null ? 0 : sktResult.FTuChi,
                                 SoKiemTraMHHV = sktResult == null ? 0 : sktResult.FMuaHangCapHienVat,
                                 SoKiemTraDT = sktResult == null ? 0 : sktResult.FPhanCap
                             };
                return result.OrderBy(t => t.SSttbc).ToList();

            }
        }

        public List<NsSktMucLuc> GetListMucLucSktByLoaiChungTu(int iloai, int loaiChungTu, int namLamViec, string idDonVi, List<string> lstIdMuclucDisplay, int hienThi, string chuyenNganh)
        {

            using (var ctx = _contextFactory.CreateDbContext())
            {
                var loaiChungTuNSSD = int.Parse(VoucherType.NSSD_Key);
                var loaiChungTuNSBD = int.Parse(VoucherType.NSBD_Key);
                var iTrangThai = StatusType.ACTIVE;
                Func<NsSktMucLuc, bool> defaultSktMucLucFilter = x => x.INamLamViec == namLamViec;
                var nsDonVi = ctx.NsDonVis.Where(x => x.IIDMaDonVi == idDonVi && x.NamLamViec == namLamViec && x.ITrangThai == iTrangThai).FirstOrDefault();
                if (nsDonVi == null) return new List<NsSktMucLuc>();
                List<NsSktMucLuc> sktMucLucsChild = new List<NsSktMucLuc>();
                var danhMucNganh = ctx.DanhMucs.AsNoTracking()
                    .AsEnumerable().Where(x => x.INamLamViec == namLamViec && x.ITrangThai == iTrangThai && VoucherType.DM_Nganh.Equals(x.SType) && x.SGiaTri != null && x.SGiaTri.Split(",").Contains(idDonVi)).ToList();
                if (DemandCheckType.CHECK.Equals(iloai) && loaiChungTu != loaiChungTuNSSD || DemandCheckType.DEMAND.Equals(iloai) && loaiChungTu != loaiChungTuNSSD && "0".Equals(nsDonVi.Loai)
                    || DemandCheckType.DISTRIBUTION.Equals(iloai) && loaiChungTu != loaiChungTuNSSD && _idDonViDuPhong.Equals(nsDonVi.IIDMaDonVi))
                {
                    danhMucNganh = ctx.DanhMucs.AsNoTracking()
                        .AsEnumerable().Where(x => x.INamLamViec == namLamViec && x.ITrangThai == iTrangThai && VoucherType.DM_Nganh.Equals(x.SType) && !StringUtils.IsNullOrEmpty(x.SGiaTri)).ToList();
                    var listIdCodeNganh = danhMucNganh.Select(x => x.IIDMaDanhMuc).ToList();
                    defaultSktMucLucFilter = x =>
                        x.INamLamViec == namLamViec && x.ITrangThai == iTrangThai && x.SLoaiNhap != null &&
                        x.SLoaiNhap.Split(',').Contains(loaiChungTu.ToString()) && listIdCodeNganh.Contains(x.SNg);
                }
                else if ("0".Equals(nsDonVi.Loai) || loaiChungTu == loaiChungTuNSSD)
                {
                    defaultSktMucLucFilter = x =>
                            x.INamLamViec == namLamViec && x.ITrangThai == iTrangThai && x.SLoaiNhap != null &&
                            x.SLoaiNhap.Split(',').Contains(loaiChungTu.ToString());
                }
                else
                {
                    var listIdCodeNganh = danhMucNganh.Select(x => x.IIDMaDanhMuc).ToList();
                    defaultSktMucLucFilter = x =>
                        x.INamLamViec == namLamViec && x.ITrangThai == iTrangThai && x.SLoaiNhap != null &&
                        x.SLoaiNhap.Split(',').Contains(loaiChungTu.ToString()) && listIdCodeNganh.Contains(x.SNg);
                }

                if (hienThi.Equals(DataStateValue.HIEN_THI_TAT_CA))
                {
                    sktMucLucsChild = ctx.NsSktMucLucs.AsNoTracking().AsEnumerable().Where(defaultSktMucLucFilter).ToList();
                }
                else
                {
                    sktMucLucsChild = ctx.NsSktMucLucs.AsNoTracking().AsEnumerable().Where(defaultSktMucLucFilter).Where(x => lstIdMuclucDisplay.Contains(x.SKyHieu)).ToList();

                }

                if (chuyenNganh != null)
                {
                    sktMucLucsChild = sktMucLucsChild.Where(x => chuyenNganh.Contains(x.SNg)).ToList();
                }

                List<Guid> listIdMlskt = new List<Guid>();
                List<NsSktMucLuc> sktMucLucs = new List<NsSktMucLuc>();
                if (sktMucLucsChild.Count > 0)
                {
                    listIdMlskt = sktMucLucsChild.Select(item => item.IIDMLSKT).ToList();
                    sktMucLucs = sktMucLucsChild;
                    while (true)
                    {
                        var listIdParent = sktMucLucsChild.Where(x => !listIdMlskt.Contains(x.IIDMLSKTCha.GetValueOrDefault())).Select(x => x.IIDMLSKTCha).ToList();
                        var listParent1 = ctx.NsSktMucLucs.Where(x => listIdParent.Contains(x.IIDMLSKT) && x.INamLamViec == namLamViec).ToList();
                        if (listParent1.Count > 0)
                        {
                            var lstId = listParent1.Select(item => item.IIDMLSKT).ToList();
                            listIdMlskt.AddRange(lstId);
                            sktMucLucs.AddRange(listParent1);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                sktMucLucs = sktMucLucs.GroupBy(x => x.IIDMLSKT).Select(x => x.First()).OrderBy(x => x.SSttBC).ToList();
                return sktMucLucs;
            }
        }

        public List<NsSktMucLuc> CheckSktMucLucByDonViQuanLy(int loaiChungTu, int namLamViec, string idDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var iTrangThai = StatusType.ACTIVE;
                Func<NsSktMucLuc, bool> defaultSktMucLucFilter;
                var nsDonVi = ctx.NsDonVis.Where(x => x.IIDMaDonVi == idDonVi && x.NamLamViec == namLamViec && x.ITrangThai == iTrangThai).FirstOrDefault();
                List<NsSktMucLuc> sktMucLucsChild = new List<NsSktMucLuc>();
                var danhMucNganh = ctx.DanhMucs.AsNoTracking()
                    .AsEnumerable().Where(x => x.INamLamViec == namLamViec && x.ITrangThai == iTrangThai && VoucherType.DM_Nganh.Equals(x.SType) && !string.IsNullOrEmpty(x.SGiaTri) && x.SGiaTri.Split(",").Contains(idDonVi)).ToList();
                if (nsDonVi.Loai == "0")
                {
                    defaultSktMucLucFilter = x =>
                        x.INamLamViec == namLamViec && x.ITrangThai == iTrangThai && x.SLoaiNhap != null &&
                        x.SLoaiNhap.Split(',').Contains(loaiChungTu.ToString());
                    sktMucLucsChild = ctx.NsSktMucLucs.AsNoTracking().AsEnumerable().Where(defaultSktMucLucFilter).ToList();
                }

                if (danhMucNganh.Count > 0 && !"0".Equals(nsDonVi.Loai) && loaiChungTu == 2)
                {
                    var listIdCodeNganh = danhMucNganh.Select(x => x.IIDMaDanhMuc).ToList();
                    defaultSktMucLucFilter = x =>
                        x.INamLamViec == namLamViec && x.ITrangThai == iTrangThai && x.SLoaiNhap != null &&
                        x.SLoaiNhap.Split(',').Contains(loaiChungTu.ToString()) && (listIdCodeNganh.Contains(x.SNg));
                    sktMucLucsChild = ctx.NsSktMucLucs.AsNoTracking().AsEnumerable().Where(defaultSktMucLucFilter).ToList();
                }
                return sktMucLucsChild;
            }
        }

        public List<DonVi> GetListDonViByLoaiChungTu(int loaiChungTu, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {

                int iTrangThai = StatusType.ACTIVE;
                string loaiDV = "";
                List<DonVi> listNsDonViChild = new List<DonVi>();
                var isDvCap4 = ctx.NsDonVis.AsNoTracking()
                    .Where(x => x.NamLamViec == namLamViec && x.ITrangThai == iTrangThai && x.Loai == "1").Count() <= 0;
                if (isDvCap4)
                {
                    loaiDV = "0";
                    listNsDonViChild = ctx.NsDonVis.Where(x => x.Loai == loaiDV && x.NamLamViec == namLamViec && x.ITrangThai == iTrangThai).ToList();
                }
                else
                {
                    loaiDV = "1";
                    if (loaiChungTu == 1)
                    {
                        listNsDonViChild = ctx.NsDonVis.Where(x => x.Loai == loaiDV && x.NamLamViec == namLamViec && x.ITrangThai == iTrangThai).ToList();
                    }
                    else if (loaiChungTu == 2)
                    {
                        listNsDonViChild = ctx.NsDonVis.Where(x => x.NamLamViec == namLamViec && x.ITrangThai == iTrangThai && x.Loai == loaiDV && (true.Equals(x.BCoNSNganh) || _idDonViDuPhong.Equals(x.IIDMaDonVi))).ToList();
                    }
                }

                return listNsDonViChild;
            }
        }

        public IEnumerable<SktChungTuChiTietQuery> FindReportNhapSoKiemTra(string idDonViUser, int namLamViec, int namNganSach, int nguonNganSach, int iLoai, int donViTinh, int loaiChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter agencyUserIdParam = new SqlParameter("@IdDonVi", idDonViUser);
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter namNganSachParm = new SqlParameter("@NamNganSach", namNganSach);
                SqlParameter nguonNganSachParm = new SqlParameter("@NguonNganSach", nguonNganSach);
                SqlParameter typeReport = new SqlParameter("@Loai", iLoai);
                SqlParameter unitCaculator = new SqlParameter("@DonViTinh", donViTinh);
                SqlParameter voucherType = new SqlParameter("@LoaiChungTu", loaiChungTu);
                return ctx.FromSqlRaw<SktChungTuChiTietQuery>("EXECUTE dbo.sp_skt_rpt_nhansokiemtra @IdDonVi, @NamLamViec, @NamNganSach, @NguonNganSach, @Loai, @LoaiChungTu, @DonViTinh ",
                    agencyUserIdParam, namLamViecParam, namNganSachParm, nguonNganSachParm, typeReport, voucherType, unitCaculator).ToList();
            }
        }

        public IEnumerable<ReportSoNhuCauTongHopQuery> FindReportSoNhuCauTongHop(string lstIdTongHop, int namLamViec, int namNganSach, int nguonNganSach,
            int loaiChungTu, int donViTinh, int iLoai)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter listIdChungTuTongHop = new SqlParameter("@ListIdChungTuTongHop", lstIdTongHop);
                SqlParameter typeVoucher = new SqlParameter("@LoaiChungTu", loaiChungTu);
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter namNganSachParm = new SqlParameter("@NamNganSach", namNganSach);
                SqlParameter nguonNganSachParm = new SqlParameter("@NguonNganSach", nguonNganSach);
                SqlParameter dvt = new SqlParameter("@dvt", donViTinh);
                SqlParameter loai = new SqlParameter("@iLoai", iLoai);
                return ctx.FromSqlRaw<ReportSoNhuCauTongHopQuery>("EXECUTE dbo.sp_rpt_skt_so_nhu_cau_tong_hop @ListIdChungTuTongHop, @LoaiChungTu, @NamLamViec, @NamNganSach, @NguonNganSach, @dvt, @iLoai ",
                    listIdChungTuTongHop, typeVoucher, namLamViecParam, namNganSachParm, nguonNganSachParm, dvt, loai).ToList();
            }
        }

        public IEnumerable<ReportPhanBoKiemTraTheoNganhPhuLucQuery> FindReportPhanBoChiTietMucLucDonVi(string nganh, int namLamViec, int namNganSach, int nguonNganSach, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter namNganSachParm = new SqlParameter("@NamNganSach", namNganSach);
                SqlParameter nguonNganSachParm = new SqlParameter("@NguonNganSach", nguonNganSach);
                SqlParameter dvt = new SqlParameter("@dvt", donViTinh);
                SqlParameter Nganh = new SqlParameter("@Nganh", nganh);
                return ctx.FromSqlRaw<ReportPhanBoKiemTraTheoNganhPhuLucQuery>("EXECUTE dbo.sp_rpt_phanbo_skt_mucluc_don_vi @Nganh, @NamLamViec, @NamNganSach, @NguonNganSach, @dvt ",
                   Nganh, namLamViecParam, namNganSachParm, nguonNganSachParm, dvt).ToList();
            }
        }

        public IEnumerable<ReportSoNhuCauTongHopDonViQuery> FindReportSoNhuCauTongHopDonVi(string lstIdTongHop, int namLamViec, int namNganSach, int nguonNganSach,
            int loaiChungTu, int donViTinh, int iLoai, string maBQuanLy, int loaiNNS)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter listIdChungTuTongHop = new SqlParameter("@ListIdChungTuTongHop", lstIdTongHop);
                SqlParameter typeVoucher = new SqlParameter("@LoaiChungTu", loaiChungTu);
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter namNganSachParm = new SqlParameter("@NamNganSach", namNganSach);
                SqlParameter nguonNganSachParm = new SqlParameter("@NguonNganSach", nguonNganSach);
                SqlParameter dvt = new SqlParameter("@dvt", donViTinh);
                SqlParameter loai = new SqlParameter("@iLoai", iLoai);
                SqlParameter maBQuanLyParam = new SqlParameter("@MaBQuanLy", maBQuanLy);
                SqlParameter loaiNNSParam = new SqlParameter("@loaiNNS", loaiNNS);
                return ctx.FromSqlRaw<ReportSoNhuCauTongHopDonViQuery>("EXECUTE dbo.sp_rpt_skt_so_nhu_cau_tong_hop_tung_don_vi @ListIdChungTuTongHop, @LoaiChungTu, @NamLamViec, @NamNganSach, @NguonNganSach, @dvt, @iLoai, @MaBQuanLy, @loaiNNS",
                    listIdChungTuTongHop, typeVoucher, namLamViecParam, namNganSachParm, nguonNganSachParm, dvt, loai, maBQuanLyParam, loaiNNSParam).ToList();
            }
        }

        public IEnumerable<ReportPhanBoSoKiemTraDonViQuery> FindReportPhanBoSoKiemTraDonVi(string idDonVi, string idChungTu, int loaiChungTu, int namLamViec, int namNganSach, int nguonNganSach,
             int donViTinh, int iLoai, int loaiNNS)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter idDV = new SqlParameter("@idDV", idDonVi);
                SqlParameter typeVoucher = new SqlParameter("@LoaiChungTu", loaiChungTu);
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter namNganSachParm = new SqlParameter("@NamNganSach", namNganSach);
                SqlParameter nguonNganSachParm = new SqlParameter("@NguonNganSach", nguonNganSach);
                SqlParameter dvt = new SqlParameter("@dvt", donViTinh);
                SqlParameter loai = new SqlParameter("@iLoai", iLoai);
                SqlParameter idCT = new SqlParameter("@idChungTu", idChungTu);
                SqlParameter iloaiNNS = new SqlParameter("@iLoaiNNS", loaiNNS);
                return ctx.FromSqlRaw<ReportPhanBoSoKiemTraDonViQuery>("EXECUTE dbo.sp_rpt_skt_phan_bo_so_kiem_tra_dv @idDV, @idChungTu, @LoaiChungTu, @NamLamViec, @NamNganSach, @NguonNganSach, @dvt, @iLoai, @iLoaiNNS ",
                    idDV, idCT, typeVoucher, namLamViecParam, namNganSachParm, nguonNganSachParm, dvt, loai, iloaiNNS).ToList();
            }
        }

        public IEnumerable<ReportTongHopPhanBoSoKiemTraTrinhKyQuery> FindReportPhanBoSoKiemTraDonViTrinhKy(string idDonVi, int loaiChungTu,
            int namLamViec, int namNganSach, int nguonNganSach, int donViTinh, string userName, int loaiNNS)
        {
            SktChungTuChiTietCriteria searchCondition = new SktChungTuChiTietCriteria();
            searchCondition.NamLamViec = namLamViec;
            searchCondition.NamNganSach = namNganSach;
            searchCondition.NguonNganSach = nguonNganSach;
            searchCondition.ITrangThai = StatusType.ACTIVE;
            searchCondition.IdDonVi = idDonVi;
            searchCondition.LoaiChungTu = loaiChungTu;
            searchCondition.UserName = userName;
            var sktChungTusSNC = FindChungTuSoNhuCauBaoCao(searchCondition);
            string lstIdChungTuSnc = string.Join(",", sktChungTusSNC.Select(x => x.Id.ToString()).ToList());
            if (string.IsNullOrEmpty(lstIdChungTuSnc))
            {
                lstIdChungTuSnc = Guid.Empty.ToString();
            }

            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_trinh_ky @idDV, @LoaiChungTu, @NamLamViec, @NamNganSach, @NguonNganSach, @ChungTuSnc, @dvt, @iLoaiNNS ";
                var parameters = new[]
                {
                    new SqlParameter("@idDV", idDonVi),
                    new SqlParameter("@LoaiChungTu", loaiChungTu),
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@NamNganSach", namNganSach),
                    new SqlParameter("@NguonNganSach", nguonNganSach),
                    new SqlParameter("@ChungTuSnc", lstIdChungTuSnc),
                    new SqlParameter("@dvt", donViTinh),
                    new SqlParameter("@iLoaiNNS", loaiNNS),
            };
                return ctx.FromSqlRaw<ReportTongHopPhanBoSoKiemTraTrinhKyQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<ReportTongHopPhanBoSoKiemTra> FindReportTongHopPhanBoSoKiemTra(string idDonVi, int loaiChungTu, int namLamViec, int namNganSach, int nguonNganSach, int donViTinh, int loaiNNS, bool typeClone = false)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter idDV = new SqlParameter("@idDV", idDonVi);
                SqlParameter typeVoucher = new SqlParameter("@LoaiChungTu", loaiChungTu);
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter namNganSachParm = new SqlParameter("@NamNganSach", namNganSach);
                SqlParameter nguonNganSachParm = new SqlParameter("@NguonNganSach", nguonNganSach);
                SqlParameter dvt = new SqlParameter("@dvt", donViTinh);
                SqlParameter iloaiNNS = new SqlParameter("@iLoaiNNS", loaiNNS);
                string sql = string.Empty;
                if (typeClone)
                {
                    sql = "EXECUTE dbo.sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_clone @idDV, @LoaiChungTu, @NamLamViec, @NamNganSach, @NguonNganSach, @dvt, @iLoaiNNS ";
                }
                else
                {
                    sql = "EXECUTE dbo.sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra @idDV, @LoaiChungTu, @NamLamViec, @NamNganSach, @NguonNganSach, @dvt, @iLoaiNNS ";

                }
                return ctx.FromSqlRaw<ReportTongHopPhanBoSoKiemTra>(sql,
                    idDV, typeVoucher, namLamViecParam, namNganSachParm, nguonNganSachParm, dvt, iloaiNNS).ToList();
            }
        }

        public IEnumerable<ReportTongHopPhanBoSoKiemTra> FindReportTongHopPhanBoSoKiemTraBVTC(string idDonVi, int loaiChungTu, int namLamViec, int namNganSach, int nguonNganSach, int donViTinh, int loaiNNS, bool typeClone = false)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter idDV = new SqlParameter("@idDV", idDonVi);
                SqlParameter typeVoucher = new SqlParameter("@LoaiChungTu", loaiChungTu);
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter namNganSachParm = new SqlParameter("@NamNganSach", namNganSach);
                SqlParameter nguonNganSachParm = new SqlParameter("@NguonNganSach", nguonNganSach);
                SqlParameter dvt = new SqlParameter("@dvt", donViTinh);
                SqlParameter iLoaiNNS = new SqlParameter("@iLoaiNNS", loaiNNS);
                string sql = string.Empty;
                if (typeClone)
                {
                    sql = "EXECUTE dbo.sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc_clone @idDV, @LoaiChungTu, @NamLamViec, @NamNganSach, @NguonNganSach, @dvt, @iLoaiNNS ";
                }
                else
                {
                    sql = "EXECUTE dbo.sp_rpt_skt_tong_hop_phan_bo_so_kiem_tra_bvtc @idDV, @LoaiChungTu, @NamLamViec, @NamNganSach, @NguonNganSach, @dvt, @iLoaiNNS ";

                }
                return ctx.FromSqlRaw<ReportTongHopPhanBoSoKiemTra>(sql,
                    idDV, typeVoucher, namLamViecParam, namNganSachParm, nguonNganSachParm, dvt, iLoaiNNS).ToList();
            }
        }

        public IEnumerable<ReportTongHopPhanBoSoKiemTra> FindReportTongHopSoKiemTraBenhVienTuChu(string idDonVi, int namLamViec, int loaiNNS)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter idDV = new SqlParameter("@lstDonVi", idDonVi);
                SqlParameter namLamViecParam = new SqlParameter("@iNamLamViec", namLamViec);
                SqlParameter iLoaiNNS = new SqlParameter("@iLoaiNNS", loaiNNS);
                return ctx.FromSqlRaw<ReportTongHopPhanBoSoKiemTra>("EXECUTE dbo.sp_rpt_skt_tonghop_skt_benhvien_tuchu @lstDonVi, @iNamLamViec, @iLoaiNNS",
                    idDV, namLamViecParam, iLoaiNNS).ToList();
            }
        }

        public IEnumerable<MucLucSoKiemTraTheoNganhQuery> FindMucLucSKTTheoNganh(string nganh, int isNganh, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter chuyenNganh = new SqlParameter("@Nganh", nganh);
                SqlParameter checkNganh = new SqlParameter("@IsNganh", isNganh);
                SqlParameter namLV = new SqlParameter("@NamLamViec", namLamViec);
                return ctx.FromSqlRaw<MucLucSoKiemTraTheoNganhQuery>("EXECUTE dbo.sp_rpt_skt_get_muc_luc_theo_nganh @Nganh , @IsNganh, @NamLamViec", chuyenNganh, checkNganh, namLV).ToList();
            }
        }

        public IEnumerable<ReportPhanBoKiemTraTheoNganhQuery> FindReportPhanBoKiemTraTheoNganh(string nganh, string idDonVi, int namLamViec, int namNganSach, int nguonNganSach, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_skt_phan_bo_kiem_tra_theo_nganh @Nganh , @MaDonVi, @NamLamViec, @NamNganSach, @NguonNganSach, @dvt";
                var parameters = new[]
                {
                    new SqlParameter("@Nganh", nganh),
                    new SqlParameter("@MaDonVi", idDonVi),
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@NamNganSach", namNganSach),
                    new SqlParameter("@NguonNganSach", nguonNganSach),
                    new SqlParameter("@dvt", donViTinh)
                };
                return ctx.FromSqlRaw<ReportPhanBoKiemTraTheoNganhQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<ReportChiTietPhanBoKiemTraNsbdQuery> FindReportChiTietPhanBoKiemTraNSBD(string idDonVi, int namLamViec, int namNganSach, int nguonNganSach, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_skt_chi_tiet_phan_bo_kiem_tra_nsbd @MaDonVi, @NamLamViec, @NamNganSach, @NguonNganSach, @dvt";
                var parameters = new[]
                {
                    new SqlParameter("@MaDonVi", idDonVi),
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@NamNganSach", namNganSach),
                    new SqlParameter("@NguonNganSach", nguonNganSach),
                    new SqlParameter("@dvt", donViTinh)
                };
                return ctx.FromSqlRaw<ReportChiTietPhanBoKiemTraNsbdQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<ReportPhanBoKiemTraTheoNganhPhuLucQuery> FindReportPhanBoKiemTraTheoNganhPhuLuc(string nganh, string idDonVi, int namLamViec, int namNganSach, int nguonNganSach, int loai, int donViTinh, bool bTongHop)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_skt_phan_bo_kiem_tra_theo_nganh_phu_luc_tong_hop @Nganh , @MaDonVi, @NamLamViec, @NamNganSach, @NguonNganSach, @Loai, @dvt, @bTongHop";
                var parameters = new[]
                {
                    new SqlParameter("@Nganh", nganh),
                    new SqlParameter("@MaDonVi", idDonVi),
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@NamNganSach", namNganSach),
                    new SqlParameter("@NguonNganSach", nguonNganSach),
                    new SqlParameter("@Loai", loai),
                    new SqlParameter("@dvt", donViTinh),
                    new SqlParameter("@bTongHop", bTongHop)
                };
                return ctx.FromSqlRaw<ReportPhanBoKiemTraTheoNganhPhuLucQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<ReportPhanBoKiemTraTheoNganhPhuLucQuery> FindReportSoNhuCauTheoNganhPhuLuc(string nganh, string idDonVi, string lstIdChungTu, int namLamViec, int namNganSach, int nguonNganSach, int loai, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_skt_so_nhu_cau_theo_nganh_phu_luc @Nganh , @MaDonVi, @IdChungTu, @NamLamViec, @NamNganSach, @NguonNganSach, @Loai, @dvt";
                var parameters = new[]
                {
                    new SqlParameter("@Nganh", nganh),
                    new SqlParameter("@MaDonVi", idDonVi),
                    new SqlParameter("@IdChungTu", lstIdChungTu),
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@NamNganSach", namNganSach),
                    new SqlParameter("@NguonNganSach", nguonNganSach),
                    new SqlParameter("@Loai", loai),
                    new SqlParameter("@dvt", donViTinh)
                };
                return ctx.FromSqlRaw<ReportPhanBoKiemTraTheoNganhPhuLucQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<ReportPhanBoKiemTraTheoNganhPhuLucQuery> FindReportNhanSoKiemTraTheoNganhPhuLuc(string nganh, string idDonVi, int namLamViec, int namNganSach, int nguonNganSach, int loai, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_skt_phan_bo_kiem_tra_theo_nganh_phu_luc @Nganh , @MaDonVi, @NamLamViec, @NamNganSach, @NguonNganSach, @Loai, @dvt";
                var parameters = new[]
                {
                    new SqlParameter("@Nganh", nganh),
                    new SqlParameter("@MaDonVi", idDonVi),
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@NamNganSach", namNganSach),
                    new SqlParameter("@NguonNganSach", nguonNganSach),
                    new SqlParameter("@Loai", loai),
                    new SqlParameter("@dvt", donViTinh)
                };
                return ctx.FromSqlRaw<ReportPhanBoKiemTraTheoNganhPhuLucQuery>(sql, parameters).ToList();
            }
        }

        public void AddAggregate(DemandVoucherDetailCriteria creation)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter listIdChungTuTongHop = new SqlParameter("@ListIdChungTuTongHop", creation.ListIdChungTuTongHop);
                SqlParameter idChungTu = new SqlParameter("@IdChungTu", creation.IdChungTu);
                SqlParameter idDonVi = new SqlParameter("@IdDonVi", creation.IdDonVi);
                SqlParameter tenDonVi = new SqlParameter("@TenDonVi", creation.TenDonVi);
                SqlParameter loaiChungTu = new SqlParameter("@LoaiChungTu", creation.LoaiChungTu);
                SqlParameter namLamViec = new SqlParameter("@NamLamViec", creation.NamLamViec);
                SqlParameter namNganSach = new SqlParameter("@NamNganSach", creation.NamNganSach);
                SqlParameter nguonNganSach = new SqlParameter("@NguonNganSach", creation.NguonNganSach);
                SqlParameter loaiNguonNganSach = new SqlParameter("@LoaiNguonNganSach", creation.LoaiNguonNganSach);
                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_skt_chungtu_chitiet_tao_tonghop @ListIdChungTuTongHop, @IdChungTu, @IdDonVi, @TenDonVi, @LoaiChungTu, @NamLamViec, @NamNganSach, @NguonNganSach, @LoaiNguonNganSach",
                    listIdChungTuTongHop, idChungTu, idDonVi, tenDonVi, loaiChungTu, namLamViec, namNganSach, nguonNganSach, loaiNguonNganSach);
            }
        }

        public IEnumerable<CanCuSoNhuCauQuery> FindCanCuSoNhuCau(string lstChungTu, string lstIdMucLuc, string idDonVi, string loaiCanCu, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter lstIdCT = new SqlParameter("@LstIdChungTu", lstChungTu);
                SqlParameter lstIdMucLucParam = new SqlParameter("@LstIdMucLuc", lstIdMucLuc);
                SqlParameter idDV = new SqlParameter("@IdDonVi", idDonVi);
                SqlParameter typeCanCu = new SqlParameter("@LoaiCanCu", loaiCanCu);
                SqlParameter namLV = new SqlParameter("@NamLamViec", namLamViec);
                return ctx.FromSqlRaw<CanCuSoNhuCauQuery>("EXECUTE dbo.sp_skt_get_can_cu_so_nhu_cau @LstIdChungTu, @LstIdMucLuc, @IdDonVi, @LoaiCanCu, @NamLamViec", lstIdCT, lstIdMucLucParam, idDV, typeCanCu, namLV).ToList();
            }
        }

        public IEnumerable<CanCuDuToanQtCpSoNhuCauQuery> FindCanCuDuToanSoNhuCau(string lstChungTu, string lstIdMucLuc, string idDonVi, string loaiCanCu, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter lstIdCT = new SqlParameter("@LstIdChungTu", lstChungTu);
                SqlParameter lstIdMucLucParam = new SqlParameter("@LstIdMucLuc", lstIdMucLuc);
                SqlParameter idDV = new SqlParameter("@IdDonVi", idDonVi);
                SqlParameter typeCanCu = new SqlParameter("@LoaiCanCu", loaiCanCu);
                SqlParameter namLV = new SqlParameter("@NamLamViec", namLamViec);
                var rs = ctx.FromSqlRaw<CanCuDuToanQtCpSoNhuCauQuery>("EXECUTE dbo.sp_skt_get_can_cu_du_toan_so_nhu_cau @LstIdChungTu, @LstIdMucLuc, @IdDonVi, @LoaiCanCu, @NamLamViec", lstIdCT, lstIdMucLucParam, idDV, typeCanCu, namLV).ToList();
                return rs;
            }
        }

        public IEnumerable<CanCuDuToanNamTruocSoKiemTraQuery> FindCanCuSoKiemTra(int loaiChungTu, string idDonVi, int namLamViec, int namNganSach, int nguonNganSach)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter loaiChungTuParam = new SqlParameter("@LoaiChungTu", loaiChungTu);
                SqlParameter idDV = new SqlParameter("@IdDonVi", idDonVi);
                SqlParameter namLV = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter namNganSachParam = new SqlParameter("@NamNganSach", namNganSach);
                SqlParameter nguonNganSachParam = new SqlParameter("@MaNguoiNganSach", nguonNganSach);
                return ctx.FromSqlRaw<CanCuDuToanNamTruocSoKiemTraQuery>("EXECUTE dbo.sp_skt_get_can_cu_so_kiem_tra @LoaiChungTu, @IdDonVi, @NamLamViec, @NamNganSach, @MaNguoiNganSach", loaiChungTuParam, idDV, namLV, namNganSachParam, nguonNganSachParam).ToList();
            }
        }

        public IEnumerable<CanCuDuToanNamTruocSoKiemTraQuery> FindCanCuPhanSoKiemTra(int loaiChungTu, string idDonVi, int namLamViec, int namNganSach, int nguonNganSach, bool isParent)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter loaiChungTuParam = new SqlParameter("@LoaiChungTu", loaiChungTu);
                SqlParameter idDV = new SqlParameter("@IdDonVi", idDonVi);
                SqlParameter namLV = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter namNganSachParam = new SqlParameter("@NamNganSach", namNganSach);
                SqlParameter nguonNganSachParam = new SqlParameter("@MaNguoiNganSach", nguonNganSach);
                SqlParameter isParentParam = new SqlParameter("@IsParent", isParent);
                return ctx.FromSqlRaw<CanCuDuToanNamTruocSoKiemTraQuery>("EXECUTE dbo.sp_skt_get_can_cu_phan_bo_so_kiem_tra @LoaiChungTu, @IdDonVi, @NamLamViec, @NamNganSach, @MaNguoiNganSach, @IsParent", loaiChungTuParam, idDV, namLV, namNganSachParam, nguonNganSachParam, isParentParam).ToList();
            }
        }

        public IEnumerable<NsSktChungTuChiTiet> FindChungTuChiTietSkt(int iLoai, int namLamViec, int namNganSach, int nguonNganSach, int loaiNguonNganSach, int loaiChungTu, string idDonVi, int bKhoa)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter loaiParam = new SqlParameter("@Loai", iLoai);
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter namNganSachParam = new SqlParameter("@NamNganSach", namNganSach);
                SqlParameter nguonNganSachParam = new SqlParameter("@NguonNganSach", nguonNganSach);
                SqlParameter loaiNguonNganSachParam = new SqlParameter("@LoaiNguonNganSach", loaiNguonNganSach);
                SqlParameter loaiChungTuParam = new SqlParameter("@LoaiChungTu", loaiChungTu);
                SqlParameter idDonViParam = new SqlParameter("@IdDonVi", idDonVi);
                SqlParameter bKhoaParam = new SqlParameter("@BKhoa", bKhoa);

                return ctx.NsSktChungTuChiTiets.FromSql("EXECUTE dbo.sp_skt_chung_tu_chi_tiet @Loai, @NamLamViec, @NamNganSach, @NguonNganSach, @LoaiNguonNganSach, @LoaiChungTu, @IdDonVi, @BKhoa",
                    loaiParam, namLamViecParam, namNganSachParam, nguonNganSachParam, loaiNguonNganSachParam, loaiChungTuParam, idDonViParam, bKhoaParam).ToList();
            }
        }

        public List<NsSktChungTuChiTiet> FindCanCuSoNhuCauPhanBoSKT(SktChungTuChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                int namLamViec = searchCondition.NamLamViec;
                int namNganSach = searchCondition.NamNganSach;
                int nguonNganSach = searchCondition.NguonNganSach;
                int iTrangThai = searchCondition.ITrangThai;
                string idDonVi = searchCondition.IdDonVi;
                int loaiChungTu = searchCondition.LoaiChungTu;
                int loaiNguonNganSach = searchCondition.ILoaiNguonNganSach;
                string userName = searchCondition.UserName;
                var donViQuanLy = ctx.NsDonVis.FirstOrDefault(x =>
                    x.NamLamViec == namLamViec && x.ITrangThai == iTrangThai && LoaiDonVi.ROOT.Equals(x.Loai));
                var donVi = ctx.NsDonVis.FirstOrDefault(x =>
                    x.NamLamViec == namLamViec && x.ITrangThai == iTrangThai && x.IIDMaDonVi.Equals(idDonVi));

                if (donVi == null) return new List<NsSktChungTuChiTiet>();

                var hasDonViQuanLy = ctx.NsNguoiDungDonVis.AsNoTracking().Count(x => x.IIDMaNguoiDung.Equals(userName)
                    && x.INamLamViec == namLamViec
                    && x.ITrangThai == StatusType.ACTIVE
                    && x.IIdMaDonVi.Equals(donViQuanLy.IIDMaDonVi)) > 0;

                var ctTongHop = ctx.NsSktChungTus.AsNoTracking()
                    .Where(x => x.INamLamViec == namLamViec
                                && x.INamNganSach == namNganSach
                                && x.IIdMaNguonNganSach == nguonNganSach
                                && x.ILoai == DemandCheckType.DEMAND
                                && x.IIdMaDonVi == donViQuanLy.IIDMaDonVi
                                && x.ILoaiChungTu == loaiChungTu
                                && x.ILoaiNguonNganSach == loaiNguonNganSach
                                && x.BKhoa);

                if (hasDonViQuanLy)
                {
                    if (ctTongHop.Any())
                    {
                        if (donVi != null && donVi.Loai.Equals(LoaiDonVi.ROOT))
                        {
                            var sktChungTuChiTietSNC = ctx.NsSktChungTuChiTiets.AsNoTracking()
                                .Where(x => x.INamLamViec == namLamViec
                                            && x.INamNganSach == namNganSach
                                            && x.IIdMaNguonNganSach == nguonNganSach
                                            && x.ILoai == DemandCheckType.DEMAND
                                            && x.IIdMaDonVi != donViQuanLy.IIDMaDonVi
                                            && x.ILoaiChungTu == loaiChungTu
                                            && x.ChungTu != null && !string.IsNullOrEmpty(x.ChungTu.SSoChungTu)
                                            && ctTongHop.Any(z => z.SDssoChungTuTongHop.Contains(x.ChungTu.SSoChungTu))).ToList();
                            return sktChungTuChiTietSNC;
                        }
                        else
                        {
                            var sktChungTuChiTietSNC = ctx.NsSktChungTuChiTiets.AsNoTracking()
                                .Where(x => x.INamLamViec == namLamViec
                                            && x.INamNganSach == namNganSach
                                            && x.IIdMaNguonNganSach == nguonNganSach
                                            && x.ILoai == DemandCheckType.DEMAND
                                            && x.IIdMaDonVi == donVi.IIDMaDonVi
                                            && x.ILoaiChungTu == loaiChungTu
                                            && ctTongHop.Any(z => z.SDssoChungTuTongHop.Contains(x.ChungTu.SSoChungTu))).ToList();
                            return sktChungTuChiTietSNC;
                        }
                    }
                }
                else
                {
                    var sktChungTuChiTietSNC = ctx.NsSktChungTuChiTiets.AsNoTracking()
                        .Where(x => x.INamLamViec == namLamViec
                                    && x.INamNganSach == namNganSach
                                    && x.IIdMaNguonNganSach == nguonNganSach
                                    && x.ILoai == DemandCheckType.DEMAND
                                    && x.IIdMaDonVi == donVi.IIDMaDonVi
                                    && x.ILoaiChungTu == loaiChungTu
                                    && x.ChungTu.BKhoa).ToList();
                    return sktChungTuChiTietSNC;
                }

                return new List<NsSktChungTuChiTiet>();
            }
        }

        public List<NsSktChungTu> FindChungTuSoNhuCauBaoCao(SktChungTuChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                int namLamViec = searchCondition.NamLamViec;
                int namNganSach = searchCondition.NamNganSach;
                int nguonNganSach = searchCondition.NguonNganSach;
                int iTrangThai = searchCondition.ITrangThai;
                int loaiChungTu = searchCondition.LoaiChungTu;
                string userName = searchCondition.UserName;
                var donViQuanLy = ctx.NsDonVis.FirstOrDefault(x =>
                    x.NamLamViec == namLamViec && x.ITrangThai == iTrangThai && LoaiDonVi.ROOT.Equals(x.Loai));
                var hasDonViQuanLy = ctx.NsNguoiDungDonVis.AsNoTracking().Count(x => x.IIDMaNguoiDung.Equals(userName)
                    && x.INamLamViec == namLamViec
                    && x.ITrangThai == StatusType.ACTIVE
                    && x.IIdMaDonVi.Equals(donViQuanLy.IIDMaDonVi)) > 0;

                var ctTongHop = ctx.NsSktChungTus.AsNoTracking()
                    .FirstOrDefault(x => x.INamLamViec == namLamViec
                                         && x.INamNganSach == namNganSach
                                         && x.IIdMaNguonNganSach == nguonNganSach
                                         && x.ILoai == DemandCheckType.DEMAND
                                         && x.IIdMaDonVi == donViQuanLy.IIDMaDonVi
                                         && x.ILoaiChungTu == loaiChungTu
                                         && x.BKhoa);
                if (hasDonViQuanLy)
                {
                    if (ctTongHop != null && !string.IsNullOrEmpty(ctTongHop.SDssoChungTuTongHop))
                    {
                        var sktChungTusSNC = ctx.NsSktChungTus.AsNoTracking()
                            .Where(x => x.INamLamViec == namLamViec
                                        && x.INamNganSach == namNganSach
                                        && x.IIdMaNguonNganSach == nguonNganSach
                                        && x.ILoai == DemandCheckType.DEMAND
                                        && x.ILoaiChungTu == loaiChungTu
                                        && ctTongHop.SDssoChungTuTongHop != null
                                        && ctTongHop.SDssoChungTuTongHop.Contains(x.SSoChungTu)).ToList();
                        return sktChungTusSNC;
                    }
                }
                else
                {
                    var sktChungTusSNC = ctx.NsSktChungTus.AsNoTracking()
                        .Where(x => x.INamLamViec == namLamViec
                                    && x.INamNganSach == namNganSach
                                    && x.IIdMaNguonNganSach == nguonNganSach
                                    && x.ILoai == DemandCheckType.DEMAND
                                    && x.ILoaiChungTu == loaiChungTu
                                    && x.BKhoa).ToList();
                    return sktChungTusSNC;
                }

                return new List<NsSktChungTu>();
            }
        }

        public void DeleteByVoucherId(Guid voucherId, int loai)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var voucherIdParam = new SqlParameter("@VoucherId", voucherId.ToString());
                var loaiChungTuParam = new SqlParameter("@LoaiChungTu", loai.ToString());
                ctx.Database.ExecuteSqlCommand($"DELETE FROM NS_SKT_ChungTuChiTiet WHERE iID_CTSoKiemTra = @VoucherId and iLoai = @LoaiChungTu", voucherIdParam, loaiChungTuParam);
            }
        }

        public IEnumerable<ReportTongHopSoNhuCauPhuLuc3Query> FindReportSoNhuCauTongHopPhuLuc3(int loaiChungTu, string maDonVi, int namLamViec, int namNganSach, int maNguonNganSach, string maBQuanLy, int dvt, int loaiNNS)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<ReportTongHopSoNhuCauPhuLuc3Query>("EXECUTE dbo.sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_3 @LoaiChungTu, @IdDonVi, @NamLamViec, @NamNganSach, @MaNguonNganSach, @MaBQuanLy, @dvt, @loaiNNS",
                    new SqlParameter("@LoaiChungTu", loaiChungTu),
                    new SqlParameter("@IdDonVi", maDonVi),
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@NamNganSach", namNganSach),
                    new SqlParameter("@MaNguonNganSach", maNguonNganSach),
                    new SqlParameter("@MaBQuanLy", maBQuanLy),
                    new SqlParameter("@dvt", dvt),
                    new SqlParameter("@loaiNNS", loaiNNS)
                ).ToList();
            }
        }

        public IEnumerable<ReportTongHopSoNhuCauPhuLuc4Query> FindReportSoNhuCauTongHopPhuLuc4(int loaiChungTu, string maDonVi, int namLamViec, int namNganSach, int maNguonNganSach, string maBQuanLy, int dvt, int loaiNNS)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<ReportTongHopSoNhuCauPhuLuc4Query>("EXECUTE dbo.sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_4 @LoaiChungTu, @IdDonVi, @NamLamViec, @NamNganSach, @MaNguonNganSach, @MaBQuanLy, @dvt, @loaiNNS",
                    new SqlParameter("@LoaiChungTu", loaiChungTu),
                    new SqlParameter("@IdDonVi", maDonVi),
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@NamNganSach", namNganSach),
                    new SqlParameter("@MaNguonNganSach", maNguonNganSach),
                    new SqlParameter("@MaBQuanLy", maBQuanLy),
                    new SqlParameter("@dvt", dvt),
                    new SqlParameter("@loaiNNS", loaiNNS)
                ).ToList();
            }
        }

        public IEnumerable<ReportTongHopSoNhuCauPhuLuc5Query> FindReportSoNhuCauTongHopPhuLuc5(int loaiChungTu, string maDonVi, int namLamViec, int namNganSach, int maNguonNganSach, string maBQuanLy, int dvt, int loaiNNS)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<ReportTongHopSoNhuCauPhuLuc5Query>("EXECUTE dbo.sp_rpt_skt_tong_hop_so_nhu_cau_phu_luc_5 @LoaiChungTu, @IdDonVi, @NamLamViec, @NamNganSach, @MaNguonNganSach, @MaBQuanLy, @dvt, @loaiNNS",
                    new SqlParameter("@LoaiChungTu", loaiChungTu),
                    new SqlParameter("@IdDonVi", maDonVi),
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@NamNganSach", namNganSach),
                    new SqlParameter("@MaNguonNganSach", maNguonNganSach),
                    new SqlParameter("@MaBQuanLy", maBQuanLy),
                    new SqlParameter("@dvt", dvt),
                    new SqlParameter("@loaiNNS", loaiNNS)
                ).ToList();
            }
        }
        public IEnumerable<ReportTongHopSoNhuCauPhuLuc6Query> FindReportSoNhuCauTongHopPhuLuc6(int loaiChungTu, string maDonVi, int namLamViec, int namNganSach, int maNguonNganSach, string maBQuanLy, int dvt, int loaiNNS)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var lstDonVi = maDonVi.Split(',');
                var lstChungTus = ctx.NsSktChungTus.Where(t => t.INamLamViec == namLamViec && t.INamNganSach == namNganSach && t.IIdMaNguonNganSach == maNguonNganSach && (loaiNNS == 0 || t.ILoaiNguonNganSach == loaiNNS) && t.ILoai == 99 && lstDonVi.Contains(t.IIdMaDonVi));
                if (lstDonVi.Length > 1)
                {
                    lstChungTus = lstChungTus.Where(t => (bool)t.BDaTongHop);
                }
                var lstChungtuChitiets = ctx.NsNc3YChungTuChiTiets.Where(t => lstChungTus.Any(x => x.Id == t.IIdCtsoKiemTra));
                var lstMuclucs = ctx.NsSktMucLucs.Where(t => t.INamLamViec == namLamViec).ToList();

                var lstCtCtsGroup = lstChungtuChitiets.GroupBy(t => t.SKyHieu)
                                            .Select(r => new
                                            {
                                                SKyHieu = r.Key,
                                                DuToan = r.Sum(rs => rs.FDuToan),
                                                UocTH = r.Sum(rs => rs.FUocTH),
                                                NCNam1 = r.Sum(rs => rs.FNCNam1),
                                                NCNam2 = r.Sum(rs => rs.FNCNam2),
                                                NCNam3 = r.Sum(rs => rs.FNCNam3),
                                            }).ToList();

                var lstCtCtsGhiChu = lstChungtuChitiets.GroupBy(t => t.SKyHieu)
                                            .Select(r => new
                                            {
                                                SKyHieu = r.Key,
                                                GhiChu = string.Join("; ", r.Select(t => t.SGhiChu)),
                                            }).ToList();

                var result = from mucluc in lstMuclucs
                             join lstCtCtGhiChu in lstCtCtsGhiChu on mucluc.SKyHieu equals lstCtCtGhiChu.SKyHieu
                                into gj
                             from temp in gj.DefaultIfEmpty()
                             join lstCtCt in lstCtCtsGroup on mucluc.SKyHieu equals lstCtCt.SKyHieu
                                into rss
                             from rs in rss.DefaultIfEmpty()
                             select new ReportTongHopSoNhuCauPhuLuc6Query
                             {
                                 Stt = mucluc.SSTT,
                                 iID_MLSKT = mucluc.IIDMLSKT,
                                 iID_MLSKTCha = mucluc.IIDMLSKTCha,
                                 SL = mucluc.SL,
                                 SK = mucluc.SK,
                                 SM = mucluc.SM,
                                 SNG = mucluc.SNg,
                                 SKyHieu = mucluc.SKyHieu,
                                 SSTTBC = mucluc.SSttBC,
                                 SMoTa = mucluc.SMoTa,
                                 BHangCha = mucluc.BHangCha,
                                 DuToan = rs == null ? 0 : Math.Round(rs.DuToan / dvt),
                                 UocThucHien = rs == null ? 0 : Math.Round(rs.UocTH / dvt),
                                 NhuCauNam1 = rs == null ? 0 : Math.Round(rs.NCNam1 / dvt),
                                 NhuCauNam2 = rs == null ? 0 : Math.Round(rs.NCNam2 / dvt),
                                 NhuCauNam3 = rs == null ? 0 : Math.Round(rs.NCNam3 / dvt),
                                 SGhiChu = rs == null ? "" : temp.GhiChu,
                             };
                return result.ToList();
            }
        }

        public IEnumerable<ReportTongHopSoNhuCauPhuLuc3Query> FindReportSoNhuCauChiTietPhuLuc3(int loaiChungTu, string maDonVi, int namLamViec, int namNganSach, int maNguonNganSach, int dvt, int loaiNNS)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var rs = ctx.FromSqlRaw<ReportTongHopSoNhuCauPhuLuc3Query>("EXECUTE dbo.sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_3 @LoaiChungTu, @IdDonVi, @NamLamViec, @NamNganSach, @MaNguonNganSach, @dvt, @LoaiNNS",
                    new SqlParameter("@LoaiChungTu", loaiChungTu),
                    new SqlParameter("@IdDonVi", maDonVi),
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@NamNganSach", namNganSach),
                    new SqlParameter("@MaNguonNganSach", maNguonNganSach),
                    new SqlParameter("@dvt", dvt),
                    new SqlParameter("@LoaiNNS", loaiNNS)
                ).ToList();
                return rs;
            }
        }

        public IEnumerable<ReportTongHopSoNhuCauPhuLuc4Query> FindReportSoNhuCauChiTietPhuLuc4(int loaiChungTu, string maDonVi, int namLamViec, int namNganSach, int maNguonNganSach, int dvt, int loaiNNS)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<ReportTongHopSoNhuCauPhuLuc4Query>("EXECUTE dbo.sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_4 @LoaiChungTu, @IdDonVi, @NamLamViec, @NamNganSach, @MaNguonNganSach, @dvt, @LoaiNNS",
                    new SqlParameter("@LoaiChungTu", loaiChungTu),
                    new SqlParameter("@IdDonVi", maDonVi),
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@NamNganSach", namNganSach),
                    new SqlParameter("@MaNguonNganSach", maNguonNganSach),
                    new SqlParameter("@dvt", dvt),
                    new SqlParameter("@LoaiNNS", loaiNNS)
                ).ToList();
            }
        }

        public IEnumerable<ReportTongHopSoNhuCauPhuLuc5Query> FindReportSoNhuCauChiTietPhuLuc5(int loaiChungTu, string maDonVi, int namLamViec, int namNganSach, int maNguonNganSach, int dvt, int loaiNNS)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<ReportTongHopSoNhuCauPhuLuc5Query>("EXECUTE dbo.sp_rpt_skt_chi_tiet_so_nhu_cau_phu_luc_5 @LoaiChungTu, @IdDonVi, @NamLamViec, @NamNganSach, @MaNguonNganSach, @dvt, @LoaiNNS",
                    new SqlParameter("@LoaiChungTu", loaiChungTu),
                    new SqlParameter("@IdDonVi", maDonVi),
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@NamNganSach", namNganSach),
                    new SqlParameter("@MaNguonNganSach", maNguonNganSach),
                    new SqlParameter("@dvt", dvt),
                    new SqlParameter("@LoaiNNS", loaiNNS)
                ).ToList();
            }
        }
        public IEnumerable<ReportTongHopSoNhuCauPhuLuc6Query> FindReportSoNhuCauChiTietPhuLuc6(int loaiChungTu, string maDonVi, int namLamViec, int namNganSach, int maNguonNganSach, int dvt, int loaiNNS)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var lstChungTus = ctx.NsSktChungTus.Where(t => t.INamLamViec == namLamViec && t.INamNganSach == namNganSach && t.IIdMaNguonNganSach == maNguonNganSach && (loaiNNS == 0 || t.ILoaiNguonNganSach == loaiNNS) && t.ILoai == 99 && maDonVi.Contains(t.IIdMaDonVi));
                var lstChungtuChitiets = ctx.NsNc3YChungTuChiTiets.Where(t => lstChungTus.Any(x => x.Id == t.IIdCtsoKiemTra));
                var lstMuclucs = ctx.NsSktMucLucs.Where(t => t.INamLamViec == namLamViec).ToList();

                var lstCtCtsGroup = lstChungtuChitiets.GroupBy(t => t.IIdMlskt)
                                            .Select(r => new
                                            {
                                                Id = r.Key,
                                                DuToan = r.Sum(rs => rs.FDuToan),
                                                UocTH = r.Sum(rs => rs.FUocTH),
                                                NCNam1 = r.Sum(rs => rs.FNCNam1),
                                                NCNam2 = r.Sum(rs => rs.FNCNam2),
                                                NCNam3 = r.Sum(rs => rs.FNCNam3),
                                            }).ToList();

                var lstCtCtsGhiChu = lstChungtuChitiets.GroupBy(t => t.IIdMlskt)
                                            .Select(r => new
                                            {
                                                Id = r.Key,
                                                GhiChu = string.Join("; ", r.Select(t => t.SGhiChu)),
                                            }).ToList();

                var result = from mucluc in lstMuclucs
                             join lstCtCtGhiChu in lstCtCtsGhiChu on mucluc.IIDMLSKT equals lstCtCtGhiChu.Id
                                into gj
                             from temp in gj.DefaultIfEmpty()
                             join lstCtCt in lstCtCtsGroup on mucluc.IIDMLSKT equals lstCtCt.Id
                                into rss
                             from rs in rss.DefaultIfEmpty()
                             select new ReportTongHopSoNhuCauPhuLuc6Query
                             {
                                 Stt = mucluc.SSTT,
                                 iID_MLSKT = mucluc.IIDMLSKT,
                                 iID_MLSKTCha = mucluc.IIDMLSKTCha,
                                 SL = mucluc.SL,
                                 SK = mucluc.SK,
                                 SM = mucluc.SM,
                                 SNG = mucluc.SNg,
                                 SKyHieu = mucluc.SKyHieu,
                                 SSTTBC = mucluc.SSttBC,
                                 SMoTa = mucluc.SMoTa,
                                 BHangCha = mucluc.BHangCha,
                                 DuToan = rs == null ? 0 : Math.Round(rs.DuToan / dvt),
                                 UocThucHien = rs == null ? 0 : Math.Round(rs.UocTH / dvt),
                                 NhuCauNam1 = rs == null ? 0 : Math.Round(rs.NCNam1 / dvt),
                                 NhuCauNam2 = rs == null ? 0 : Math.Round(rs.NCNam2 / dvt),
                                 NhuCauNam3 = rs == null ? 0 : Math.Round(rs.NCNam3 / dvt),
                                 SGhiChu = rs == null ? "" : temp.GhiChu,
                             };
                return result.ToList();
            }
        }
        bool ISktChungTuChiTietRepository.ExistChungTuChiTiet(Guid chungtuId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsSktChungTuChiTiets.Any(t => t.IIdCtsoKiemTra.Equals(chungtuId));
            }
        }

        public IEnumerable<ReportPhanBoKiemTraTheoNganhPhuLucQuery> PrintReportPhuongAnPhanBoSKT02A(string nganh, int namLamViec, int namNganSach, int nguonNganSach, int khoi, int donViTinh, bool bTongHop, int loaiNNS)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_skt_phuong_an_phan_bo_skt_02a @Nganh, @NamLamViec, @NamNganSach, @NguonNganSach, @Khoi, @DVT, @BTongHop, @loaiNNS";
                var parameters = new[]
                {
                    new SqlParameter("@Nganh", nganh),
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@NamNganSach", namNganSach),
                    new SqlParameter("@NguonNganSach", nguonNganSach),
                    new SqlParameter("@Khoi", khoi),
                    new SqlParameter("@DVT", donViTinh),
                    new SqlParameter("@BTongHop", bTongHop),
                    new SqlParameter("@loaiNNS", loaiNNS)
                };
                return ctx.FromSqlRaw<ReportPhanBoKiemTraTheoNganhPhuLucQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<ReportPhanBoKiemTraPhuongAnPhanBoQuery> PrintReportPhuongAnPhanBoSKT02B(string nganh, int namLamViec, int namNganSach, int nguonNganSach, int khoi, int donViTinh, bool bTongHop, int loaiNNS)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_skt_phuong_an_phan_bo_skt_02b @Nganh, @NamLamViec, @NamNganSach, @NguonNganSach, @Khoi, @DVT, @BTongHop, @loaiNNS";
                var parameters = new[]
                {
                    new SqlParameter("@Nganh", nganh),
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@NamNganSach", namNganSach),
                    new SqlParameter("@NguonNganSach", nguonNganSach),
                    new SqlParameter("@Khoi", khoi),
                    new SqlParameter("@DVT", donViTinh),
                    new SqlParameter("@BTongHop", bTongHop),
                    new SqlParameter("@loaiNNS", loaiNNS)
                };
                return ctx.FromSqlRaw<ReportPhanBoKiemTraPhuongAnPhanBoQuery>(sql, parameters).ToList();
            }
        }
    }
}