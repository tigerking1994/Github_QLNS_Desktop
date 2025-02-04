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
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class BhKhcKinhphiQuanlyChiTietRepostiory : Repository<BhKhcKinhphiQuanlyChiTiet>, IBhKhcKinhphiQuanlyChiTietRepostiory
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public BhKhcKinhphiQuanlyChiTietRepostiory(ApplicationDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            _contextFactory = dbContextFactory;
        }

        public IEnumerable<BhKhcKinhphiQuanlyChiTiet> FindByConditionForChildUnit(KhcQuanlyKinhphiChiTietCriteria searchCondition)
        {
            return FindByConditionForChildUnitSummary(searchCondition);
        }

        private IEnumerable<BhKhcKinhphiQuanlyChiTiet> FindByConditionForChildUnitSummary(KhcQuanlyKinhphiChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                int namLamViec = searchCondition.NamLamViec;
                Guid khcBHXHId = searchCondition.KhcKinhphiQuanlyId;
                string sLoaiTroCap = searchCondition.sLoaiTroCap;
                int iTrangThai = searchCondition.ITrangThai;
                string idDonVi = searchCondition.IdDonVi;

                Func<DonVi, bool> defaultNsDonViFilter = x => x.NamLamViec == namLamViec;

                var nsDonVi = ctx.NsDonVis.Where(defaultNsDonViFilter)
                    .FirstOrDefault(x => x.IIDMaDonVi == idDonVi && x.NamLamViec == namLamViec);

                //var nsChungTuChiTiets = ctx.BhKhcCheDoBhXhChiTiets.AsNoTracking().AsEnumerable().ToList();

                var NsMucLucs = GetListMucLucBhxhByLoaiChungtu(namLamViec, idDonVi);
                List<BhKhcKinhphiQuanlyChiTiet> lstBhxhChungTuChiTiets = new List<BhKhcKinhphiQuanlyChiTiet>();

                lstBhxhChungTuChiTiets = FindKhcChedoBhxhChiTiet(namLamViec, idDonVi, khcBHXHId).ToList();

                var result = from nsMucLuc in NsMucLucs
                             join khcCheDoBHXHChungTuChiTiet in lstBhxhChungTuChiTiets on nsMucLuc.IIDMLNS equals khcCheDoBHXHChungTuChiTiet.IID_MucLucNganSach
                                         into gj
                             from sub in gj.DefaultIfEmpty()
                             orderby nsMucLuc.SXauNoiMa
                             select new BhKhcKinhphiQuanlyChiTiet
                             {
                                 Id = sub == null ? Guid.NewGuid() : sub.Id == Guid.Empty ? Guid.Empty : sub.Id,
                                 IID_KHC_KinhPhiQuanLy = khcBHXHId,
                                 SM = nsMucLuc.SM,
                                 STM = nsMucLuc.STM,
                                 SNoiDung = nsMucLuc.SMoTa,
                                 IIdMaDonVi = nsDonVi?.IIDMaDonVi ?? string.Empty,
                                 IdParent = nsMucLuc.IIDMLNSCha,
                                 STenDonVi = sub == null ? string.Empty : nsDonVi?.TenDonVi,
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
                                 FTienCanBo = sub?.FTienCanBo ?? 0,
                                 FTienQuanLuc = sub?.FTienQuanLuc ?? 0,
                                 FTienTaiChinh = sub?.FTienTaiChinh ?? 0,
                                 FTienQuanY = sub?.FTienQuanY ?? 0,
                                 SGhiChu = sub?.SGhiChu ?? string.Empty,
                                 SXauNoiMa = nsMucLuc.SXauNoiMa,
                                 STTM = nsMucLuc.STTM,

                             };
                return result;
            }
        }

        public IEnumerable<BhKhcKinhphiQuanlyChiTiet> FindKhcChedoBhxhChiTiet(int namLamViec, string idDonVi, Guid iID_KHC_KinhPhiQuanLy)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter idDonViParam = new SqlParameter("@IdDonVi", idDonVi);
                SqlParameter idKhcKinhphiQuanlyParam = new SqlParameter("@iID_KHC_KinhPhiQuanLy", iID_KHC_KinhPhiQuanLy);

                return ctx.Set<BhKhcKinhphiQuanlyChiTiet>().FromSql("EXECUTE dbo.sp_khc_kinhphi_quanly_chi_tiet @NamLamViec, @IdDonVi,@iID_KHC_KinhPhiQuanLy",
                    namLamViecParam, idDonViParam, idKhcKinhphiQuanlyParam).ToList();
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

                NsMucLucsChild = ctx.BhDmMucLucNganSachs.AsNoTracking().AsEnumerable().Where(defaultSktMucLucFilter).Where(x => x.SLNS == KhcQlBhxhMLNS.KHOI).ToList();

                List<BhDmMucLucNganSach> nsMucLucs = new List<BhDmMucLucNganSach>();
                if (NsMucLucsChild.Count > 0)
                {
                    var listIdMlskt = NsMucLucsChild.Select(item => item.IIDMLNS).ToList();
                    nsMucLucs = NsMucLucsChild;
                    while (true)
                    {
                        var test = NsMucLucsChild.Where(x => !listIdMlskt.Contains(x.IIDMLNSCha.GetValueOrDefault())).ToList();
                        var listIdParent = NsMucLucsChild.Where(x => !listIdMlskt.Contains(x.IIDMLNSCha.GetValueOrDefault())).Select(x => x.IIDMLNSCha).ToList();
                        var listParent1 = ctx.BhDmMucLucNganSachs.Where(x => listIdParent.Contains(x.IIDMLNS) && x.INamLamViec == namLamViec ).ToList();
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
                return nsMucLucs.Where(x=> x.ITrangThai == iTrangThai).ToList();
            }
        }

        public bool ExistKhcKinhphiQuanlyChiTiet(Guid bhxhId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhKhcKinhphiQuanlyChiTiets.Any(t => t.IID_KHC_KinhPhiQuanLy.Equals(bhxhId));
            }
        }

        public IEnumerable<BhKhcKinhphiQuanlyChiTiet> FindByIdChiTiet(Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhKhcKinhphiQuanlyChiTiets.Where(x => x.IID_KHC_KinhPhiQuanLy == id).ToList();
            }
        }

        public void AddAggregate(KhcQuanlyKinhphiChiTietCriteria creation)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter listIdChungTuTongHop = new SqlParameter("@ListIdChungTuTongHop", creation.ListIdChungTuTongHop);
                SqlParameter iDMaDonVi = new SqlParameter("@IDMaDonVi", creation.IdDonVi);
                SqlParameter nguoiTao = new SqlParameter("@Nguoitao", creation.NguoiTao);
                SqlParameter idChungTu = new SqlParameter("@IdChungTu", creation.IdChungTu);
                SqlParameter namLamViec = new SqlParameter("@NamLamViec", creation.NamLamViec);

                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_khc_kinhphi_quanly_chungtu_chitiet_tao_tonghop @ListIdChungTuTongHop, @IDMaDonVi, @Nguoitao ,@IdChungTu, @NamLamViec",
                    listIdChungTuTongHop, iDMaDonVi, nguoiTao, idChungTu, namLamViec);
            }
        }

        public IEnumerable<ReportKhcQuanLyKinhPhiTongHopBHXHQuery> FindChungTuTongHopForDonVi(int iNamlamViec, string listTenDonVi, int iLoaiTongHop)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@listTenDonVi", listTenDonVi),
                    new SqlParameter("@namLamViec", iNamlamViec),
                    new SqlParameter("@iLoaiTongHop", iLoaiTongHop)
                };
                string executeSql = "EXECUTE dbo.sp_rpt_khc_qlkp_chungtu_tonghop_bhxh @listTenDonVi,@namLamViec,@iLoaiTongHop";
                return ctx.FromSqlRaw<ReportKhcQuanLyKinhPhiTongHopBHXHQuery>(executeSql, parameters).ToList();

            }
        }

        public List<BhKhcKinhphiQuanlyChiTiet> GetDataDetailVoucher(KhcQuanlyKinhphiChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", searchCondition.NamLamViec),
                    new SqlParameter("@IdDonVi", searchCondition.IdDonVi),
                    new SqlParameter("@Dvt",searchCondition.DonViTinh)
                };
                string executeSql = "EXECUTE dbo.sp_rpt_khc_kpql_chitiet @NamLamViec,@IdDonVi,@Dvt";
                return ctx.FromSqlRaw<BhKhcKinhphiQuanlyChiTiet>(executeSql, parameters).ToList();

            }
        }
    }
}
