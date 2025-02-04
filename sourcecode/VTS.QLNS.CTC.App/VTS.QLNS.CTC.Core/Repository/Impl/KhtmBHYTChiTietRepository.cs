using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class KhtmBHYTChiTietRepository : Repository<BhKhtmBHYTChiTiet>, IKhtmBHYTChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public KhtmBHYTChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhKhtmBHYTChiTiet> FindBhKhtmBHYTChiTietByCondition(KhtmBHYTChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                int? namLamViec = searchCondition.NamLamViec;
                var heSoLCS = GetThamSoLCS(namLamViec);
                var heSoBHYT = GetThamSoBHYT(namLamViec);


                string idDonVi = searchCondition.IdDonVi;
                var idChungTuDonVi = GetIdChungTuByDonVi(searchCondition.MaDonVi, namLamViec);
                Guid khtmBHYTId = searchCondition.IsPrintReport ? idChungTuDonVi : searchCondition.khtmBhytId;
                var lstMLNS = GetListBhMucLucNs(namLamViec);
                var khtmBhytChiTiet = FindKhtmBHYTChiTiet(khtmBHYTId, namLamViec, searchCondition.MaDonVi).ToList();

                var result = from bhMucLuc in lstMLNS
                             join khtBhxhChiTiet in khtmBhytChiTiet on bhMucLuc.IIDMLNS equals khtBhxhChiTiet.IIDNoiDung
                                  into gj
                             from sub in gj.DefaultIfEmpty()
                             orderby bhMucLuc.SXauNoiMa
                             select new BhKhtmBHYTChiTiet
                             {
                                 Id = sub == null ? Guid.NewGuid() : sub.Id == Guid.Empty ? Guid.NewGuid() : sub.Id,
                                 IdKhtmBHYT = khtmBHYTId,
                                 IIDNoiDung = bhMucLuc.IIDMLNS,
                                 STenNoiDung = bhMucLuc.SMoTa,
                                 SXauNoiMa = bhMucLuc.SXauNoiMa,
                                 SLNS = bhMucLuc.SLNS,
                                 SL = bhMucLuc.SL,
                                 SK = bhMucLuc.SK,
                                 SM = bhMucLuc.SM,
                                 STM = bhMucLuc.STM,
                                 STTM = bhMucLuc.STTM,
                                 SNG = bhMucLuc.SNG,
                                 STNG = bhMucLuc.STNG,
                                 IsAdd = sub == null,
                                 DNgayTao = null,
                                 SNguoiTao = null,
                                 DNgaySua = null,
                                 SNguoiSua = null,
                                 IsHangCha = bhMucLuc.BHangCha,
                                 IdParent = bhMucLuc.IIDMLNSCha,
                                 ISoNguoi = sub?.ISoNguoi ?? 0,
                                 ISoThang = sub?.ISoThang ?? 0,
                                 FDinhMuc = sub?.FDinhMuc ?? 0,
                                 FThanhTien = sub?.FThanhTien ?? 0,
                                 IIDMaDonVi = sub?.IIDMaDonVi ?? null,
                                 STenDonVi = sub?.STenDonVi ?? null,
                                 SGhiChu = sub?.SGhiChu ?? null,
                                 DHeSoLCS = (decimal)(heSoLCS?.fGiaTri ?? 0),
                                 DHeSoBHYT = (decimal)(heSoBHYT?.fGiaTri ?? 0),
                                 INamLamViec = searchCondition.NamLamViec
                             };

                return result;
            }
        }

        public IEnumerable<BhKhtmBHYTChiTiet> FindBhKhtmBHYTReportByCondition(KhtmBHYTChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                int? namLamViec = searchCondition.NamLamViec;
                var heSoLCS = GetThamSoLCS(namLamViec);
                var heSoBHYT = GetThamSoBHYT(namLamViec);

                var lstMLNS = GetListBhMucLucNs(namLamViec);
                var khtmBhytChiTiet = FindKhtmBHYTChiTietTongHop(namLamViec, searchCondition.MaDonVi).ToList();

                var result = from bhMucLuc in lstMLNS
                             join khtBhxhChiTiet in khtmBhytChiTiet on bhMucLuc.IIDMLNS equals khtBhxhChiTiet.IIDNoiDung
                                  into gj
                             from sub in gj.DefaultIfEmpty()
                             orderby bhMucLuc.SXauNoiMa
                             select new BhKhtmBHYTChiTiet
                             {
                                 Id = sub == null ? Guid.NewGuid() : sub.Id == Guid.Empty ? Guid.NewGuid() : sub.Id,
                                 IdKhtmBHYT = Guid.NewGuid(),
                                 IIDNoiDung = bhMucLuc.IIDMLNS,
                                 STenNoiDung = bhMucLuc.SMoTa,
                                 SXauNoiMa = bhMucLuc.SXauNoiMa,
                                 SLNS = bhMucLuc.SLNS,
                                 SL = bhMucLuc.SL,
                                 SK = bhMucLuc.SK,
                                 SM = bhMucLuc.SM,
                                 STM = bhMucLuc.STM,
                                 STTM = bhMucLuc.STTM,
                                 SNG = bhMucLuc.SNG,
                                 STNG = bhMucLuc.STNG,
                                 IsAdd = sub == null,
                                 DNgayTao = null,
                                 SNguoiTao = null,
                                 DNgaySua = null,
                                 SNguoiSua = null,
                                 IsHangCha = bhMucLuc.BHangCha,
                                 IdParent = bhMucLuc.IIDMLNSCha,
                                 ISoNguoi = sub?.ISoNguoi ?? 0,
                                 ISoThang = sub?.ISoThang ?? 0,
                                 FDinhMuc = sub?.FDinhMuc ?? 0,
                                 FThanhTien = sub?.FThanhTien ?? 0,
                                 IIDMaDonVi = sub?.IIDMaDonVi ?? null,
                                 STenDonVi = sub?.STenDonVi ?? null,
                                 SGhiChu = sub?.SGhiChu ?? null,
                                 DHeSoLCS = (decimal)(heSoLCS?.fGiaTri ?? 0),
                                 DHeSoBHYT = (decimal)(heSoBHYT?.fGiaTri ?? 0),
                                 INamLamViec = searchCondition.NamLamViec
                             };

                return result;
            }
        }

        public BhDmCauHinhThamSo GetThamSoLCS(int? namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var thamSoLCS = ctx.BhDmCauHinhThamSo.FirstOrDefault(x => x.INamLamViec == namLamViec && x.SMa == BHYTCauHinhThamSo.LCS && x.BTrangThai);
                return thamSoLCS;
            }
        }

        public BhDmCauHinhThamSo GetThamSoBHYT(int? namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var thamSoLCS = ctx.BhDmCauHinhThamSo.FirstOrDefault(x => x.INamLamViec == namLamViec && x.SMa == BHYTCauHinhThamSo.HESO_BHYT && x.BTrangThai);
                return thamSoLCS;
            }
        }

        public TlDmPhuCap GetHeSoLCS(string heSoLCS)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var phuCapLCS = ctx.TlDmPhuCaps.FirstOrDefault(x => x.MaPhuCap == heSoLCS);
                return phuCapLCS;
            }
        }
        public List<BhDmMucLucNganSach> GetListBhMucLucNs(int? namLamViec)
        {

            using (var ctx = _contextFactory.CreateDbContext())
            {
                var NsMucLucsChild = ctx.BhDmMucLucNganSachs.AsNoTracking().AsEnumerable().Where(x => x.INamLamViec == namLamViec && x.SLNS.StartsWith(BhxhMLNS.THU_MUA_BHYT) && x.ITrangThai == StatusType.ACTIVE).ToList();
                var nsMucLucs = new List<BhDmMucLucNganSach>();
                if (NsMucLucsChild.Count > 0)
                {
                    var listIdMlKht = NsMucLucsChild.Select(item => item.IIDMLNS).ToList();
                    nsMucLucs = NsMucLucsChild;
                    while (true)
                    {
                        var listIdParent = NsMucLucsChild.Where(x => !listIdMlKht.Contains(x.IIDMLNSCha.GetValueOrDefault())).Select(x => x.IIDMLNSCha).ToList();
                        var listParent1 = ctx.BhDmMucLucNganSachs.Where(x => listIdParent.Contains(x.IIDMLNS) && x.INamLamViec == namLamViec).ToList();
                        if (listParent1.Count > 0)
                        {
                            var lstId = listParent1.Select(item => item.IIDMLNS).ToList();
                            listIdMlKht.AddRange(lstId);
                            nsMucLucs.AddRange(listParent1);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                nsMucLucs = nsMucLucs.GroupBy(x => x.IIDMLNS).Select(x => x.First()).OrderBy(x => x.SXauNoiMa).ToList();
                nsMucLucs.RemoveRange(0, 2);
                return nsMucLucs;
            }
        }

        public IEnumerable<BhKhtmBHYTChiTiet> FindKhtmBHYTChiTiet(Guid khtmBHYTId, int? namLamViec, string maDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter khtmBHYTIdParam = new SqlParameter("@KhtmBHYTId", khtmBHYTId);
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter maDonViParam = new SqlParameter("@MaDonVi", maDonVi);
                return ctx.BhKhtmBHYTChiTiets.FromSql("EXECUTE dbo.sp_khtm_bhyt_chi_tiet @khtmBHYTId, @NamLamViec, @MaDonVi",
                    khtmBHYTIdParam, namLamViecParam, maDonViParam).ToList();
            }
        }

        public IEnumerable<BhKhtmBHYTChiTiet> FindKhtmBHYTChiTietTongHop(int? namLamViec, string maDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter maDonViParam = new SqlParameter("@MaDonVi", maDonVi);
                return ctx.BhKhtmBHYTChiTiets.FromSql("EXECUTE dbo.sp_khtm_bhyt_chi_tiet_tonghop_donvi @NamLamViec, @MaDonVi",
                    namLamViecParam, maDonViParam).ToList();
            }
        }

        public BhKhtmBHYTChiTiet FindById(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhKhtmBHYTChiTiets.Find(id);
            }
        }

        public IEnumerable<BhKhtmBHYTChiTiet> FindKhtmBHYTChiTietByIdBhyt(KhtmBHYTChiTietCriteria searchCondition)
        {
            Guid khtmBHYTId = searchCondition.khtmBhytId;
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhKhtmBHYTChiTiets.Where(x => x.IdKhtmBHYT == khtmBHYTId).ToList();
            }
        }

        public bool ExistBHYTChiTiet(Guid bhytId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhKhtmBHYTChiTiets.Any(t => t.IdKhtmBHYT.Equals(bhytId));
            }
        }

        public IEnumerable<ReportKhtmDuToanBHYTQuery> FindKhtmDuToanThuBhytThanNhan(int namLamViec, int bloaiChungTu, bool bDaTongHop, string lstDonvi, string thanNhanQuanNhan, string thanNhanCNVQP, string smDuToan, string smHachToan, int dvt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_khtm_du_toan_thu_bhyt_than_nhan @namLamViec, @loaiChungTu, @daTongHop, @lstSelectedUnit, @thanNhanQuanNhan, @thanNhanCNVQP, @smDuToan, @smHachToan, @dvt";
                var parameters = new[]
                {
                    new SqlParameter("@namLamViec", namLamViec),
                    new SqlParameter("@loaiChungTu", bloaiChungTu),
                    new SqlParameter("@daTongHop", bDaTongHop),
                    new SqlParameter("@lstSelectedUnit", lstDonvi),
                    new SqlParameter("@thanNhanQuanNhan", thanNhanQuanNhan),
                    new SqlParameter("@thanNhanCNVQP", thanNhanCNVQP),
                    new SqlParameter("@smDuToan", smDuToan),
                    new SqlParameter("@smHachToan", smHachToan),
                    new SqlParameter("@dvt", dvt)
                };
                return ctx.FromSqlRaw<ReportKhtmDuToanBHYTQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<ReportKhtmDuToanBHYTQuery> FindKhtmDuToanThuBhytHSSV(int namLamViec, int bloaiChungTu, bool bDaTongHop, string lstDonvi, string hSSV, string luuHS, string hVSQ, string sQDuBi, int dvt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_khtm_du_toan_thu_bhyt_hssv_hvqs @namLamViec, @loaiChungTu, @daTongHop, @lstSelectedUnit, @hSSV, @luuHS, @hVSQ, @sQDuBi, @dvt";
                var parameters = new[]
                {
                    new SqlParameter("@namLamViec", namLamViec),
                    new SqlParameter("@loaiChungTu", bloaiChungTu),
                    new SqlParameter("@daTongHop", bDaTongHop),
                    new SqlParameter("@lstSelectedUnit", lstDonvi),
                    new SqlParameter("@hSSV", hSSV),
                    new SqlParameter("@luuHS", luuHS),
                    new SqlParameter("@hVSQ", hVSQ),
                    new SqlParameter("@sQDuBi", sQDuBi),
                    new SqlParameter("@dvt", dvt)
                };
                return ctx.FromSqlRaw<ReportKhtmDuToanBHYTQuery>(sql, parameters).ToList();
            }
        }

        public Guid GetIdChungTuByDonVi(string maDonVi, int? namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhKhtmBHYTs.Where(x => x.INamLamViec == namLamViec && x.IIDMaDonVi == maDonVi).Select(a => a.Id).FirstOrDefault();
            }
        }

        public IEnumerable<BhKhtmBHYTChiTietQuery> GetAggregatePlanData(int iNam, string sMaDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_get_data_khtm_tong_hop @NamLamViec, @MaDonVi";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", iNam),
                    new SqlParameter("@MaDonVi", sMaDonVi)
                };
                return ctx.FromSqlRaw<BhKhtmBHYTChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<BhKhtmBHYTChiTietQuery> GetPlanData(int iNam, string sSoChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_get_data_khtm @NamLamViec, @SoChungTu";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", iNam),
                    new SqlParameter("@SoChungTu", sSoChungTu)
                };
                return ctx.FromSqlRaw<BhKhtmBHYTChiTietQuery>(sql, parameters).ToList();
            }
        }

        public List<BhKhtmBHYTChiTietQuery> FindBhKhtmBHYTTongHopChiTietByCondition(KhtmBHYTChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_kht_bhxh_tong_hop_chi_tiet_donvi @NamLamViec, @MaDonVi,@LoaiChungTu,@DVT";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", searchCondition.NamLamViec),
                    new SqlParameter("@MaDonVi", searchCondition.MaDonVi),
                    new SqlParameter("@LoaiChungTu", searchCondition.LoaiChungTu),
                    new SqlParameter("@DVT", searchCondition.DonViTinh)
                };
                return ctx.FromSqlRaw<BhKhtmBHYTChiTietQuery>(sql, parameters).ToList();
            }
        }
    }
}
