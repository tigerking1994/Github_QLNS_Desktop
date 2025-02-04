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

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class SktSoLieuRepository : Repository<NsDtdauNamChungTuChiTiet>, ISktSoLieuRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public SktSoLieuRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<SktSoLieuChiTietMlnsQuery> FindByCondition(int namLamViec, int namNganSach, int nguonNganSach, int loai, int typeGet, string idDonVi, string loaiChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                SqlParameter yearOfBudgetParam = new SqlParameter("@YearOfBudget", namNganSach);
                SqlParameter budgetSourceParam = new SqlParameter("@BudgetSource", nguonNganSach);
                SqlParameter loaiParam = new SqlParameter("@Loai", loai);
                SqlParameter typeGetParam = new SqlParameter("@TypeGet", typeGet);
                SqlParameter agencyIdParam = new SqlParameter("@AgencyId", idDonVi);
                SqlParameter loaiChungTuParam = new SqlParameter("@LoaiChungTu", loaiChungTu);
                return ctx.FromSqlRaw<SktSoLieuChiTietMlnsQuery>("EXECUTE dbo.sp_skt_solieuchitiet_index @YearOfWork, @YearOfBudget, @BudgetSource, @Loai, @TypeGet, @AgencyId, @LoaiChungTu", yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, loaiParam, typeGetParam, agencyIdParam, loaiChungTuParam).ToList();
            }
        }

        public IEnumerable<SktSoLieuChiTietMlnsQuery> FindByCondition(int namLamViec, int namNganSach, int nguonNganSach, int loai, int typeGet, string idDonVi, string loaiChungTu, string lns, string voucherId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                SqlParameter yearOfBudgetParam = new SqlParameter("@YearOfBudget", namNganSach);
                SqlParameter budgetSourceParam = new SqlParameter("@BudgetSource", nguonNganSach);
                SqlParameter loaiParam = new SqlParameter("@Loai", loai);
                SqlParameter typeGetParam = new SqlParameter("@TypeGet", typeGet);
                SqlParameter agencyIdParam = new SqlParameter("@AgencyId", idDonVi);
                SqlParameter loaiChungTuParam = new SqlParameter("@LoaiChungTu", loaiChungTu);
                SqlParameter lnsParam = new SqlParameter("@Lns", lns);
                SqlParameter voucherIdParam = new SqlParameter("@VoucherId", voucherId);
                return ctx.FromSqlRaw<SktSoLieuChiTietMlnsQuery>("EXECUTE dbo.sp_skt_solieuchitiet_index_3 @YearOfWork, @YearOfBudget, @BudgetSource, @Loai, @TypeGet, @AgencyId, @LoaiChungTu, @Lns, @VoucherId",
                    yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, loaiParam, typeGetParam, agencyIdParam, loaiChungTuParam, lnsParam, voucherIdParam).ToList();

            }
        }

        public List<SktSoLieuChiTietMlnsQuery> GetDataReportChiNganSach(int namLamViec, int namNganSach, int nguonNganSach, string idDonVi, string loaiChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                SqlParameter yearOfBudgetParam = new SqlParameter("@YearOfBudget", namNganSach);
                SqlParameter budgetSourceParam = new SqlParameter("@BudgetSource", nguonNganSach);
                SqlParameter agencyIdParam = new SqlParameter("@AgencyId", idDonVi);
                SqlParameter loaiChungTuParam = new SqlParameter("@LoaiChungTu", loaiChungTu);
                return ctx.FromSqlRaw<SktSoLieuChiTietMlnsQuery>("EXECUTE dbo.sp_skt_dutoandaunam_report_chingansach @YearOfWork, @YearOfBudget, @BudgetSource, @AgencyId, @LoaiChungTu",
                    yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, agencyIdParam, loaiChungTuParam).ToList();
            }
        }

        public void CreateDataReportTotal(int namLamViec, int namNganSach, int nguonNganSach, int loai, int typeGet, string idDonVi, string loaiChungTu, string listDonViTongHop, string nguoiTao)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                SqlParameter yearOfBudgetParam = new SqlParameter("@YearOfBudget", namNganSach);
                SqlParameter budgetSourceParam = new SqlParameter("@BudgetSource", nguonNganSach);
                SqlParameter loaiParam = new SqlParameter("@Loai", loai);
                SqlParameter typeGetParam = new SqlParameter("@TypeGet", typeGet);
                SqlParameter agencyIdParam = new SqlParameter("@AgencyId", idDonVi);
                SqlParameter loaiChungTuParam = new SqlParameter("@LoaiChungTu", loaiChungTu);
                SqlParameter listDonViTongHopParam = new SqlParameter("@DonViTongHop", listDonViTongHop);
                SqlParameter nguoiTaoParam = new SqlParameter("@NguoiTao", nguoiTao);
                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_skt_solieuchitiet_create_data_report @YearOfWork, @YearOfBudget, @BudgetSource, @Loai, @TypeGet, @AgencyId, @LoaiChungTu, @DonViTongHop, @NguoiTao",
                    yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, loaiParam, typeGetParam, agencyIdParam, loaiChungTuParam, listDonViTongHopParam, nguoiTaoParam);
            }
        }

        public void CreateDataReportTotalSummary(string id, int namLamViec, int namNganSach, int nguonNganSach, int loai, int typeGet, string idDonVi, string loaiChungTu, string listChungTuTongHop, string nguoiTao)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter chungTuId = new SqlParameter("@chungTuId", id);
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                SqlParameter yearOfBudgetParam = new SqlParameter("@YearOfBudget", namNganSach);
                SqlParameter budgetSourceParam = new SqlParameter("@BudgetSource", nguonNganSach);
                SqlParameter loaiParam = new SqlParameter("@Loai", loai);
                SqlParameter typeGetParam = new SqlParameter("@TypeGet", typeGet);
                SqlParameter agencyIdParam = new SqlParameter("@AgencyId", idDonVi);
                SqlParameter loaiChungTuParam = new SqlParameter("@LoaiChungTu", loaiChungTu);
                SqlParameter listChungTuTongHopParam = new SqlParameter("@ChungTuTongHop", listChungTuTongHop);
                SqlParameter nguoiTaoParam = new SqlParameter("@NguoiTao", nguoiTao);
                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_skt_solieuchitiet_create_data_report_2 @chungTuId, @YearOfWork, @YearOfBudget, @BudgetSource, @Loai, @TypeGet, @AgencyId, @LoaiChungTu, @ChungTuTongHop, @NguoiTao",
                    chungTuId, yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, loaiParam, typeGetParam, agencyIdParam, loaiChungTuParam, listChungTuTongHopParam, nguoiTaoParam);
            }
        }

        public bool IsLockDonViStatus(string idDonVi, int namLamViec, string loaiChungTu, int namNganSach, int nguonNganSach)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                List<NsDtdauNamChungTuChiTiet> list = ctx.NsDtdauNamChungTuChiTiets.Where(n => n.INamLamViec == namLamViec && n.IIdMaDonVi == idDonVi && n.ILoaiChungTu == loaiChungTu
                && n.INamNganSach == namNganSach && n.IIdMaNguonNganSach == nguonNganSach && n.BKhoa.HasValue && n.BKhoa.Value).ToList();

                NsDtdauNamChungTu itemChungTu = ctx.NsDtdauNamChungTus.Where(n => n.INamLamViec == namLamViec && n.IIdMaDonVi == idDonVi && n.ILoaiChungTu.Value == int.Parse(loaiChungTu)
                && n.INamNganSach == namNganSach && n.IIdMaNguonNganSach == nguonNganSach && n.BKhoa).FirstOrDefault();
                if (itemChungTu != null || (list != null && list.Count > 0))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void UnLockDataReportTotal(int namLamViec, int namNganSach, int nguonNganSach, int loai, int typeGet, string idDonVi, string loaiChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                SqlParameter yearOfBudgetParam = new SqlParameter("@YearOfBudget", namNganSach);
                SqlParameter budgetSourceParam = new SqlParameter("@BudgetSource", nguonNganSach);
                SqlParameter loaiParam = new SqlParameter("@Loai", loai);
                SqlParameter typeGetParam = new SqlParameter("@TypeGet", typeGet);
                SqlParameter agencyIdParam = new SqlParameter("@AgencyId", idDonVi);
                SqlParameter loaiChungTuParam = new SqlParameter("@LoaiChungTu", loaiChungTu);
                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_skt_unlock_data_report @YearOfWork, @YearOfBudget, @BudgetSource, @Loai, @TypeGet, @AgencyId, @LoaiChungTu", yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, loaiParam, typeGetParam, agencyIdParam, loaiChungTuParam);
            }
        }

        public IEnumerable<SktSoLieuChiTietMlnsQuery> FindByConditionDonVi0(int namLamViec, int namNganSach, int nguonNganSach, int loai, int typeGet, string idDonVi, string loaiChungTu, string listIdChungTu, string lns)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                SqlParameter yearOfBudgetParam = new SqlParameter("@YearOfBudget", namNganSach);
                SqlParameter budgetSourceParam = new SqlParameter("@BudgetSource", nguonNganSach);
                SqlParameter loaiParam = new SqlParameter("@Loai", loai);
                SqlParameter typeGetParam = new SqlParameter("@TypeGet", typeGet);
                SqlParameter agencyIdParam = new SqlParameter("@AgencyId", idDonVi);
                SqlParameter loaiChungTuParam = new SqlParameter("@LoaiChungTu", loaiChungTu);
                SqlParameter listIdChungTuParam = new SqlParameter("@ListIdChungTu", listIdChungTu);
                SqlParameter lnsParam = new SqlParameter("@Lns", lns);
                return ctx.FromSqlRaw<SktSoLieuChiTietMlnsQuery>("EXECUTE dbo.sp_skt_solieuchitiet_donvi0_index_2 @YearOfWork, @YearOfBudget, @BudgetSource, @Loai, @TypeGet, @AgencyId, @LoaiChungTu, @ListIdChungTu, @Lns",
                    yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, loaiParam, typeGetParam, agencyIdParam, loaiChungTuParam, listIdChungTuParam, lnsParam).ToList();
            }
        }

        public IEnumerable<SktSoLieuChiTietMLNSBudget> FindForFillBudget(EstimationVoucherDetailCriteria condition, string procedure)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var yearOfWorkParam = new SqlParameter("@YearOfWork", condition.YearOfWork);
                var yearOfBudgetParam = new SqlParameter("@YearOfBudget", condition.YearOfBudget);
                var budgetSourceParam = new SqlParameter("@BudgetSource", condition.BudgetSource);
                var iLoaiParam = new SqlParameter("@ILoai", condition.ILoai);
                var agencyIdParam = new SqlParameter("@AgencyId", condition.IdDonVi);
                var loaiChungTuParam = new SqlParameter("@LoaiChungTu", condition.LoaiChungTu);
                return ctx.FromSqlRaw<SktSoLieuChiTietMLNSBudget>($"EXECUTE dbo.{procedure} @YearOfWork, @YearOfBudget, @BudgetSource, @ILoai, @AgencyId, @LoaiChungTu", yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, iLoaiParam, agencyIdParam, loaiChungTuParam).ToList();
            }
        }

        public IEnumerable<ReportDuToanDauNamTongHopQuery> GetDataReportDuToanDauNamTongHop(int namLamViec, string idDonvi, string loaiChungTu, int loaiNNS, double donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter idDonviParam = new SqlParameter("@IdDonvi", idDonvi);
                SqlParameter loaiChungTuParam = new SqlParameter("@LoaiChungTu", loaiChungTu);
                SqlParameter loaiNNSParam = new SqlParameter("@iLoaiNNS", loaiNNS);
                SqlParameter donViTinhParam = new SqlParameter("@DonViTinh", donViTinh);
                return ctx.FromSqlRaw<ReportDuToanDauNamTongHopQuery>("EXECUTE dbo.sp_skt_rpt_dutoandaunam_tonghop @NamLamViec, @IdDonvi, @LoaiChungTu, @iLoaiNNS, @DonViTinh",
                    namLamViecParam, idDonviParam, loaiChungTuParam, loaiNNSParam, donViTinhParam).ToList();
            }
        }


        public IEnumerable<ReportDuToanDauNamTongHopQuery> GetDataReportDuToanDauNamTongHop_1(int namLamViec, string idDonvi, string loaiChungTu, int loaiNNS, double donViTinh, bool isInTheoTongHop)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter idDonviParam = new SqlParameter("@IdDonvi", idDonvi);
                SqlParameter loaiChungTuParam = new SqlParameter("@LoaiChungTu", loaiChungTu);
                SqlParameter loaiNNSParam = new SqlParameter("@iLoaiNNS", loaiNNS);
                SqlParameter donViTinhParam = new SqlParameter("@DonViTinh", donViTinh);
                SqlParameter isInTheoTongHopParam = new SqlParameter("@IsInTheoTongHop", isInTheoTongHop);
                return ctx.FromSqlRaw<ReportDuToanDauNamTongHopQuery>("EXECUTE dbo.sp_skt_rpt_dutoandaunam_tonghop_1 @NamLamViec, @IdDonvi, @LoaiChungTu, @iLoaiNNS, @DonViTinh, @IsInTheoTongHop",
                    namLamViecParam, idDonviParam, loaiChungTuParam, loaiNNSParam, donViTinhParam, isInTheoTongHopParam).ToList();
            }
        }

        public IEnumerable<ReportDuToanDauNamTongHopQuery> GetDataReportDuToanDauNamTongHop_2(int namLamViec, string idDonvi, string loaiChungTu, int loaiNNS, double donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter idDonviParam = new SqlParameter("@IdDonvi", idDonvi);
                SqlParameter loaiChungTuParam = new SqlParameter("@LoaiChungTu", loaiChungTu);
                SqlParameter loaiNNSParam = new SqlParameter("@iLoaiNNS", loaiNNS);
                SqlParameter donViTinhParam = new SqlParameter("@DonViTinh", donViTinh);
                return ctx.FromSqlRaw<ReportDuToanDauNamTongHopQuery>("EXECUTE dbo.sp_skt_rpt_dutoandaunam_tonghop_2 @NamLamViec, @IdDonvi, @LoaiChungTu, @iLoaiNNS, @DonViTinh",
                    namLamViecParam, idDonviParam, loaiChungTuParam, loaiNNSParam, donViTinhParam).ToList();
            }
        }

        public IEnumerable<NsMucLucNganSach> GetParentReportTongHop(int namLamViec, string xauNoiMa)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter xauNoiMaParam = new SqlParameter("@XauNoiMa", StringUtils.GetXauNoiMaParent(xauNoiMa));
                return ctx.Set<NsMucLucNganSach>().FromSql("EXECUTE dbo.sp_skt_rpt_parent_mlns_by_lns_xau_noi_ma_report_dutoandaunam @NamLamViec, @XauNoiMa",
                    namLamViecParam, xauNoiMaParam).ToList();
            }
        }

        public IEnumerable<NsMucLucNganSach> GetParentReportByLNS(int namLamViec, string lns)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter lnsParam = new SqlParameter("@LNS", lns);
                return ctx.Set<NsMucLucNganSach>().FromSql("EXECUTE dbo.sp_dt_rpt_parent_by_lns @NamLamViec, @LNS",
                    namLamViecParam, lnsParam).ToList();
            }
        }

        public IEnumerable<NsDtdauNamChungTuChiTiet> FindDataDonViLoai0ByCondition(int namLamViec, string loaiChungTu, string idDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDtdauNamChungTuChiTiets.Where(n => n.INamLamViec == namLamViec && n.ILoaiChungTu == loaiChungTu && n.IIdMaDonVi == idDonVi).ToList();
            }
        }

        public IEnumerable<ReportDuToanDauNamSoSanhQuery> GetDataReportDuToanDauNamSoSanh(string loai, string idDonvi, int namLamViec, double donViTinh, string loaiChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter loaiParam = new SqlParameter("@Loai", loai);
                SqlParameter idDonviParam = new SqlParameter("@IdDonVi", idDonvi);
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter donViTinhParam = new SqlParameter("@Dvt", donViTinh);
                SqlParameter loaiChungTuParam = new SqlParameter("@LoaiChungTu", loaiChungTu);
                return ctx.FromSqlRaw<ReportDuToanDauNamSoSanhQuery>("EXECUTE dbo.sp_rpt_skt_dutoandaunam_sosanh_skt @Loai, @IdDonVi, @NamLamViec, @Dvt, @LoaiChungTu",
                    loaiParam, idDonviParam, namLamViecParam, donViTinhParam, loaiChungTuParam).ToList();
            }
        }

        public IEnumerable<ReportDuToanDauNamSoSanhQuery> GetDataReportDuToanDauNamSoSanh_1(string loai, string idDonvi, int namLamViec, double donViTinh, string loaiChungTu, string lns)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter loaiParam = new SqlParameter("@Loai", loai);
                SqlParameter idDonviParam = new SqlParameter("@IdDonVi", idDonvi);
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter donViTinhParam = new SqlParameter("@Dvt", donViTinh);
                SqlParameter loaiChungTuParam = new SqlParameter("@LoaiChungTu", loaiChungTu);
                SqlParameter lnsParam = new SqlParameter("@Lns", lns);
                return ctx.FromSqlRaw<ReportDuToanDauNamSoSanhQuery>("EXECUTE dbo.sp_rpt_skt_dutoandaunam_sosanh_skt_1 @Loai, @IdDonVi, @NamLamViec, @Dvt, @LoaiChungTu, @Lns",
                    loaiParam, idDonviParam, namLamViecParam, donViTinhParam, loaiChungTuParam, lnsParam).ToList();
            }
        }

        public IEnumerable<ReportDuToanDauNamSoSanhQuery> GetDataReportDuToanDauNamSoSanhAll(string loai, string idDonvi, int namLamViec, double donViTinh, string loaiChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter loaiParam = new SqlParameter("@Loai", loai);
                SqlParameter idDonviParam = new SqlParameter("@IdDonVi", idDonvi);
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter donViTinhParam = new SqlParameter("@Dvt", donViTinh);
                SqlParameter loaiChungTuParam = new SqlParameter("@LoaiChungTu", loaiChungTu);
                return ctx.FromSqlRaw<ReportDuToanDauNamSoSanhQuery>("EXECUTE dbo.sp_rpt_skt_dutoandaunam_sosanh_skt_all @Loai, @IdDonVi, @NamLamViec, @Dvt, @LoaiChungTu",
                    loaiParam, idDonviParam, namLamViecParam, donViTinhParam, loaiChungTuParam).ToList();
            }
        }

        public IEnumerable<ReportDuToanDauNamSoSanhQuery> GetDataReportDuToanDauNamSoSanhAll_1(string loai, string idDonvi, int namLamViec, double donViTinh, string loaiChungTu, string lns)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter loaiParam = new SqlParameter("@Loai", loai);
                SqlParameter idDonviParam = new SqlParameter("@IdDonVi", idDonvi);
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter donViTinhParam = new SqlParameter("@Dvt", donViTinh);
                SqlParameter loaiChungTuParam = new SqlParameter("@LoaiChungTu", loaiChungTu);
                SqlParameter lnsParam = new SqlParameter("@Lns", lns);
                return ctx.FromSqlRaw<ReportDuToanDauNamSoSanhQuery>("EXECUTE dbo.sp_rpt_skt_dutoandaunam_sosanh_skt_all_1 @Loai, @IdDonVi, @NamLamViec, @Dvt, @LoaiChungTu, @Lns",
                    loaiParam, idDonviParam, namLamViecParam, donViTinhParam, loaiChungTuParam, lnsParam).ToList();
            }
        }

        public void GetHeaderReportChiNganSach(string kyHieu, int namLamViec, ref string header1, ref string header2)
        {
            header1 = string.Empty;
            header2 = string.Empty;
            using (var ctx = _contextFactory.CreateDbContext())
            {
                NsSktMucLuc item = ctx.NsSktMucLucs.Where(n => n.INamLamViec == namLamViec && n.SKyHieu == kyHieu).FirstOrDefault();
                if (item == null)
                {
                    return;
                }
                NsSktMucLuc itemParentSub = ctx.NsSktMucLucs.Where(n => n.INamLamViec == namLamViec && n.IIDMLSKT == item.IIDMLSKTCha).FirstOrDefault();
                if (itemParentSub == null)
                {
                    return;
                }
                else
                {
                    header2 = itemParentSub.SMoTa;
                }
                NsSktMucLuc itemParent = ctx.NsSktMucLucs.Where(n => n.INamLamViec == namLamViec && n.IIDMLSKT == itemParentSub.IIDMLSKTCha).FirstOrDefault();
                if (itemParent == null)
                {
                    return;
                }
                else
                {
                    header1 = itemParent.SMoTa;
                }
            }
        }

        public void DeleteByVoucherId(Guid voucherId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var voucherIdParam = new SqlParameter("@VoucherId", voucherId.ToString());
                ctx.Database.ExecuteSqlCommand($"DELETE NS_DTDauNam_ChungTuChiTiet WHERE iID_CTDTDauNam = @VoucherId", voucherIdParam);
            }
        }

        public IEnumerable<SktSoLieuChiTietMlnsQuery> FindByConditionDonVi0ChiTietDonVi(int namLamViec, int namNganSach, int nguonNganSach, int loai, int typeGet, string idDonVi, string loaiChungTu, string listChungTuTongHop, string lns)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", namLamViec);
                SqlParameter yearOfBudgetParam = new SqlParameter("@YearOfBudget", namNganSach);
                SqlParameter budgetSourceParam = new SqlParameter("@BudgetSource", nguonNganSach);
                SqlParameter loaiParam = new SqlParameter("@Loai", loai);
                SqlParameter typeGetParam = new SqlParameter("@TypeGet", typeGet);
                SqlParameter agencyIdParam = new SqlParameter("@AgencyId", idDonVi);
                SqlParameter loaiChungTuParam = new SqlParameter("@LoaiChungTu", loaiChungTu);
                SqlParameter listChungTuParam = new SqlParameter("@ListChungTuTongHop", listChungTuTongHop);
                SqlParameter lnsParam = new SqlParameter("@Lns", lns);
                return ctx.FromSqlRaw<SktSoLieuChiTietMlnsQuery>("EXECUTE dbo.sp_skt_solieuchitiet_donvi0_chitiet_index_2 @YearOfWork, @YearOfBudget, @BudgetSource, @Loai, @TypeGet, @AgencyId, @LoaiChungTu, @ListChungTuTongHop, @Lns",
                    yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, loaiParam, typeGetParam, agencyIdParam, loaiChungTuParam, listChungTuParam, lnsParam).ToList();
            }
        }

        public IEnumerable<CanCuDuToanNamTruocQuery> FindCanCuSoLapDuToanDauNam(int loaiChungTu, int loai, string idDonVi, int namLamViec, int namNganSach, int nguonNganSach)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter loaiChungTuParam = new SqlParameter("@LoaiChungTu", loaiChungTu);
                SqlParameter loaiParam = new SqlParameter("@ILoai", loai);
                SqlParameter idDV = new SqlParameter("@IdDonVi", idDonVi);
                SqlParameter namLV = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter namNganSachParam = new SqlParameter("@NamNganSach", namNganSach);
                SqlParameter nguonNganSachParam = new SqlParameter("@MaNguoiNganSach", nguonNganSach);
                return ctx.FromSqlRaw<CanCuDuToanNamTruocQuery>("EXECUTE dbo.sp_skt_get_can_cu_du_toan_lap_du_toan_dau_nam @LoaiChungTu, @ILoai, @IdDonVi, @NamLamViec, @NamNganSach, @MaNguoiNganSach", loaiChungTuParam, loaiParam, idDV, namLV, namNganSachParam, nguonNganSachParam).ToList();
            }
        }

        public IEnumerable<ReportChungTuDacThuDauNamPhanCapQuery> GetDataBaoCaoDuToanPhanBoNganSachDacThuPhanCap(List<string> listNganh, int namLamViec, int namNganSach, int nguonNganSach, int loaiChungTu, string maDonVi, bool IsInTongHop)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@YearOfWork", namLamViec);
                SqlParameter namNganSachParam = new SqlParameter("@YearOfBudget", namNganSach);
                SqlParameter nguonNganSachParam = new SqlParameter("@BudgetSource", nguonNganSach);
                SqlParameter loaaiChungTuParam = new SqlParameter("@LoaiNganSach", loaiChungTu);
                SqlParameter maDonViParam = new SqlParameter("@AgencyId", maDonVi);
                SqlParameter IsTongHopParam = new SqlParameter("@IsInTongHop", IsInTongHop);
                SqlParameter nganhParam = new SqlParameter("@Nganh", string.Join(",", listNganh));
                return ctx.FromSqlRaw<ReportChungTuDacThuDauNamPhanCapQuery>("EXECUTE dbo.sp_skt_get_data_report_phan_cap_ngan_sach_dac_thu @YearOfWork, @YearOfBudget, @BudgetSource, @LoaiNganSach, @AgencyId, @Nganh, @IsInTongHop",
                    namLamViecParam, namNganSachParam, nguonNganSachParam, loaaiChungTuParam, maDonViParam, nganhParam, IsTongHopParam).ToList();
            }
        }

        public IEnumerable<ReportDuToanDauNamTheoNganhPhuLucQuery> FindReportDuToanDauNamTheoNganhPhuLuc(string nganh, string idDonVi, int namLamViec, int namNganSach, int nguonNganSach, int loai, int donViTinh, bool bTongHop)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_du_toan_dau_nam_theo_nganh_phu_luc_tong_hop @Nganh , @MaDonVi, @NamLamViec, @NamNganSach, @NguonNganSach, @Loai, @dvt, @bTongHop";
                var parameters = new[]
                {
                    new SqlParameter("@Nganh", nganh),
                    new SqlParameter("@MaDonVi", idDonVi),
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@NamNganSach", namNganSach),
                    new SqlParameter("@NguonNganSach", nguonNganSach),
                    new SqlParameter("@Loai", loai),
                    new SqlParameter("@dvt", donViTinh),
                    new SqlParameter("@bTongHop", bTongHop)
                };
                return ctx.FromSqlRaw<ReportDuToanDauNamTheoNganhPhuLucQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<ReportDuToanDauNamTheoNganhPhuLucQuery> FindReportDuToanDauNamTheoNganhPhuLuc(string nganh, string idDonVi, string lstIdChungTu, int namLamViec, int namNganSach, int nguonNganSach, int loai, int donViTinh, bool bTongHop)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_du_toan_dau_nam_theo_nganh_phu_luc @Nganh , @MaDonVi, @IdChungTu, @NamLamViec, @NamNganSach, @NguonNganSach, @Loai, @dvt, @bTongHop";
                var parameters = new[]
                {
                    new SqlParameter("@Nganh", nganh),
                    new SqlParameter("@MaDonVi", idDonVi),
                    new SqlParameter("@IdChungTu", lstIdChungTu),
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@NamNganSach", namNganSach),
                    new SqlParameter("@NguonNganSach", nguonNganSach),
                    new SqlParameter("@Loai", loai),
                    new SqlParameter("@dvt", donViTinh),
                    new SqlParameter("@bTongHop", bTongHop)
                };
                return ctx.FromSqlRaw<ReportDuToanDauNamTheoNganhPhuLucQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<ReportDuToanDauNamTheoNganhPhuLucQuery> FindReportDuToanDauNamPhanCapTheoNganh(string nganh, string idDonVi, string lstIdChungTu, int namLamViec, int namNganSach, int nguonNganSach, int loai, int donViTinh, bool bTongHop)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_du_toan_dau_nam_phan_cap_theo_nganh @Nganh , @MaDonVi, @IdChungTu, @NamLamViec, @NamNganSach, @NguonNganSach, @Loai, @dvt, @bTongHop";
                var parameters = new[]
                {
                    new SqlParameter("@Nganh", nganh),
                    new SqlParameter("@MaDonVi", idDonVi),
                    new SqlParameter("@IdChungTu", lstIdChungTu),
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@NamNganSach", namNganSach),
                    new SqlParameter("@NguonNganSach", nguonNganSach),
                    new SqlParameter("@Loai", loai),
                    new SqlParameter("@dvt", donViTinh),
                    new SqlParameter("@bTongHop", bTongHop)
                };
                return ctx.FromSqlRaw<ReportDuToanDauNamTheoNganhPhuLucQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<ReportDuToanDauNamTongHopQuery> GetDataReportDuToanDauNamTongHop_TatCa(int namLamViec, string idDonvi, string loaiChungTu, int loaiNNS, double donViTinh, bool isInTheoTongHop)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter idDonviParam = new SqlParameter("@IdDonvi", idDonvi);
                SqlParameter loaiChungTuParam = new SqlParameter("@LoaiChungTu", loaiChungTu);
                SqlParameter loaiNNSParam = new SqlParameter("@iLoaiNNS", loaiNNS);
                SqlParameter donViTinhParam = new SqlParameter("@DonViTinh", donViTinh);
                SqlParameter isInTheoTongHopParam = new SqlParameter("@IsInTheoTongHop", isInTheoTongHop);
                return ctx.FromSqlRaw<ReportDuToanDauNamTongHopQuery>("EXECUTE dbo.sp_skt_rpt_dutoandaunam_tonghop_tatca @NamLamViec, @IdDonvi, @LoaiChungTu, @iLoaiNNS, @DonViTinh, @IsInTheoTongHop",
                    namLamViecParam, idDonviParam, loaiChungTuParam, loaiNNSParam, donViTinhParam, isInTheoTongHopParam).ToList();
            }
        }

        public IEnumerable<ReportBudgetEstimateQuery> ExportDuToanNganSach(int namLamViec, int namNganSach, int nguonNganSach, int maNguonNS, string maDonVi, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViecc", namLamViec);
                SqlParameter namNganSachParam = new SqlParameter("@NamNganSach", namNganSach);
                SqlParameter lnguonNganSachParam = new SqlParameter("@NguonNganSach", nguonNganSach);
                SqlParameter maNguonNSParam = new SqlParameter("@MaNguonNganSach", maNguonNS);
                SqlParameter maDonViParam = new SqlParameter("@MaDonVi", maDonVi);
                SqlParameter donViTinhParam = new SqlParameter("@DVT", donViTinh);
                return ctx.FromSqlRaw<ReportBudgetEstimateQuery>("EXECUTE dbo.sp_ns_rpt_dtdn_du_toan_ngan_sach @NamLamViecc, @NamNganSach, @NguonNganSach, @MaNguonNganSach, @MaDonVi, @DVT",
                    namLamViecParam, namNganSachParam, lnguonNganSachParam, maNguonNSParam, maDonViParam, donViTinhParam).ToList();
            }
        }

        public IEnumerable<ReportBudgetEstimateQuery> ExportSoSanhSKTDTDN(int namLamViec, int namNganSach, int nguonNganSach, int maNguonNS, string maDonVi, int loaiChungTu, bool inTheoTongHop, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter namNganSachParam = new SqlParameter("@NamNganSach", namNganSach);
                SqlParameter lnguonNganSachParam = new SqlParameter("@NguonNganSach", nguonNganSach);
                SqlParameter maNguonNSParam = new SqlParameter("@MaNguonNganSach", maNguonNS);
                SqlParameter maDonViParam = new SqlParameter("@MaDonVi", maDonVi);
                SqlParameter loaiChungTuParam = new SqlParameter("@LoaiChungTu", loaiChungTu);
                SqlParameter inTheoTongHopParam = new SqlParameter("@InTheoTongHop", inTheoTongHop);
                SqlParameter donViTinhParam = new SqlParameter("@DonViTinh", donViTinh);
                return ctx.FromSqlRaw<ReportBudgetEstimateQuery>("EXECUTE dbo.sp_ns_rpt_sosanh_sktdtdn @NamLamViec, @NamNganSach, @NguonNganSach, @MaNguonNganSach, @MaDonVi, @LoaiChungTu, @InTheoTongHop, @DonViTinh",
                    namLamViecParam, namNganSachParam, lnguonNganSachParam, maNguonNSParam, maDonViParam, loaiChungTuParam, inTheoTongHopParam, donViTinhParam).ToList();
            }
        }

        public IEnumerable<ReportBudgetEstimateQuery> ExportDuToanNganSachDonViNgang(int namLamViec, int namNganSach, int nguonNganSach, int maNguonNS, string maDonVi, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViecc", namLamViec);
                SqlParameter namNganSachParam = new SqlParameter("@NamNganSach", namNganSach);
                SqlParameter lnguonNganSachParam = new SqlParameter("@NguonNganSach", nguonNganSach);
                SqlParameter maNguonNSParam = new SqlParameter("@MaNguonNganSach", maNguonNS);
                SqlParameter maDonViParam = new SqlParameter("@MaDonVi", maDonVi);
                SqlParameter donViTinhParam = new SqlParameter("@DVT", donViTinh);
                return ctx.FromSqlRaw<ReportBudgetEstimateQuery>("EXECUTE dbo.sp_ns_rpt_dtdn_du_toan_ngan_sach_donvi_ngang @NamLamViecc, @NamNganSach, @NguonNganSach, @MaNguonNganSach, @MaDonVi, @DVT",
                    namLamViecParam, namNganSachParam, lnguonNganSachParam, maNguonNSParam, maDonViParam, donViTinhParam).ToList();
            }
        }

        public IEnumerable<ReportBudgetEstimateQuery> ExportDuToanNganSachDonViNgangExcel(int namLamViec, int namNganSach, int nguonNganSach, int maNguonNS, string maDonVi, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViecc", namLamViec);
                SqlParameter namNganSachParam = new SqlParameter("@NamNganSach", namNganSach);
                SqlParameter lnguonNganSachParam = new SqlParameter("@NguonNganSach", nguonNganSach);
                SqlParameter maNguonNSParam = new SqlParameter("@MaNguonNganSach", maNguonNS);
                SqlParameter maDonViParam = new SqlParameter("@MaDonVi", maDonVi);
                SqlParameter donViTinhParam = new SqlParameter("@DVT", donViTinh);
                return ctx.FromSqlRaw<ReportBudgetEstimateQuery>("EXECUTE dbo.sp_ns_rpt_dtdn_du_toan_ngan_sach_excel @NamLamViecc, @NamNganSach, @NguonNganSach, @MaNguonNganSach, @MaDonVi, @DVT",
                    namLamViecParam, namNganSachParam, lnguonNganSachParam, maNguonNSParam, maDonViParam, donViTinhParam).ToList();
            }
        }

        public IEnumerable<ReportBudgetEstimateQuery> ExportDuToanNganSachNSDTN(int namLamViec, int namNganSach, int nguonNganSach, int maNguonNS, string maDonVi, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViecc", namLamViec);
                SqlParameter namNganSachParam = new SqlParameter("@NamNganSach", namNganSach);
                SqlParameter lnguonNganSachParam = new SqlParameter("@NguonNganSach", nguonNganSach);
                SqlParameter maNguonNSParam = new SqlParameter("@MaNguonNganSach", maNguonNS);
                SqlParameter maDonViParam = new SqlParameter("@MaDonVi", maDonVi);
                SqlParameter donViTinhParam = new SqlParameter("@DVT", donViTinh);
                return ctx.FromSqlRaw<ReportBudgetEstimateQuery>("EXECUTE dbo.sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn @NamLamViecc, @NamNganSach, @NguonNganSach, @MaNguonNganSach, @MaDonVi, @DVT",
                    namLamViecParam, namNganSachParam, lnguonNganSachParam, maNguonNSParam, maDonViParam, donViTinhParam).ToList();
            }
        }

        public IEnumerable<ReportBudgetEstimateQuery> ExportDuToanNganSachDonViNgangNSDTN(int namLamViec, int namNganSach, int nguonNganSach, int maNguonNS, string maDonVi, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViecc", namLamViec);
                SqlParameter namNganSachParam = new SqlParameter("@NamNganSach", namNganSach);
                SqlParameter lnguonNganSachParam = new SqlParameter("@NguonNganSach", nguonNganSach);
                SqlParameter maNguonNSParam = new SqlParameter("@MaNguonNganSach", maNguonNS);
                SqlParameter maDonViParam = new SqlParameter("@MaDonVi", maDonVi);
                SqlParameter donViTinhParam = new SqlParameter("@DVT", donViTinh);
                return ctx.FromSqlRaw<ReportBudgetEstimateQuery>("EXECUTE dbo.sp_ns_rpt_dtdn_du_toan_ngan_sach_donvingang_nsdtn @NamLamViecc, @NamNganSach, @NguonNganSach, @MaNguonNganSach, @MaDonVi, @DVT",
                    namLamViecParam, namNganSachParam, lnguonNganSachParam, maNguonNSParam, maDonViParam, donViTinhParam).ToList();
            }
        }

        public IEnumerable<ReportBudgetEstimateQuery> ExportDuToanNganSachDonViNgangNSDTNExcel(int namLamViec, int namNganSach, int nguonNganSach, int maNguonNS, string maDonVi, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViecc", namLamViec);
                SqlParameter namNganSachParam = new SqlParameter("@NamNganSach", namNganSach);
                SqlParameter lnguonNganSachParam = new SqlParameter("@NguonNganSach", nguonNganSach);
                SqlParameter maNguonNSParam = new SqlParameter("@MaNguonNganSach", maNguonNS);
                SqlParameter maDonViParam = new SqlParameter("@MaDonVi", maDonVi);
                SqlParameter donViTinhParam = new SqlParameter("@DVT", donViTinh);
                return ctx.FromSqlRaw<ReportBudgetEstimateQuery>("EXECUTE dbo.sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn_excel @NamLamViecc, @NamNganSach, @NguonNganSach, @MaNguonNganSach, @MaDonVi, @DVT",
                    namLamViecParam, namNganSachParam, lnguonNganSachParam, maNguonNSParam, maDonViParam, donViTinhParam).ToList();
            }
        }
    }
}
