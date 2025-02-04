using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class KhtBHXHChiTietRepository : Repository<BhKhtBHXHChiTiet>, IKhtBHXHChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public KhtBHXHChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhKhtBHXHChiTiet> FindBhKhtBHXHChiTietByCondition(KhtBHXHChiTietCriteria searchCondition)
        {
            return FindBHXHChiTietByCondition(searchCondition);
        }

        public IEnumerable<BhKhtBHXHChiTiet> FindKhtBHXHChiTietByIdBhxh(KhtBHXHChiTietCriteria searchCondition)
        {
            Guid khtBHXHId = searchCondition.khtBhxhId;
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhKhtBHXHChiTiets.Where(x => x.KhtBHXHId == khtBHXHId).ToList();
            }
        }

        public IEnumerable<BhKhtBHXHChiTiet> FindBHXHChiTietByCondition(KhtBHXHChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                //var heSoLCS = GetHeSoLCS(BHYTHeSoLuong.LCS);
                int? namLamViec = searchCondition.NamLamViec;
                var heSoLCS = GetThamSoLCS(namLamViec);
                int namNganSach = searchCondition.NamNganSach;
                int nguonNganSach = searchCondition.NguonNganSach;
                int iLoai = searchCondition.ILoai;
                int iTrangThai = searchCondition.ITrangThai;
                string idDonVi = searchCondition.IdDonVi;
                var dvt = searchCondition.DonViTinh;
                var idChungTuDonVi = GetIdChungTuByDonVi(searchCondition.MaDonVi, namLamViec);
                Guid khtBHXHId = searchCondition.IsPrintReport ? idChungTuDonVi : searchCondition.khtBhxhId;
                Func<DonVi, bool> defaultNsDonViFilter = x => x.NamLamViec == namLamViec;
                var bhMucLucs = GetListBhMucLucNs(namLamViec);
                var khtBhxhChiTiets = FindKhtBHXHChiTiet(khtBHXHId, namLamViec, searchCondition.MaDonVi).ToList();

                var result = from bhMucLuc in bhMucLucs
                             join khtBhxhChiTiet in khtBhxhChiTiets on bhMucLuc.IIDMLNS equals khtBhxhChiTiet.IIDMucLucNganSach
                                  into gj
                             from sub in gj.DefaultIfEmpty()
                             orderby bhMucLuc.SXauNoiMa
                             select new BhKhtBHXHChiTiet
                             {
                                 Id = sub == null ? Guid.NewGuid() : sub.Id == Guid.Empty ? Guid.NewGuid() : sub.Id,
                                 KhtBHXHId = khtBHXHId,
                                 IIDMucLucNganSach = bhMucLuc.IIDMLNS,
                                 STenBhMLNS = bhMucLuc.SMoTa,
                                 SXauNoiMa = bhMucLuc.SXauNoiMa,
                                 SLNS = bhMucLuc.SLNS,
                                 SL = bhMucLuc.SL,
                                 SK = bhMucLuc.SK,
                                 SM = bhMucLuc.SM,
                                 STM = bhMucLuc.STM,
                                 STTM = bhMucLuc.STTM,
                                 SNG = bhMucLuc.SNG,
                                 STNG = bhMucLuc.STNG,
                                 FTyLeBHXHNLD = bhMucLuc.FTyLeBHXHNLD,
                                 FTyLeBHXHNSD = bhMucLuc.FTyLeBHXHNSD,
                                 FTyLeBHYTNLD = bhMucLuc.FTyLeBHYTNLD,
                                 FTyLeBHYTNSD = bhMucLuc.FTyLeBHYTNSD,
                                 FTyLeBHTNNLD = bhMucLuc.FTyLeBHTNNLD,
                                 FTyLeBHTNNSD = bhMucLuc.FTyLeBHTNNSD,
                                 IsAdd = sub == null,
                                 DNgayTao = null,
                                 SNguoiTao = null,
                                 DNgaySua = null,
                                 SNguoiSua = null,
                                 IsHangCha = bhMucLuc.BHangCha,
                                 IdParent = bhMucLuc.IIDMLNSCha,
                                 FLuongChinh = ((bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_DU_TOAN || bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_HACH_TOAN) && sub?.FLuongChinh.GetValueOrDefault() == 0)
                                                            ? (sub?.IQSBQNam ?? 0) * 12 * (heSoLCS?.fGiaTri ?? 0) : (sub?.FLuongChinh ?? 0),
                                 FPhuCapChucVu = sub?.FPhuCapChucVu ?? 0,
                                 FPCTNNghe = sub?.FPCTNNghe ?? 0,
                                 FPCTNVuotKhung = sub?.FPCTNVuotKhung ?? 0,
                                 FNghiOm = sub?.FNghiOm ?? 0,
                                 FHSBL = sub?.FHSBL ?? 0,
                                 FTongQuyTienLuongNam = sub?.FTongQuyTienLuongNam ?? 0,
                                 FThuBHXHNguoiLaoDong = ((bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_DU_TOAN || bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_HACH_TOAN) && sub?.FThuBHXHNguoiLaoDong.GetValueOrDefault() == 0)
                                                            ? ((sub?.FTongQuyTienLuongNam ?? 0) * bhMucLuc.FTyLeBHXHNLD) : (sub?.FThuBHXHNguoiLaoDong ?? 0),
                                 FThuBHXHNguoiSuDungLaoDong = ((bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_DU_TOAN || bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_HACH_TOAN) && sub?.FThuBHXHNguoiSuDungLaoDong.GetValueOrDefault() == 0)
                                                            ? ((sub?.FTongQuyTienLuongNam ?? 0) * bhMucLuc.FTyLeBHXHNSD) : sub?.FThuBHXHNguoiSuDungLaoDong ?? 0,
                                 FThuBHYTNguoiLaoDong = ((bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_DU_TOAN || bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_HACH_TOAN) && sub?.FThuBHYTNguoiLaoDong.GetValueOrDefault() == 0)
                                                            ? ((sub?.FTongQuyTienLuongNam ?? 0) * bhMucLuc.FTyLeBHYTNLD) : sub?.FThuBHYTNguoiLaoDong ?? 0,
                                 FThuBHYTNguoiSuDungLaoDong = ((bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_DU_TOAN || bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_HACH_TOAN) && sub?.FThuBHYTNguoiSuDungLaoDong.GetValueOrDefault() == 0)
                                                            ? ((sub?.FTongQuyTienLuongNam ?? 0) * bhMucLuc.FTyLeBHYTNSD) : sub?.FThuBHYTNguoiSuDungLaoDong ?? 0,
                                 FThuBHTNNguoiLaoDong = ((bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_DU_TOAN || bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_HACH_TOAN) && sub?.FThuBHTNNguoiLaoDong.GetValueOrDefault() == 0)
                                                            ? ((sub?.FTongQuyTienLuongNam ?? 0) * bhMucLuc.FTyLeBHTNNLD) : sub?.FThuBHTNNguoiLaoDong ?? 0,
                                 FThuBHTNNguoiSuDungLaoDong = ((bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_DU_TOAN || bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_HACH_TOAN) && sub?.FThuBHTNNguoiSuDungLaoDong.GetValueOrDefault() == 0)
                                                            ? ((sub?.FTongQuyTienLuongNam ?? 0) * bhMucLuc.FTyLeBHTNNSD) : sub?.FThuBHTNNguoiSuDungLaoDong ?? 0,
                                 FTongThuBHXH = sub?.FTongThuBHXH ?? 0,
                                 FTongThuBHYT = sub?.FTongThuBHYT ?? 0,
                                 FTongThuBHTN = sub?.FTongThuBHTN ?? 0,
                                 FTongCong = sub?.FTongCong ?? 0,
                                 IIdMaDonVi = sub?.IIdMaDonVi ?? searchCondition.MaDonVi,
                                 STenDonVi = sub?.STenDonVi ?? null,
                                 IQSBQNam = sub?.IQSBQNam ?? 0,
                                 SMaPhuCap = bhMucLuc.SMaPhuCap,
                                 SMaCapBac = bhMucLuc.SMaCB,
                                 DHeSoLCS = (decimal)(heSoLCS?.fGiaTri ?? 0),
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

        public TlDmPhuCap GetHeSoLCS(string heSoLCS)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var phuCapLCS = ctx.TlDmPhuCaps.FirstOrDefault(x => x.MaPhuCap == heSoLCS);
                return phuCapLCS;
            }
        }
        public IEnumerable<BhKhtBHXHChiTiet> FindKhtBHXHChiTiet(Guid khtBHXHId, int? namLamViec, string maDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter khtBHXHIdParam = new SqlParameter("@KhtBHXHId", khtBHXHId);
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter maDonViParam = new SqlParameter("@MaDonVi", maDonVi);
                return ctx.BhKhtBHXHChiTiets.FromSql("EXECUTE dbo.sp_kht_bhxh_chi_tiet @KhtBHXHId, @NamLamViec, @MaDonVi",
                    khtBHXHIdParam, namLamViecParam, maDonViParam).ToList();
            }
        }

        public Guid GetIdChungTuByDonVi(string maDonVi, int? namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhKhtBHXHs.Where(x => x.INamLamViec == namLamViec && x.IID_MaDonVi == maDonVi).Select(a => a.Id).FirstOrDefault();
            }
        }
        public List<BhDmMucLucNganSach> GetListBhMucLucNs(int? namLamViec)
        {

            using (var ctx = _contextFactory.CreateDbContext())
            {
                Func<BhDmMucLucNganSach, bool> defaultKhtBhxhMucLucFilter = x => x.INamLamViec == namLamViec && (x.SLNS == BhxhMLNS.KHOI_DU_TOAN || x.SLNS == BhxhMLNS.KHOI_HACH_TOAN) && x.ITrangThai == StatusType.ACTIVE;
                var NsMucLucsChild = ctx.BhDmMucLucNganSachs.AsNoTracking().AsEnumerable().Where(defaultKhtBhxhMucLucFilter).ToList();
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

        public bool ExistBHXHChiTiet(Guid bhxhId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhKhtBHXHChiTiets.Any(t => t.KhtBHXHId.Equals(bhxhId));
            }
        }

        public BhKhtBHXHChiTiet FindById(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhKhtBHXHChiTiets.Find(id);
            }
        }

        public IEnumerable<BhKhtBHXHChiTiet> FindByCondition(Expression<Func<BhKhtBHXHChiTiet, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhKhtBHXHChiTiets.Where(predicate).ToList();
            }
        }

        public IEnumerable<ReportKhtDuToanBHXHQuery> FindReportKhtDuToanBHXH(int namLamViec, int bloaiChungTu, bool bDaTongHop, string lstDonvi, string khoiDuToan, string khoiHachToan, int dvt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_kht_du_toan_thu_bhxh @namLamViec, @bTongHop, @daTongHop, @lstSelectedUnit, @khoiDuToan, @khoiHachToan, @dvt";
                var parameters = new[]
                {
                    new SqlParameter("@namLamViec", namLamViec),
                    new SqlParameter("@bTongHop", bloaiChungTu),
                    new SqlParameter("@daTongHop", bDaTongHop),
                    new SqlParameter("@lstSelectedUnit", lstDonvi),
                    new SqlParameter("@khoiDuToan", khoiDuToan),
                    new SqlParameter("@khoiHachToan", khoiHachToan),
                    new SqlParameter("@dvt", dvt)
                };
                return ctx.FromSqlRaw<ReportKhtDuToanBHXHQuery>(sql, parameters).ToList();
            }
        }
        public IEnumerable<ReportKhtDuToanBHXHQuery> FindReportKhtDuToanBHYT(int namLamViec, int bloaiChungTu, bool bDaTongHop, string lstDonvi, string khoiDuToan, string khoiHachToan, string sm, int dvt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_kht_du_toan_thu_bhyt @namLamViec, @bTongHop, @daTongHop, @lstSelectedUnit, @khoiDuToan, @khoiHachToan, @sm, @dvt";
                var parameters = new[]
                {
                    new SqlParameter("@namLamViec", namLamViec),
                    new SqlParameter("@bTongHop", bloaiChungTu),
                    new SqlParameter("@daTongHop", bDaTongHop),
                    new SqlParameter("@lstSelectedUnit", lstDonvi),
                    new SqlParameter("@khoiDuToan", khoiDuToan),
                    new SqlParameter("@khoiHachToan", khoiHachToan),
                    new SqlParameter("@sm", sm),
                    new SqlParameter("@dvt", dvt)
                };
                return ctx.FromSqlRaw<ReportKhtDuToanBHXHQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<ReportKhtDuToanBHXHQuery> FindReportKhtcTongHop(int namLamViec, string lstDonvi, int dvt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_tong_hop_du_toan_thu_chi @namLamViec, @lstSelectedUnit, @dvt";
                var parameters = new[]
                {
                    new SqlParameter("@namLamViec", namLamViec),
                    new SqlParameter("@lstSelectedUnit", lstDonvi),
                    new SqlParameter("@dvt", dvt)
                };
                return ctx.FromSqlRaw<ReportKhtDuToanBHXHQuery>(sql, parameters).ToList();
            }
        }
        public IEnumerable<BhKhtBHXHChiTietQuery> GetPlanSalary(int iNam, string sLuongChinh, string sPhuCapCV, string sPhuCapTNN, string sPhuCapTNVK, string lstIdChungTus)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bhxh_lay_luong_ke_hoach @NamLamViec, @LuongChinh, @PhuCapCV, @PhuCapTNN, @PhuCapTNVK, @lstIdChungTus";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", iNam),
                    new SqlParameter("@LuongChinh", sLuongChinh),
                    new SqlParameter("@PhuCapCV", sPhuCapCV),
                    new SqlParameter("@PhuCapTNN", sPhuCapTNN),
                    new SqlParameter("@PhuCapTNVK", sPhuCapTNVK),
                    new SqlParameter("@lstIdChungTus", lstIdChungTus)
                };
                return ctx.FromSqlRaw<BhKhtBHXHChiTietQuery>(sql, parameters).ToList();
            }
        }
        public IEnumerable<BhKhtBHXHChiTietQuery> GetQuanSoBinhQuan(int iNam, string sLuongKehoachId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bhxh_get_quan_so_binh_quan_nam @NamLamViec, @sLuongKehoachId";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", iNam),
                    new SqlParameter("@sLuongKehoachId", sLuongKehoachId)
                };
                return ctx.FromSqlRaw<BhKhtBHXHChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<BhKhtBHXHChiTietQuery> GetAggregatePlanData(int iNam, string sMaDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_get_data_kht_tong_hop @NamLamViec, @MaDonVi";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", iNam),
                    new SqlParameter("@MaDonVi", sMaDonVi)
                };
                return ctx.FromSqlRaw<BhKhtBHXHChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<BhKhtBHXHChiTietQuery> GetPlanData(int iNam, string sSoChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_get_data_kht @NamLamViec, @SoChungTu";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", iNam),
                    new SqlParameter("@SoChungTu", sSoChungTu)
                };
                return ctx.FromSqlRaw<BhKhtBHXHChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<BhKhtBHXHChiTietQuery> PrintVoucherDetailAggregate(int namLamViec, string maDonvis, int dvt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_kht_rpt_chi_tiet_tong_hop @NamLamViec, @IdDonVis, @Donvitinh";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@IdDonVis", maDonvis),
                    new SqlParameter("@Donvitinh", dvt)
                };
                return ctx.FromSqlRaw<BhKhtBHXHChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<BhKhtBHXHChiTietQuery> PrintVoucherDetailAggregateByUnits(int namLamViec, string maDonvis, bool isAggregate, int loaiChungTu, int dvt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql;
                //if (isAggregate)
                //    sql = "EXECUTE dbo.sp_kht_bhxh_chi_tiet_tong_hop_don_vi_da_tong_hop @NamLamViec, @MaDonVi, @LoaiChungTu, @DVT";
                //else
                //    sql = "EXECUTE dbo.sp_kht_bhxh_chi_tiet_tong_hop_don_vi @NamLamViec, @MaDonVi, @LoaiChungTu, @DVT";

                sql = "EXECUTE dbo.sp_kht_bhxh_chi_tiet_tong_hop_don_vi_da_tong_hop @NamLamViec, @MaDonVi, @LoaiChungTu, @DVT";


                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@MaDonVi", maDonvis),
                    new SqlParameter("@LoaiChungTu", loaiChungTu),
                    new SqlParameter("@DVT", dvt)
                };
                return ctx.FromSqlRaw<BhKhtBHXHChiTietQuery>(sql, parameters).ToList();
            }
        }
    }
}
