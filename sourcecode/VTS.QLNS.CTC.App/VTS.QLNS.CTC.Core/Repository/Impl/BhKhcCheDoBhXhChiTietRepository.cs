using Microsoft.EntityFrameworkCore;
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
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class BhKhcCheDoBhXhChiTietRepository : Repository<BhKhcCheDoBhXhChiTiet>, IBhKhcCheDoBhXhChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public BhKhcCheDoBhXhChiTietRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhKhcCheDoBhXhChiTietQuery> FindTongHopIndex()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_nh_quyettoan_niendo_tonghop_index";
                return ctx.FromSqlRaw<BhKhcCheDoBhXhChiTietQuery>(executeSql, new { }).ToList();
            }
        }
        public IEnumerable<BhKhcCheDoBhXhChiTiet> FindByIdChiTiet(Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhKhcCheDoBhXhChiTiets.Where(x => x.IID_KHC_CheDoBHXH == id).ToList();
            }
        }

        public IEnumerable<BhKhcCheDoBhXhChiTiet> FindByConditionForChildUnit(KhcCheDoBhXhChiTietCriteria searchCondition)
        {
            return FindByConditionForChildUnitSummary(searchCondition);
        }

        public IEnumerable<BhKhcCheDoBhXhChiTiet> FindByConditionForDonVi(KhcCheDoBhXhChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                int namLamViec = searchCondition.NamLamViec;
                Guid khcBHXHId = searchCondition.KhcBhxhId;
                string sLoaiTroCap = searchCondition.sLoaiTroCap;
                int iTrangThai = searchCondition.ITrangThai;
                string idDonVi = searchCondition.IdDonVi;

                Func<DonVi, bool> defaultNsDonViFilter = x => x.NamLamViec == namLamViec;

                var nsDonVi = ctx.NsDonVis.Where(defaultNsDonViFilter)
                    .FirstOrDefault(x => x.IIDMaDonVi == idDonVi && x.NamLamViec == namLamViec);

                //var nsChungTuChiTiets = ctx.BhKhcCheDoBhXhChiTiets.AsNoTracking().AsEnumerable().ToList();

                var NsMucLucs = GetListMucLucBhxhByLoaiChungtu(namLamViec, idDonVi);
                List<BhKhcCheDoBhXhChiTiet> lstBhxhChungTuChiTiets = new List<BhKhcCheDoBhXhChiTiet>();

                lstBhxhChungTuChiTiets = FindKhcChedoBhxhChiTiet(namLamViec, idDonVi).ToList();

                var result = from nsMucLuc in NsMucLucs
                             join khcCheDoBHXHChungTuChiTiet in lstBhxhChungTuChiTiets on nsMucLuc.IIDMLNS equals khcCheDoBHXHChungTuChiTiet.IID_MucLucNganSach
                                         into gj
                             from sub in gj.DefaultIfEmpty()
                             orderby nsMucLuc.SXauNoiMa
                             select new BhKhcCheDoBhXhChiTiet
                             {
                                 Id = sub == null ? Guid.NewGuid() : sub.Id == Guid.Empty ? Guid.NewGuid() : sub.Id,
                                 IID_KHC_CheDoBHXH = sub == null ? null : sub.IID_KHC_CheDoBHXH,
                                 SLoaiTroCap = nsMucLuc.SMoTa,
                                 SMoTa = nsMucLuc.SMoTa,
                                 SM = nsMucLuc.SM,
                                 Nganh = nsMucLuc.SNG,
                                 INamLamViec = sub?.INamLamViec ?? 0,
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
                                 ISoDaThucHienNamTruoc = sub?.ISoDaThucHienNamTruoc ?? 0,
                                 FTienDaThucHienNamTruoc = sub?.FTienDaThucHienNamTruoc ?? 0,
                                 ISoUocThucHienNamTruoc = sub?.ISoUocThucHienNamTruoc ?? 0,
                                 FTienUocThucHienNamTruoc = sub?.FTienUocThucHienNamTruoc ?? 0,
                                 ISoKeHoachThucHienNamNay = sub?.ISoKeHoachThucHienNamNay ?? 0,
                                 FTienKeHoachThucHienNamNay = sub?.FTienKeHoachThucHienNamNay ?? 0,
                                 ISoSQ = sub?.ISoSQ ?? 0,
                                 FTienSQ = sub?.FTienSQ ?? 0,
                                 ISoQNCN = sub?.ISoQNCN ?? 0,
                                 FTienQNCN = sub?.FTienQNCN ?? 0,
                                 ISoCNVQP = sub?.ISoCNVQP ?? 0,
                                 FTienCNVQP = sub?.FTienCNVQP ?? 0,
                                 ISoLDHD = sub?.ISoLDHD ?? 0,
                                 FTienLDHD = sub?.FTienLDHD ?? 0,
                                 ISoHSQBS = sub?.ISoHSQBS ?? 0,
                                 FTienHSQBS = sub?.FTienHSQBS ?? 0,
                                 SGhiChu = sub == null ? string.Empty : sub.SGhiChu,
                                 SXauNoiMa = nsMucLuc.SXauNoiMa

                             };
                return result;
            }
        }
        public IEnumerable<BhKhcCheDoBhXhChiTiet> FindByConditionForChildUnitSummary(KhcCheDoBhXhChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                int namLamViec = searchCondition.NamLamViec;
                Guid khcBHXHId = searchCondition.KhcBhxhId;
                string sLoaiTroCap = searchCondition.sLoaiTroCap;
                int iTrangThai = searchCondition.ITrangThai;
                string idDonVi = searchCondition.IdDonVi;

                Func<DonVi, bool> defaultNsDonViFilter = x => x.NamLamViec == namLamViec;

                var nsDonVi = ctx.NsDonVis.Where(defaultNsDonViFilter)
                    .FirstOrDefault(x => x.IIDMaDonVi == idDonVi && x.NamLamViec == namLamViec);

                //var nsChungTuChiTiets = ctx.BhKhcCheDoBhXhChiTiets.AsNoTracking().AsEnumerable().ToList();

                var NsMucLucs = GetListMucLucBhxhByLoaiChungtu(namLamViec, idDonVi);
                List<BhKhcCheDoBhXhChiTiet> lstBhxhChungTuChiTiets = new List<BhKhcCheDoBhXhChiTiet>();

                lstBhxhChungTuChiTiets = FindKhcChedoBhxhChiTiet(namLamViec, idDonVi).Where(x => x.IID_KHC_CheDoBHXH == khcBHXHId).ToList();

                var result = from nsMucLuc in NsMucLucs
                             join khcCheDoBHXHChungTuChiTiet in lstBhxhChungTuChiTiets on nsMucLuc.IIDMLNS equals khcCheDoBHXHChungTuChiTiet.IID_MucLucNganSach
                                         into gj
                             from sub in gj.DefaultIfEmpty()
                             orderby nsMucLuc.SXauNoiMa
                             select new BhKhcCheDoBhXhChiTiet
                             {
                                 Id = sub == null ? Guid.NewGuid() : sub.Id == Guid.Empty ? Guid.NewGuid() : sub.Id,
                                 IID_KHC_CheDoBHXH = khcBHXHId,
                                 SLoaiTroCap = nsMucLuc.SMoTa,
                                 SMoTa = nsMucLuc.SMoTa,
                                 SM = nsMucLuc.SM,
                                 Nganh = nsMucLuc.SNG,
                                 INamLamViec = sub?.INamLamViec ?? 0,
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
                                 ISoDaThucHienNamTruoc = sub?.ISoDaThucHienNamTruoc ?? 0,
                                 FTienDaThucHienNamTruoc = sub?.FTienDaThucHienNamTruoc ?? 0,
                                 ISoUocThucHienNamTruoc = sub?.ISoUocThucHienNamTruoc ?? 0,
                                 FTienUocThucHienNamTruoc = sub?.FTienUocThucHienNamTruoc ?? 0,
                                 ISoKeHoachThucHienNamNay = sub?.ISoKeHoachThucHienNamNay ?? 0,
                                 FTienKeHoachThucHienNamNay = sub?.FTienKeHoachThucHienNamNay ?? 0,
                                 ISoSQ = sub?.ISoSQ ?? 0,
                                 FTienSQ = sub?.FTienSQ ?? 0,
                                 ISoQNCN = sub?.ISoQNCN ?? 0,
                                 FTienQNCN = sub?.FTienQNCN ?? 0,
                                 ISoCNVQP = sub?.ISoCNVQP ?? 0,
                                 FTienCNVQP = sub?.FTienCNVQP ?? 0,
                                 ISoLDHD = sub?.ISoLDHD ?? 0,
                                 FTienLDHD = sub?.FTienLDHD ?? 0,
                                 ISoHSQBS = sub?.ISoHSQBS ?? 0,
                                 FTienHSQBS = sub?.FTienHSQBS ?? 0,
                                 SGhiChu = sub == null ? string.Empty : sub.SGhiChu,
                                 SXauNoiMa = nsMucLuc.SXauNoiMa

                             };
                return result;
            }
        }

        public List<BhDmMucLucNganSach> GetListMucLucBhxhByLoaiChungtu(int namLamViec, string idDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var loaiChungTuNSSD = int.Parse(VoucherType.NSSD_Key);
                var loaiChungTuNSBD = int.Parse(VoucherType.NSBD_Key);
                var iTrangThai = StatusType.ACTIVE;
                Func<BhDmMucLucNganSach, bool> defaultSktMucLucFilter = x => x.INamLamViec == namLamViec;
                var nsDonVi = ctx.NsDonVis.Where(x => x.IIDMaDonVi == idDonVi && x.NamLamViec == namLamViec && x.ITrangThai == iTrangThai).FirstOrDefault();
                if (nsDonVi == null) return new List<BhDmMucLucNganSach>();
                List<BhDmMucLucNganSach> NsMucLucsChild = new List<BhDmMucLucNganSach>();

                NsMucLucsChild = ctx.BhDmMucLucNganSachs.AsNoTracking().AsEnumerable().Where(defaultSktMucLucFilter).Where(x => x.SLNS == KhcBhxhMLNS.KHOI_DU_TOAN || x.SLNS == KhcBhxhMLNS.KHOI_HACH_TOAN).ToList();

                List<BhDmMucLucNganSach> nsMucLucs = new List<BhDmMucLucNganSach>();
                if (NsMucLucsChild.Count > 0)
                {
                    var listIdMlskt = NsMucLucsChild.Select(item => item.IIDMLNS).ToList();
                    nsMucLucs = NsMucLucsChild;
                    while (true)
                    {
                        var test = NsMucLucsChild.Where(x => !listIdMlskt.Contains(x.IIDMLNSCha.GetValueOrDefault())).ToList();
                        var listIdParent = NsMucLucsChild.Where(x => !listIdMlskt.Contains(x.IIDMLNSCha.GetValueOrDefault())).Select(x => x.IIDMLNSCha).ToList();
                        var listParent1 = ctx.BhDmMucLucNganSachs.Where(x => listIdParent.Contains(x.IIDMLNS) && x.INamLamViec == namLamViec).ToList();
                        if (listParent1.Count > 0)
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
        public IEnumerable<BhKhcCheDoBhXhChiTiet> FindKhcChedoBhxhChiTiet(int namLamViec, string idDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter idDonViParam = new SqlParameter("@IdDonVi", idDonVi);

                return ctx.Set<BhKhcCheDoBhXhChiTiet>().FromSql("EXECUTE dbo.sp_khc_cd_bhxh_chi_tiet @NamLamViec, @IdDonVi",
                    namLamViecParam, idDonViParam).ToList();
            }
        }

        public bool ExistBHXHChiTiet(Guid bhxhId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhKhcCheDoBhXhChiTiets.Any(t => t.IID_KHC_CheDoBHXH.Equals(bhxhId));
            }
        }

        public void AddAggregate(KhcCheDoBhXhChiTietCriteria creation)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter listIdChungTuTongHop = new SqlParameter("@ListIdChungTuTongHop", creation.ListIdChungTuTongHop);
                SqlParameter nguoiTao = new SqlParameter("@Nguoitao", creation.NguoiTao);
                SqlParameter idMaDonvi = new SqlParameter("@IDMaDonVi", creation.IdDonVi);
                SqlParameter idChungTu = new SqlParameter("@IdChungTu", creation.IdChungTu);
                SqlParameter namLamViec = new SqlParameter("@NamLamViec", creation.NamLamViec);

                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_khc_cd_bhxh_chungtu_chitiet_tao_tonghop @ListIdChungTuTongHop,@IDMaDonVi,@Nguoitao ,@IdChungTu, @NamLamViec",
                    listIdChungTuTongHop, idMaDonvi, nguoiTao, idChungTu, namLamViec);
            }
        }

        public IEnumerable<ReportKhcTongHopBHXHQuery> FindChungTuTongHopForDonVi(int iNamlamViec, string listTenDonVi, int iLoaiTongHop)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@listTenDonVi", listTenDonVi),
                    new SqlParameter("@namLamViec", iNamlamViec),
                    new SqlParameter("@iLoaiTongHop",iLoaiTongHop)
                };
                string executeSql = "EXECUTE dbo.sp_rpt_khc_chungtu_tonghop_bhxh @listTenDonVi,@namLamViec,@iLoaiTongHop";
                return ctx.FromSqlRaw<ReportKhcTongHopBHXHQuery>(executeSql, parameters).ToList();

            }
        }

        public List<BhKhcCheDoBhXhChiTiet> GetDataDetailVoucher(KhcCheDoBhXhChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", searchCondition.NamLamViec),
                    new SqlParameter("@IdDonVi", searchCondition.IdDonVi),
                    new SqlParameter("@Dvt",searchCondition.DonViTinh)
                };
                string executeSql = "EXECUTE dbo.sp_rpt_khc_bhxh_chitiet @NamLamViec,@IdDonVi,@Dvt";
                return ctx.FromSqlRaw<BhKhcCheDoBhXhChiTiet>(executeSql, parameters).ToList();

            }
        }

        public List<BhKhcCheDoBhXhChiTiet> GetDataSummaryDetailVoucher(KhcCheDoBhXhChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", searchCondition.NamLamViec),
                    new SqlParameter("@IdDonVi", searchCondition.IdDonVi),
                    new SqlParameter("@Dvt",searchCondition.DonViTinh)
                };
                string executeSql = "EXECUTE dbo.sp_rpt_khc_bhxh_chitiet_tonghop_donvi @NamLamViec,@IdDonVi,@Dvt";
                return ctx.FromSqlRaw<BhKhcCheDoBhXhChiTiet>(executeSql, parameters).ToList();

            }
        }

        public IEnumerable<BhKhcCheDoBhXhChiTietQuery> GetPlanData(int iNam, string sSoChungTu, string sLNS)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_get_data_khc_clone @NamLamViec, @SoChungTu,@SLNS";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", iNam),
                    new SqlParameter("@SoChungTu", sSoChungTu),
                      new SqlParameter("@SLNS", sLNS)
                };
                return ctx.FromSqlRaw<BhKhcCheDoBhXhChiTietQuery>(sql, parameters).ToList();
            }
        }
    }
}
