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
    public class BhDcDuToanThuChiTietRepository : Repository<BhDcDuToanThuChiTiet>, IBhDcDuToanThuChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public BhDcDuToanThuChiTietRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhDcDuToanThuChiTiet> FindByConditionForChildUnit(BhDcDuToanThuChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                int namLamViec = searchCondition.NamLamViec;
                Guid iIdDcDuToanThu = searchCondition.BhDcDuToanThuId;
                string sLNS = searchCondition.LNS;
                //int iTrangThai = searchCondition.ITrangThai
                string idDonVi = searchCondition.IdDonVi;
                int? iIDTongHop = searchCondition.ILoaiTongHop;
                Func<DonVi, bool> defaultNsDonViFilter = x => x.NamLamViec == namLamViec;
                var nsDonVi = ctx.NsDonVis.Where(defaultNsDonViFilter)
                    .FirstOrDefault(x => x.IIDMaDonVi == idDonVi && x.NamLamViec == namLamViec);
                //var bhPhanboDTTChungTuChiTiets = ctx.BhDtPhanBoChungTuChiTiets.AsNoTracking().AsEnumerable().ToList()
                var bhMucLucs = GetListMucLucBhxhByLoaiChungtu(namLamViec, idDonVi, sLNS);
                var lstDcDuToanThuChiChiTiets = FindDTDieuChinhDuToanChiChiTiet(namLamViec, idDonVi).Where(x => x.IIDDttDieuChinh == iIdDcDuToanThu).ToList();
                var lstBhPbdttChiTiet = iIDTongHop.Equals(BhxhLoaiChungTu.BhxhChungTu) ? FindPbDttChiTiet(namLamViec, idDonVi, searchCondition.NgayChungTu ?? DateTime.Now).ToList() : FindNhanDttChiTiet(namLamViec, searchCondition.NgayChungTu ?? DateTime.Now).ToList();
                var lstDataQTT = GetDataQuyetToanThuBHXH(namLamViec, idDonVi);

                var result = from bhMucLuc in bhMucLucs
                             join dtcDcdToanChiChiTiet in lstDcDuToanThuChiChiTiets on bhMucLuc.IIDMLNS equals dtcDcdToanChiChiTiet.IIDMLNS
                                into gj
                             from sub in gj.DefaultIfEmpty()
                             join pbdtcBHXHChiTiet in lstBhPbdttChiTiet on bhMucLuc.IIDMLNS equals pbdtcBHXHChiTiet.IIdMlns
                                into pbd
                             from pbdt in pbd.DefaultIfEmpty()
                             join qttData in lstDataQTT on bhMucLuc.SXauNoiMa equals qttData.SXauNoiMa
                                into qt
                             from qtt in qt.DefaultIfEmpty()

                             orderby bhMucLuc.SXauNoiMa
                             select new BhDcDuToanThuChiTiet
                             {
                                 Id = sub == null ? Guid.NewGuid() : sub.Id == Guid.Empty ? Guid.NewGuid() : sub.Id,
                                 IIDDttDieuChinh = iIdDcDuToanThu,
                                 SNoiDung = bhMucLuc.SMoTa,
                                 SMoTa = bhMucLuc.SMoTa,
                                 SLNS = bhMucLuc.SLNS,
                                 SL = bhMucLuc.SL,
                                 SK = bhMucLuc.SK,
                                 SM = bhMucLuc.SM,
                                 STM = bhMucLuc.STM,
                                 STTM = bhMucLuc.STTM,
                                 SNG = bhMucLuc.SNG,
                                 STNG = bhMucLuc.STNG,
                                 Nganh = bhMucLuc.SNG,
                                 INamLamViec = namLamViec,
                                 IIdMaDonVi = idDonVi,
                                 IdParent = bhMucLuc.IIDMLNSCha,
                                 STenDonVi = sub == null ? string.Empty : nsDonVi?.TenDonVi,
                                 IIDMLNS = bhMucLuc.IIDMLNS,
                                 IsHangCha = bhMucLuc.BHangCha,
                                 IsAdd = sub == null,
                                 DNgayTao = null,
                                 SNguoiTao = null,
                                 DNgaySua = null,
                                 SNguoiSua = null,
                                 /*FThuBHXHNLD = iIDTongHop.Equals(BhxhLoaiChungTu.BhxhChungTu) ? (pbdt?.FBHXHNLD ?? 0) : (sub?.FThuBHXHNLD ?? 0),
                                 FThuBHXHNSD = iIDTongHop.Equals(BhxhLoaiChungTu.BhxhChungTu) ? (pbdt?.FBHXHNSD ?? 0) : (sub?.FThuBHXHNSD ?? 0),
                                 FThuBHYTNLD = iIDTongHop.Equals(BhxhLoaiChungTu.BhxhChungTu) ? (pbdt?.FBHYTNLD ?? 0) : (sub?.FThuBHYTNLD ?? 0),
                                 FThuBHYTNSD = iIDTongHop.Equals(BhxhLoaiChungTu.BhxhChungTu) ? (pbdt?.FBHYTNSD ?? 0) : (sub?.FThuBHYTNSD ?? 0),
                                 FThuBHTNNLD = iIDTongHop.Equals(BhxhLoaiChungTu.BhxhChungTu) ? (pbdt?.FBHTNNLD ?? 0) : (sub?.FThuBHTNNLD ?? 0),
                                 FThuBHTNNSD = iIDTongHop.Equals(BhxhLoaiChungTu.BhxhChungTu) ? (pbdt?.FBHTNNSD ?? 0) : (sub?.FThuBHTNNSD ?? 0),*/

                                 FThuBHXHNLD = (pbdt?.FBHXHNLD ?? 0),
                                 FThuBHXHNSD = (pbdt?.FBHXHNSD ?? 0),
                                 FThuBHYTNLD = (pbdt?.FBHYTNLD ?? 0),
                                 FThuBHYTNSD = (pbdt?.FBHYTNSD ?? 0),
                                 FThuBHTNNLD = (pbdt?.FBHTNNLD ?? 0),
                                 FThuBHTNNSD = (pbdt?.FBHTNNSD ?? 0),

                                 FThuBHXHNLDQTDauNam = sub?.FThuBHXHNLDQTDauNam ?? (qtt != null ? qtt.FThuBHXHNLDQTDauNam : 0),
                                 FThuBHXHNSDQTDauNam = sub?.FThuBHXHNSDQTDauNam ?? (qtt != null ? qtt.FThuBHXHNSDQTDauNam : 0),
                                 FThuBHYTNLDQTDauNam = sub?.FThuBHYTNLDQTDauNam ?? (qtt != null ? qtt.FThuBHYTNLDQTDauNam : 0),
                                 FThuBHYTNSDQTDauNam = sub?.FThuBHYTNSDQTDauNam ?? (qtt != null ? qtt.FThuBHYTNSDQTDauNam : 0),
                                 FThuBHTNNLDQTDauNam = sub?.FThuBHTNNLDQTDauNam ?? (qtt != null ? qtt.FThuBHTNNLDQTDauNam : 0),
                                 FThuBHTNNSDQTDauNam = sub?.FThuBHTNNSDQTDauNam ?? (qtt != null ? qtt.FThuBHTNNSDQTDauNam : 0),

                                 FThuBHXHNLDQTCuoiNam = sub?.FThuBHXHNLDQTCuoiNam ?? 0,
                                 FThuBHXHNSDQTCuoiNam = sub?.FThuBHXHNSDQTCuoiNam ?? 0,
                                 FThuBHYTNLDQTCuoiNam = sub?.FThuBHYTNLDQTCuoiNam ?? 0,
                                 FThuBHYTNSDQTCuoiNam = sub?.FThuBHYTNSDQTCuoiNam ?? 0,
                                 FThuBHTNNLDQTCuoiNam = sub?.FThuBHTNNLDQTCuoiNam ?? 0,
                                 FThuBHTNNSDQTCuoiNam = sub?.FThuBHTNNSDQTCuoiNam ?? 0,

                                 FTongThuBHXHNLD = sub?.FTongThuBHXHNLD ?? 0,
                                 FTongThuBHXHNSD = sub?.FTongThuBHXHNSD ?? 0,
                                 FTongThuBHYTNLD = sub?.FTongThuBHYTNLD ?? 0,
                                 FTongThuBHYTNSD = sub?.FTongThuBHYTNSD ?? 0,
                                 FTongThuBHTNNLD = sub?.FTongThuBHTNNLD ?? 0,
                                 FTongThuBHTNNSD = sub?.FTongThuBHTNNSD ?? 0,

                                 FThuBHXHNLDTang = sub?.FThuBHXHNLDTang ?? 0,
                                 FThuBHXHNSDTang = sub?.FThuBHXHNSDTang ?? 0,
                                 FThuBHXHTang = sub?.FThuBHXHTang ?? 0,
                                 FThuBHYTNLDTang = sub?.FThuBHYTNLDTang ?? 0,
                                 FThuBHYTNSDTang = sub?.FThuBHYTNSDTang ?? 0,
                                 FThuBHYTTang = sub?.FThuBHYTTang ?? 0,
                                 FThuBHTNNLDTang = sub?.FThuBHTNNLDTang ?? 0,
                                 FThuBHTNNSDTang = sub?.FThuBHTNNSDTang ?? 0,
                                 FThuBHTNTang = sub?.FThuBHTNTang ?? 0,

                                 FThuBHXHNLDGiam = sub?.FThuBHXHNLDGiam ?? 0,
                                 FThuBHXHNSDGiam = sub?.FThuBHXHNSDGiam ?? 0,
                                 FThuBHXHGiam = sub?.FThuBHXHGiam ?? 0,
                                 FThuBHYTNLDGiam = sub?.FThuBHYTNLDGiam ?? 0,
                                 FThuBHYTNSDGiam = sub?.FThuBHYTNSDGiam ?? 0,
                                 FThuBHYTGiam = sub?.FThuBHYTGiam ?? 0,
                                 FThuBHTNNLDGiam = sub?.FThuBHTNNLDGiam ?? 0,
                                 FThuBHTNNSDGiam = sub?.FThuBHTNNSDGiam ?? 0,
                                 FThuBHTNGiam = sub?.FThuBHTNGiam ?? 0,

                                 SGhiChu = sub == null ? string.Empty : sub.SGhiChu,
                                 SXauNoiMa = bhMucLuc.SXauNoiMa
                             };
                return result;
            }
        }
        private List<BhDmMucLucNganSach> GetListMucLucBhxhByLoaiChungtu(int namLamViec, string idDonVi, string SLNS)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var dataSLNS = SLNS.Split(',');
                var iTrangThai = StatusType.ACTIVE;
                Func<BhDmMucLucNganSach, bool> defaultSktMucLucFilter = x => x.INamLamViec == namLamViec;
                var nsDonVi = ctx.NsDonVis.Where(x => x.IIDMaDonVi == idDonVi && x.NamLamViec == namLamViec && x.ITrangThai == iTrangThai).FirstOrDefault();
                if (nsDonVi == null) return new List<BhDmMucLucNganSach>();
                List<BhDmMucLucNganSach> bhMucLucsChild = new List<BhDmMucLucNganSach>();
                bhMucLucsChild = ctx.BhDmMucLucNganSachs.AsNoTracking().AsEnumerable().Where(defaultSktMucLucFilter).Where(x => dataSLNS.Contains(x.SLNS)).ToList();

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
        private IEnumerable<BhDcDuToanThuChiTiet> FindDTDieuChinhDuToanChiChiTiet(int namLamViec, string idDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter idDonViParam = new SqlParameter("@IdDonVi", idDonVi);

                return ctx.Set<BhDcDuToanThuChiTiet>().FromSql("EXECUTE dbo.sp_bh_dieu_chinh_du_toan_thu_chi_tiet @NamLamViec, @IdDonVi",
                    namLamViecParam, idDonViParam).ToList();
            }
        }
        private IEnumerable<BhDtPhanBoChungTuChiTietQuery> FindPbDttChiTiet(int namLamViec, string idDonVi, DateTime? ngayChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter idDonViParam = new SqlParameter("@IdDonVi", idDonVi);
                SqlParameter ngayChungTuParam = new SqlParameter("@ngayChungTu", ngayChungTu);

                return ctx.FromSqlRaw<BhDtPhanBoChungTuChiTietQuery>("EXECUTE dbo.sp_bh_phan_bo_dtt_dieu_chinh_chi_tiet @NamLamViec, @IdDonVi,@ngayChungTu",
                    namLamViecParam, idDonViParam, ngayChungTuParam).ToList();
            }
        }

        private IEnumerable<BhDtPhanBoChungTuChiTietQuery> FindNhanDttChiTiet(int namLamViec, DateTime? ngayChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter ngayChungTuParam = new SqlParameter("@ngayChungTu", ngayChungTu);

                return ctx.FromSqlRaw<BhDtPhanBoChungTuChiTietQuery>("EXECUTE dbo.sp_bh_nhan_dtt_dieu_tiet @NamLamViec, @ngayChungTu",
                    namLamViecParam, ngayChungTuParam).ToList();
            }
        }

        public IEnumerable<BhDcDuToanThuChiTiet> FindByIdChiTiet(Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhDcDuToanThuChiTiets.Where(x => x.IIDDttDieuChinh == id).ToList();
            }
        }

        public void AddAggregate(BhDcDuToanThuChiTietCriteria creation)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter listIdChungTuTongHop = new SqlParameter("@ListIdChungTuTongHop", creation.ListIdChungTuTongHop);
                SqlParameter nguoiTao = new SqlParameter("@Nguoitao", creation.NguoiTao);
                SqlParameter idChungTu = new SqlParameter("@IdChungTu", creation.IdChungTu);
                SqlParameter namLamViec = new SqlParameter("@NamLamViec", creation.NamLamViec);
                SqlParameter maDonVi = new SqlParameter("@MaDonVi", creation.IdDonVi);
                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_dieu_chinh_dtt_tao_tonghop_chungtu_chitiet @ListIdChungTuTongHop,@Nguoitao ,@IdChungTu, @NamLamViec, @MaDonVi",
                    listIdChungTuTongHop, nguoiTao, idChungTu, namLamViec, maDonVi);
            }
        }

        public bool ExistKhcKcbChiTiet(Guid bhxhId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhDcDuToanThuChiTiets.Any(t => t.IIDDttDieuChinh.Equals(bhxhId));
            }
        }

        public IEnumerable<RptDcDuToanThuChiTietQuery> FindBhDcDttChiTietByCondition(BhDcDuToanThuChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter chungTuId = new SqlParameter("@ChungTuId", searchCondition.BhDcDuToanThuId);
                SqlParameter donViTinh = new SqlParameter("@DonViTinh", searchCondition.DonViTinh);
                SqlParameter namLamViec = new SqlParameter("@NamLamViec", searchCondition.NamLamViec);
                return ctx.FromSqlRaw<RptDcDuToanThuChiTietQuery>("EXECUTE dbo.sp_rpt_dieu_chinh_du_toan_thu_bhxh @ChungTuId, @DonViTinh, @NamLamViec", chungTuId, donViTinh, namLamViec).ToList();
            }
        }

        public IEnumerable<RptDcDuToanThuChiTietQuery> FindBhDcDttChiTietByUnit(string maDonVi, int dvt, int namLamViec, bool isAggregate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string lstMaDonvi = "";
                string sql = "EXECUTE dbo.sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit @MaDonVi, @DonViTinh, @NamLamViec";

                if (!isAggregate)
                {
                    var countDuToan = CountEstimateData(maDonVi, namLamViec);
                    if (countDuToan.ICountRow > 0)
                    {
                        sql = "EXECUTE dbo.sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit @MaDonVi, @DonViTinh, @NamLamViec";
                    }
                    else
                    {
                        sql = "EXECUTE dbo.sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_qt @MaDonVi, @DonViTinh, @NamLamViec";
                    }
                }
                else
                {
                    var soChungTus = ctx.BhDcDuToanThus.FirstOrDefault(x => x.INamLamViec == namLamViec && x.ILoaiTongHop == 2 && x.IIDMaDonVi == maDonVi).STongHop;
                    lstMaDonvi = string.Join(",", ctx.BhDcDuToanThus.Where(x => soChungTus.Contains(x.SSoChungTu)).Select(x => x.IIDMaDonVi).ToList());
                    var countDuToan = CountEstimateReceiveData(maDonVi, namLamViec);
                    if (countDuToan.ICountRow > 0)
                    {
                        sql = "EXECUTE dbo.sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_tonghop @MaDonVi, @DonViTinh, @NamLamViec, @LstMaDonVi";
                    }
                    else
                    {
                        sql = "EXECUTE dbo.sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit_tonghop_qt @MaDonVi, @DonViTinh, @NamLamViec, @LstMaDonVi";
                    }
                }

                SqlParameter chungTuIdParam = new SqlParameter("@MaDonVi", maDonVi);
                SqlParameter donViTinhParam = new SqlParameter("@DonViTinh", dvt);
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter lstMaDonViParam = new SqlParameter("@LstMaDonVi", lstMaDonvi);
                return ctx.FromSqlRaw<RptDcDuToanThuChiTietQuery>(sql, chungTuIdParam, donViTinhParam, namLamViecParam, lstMaDonViParam).ToList();
            }
        }

        private IEnumerable<BhDcDuToanThuChiTietQuery> GetDataQuyetToanThuBHXH(int namLamViec, string idDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter idDonViParam = new SqlParameter("@IdDonVi", idDonVi);
                return ctx.FromSqlRaw<BhDcDuToanThuChiTietQuery>("EXECUTE dbo.sp_bh_dcdtt_chi_tiet_lay_quyet_toan_thu_bhxh @NamLamViec, @IdDonVi",
                    namLamViecParam, idDonViParam).ToList();
            }
        }

        public IEnumerable<BhDcDuToanThuChiTietQuery> GetAggregateAdjustData(int iNam, string sMaDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_get_data_dieu_chinh_dtt_tong_hop @NamLamViec, @MaDonVi";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", iNam),
                    new SqlParameter("@MaDonVi", sMaDonVi)
                };
                return ctx.FromSqlRaw<BhDcDuToanThuChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<BhDcDuToanThuChiTietQuery> GetUnitAggregateAdjustData(int iNam, string sMaDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_get_data_dieu_chinh_dtt_don_vi_tong_hop @NamLamViec, @MaDonVi";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", iNam),
                    new SqlParameter("@MaDonVi", sMaDonVi)
                };
                return ctx.FromSqlRaw<BhDcDuToanThuChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<RptDcDuToanThuChiTietQuery> ExportDieuChinhDtTheoDoiTuong(string maDonVi, int dvt, int namLamViec, bool isAggregate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string lstMaDonvi = "";
                string sql = "EXECUTE dbo.sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong @MaDonVi, @DonViTinh, @NamLamViec";

                if (!isAggregate)
                {
                    var countDuToan = CountEstimateData(maDonVi, namLamViec);
                    if (countDuToan.ICountRow > 0)
                    {
                        sql = "EXECUTE dbo.sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong @MaDonVi, @DonViTinh, @NamLamViec";
                    }
                    else
                    {
                        sql = "EXECUTE dbo.sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong_qt @MaDonVi, @DonViTinh, @NamLamViec";
                    }
                }
                else
                {
                    var soChungTus = ctx.BhDcDuToanThus.FirstOrDefault(x => x.INamLamViec == namLamViec && x.ILoaiTongHop == 2 && x.IIDMaDonVi == maDonVi).STongHop;
                    lstMaDonvi = string.Join(",", ctx.BhDcDuToanThus.Where(x => soChungTus.Contains(x.SSoChungTu)).Select(x => x.IIDMaDonVi).ToList());
                    var countDuToan = CountEstimateReceiveData(maDonVi, namLamViec);
                    if (countDuToan.ICountRow > 0)
                    {
                        sql = "EXECUTE dbo.sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong_tonghop @MaDonVi, @DonViTinh, @NamLamViec";
                    }
                    else
                    {
                        sql = "EXECUTE dbo.sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong_tonghop_qt @MaDonVi, @DonViTinh, @NamLamViec, @LstMaDonVi";
                    }
                }
                SqlParameter chungTuIdPara = new SqlParameter("@MaDonVi", maDonVi);
                SqlParameter donViTinhPara = new SqlParameter("@DonViTinh", dvt);
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter lstMaDonViParam = new SqlParameter("@LstMaDonVi", lstMaDonvi);
                return ctx.FromSqlRaw<RptDcDuToanThuChiTietQuery>(sql, chungTuIdPara, donViTinhPara, namLamViecParam, lstMaDonViParam).ToList();
            }
        }

        public IEnumerable<RptDcDuToanThuChiTietQuery> FindBhDcDttChiTietByAgencySummaryDetail(string maDonVi, int dvt, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_dieu_chinh_du_toan_thu_bhxh_thctdv @MaDonVi, @DonViTinh, @NamLamViec";

                var countDuToan = CountEstimateData(maDonVi, namLamViec);
                if (countDuToan.ICountRow > 0)
                {
                    sql = "EXECUTE dbo.sp_rpt_dieu_chinh_du_toan_thu_bhxh_thctdv @MaDonVi, @DonViTinh, @NamLamViec";
                }
                else
                {
                    sql = "EXECUTE dbo.sp_rpt_dieu_chinh_du_toan_thu_bhxh_thctdv_qt @MaDonVi, @DonViTinh, @NamLamViec";
                }

                SqlParameter chungTuIdPara = new SqlParameter("@MaDonVi", maDonVi);
                SqlParameter donViTinhPara = new SqlParameter("@DonViTinh", dvt);
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                return ctx.FromSqlRaw<RptDcDuToanThuChiTietQuery>(sql, chungTuIdPara, donViTinhPara, namLamViecParam).ToList();
            }
        }

        public BhDcDuToanThuChiTietQuery CountEstimateData(string maDonVi, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter chungTuIdPara = new SqlParameter("@MaDonVi", maDonVi);
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                return ctx.FromSqlRaw<BhDcDuToanThuChiTietQuery>("EXECUTE dbo.sp_rpt_dieu_chinh_du_toan_thu_count_du_toan @MaDonVi, @NamLamViec"
                    , chungTuIdPara, namLamViecParam).FirstOrDefault();
            }
        }

        public BhDcDuToanThuChiTietQuery CountEstimateReceiveData(string maDonVi, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter chungTuIdPara = new SqlParameter("@MaDonVi", maDonVi);
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                return ctx.FromSqlRaw<BhDcDuToanThuChiTietQuery>("EXECUTE dbo.sp_rpt_dieu_chinh_du_toan_thu_count_nhan_du_toan @MaDonVi, @NamLamViec"
                    , chungTuIdPara, namLamViecParam).FirstOrDefault();
            }
        }

        public IEnumerable<BhDcDuToanThuChiTietQuery> GetSettlementData(int namLamViec, string maDonVi, int thangQuy, int loaiThangQuy)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter maDonViParam = new SqlParameter("@MaDonVi", maDonVi);
                SqlParameter thangQuyParam = new SqlParameter("@ThangQuy", thangQuy);
                SqlParameter loaiThangQuyParam = new SqlParameter("@LoaiThangQuy", loaiThangQuy);
                return ctx.FromSqlRaw<BhDcDuToanThuChiTietQuery>("EXECUTE dbo.sp_bh_dctt_get_data_quyet_toan @NamLamViec, @MaDonVi, @ThangQuy, @LoaiThangQuy"
                    , namLamViecParam, maDonViParam, thangQuyParam, loaiThangQuyParam).ToList();
            }
        }
    }
}
