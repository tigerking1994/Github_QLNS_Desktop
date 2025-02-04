using log4net;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class QttBHXHChiTietRepository : Repository<BhQttBHXHChiTiet>, IQttBHXHChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        private ILog _logger;

        public QttBHXHChiTietRepository(ApplicationDbContextFactory contextFactory, ILog log)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
            _logger = log;
        }

        public void AddAggregateVoucherDetail(BhQttBHXHChiTietCriteria creation)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_qtt_bhxh_chungtu_chitiet_tao_tonghop @VoucherIds, @VoucherId, @YearOfWork, @UserName, @MaDonVi";
                var parameters = new[]
                {
                    new SqlParameter("@VoucherIds", creation.VoucherIds),
                    new SqlParameter("@VoucherId", creation.VoucherId),
                    new SqlParameter("@YearOfWork", creation.INamLamViec),
                    new SqlParameter("@UserName", creation.UserName),
                    new SqlParameter("@MaDonVi", creation.IIDMaDonVi)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public BhQttBHXHChiTiet FindById(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQttBHXHChiTiets.Find(id);
            }
        }

        public IEnumerable<BhQttBHXHChiTiet> FindVoucherDetailByCondition(BhQttBHXHChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                int? namLamViec = searchCondition.INamLamViec;
                var idDonVi = searchCondition.IIDMaDonVi;
                var idDonViCha = searchCondition.IIDMaDonViCha;
                var dvt = searchCondition.DonViTinh;
                var loaiQuynam = searchCondition.IQuyNamLoai;
                var idChungTuDonVi = GetIdChungTuByDonVi(searchCondition.IIDMaDonVi, namLamViec);
                var voucherIDs = GetListIdChungTuByDonVi(searchCondition.IIDMaDonVi, namLamViec);
                Guid voucherID = searchCondition.IsPrintReport ? idChungTuDonVi : searchCondition.IdQttBhxh;
                Func<DonVi, bool> defaultNsDonViFilter = x => x.NamLamViec == namLamViec;
                var bhMucLucs = GetListBhMucLucNs(namLamViec, idDonVi);
                var chungChiTiets = FindVoucherDetail(voucherID, namLamViec, idDonVi).ToList();
                var heSoLCS = GetThamSoLCS(namLamViec) ?? 0;
                //var chungChiTiets = searchCondition.IsNam ? FindVoucherDetailNam(voucherIDs, namLamViec, idDonVi).ToList() : FindVoucherDetail(voucherID, namLamViec, idDonVi).ToList();
                List<BhQttBHXHChiTietQuery> dataQuyThangs = new List<BhQttBHXHChiTietQuery>();
                if (!searchCondition.IsDonViCha && searchCondition.ILoai == null)
                {
                    dataQuyThangs = GetChiTietChungTuThangQuy(namLamViec, idDonVi).Where(x => x.ILoai == null).ToList();
                }
                else if (!searchCondition.IsDonViCha && searchCondition.ILoai == 1)
                {
                    dataQuyThangs = GetChiTietChungTuThangQuyDonViCha(namLamViec, idDonVi, idDonViCha).ToList();
                }
                else if (searchCondition.IsDonViCha && searchCondition.ILoai == 2)
                {
                    dataQuyThangs = GetChiTietChungTuThangQuy(namLamViec, idDonVi).Where(x => x.ILoai == 2).ToList();
                }
                
                var dataDTTs = GetDataDTT(searchCondition.IsDonViCha, namLamViec, idDonVi);
                var dataQuyetToans = GetDataDaQuyetToan(voucherID, namLamViec, idDonVi, searchCondition.IQuyNamLoai);

                var result = from bhMucLuc in bhMucLucs
                             join chungChiTiet in chungChiTiets on bhMucLuc.SXauNoiMa equals chungChiTiet.SXauNoiMa
                                  into gj
                             from sub in gj.DefaultIfEmpty()
                             join phanBoDTT in dataDTTs on bhMucLuc.SXauNoiMa equals phanBoDTT.SXauNoiMa into pb
                             from dtt in pb.DefaultIfEmpty()
                             join dataQuyetToan in dataQuyetToans on bhMucLuc.SXauNoiMa equals dataQuyetToan.SXauNoiMa into qt
                             from qtt in qt.DefaultIfEmpty()
                             join dataQuyThang in dataQuyThangs on bhMucLuc.SXauNoiMa equals dataQuyThang.SXauNoiMa into dtqt
                             from dtQuyThang in dtqt.DefaultIfEmpty()
                             orderby bhMucLuc.SXauNoiMa

                             select new BhQttBHXHChiTiet
                             {
                                 Id = sub == null ? Guid.NewGuid() : sub.Id == Guid.Empty ? Guid.NewGuid() : sub.Id,
                                 QttBHXHId = voucherID,
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
                                 SNsLuongChinh = bhMucLuc.SNS_LuongChinh,
                                 SNsPCCV = bhMucLuc.SNS_PCCV,
                                 SNsPCTN = bhMucLuc.SNS_PCTN,
                                 SNsPCTNVK = bhMucLuc.SNS_PCTNVK,
                                 SNsHSBL = bhMucLuc.SNS_HSBL,
                                 FTyLeBHXHNLD = bhMucLuc.FTyLeBHXHNLD,
                                 FTyLeBHXHNSD = bhMucLuc.FTyLeBHXHNSD,
                                 FTyLeBHYTNLD = bhMucLuc.FTyLeBHYTNLD,
                                 FTyLeBHYTNSD = bhMucLuc.FTyLeBHYTNSD,
                                 FTyLeBHTNNLD = bhMucLuc.FTyLeBHTNNLD,
                                 FTyLeBHTNNSD = bhMucLuc.FTyLeBHTNNSD,
                                 FHeSoLayQuyLuong = bhMucLuc.FHeSoLayQuyLuong,
                                 IsAdd = sub == null,
                                 IsHangCha = bhMucLuc.BHangCha,
                                 SMaCapBac = bhMucLuc.SMaCB,

                                 IQSBQNam = (loaiQuynam == (int)QuarterMonth.YEAR && dtQuyThang != null && (sub?.IQSBQNam ?? 0) == 0) ? dtQuyThang.IQSBQNam : sub?.IQSBQNam ?? 0,
                                 FLuongChinh = (loaiQuynam == (int)QuarterMonth.YEAR && dtQuyThang != null && (sub?.FLuongChinh ?? 0) == 0) ? dtQuyThang.FLuongChinh : sub?.FLuongChinh ?? 0,
                                 FPhuCapChucVu = (loaiQuynam == (int)QuarterMonth.YEAR && dtQuyThang != null && (sub?.FPhuCapChucVu ?? 0) == 0) ? dtQuyThang.FPhuCapChucVu : sub?.FPhuCapChucVu ?? 0,
                                 FPCTNNghe = (loaiQuynam == (int)QuarterMonth.YEAR && dtQuyThang != null && (sub?.FPCTNNghe ?? 0) == 0) ? dtQuyThang.FPCTNNghe : sub?.FPCTNNghe ?? 0,
                                 FPCTNVuotKhung = (loaiQuynam == (int)QuarterMonth.YEAR && dtQuyThang != null && (sub?.FPCTNVuotKhung ?? 0) == 0) ? dtQuyThang.FPCTNVuotKhung : sub?.FPCTNVuotKhung ?? 0,
                                 FNghiOm = (loaiQuynam == (int)QuarterMonth.YEAR && dtQuyThang != null && (sub?.FNghiOm ?? 0) == 0) ? dtQuyThang.FNghiOm : sub?.FNghiOm ?? 0,
                                 FHSBL = (loaiQuynam == (int)QuarterMonth.YEAR && dtQuyThang != null && (sub?.FHSBL ?? 0) == 0) ? dtQuyThang.FHSBL : sub?.FHSBL ?? 0,
                                 FTongQuyTienLuongNam = (loaiQuynam == (int)QuarterMonth.YEAR && dtQuyThang != null && (sub?.FTongQuyTienLuongNam ?? 0) == 0) ? dtQuyThang.FTongQuyTienLuongNam : sub?.FTongQuyTienLuongNam ?? 0,

                                 FDuToan = dtt?.FTongCong ?? 0,
                                 FDaQuyetToan = qtt?.FDaQuyetToan ?? 0,
                                 FConLai = sub?.FConLai ?? 0,
                                 FThuBHXHNLD = (bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_DU_TOAN || bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_HACH_TOAN)
                                                    ? (bhMucLuc.FTyLeBHXHNLD * (sub?.IQSBQNam ?? 0) * (sub?.LCS ?? 0)) 
                                                    : (sub?.FThuBHXHNLD ?? 0),
                                 FThuBHXHNSD = (bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_DU_TOAN || bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_HACH_TOAN)
                                                    ? (bhMucLuc.FTyLeBHXHNSD * (sub?.IQSBQNam ?? 0) * (sub?.LCS ?? 0))
                                                    : (sub?.FThuBHXHNSD ?? 0),                               
                                 FTongSoPhaiThuBHXH = (bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_DU_TOAN || bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_HACH_TOAN) 
                                                    ? ((bhMucLuc.FTyLeBHXHNLD + bhMucLuc.FTyLeBHXHNSD) * (sub?.IQSBQNam ?? 0) * (sub?.LCS ?? 0))
                                                    : (sub?.FTongSoPhaiThuBHXH ?? 0),
                                 FThuBHYTNLD = (bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_DU_TOAN || bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_HACH_TOAN)
                                                    ? (bhMucLuc.FTyLeBHYTNLD * (sub?.IQSBQNam ?? 0) * (sub?.LCS ?? 0))
                                                    : (sub?.FThuBHYTNLD ?? 0),
                                 FThuBHYTNSD = (bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_DU_TOAN || bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_HACH_TOAN)
                                                    ? (bhMucLuc.FTyLeBHYTNSD * (sub?.IQSBQNam ?? 0) * (sub?.LCS ?? 0))
                                                    : (sub?.FThuBHYTNSD ?? 0),
                                 FTongSoPhaiThuBHYT = (bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_DU_TOAN || bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_HACH_TOAN)
                                                    ? ((bhMucLuc.FTyLeBHYTNLD + bhMucLuc.FTyLeBHYTNSD) * (sub?.IQSBQNam ?? 0) * (sub?.LCS ?? 0))
                                                    : (sub?.FTongSoPhaiThuBHYT ?? 0),
                                 FThuBHTNNLD = (bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_DU_TOAN || bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_HACH_TOAN)
                                                    ? (bhMucLuc.FTyLeBHTNNLD * (sub?.IQSBQNam ?? 0) * (sub?.LCS ?? 0))
                                                    : (sub?.FThuBHTNNLD ?? 0),
                                 FThuBHTNNSD = (bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_DU_TOAN || bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_HACH_TOAN)
                                                    ? (bhMucLuc.FTyLeBHTNNSD * (sub?.IQSBQNam ?? 0) * (sub?.LCS ?? 0))
                                                    : (sub?.FThuBHTNNSD ?? 0),
                                 FTongSoPhaiThuBHTN = (bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_DU_TOAN || bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_HACH_TOAN)
                                                    ? ((bhMucLuc.FTyLeBHTNNLD + bhMucLuc.FTyLeBHTNNSD) * (sub?.IQSBQNam ?? 0) * (sub?.LCS ?? 0))
                                                    : (sub?.FTongSoPhaiThuBHTN ?? 0),
                                 FTongCong = sub?.FTongCong ?? 0,
                                 SGhiChu = sub?.SGhiChu ?? null,
                                 STenDonVi = sub?.STenDonVi ?? null,
                                 INamLamViec = sub?.INamLamViec ?? namLamViec,
                                 IIDMaDonVi = sub?.IIDMaDonVi ?? idDonVi,
                                 LCS = sub?.LCS ?? heSoLCS,
                                 IsModified = (loaiQuynam == (int)QuarterMonth.YEAR && dtQuyThang != null && (sub?.FTongQuyTienLuongNam ?? 0) == 0),
                             };

                return result;
            }
        }
        public IEnumerable<BhQttBHXHChiTiet> FindVoucherDetailsByCondition(BhQttBHXHChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                int? namLamViec = searchCondition.INamLamViec;
                string idDonVi = searchCondition.IIDMaDonVi;
                int? selectedQuarter = searchCondition.IQuyNam;
                int selectedQuarterType = searchCondition.IQuyNamLoai;
                var dvt = searchCondition.DonViTinh;
                
                List<BhQttBHXH> voucherIDs = new List<BhQttBHXH>();
                if (selectedQuarter == 3 && selectedQuarterType == 1)
                {
                    int[] sMonths = { 1, 2, 3 };
                    voucherIDs = ctx.BhQttBHXHs.Where(x => x.INamLamViec == namLamViec && ((x.IQuyNam == selectedQuarter && selectedQuarterType == 0)
                        || (sMonths.Contains(x.IQuyNam) && selectedQuarterType == 1))).ToList();
                }
                else if (selectedQuarter == 6 && selectedQuarterType == 1)
                {
                    int[] sMonths = { 4, 5, 6 };
                    voucherIDs = ctx.BhQttBHXHs.Where(x => x.INamLamViec == namLamViec && ((x.IQuyNam == selectedQuarter && selectedQuarterType == 0) 
                        || (sMonths.Contains(x.IQuyNam) && selectedQuarterType == 1))).ToList();
                } 
                else if (selectedQuarter == 9 && selectedQuarterType == 1)
                {
                    int[] sMonths = { 7, 8, 9 };
                    voucherIDs = ctx.BhQttBHXHs.Where(x => x.INamLamViec == namLamViec && ((x.IQuyNam == selectedQuarter && selectedQuarterType == 0)
                        || (sMonths.Contains(x.IQuyNam) && selectedQuarterType == 1))).ToList();
                }
                else if (selectedQuarter == 12 && selectedQuarterType == 1)
                {
                    int[] sMonths = { 10, 11, 12 };
                    voucherIDs = ctx.BhQttBHXHs.Where(x => x.INamLamViec == namLamViec && ((x.IQuyNam == selectedQuarter && selectedQuarterType == 0)
                        || (sMonths.Contains(x.IQuyNam) && selectedQuarterType == 1))).ToList();
                }
                else
                {
                    voucherIDs = ctx.BhQttBHXHs.Where(x => x.INamLamViec == namLamViec && (x.IQuyNam == selectedQuarter && selectedQuarterType == 0)).ToList();
                }    

                Func<DonVi, bool> defaultNsDonViFilter = x => x.NamLamViec == namLamViec;
                var bhMucLucs = GetListBhMucLucNs(namLamViec, idDonVi);
                var chungChiTiets = FindVoucherDetails(namLamViec, idDonVi).Where(x => x.IIDMaDonVi == idDonVi && voucherIDs.Any(t => t.Id == x.QttBHXHId)).ToList();
                var heSoLCS = GetThamSoLCS(namLamViec) ?? 0;

                var dataDTTs = GetDataDTT(searchCondition.IsDonViCha, namLamViec, idDonVi);
                var dataQuyetToans = GetDatasDaQuyetToan(namLamViec, idDonVi, selectedQuarter, selectedQuarterType);
                var id = Guid.NewGuid();

                var result = from bhMucLuc in bhMucLucs
                             join chungChiTiet in chungChiTiets on bhMucLuc.IIDMLNS equals chungChiTiet.IIDMLNS
                                  into gj
                             from sub in gj.DefaultIfEmpty()
                             join phanBoDTT in dataDTTs on bhMucLuc.IIDMLNS equals phanBoDTT.IIdMlns into pb
                             from dtt in pb.DefaultIfEmpty()
                             join dataQuyetToan in dataQuyetToans on bhMucLuc.IIDMLNS equals dataQuyetToan.IIDMLNS into qt
                             from qtt in qt.DefaultIfEmpty()
                             orderby bhMucLuc.SXauNoiMa

                             select new BhQttBHXHChiTiet
                             {
                                 Id = sub == null ? Guid.NewGuid() : sub.Id == Guid.Empty ? Guid.NewGuid() : sub.Id,
                                 QttBHXHId = id,
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
                                 FTyLeBHXHNLD = bhMucLuc.FTyLeBHXHNLD,
                                 FTyLeBHXHNSD = bhMucLuc.FTyLeBHXHNSD,
                                 FTyLeBHYTNLD = bhMucLuc.FTyLeBHYTNLD,
                                 FTyLeBHYTNSD = bhMucLuc.FTyLeBHYTNSD,
                                 FTyLeBHTNNLD = bhMucLuc.FTyLeBHTNNLD,
                                 FTyLeBHTNNSD = bhMucLuc.FTyLeBHTNNSD,
                                 IsAdd = sub == null,
                                 IsHangCha = bhMucLuc.BHangCha,
                                 SMaCapBac = bhMucLuc.SMaCB,

                                 IQSBQNam = sub?.IQSBQNam ?? 0,
                                 FLuongChinh = sub?.FLuongChinh ?? 0,
                                 FPhuCapChucVu = sub?.FPhuCapChucVu ?? 0,
                                 FPCTNNghe = sub?.FPCTNNghe ?? 0,
                                 FPCTNVuotKhung = sub?.FPCTNVuotKhung ?? 0,
                                 FNghiOm = sub?.FNghiOm ?? 0,
                                 FHSBL = sub?.FHSBL ?? 0,
                                 FTongQuyTienLuongNam = sub?.FTongQuyTienLuongNam ?? 0,

                                 FDuToan = dtt?.FTongCong ?? 0,
                                 FDaQuyetToan = qtt?.FDaQuyetToan ?? 0,
                                 FConLai = sub?.FConLai ?? 0,
                                 FThuBHXHNLD = (bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_DU_TOAN || bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_HACH_TOAN)
                                                    ? (bhMucLuc.FTyLeBHXHNLD * (sub?.IQSBQNam ?? 0) * (sub?.LCS ?? 0))
                                                    : (sub?.FThuBHXHNLD ?? 0),
                                 FThuBHXHNSD = (bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_DU_TOAN || bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_HACH_TOAN)
                                                    ? (bhMucLuc.FTyLeBHXHNSD * (sub?.IQSBQNam ?? 0) * (sub?.LCS ?? 0))
                                                    : (sub?.FThuBHXHNSD ?? 0),
                                 FTongSoPhaiThuBHXH = (bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_DU_TOAN || bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_HACH_TOAN)
                                                    ? ((bhMucLuc.FTyLeBHXHNLD + bhMucLuc.FTyLeBHXHNSD) * (sub?.IQSBQNam ?? 0) * (sub?.LCS ?? 0))
                                                    : (sub?.FTongSoPhaiThuBHXH ?? 0),
                                 FThuBHYTNLD = (bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_DU_TOAN || bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_HACH_TOAN)
                                                    ? (bhMucLuc.FTyLeBHYTNLD * (sub?.IQSBQNam ?? 0) * (sub?.LCS ?? 0))
                                                    : (sub?.FThuBHYTNLD ?? 0),
                                 FThuBHYTNSD = (bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_DU_TOAN || bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_HACH_TOAN)
                                                    ? (bhMucLuc.FTyLeBHYTNSD * (sub?.IQSBQNam ?? 0) * (sub?.LCS ?? 0))
                                                    : (sub?.FThuBHYTNSD ?? 0),
                                 FTongSoPhaiThuBHYT = (bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_DU_TOAN || bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_HACH_TOAN)
                                                    ? ((bhMucLuc.FTyLeBHYTNLD + bhMucLuc.FTyLeBHYTNSD) * (sub?.IQSBQNam ?? 0) * (sub?.LCS ?? 0))
                                                    : (sub?.FTongSoPhaiThuBHYT ?? 0),
                                 FThuBHTNNLD = (bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_DU_TOAN || bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_HACH_TOAN)
                                                    ? (bhMucLuc.FTyLeBHTNNLD * (sub?.IQSBQNam ?? 0) * (sub?.LCS ?? 0))
                                                    : (sub?.FThuBHTNNLD ?? 0),
                                 FThuBHTNNSD = (bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_DU_TOAN || bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_HACH_TOAN)
                                                    ? (bhMucLuc.FTyLeBHTNNSD * (sub?.IQSBQNam ?? 0) * (sub?.LCS ?? 0))
                                                    : (sub?.FThuBHTNNSD ?? 0),
                                 FTongSoPhaiThuBHTN = (bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_DU_TOAN || bhMucLuc.SXauNoiMa == BhxhMLNS.HSQ_BS_HACH_TOAN)
                                                    ? ((bhMucLuc.FTyLeBHTNNLD + bhMucLuc.FTyLeBHTNNSD) * (sub?.IQSBQNam ?? 0) * (sub?.LCS ?? 0))
                                                    : (sub?.FTongSoPhaiThuBHTN ?? 0),
                                 FTongCong = sub?.FTongCong ?? 0,
                                 SGhiChu = sub?.SGhiChu ?? null,
                                 STenDonVi = sub?.STenDonVi ?? null,
                                 INamLamViec = sub?.INamLamViec ?? namLamViec,
                                 IIDMaDonVi = sub?.IIDMaDonVi ?? idDonVi,
                                 LCS = sub?.LCS ?? heSoLCS,
                             };

                return result;
            }
        }

        public List<string> FindListChiTietDonViByListMonth(int iNamLamViec, int loaiTongHop, bool bDaTongHop, string selectedQuarterList)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var listBhCTCT = ctx.BhQttBHXHChiTiets;
                var listMonth = selectedQuarterList.Split(',').ToList();
                var listBhCT = ctx.BhQttBHXHs.Where(x => x.INamLamViec == iNamLamViec && listMonth.Contains(x.IQuyNam.ToString()) && x.ILoaiTongHop == loaiTongHop);

                if (bDaTongHop)
                {
                    listBhCT = listBhCT.Where(x => x.BDaTongHop == bDaTongHop);
                }

                var sql = from bhctct in listBhCTCT
                          join bhct in listBhCT
                          on bhctct.QttBHXHId equals bhct.Id into tbl
                          from m in tbl.DefaultIfEmpty()
                          where m != null && m.INamLamViec == iNamLamViec && listMonth.Contains(m.IQuyNam.ToString())
                          select new { IIDMaDonVi = bhctct.IIDMaDonVi };
                var result = sql.Select(t => t.IIDMaDonVi).Distinct().ToList();
                return result;
            }
        }

        public List<string> FindListDonViExistSettlement(Guid id, int iNamLamViec, string userName, int selectedQuarter, int loaiQuy)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var listBhCTCT = ctx.BhQttBHXHChiTiets;
                var listBhCT = ctx.BhQttBHXHs.Where(x => x.Id != id && x.INamLamViec == iNamLamViec && x.IQuyNam == selectedQuarter && x.IQuyNamLoai == loaiQuy && x.ILoaiTongHop == 1);

                var sql = from bhctct in listBhCTCT
                          join bhct in listBhCT
                          on bhctct.QttBHXHId equals bhct.Id into tbl
                          from m in tbl.DefaultIfEmpty()
                          where m.INamLamViec == iNamLamViec && m.IQuyNam == selectedQuarter && m.IQuyNamLoai == loaiQuy && m.ILoaiTongHop == 1
                          select new { IIDMaDonVi = bhctct.IIDMaDonVi };
                var result = sql.ToList().Select(t => t.IIDMaDonVi).ToList();
                return result;
            }
        }
        public List<string> FindChiTietDonViThangQuy(int iNamLamViec, int loaiTongHop, bool bDaTongHop, int selectedQuarter, int loaiQuy)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var selectedQuarterList = new List<int>();
                var loaiQuarterList = new List<int>();

                if (loaiQuy == 0)
                {
                    selectedQuarterList = new List<int> { selectedQuarter };
                    loaiQuarterList = new List<int> { loaiQuy };
                }
                else
                {
                    if (loaiQuy == 1 && selectedQuarter == 3)
                    {
                        selectedQuarterList = new List<int> { 1, 2, 3 };
                    }
                    else if (loaiQuy == 1 && selectedQuarter == 6)
                    {
                        selectedQuarterList = new List<int> { 4, 5, 6 };
                    }
                    else if (loaiQuy == 1 && selectedQuarter == 9)
                    {
                        selectedQuarterList = new List<int> { 7, 8, 9 };
                    }
                    else if (loaiQuy == 1 && selectedQuarter == 12)
                    {
                        selectedQuarterList = new List<int> { 10, 11, 12 };
                    }
                    loaiQuarterList = new List<int> { 0, 1 };
                }

                var listBhCTCT = ctx.BhQttBHXHChiTiets;
                var listBhCT = ctx.BhQttBHXHs.Where(x => x.INamLamViec == iNamLamViec && selectedQuarterList.Contains(x.IQuyNam) && loaiQuarterList.Contains(x.IQuyNamLoai) && x.ILoaiTongHop == loaiTongHop);
                if (bDaTongHop)
                {
                    listBhCT = listBhCT.Where(x => x.BDaTongHop == bDaTongHop);
                }

                var sql = from bhctct in listBhCTCT
                          join bhct in listBhCT
                          on bhctct.QttBHXHId equals bhct.Id into tbl
                          from m in tbl.DefaultIfEmpty()
                          where m.INamLamViec == iNamLamViec && selectedQuarterList.Contains(m.IQuyNam) && loaiQuarterList.Contains(m.IQuyNamLoai)
                          select new { IIDMaDonVi = bhctct.IIDMaDonVi };
                var result = sql.ToList().Select(t => t.IIDMaDonVi).Distinct().ToList();
                return result;
            }
        }

        public List<string> FindChiTietDonVi(int iNamLamViec, int loaiTongHop, bool bDaTongHop, int selectedQuarter, int loaiQuy)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                //var selectedQuarterList = new List<int>();
                //var loaiQuarterList = new List<int>();

                //if (loaiQuy == 0)
                //{
                //    selectedQuarterList = new List<int> { selectedQuarter };
                //    loaiQuarterList = new List<int> { loaiQuy };
                //}
                //else
                //{
                //    if (loaiQuy == 1 && selectedQuarter == 3)
                //    {
                //        selectedQuarterList = new List<int> { 1, 2, 3 };
                //    }
                //    else if (loaiQuy == 1 && selectedQuarter == 6)
                //    {
                //        selectedQuarterList = new List<int> { 4, 5, 6 };
                //    }
                //    else if (loaiQuy == 1 && selectedQuarter == 9)
                //    {
                //        selectedQuarterList = new List<int> { 7, 8, 9 };
                //    }
                //    else if (loaiQuy == 1 && selectedQuarter == 12)
                //    {
                //        selectedQuarterList = new List<int> { 10, 11, 12 };
                //    }
                //    loaiQuarterList = new List<int> { 0, 1 };
                //}

                var listBhCTCT = ctx.BhQttBHXHChiTiets;
                var listBhCT = ctx.BhQttBHXHs.Where(x => x.INamLamViec == iNamLamViec
                                    //&& selectedQuarterList.Contains(x.IQuyNam) && loaiQuarterList.Contains(x.IQuyNamLoai) 
                                    && x.ILoaiTongHop == loaiTongHop);

                var sql = from bhctct in listBhCTCT
                          join bhct in listBhCT
                          on bhctct.QttBHXHId equals bhct.Id into tbl
                          from m in tbl.DefaultIfEmpty()
                          where m.INamLamViec == iNamLamViec
                          //&& selectedQuarterList.Contains(m.IQuyNam) && loaiQuarterList.Contains(m.IQuyNamLoai)
                          select new { IIDMaDonVi = bhctct.IIDMaDonVi };
                var result = sql.ToList().Select(t => t.IIDMaDonVi).Distinct().ToList();
                return result;
            }
        }
        public List<string> FindChiTietDonViTongHopThangQuy(int iNamLamViec, int loaiTongHop, string userName, int selectedQuarter, int loaiQuy)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var selectedQuarterList = new List<int>();
                var loaiQuarterList = new List<int>();

                if (loaiQuy == 0)
                {
                    selectedQuarterList = new List<int> { selectedQuarter };
                    loaiQuarterList = new List<int> { loaiQuy };
                }
                else
                {
                    if (loaiQuy == 1 && selectedQuarter == 3)
                    {
                        selectedQuarterList = new List<int> { 1, 2, 3 };
                    }
                    else if (loaiQuy == 1 && selectedQuarter == 6)
                    {
                        selectedQuarterList = new List<int> { 4, 5, 6 };
                    }
                    else if (loaiQuy == 1 && selectedQuarter == 9)
                    {
                        selectedQuarterList = new List<int> { 7, 8, 9 };
                    }
                    else if (loaiQuy == 1 && selectedQuarter == 12)
                    {
                        selectedQuarterList = new List<int> { 10, 11, 12 };
                    }
                    loaiQuarterList = new List<int> { 0, 1 };
                }

                var listDonVi = ctx.NsDonVis.Where(x => x.NamLamViec == iNamLamViec && x.Loai == "0").Select(t => t.IIDMaDonVi).ToList();
                var listDonViNguoiDung = ctx.NsNguoiDungDonVis.Where(x => x.INamLamViec == iNamLamViec && x.IIDMaNguoiDung == userName && listDonVi.Contains(x.IIdMaDonVi));

                var listBhCTCT = ctx.BhQttBHXHChiTiets;
                var listBhCT = ctx.BhQttBHXHs.Where(x => x.INamLamViec == iNamLamViec && selectedQuarterList.Contains(x.IQuyNam) && loaiQuarterList.Contains(x.IQuyNamLoai) && x.ILoaiTongHop == loaiTongHop);

                var sql = from bhctct in listBhCTCT
                          join bhct in listBhCT
                          on bhctct.QttBHXHId equals bhct.Id into tbl
                          from m in tbl.DefaultIfEmpty()
                          where m.INamLamViec == iNamLamViec && selectedQuarterList.Contains(m.IQuyNam) && loaiQuarterList.Contains(m.IQuyNamLoai)
                          select new { IIDMaDonVi = bhctct.IIDMaDonVi };
                var result = sql.ToList().Select(t => t.IIDMaDonVi).Distinct().ToList();
                //if (listDonViNguoiDung.Any())
                //{
                //    result.AddRange(listDonVi);
                //}
                return result;
            }
        }

        public List<string> FindChiTietDonViTongHop(int iNamLamViec, int loaiTongHop, string userName, int selectedQuarter, int loaiQuy)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                //var selectedQuarterList = new List<int>();
                //var loaiQuarterList = new List<int>();

                //if (loaiQuy == 0)
                //{
                //    selectedQuarterList = new List<int> { selectedQuarter };
                //    loaiQuarterList = new List<int> { loaiQuy };
                //}
                //else
                //{
                //    if (loaiQuy == 1 && selectedQuarter == 3)
                //    {
                //        selectedQuarterList = new List<int> { 1, 2, 3 };
                //    }
                //    else if (loaiQuy == 1 && selectedQuarter == 6)
                //    {
                //        selectedQuarterList = new List<int> { 4, 5, 6 };
                //    }
                //    else if (loaiQuy == 1 && selectedQuarter == 9)
                //    {
                //        selectedQuarterList = new List<int> { 7, 8, 9 };
                //    }
                //    else if (loaiQuy == 1 && selectedQuarter == 12)
                //    {
                //        selectedQuarterList = new List<int> { 10, 11, 12 };
                //    }
                //    loaiQuarterList = new List<int> { 0, 1 };
                //}

                var listDonVi = ctx.NsDonVis.Where(x => x.NamLamViec == iNamLamViec && x.Loai == "0").Select(t => t.IIDMaDonVi).ToList();
                var listDonViNguoiDung = ctx.NsNguoiDungDonVis.Where(x => x.INamLamViec == iNamLamViec && x.IIDMaNguoiDung == userName && listDonVi.Contains(x.IIdMaDonVi));


                var listBhCTCT = ctx.BhQttBHXHChiTiets;
                var listBhCT = ctx.BhQttBHXHs.Where(x => x.INamLamViec == iNamLamViec
                                    //&& selectedQuarterList.Contains(x.IQuyNam) && loaiQuarterList.Contains(x.IQuyNamLoai) 
                                    && x.ILoaiTongHop == loaiTongHop && (bool)x.BDaTongHop && x.BIsKhoa);

                var sql = from bhctct in listBhCTCT
                          join bhct in listBhCT
                          on bhctct.QttBHXHId equals bhct.Id into tbl
                          from m in tbl.DefaultIfEmpty()
                          where m.INamLamViec == iNamLamViec
                          //&& selectedQuarterList.Contains(m.IQuyNam) && loaiQuarterList.Contains(m.IQuyNamLoai)
                          select new { IIDMaDonVi = bhctct.IIDMaDonVi };
                var result = sql.ToList().Select(t => t.IIDMaDonVi).Distinct().ToList();
                if (listDonViNguoiDung.Any())
                {
                    result.AddRange(listDonVi);
                }
                return result;
            }
        }
        public List<string> FindAllDonVi(int iNamLamViec, int loaiTongHop, bool bDaTongHop, int selectedQuarter, int loaiQuy)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var selectedQuarterList = new List<int>();
                var loaiQuarterList = new List<int>();

                if (loaiQuy == 0)
                {
                    selectedQuarterList = new List<int> { selectedQuarter };
                    loaiQuarterList = new List<int> { loaiQuy };
                }
                else
                {
                    if (loaiQuy == 1 && selectedQuarter == 3)
                    {
                        selectedQuarterList = new List<int> { 1, 2, 3 };
                    }
                    else if (loaiQuy == 1 && selectedQuarter == 6)
                    {
                        selectedQuarterList = new List<int> { 4, 5, 6 };
                    }
                    else if (loaiQuy == 1 && selectedQuarter == 9)
                    {
                        selectedQuarterList = new List<int> { 7, 8, 9 };
                    }
                    else if (loaiQuy == 1 && selectedQuarter == 12)
                    {
                        selectedQuarterList = new List<int> { 10, 11, 12 };
                    }
                    loaiQuarterList = new List<int> { 0, 1 };
                }

                var listBhCTCT = ctx.BhQttBHXHChiTiets;
                var listBhCT = ctx.BhQttBHXHs.Where(x => x.INamLamViec == iNamLamViec
                                    && selectedQuarterList.Contains(x.IQuyNam)
                                    //&& loaiQuarterList.Contains(x.IQuyNamLoai)
                                    && x.ILoaiTongHop == loaiTongHop);

                var sql = from bhctct in listBhCTCT
                          join bhct in listBhCT
                          on bhctct.QttBHXHId equals bhct.Id into tbl
                          from m in tbl.DefaultIfEmpty()
                          where m.INamLamViec == iNamLamViec
                          && selectedQuarterList.Contains(m.IQuyNam)
                          //&& loaiQuarterList.Contains(m.IQuyNamLoai)
                          select new { IIDMaDonVi = bhctct.IIDMaDonVi };
                var result = sql.ToList().Select(t => t.IIDMaDonVi).Distinct().ToList();
                return result;
            }
        }
        public IEnumerable<BhQttBHXHChiTiet> FindDetailsQT(string idDonVi, int iNamLamViec, int iNamNganSach, int iNguonNganSach, int selectedQuarter, int loaiQuy)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter maDonvisParam = new SqlParameter("@IdDonVis", idDonVi);
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter namNSParam = new SqlParameter("@NamNganSach", iNamNganSach);
                SqlParameter nguonNSParam = new SqlParameter("@NguonNganSach", iNguonNganSach);
                SqlParameter selectedQuaterParam = new SqlParameter("@IQuy", selectedQuarter);
                SqlParameter loaiQuyParam = new SqlParameter("@ILoaiQuy", loaiQuy);
                return ctx.BhQttBHXHChiTiets.FromSql("EXECUTE dbo.sp_bh_qtt_bhxh_qt_chi_tiet @IdDonVis, @NamLamViec, @NamNganSach, @NguonNganSach, @IQuy, @ILoaiQuy",
                    maDonvisParam, namLamViecParam, namNSParam, nguonNSParam, selectedQuaterParam, loaiQuyParam).ToList();
            }
        }
        public List<BhDmMucLucNganSach> GetListBhMucLucNs(int? namLamViec, string idDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var iTrangThai = StatusType.ACTIVE;
                Func<BhDmMucLucNganSach, bool> defaultMucLucFilter = x => x.INamLamViec == namLamViec && x.SLNS.StartsWith(BhxhMLNS.KHT_BHXH_BHYT_BHTN) && x.ITrangThai == iTrangThai;
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

        public IEnumerable<BhQttBHXHChiTiet> FindVoucherDetail(Guid chungTuId, int? namLamViec, string maDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter chungTuIdParam = new SqlParameter("@ChungTuId", chungTuId);
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter maDonViParam = new SqlParameter("@MaDonVi", maDonVi);
                return ctx.BhQttBHXHChiTiets.FromSql("EXECUTE dbo.sp_bh_qtt_bhxh_chi_tiet @ChungTuId, @NamLamViec, @MaDonVi",
                    chungTuIdParam, namLamViecParam, maDonViParam).ToList();
            }
        }
        public IEnumerable<BhQttBHXHChiTiet> FindVoucherDetails(int? namLamViec, string maDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter maDonViParam = new SqlParameter("@MaDonVi", maDonVi);
                return ctx.BhQttBHXHChiTiets.FromSql("EXECUTE dbo.sp_bh_qtt_bhxh_chi_tiet_donvi @NamLamViec, @MaDonVi",
                    namLamViecParam, maDonViParam).ToList();
            }
        }
        public IEnumerable<BhQttBHXHChiTiet> FindVoucherDetailNam(IEnumerable<Guid> chungTuIds, int? namLamViec, string maDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var listIds = string.Join(StringUtils.COMMA, chungTuIds.ToList());
                SqlParameter chungTuIdParam = new SqlParameter("@ChungTuIds", listIds);
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter maDonViParam = new SqlParameter("@MaDonVi", maDonVi);
                var listBHQTT = ctx.BhQttBHXHChiTiets.FromSql("EXECUTE dbo.sp_bh_qtt_bhxh_chi_tiet_nam @ChungTuIds, @NamLamViec, @MaDonVi",
                    chungTuIdParam, namLamViecParam, maDonViParam).ToList();

                var listBHQTByIIDMLNS = listBHQTT.GroupBy(i => new { i.IIDMLNS, i.IIDMLNSCha, i.SXauNoiMa, i.SLns, i.INamLamViec, i.IIDMaDonVi, i.STenDonVi }).Select(group => new BhQttBHXHChiTiet
                {
                    Id = group.FirstOrDefault().Id,
                    IIDMLNS = group.Key.IIDMLNS,
                    IIDMLNSCha = group.Key.IIDMLNSCha,
                    SXauNoiMa = group.Key.SXauNoiMa,
                    SLns = group.Key.SLns,
                    INamLamViec = group.Key.INamLamViec,
                    IIDMaDonVi = group.Key.IIDMaDonVi,
                    STenDonVi = group.Key.STenDonVi,
                    IQSBQNam = group.Sum(x => x.IQSBQNam),
                    FLuongChinh = group.Sum(x => x.FLuongChinh),
                    FPhuCapChucVu = group.Sum(x => x.FPhuCapChucVu),
                    FPCTNNghe = group.Sum(x => x.FPCTNNghe),
                    FPCTNVuotKhung = group.Sum(x => x.FPCTNVuotKhung),
                    FNghiOm = group.Sum(x => x.FNghiOm),
                    FHSBL = group.Sum(x => x.FHSBL),
                    FTongQuyTienLuongNam = group.Sum(x => x.FTongQuyTienLuongNam),
                    FDuToan = group.Sum(x => x.FDuToan),
                    FDaQuyetToan = group.Sum(x => x.FDaQuyetToan),
                    FConLai = group.Sum(x => x.FConLai),
                    FThuBHXHNLD = group.Sum(x => x.FThuBHXHNLD),
                    FThuBHXHNSD = group.Sum(x => x.FThuBHXHNSD),
                    FTongSoPhaiThuBHXH = group.Sum(x => x.FTongSoPhaiThuBHXH),
                    FThuBHYTNLD = group.Sum(x => x.FThuBHYTNLD),
                    FThuBHYTNSD = group.Sum(x => x.FThuBHYTNSD),
                    FTongSoPhaiThuBHYT = group.Sum(x => x.FTongSoPhaiThuBHYT),
                    FThuBHTNNLD = group.Sum(x => x.FThuBHTNNLD),
                    FThuBHTNNSD = group.Sum(x => x.FThuBHTNNSD),
                    FTongSoPhaiThuBHTN = group.Sum(x => x.FTongSoPhaiThuBHTN),
                    FTongCong = group.Sum(x => x.FTongCong),
                }).ToList();

                return listBHQTByIIDMLNS;
            }
        }

        public IEnumerable<BhQttBHXHChiTietQuery> GetDataDaQuyetToan(Guid chungTuId, int? namLamViec, string maDonVi, int quyNamLoai)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_qtt_get_tong_da_quyet_toan @ChungTuId, @NamLamViec, @MaDonVi, @QuyNamLoai";
                var parameters = new[]
                {
                    new SqlParameter("@ChungTuId", chungTuId),
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@QuyNamLoai", quyNamLoai)
                };
                return ctx.FromSqlRaw<BhQttBHXHChiTietQuery>(sql, parameters).ToList();
            }
        }
        public IEnumerable<BhQttBHXHChiTietQuery> GetDatasDaQuyetToan(int? namLamViec, string idDonVi, int? selectedQuarter, int selectedQuarterType)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bh_qtt_get_tong_da_quyet_toan_donvi @NamLamViec, @MaDonVi, @QuyNam, @QuyNamLoai";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@MaDonVi", idDonVi),
                    new SqlParameter("@QuyNam", selectedQuarter),
                    new SqlParameter("@QuyNamLoai", selectedQuarterType)
                };
                return ctx.FromSqlRaw<BhQttBHXHChiTietQuery>(sql, parameters).ToList();
            }
        }
        public IEnumerable<BhQttBHXHChiTietQuery> GetDataLuongCanCu(string maDonVi, int? namLamViec, string months, int loaiQuyNam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql;
                if (loaiQuyNam == 2)
                    sql = "EXECUTE dbo.sp_bh_qtt_get_luong_can_cu_nam @MaDonVi, @NamLamViec";
                else
                    sql = "EXECUTE dbo.sp_bh_qtt_get_luong_can_cu_quy @MaDonVi, @NamLamViec, @Months, @LoaiQuyNam";

                var parameters = new[]
                {
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@Months", months),
                    new SqlParameter("@LoaiQuyNam", loaiQuyNam)
                };
                return ctx.FromSqlRaw<BhQttBHXHChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<BhDtPhanBoChungTuChiTietQuery> GetDataDTT(bool isDonViCha, int? namLamViec, string maDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "";
                if (isDonViCha)
                {
                    sql = "EXECUTE dbo.sp_bh_qtt_get_tong_du_toan_nhan_dtt @NamLamViec, @MaDonVi";
                }
                else
                {
                    sql = "EXECUTE dbo.sp_bh_qtt_get_tong_du_toan_dtt @NamLamViec, @MaDonVi";
                }

                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@MaDonVi", maDonVi)
                };
                return ctx.FromSqlRaw<BhDtPhanBoChungTuChiTietQuery>(sql, parameters).ToList();
            }
        }

        public double? GetThamSoLCS(int? namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhDmCauHinhThamSo.Where(x => x.INamLamViec == namLamViec && x.SMa == "LCS" && x.BTrangThai).Select(a => a.fGiaTri).FirstOrDefault();
            }
        }
        public Guid GetIdChungTuByDonVi(string maDonVi, int? namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQttBHXHs.Where(x => x.INamLamViec == namLamViec && x.IIDMaDonVi == maDonVi).Select(a => a.Id).FirstOrDefault();
            }
        }

        public IEnumerable<Guid> GetListIdChungTuByDonVi(string maDonVi, int? namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQttBHXHs.Where(x => x.INamLamViec == namLamViec && x.IIDMaDonVi == maDonVi).Select(a => a.Id).ToList();
            }
        }

        public IEnumerable<BhQttBHXHChiTiet> FindVoucherDetailById(BhQttBHXHChiTietCriteria searchCondition)
        {
            Guid voucherID = searchCondition.IdQttBhxh;
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQttBHXHChiTiets.Where(x => x.QttBHXHId == voucherID).ToList();
            }
        }

        public IEnumerable<string> GetLnsHasData(List<Guid> chungTuIds)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQttBHXHChiTiets.Where(x => x.QttBHXHId.HasValue && chungTuIds.Contains(x.QttBHXHId.Value) && x.IsHasData).Select(x => x.SLns).Distinct().ToList();
            }
        }

        public bool ExistVoucherDetail(Guid voucherID)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQttBHXHChiTiets.Any(t => t.QttBHXHId.Equals(voucherID));
            }
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQuyetToanThuNopBhxhBhytBhtnQuy(int iNamLamViec, string sIdDonVi, int donViTinh, bool isTongHop, int selectedQuarter, int loaiQuy, bool isLuyKe)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter sIdDonViParam = new SqlParameter("@IdDonVi", sIdDonVi);
                SqlParameter donViTinhParam = new SqlParameter("@Donvitinh", donViTinh);
                SqlParameter isTongHopParam = new SqlParameter("@IsTongHop", isTongHop);
                SqlParameter iQuyParam = new SqlParameter("@IQuy", selectedQuarter);
                SqlParameter iLoaiQuyParam = new SqlParameter("@ILoaiQuy", loaiQuy);
                SqlParameter isLuyKeParam = new SqlParameter("@IsLuyKe", isLuyKe);

                return ctx.FromSqlRaw<BhQttBHXHChiTietQuery>("EXECUTE dbo.sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy "
                    + "@NamLamViec, @IdDonVi, @Donvitinh,  @IsTongHop, @IQuy, @ILoaiQuy, @IsLuyKe",
                    iNamLamViecParam, sIdDonViParam, donViTinhParam, isTongHopParam, iQuyParam, iLoaiQuyParam, isLuyKeParam).ToList();
            }
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQuyetToanThuNopBhxhBhytBhtnTheoThoiGian(int iNamLamViec, string sIdDonVi, int donViTinh, string lstMonthSelected)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter sIdDonViParam = new SqlParameter("@IdDonVi", sIdDonVi);
                SqlParameter donViTinhParam = new SqlParameter("@Donvitinh", donViTinh);
                SqlParameter listTongHopParam = new SqlParameter("@LstDonVi", lstMonthSelected);

                return ctx.FromSqlRaw<BhQttBHXHChiTietQuery>("EXECUTE dbo.sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_theo_thoi_gian "
                    + "@NamLamViec, @IdDonVi, @Donvitinh,  @LstDonVi",
                    iNamLamViecParam, sIdDonViParam, donViTinhParam, listTongHopParam).ToList();
            }
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQTTNopBhxhBhytBhtnTongHopTheoThoiGian(int iNamLamViec, string sIdDonVi, int donViTinh, string lstMonthSelected)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter sIdDonViParam = new SqlParameter("@IdDonVi", sIdDonVi);
                SqlParameter donViTinhParam = new SqlParameter("@Donvitinh", donViTinh);
                SqlParameter listTongHopParam = new SqlParameter("@LstDonVi", lstMonthSelected);

                return ctx.FromSqlRaw<BhQttBHXHChiTietQuery>("EXECUTE dbo.sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_theo_thoi_gian "
                    + "@NamLamViec, @IdDonVi, @Donvitinh, @LstDonVi",
                    iNamLamViecParam, sIdDonViParam, donViTinhParam, listTongHopParam).ToList();
            }
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQTTNopBhxhBhytBhtnTongHopChiTietDonViTheoThoiGian(int iNamLamViec, string sIdDonVis, int donViTinh, string lstMonthSelected)
        {
            try
            {
                using (var ctx = _contextFactory.CreateDbContext())
                {
                    SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                    SqlParameter sIdDonViParam = new SqlParameter("@IdDonVis", sIdDonVis);
                    SqlParameter donViTinhParam = new SqlParameter("@Donvitinh", donViTinh);
                    SqlParameter listTongHopParam = new SqlParameter("@LstDonVi", lstMonthSelected);
                    
                    return ctx.FromSqlRaw<BhQttBHXHChiTietQuery>("EXECUTE dbo.sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_theo_thoi_gian "
                            + "@NamLamViec, @IdDonVis, @Donvitinh, @LstDonVi",
                            iNamLamViecParam, sIdDonViParam, donViTinhParam, listTongHopParam).ToList();

                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return new List<BhQttBHXHChiTietQuery>();
            }
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQuyetToanThuNopBhxhBhytBhtnThang(int iNamLamViec, string sIdDonVi, int donViTinh, int selectedQuarter, int loaiQuy, bool isLuyKe)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter sIdDonViParam = new SqlParameter("@IdDonVi", sIdDonVi);
                SqlParameter donViTinhParam = new SqlParameter("@Donvitinh", donViTinh);
                SqlParameter iQuyParam = new SqlParameter("@IQuy", selectedQuarter);
                SqlParameter iLoaiQuyParam = new SqlParameter("@ILoaiQuy", loaiQuy);
                SqlParameter isLuyKeParam = new SqlParameter("@IsLuyKe", isLuyKe);

                return ctx.FromSqlRaw<BhQttBHXHChiTietQuery>("EXECUTE dbo.sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_thang "
                    + "@NamLamViec, @IdDonVi, @Donvitinh, @IQuy, @ILoaiQuy, @IsLuyKe",
                    iNamLamViecParam, sIdDonViParam, donViTinhParam, iQuyParam, iLoaiQuyParam, isLuyKeParam).ToList();
            }
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQuyetToanThuNopBhxhBhytBhtnNam(int iNamLamViec, string sIdDonVi, int donViTinh, bool isTongHop, int selectedQuarter)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter sIdDonViParam = new SqlParameter("@IdDonVi", sIdDonVi);
                SqlParameter donViTinhParam = new SqlParameter("@Donvitinh", donViTinh);
                SqlParameter isTongHopParam = new SqlParameter("@IsTongHop", isTongHop);
                SqlParameter iQuy = new SqlParameter("@IQuy ", selectedQuarter);
                return ctx.FromSqlRaw<BhQttBHXHChiTietQuery>("EXECUTE dbo.sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_nam "
                    + "@NamLamViec, @IdDonVi, @Donvitinh,  @IsTongHop, @IQuy",
                    iNamLamViecParam, sIdDonViParam, donViTinhParam, isTongHopParam, iQuy).ToList();
            }
        }

        public IEnumerable<BhQttBHXHChiTietQuery> GetDataTongHopSoSanhNam(int iNamLamViec, string sIdDonVi, int donViTinh, bool isTongHop, int selectedQuarter)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter sIdDonViParam = new SqlParameter("@IdDonVi", sIdDonVi);
                SqlParameter donViTinhParam = new SqlParameter("@Donvitinh", donViTinh);
                SqlParameter isTongHopParam = new SqlParameter("@IsTongHop", isTongHop);
                SqlParameter iQuy = new SqlParameter("@IQuy ", selectedQuarter);
                return ctx.FromSqlRaw<BhQttBHXHChiTietQuery>("EXECUTE dbo.sp_bh_qtt_rpt_quyet_toan_thu_tong_hop_so_sanh_nam "
                    + "@NamLamViec, @IdDonVi, @Donvitinh,  @IsTongHop, @IQuy",
                    iNamLamViecParam, sIdDonViParam, donViTinhParam, isTongHopParam, iQuy).ToList();
            }
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQuyetToanThuBhxhBhtn(int namLamViec, string lstDonvi, string khoiDuToan, string khoiHachToan, int dvt, bool isTongHop)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter lstDonviParam = new SqlParameter("@LstSelectedUnit ", lstDonvi);
                SqlParameter khoiDuToanParam = new SqlParameter("@KhoiDuToan", khoiDuToan);
                SqlParameter khoiHachToanParam = new SqlParameter("@KhoiHachToan", khoiHachToan);
                SqlParameter dvtParam = new SqlParameter("@Dvt", dvt);
                SqlParameter isTongHopParam = new SqlParameter("@IsTongHop", isTongHop);

                return ctx.FromSqlRaw<BhQttBHXHChiTietQuery>("EXECUTE dbo.sp_bh_qtt_rpt_quyet_toan_thu_bhxh_bhtn "
                    + "@NamLamViec, @LstSelectedUnit, @KhoiDuToan,  @KhoiHachToan, @Dvt, @IsTongHop",
                    iNamLamViecParam, lstDonviParam, khoiDuToanParam, khoiHachToanParam, dvtParam, isTongHopParam).ToList();
            }
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQuyetToanThuBHYT(int namLamViec, string lstDonvi, string khoiDuToan, string khoiHachToan, int dvt, bool isTongHop, string sm)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter lstDonviParam = new SqlParameter("@LstSelectedUnit ", lstDonvi);
                SqlParameter khoiDuToanParam = new SqlParameter("@KhoiDuToan", khoiDuToan);
                SqlParameter khoiHachToanParam = new SqlParameter("@KhoiHachToan", khoiHachToan);
                SqlParameter dvtParam = new SqlParameter("@Dvt", dvt);
                SqlParameter isTongHopParam = new SqlParameter("@IsTongHop", isTongHop);
                SqlParameter smParam = new SqlParameter("@SM", sm);

                return ctx.FromSqlRaw<BhQttBHXHChiTietQuery>("EXECUTE dbo.sp_bh_qtt_rpt_quyet_toan_thu_bhyt "
                    + "@NamLamViec, @LstSelectedUnit, @KhoiDuToan,  @KhoiHachToan, @Dvt, @IsTongHop, @SM",
                    iNamLamViecParam, lstDonviParam, khoiDuToanParam, khoiHachToanParam, dvtParam, isTongHopParam, smParam).ToList();
            }
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportTongHopQuyetToanThuChi(int namLamViec, string lstDonvi, int dvt, bool isTongHop)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter lstDonviParam = new SqlParameter("@IdDonVi ", lstDonvi);
                SqlParameter dvtParam = new SqlParameter("@DonViTinh", dvt);
                SqlParameter isTongHopParam = new SqlParameter("@IsTongHop", isTongHop);

                return ctx.FromSqlRaw<BhQttBHXHChiTietQuery>("EXECUTE dbo.sp_rpt_tong_hop_quyet_toan_thu_chi "
                    + "@NamLamViec, @IdDonVi, @DonViTinh, @IsTongHop",
                    iNamLamViecParam, lstDonviParam, dvtParam, isTongHopParam).ToList();
            }
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQTTNopBhxhBhytBhtnTongHopQuy(int iNamLamViec, string sIdDonVi, int donViTinh, bool isTongHop, int selectedQuarter, int loaiQuy, bool isLuyKe)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter sIdDonViParam = new SqlParameter("@IdDonVi", sIdDonVi);
                SqlParameter donViTinhParam = new SqlParameter("@Donvitinh", donViTinh);
                SqlParameter isTongHopParam = new SqlParameter("@IsTongHop", isTongHop);
                SqlParameter iQuyParam = new SqlParameter("@IQuy", selectedQuarter);
                SqlParameter iLoaiQuyParam = new SqlParameter("@ILoaiQuy", loaiQuy);
                SqlParameter isLuyKeParam = new SqlParameter("@IsLuyKe", isLuyKe);

                return ctx.FromSqlRaw<BhQttBHXHChiTietQuery>("EXECUTE dbo.sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_quy "
                    + "@NamLamViec, @IdDonVi, @Donvitinh,  @IsTongHop, @IQuy, @ILoaiQuy, @IsLuyKe",
                    iNamLamViecParam, sIdDonViParam, donViTinhParam, isTongHopParam, iQuyParam, iLoaiQuyParam, isLuyKeParam).ToList();
            }
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQTTNopBhxhBhytBhtnTongHopThang(int iNamLamViec, string sIdDonVi, int donViTinh, int selectedQuarter, int loaiQuy, bool isLuyKe)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter sIdDonViParam = new SqlParameter("@IdDonVi", sIdDonVi);
                SqlParameter donViTinhParam = new SqlParameter("@Donvitinh", donViTinh);
                SqlParameter iQuyParam = new SqlParameter("@IQuy", selectedQuarter);
                SqlParameter iLoaiQuyParam = new SqlParameter("@ILoaiQuy", loaiQuy);
                SqlParameter isLuyKeParam = new SqlParameter("@IsLuyKe", isLuyKe);

                return ctx.FromSqlRaw<BhQttBHXHChiTietQuery>("EXECUTE dbo.sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_thang "
                    + "@NamLamViec, @IdDonVi, @Donvitinh, @IQuy, @ILoaiQuy, @IsLuyKe",
                    iNamLamViecParam, sIdDonViParam, donViTinhParam, iQuyParam, iLoaiQuyParam, isLuyKeParam).ToList();
            }
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQTTNopBhxhBhytBhtnTongHopDonViQuy(int iNamLamViec, string sIdDonVis, int donViTinh, int selectedQuarter, int loaiQuy, bool isLuyKe)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter sIdDonViParam = new SqlParameter("@IdDonVis", sIdDonVis);
                SqlParameter donViTinhParam = new SqlParameter("@Donvitinh", donViTinh);
                SqlParameter iQuyParam = new SqlParameter("@IQuy ", selectedQuarter);
                SqlParameter iLoaiQuyParam = new SqlParameter("@ILoaiQuy", loaiQuy);
                SqlParameter isLuyKeParam = new SqlParameter("@IsLuyKe", isLuyKe);

                return ctx.FromSqlRaw<BhQttBHXHChiTietQuery>("EXECUTE dbo.sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_quy "
                    + "@NamLamViec, @IdDonVis, @Donvitinh, @IQuy, @ILoaiQuy, @IsLuyKe",
                    iNamLamViecParam, sIdDonViParam, donViTinhParam, iQuyParam, iLoaiQuyParam, isLuyKeParam).ToList();
            }
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQTTNopBhxhBhytBhtnTongHopDonViThang(int iNamLamViec, string sIdDonVis, int donViTinh, int selectedQuarter, int loaiQuy, bool isLuyKe)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter sIdDonViParam = new SqlParameter("@IdDonVis", sIdDonVis);
                SqlParameter donViTinhParam = new SqlParameter("@Donvitinh", donViTinh);
                SqlParameter iQuyParam = new SqlParameter("@IQuy ", selectedQuarter);
                SqlParameter iLoaiQuyParam = new SqlParameter("@ILoaiQuy", loaiQuy);
                SqlParameter isLuyKeParam = new SqlParameter("@IsLuyKe", isLuyKe);

                return ctx.FromSqlRaw<BhQttBHXHChiTietQuery>("EXECUTE dbo.sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_thang "
                    + "@NamLamViec, @IdDonVis, @Donvitinh, @IQuy, @ILoaiQuy, @IsLuyKe",
                    iNamLamViecParam, sIdDonViParam, donViTinhParam, iQuyParam, iLoaiQuyParam, isLuyKeParam).ToList();
            }
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQuyetToanThuNopTongHopNam(int iNamLamViec, string sIdDonVi, int donViTinh, bool isTongHop, int selectedQuarter)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter sIdDonViParam = new SqlParameter("@IdDonVi", sIdDonVi);
                SqlParameter donViTinhParam = new SqlParameter("@Donvitinh", donViTinh);
                SqlParameter isTongHopParam = new SqlParameter("@IsTongHop", isTongHop);
                SqlParameter iQuy = new SqlParameter("@IQuy ", selectedQuarter);
                return ctx.FromSqlRaw<BhQttBHXHChiTietQuery>("EXECUTE dbo.sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_nam "
                    + "@NamLamViec, @IdDonVi, @Donvitinh,  @IsTongHop, @IQuy",
                    iNamLamViecParam, sIdDonViParam, donViTinhParam, isTongHopParam, iQuy).ToList();
            }
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQTTNopBhxhBhytBhtnTongHopDonViNam(int iNamLamViec, string sIdDonVis, int donViTinh, int selectedQuarter)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter sIdDonViParam = new SqlParameter("@IdDonVis", sIdDonVis);
                SqlParameter donViTinhParam = new SqlParameter("@Donvitinh", donViTinh);
                SqlParameter iQuy = new SqlParameter("@IQuy ", selectedQuarter);
                return ctx.FromSqlRaw<BhQttBHXHChiTietQuery>("EXECUTE dbo.sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_nam "
                    + "@NamLamViec, @IdDonVis, @Donvitinh, @IQuy",
                    iNamLamViecParam, sIdDonViParam, donViTinhParam, iQuy).ToList();
            }
        }

        public IEnumerable<BhReportQttBHXHChiTietQuery> ExportQTTBhxhBhytBhtnTongHopDonViNam(int iNamLamViec, string sIdDonVis, int donViTinh, int type)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                SqlParameter sIdDonViParam = new SqlParameter("@LstMaDonVi", sIdDonVis);
                SqlParameter donViTinhParam = new SqlParameter("@DVT", donViTinh);
                SqlParameter iType = new SqlParameter("@Type ", type);
                return ctx.FromSqlRaw<BhReportQttBHXHChiTietQuery>("EXECUTE dbo.sp_bh_report_tong_hop_quyet_toan_nam "
                    + "@NamLamViec, @LstMaDonVi, @Type, @DVT",
                    iNamLamViecParam, sIdDonViParam, iType, donViTinhParam).ToList();
            }
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQTTNopBhxhBhytBhtnTongHopChiTietDonViQuy(int iNamLamViec, string sIdDonVis, int donViTinh, int selectedQuarter, int iLoaiQuy, bool isLuyKe, bool isTongHop = false)
        {
            try
            {
                using (var ctx = _contextFactory.CreateDbContext())
                {
                    SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                    SqlParameter sIdDonViParam = new SqlParameter("@IdDonVis", sIdDonVis);
                    SqlParameter donViTinhParam = new SqlParameter("@Donvitinh", donViTinh);
                    SqlParameter iQuyParam = new SqlParameter("@IQuy ", selectedQuarter);
                    SqlParameter iLoaiQuyParam = new SqlParameter("@ILoaiQuy ", iLoaiQuy);
                    SqlParameter isLuyKeParam = new SqlParameter("@IsLuyKe", isLuyKe);

                    if (isTongHop)
                    {
                        return ctx.FromSqlRaw<BhQttBHXHChiTietQuery>("EXECUTE dbo.sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_quy "
                            + "@NamLamViec, @IdDonVis, @Donvitinh, @IQuy, @ILoaiQuy, @IsLuyKe",
                            iNamLamViecParam, sIdDonViParam, donViTinhParam, iQuyParam, iLoaiQuyParam, isLuyKeParam).ToList();
                    }
                    else
                    {
                        return ctx.FromSqlRaw<BhQttBHXHChiTietQuery>("EXECUTE dbo.sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_quy "
                            + "@NamLamViec, @IdDonVis, @Donvitinh, @IQuy, @ILoaiQuy, @IsLuyKe",
                            iNamLamViecParam, sIdDonViParam, donViTinhParam, iQuyParam, iLoaiQuyParam, isLuyKeParam).ToList();
                    }

                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return new List<BhQttBHXHChiTietQuery>(); 
            }
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQTTNopBhxhBhytBhtnTongHopChiTietDonViThang(int iNamLamViec, string sIdDonVis, int donViTinh, int selectedQuarter, int iLoaiQuy, bool isTongHop, bool isLuyKe)
        {
            try
            {
                using (var ctx = _contextFactory.CreateDbContext())
                {
                    SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                    SqlParameter sIdDonViParam = new SqlParameter("@IdDonVis", sIdDonVis);
                    SqlParameter donViTinhParam = new SqlParameter("@Donvitinh", donViTinh);
                    SqlParameter iQuyParam = new SqlParameter("@IQuy ", selectedQuarter);
                    SqlParameter iLoaiQuyParam = new SqlParameter("@ILoaiQuy ", iLoaiQuy);
                    SqlParameter isLuyKeParam = new SqlParameter("@IsLuyKe", isLuyKe);

                    if (isTongHop)
                    {
                        return ctx.FromSqlRaw<BhQttBHXHChiTietQuery>("EXECUTE dbo.sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_thang "
                            + "@NamLamViec, @IdDonVis, @Donvitinh, @IQuy, @ILoaiQuy, @IsLuyKe",
                            iNamLamViecParam, sIdDonViParam, donViTinhParam, iQuyParam, iLoaiQuyParam, isLuyKeParam).ToList();
                    }
                    else
                    {
                        return ctx.FromSqlRaw<BhQttBHXHChiTietQuery>("EXECUTE dbo.sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_thang "
                            + "@NamLamViec, @IdDonVis, @Donvitinh, @IQuy, @ILoaiQuy, @IsLuyKe",
                            iNamLamViecParam, sIdDonViParam, donViTinhParam, iQuyParam, iLoaiQuyParam, isLuyKeParam).ToList();
                    }

                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return new List<BhQttBHXHChiTietQuery>();
            }
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQTTNopBhxhBhytBhtnTongHopChiTietDonViNam(int iNamLamViec, string sIdDonVis, int donViTinh, int selectedQuarter, bool isTongHop = false)
        {
            try
            {
                using (var ctx = _contextFactory.CreateDbContext())
                {
                    SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", iNamLamViec);
                    SqlParameter sIdDonViParam = new SqlParameter("@IdDonVis", sIdDonVis);
                    SqlParameter donViTinhParam = new SqlParameter("@Donvitinh", donViTinh);
                    SqlParameter iQuy = new SqlParameter("@IQuy ", selectedQuarter);
                    if (isTongHop)
                    {
                        return ctx.FromSqlRaw<BhQttBHXHChiTietQuery>("EXECUTE dbo.sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_nam "
                            + "@NamLamViec, @IdDonVis, @Donvitinh, @IQuy",
                            iNamLamViecParam, sIdDonViParam, donViTinhParam, iQuy).ToList();
                    }
                    else
                    {
                        return ctx.FromSqlRaw<BhQttBHXHChiTietQuery>("EXECUTE dbo.sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_nam "
                            + "@NamLamViec, @IdDonVis, @Donvitinh, @IQuy",
                            iNamLamViecParam, sIdDonViParam, donViTinhParam, iQuy).ToList();
                    }

                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return new List<BhQttBHXHChiTietQuery>();
            }
        }

        public IEnumerable<BhQttBHXHChiTietQuery> GetChiTietChungTuThangQuy(int? iNamLamViec, string sIdDonVis)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@INamLamViec", iNamLamViec);
                SqlParameter sIdDonViParam = new SqlParameter("@IdMaDonVi", sIdDonVis);
                return ctx.FromSqlRaw<BhQttBHXHChiTietQuery>("EXECUTE dbo.sp_bhxh_qtt_get_data_thang_quy_donvi " + "@INamLamViec, @IdMaDonVi", iNamLamViecParam, sIdDonViParam).ToList();
            }
        }

        public IEnumerable<BhQttBHXHChiTietQuery> GetChiTietChungTuThangQuyDonViCha(int? iNamLamViec, string sIdDonVis, string sIdDonViChas)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@INamLamViec", iNamLamViec);
                SqlParameter sIdDonViParam = new SqlParameter("@IdMaDonVi", sIdDonVis);
                SqlParameter sIdDonViChaParam = new SqlParameter("@IdMaDonViCha", sIdDonViChas);
                return ctx.FromSqlRaw<BhQttBHXHChiTietQuery>("EXECUTE dbo.sp_bhxh_qtt_get_data_thang_quy_donvi_cha " 
                    + "@INamLamViec, @IdMaDonVi, @IdMaDonViCha", iNamLamViecParam, sIdDonViParam, sIdDonViChaParam).ToList();
            }
        }

        public IEnumerable<BhReportQttBHXHChiTietQuery> FindVoucherDetailsThongTri(BhQttBHXHChiTietCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", searchCondition.INamLamViec);
                SqlParameter sIdDonViParam = new SqlParameter("@LstMaDonVi", searchCondition.IIDMaDonVi);
                SqlParameter iQuyNamParam = new SqlParameter("@IQuyNam", searchCondition.IQuyNam);
                SqlParameter iQuyNamLoaiParam = new SqlParameter("@IQuyNamLoai", searchCondition.IQuyNamLoai);
                SqlParameter iLoaiThongTriParam = new SqlParameter("@ILoaiThongTri", searchCondition.ILoaiThongTri);
                SqlParameter donViTinhParam = new SqlParameter("@DVT", searchCondition.DonViTinh);
                return ctx.FromSqlRaw<BhReportQttBHXHChiTietQuery>("EXECUTE dbo.sp_bh_report_qtt_thong_tri "
                    + "@NamLamViec, @LstMaDonVi, @IQuyNam, @IQuyNamLoai, @ILoaiThongTri, @DVT",
                    iNamLamViecParam, sIdDonViParam, iQuyNamParam, iQuyNamLoaiParam, iLoaiThongTriParam, donViTinhParam).ToList();
            }
        }
    }
}
