using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class BhKhcKChiTietRepository : Repository<BhKhcKChiTiet>, IBhKhcKChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public BhKhcKChiTietRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void AddAggregate(KhcKChiTietCriteria creation)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter listIdChungTuTongHop = new SqlParameter("@ListIdChungTuTongHop", creation.ListIdChungTuTongHop);
                SqlParameter iDMaDonVi = new SqlParameter("@IDMaDonVi", creation.IdDonVi);
                SqlParameter nguoiTao = new SqlParameter("@Nguoitao", creation.NguoiTao);
                SqlParameter idChungTu = new SqlParameter("@IdChungTu", creation.IdChungTu);
                SqlParameter namLamViec = new SqlParameter("@NamLamViec", creation.NamLamViec);

                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_khc_khac_chungtu_chitiet_tao_tonghop @ListIdChungTuTongHop,@IDMaDonVi,@Nguoitao ,@IdChungTu, @NamLamViec",
                    listIdChungTuTongHop, iDMaDonVi, nguoiTao, idChungTu, namLamViec);
            }
        }

        public bool ExistKhcKcbChiTiet(Guid bhxhId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhDtcDcdToanChiChiTiets.Any(t => t.IID_BH_DTC.Equals(bhxhId));
            }
        }

        public IEnumerable<BhKhcKChiTiet> FindByConditionForChildUnit(KhcKChiTietCriteria searchCondition)
        {
            return FindByConditionForChildUnitSummary(searchCondition);
        }

        private IEnumerable<BhKhcKChiTiet> FindByConditionForChildUnitSummary(KhcKChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                int namLamViec = searchCondition.NamLamViec;
                Guid khcKcbId = searchCondition.KhcKcbId;
                string sLoaiTroCap = searchCondition.sLoaiTroCap;
                int iTrangThai = searchCondition.ITrangThai;
                string idDonVi = searchCondition.IdDonVi;
                string SLNS = searchCondition.SLNS;
                List<BhDmMucLucNganSach> NsMucLucs;
                Func<DonVi, bool> defaultNsDonViFilter = x => x.NamLamViec == namLamViec;

                var nsDonVi = ctx.NsDonVis.Where(defaultNsDonViFilter)
                    .FirstOrDefault(x => x.IIDMaDonVi == idDonVi && x.NamLamViec == namLamViec);

                NsMucLucs = GetListMucLucBhxhByLoaiChungtu(namLamViec, idDonVi, SLNS);

                List<BhKhcKChiTiet> lstBhxhChungTuChiTiets = new List<BhKhcKChiTiet>();

                lstBhxhChungTuChiTiets = FindKhcChedoBhxhChiTiet(namLamViec, idDonVi, khcKcbId).ToList();

                var result = from nsMucLuc in NsMucLucs
                             join khcKhacChungTuChiTiet in lstBhxhChungTuChiTiets on nsMucLuc.IIDMLNS equals khcKhacChungTuChiTiet.IID_MucLucNganSach
                                         into gj
                             from sub in gj.DefaultIfEmpty()
                             orderby nsMucLuc.SXauNoiMa

                             select new BhKhcKChiTiet
                             {
                                 Id = sub == null ? Guid.NewGuid() : sub.Id == Guid.Empty ? Guid.NewGuid() : sub.Id,
                                 IID_KHC_K = khcKcbId,
                                 SNoiDung = nsMucLuc.SMoTa,
                                 IIdMaDonVi = nsDonVi?.IIDMaDonVi ?? string.Empty,
                                 IdParent = nsMucLuc.IIDMLNSCha,
                                 STenDonVi = nsDonVi?.TenDonVi ?? string.Empty,
                                 IID_MucLucNganSach = nsMucLuc.IIDMLNS,
                                 IsHangCha = nsMucLuc.BHangCha,
                                 IsAdd = sub == null,
                                 DNgayTao = null,
                                 SNguoiTao = null,
                                 DNgaySua = null,
                                 SNguoiSua = null,
                                 FTienDaThucHienNamTruoc = sub?.FTienDaThucHienNamTruoc ?? 0,
                                 FTienUocThucHienNamTruoc = sub?.FTienUocThucHienNamTruoc ?? 0,
                                 FTienKeHoachThucHienNamNay = sub?.FTienKeHoachThucHienNamNay ?? 0,
                                 SGhiChu = sub?.SGhiChu ?? string.Empty,
                                 SXauNoiMa = nsMucLuc.SXauNoiMa,
                                 STTM = nsMucLuc.STTM,
                             };
                return result;
            }
        }

        private IEnumerable<BhKhcKChiTiet> FindKhcChedoBhxhChiTiet(int namLamViec, string idDonVi, Guid khcKId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter idDonViParam = new SqlParameter("@IdDonVi", idDonVi);
                SqlParameter idKhcKinhphiQuanlyParam = new SqlParameter("@iID_BH_KHC_K", khcKId);

                return ctx.Set<BhKhcKChiTiet>().FromSql("EXECUTE dbo.sp_khc_k_chi_tiet @NamLamViec, @IdDonVi,@iID_BH_KHC_K",
                    namLamViecParam, idDonViParam, idKhcKinhphiQuanlyParam).ToList();
            }
        }

        private List<BhDmMucLucNganSach> GetListMucLucBhxhByLoaiChungtu(int namLamViec, string idDonVi, string sLNS)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var lstLSN = sLNS.Split(',');
                var loaiChungTuNSSD = int.Parse(VoucherType.NSSD_Key);
                var loaiChungTuNSBD = int.Parse(VoucherType.NSBD_Key);
                var iTrangThai = StatusType.ACTIVE;
                Func<BhDmMucLucNganSach, bool> defaultSktMucLucFilter = x => x.INamLamViec == namLamViec;
                var nsDonVi = ctx.NsDonVis.Where(x => x.IIDMaDonVi == idDonVi && x.NamLamViec == namLamViec && x.ITrangThai == iTrangThai).FirstOrDefault();
                if (nsDonVi == null) return new List<BhDmMucLucNganSach>();
                List<BhDmMucLucNganSach> NsMucLucsChild = new List<BhDmMucLucNganSach>();

                NsMucLucsChild = ctx.BhDmMucLucNganSachs.AsNoTracking().AsEnumerable().Where(defaultSktMucLucFilter).Where(x => lstLSN.Contains(x.SLNS) && x.ITrangThai == iTrangThai).ToList();

                List<BhDmMucLucNganSach> nsMucLucs = new List<BhDmMucLucNganSach>();
                if (NsMucLucsChild.Count > 0)
                {
                    var listIdMlskt = NsMucLucsChild.Select(item => item.IIDMLNS).ToList();
                    nsMucLucs = NsMucLucsChild;
                    while (true)
                    {
                        var listIdParent = NsMucLucsChild.Where(x => !listIdMlskt.Contains(x.IIDMLNSCha.GetValueOrDefault())).Select(x => x.IIDMLNSCha).ToList();
                        var listParent1 = ctx.BhDmMucLucNganSachs.Where(x => listIdParent.Contains(x.IIDMLNS) && x.INamLamViec == namLamViec).ToList();
                        if (listParent1.Any())
                        {
                            var lstId = listParent1.Select(item => item.IIDMLNS).ToList();
                            listIdMlskt.AddRange(lstId);
                            nsMucLucs.AddRange(listParent1);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                nsMucLucs = nsMucLucs.GroupBy(x => x.IIDMLNS).Select(x => x.First()).ToList();
                return nsMucLucs.Where(x => x.ITrangThai == iTrangThai).ToList();
            }
        }

        public IEnumerable<BhKhcKChiTiet> FindByIdChiTiet(Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhKhcKChiTiets.Where(x => x.IID_KHC_K == id).ToList();
            }
        }

        public IEnumerable<ReportKhcKQuery> FindChungTuTongHopForDonVi(string listTenDonVi, int iNamlamViec, Guid IDLoaichi, int donViTinh, string sLNS)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@listTenDonVi", listTenDonVi),
                    new SqlParameter("@namLamViec", iNamlamViec),
                    new SqlParameter("@LNS", sLNS),
                    new SqlParameter("@IDLoaichi",IDLoaichi),
                    new SqlParameter("@Dvt",donViTinh)

                };
                string executeSql = "EXECUTE dbo.sp_rpt_khc_k_chungtu_truongsDK_tonghop @listTenDonVi,@namLamViec,@LNS,@IDLoaichi,@Dvt";
                return ctx.FromSqlRaw<ReportKhcKQuery>(executeSql, parameters).ToList();

            }
        }
        public IEnumerable<ReportKhcKQuery> FindChungTuHSSVNLDForDonVi(string listTenDonVi, int iNamlamViec, int iLoaiTongHop, string lstSLNS)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@listTenDonVi", listTenDonVi),
                    new SqlParameter("@namLamViec", iNamlamViec),
                    new SqlParameter("@iLoaiTongHop",iLoaiTongHop),
                    new SqlParameter("@listSLNS",lstSLNS)
                };
                string executeSql = "EXECUTE dbo.sp_rpt_khc_khssv_nld_chungtu_tonghop_bhxh @listTenDonVi,@namLamViec,@iLoaiTongHop,@listSLNS";
                return ctx.FromSqlRaw<ReportKhcKQuery>(executeSql, parameters).ToList();

            }
        }

        public IEnumerable<BhKhcKChiTiet> GetReportKeHoach(KhcKChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                int namLamViec = searchCondition.NamLamViec;
                Guid khcKcbId = searchCondition.KhcKcbId;
                string sLoaiTroCap = searchCondition.sLoaiTroCap;
                int iTrangThai = searchCondition.ITrangThai;
                string idDonVi = searchCondition.IdDonVi;
                string SLNS = searchCondition.SLNS;
                List<BhDmMucLucNganSach> NsMucLucs;
                Func<DonVi, bool> defaultNsDonViFilter = x => x.NamLamViec == namLamViec;

                var nsDonVi = ctx.NsDonVis.Where(defaultNsDonViFilter)
                    .FirstOrDefault(x => x.IIDMaDonVi == idDonVi && x.NamLamViec == namLamViec);

                NsMucLucs = GetListMucLucBhxhByLoaiChungtu(namLamViec, idDonVi, SLNS);

                List<BhKhcKChiTiet> lstBhxhChungTuChiTiets = new List<BhKhcKChiTiet>();

                lstBhxhChungTuChiTiets = FindKhcChedoBhxhChiTiet(namLamViec, idDonVi, khcKcbId).ToList();

                var result = from nsMucLuc in NsMucLucs
                             join khcKhacChungTuChiTiet in lstBhxhChungTuChiTiets on nsMucLuc.IIDMLNS equals khcKhacChungTuChiTiet.IID_MucLucNganSach
                                         into gj
                             from sub in gj.DefaultIfEmpty()
                             orderby nsMucLuc.SXauNoiMa

                             select new BhKhcKChiTiet
                             {
                                 Id = sub == null ? Guid.NewGuid() : sub.Id == Guid.Empty ? Guid.NewGuid() : sub.Id,
                                 IID_KHC_K = khcKcbId,
                                 SNoiDung = nsMucLuc.SMoTa,
                                 IIdMaDonVi = nsDonVi?.IIDMaDonVi ?? string.Empty,
                                 IdParent = nsMucLuc.IIDMLNSCha,
                                 STenDonVi = nsDonVi?.TenDonVi ?? string.Empty,
                                 IID_MucLucNganSach = nsMucLuc.IIDMLNS,
                                 IsHangCha = nsMucLuc.BHangCha,
                                 IsAdd = sub == null,
                                 DNgayTao = null,
                                 SNguoiTao = null,
                                 DNgaySua = null,
                                 SNguoiSua = null,
                                 FTienDaThucHienNamTruoc = sub?.FTienDaThucHienNamTruoc ?? 0,
                                 FTienUocThucHienNamTruoc = sub?.FTienUocThucHienNamTruoc ?? 0,
                                 FTienKeHoachThucHienNamNay = sub?.FTienKeHoachThucHienNamNay ?? 0,
                                 SGhiChu = sub?.SGhiChu ?? string.Empty,
                                 SXauNoiMa = nsMucLuc.SXauNoiMa,
                                 STTM = nsMucLuc.STTM,
                             };
                return result;
            }
        }
    }
}
