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
    public class BhKhcKcbChiTietRepository : Repository<BhKhcKcbChiTiet>, IBhKhcKcbChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public BhKhcKcbChiTietRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void AddAggregate(KhcKcbChiTietCriteria creation)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter listIdChungTuTongHop = new SqlParameter("@ListIdChungTuTongHop", creation.ListIdChungTuTongHop);
                SqlParameter iDMaDonVi = new SqlParameter("@IDMaDonVi", creation.IdDonVi);
                SqlParameter nguoiTao = new SqlParameter("@Nguoitao", creation.NguoiTao);
                SqlParameter idChungTu = new SqlParameter("@IdChungTu", creation.IdChungTu);
                SqlParameter namLamViec = new SqlParameter("@NamLamViec", creation.NamLamViec);

                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_khc_kcb_chungtu_chitiet_tao_tonghop @ListIdChungTuTongHop,@IDMaDonVi,@Nguoitao ,@IdChungTu, @NamLamViec",
                    listIdChungTuTongHop, iDMaDonVi, nguoiTao, idChungTu, namLamViec);
            }
        }

        public bool ExistKhcKcbChiTiet(Guid bhxhId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhKhcKcbChiTiets.Any(t => t.IID_KHC_KCB.Equals(bhxhId));
            }
        }

        public IEnumerable<BhKhcKcbChiTiet> FindByConditionForChildUnit(KhcKcbChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                int namLamViec = searchCondition.NamLamViec;
                Guid khcKcbId = searchCondition.KhcKcbId;
                string sLoaiTroCap = searchCondition.sLoaiTroCap;
                int iTrangThai = searchCondition.ITrangThai;
                string idDonVi = searchCondition.IdDonVi;
                List<BhDmMucLucNganSach> NsMucLucs;

                Func<DonVi, bool> defaultNsDonViFilter = x => x.NamLamViec == namLamViec;

                var nsDonVi = ctx.NsDonVis.Where(defaultNsDonViFilter)
                    .FirstOrDefault(x => x.IIDMaDonVi == idDonVi && x.NamLamViec == namLamViec);
                NsMucLucs = GetListMucLucBhxhByLoaiChungtu(namLamViec, idDonVi, KhcKcbBhxhMLNS.QUAN_Y_DON_VI_KHOI_DU_TOAN, KhcKcbBhxhMLNS.QUAN_Y_DON_VI_KHOI_HACH_TOAN);

                List<BhKhcKcbChiTiet> lstBhxhChungTuChiTiets = new List<BhKhcKcbChiTiet>();

                lstBhxhChungTuChiTiets = FindKhcChedoBhxhChiTiet(namLamViec, idDonVi, khcKcbId).ToList();

                var result = from nsMucLuc in NsMucLucs
                             join khcCheDoBHXHChungTuChiTiet in lstBhxhChungTuChiTiets on nsMucLuc.IIDMLNS equals khcCheDoBHXHChungTuChiTiet.IID_MucLucNganSach
                                         into gj
                             from sub in gj.DefaultIfEmpty()
                             orderby nsMucLuc.SXauNoiMa

                             select new BhKhcKcbChiTiet
                             {
                                 Id = sub == null ? Guid.NewGuid() : sub.Id == Guid.Empty ? Guid.NewGuid() : sub.Id,
                                 IID_KHC_KCB = khcKcbId,
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

        private IEnumerable<BhKhcKcbChiTiet> FindKhcChedoBhxhChiTiet(int namLamViec, string idDonVi, Guid khcKcbId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter idDonViParam = new SqlParameter("@IdDonVi", idDonVi);
                SqlParameter idKhcKinhphiQuanlyParam = new SqlParameter("@iID_BH_KHC_KCB", khcKcbId);

                return ctx.Set<BhKhcKcbChiTiet>().FromSql("EXECUTE dbo.sp_khc_kcb_chi_tiet @NamLamViec, @IdDonVi,@iID_BH_KHC_KCB",
                    namLamViecParam, idDonViParam, idKhcKinhphiQuanlyParam).ToList();
            }
        }

        private List<BhDmMucLucNganSach> GetListMucLucBhxhByLoaiChungtu(int namLamViec, string idDonVi, string sLNS1, string sLNS2)
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

                NsMucLucsChild = ctx.BhDmMucLucNganSachs.AsNoTracking().AsEnumerable().Where(defaultSktMucLucFilter).Where(x => x.SLNS == sLNS1 || x.SLNS == sLNS2).ToList();

                List<BhDmMucLucNganSach> nsMucLucs = new List<BhDmMucLucNganSach>();
                if (NsMucLucsChild.Count > 0)
                {
                    var listIdMlskt = NsMucLucsChild.Select(item => item.IIDMLNS).ToList();
                    nsMucLucs = NsMucLucsChild;
                    while (true)
                    {
                        var listIdParent = NsMucLucsChild.Where(x => !listIdMlskt.Contains(x.IIDMLNSCha.GetValueOrDefault())).Select(x => x.IIDMLNSCha).ToList();
                        var listParent1 = ctx.BhDmMucLucNganSachs.Where(x => listIdParent.Contains(x.IIDMLNS) && x.INamLamViec == namLamViec).ToList();
                        if (!listParent1.IsEmpty() && listParent1.Any(x => !string.IsNullOrEmpty(x.SLNS) && x.SLNS.Count() > 1))
                        {
                            // bỏ những MLNS cha có SLNS = 1
                            listParent1 = listParent1.Where(x => !string.IsNullOrEmpty(x.SLNS) && x.SLNS.Count() > 1).ToList();
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

        public IEnumerable<BhKhcKcbChiTiet> FindByIdChiTiet(Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhKhcKcbChiTiets.Where(x => x.IID_KHC_KCB == id).ToList();
            }
        }

        public IEnumerable<ReportKhcKcbBHXHQuery> FindChungTuTongHopForDonVi(int iNamlamViec, string listTenDonVi, int iLoaiTongHop)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@listTenDonVi", listTenDonVi),
                    new SqlParameter("@namLamViec", iNamlamViec),
                    new SqlParameter("@iLoaiTongHop",iLoaiTongHop)
                };
                string executeSql = "EXECUTE dbo.sp_rpt_khc_kcb_chungtu_tonghop_bhxh @listTenDonVi,@namLamViec,@iLoaiTongHop";
                return ctx.FromSqlRaw<ReportKhcKcbBHXHQuery>(executeSql, parameters).ToList();

            }
        }

        public List<BhKhcKcbChiTiet> GetDataDetailVoucher(KhcKcbChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", searchCondition.NamLamViec),
                    new SqlParameter("@IdDonVi", searchCondition.IdDonVi),
                    new SqlParameter("@Dvt",searchCondition.DonViTinh)
                };
                string executeSql = "EXECUTE dbo.sp_rpt_khc_kcbqy_chitiet @NamLamViec,@IdDonVi,@Dvt";
                return ctx.FromSqlRaw<BhKhcKcbChiTiet>(executeSql, parameters).ToList();

            }
        }

        public IEnumerable<BhKhcKcbChiTietQuery> FindGiaTriKeHoachThuBHXH(string sMaDonVi, int iNamLamViec, double fTyLeThu)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_bhxh_khckcb_getBHYTQN @sMaDonVi,@iNamLamViec,@fTyLeThu,@fTyLeThuTheoPhanTram";
                var parameters = new[] {
                    new SqlParameter("@sMaDonVi", sMaDonVi),
                    new SqlParameter("@iNamLamViec", iNamLamViec),
                    new SqlParameter("@fTyLeThu", fTyLeThu),
                     new SqlParameter("@fTyLeThuTheoPhanTram", fTyLeThu.ToString()),
                };
                return ctx.FromSqlRaw<BhKhcKcbChiTietQuery>(executeSql, parameters).ToList();
            }
        }
    }
}
