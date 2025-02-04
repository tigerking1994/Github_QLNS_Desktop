using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class QttmBHYTChiTietRepository : Repository<BhQttmBHYTChiTiet>, IQttmBHYTChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public QttmBHYTChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void AddAggregateVoucherDetail(BhQttmBHYTChiTietCriteria creation)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_qttm_bhyt_chungtu_chitiet_tao_tonghop @VoucherIds, @VoucherId, @YearOfWork, @UserName";
                var parameters = new[]
                {
                    new SqlParameter("@VoucherIds", creation.VoucherIds),
                    new SqlParameter("@VoucherId", creation.VoucherId),
                    new SqlParameter("@YearOfWork", creation.INamLamViec),
                    new SqlParameter("@UserName", creation.UserName)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public BhQttmBHYTChiTiet FindById(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQttmBHYTChiTiets.Find(id);
            }
        }

        public IEnumerable<BhQttmBHYTChiTiet> FindVoucherDetailByCondition(BhQttmBHYTChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                int? namLamViec = searchCondition.INamLamViec;
                string idDonVi = searchCondition.IIDMaDonVi;
                string sLNS = searchCondition.SLns;
                var dvt = searchCondition.DonViTinh;
                var loaiQuynam = searchCondition.IQuyNamLoai;
                var idChungTuDonVi = GetIdChungTuByDonVi(searchCondition.IIDMaDonVi, namLamViec);
                Guid voucherID = searchCondition.IsPrintReport ? idChungTuDonVi : searchCondition.VoucherID;
                Func<DonVi, bool> defaultNsDonViFilter = x => x.NamLamViec == namLamViec;
                var bhMucLucs = GetListBhMucLucNs(namLamViec, idDonVi);
                var chungChiTiets = FindVoucherDetail(voucherID, namLamViec, idDonVi).ToList();
                var duToanThuMuas = GetDuToanThuMuaBHYT(searchCondition.IsDonViCha, namLamViec, idDonVi);
                var dataQuyetToans = GetDataDaQuyetToan(voucherID, namLamViec, idDonVi, searchCondition.IQuyNamLoai);
                var dataQuys = GetChiTietChungTuThangQuy(namLamViec, idDonVi);

                var result = from bhMucLuc in bhMucLucs
                             join chungChiTiet in chungChiTiets on bhMucLuc.IIDMLNS equals chungChiTiet.IIDMLNS
                                  into gj
                             from sub in gj.DefaultIfEmpty()
                             join duToanThuMua in duToanThuMuas on bhMucLuc.IIDMLNS equals duToanThuMua.IIDMLNS into pbDuToanThuMua
                             from dttm in pbDuToanThuMua.DefaultIfEmpty()
                             join dataQuyetToan in dataQuyetToans on bhMucLuc.IIDMLNS equals dataQuyetToan.IIDMLNS into qt
                             from qttm in qt.DefaultIfEmpty()
                             join dataQuy in dataQuys on bhMucLuc.SXauNoiMa equals dataQuy.SXauNoiMa into dtq
                             from dataQuy in dtq.DefaultIfEmpty()
                             orderby bhMucLuc.SXauNoiMa

                             select new BhQttmBHYTChiTiet
                             {
                                 Id = sub == null ? Guid.NewGuid() : sub.Id == Guid.Empty ? Guid.NewGuid() : sub.Id,
                                 VoucherId = voucherID,
                                 IIDMLNS = bhMucLuc.IIDMLNS,
                                 IIDMLNSCha = bhMucLuc.IIDMLNSCha,
                                 STenBhMLNS = bhMucLuc.SMoTa,
                                 SXauNoiMa = bhMucLuc.SXauNoiMa,
                                 SLns = bhMucLuc.SLNS,
                                 SL = bhMucLuc.SL,
                                 SK = bhMucLuc.SK,
                                 SM = bhMucLuc.SM,
                                 STM = bhMucLuc.STM,
                                 STTM = bhMucLuc.STTM,
                                 SNG = bhMucLuc.SNG,
                                 STNG = bhMucLuc.STNG,
                                 IsAdd = sub == null,
                                 IsHangCha = bhMucLuc.BHangCha,
                                 FDuToan = dttm?.FDuToan ?? 0,
                                 FDaQuyetToan = qttm?.FDaQuyetToan ?? 0,
                                 FConLai = sub?.FConLai ?? 0,
                                 FSoPhaiThu = (loaiQuynam == (int)QuarterMonth.YEAR && dataQuy != null && (sub?.FSoPhaiThu ?? 0) == 0) ? dataQuy.FSoPhaiThu : sub?.FSoPhaiThu ?? 0,
                                 SGhiChu = sub?.SGhiChu ?? null,
                                 STenDonVi = sub?.STenDonVi ?? null,
                                 INamLamViec = sub?.INamLamViec ?? namLamViec,
                                 IIDMaDonVi = sub?.IIDMaDonVi ?? idDonVi,
                                 IsModified = (loaiQuynam == (int)QuarterMonth.YEAR && dataQuy != null && (sub?.FSoPhaiThu ?? 0) == 0),
                             };

                return result;
            }
        }

        public IEnumerable<BhQttmBHYTChiTietQuery> GetChiTietChungTuThangQuy(int? iNamLamViec, string sIdDonVis)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@INamLamViec", iNamLamViec);
                SqlParameter sIdDonViParam = new SqlParameter("@IdMaDonVi", sIdDonVis);
                return ctx.FromSqlRaw<BhQttmBHYTChiTietQuery>("EXECUTE dbo.sp_bhxh_qttm_get_data_quy " + "@INamLamViec, @IdMaDonVi", iNamLamViecParam, sIdDonViParam).ToList();
            }
        }

        public IEnumerable<BhQttmBHYTChiTiet> FindVoucherDetail(Guid chungTuId, int? namLamViec, string maDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter chungTuIdParam = new SqlParameter("@ChungTuId", chungTuId);
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter maDonViParam = new SqlParameter("@MaDonVi", maDonVi);
                return ctx.BhQttmBHYTChiTiets.FromSql("EXECUTE dbo.sp_bh_qttm_bhyt_chi_tiet @ChungTuId, @NamLamViec, @MaDonVi",
                    chungTuIdParam, namLamViecParam, maDonViParam).ToList();
            }
        }

        public List<BhDmMucLucNganSach> GetListBhMucLucNs(int? namLamViec, string idDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var iTrangThai = StatusType.ACTIVE;
                Func<BhDmMucLucNganSach, bool> defaultMucLucFilter = x => x.INamLamViec == namLamViec && x.SLNS.StartsWith(BhxhMLNS.THU_MUA_BHYT) && x.ITrangThai == iTrangThai;
                var nsDonVi = ctx.NsDonVis.Where(x => x.IIDMaDonVi == idDonVi && x.NamLamViec == namLamViec && x.ITrangThai == iTrangThai).FirstOrDefault();
                if (nsDonVi == null) return new List<BhDmMucLucNganSach>();
                List<BhDmMucLucNganSach> bhMucLucsChild = new List<BhDmMucLucNganSach>();
                bhMucLucsChild = ctx.BhDmMucLucNganSachs.AsNoTracking().AsEnumerable().Where(defaultMucLucFilter).ToList();

                List<BhDmMucLucNganSach> bhMucLucs = new List<BhDmMucLucNganSach>();
                if (bhMucLucsChild.Count > 0)
                {
                    var listIdMlskt = bhMucLucsChild.Select(item => item.IIDMLNS).ToList();
                    bhMucLucs = bhMucLucsChild;
                    while (true)
                    {
                        var test = bhMucLucsChild.Where(x => !listIdMlskt.Contains(x.IIDMLNSCha.GetValueOrDefault())).ToList();
                        var listIdParent = bhMucLucsChild.Where(x => !listIdMlskt.Contains(x.IIDMLNSCha.GetValueOrDefault())).Select(x => x.IIDMLNSCha).ToList();
                        var listParent1 = ctx.BhDmMucLucNganSachs.Where(x => listIdParent.Contains(x.IIDMLNS) && x.INamLamViec == namLamViec).ToList();
                        if (listParent1.Count > 0)
                        {
                            var lstId = listParent1.Select(item => item.IIDMLNS).ToList();
                            listIdMlskt.AddRange(lstId);
                            bhMucLucs.AddRange(listParent1);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                bhMucLucs = bhMucLucs.GroupBy(x => x.IIDMLNS).Select(x => x.First()).OrderBy(x => x.SXauNoiMa).ToList();
                bhMucLucs.RemoveRange(0, 2);
                return bhMucLucs;
            }
        }

        public IEnumerable<BhQttmBHYTChiTietQuery> GetDuToanThuMuaBHYT(bool isDonViCha, int? namLamViec, string maDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "";
                if (isDonViCha)
                {
                    sql = "EXECUTE dbo.sp_bh_qttm_get_tong_nhan_du_toan_thu_mua @NamLamViec, @MaDonVi";
                }
                else
                {
                    sql = "EXECUTE dbo.sp_bh_qttm_get_tong_du_toan_thu_mua @NamLamViec, @MaDonVi";
                }
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@MaDonVi", maDonVi)
                };
                return ctx.FromSqlRaw<BhQttmBHYTChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<BhQttmBHYTChiTietQuery> GetDataDaQuyetToan(Guid chungTuId, int? namLamViec, string maDonVi, int quyNamLoai)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter chungTuIdParam = new SqlParameter("@ChungTuId", chungTuId);
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter maDonViParam = new SqlParameter("@MaDonVi", maDonVi);
                string sql = "EXECUTE dbo.sp_bh_qttm_get_tong_da_quyet_toan @ChungTuId, @NamLamViec, @MaDonVi, @QuyNamLoai";
                var parameters = new[]
                {
                    new SqlParameter("@ChungTuId", chungTuId),
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@QuyNamLoai", quyNamLoai)
                };
                return ctx.FromSqlRaw<BhQttmBHYTChiTietQuery>(sql, parameters).ToList();
            }
        }

        public Guid GetIdChungTuByDonVi(string maDonVi, int? namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQttmBHYTs.Where(x => x.INamLamViec == namLamViec && x.IIDMaDonVi == maDonVi).Select(a => a.Id).FirstOrDefault();
            }
        }

        public IEnumerable<BhQttmBHYTChiTiet> FindVoucherDetailById(BhQttmBHYTChiTietCriteria searchCondition)
        {
            Guid voucherID = searchCondition.VoucherID;
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQttmBHYTChiTiets.Where(x => x.VoucherId == voucherID).ToList();
            }
        }

        public IEnumerable<string> GetLnsHasData(List<Guid> chungTuIds)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQttmBHYTChiTiets.Where(x => x.VoucherId.HasValue && chungTuIds.Contains(x.VoucherId.Value) && x.IsHasData).Select(x => x.SLns).Distinct().ToList();
            }
        }

        public bool ExistVoucherDetail(Guid voucherID)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQttmBHYTChiTiets.Any(t => t.VoucherId.Equals(voucherID));
            }
        }

        public IEnumerable<BhQttmBHYTChiTietQuery> ExportQuyetToanThuMuaBhytThanNhanNLD(int iNamLamViec, string sIdDonVi, int donViTinh, bool isTongHop, int selectedQuarter)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter sIdDonViParam = new SqlParameter("@IdDonVi", sIdDonVi);
                SqlParameter donViTinhParam = new SqlParameter("@Donvitinh", donViTinh);
                SqlParameter isTongHopParam = new SqlParameter("@IsTongHop", isTongHop);
                SqlParameter iQuy = new SqlParameter("@IQuy ", selectedQuarter);

                return ctx.FromSqlRaw<BhQttmBHYTChiTietQuery>("EXECUTE sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan_nld " +
                    "@NamLamViec, @IdDonVi, @Donvitinh,  @IsTongHop, @IQuy",
                    iNamLamViecParam, sIdDonViParam, donViTinhParam, isTongHopParam, iQuy).ToList();
            }
        }

        public IEnumerable<BhQttmBHYTChiTietQuery> ExportQuyetToanThuMuaBhytThanNhan(int namLamViec, bool isTongHop, string lstDonvi, string thanNhanQuanNhan, string thanNhanCNVQP, int dvt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_than_nhan @NamLamViec, @IsTongHop, @lstSelectedUnit, @thanNhanQuanNhan, @thanNhanCNVQP, @dvt";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@IsTongHop", isTongHop),
                    new SqlParameter("@LstSelectedUnit", lstDonvi),
                    new SqlParameter("@ThanNhanQuanNhan", thanNhanQuanNhan),
                    new SqlParameter("@ThanNhanCNVQP", thanNhanCNVQP),
                    new SqlParameter("@Dvt", dvt)
                };
                return ctx.FromSqlRaw<BhQttmBHYTChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<BhQttmBHYTChiTietQuery> ExportQuyetToanThuMuaBhytHSSV(int namLamViec, bool isTongHop, string lstDonvi, string hSSV, string luuHS, string hVSQ, string sQDB, int dvt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_qttm_rpt_quyet_toan_thu_mua_bhyt_hssv @NamLamViec, @IsTongHop, @LstSelectedUnit, @HSSV, @LuuHS, @HVSQ, @SQDuBi, @dvt";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@IsTongHop", isTongHop),
                    new SqlParameter("@LstSelectedUnit", lstDonvi),
                    new SqlParameter("@HSSV", hSSV),
                    new SqlParameter("@LuuHS", luuHS),
                    new SqlParameter("@HVSQ", hVSQ),
                    new SqlParameter("@SQDuBi", sQDB),
                    new SqlParameter("@Dvt", dvt)
                };
                return ctx.FromSqlRaw<BhQttmBHYTChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<BhQttmBHYTChiTietQuery> ExportQTTMBhytThanNhanNLDTongHop(int iNamLamViec, string sIdDonVi, int donViTinh, bool isTongHop, int selectedQuarter)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter sIdDonViParam = new SqlParameter("@IdDonVi", sIdDonVi);
                SqlParameter donViTinhParam = new SqlParameter("@Donvitinh", donViTinh);
                SqlParameter isTongHopParam = new SqlParameter("@IsTongHop", isTongHop);
                SqlParameter iQuy = new SqlParameter("@IQuy ", selectedQuarter);

                return ctx.FromSqlRaw<BhQttmBHYTChiTietQuery>("EXECUTE sp_bh_qttm_rpt_qttm_bhyt_than_nhan_nld_tong_hop " +
                    "@NamLamViec, @IdDonVi, @Donvitinh,  @IsTongHop, @IQuy",
                    iNamLamViecParam, sIdDonViParam, donViTinhParam, isTongHopParam, iQuy).ToList();
            }
        }

        public IEnumerable<BhQttmBHYTChiTietQuery> ExportQTTMBhytThanNhanNLDTongHopDonVi(int iNamLamViec, string sIdDonVis, int donViTinh, int selectedQuarter)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter sIdDonViParam = new SqlParameter("@IdDonVis", sIdDonVis);
                SqlParameter donViTinhParam = new SqlParameter("@Donvitinh", donViTinh);
                SqlParameter iQuy = new SqlParameter("@IQuy ", selectedQuarter);

                return ctx.FromSqlRaw<BhQttmBHYTChiTietQuery>("EXECUTE sp_bh_rpt_qttm_bhyt_than_nhan_nld_tong_hop_don_vi " +
                    "@NamLamViec, @IdDonVis, @Donvitinh, @IQuy",
                    iNamLamViecParam, sIdDonViParam, donViTinhParam, iQuy).ToList();
            }
        }

        public IEnumerable<BhQttmBHYTChiTietQuery> ExportQuyetToanThuMuaBhytThanNhanTongHop(int namLamViec, bool isTongHop, string lstDonvi, string thanNhanQuanNhan, string thanNhanCNVQP, int dvt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_qttm_rpt_bhyt_than_nhan_tong_hop @NamLamViec, @IsTongHop, @lstSelectedUnit, @thanNhanQuanNhan, @thanNhanCNVQP, @dvt";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@IsTongHop", isTongHop),
                    new SqlParameter("@LstSelectedUnit", lstDonvi),
                    new SqlParameter("@ThanNhanQuanNhan", thanNhanQuanNhan),
                    new SqlParameter("@ThanNhanCNVQP", thanNhanCNVQP),
                    new SqlParameter("@Dvt", dvt)
                };
                return ctx.FromSqlRaw<BhQttmBHYTChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<BhQttmBHYTChiTietQuery> ExportQuyetToanThuMuaBhytHSSVTongHop(int namLamViec, bool isTongHop, string lstDonvi, string hSSV, string luuHS, string hVSQ, string sQDB, int dvt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_rpt_quyet_toan_thu_mua_bhyt_hssv_tong_hop @NamLamViec, @IsTongHop, @LstSelectedUnit, @HSSV, @LuuHS, @HVSQ, @SQDuBi, @dvt";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@IsTongHop", isTongHop),
                    new SqlParameter("@LstSelectedUnit", lstDonvi),
                    new SqlParameter("@HSSV", hSSV),
                    new SqlParameter("@LuuHS", luuHS),
                    new SqlParameter("@HVSQ", hVSQ),
                    new SqlParameter("@SQDuBi", sQDB),
                    new SqlParameter("@Dvt", dvt)
                };
                return ctx.FromSqlRaw<BhQttmBHYTChiTietQuery>(sql, parameters).ToList();
            }
        }
    }
}
