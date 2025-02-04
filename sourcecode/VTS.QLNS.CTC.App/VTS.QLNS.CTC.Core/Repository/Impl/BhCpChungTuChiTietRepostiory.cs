using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;
using System.Data.SqlClient;
using VTS.QLNS.CTC.Core.Domain.Query;
using System.Web.UI.WebControls;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class BhCpChungTuChiTietRepostiory : Repository<BhCpChungTuChiTiet>, IBhCpChungTuChiTietRepostiory
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public BhCpChungTuChiTietRepostiory(ApplicationDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            _contextFactory = dbContextFactory;
        }

        public void AddAggregate(BhCpChungTuChiChiTietCriteria criteria)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter listIdChungTuTongHop = new SqlParameter("@ListIdChungTuTongHop", criteria.ListIdChungTu);
                SqlParameter nguoiTao = new SqlParameter("@Nguoitao", criteria.NguoiTao);
                SqlParameter idChungTu = new SqlParameter("@IdChungTu", criteria.IdChungTu);
                SqlParameter namLamViec = new SqlParameter("@NamLamViec", criteria.NamLamViec);

                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_cp_chungtu_chi_tiet_tonghop @ListIdChungTuTongHop,@Nguoitao ,@IdChungTu, @NamLamViec",
                    listIdChungTuTongHop, nguoiTao, idChungTu, namLamViec);
            }
        }

        public bool ExistCpChungTuChiTiet(BhCpChungTuChiChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhCpChungTuChiTiets.Any(t => t.IID_CP_ChungTu.Equals(searchCondition.CpChungTuChiId));
            }
        }

        public List<BhCpChungTuChiTietQuery> FindBhCpChungTuCheDoBHXHChiTiet(BhCpChungTuChiChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@VoucherId",searchCondition.CpChungTuChiId),
                    new SqlParameter("@LNS",searchCondition.SLNS),
                    new SqlParameter("@YearOfWork", searchCondition.NamLamViec),
                    new SqlParameter("@AgencyId", searchCondition.SIdDonVi),
                    new SqlParameter("@VoucherDate", searchCondition.NgayChungTu),
                    new SqlParameter("@iID_LoaiDanhMucChi", searchCondition.ILoaiDanhMucChi)
                };

                string executeSql = "EXECUTE dbo.sp_cp_chungtu_chi_tiet_chedo_bhxh @VoucherId, @LNS,@YearOfWork,@AgencyId,@VoucherDate,@iID_LoaiDanhMucChi";
                return ctx.FromSqlRaw<BhCpChungTuChiTietQuery>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<DonVi> FindByDonViOfAllocationThongTri(int yearOfWork, int quy, Guid idLoaiChi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_bhxh_cp_get_donvi_thongtri @YearOfWork, @Quy";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", yearOfWork),
                    new SqlParameter("@Quy", quy),
                };
                return ctx.NsDonVis.FromSql(executeSql, parameters).ToList();
            }
        }

        public List<BhCpChungTuChiTietQuery> FindBhCpChungTuChiTiet(BhCpChungTuChiChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@VoucherId",searchCondition.CpChungTuChiId),
                    new SqlParameter("@LNS",searchCondition.SLNS),
                    new SqlParameter("@YearOfWork", searchCondition.NamLamViec),
                    new SqlParameter("@AgencyId", searchCondition.SIdDonVi),
                    new SqlParameter("@VoucherDate", searchCondition.NgayChungTu),
                    new SqlParameter("@iID_LoaiDanhMucChi", searchCondition.ILoaiDanhMucChi)
                };

                string executeSql = "EXECUTE dbo.sp_cp_chungtu_chi_tiet @VoucherId, @LNS,@YearOfWork,@AgencyId,@VoucherDate,@iID_LoaiDanhMucChi";
                return ctx.FromSqlRaw<BhCpChungTuChiTietQuery>(executeSql, parameters).ToList();
            }
        }

        public List<BhCpChungTuChiTietQuery> FindBhCpChungTuCSSKHSSVandNLDChiTiet(BhCpChungTuChiChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@VoucherId",searchCondition.CpChungTuChiId),
                    new SqlParameter("@LNS",searchCondition.SLNS),
                    new SqlParameter("@YearOfWork", searchCondition.NamLamViec),
                    new SqlParameter("@AgencyId", searchCondition.SIdDonVi),
                    new SqlParameter("@VoucherDate", searchCondition.NgayChungTu),
                    new SqlParameter("@iID_LoaiDanhMucChi", searchCondition.ILoaiDanhMucChi)
                };

                string executeSql = "EXECUTE dbo.sp_cp_chungtu_chi_tiet_cssk_hssv_nld @VoucherId, @LNS,@YearOfWork,@AgencyId,@VoucherDate,@iID_LoaiDanhMucChi";
                return ctx.FromSqlRaw<BhCpChungTuChiTietQuery>(executeSql, parameters).ToList();
            }
        }

        public BhCpChungTuChiTiet FindById(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhCpChungTuChiTiets.Find(id);
            }
        }

        public IEnumerable<BhCpChungTuChiTiet> FindByIdChiTiet(Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhCpChungTuChiTiets.Where(x => x.IID_CP_ChungTu == id).ToList();
            }
        }


        private List<BhDmMucLucNganSach> GetListMucLucCapPhatLoaiChungtu(int? namLamViec, string idDonVi, string sLNS)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var dataSLNS = sLNS.Split(',');
                var iTrangThai = StatusType.ACTIVE;
                Func<BhDmMucLucNganSach, bool> defaultSktMucLucFilter = x => x.INamLamViec == namLamViec;
                var nsDonVi = ctx.NsDonVis.Where(x => x.IIDMaDonVi == idDonVi && x.NamLamViec == namLamViec && x.ITrangThai == iTrangThai).FirstOrDefault();
                if (nsDonVi == null) return new List<BhDmMucLucNganSach>();
                List<BhDmMucLucNganSach> NsMucLucsChild = new List<BhDmMucLucNganSach>();

                NsMucLucsChild = ctx.BhDmMucLucNganSachs.AsNoTracking().AsEnumerable().Where(defaultSktMucLucFilter).Where(x => dataSLNS.Contains(x.SLNS)).ToList();

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
                return nsMucLucs;
            }
        }

        public List<ReportBHChungTuCapPhatThongTriQuery> GetDataReportCapPhatThongTriNhieuLoaiChi(int yearOfWork, string iIDMaDonVi, string principal, int donViTinh, int iQuy, string lstsIDLoaiChi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec",yearOfWork),
                    new SqlParameter("@IdMaDonVi",iIDMaDonVi),
                    new SqlParameter("@Quy", iQuy),
                    new SqlParameter("@donViTinh", donViTinh),
                    new SqlParameter("@lstsIDLoaiChi", lstsIDLoaiChi),
                };

                string executeSql = "EXECUTE dbo.sp_cp_rpt_get_thongtri_nhieuloaichi @NamLamViec, @IdMaDonVi,@Quy,@donViTinh,@lstsIDLoaiChi";
                return ctx.FromSqlRaw<ReportBHChungTuCapPhatThongTriQuery>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<BhDanhMucLoaiChi> GetDanhMucLoaiChi(int yearOfWork, int quy, string maDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork",yearOfWork),
                    new SqlParameter("@Quy", quy),
                    new SqlParameter("@LstDonVi", maDonVi),
                };

                string executeSql = "EXECUTE dbo.sp_cp_get_loaichi_thongtri @YearOfWork, @Quy,@LstDonVi";
                return ctx.FromSqlRaw<BhDanhMucLoaiChi>(executeSql, parameters).ToList();
            }
        }
    }
}