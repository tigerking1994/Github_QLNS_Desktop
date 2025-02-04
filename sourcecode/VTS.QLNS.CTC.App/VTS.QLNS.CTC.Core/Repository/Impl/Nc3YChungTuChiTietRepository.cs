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
    public class Nc3YChungTuChiTietRepository : Repository<NsNc3YChungTuChiTiet>, INc3YChungTuChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        private const string _idDonViDuPhong = "999";

        public Nc3YChungTuChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public NsNc3YChungTuChiTiet FindById(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsNc3YChungTuChiTiets.Find(id);
            }
        }
        public IEnumerable<NsNc3YChungTuChiTiet> FindByConditionForChildUnit(Nc3YChungTuChiTietCriteria searchCondition)
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

        public IEnumerable<NsNc3YChungTuChiTiet> FindByConditionForChildUnitDetail(Nc3YChungTuChiTietCriteria searchCondition)
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
                Func<NsNc3YChungTuChiTiet, bool> defaultNc3YChungTuChiTietFilter = x => x.INamLamViec == namLamViec && x.INamNganSach == namNganSach && x.IIdMaNguonNganSach == nguonNganSach && x.ILoai == iLoai;
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
                var nc3YChungTuChiTiets = ctx.NsNc3YChungTuChiTiets.AsNoTracking().AsEnumerable()
                    .Where(defaultNc3YChungTuChiTietFilter).Where(x => x.IIdCtsoKiemTra == sktChungTuId || lstIdChungTuChild.Contains(x.IIdCtsoKiemTra)).ToList();

                nc3YChungTuChiTiets = nc3YChungTuChiTiets.Count == 0 ? GetListTempDuLieuByLoaiChungTu(sktChungTuId, 0, loaiChungTu, namLamViec, idDonVi, iLoaiNguonNganSach, nguonNganSach, namNganSach) : nc3YChungTuChiTiets;

                nc3YChungTuChiTiets = nc3YChungTuChiTiets
                    .Where(x => x.FDuToan != 0 || x.FUocTH != 0 || x.FNCNam1 != 0 || x.FNCNam2 != 0 || x.FNCNam3 != 0 || !string.IsNullOrEmpty(x.SGhiChu)).ToList();
                var lstIdMuclucDisplay = new List<string>();
                if (hienThi.Equals(DataStateValue.DA_NHAP_SKT))
                {
                    lstIdMuclucDisplay = nc3YChungTuChiTiets.Select(x => x.SKyHieu).Distinct().ToList();
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
                             join nc3YChungTuChiTiet in nc3YChungTuChiTiets on new { SKyHieu = sktMucLuc.SKyHieu, sktMucLuc.IdDonVi } equals new
                             { SKyHieu = nc3YChungTuChiTiet.SKyHieu, IdDonVi = nc3YChungTuChiTiet.IIdMaDonVi } into gj
                             from sub in gj.DefaultIfEmpty()
                                 /*join cancu in CanCuData on new {chungtuId = ct == null ? Guid.Empty : ct.Id, iidmlskt = sktMucLuc.IIDMLSKT } equals new { chungtuId = cancu.IiIdCtsoKiemTra , iidmlskt = cancu.IIdMlskt} into cancuTbl
                                 from cc in cancuTbl.DefaultIfEmpty()*/
                             select new NsNc3YChungTuChiTiet
                             {
                                 Id = sub == null ? Guid.NewGuid() : sub.Id == Guid.Empty ? Guid.NewGuid() : sub.Id,
                                 Stt = sktMucLuc.SSTT,
                                 SMoTa = sktMucLuc.SMoTa,
                                 SKyHieu = sktMucLuc.SKyHieu,
                                 SKyHieuCu = sktMucLuc.SKyHieuCu,
                                 SL = sktMucLuc.SL,
                                 SK = sktMucLuc.SK,
                                 SM = sktMucLuc.SM,
                                 Nganh = sktMucLuc.SNg,
                                 FDuToan = sub == null ? 0 : sub.FDuToan,
                                 FUocTH = sub == null ? 0 : sub.FUocTH,
                                 FNCNam1 = sub == null ? 0 : sub.FNCNam1,
                                 FNCNam2 = sub == null ? 0 : sub.FNCNam2,
                                 FNCNam3 = sub == null ? 0 : sub.FNCNam3,
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
                             };

                return result.OrderBy(sktMucLuc => sktMucLuc.SKyHieu).ToList();
            }
        }

        private List<NsNc3YChungTuChiTiet> GetListTempDuLieuByLoaiChungTu(Guid sktChungTuId, int iLoai, int loaiChungTu, int namLamViec, string idDonVi, int iLoaiNguonNganSach, int nguonNganSach, int namNganSach)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var listChungTuCanCu = ctx.NsSktChungTus.AsNoTracking().AsEnumerable().Where(x => x.INamLamViec == namLamViec && x.ILoai == iLoai && x.ILoaiChungTu == loaiChungTu && x.IIdMaDonVi == idDonVi && x.IIdMaNguonNganSach == nguonNganSach && x.INamNganSach == namNganSach && x.ILoaiNguonNganSach == iLoaiNguonNganSach).ToList();
                var maCanCu = ctx.NsCauHinhCanCus.Where(x => x.INamLamViec == namLamViec && x.SModule == TypeModuleCanCu.DEMAND && x.IIDMaChucNang == TypeCanCu.ESTIMATE).First().Id;
                var chungTuChiTietCanCus = ctx.NsSktChungTuChiTietCanCus.AsNoTracking().AsEnumerable().Where(x => listChungTuCanCu.Any(l => l.Id == x.IiIdCtsoKiemTra) && x.IIdCanCu == maCanCu && x.FTuChi != 0);
                var chungTuChiTietNhuCaus = ctx.NsSktChungTuChiTiets.AsNoTracking().AsEnumerable().Where(x => listChungTuCanCu.Any(l => l.Id == x.IIdCtsoKiemTra) && x.FTuChi != 0);
                var sktMucLucs = ctx.NsSktMucLucs.AsNoTracking().AsEnumerable().Where(x => x.INamLamViec == namLamViec);

                var tempresult = from sktMucLuc in sktMucLucs
                                 join temp in chungTuChiTietCanCus on sktMucLuc.SKyHieu equals temp.SKyHieu
                                             into rs
                                 from sub in rs.DefaultIfEmpty()
                                 select new NsNc3YChungTuChiTiet
                                 {
                                     Id = Guid.NewGuid(),
                                     Stt = sktMucLuc.SSTT,
                                     SMoTa = sktMucLuc.SMoTa,
                                     SKyHieu = sktMucLuc.SKyHieu,
                                     SKyHieuCu = sktMucLuc.SKyHieuCu,
                                     SL = sktMucLuc.SL,
                                     SK = sktMucLuc.SK,
                                     SM = sktMucLuc.SM,
                                     Nganh = sktMucLuc.SNg,
                                     FDuToan = sub == null ? 0 : sub.FTuChi,
                                     FUocTH = sub == null ? 0 : sub.FTuChi,
                                     FNCNam1 = 0,
                                     FNCNam2 = 0,
                                     FNCNam3 = 0,
                                     INamLamViec = namLamViec,
                                     INamNganSach = namNganSach,
                                     IIdMaNguonNganSach = nguonNganSach,
                                     ILoaiChungTu = loaiChungTu,
                                     IIdCtsoKiemTra = sktChungTuId,
                                     IIdCtsoKiemTraChild = sktChungTuId,
                                     IsHangCha = sktMucLuc.BHangCha,
                                     IIdMaDonVi = idDonVi,
                                     IdParent = sktMucLuc.IIDMLSKTCha,
                                     STenDonVi = idDonVi,
                                     IIdMlskt = sktMucLuc.IIDMLSKT,
                                     IsAdd = sub == null,
                                     SGhiChu = "",
                                     ILoai = iLoai,
                                     DNgayTao = null,
                                     SNguoiTao = null,
                                     DNgaySua = null,
                                     SNguoiSua = null,
                                 };

                var result = from rs in tempresult
                             join temp in chungTuChiTietNhuCaus on rs.SKyHieu equals temp.SKyHieu
                                         into results
                             from sub in results.DefaultIfEmpty()
                             select new NsNc3YChungTuChiTiet
                             {
                                 Id = Guid.NewGuid(),
                                 Stt = rs.Stt,
                                 SMoTa = rs.SMoTa,
                                 SKyHieu = rs.SKyHieu,
                                 SKyHieuCu = rs.SKyHieuCu,
                                 SL = rs.SL,
                                 SK = rs.SK,
                                 SM = rs.SM,
                                 Nganh = rs.Nganh,
                                 FDuToan = rs.FDuToan,
                                 FUocTH = rs.FUocTH,
                                 FNCNam1 = sub == null ? 0 : sub.FTuChi,
                                 FNCNam2 = sub == null ? 0 : sub.FTuChi,
                                 FNCNam3 = sub == null ? 0 : sub.FTuChi,
                                 INamLamViec = namLamViec,
                                 INamNganSach = namNganSach,
                                 IIdMaNguonNganSach = nguonNganSach,
                                 ILoaiChungTu = loaiChungTu,
                                 IIdCtsoKiemTra = sktChungTuId,
                                 IIdCtsoKiemTraChild = sktChungTuId,
                                 IsHangCha = rs.IsHangCha,
                                 IIdMaDonVi = idDonVi,
                                 IdParent = rs.IdParent,
                                 STenDonVi = idDonVi,
                                 IIdMlskt = rs.IIdMlskt,
                                 IsAdd = sub == null,
                                 SGhiChu = "",
                                 ILoai = iLoai,
                                 DNgayTao = null,
                                 SNguoiTao = null,
                                 DNgaySua = null,
                                 SNguoiSua = null,
                             };

                return result.Where(t => t.FDuToan + t.FUocTH + t.FNCNam1 + t.FNCNam2 + t.FNCNam3 != 0).ToList();
            }
        }

        public IEnumerable<NsNc3YChungTuChiTiet> FindByConditionForChildUnitSummary(Nc3YChungTuChiTietCriteria searchCondition)
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
                Func<NsNc3YChungTuChiTiet, bool> defaultNc3YChungTuChiTietFilter = x => x.INamLamViec == namLamViec && x.INamNganSach == namNganSach && x.IIdMaNguonNganSach == nguonNganSach && x.ILoai == iLoai;
                Func<DonVi, bool> defaultNsDonViFilter = x => x.NamLamViec == namLamViec && x.ITrangThai == iTrangThai;
                Func<NsSktMucLuc, bool> defaultSktMucLucFilter = x => false;
                var nsDonVi = ctx.NsDonVis.Where(defaultNsDonViFilter)
                    .FirstOrDefault(x => x.IIDMaDonVi == idDonVi && x.NamLamViec == namLamViec);

                var nc3YChungTuChiTiets = ctx.NsNc3YChungTuChiTiets.AsNoTracking().AsEnumerable()
                    .Where(defaultNc3YChungTuChiTietFilter).Where(x => x.IIdMaDonVi == idDonVi && x.IIdCtsoKiemTra == sktChungTuId).ToList();

                nc3YChungTuChiTiets = nc3YChungTuChiTiets.Count == 0 ? GetListTempDuLieuByLoaiChungTu(sktChungTuId, 0, loaiChungTu, namLamViec, idDonVi, iLoaiNguonNganSach, nguonNganSach, namNganSach) : nc3YChungTuChiTiets;

                nc3YChungTuChiTiets = nc3YChungTuChiTiets
                    .Where(x => x.FDuToan != 0 || x.FUocTH != 0 || x.FNCNam1 != 0 || x.FNCNam2 != 0 || x.FNCNam3 != 0 || !string.IsNullOrEmpty(x.SGhiChu)).ToList();
                var lstIdMuclucDisplay = new List<string>();
                if (hienThi.Equals(DataStateValue.DA_NHAP_SKT))
                {
                    lstIdMuclucDisplay = nc3YChungTuChiTiets.Select(x => x.SKyHieu).Distinct().ToList();
                }
                var sktMucLucs = GetListMucLucSktByLoaiChungTu(iLoai, loaiChungTu, namLamViec, idDonVi, lstIdMuclucDisplay, hienThi, null);

                var result = from sktMucLuc in sktMucLucs
                             join nc3YChungTuChiTiet in nc3YChungTuChiTiets on sktMucLuc.SKyHieu equals nc3YChungTuChiTiet.SKyHieu
                                         into gj
                             from sub in gj.DefaultIfEmpty()
                             orderby sktMucLuc.SKyHieu
                             select new NsNc3YChungTuChiTiet
                             {
                                 Id = sub == null ? Guid.NewGuid() : sub.Id == Guid.Empty ? Guid.NewGuid() : sub.Id,
                                 Stt = sktMucLuc.SSTT,
                                 SMoTa = sktMucLuc.SMoTa,
                                 SKyHieu = sktMucLuc.SKyHieu,
                                 SKyHieuCu = sktMucLuc.SKyHieuCu,
                                 SL = sktMucLuc.SL,
                                 SK = sktMucLuc.SK,
                                 SM = sktMucLuc.SM,
                                 Nganh = sktMucLuc.SNg,
                                 FDuToan = sub == null ? 0 : sub.FDuToan,
                                 FUocTH = sub == null ? 0 : sub.FUocTH,
                                 FNCNam1 = sub == null ? 0 : sub.FNCNam1,
                                 FNCNam2 = sub == null ? 0 : sub.FNCNam2,
                                 FNCNam3 = sub == null ? 0 : sub.FNCNam3,
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
                             };
                var resultGroup = from p in result
                                  group p by new { p.SKyHieu, p.SKyHieuCu, p.IIdMaDonVi, p.SMoTa, p.STenDonVi } into g
                                  select new NsNc3YChungTuChiTiet
                                  {
                                      Id = g.First().Id,
                                      Stt = g.First().Stt,
                                      SMoTa = g.First().SMoTa,
                                      SKyHieu = g.First().SKyHieu,
                                      SKyHieuCu = g.First().SKyHieuCu,
                                      SL = g.First().SL,
                                      SK = g.First().SK,
                                      SM = g.First().SM,
                                      Nganh = g.First().Nganh,
                                      FDuToan = g.First().FDuToan,
                                      FUocTH = g.First().FUocTH,
                                      FNCNam1 = g.First().FNCNam1,
                                      FNCNam2 = g.First().FNCNam2,
                                      FNCNam3 = g.First().FNCNam3,
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
                                  };
                return resultGroup;
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
                sktMucLucs = sktMucLucs.GroupBy(x => x.IIDMLSKT).Select(x => x.First()).OrderBy(x => x.SKyHieu).ToList();
                return sktMucLucs;
            }
        }

        public bool ExistChungTuChiTiet(Guid chungtuId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsNc3YChungTuChiTiets.Any(t => t.IIdCtsoKiemTra.Equals(chungtuId));
            }
        }
        public IEnumerable<NsNc3YChungTuChiTiet> FindByCondition(Expression<Func<NsNc3YChungTuChiTiet, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsNc3YChungTuChiTiets.Where(predicate).ToList();
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
                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_nc3y_chungtu_chitiet_tao_tonghop @ListIdChungTuTongHop, @IdChungTu, @IdDonVi, @TenDonVi, @LoaiChungTu, @NamLamViec, @NamNganSach, @NguonNganSach, @LoaiNguonNganSach",
                    listIdChungTuTongHop, idChungTu, idDonVi, tenDonVi, loaiChungTu, namLamViec, namNganSach, nguonNganSach, loaiNguonNganSach);
            }
        }
    }
}