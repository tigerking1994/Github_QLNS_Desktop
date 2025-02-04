using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NsDtChungTuChiTietRepository : Repository<NsDtChungTuChiTiet>, INsDtChungTuChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NsDtChungTuChiTietRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NsDtChungTuChiTietQuery> FindByCond(EstimationVoucherDetailCriteria searchCondition, string procedure)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                List<string> lstProcedure = new List<string> { "rpt_du_toan_chi_tieu_tong_hop", "rpt_du_toan_chi_tieu_tong_hop_dieuchinh", "rpt_du_toan_chi_tieu_tong_hop_used_so_quyet_dinh", "rpt_du_toan_chi_tieu_LNS" };
                SqlParameter voucherIdParam = new SqlParameter();
                voucherIdParam.ParameterName = "@ChungTuId";
                voucherIdParam.DbType = DbType.String;
                voucherIdParam.Value = Guid.Empty.Equals(searchCondition.VoucherId) ? searchCondition.ChungTuId : searchCondition.VoucherId.ToString();
                voucherIdParam.Direction = ParameterDirection.Input;

                SqlParameter lnsParam = new SqlParameter();
                lnsParam.ParameterName = "@LNS";
                lnsParam.DbType = DbType.String;
                lnsParam.Value = searchCondition.LNS;
                lnsParam.Direction = ParameterDirection.Input;

                SqlParameter yearOfWorkParam = new SqlParameter();
                yearOfWorkParam.ParameterName = "@YearOfWork";
                yearOfWorkParam.DbType = DbType.Int32;
                yearOfWorkParam.Value = searchCondition.YearOfWork;
                yearOfWorkParam.Direction = ParameterDirection.Input;

                SqlParameter yearOfBudgetParam = new SqlParameter();
                yearOfBudgetParam.ParameterName = "@YearOfBudget";
                yearOfBudgetParam.DbType = DbType.Int32;
                yearOfBudgetParam.Value = searchCondition.YearOfBudget;
                yearOfBudgetParam.Direction = ParameterDirection.Input;

                SqlParameter budgetSourceParam = new SqlParameter();
                budgetSourceParam.ParameterName = "@BudgetSource";
                budgetSourceParam.DbType = DbType.Int32;
                budgetSourceParam.Value = searchCondition.BudgetSource;
                budgetSourceParam.Direction = ParameterDirection.Input;

                SqlParameter voucherDateParam = new SqlParameter();
                voucherDateParam.ParameterName = "@VoucherDate";
                voucherDateParam.DbType = DbType.DateTime;
                voucherDateParam.Value = searchCondition.VoucherDate;
                voucherDateParam.Direction = ParameterDirection.Input;

                SqlParameter soChugTuParam = new SqlParameter();
                soChugTuParam.ParameterName = "@SoChungTu";
                soChugTuParam.DbType = DbType.String;
                soChugTuParam.Value = string.IsNullOrEmpty(searchCondition.IdDotNhan) ? string.Empty : searchCondition.IdDotNhan;
                soChugTuParam.Direction = ParameterDirection.Input;

                if (lstProcedure.Contains(procedure))
                {
                    SqlParameter unitTypeParam = new SqlParameter();
                    unitTypeParam.ParameterName = "@UnitType";
                    unitTypeParam.DbType = DbType.Int32;
                    unitTypeParam.Value = searchCondition.DonViTinh;
                    unitTypeParam.Direction = ParameterDirection.Input;
                    if (!string.IsNullOrEmpty(searchCondition.UserName))
                    {
                        SqlParameter userNameParam = new SqlParameter();
                        userNameParam.ParameterName = "@UserName";
                        userNameParam.DbType = DbType.String;
                        userNameParam.Value = searchCondition.UserName;
                        userNameParam.Direction = ParameterDirection.Input;
                        return ctx.FromSqlRaw<NsDtChungTuChiTietQuery>($"EXECUTE {procedure} @ChungTuId, @LNS, @YearOfWork, @YearOfBudget, @BudgetSource, @VoucherDate, @SoChungTu,@UnitType, @UserName",
                                    voucherIdParam, lnsParam, yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, voucherDateParam, soChugTuParam, unitTypeParam, userNameParam).ToList();
                    }
                    else
                    {
                        return ctx.FromSqlRaw<NsDtChungTuChiTietQuery>($"EXECUTE {procedure} @ChungTuId, @LNS, @YearOfWork, @YearOfBudget, @BudgetSource, @VoucherDate, @SoChungTu, @UnitType",
                                    voucherIdParam, lnsParam, yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, voucherDateParam, soChugTuParam, unitTypeParam).ToList();
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(searchCondition.UserName))
                    {
                        SqlParameter userNameParam = new SqlParameter();
                        userNameParam.ParameterName = "@UserName";
                        userNameParam.DbType = DbType.String;
                        userNameParam.Value = searchCondition.UserName;
                        userNameParam.Direction = ParameterDirection.Input;
                        return ctx.FromSqlRaw<NsDtChungTuChiTietQuery>($"EXECUTE {procedure} @ChungTuId, @LNS, @YearOfWork, @YearOfBudget, @BudgetSource, @VoucherDate, @SoChungTu, @UserName",
                                    voucherIdParam, lnsParam, yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, voucherDateParam, soChugTuParam, userNameParam).ToList();
                    }
                    else
                    {
                        return ctx.FromSqlRaw<NsDtChungTuChiTietQuery>($"EXECUTE {procedure} @ChungTuId, @LNS, @YearOfWork, @YearOfBudget, @BudgetSource, @VoucherDate, @SoChungTu",
                                    voucherIdParam, lnsParam, yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, voucherDateParam, soChugTuParam).ToList();
                    }
                }

            }
        }

        public IEnumerable<NsDtChungTuChiTietQuery> FindByCondition(EstimationVoucherDetailCriteria searchCondition, string procedure)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter voucherIdParam = new SqlParameter();
                voucherIdParam.ParameterName = "@ChungTuId";
                voucherIdParam.DbType = DbType.String;
                if (!searchCondition.VoucherId.IsNullOrEmpty())
                {
                    voucherIdParam.Value = searchCondition.VoucherId.ToString();
                }
                else
                {
                    voucherIdParam.Value = searchCondition.VoucherIds.ToString();
                }
                voucherIdParam.Direction = ParameterDirection.Input;

                SqlParameter lnsParam = new SqlParameter();
                lnsParam.ParameterName = "@LNS";
                lnsParam.DbType = DbType.String;
                lnsParam.Value = searchCondition.LNS.ToString();
                lnsParam.Direction = ParameterDirection.Input;

                SqlParameter yearOfWorkParam = new SqlParameter();
                yearOfWorkParam.ParameterName = "@YearOfWork";
                yearOfWorkParam.DbType = DbType.Int32;
                yearOfWorkParam.Value = searchCondition.YearOfWork;
                yearOfWorkParam.Direction = ParameterDirection.Input;

                SqlParameter yearOfBudgetParam = new SqlParameter();
                yearOfBudgetParam.ParameterName = "@YearOfBudget";
                yearOfBudgetParam.DbType = DbType.Int32;
                yearOfBudgetParam.Value = searchCondition.YearOfBudget;
                yearOfBudgetParam.Direction = ParameterDirection.Input;

                SqlParameter budgetSourceParam = new SqlParameter();
                budgetSourceParam.ParameterName = "@BudgetSource";
                budgetSourceParam.DbType = DbType.Int32;
                budgetSourceParam.Value = searchCondition.BudgetSource;
                budgetSourceParam.Direction = ParameterDirection.Input;

                SqlParameter userNameParam = new SqlParameter();
                userNameParam.ParameterName = "@UserName";
                userNameParam.DbType = DbType.String;
                userNameParam.Value = searchCondition.UserName;
                userNameParam.Direction = ParameterDirection.Input;

                return ctx.FromSqlRaw<NsDtChungTuChiTietQuery>(string.Format("EXECUTE {0} @ChungTuId, @LNS, @YearOfWork, @YearOfBudget, @BudgetSource, @UserName", procedure),
                 voucherIdParam, lnsParam, yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, userNameParam).ToList();
            }
        }

        public IEnumerable<NsDtChungTuChiTietQuery> FindReportNhanPhanBoDuToanTheoDot(EstimationVoucherDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter voucherIdParam = new SqlParameter();
                voucherIdParam.ParameterName = "@ChungTuId";
                voucherIdParam.DbType = DbType.String;
                if (!searchCondition.VoucherId.IsNullOrEmpty())
                {
                    voucherIdParam.Value = searchCondition.VoucherId.ToString();
                }
                else
                {
                    voucherIdParam.Value = searchCondition.VoucherIds.ToString();
                }
                voucherIdParam.Direction = ParameterDirection.Input;

                SqlParameter lnsParam = new SqlParameter();
                lnsParam.ParameterName = "@LNS";
                lnsParam.DbType = DbType.String;
                lnsParam.Value = searchCondition.LNS.ToString();
                lnsParam.Direction = ParameterDirection.Input;

                SqlParameter yearOfWorkParam = new SqlParameter();
                yearOfWorkParam.ParameterName = "@YearOfWork";
                yearOfWorkParam.DbType = DbType.Int32;
                yearOfWorkParam.Value = searchCondition.YearOfWork;
                yearOfWorkParam.Direction = ParameterDirection.Input;

                SqlParameter yearOfBudgetParam = new SqlParameter();
                yearOfBudgetParam.ParameterName = "@YearOfBudget";
                yearOfBudgetParam.DbType = DbType.Int32;
                yearOfBudgetParam.Value = searchCondition.YearOfBudget;
                yearOfBudgetParam.Direction = ParameterDirection.Input;

                SqlParameter budgetSourceParam = new SqlParameter();
                budgetSourceParam.ParameterName = "@BudgetSource";
                budgetSourceParam.DbType = DbType.Int32;
                budgetSourceParam.Value = searchCondition.BudgetSource;
                budgetSourceParam.Direction = ParameterDirection.Input;

                SqlParameter userNameParam = new SqlParameter();
                userNameParam.ParameterName = "@UserName";
                userNameParam.DbType = DbType.String;
                userNameParam.Value = searchCondition.UserName;
                userNameParam.Direction = ParameterDirection.Input;

                return ctx.FromSqlRaw<NsDtChungTuChiTietQuery>(string.Format("EXECUTE sp_dt_rpt_nhan_phanbo_dutoan_theodot @ChungTuId, @LNS, @YearOfWork, @YearOfBudget, @BudgetSource, @UserName"),
                 voucherIdParam, lnsParam, yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, userNameParam).ToList();
            }
        }


        public IEnumerable<NsDtChungTuChiTietCongKhaiQuery> FindDtChungTuChiTietCongKhai(EstimationVoucherDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter danhSachId = new SqlParameter("@ListIdPublic", searchCondition.LNS);
                SqlParameter namLamViec = new SqlParameter("@YearOfWork", searchCondition.YearOfWork);
                SqlParameter namNganSach = new SqlParameter("@YearOfBudget", searchCondition.YearOfBudget);
                SqlParameter nguonNganSach = new SqlParameter("@BudgetSource", searchCondition.BudgetSource);
                SqlParameter thoiGian = new SqlParameter("@Time", searchCondition.IThangQuy);

                return ctx.FromSqlRaw<NsDtChungTuChiTietCongKhaiQuery>("EXECUTE sp_dt_dutoan_mucluccongkhai @ListIdPublic, @YearOfWork, @YearOfBudget, @BudgetSource, @Time",
                                danhSachId, namLamViec, namNganSach, nguonNganSach, thoiGian).ToList();
            }
        }

        public IEnumerable<NsQtCongKhaiThuChi> FindRptQtCongKhaiThuChi(EstimationVoucherDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter danhSachId = new SqlParameter("@MaMucLucs", searchCondition.LNS);
                SqlParameter namLamViec = new SqlParameter("@YearOfWork", searchCondition.YearOfWork);
                SqlParameter namNganSach = new SqlParameter("@YearOfBudget", searchCondition.YearOfBudget);
                SqlParameter nguonNganSach = new SqlParameter("@BudgetSource", searchCondition.BudgetSource);
                SqlParameter donViTinh = new SqlParameter("@DonViTinh", searchCondition.DonViTinh);
                SqlParameter thoiGian = new SqlParameter("@Time", searchCondition.IThangQuy);

                return ctx.FromSqlRaw<NsQtCongKhaiThuChi>("EXECUTE sp_rpt_qt_congkhai_thuchi @MaMucLucs, @YearOfWork, @YearOfBudget, @BudgetSource, @Time, @DonViTinh",
                                danhSachId, namLamViec, namNganSach, nguonNganSach, thoiGian, donViTinh).ToList();
            }
        }

        public IEnumerable<NsQtCongKhaiThuChi> FindRptQtCongKhaiThuChiDonVi(EstimationVoucherDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter danhSachId = new SqlParameter("@MaMucLucs", searchCondition.LNS);
                SqlParameter namLamViec = new SqlParameter("@YearOfWork", searchCondition.YearOfWork);
                SqlParameter namNganSach = new SqlParameter("@YearOfBudget", searchCondition.YearOfBudget);
                SqlParameter nguonNganSach = new SqlParameter("@BudgetSource", searchCondition.BudgetSource);
                SqlParameter maDonVis = new SqlParameter("@MaDonVis", searchCondition.IIDMaDonVis);
                SqlParameter donViTinh = new SqlParameter("@DonViTinh", searchCondition.DonViTinh);

                return ctx.FromSqlRaw<NsQtCongKhaiThuChi>("EXECUTE sp_rpt_qt_congkhai_thuchi_donvi @MaMucLucs, @YearOfWork, @YearOfBudget, @BudgetSource, @MaDonVis, @DonViTinh",
                                danhSachId, namLamViec, namNganSach, nguonNganSach, maDonVis, donViTinh).ToList();
            }
        }

        public IEnumerable<NsDtChungTuChiTietCongKhaiQuery> FindDtChungTuChiTietCongKhaiClone(EstimationVoucherDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter danhSachId = new SqlParameter("@ListIdPublic", searchCondition.LNS);
                SqlParameter namLamViec = new SqlParameter("@YearOfWork", searchCondition.YearOfWork);
                SqlParameter namNganSach = new SqlParameter("@YearOfBudget", searchCondition.YearOfBudget);
                SqlParameter nguonNganSach = new SqlParameter("@BudgetSource", searchCondition.BudgetSource);
                SqlParameter thoiGian = new SqlParameter("@Time", searchCondition.IThangQuy);
                SqlParameter chungTuIds = new SqlParameter("@VoucherIds", searchCondition.VoucherIds);
                SqlParameter unitType = new SqlParameter("@UnitType", searchCondition.DonViTinh);

                return ctx.FromSqlRaw<NsDtChungTuChiTietCongKhaiQuery>("EXECUTE sp_dt_dutoan_mucluccongkhai_clone @ListIdPublic, @YearOfWork, @YearOfBudget, @BudgetSource, @Time, @VoucherIds,@UnitType",
                                danhSachId, namLamViec, namNganSach, nguonNganSach, thoiGian, chungTuIds, unitType).ToList();
            }
        }

        public IEnumerable<NsDtChungTuChiTietQuery> FindReportCongKhaiTaiChinh(EstimationVoucherDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter voucherIdParam = new SqlParameter();
                voucherIdParam.ParameterName = "@ChungTuId";
                voucherIdParam.DbType = DbType.String;
                voucherIdParam.Value = Guid.Empty.Equals(searchCondition.VoucherId) ? searchCondition.ChungTuId : searchCondition.VoucherId.ToString();
                voucherIdParam.Direction = ParameterDirection.Input;

                SqlParameter lnsParam = new SqlParameter();
                lnsParam.ParameterName = "@LNS";
                lnsParam.DbType = DbType.String;
                lnsParam.Value = searchCondition.LNS;
                lnsParam.Direction = ParameterDirection.Input;

                SqlParameter yearOfWorkParam = new SqlParameter();
                yearOfWorkParam.ParameterName = "@YearOfWork";
                yearOfWorkParam.DbType = DbType.Int32;
                yearOfWorkParam.Value = searchCondition.YearOfWork;
                yearOfWorkParam.Direction = ParameterDirection.Input;

                SqlParameter yearOfBudgetParam = new SqlParameter();
                yearOfBudgetParam.ParameterName = "@YearOfBudget";
                yearOfBudgetParam.DbType = DbType.Int32;
                yearOfBudgetParam.Value = searchCondition.YearOfBudget;
                yearOfBudgetParam.Direction = ParameterDirection.Input;

                SqlParameter budgetSourceParam = new SqlParameter();
                budgetSourceParam.ParameterName = "@BudgetSource";
                budgetSourceParam.DbType = DbType.Int32;
                budgetSourceParam.Value = searchCondition.BudgetSource;
                budgetSourceParam.Direction = ParameterDirection.Input;

                SqlParameter voucherDateParam = new SqlParameter();
                voucherDateParam.ParameterName = "@VoucherDate";
                voucherDateParam.DbType = DbType.DateTime;
                voucherDateParam.Value = searchCondition.VoucherDate;
                voucherDateParam.Direction = ParameterDirection.Input;

                SqlParameter soChugTuParam = new SqlParameter();
                soChugTuParam.ParameterName = "@SoChungTu";
                soChugTuParam.DbType = DbType.String;
                soChugTuParam.Value = string.IsNullOrEmpty(searchCondition.IdDotNhan) ? string.Empty : searchCondition.IdDotNhan;
                soChugTuParam.Direction = ParameterDirection.Input;

                SqlParameter dvtParam = new SqlParameter();
                dvtParam.ParameterName = "@dvt";
                dvtParam.DbType = DbType.Int32;
                dvtParam.Value = searchCondition.dvt;
                dvtParam.Direction = ParameterDirection.Input;


                return ctx.FromSqlRaw<NsDtChungTuChiTietQuery>($"EXECUTE rpt_du_toan_chi_tieu_LNS_1 @ChungTuId, @LNS, @YearOfWork, @YearOfBudget, @BudgetSource, @VoucherDate, @SoChungTu, @dvt",
                    voucherIdParam, lnsParam, yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, voucherDateParam, soChugTuParam, dvtParam).ToList();

            }
        }

        public IEnumerable<NsDtChungTuChiTiet> FindByIdChungTu(string idChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                List<string> listId = idChungTu.Split(',').ToList();
                return ctx.NsDtChungTuChiTiets.Where(n => listId.Contains(n.IIdDtchungTu.ToString())).ToList();
            }
        }

        public NsDtChungTuChiTiet FindByIdMlns(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDtChungTuChiTiets.Where(n => n.IIdMlns == id).FirstOrDefault();
            }
        }

        public IEnumerable<DuToanDonViQuery> FindDuToanDonvi(DuToanDonViCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter dvt = new SqlParameter("@dvt", searchCondition.dvt);
                SqlParameter namLamViec = new SqlParameter("@NamLamViec", searchCondition.NamLamViec);
                SqlParameter namNganSach = new SqlParameter("@NamNganSach", searchCondition.NamNganSach);
                SqlParameter nguonNganSach = new SqlParameter("@NguonNganSach", searchCondition.NguonNganSach);
                SqlParameter idDonVi = new SqlParameter("@id_donvi", searchCondition.IdDonVi);
                SqlParameter ngayChungTuParam = new SqlParameter("@NgayChungTu", searchCondition.NgayChungTu);
                SqlParameter lnsParam = new SqlParameter("@lns", searchCondition.LNS);
                SqlParameter khoaParam = new SqlParameter("@bkhoa", searchCondition.bKhoa);

                return ctx.FromSqlRaw<DuToanDonViQuery>("EXECUTE sp_dt_dutoan_donvi @dvt, @NamLamViec, @NamNganSach, @NguonNganSach, @id_donvi, @NgayChungTu, @lns, @bkhoa ",
                                dvt, namLamViec, namNganSach, nguonNganSach, idDonVi, ngayChungTuParam, lnsParam, khoaParam).ToList();
            }
        }

        public IEnumerable<ReportDuToanNhanPhanBoTheoDotQuery> FindDuToanTheoDot(EstimationVoucherDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter LnsParam = new SqlParameter();
                LnsParam.ParameterName = "@LNS";
                LnsParam.DbType = DbType.String;
                LnsParam.Value = searchCondition.LNS;
                LnsParam.Direction = ParameterDirection.Input;

                SqlParameter yearOfWorkParam = new SqlParameter();
                yearOfWorkParam.ParameterName = "@YearOfWork";
                yearOfWorkParam.DbType = DbType.Int32;
                yearOfWorkParam.Value = searchCondition.YearOfWork;
                yearOfWorkParam.Direction = ParameterDirection.Input;

                SqlParameter yearOfBudgetParam = new SqlParameter();
                yearOfBudgetParam.ParameterName = "@YearOfBudget";
                yearOfBudgetParam.DbType = DbType.Int32;
                yearOfBudgetParam.Value = searchCondition.YearOfBudget;
                yearOfBudgetParam.Direction = ParameterDirection.Input;

                SqlParameter budgetSourceParam = new SqlParameter();
                budgetSourceParam.ParameterName = "@BudgetSource";
                budgetSourceParam.DbType = DbType.Int32;
                budgetSourceParam.Value = searchCondition.BudgetSource;
                budgetSourceParam.Direction = ParameterDirection.Input;

                SqlParameter voucherIdParam = new SqlParameter();
                voucherIdParam.ParameterName = "@ChungTuId";
                voucherIdParam.DbType = DbType.String;
                voucherIdParam.Value = searchCondition.ChungTuId.ToString();
                voucherIdParam.Direction = ParameterDirection.Input;


                SqlParameter donViTinhParam = new SqlParameter();
                donViTinhParam.ParameterName = "@dvt";
                donViTinhParam.DbType = DbType.Int32;
                donViTinhParam.Value = searchCondition.dvt;
                donViTinhParam.Direction = ParameterDirection.Input;

                return ctx.FromSqlRaw<ReportDuToanNhanPhanBoTheoDotQuery>("EXECUTE sp_dt_rpt_dutoan_theodot @LNS,@YearOfWork,@YearOfBudget,@BudgetSource," +
                    "@ChungTuId, @dvt", LnsParam, yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, voucherIdParam, donViTinhParam).ToList();
            }
        }

        public IEnumerable<NsDtChungTuChiTietQuery> FindDuToanTheoNganh(EstimationVoucherDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter LnsParam = new SqlParameter();
                LnsParam.ParameterName = "@LNS";
                LnsParam.DbType = DbType.String;
                LnsParam.Value = searchCondition.LNS;
                LnsParam.Direction = ParameterDirection.Input;

                SqlParameter yearOfWorkParam = new SqlParameter();
                yearOfWorkParam.ParameterName = "@YearOfWork";
                yearOfWorkParam.DbType = DbType.Int32;
                yearOfWorkParam.Value = searchCondition.YearOfWork;
                yearOfWorkParam.Direction = ParameterDirection.Input;

                SqlParameter yearOfBudgetParam = new SqlParameter();
                yearOfBudgetParam.ParameterName = "@YearOfBudget";
                yearOfBudgetParam.DbType = DbType.Int32;
                yearOfBudgetParam.Value = searchCondition.YearOfBudget;
                yearOfBudgetParam.Direction = ParameterDirection.Input;

                SqlParameter budgetSourceParam = new SqlParameter();
                budgetSourceParam.ParameterName = "@BudgetSource";
                budgetSourceParam.DbType = DbType.Int32;
                budgetSourceParam.Value = searchCondition.BudgetSource;
                budgetSourceParam.Direction = ParameterDirection.Input;

                SqlParameter voucherIdParam = new SqlParameter();
                voucherIdParam.ParameterName = "@ChungTuId";
                voucherIdParam.DbType = DbType.String;
                voucherIdParam.Value = searchCondition.ChungTuId;
                voucherIdParam.Direction = ParameterDirection.Input;

                SqlParameter idNganhParam = new SqlParameter();
                idNganhParam.ParameterName = "@IdNganh";
                idNganhParam.DbType = DbType.String;
                idNganhParam.Value = searchCondition.IdNganh;
                idNganhParam.Direction = ParameterDirection.Input;

                return ctx.FromSqlRaw<NsDtChungTuChiTietQuery>("EXECUTE sp_dt_rpt_dutoan_theonganh @LNS,@YearOfWork,@YearOfBudget,@BudgetSource," +
                    "@ChungTuId,@IdNganh", LnsParam, yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, voucherIdParam, idNganhParam).ToList();
            }
        }

        public IEnumerable<NsDtChungTuChiTiet> FindDuToanTongHop(EstimationVoucherDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter LnsParam = new SqlParameter();
                LnsParam.ParameterName = "@LNS";
                LnsParam.DbType = DbType.String;
                LnsParam.Value = searchCondition.LNS;
                LnsParam.Direction = ParameterDirection.Input;

                SqlParameter voucherIdParam = new SqlParameter();
                voucherIdParam.ParameterName = "@ChungTuId";
                voucherIdParam.DbType = DbType.String;
                voucherIdParam.Value = searchCondition.ChungTuId;
                voucherIdParam.Direction = ParameterDirection.Input;

                SqlParameter yearOfWorkParam = new SqlParameter();
                yearOfWorkParam.ParameterName = "@YearOfWork";
                yearOfWorkParam.DbType = DbType.Int32;
                yearOfWorkParam.Value = searchCondition.YearOfWork;
                yearOfWorkParam.Direction = ParameterDirection.Input;

                SqlParameter yearOfBudgetParam = new SqlParameter();
                yearOfBudgetParam.ParameterName = "@YearOfBudget";
                yearOfBudgetParam.DbType = DbType.Int32;
                yearOfBudgetParam.Value = searchCondition.YearOfBudget;
                yearOfBudgetParam.Direction = ParameterDirection.Input;

                SqlParameter budgetSourceParam = new SqlParameter();
                budgetSourceParam.ParameterName = "@BudgetSource";
                budgetSourceParam.DbType = DbType.Int32;
                budgetSourceParam.Value = searchCondition.BudgetSource;
                budgetSourceParam.Direction = ParameterDirection.Input;

                return ctx.FromSqlRaw<NsDtChungTuChiTiet>("EXECUTE sp_dt_rpt_dutoan_tonghop @LNS,@ChungTuId,@YearOfWork,@YearOfBudget,@BudgetSource",
                    LnsParam, voucherIdParam, yearOfWorkParam, yearOfBudgetParam, budgetSourceParam).ToList();
            }
        }

        public IEnumerable<NsDtChungTuChiTietQuery> FindByLuyKePhanBoTongHop(EstimationVoucherDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter voucherIdParam = new SqlParameter();
                voucherIdParam.ParameterName = "@ChungTuId";
                voucherIdParam.DbType = DbType.String;
                voucherIdParam.Value = searchCondition.VoucherId.ToString();
                voucherIdParam.Direction = ParameterDirection.Input;

                SqlParameter lnsParam = new SqlParameter();
                lnsParam.ParameterName = "@LNS";
                lnsParam.DbType = DbType.String;
                lnsParam.Value = searchCondition.LNS.ToString();
                lnsParam.Direction = ParameterDirection.Input;

                SqlParameter yearOfWorkParam = new SqlParameter();
                yearOfWorkParam.ParameterName = "@YearOfWork";
                yearOfWorkParam.DbType = DbType.Int32;
                yearOfWorkParam.Value = searchCondition.YearOfWork;
                yearOfWorkParam.Direction = ParameterDirection.Input;

                SqlParameter yearOfBudgetParam = new SqlParameter();
                yearOfBudgetParam.ParameterName = "@YearOfBudget";
                yearOfBudgetParam.DbType = DbType.Int32;
                yearOfBudgetParam.Value = searchCondition.YearOfBudget;
                yearOfBudgetParam.Direction = ParameterDirection.Input;

                SqlParameter budgetSourceParam = new SqlParameter();
                budgetSourceParam.ParameterName = "@BudgetSource";
                budgetSourceParam.DbType = DbType.Int32;
                budgetSourceParam.Value = searchCondition.BudgetSource;
                budgetSourceParam.Direction = ParameterDirection.Input;

                SqlParameter voucherDateParam = new SqlParameter();
                voucherDateParam.ParameterName = "@VoucherDate";
                voucherDateParam.DbType = DbType.DateTime;
                voucherDateParam.Value = searchCondition.VoucherDate;
                voucherDateParam.Direction = ParameterDirection.Input;

                SqlParameter soChugTuParam = new SqlParameter();
                soChugTuParam.ParameterName = "@SoChungTu";
                soChugTuParam.DbType = DbType.String;
                soChugTuParam.Value = string.IsNullOrEmpty(searchCondition.IdDotNhan) ? string.Empty : searchCondition.IdDotNhan; ;
                soChugTuParam.Direction = ParameterDirection.Input;

                return ctx.FromSqlRaw<NsDtChungTuChiTietQuery>("EXECUTE sp_dt_rpt_chi_tieu_luy_ke_phan_bo_tong_hop @ChungTuId, @LNS, @YearOfWork, @YearOfBudget, @BudgetSource, @VoucherDate, @SoChungTu",
                                voucherIdParam, lnsParam, yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, voucherDateParam, soChugTuParam).ToList();
            }
        }

        public IEnumerable<NsDtChungTuChiTietQuery> FindByLuyKePhanBoTongHopClone(EstimationVoucherDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter voucherIdParam = new SqlParameter();
                voucherIdParam.ParameterName = "@ChungTuIds";
                voucherIdParam.DbType = DbType.String;
                voucherIdParam.Value = searchCondition.VoucherIds;
                voucherIdParam.Direction = ParameterDirection.Input;

                SqlParameter lnsParam = new SqlParameter();
                lnsParam.ParameterName = "@LNS";
                lnsParam.DbType = DbType.String;
                lnsParam.Value = searchCondition.LNS.ToString();
                lnsParam.Direction = ParameterDirection.Input;

                SqlParameter yearOfWorkParam = new SqlParameter();
                yearOfWorkParam.ParameterName = "@YearOfWork";
                yearOfWorkParam.DbType = DbType.Int32;
                yearOfWorkParam.Value = searchCondition.YearOfWork;
                yearOfWorkParam.Direction = ParameterDirection.Input;

                SqlParameter yearOfBudgetParam = new SqlParameter();
                yearOfBudgetParam.ParameterName = "@YearOfBudget";
                yearOfBudgetParam.DbType = DbType.Int32;
                yearOfBudgetParam.Value = searchCondition.YearOfBudget;
                yearOfBudgetParam.Direction = ParameterDirection.Input;

                SqlParameter budgetSourceParam = new SqlParameter();
                budgetSourceParam.ParameterName = "@BudgetSource";
                budgetSourceParam.DbType = DbType.Int32;
                budgetSourceParam.Value = searchCondition.BudgetSource;
                budgetSourceParam.Direction = ParameterDirection.Input;

                SqlParameter voucherDateParam = new SqlParameter();
                voucherDateParam.ParameterName = "@VoucherDate";
                voucherDateParam.DbType = DbType.DateTime;
                voucherDateParam.Value = searchCondition.VoucherDate;
                voucherDateParam.Direction = ParameterDirection.Input;

                SqlParameter soChugTuParam = new SqlParameter();
                soChugTuParam.ParameterName = "@SoChungTu";
                soChugTuParam.DbType = DbType.String;
                soChugTuParam.Value = string.IsNullOrEmpty(searchCondition.IdDotNhan) ? string.Empty : searchCondition.IdDotNhan; ;
                soChugTuParam.Direction = ParameterDirection.Input;

                return ctx.FromSqlRaw<NsDtChungTuChiTietQuery>("EXECUTE sp_dt_rpt_chi_tieu_luy_ke_phan_bo_tong_hop @ChungTuIds, @LNS, @YearOfWork, @YearOfBudget, @BudgetSource, @VoucherDate, @SoChungTu",
                                voucherIdParam, lnsParam, yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, voucherDateParam, soChugTuParam).ToList();
            }
        }

        public IEnumerable<NsDtChungTuChiTietQuery> FindByLuyKePhanBoTongHopSummary(EstimationVoucherDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter voucherIdParam = new SqlParameter();
                voucherIdParam.ParameterName = "@ChungTuId";
                voucherIdParam.DbType = DbType.String;
                voucherIdParam.Value = searchCondition.VoucherIds;
                voucherIdParam.Direction = ParameterDirection.Input;

                SqlParameter lnsParam = new SqlParameter();
                lnsParam.ParameterName = "@LNS";
                lnsParam.DbType = DbType.String;
                lnsParam.Value = searchCondition.LNS.ToString();
                lnsParam.Direction = ParameterDirection.Input;

                SqlParameter yearOfWorkParam = new SqlParameter();
                yearOfWorkParam.ParameterName = "@YearOfWork";
                yearOfWorkParam.DbType = DbType.Int32;
                yearOfWorkParam.Value = searchCondition.YearOfWork;
                yearOfWorkParam.Direction = ParameterDirection.Input;

                SqlParameter yearOfBudgetParam = new SqlParameter();
                yearOfBudgetParam.ParameterName = "@YearOfBudget";
                yearOfBudgetParam.DbType = DbType.Int32;
                yearOfBudgetParam.Value = searchCondition.YearOfBudget;
                yearOfBudgetParam.Direction = ParameterDirection.Input;

                SqlParameter budgetSourceParam = new SqlParameter();
                budgetSourceParam.ParameterName = "@BudgetSource";
                budgetSourceParam.DbType = DbType.Int32;
                budgetSourceParam.Value = searchCondition.BudgetSource;
                budgetSourceParam.Direction = ParameterDirection.Input;

                return ctx.FromSqlRaw<NsDtChungTuChiTietQuery>("EXECUTE sp_dt_rpt_chi_tieu_luy_ke_phan_bo_tong_hop_summary @ChungTuId, @LNS, @YearOfWork, @YearOfBudget, @BudgetSource",
                                voucherIdParam, lnsParam, yearOfWorkParam, yearOfBudgetParam, budgetSourceParam).ToList();
            }
        }

        public IEnumerable<NsDtChungTuChiTietQuery> FindByLuyKeTongHop(EstimationVoucherDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter voucherIdParam = new SqlParameter();
                voucherIdParam.ParameterName = "@ChungTuId";
                voucherIdParam.DbType = DbType.String;
                voucherIdParam.Value = searchCondition.VoucherId.ToString();
                voucherIdParam.Direction = ParameterDirection.Input;

                SqlParameter lnsParam = new SqlParameter();
                lnsParam.ParameterName = "@LNS";
                lnsParam.DbType = DbType.String;
                lnsParam.Value = searchCondition.LNS.ToString();
                lnsParam.Direction = ParameterDirection.Input;

                SqlParameter yearOfWorkParam = new SqlParameter();
                yearOfWorkParam.ParameterName = "@YearOfWork";
                yearOfWorkParam.DbType = DbType.Int32;
                yearOfWorkParam.Value = searchCondition.YearOfWork;
                yearOfWorkParam.Direction = ParameterDirection.Input;

                SqlParameter yearOfBudgetParam = new SqlParameter();
                yearOfBudgetParam.ParameterName = "@YearOfBudget";
                yearOfBudgetParam.DbType = DbType.Int32;
                yearOfBudgetParam.Value = searchCondition.YearOfBudget;
                yearOfBudgetParam.Direction = ParameterDirection.Input;

                SqlParameter budgetSourceParam = new SqlParameter();
                budgetSourceParam.ParameterName = "@BudgetSource";
                budgetSourceParam.DbType = DbType.Int32;
                budgetSourceParam.Value = searchCondition.BudgetSource;
                budgetSourceParam.Direction = ParameterDirection.Input;

                SqlParameter isPhanBoParam = new SqlParameter();
                isPhanBoParam.ParameterName = "@IsPhanBo";
                isPhanBoParam.DbType = DbType.Boolean;
                isPhanBoParam.Value = searchCondition.IsPhanBo;
                isPhanBoParam.Direction = ParameterDirection.Input;

                return ctx.FromSqlRaw<NsDtChungTuChiTietQuery>("EXECUTE sp_dt_rpt_chi_tieu_luy_ke_tong_hop @ChungTuId, @LNS, @YearOfWork, @YearOfBudget, @BudgetSource, @IsPhanBo",
                    voucherIdParam, lnsParam, yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, isPhanBoParam).ToList();
            }
        }

        public IEnumerable<NsDtChungTuChiTietQuery> FindByLuyKeTongHopClone(EstimationVoucherDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter voucherIdParam = new SqlParameter();
                voucherIdParam.ParameterName = "@ChungTuIds";
                voucherIdParam.DbType = DbType.String;
                voucherIdParam.Value = searchCondition.VoucherIds;
                voucherIdParam.Direction = ParameterDirection.Input;

                SqlParameter lnsParam = new SqlParameter();
                lnsParam.ParameterName = "@LNS";
                lnsParam.DbType = DbType.String;
                lnsParam.Value = searchCondition.LNS.ToString();
                lnsParam.Direction = ParameterDirection.Input;

                SqlParameter yearOfWorkParam = new SqlParameter();
                yearOfWorkParam.ParameterName = "@YearOfWork";
                yearOfWorkParam.DbType = DbType.Int32;
                yearOfWorkParam.Value = searchCondition.YearOfWork;
                yearOfWorkParam.Direction = ParameterDirection.Input;

                SqlParameter yearOfBudgetParam = new SqlParameter();
                yearOfBudgetParam.ParameterName = "@YearOfBudget";
                yearOfBudgetParam.DbType = DbType.Int32;
                yearOfBudgetParam.Value = searchCondition.YearOfBudget;
                yearOfBudgetParam.Direction = ParameterDirection.Input;

                SqlParameter budgetSourceParam = new SqlParameter();
                budgetSourceParam.ParameterName = "@BudgetSource";
                budgetSourceParam.DbType = DbType.Int32;
                budgetSourceParam.Value = searchCondition.BudgetSource;
                budgetSourceParam.Direction = ParameterDirection.Input;

                return ctx.FromSqlRaw<NsDtChungTuChiTietQuery>("EXECUTE sp_dt_rpt_chi_tieu_luy_ke_tong_hop_clone @ChungTuIds, @LNS, @YearOfWork, @YearOfBudget, @BudgetSource",
                    voucherIdParam, lnsParam, yearOfWorkParam, yearOfBudgetParam, budgetSourceParam).ToList();
            }
        }

        public IEnumerable<NsDtChungTuChiTietQuery> FindByLuyKeTongHopSummary(EstimationVoucherDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter voucherIdParam = new SqlParameter();
                voucherIdParam.ParameterName = "@ChungTuId";
                voucherIdParam.DbType = DbType.String;
                voucherIdParam.Value = searchCondition.VoucherIds;
                voucherIdParam.Direction = ParameterDirection.Input;

                SqlParameter lnsParam = new SqlParameter();
                lnsParam.ParameterName = "@LNS";
                lnsParam.DbType = DbType.String;
                lnsParam.Value = searchCondition.LNS.ToString();
                lnsParam.Direction = ParameterDirection.Input;

                SqlParameter yearOfWorkParam = new SqlParameter();
                yearOfWorkParam.ParameterName = "@YearOfWork";
                yearOfWorkParam.DbType = DbType.Int32;
                yearOfWorkParam.Value = searchCondition.YearOfWork;
                yearOfWorkParam.Direction = ParameterDirection.Input;

                SqlParameter yearOfBudgetParam = new SqlParameter();
                yearOfBudgetParam.ParameterName = "@YearOfBudget";
                yearOfBudgetParam.DbType = DbType.Int32;
                yearOfBudgetParam.Value = searchCondition.YearOfBudget;
                yearOfBudgetParam.Direction = ParameterDirection.Input;

                SqlParameter budgetSourceParam = new SqlParameter();
                budgetSourceParam.ParameterName = "@BudgetSource";
                budgetSourceParam.DbType = DbType.Int32;
                budgetSourceParam.Value = searchCondition.BudgetSource;
                budgetSourceParam.Direction = ParameterDirection.Input;

                SqlParameter isPhanBoParam = new SqlParameter();
                isPhanBoParam.ParameterName = "@IsPhanBo";
                isPhanBoParam.DbType = DbType.Boolean;
                isPhanBoParam.Value = searchCondition.IsPhanBo;
                isPhanBoParam.Direction = ParameterDirection.Input;

                return ctx.FromSqlRaw<NsDtChungTuChiTietQuery>("EXECUTE sp_dt_rpt_chi_tieu_luy_ke_tong_hop_summary @ChungTuId, @LNS, @YearOfWork, @YearOfBudget, @BudgetSource, @IsPhanBo",
                    voucherIdParam, lnsParam, yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, isPhanBoParam).ToList();
            }
        }

        public IEnumerable<NsDtChungTuChiTietQuery> FindByLuyKeTongHopDotSummary(EstimationVoucherDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter voucherIdParam = new SqlParameter();
                voucherIdParam.ParameterName = "@ChungTuId";
                voucherIdParam.DbType = DbType.String;
                voucherIdParam.Value = searchCondition.VoucherIds;
                voucherIdParam.Direction = ParameterDirection.Input;

                SqlParameter lnsParam = new SqlParameter();
                lnsParam.ParameterName = "@LNS";
                lnsParam.DbType = DbType.String;
                lnsParam.Value = searchCondition.LNS.ToString();
                lnsParam.Direction = ParameterDirection.Input;

                SqlParameter yearOfWorkParam = new SqlParameter();
                yearOfWorkParam.ParameterName = "@YearOfWork";
                yearOfWorkParam.DbType = DbType.Int32;
                yearOfWorkParam.Value = searchCondition.YearOfWork;
                yearOfWorkParam.Direction = ParameterDirection.Input;

                SqlParameter yearOfBudgetParam = new SqlParameter();
                yearOfBudgetParam.ParameterName = "@YearOfBudget";
                yearOfBudgetParam.DbType = DbType.Int32;
                yearOfBudgetParam.Value = searchCondition.YearOfBudget;
                yearOfBudgetParam.Direction = ParameterDirection.Input;

                SqlParameter budgetSourceParam = new SqlParameter();
                budgetSourceParam.ParameterName = "@BudgetSource";
                budgetSourceParam.DbType = DbType.Int32;
                budgetSourceParam.Value = searchCondition.BudgetSource;
                budgetSourceParam.Direction = ParameterDirection.Input;

                SqlParameter donViTinhParam = new SqlParameter();
                donViTinhParam.ParameterName = "@DonViTinh";
                donViTinhParam.DbType = DbType.Int32;
                donViTinhParam.Value = searchCondition.DonViTinh;
                donViTinhParam.Direction = ParameterDirection.Input;

                return ctx.FromSqlRaw<NsDtChungTuChiTietQuery>("EXECUTE sp_dt_rpt_chi_tieu_luy_ke_tong_hop_dot_summary @ChungTuId, @LNS, @YearOfWork, @YearOfBudget, @BudgetSource, @DonViTinh",
                    voucherIdParam, lnsParam, yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, donViTinhParam).ToList();
            }
        }

        public IEnumerable<NsDtChungTuChiTietQuery> FindBudgetEstimateDivision(EstimationVoucherDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var chungTuIdParam = new SqlParameter
                {
                    ParameterName = "@ChungTuId",
                    DbType = DbType.String,
                    Value = searchCondition.ChungTuId,
                    Direction = ParameterDirection.Input
                };

                var lnsParam = new SqlParameter
                {
                    ParameterName = "@LNS",
                    DbType = DbType.String,
                    Value = searchCondition.LNS,
                    Direction = ParameterDirection.Input
                };

                var yearOfWorkParam = new SqlParameter
                {
                    ParameterName = "@YearOfWork",
                    DbType = DbType.Int32,
                    Value = searchCondition.YearOfWork,
                    Direction = ParameterDirection.Input
                };

                var yearOfBudgetParam = new SqlParameter
                {
                    ParameterName = "@YearOfBudget",
                    DbType = DbType.Int32,
                    Value = searchCondition.YearOfBudget,
                    Direction = ParameterDirection.Input
                };

                var budgetSourceParam = new SqlParameter
                {
                    ParameterName = "@BudgetSource",
                    DbType = DbType.Int32,
                    Value = searchCondition.BudgetSource,
                    Direction = ParameterDirection.Input
                };

                var levelParam = new SqlParameter
                {
                    ParameterName = "@Level",
                    DbType = DbType.Int32,
                    Value = searchCondition.Level,
                    Direction = ParameterDirection.Input
                };

                var statusParam = new SqlParameter
                {
                    ParameterName = "@Status",
                    DbType = DbType.Int32,
                    Value = searchCondition.Status,
                    Direction = ParameterDirection.Input
                };

                return ctx.FromSqlRaw<NsDtChungTuChiTietQuery>("EXECUTE sp_dt_nhan_phan_bo_du_toan_chi_tiet @ChungTuId, @LNS, @YearOfWork, @YearOfBudget, @BudgetSource, @Level, @Status",
                    chungTuIdParam, lnsParam, yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, levelParam, statusParam).ToList();
            }
        }

        public IEnumerable<NsDtChungTuChiTietQuery> FindBudgetEstimateDivisionBySoQuyetDinh(EstimationVoucherDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_dt_nhan_phan_bo_du_toan_chi_tiet_so_quyet_dinh @ChungTuId, @LNS, @YearOfWork, @YearOfBudget, @BudgetSource, @VoucherDate, @VoucherIndex, @IsLuyKe, @UnitType";
                var parameters = new[]
                {
                    new SqlParameter("@ChungTuId", searchCondition.ChungTuId),
                    new SqlParameter("@LNS", searchCondition.LNS),
                    new SqlParameter("@YearOfWork", searchCondition.YearOfWork),
                    new SqlParameter("@YearOfBudget", searchCondition.YearOfBudget),
                    new SqlParameter("@BudgetSource", searchCondition.BudgetSource),
                    new SqlParameter("@VoucherDate", searchCondition.VoucherDate),
                    new SqlParameter("@VoucherIndex", searchCondition.VoucherIndex),
                    new SqlParameter("@IsLuyKe", searchCondition.IsLuyKe),
                    new SqlParameter("@UnitType", searchCondition.DonViTinh),
                };
                return ctx.FromSqlRaw<NsDtChungTuChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<NsDtChungTuChiTietQuery> FindBudgetEstimateDivisionBySoQuyetDinhLNS(EstimationVoucherDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_dt_nhan_phan_bo_du_toan_chi_tiet_so_quyet_dinh_clone @ChungTuId, @LNS, @YearOfWork, @YearOfBudget, @BudgetSource, @VoucherDate, @VoucherIndex, @IsLuyKe,@UnitType";
                var parameters = new[]
                {
                    new SqlParameter("@ChungTuId", searchCondition.ChungTuId),
                    new SqlParameter("@LNS", searchCondition.LNS),
                    new SqlParameter("@YearOfWork", searchCondition.YearOfWork),
                    new SqlParameter("@YearOfBudget", searchCondition.YearOfBudget),
                    new SqlParameter("@BudgetSource", searchCondition.BudgetSource),
                    new SqlParameter("@VoucherDate", searchCondition.VoucherDate),
                    new SqlParameter("@VoucherIndex", searchCondition.VoucherIndex),
                    new SqlParameter("@IsLuyKe", searchCondition.IsLuyKe),
                    new SqlParameter("@UnitType", searchCondition.DonViTinh),
                };
                return ctx.FromSqlRaw<NsDtChungTuChiTietQuery>(sql, parameters).ToList();
            }
        }

        public void DeleteByIdChungTu(System.Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var idChungTuParam = new SqlParameter("@idChungTuParam", id.ToString());
                ctx.Database.ExecuteSqlCommand($"DELETE FROM NS_DT_ChungTuChiTiet WHERE iID_DTChungTu = @idChungTuParam", idChungTuParam);
            }
        }

        public void DeleteByIdChungTuDuToanNhan(System.Guid id, String idDuToanNhan)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_ns_delete_chungtuchitiet_by_dotnhan @iID_DTChungTu, @iID_CTDuToan_Nhan";
                var parameters = new[]
                {
                    new SqlParameter("@iID_DTChungTu", id.ToString()),
                    new SqlParameter("@iID_CTDuToan_Nhan", idDuToanNhan),
                };
                ctx.FromSqlRaw<NsDtChungTuChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<NsDtChungTuChiTiet> FindByListIdChungTu(IEnumerable<string> listIdChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDtChungTuChiTiets.Where(x => x.IIdDtchungTu.HasValue && listIdChungTu.Contains(x.IIdDtchungTu.ToString())
                                                                       && (x.FTuChi > 0 || x.FHienVat > 0 || x.FHangNhap > 0 || x.FHangMua > 0 || x.FPhanCap > 0)).ToList();
            }
        }

        public IEnumerable<NsDtChungTuChiTiet> FindByListIds(IEnumerable<string> ids)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                if (ids == null || !ids.Any())
                {
                    return Enumerable.Empty<NsDtChungTuChiTiet>();
                }

                return ctx.NsDtChungTuChiTiets.Where(x => ids.Contains(x.Id.ToString())).ToList();
            }
        }

        public void DeleteByIds(IEnumerable<string> ids)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                if (ids == null || !ids.Any())
                {
                    return;
                }

                foreach (var id in ids)
                {
                    var idParam = new SqlParameter("@id", id);
                    ctx.Database.ExecuteSqlCommand($"DELETE FROM NS_DT_ChungTuChiTiet WHERE iID_DTCTChiTiet = @id", idParam);
                }
            }
        }

        public IEnumerable<ReportChiTieuDuToanQuery> GetDataReportChiTieuToBia(string idChungTu, int donViTinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter idChungTuParam = new SqlParameter("@IdChungtu", idChungTu);
                SqlParameter donViTinhParam = new SqlParameter("@DonViTinh", donViTinh);
                return ctx.FromSqlRaw<ReportChiTieuDuToanQuery>("EXECUTE dbo.sp_pbdt_rpt_chitieu_to_bia @IdChungtu, @DonViTinh", idChungTuParam, donViTinhParam).ToList();
            }
        }

        public IEnumerable<ReportChiTieuDuToanQuery> GetDataReportChiTieuToBiaLuyKe(string idChungTu, int namLamViec, int namNganSach, int nguonNganSach, int loaiChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter idChungTuParam = new SqlParameter("@IdChungtu", idChungTu);
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter namNganSachParam = new SqlParameter("@NamNganSach", namNganSach);
                SqlParameter nguonNganSachParam = new SqlParameter("@NguonNganSach", nguonNganSach);
                SqlParameter loaiChungTuParam = new SqlParameter("@LoaiChungTu", nguonNganSach);
                return ctx.FromSqlRaw<ReportChiTieuDuToanQuery>("EXECUTE dbo.sp_pbdt_rpt_chitieu_to_bia_luy_ke @IdChungtu, @NamLamViec, @NamNganSach, @NguonNganSach, @LoaiChungTu",
                    idChungTuParam, namLamViecParam, namNganSachParam, nguonNganSachParam, loaiChungTuParam).ToList();
            }
        }

        public IEnumerable<ReportChiTieuDuToanQuery> GetDataReportChiTieuDonVi(int namLamViec, int nguonNganSach, int namNganSach, string idDonVi,
            string idChungTu, DateTime? ngayQuyetDinh, int donViTinh, bool isLuyKe)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_dutoan_rpt_inchitieu_donvi @NamLamViec, @NguonNganSach, @NamNganSach, @IdDonvi, @IdChungTu, @NgayQuyetDinh, @Dvt, @isLuyke";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@NguonNganSach", nguonNganSach),
                    new SqlParameter("@NamNganSach", namNganSach),
                    new SqlParameter("@IdDonvi", idDonVi),
                    new SqlParameter("@IdChungTu", idChungTu),
                    new SqlParameter("@NgayQuyetDinh", ngayQuyetDinh),
                    new SqlParameter("@Dvt", donViTinh),
                    new SqlParameter("@IsLuyKe", isLuyKe ? 1 : 0)
                };
                return ctx.FromSqlRaw<ReportChiTieuDuToanQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<ReportChiTieuDuToanQuery> GetDataReportChiTieuDonViDuToan(int namLamViec, int nguonNganSach, int namNganSach, DateTime? ngayChungTu, string idChungTu, int donViTinh, bool isPrintTNG)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter nguonNganSachParam = new SqlParameter("@NguonNganSach", nguonNganSach);
                SqlParameter namNganSachParam = new SqlParameter("@NamNganSach", namNganSach);
                SqlParameter ngayChungTuParam = new SqlParameter("@NgayChungTu", ngayChungTu);
                SqlParameter idChungTuParam = new SqlParameter("@IdChungTu", idChungTu);
                SqlParameter donViTinhParam = new SqlParameter("@Dvt", donViTinh);
                if (isPrintTNG)
                {
                    return ctx.FromSqlRaw<ReportChiTieuDuToanQuery>("EXECUTE dbo.sp_dutoan_rpt_inchitieu_donvi_dutoan_tng @NamLamViec, @NguonNganSach, " +
                    "@NamNganSach, @NgayChungTu, @IdChungTu, @Dvt", namLamViecParam, nguonNganSachParam, namNganSachParam,
                    ngayChungTuParam, idChungTuParam, donViTinhParam).ToList();
                }
                else
                {
                    return ctx.FromSqlRaw<ReportChiTieuDuToanQuery>("EXECUTE dbo.sp_dutoan_rpt_inchitieu_donvi_dutoan @NamLamViec, @NguonNganSach, " +
                    "@NamNganSach, @NgayChungTu, @IdChungTu, @Dvt", namLamViecParam, nguonNganSachParam, namNganSachParam,
                    ngayChungTuParam, idChungTuParam, donViTinhParam).ToList();
                }
            }
        }

        public IEnumerable<ReportDuToanTongHopSoPhanBoQuery> GetDataReportTongHopSoPhanBo(int namLamViec, int namNganSach, int nguonNganSach, string lns, DateTime? ngayQuyetDinh, double donViTinh, int loaiDuToan, string sSoQuyetDinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter nguonNganSachParam = new SqlParameter("@NguonNganSach", nguonNganSach);
                SqlParameter namNganSachParam = new SqlParameter("@NamNganSach", namNganSach);
                SqlParameter ngayQuyetDinhParam = new SqlParameter("@NgayQuyetDinh", ngayQuyetDinh);
                SqlParameter lnsParam = new SqlParameter("@LNS", lns);
                SqlParameter donViTinhParam = new SqlParameter("@Dvt", donViTinh);
                SqlParameter loaiDuToanParam = new SqlParameter("@LoaiDuToan", loaiDuToan);
                SqlParameter soQuyetDinhParam = new SqlParameter("@SoQuyetDinh", sSoQuyetDinh);
                return ctx.FromSqlRaw<ReportDuToanTongHopSoPhanBoQuery>("EXECUTE dbo.sp_dutoan_rpt_tong_hop_so_phan_bo_1 @NamLamViec, @NguonNganSach, " +
                    "@NamNganSach, @NgayQuyetDinh, @LNS, @Dvt, @LoaiDuToan, @SoQuyetDinh", namLamViecParam, nguonNganSachParam, namNganSachParam, ngayQuyetDinhParam,
                     lnsParam, donViTinhParam, loaiDuToanParam, soQuyetDinhParam).ToList();
            }
        }

        public IEnumerable<ReportDuToanTongHopSoPhanBoQuery> GetDataReportTongHopSoPhanBoHienVat(int namLamViec, int namNganSach, int nguonNganSach, string lns, DateTime? ngayQuyetDinh, double donViTinh, int loaiDuToan, string sSoQuyetDinh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter nguonNganSachParam = new SqlParameter("@NguonNganSach", nguonNganSach);
                SqlParameter namNganSachParam = new SqlParameter("@NamNganSach", namNganSach);
                SqlParameter ngayQuyetDinhParam = new SqlParameter("@NgayQuyetDinh", ngayQuyetDinh);
                SqlParameter lnsParam = new SqlParameter("@LNS", lns);
                SqlParameter donViTinhParam = new SqlParameter("@Dvt", donViTinh);
                SqlParameter loaiDuToanParam = new SqlParameter("@LoaiDuToan", loaiDuToan);
                SqlParameter soQuyetDinhParam = new SqlParameter("@SoQuyetDinh", sSoQuyetDinh);
                return ctx.FromSqlRaw<ReportDuToanTongHopSoPhanBoQuery>("EXECUTE sp_dutoan_rpt_tong_hop_so_phan_bo_hienvat_1 @NamLamViec, @NguonNganSach, " +
                    "@NamNganSach, @NgayQuyetDinh, @LNS, @Dvt, @LoaiDuToan, @SoQuyetDinh", namLamViecParam, nguonNganSachParam, namNganSachParam, ngayQuyetDinhParam,
                     lnsParam, donViTinhParam, loaiDuToanParam, soQuyetDinhParam).ToList();
            }
        }

        public void DeleteInputData(Guid chungTuId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var idChungTuParam = new SqlParameter("@idChungTuParam", chungTuId.ToString());
                ctx.Database.ExecuteSqlCommand($"DELETE FROM NS_DT_ChungTuChiTiet WHERE iID_DTChungTu = @idChungTuParam AND IDuLieuNhan = 2", idChungTuParam);
            }
        }

        public IEnumerable<ReportChiTieuDuToanQuery> GetDataReportChiTieuNganh(int namLamViec, int nguonNganSach, int namNganSach, string nganh, string idChungTu, int loaiChungTu, int donViTinh, bool isLuyKe, bool haveDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter nguonNganSachParam = new SqlParameter("@NguonNganSach", nguonNganSach);
                SqlParameter namNganSachParam = new SqlParameter("@NamNganSach", namNganSach);
                SqlParameter nganhParam = new SqlParameter("@Nganh", nganh);
                SqlParameter idChungTuParam = new SqlParameter("@IdChungTu", idChungTu);
                SqlParameter loaiChungTuParam = new SqlParameter("@LoaiChungTu", loaiChungTu);
                SqlParameter donViTinhParam = new SqlParameter("@Dvt", donViTinh);
                SqlParameter isLuyKeParam = new SqlParameter("@IsLuyKe", isLuyKe ? 1 : 0);
                string procedure = haveDonVi ? "dbo.sp_dutoan_rpt_inchitieu_nganh_donvi" : "dbo.sp_dutoan_rpt_inchitieu_nganh";
                return ctx.FromSqlRaw<ReportChiTieuDuToanQuery>($"EXECUTE {procedure} @NamLamViec, @NguonNganSach, " +
                    "@NamNganSach, @Nganh, @IdChungTu, @LoaiChungTu, @Dvt, @isLuyke", namLamViecParam, nguonNganSachParam, namNganSachParam, nganhParam,
                    idChungTuParam, loaiChungTuParam, donViTinhParam, isLuyKeParam).ToList();
            }
        }

        public IEnumerable<ReportChiTieuDuToanDynamicQuery> GetDataReportChiTieuNganhAll(int namLamViec, int nguonNganSach, int namNganSach, string nganh, string idChungTu, int donViTinh, bool isLuyKe, bool haveDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter nguonNganSachParam = new SqlParameter("@NguonNganSach", nguonNganSach);
                SqlParameter namNganSachParam = new SqlParameter("@NamNganSach", namNganSach);
                SqlParameter nganhParam = new SqlParameter("@Nganh", nganh);
                SqlParameter idChungTuParam = new SqlParameter("@IdChungTu", idChungTu);
                SqlParameter donViTinhParam = new SqlParameter("@Dvt", donViTinh);
                SqlParameter isLuyKeParam = new SqlParameter("@IsLuyKe", isLuyKe ? 1 : 0);
                string procedure = haveDonVi ? "dbo.sp_dutoan_rpt_inchitieu_nganh_donvi_all" : "dbo.sp_dutoan_rpt_inchitieu_nganh_all";
                return ctx.FromSqlRaw<ReportChiTieuDuToanDynamicQuery>($"EXECUTE {procedure} @NamLamViec, @NguonNganSach, " +
                    "@NamNganSach, @Nganh, @IdChungTu, @Dvt, @isLuyke", namLamViecParam, nguonNganSachParam, namNganSachParam, nganhParam,
                    idChungTuParam, donViTinhParam, isLuyKeParam).ToList();
            }
        }

        public IEnumerable<ReportChiTieuDuToanDynamicMLNSQuery> GetDataReportChiTieuNganhAllMLNS(int namLamViec, int nguonNganSach, int namNganSach, string nganh, string idChungTu, int donViTinh, bool isLuyKe)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                SqlParameter nguonNganSachParam = new SqlParameter("@NguonNganSach", nguonNganSach);
                SqlParameter namNganSachParam = new SqlParameter("@NamNganSach", namNganSach);
                SqlParameter nganhParam = new SqlParameter("@Nganh", nganh);
                SqlParameter idChungTuParam = new SqlParameter("@IdChungTu", idChungTu);
                SqlParameter donViTinhParam = new SqlParameter("@Dvt", donViTinh);
                SqlParameter isLuyKeParam = new SqlParameter("@IsLuyKe", isLuyKe ? 1 : 0);
                return ctx.FromSqlRaw<ReportChiTieuDuToanDynamicMLNSQuery>($"EXECUTE sp_dutoan_rpt_inchitieu_nganh_donvi_all_mlns @NamLamViec, @NguonNganSach, " +
                    "@NamNganSach, @Nganh, @IdChungTu, @Dvt, @isLuyke", namLamViecParam, nguonNganSachParam, namNganSachParam, nganhParam,
                    idChungTuParam, donViTinhParam, isLuyKeParam).ToList();
            }
        }

        public IEnumerable<ReportDuToanThongKeSoQuyetDinhQuery> GetDataReportDuToanThongKeSoQuyetDinh(int yearOfWork, int yearOfBudget, int budgetSource, string soQuyetDinh, string lns, int dvt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", yearOfWork);
                SqlParameter yearOfBudgetParam = new SqlParameter("@YearOfBudget", yearOfBudget);
                SqlParameter budgetSourceParam = new SqlParameter("@BudgetSource", budgetSource);
                SqlParameter soQuyetDinhParam = new SqlParameter("@SoQuyetDinh", soQuyetDinh);
                SqlParameter lnsParam = new SqlParameter("@LNS", lns);
                SqlParameter dvtParam = new SqlParameter("@dvt", dvt);

                return ctx.FromSqlRaw<ReportDuToanThongKeSoQuyetDinhQuery>("EXECUTE sp_dutoan_rpt_thongke_soquyetdinh @YearOfWork, @YearOfBudget, @BudgetSource, @SoQuyetDinh, @LNS, @dvt",
                                dvt, yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, soQuyetDinhParam, lnsParam, dvtParam).ToList();
            }
        }

        public IEnumerable<NsDuToanChungTuChiTietQuery> FindChungTuChiTiet(EstimationVoucherDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string procedure = string.Empty;
                if (searchCondition.IsNamLuyKe)
                    procedure = "sp_dt_danhsach_chungtu_chitiet_luyke";
                else
                    procedure = "sp_dt_danhsach_chungtu_chitiet";
                string sql = $"EXECUTE {procedure} @ChungTuId, @LNS, @IdDonVi, @NamLamViec, @NamNganSach, @NguonNganSach, @UserName, @IsGetAll";
                var parameters = new[]
                {
                    new SqlParameter("@ChungTuId", searchCondition.VoucherId.ToString()),
                    new SqlParameter("@LNS", searchCondition.LNS),
                    new SqlParameter("@IdDonVi", searchCondition.IdDonVi),
                    new SqlParameter("@NamLamViec", searchCondition.YearOfWork),
                    new SqlParameter("@NamNganSach", searchCondition.YearOfBudget),
                    new SqlParameter("@NguonNganSach", searchCondition.BudgetSource),
                    new SqlParameter("@UserName", searchCondition.UserName),
                    new SqlParameter("@IsGetAll", searchCondition.IsGetAll)
                };
                return ctx.FromSqlRaw<NsDuToanChungTuChiTietQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<NsDuToanChungTuChiTietDieuChinhQuery> FindChungTuChiTietDieuChinh(EstimationVoucherDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter chungTuIdParam = new SqlParameter("@ChungTuId", searchCondition.VoucherId.ToString());
                SqlParameter lnsParam = new SqlParameter("@LNS", searchCondition.LNS);
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", searchCondition.YearOfWork);
                SqlParameter namNganSachParam = new SqlParameter("@NamNganSach", searchCondition.YearOfBudget);
                SqlParameter nguonNganSachParam = new SqlParameter("@NguonNganSach", searchCondition.BudgetSource);
                SqlParameter userNameParam = new SqlParameter("@UserName", searchCondition.UserName);

                return ctx.FromSqlRaw<NsDuToanChungTuChiTietDieuChinhQuery>("EXECUTE sp_dt_danhsach_chungtu_chitiet_dieuchinh @ChungTuId, @LNS, @NamLamViec, @NamNganSach, @NguonNganSach, @UserName",
                                chungTuIdParam, lnsParam, namLamViecParam, namNganSachParam, nguonNganSachParam, userNameParam).ToList();
            }
        }

        public IEnumerable<string> GetLnsHasData(List<Guid> chungTuIds)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDtChungTuChiTiets.Where(x => x.IIdDtchungTu.HasValue && chungTuIds.Contains(x.IIdDtchungTu.Value) && (x.FTuChi != 0 || x.FHienVat != 0 || x.FDuPhong != 0 ||
                                                    x.FHangNhap != 0 || x.FHangMua != 0 || x.FPhanCap != 0)).Select(x => x.SLns).Distinct().ToList();
            }
        }

        public IEnumerable<string> GetLnsHasSpendData(List<Guid> chungTuIds)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDtChungTuChiTiets.Where(x => x.IIdDtchungTu.HasValue && chungTuIds.Contains(x.IIdDtchungTu.Value) && (x.FTuChi != 0 || x.FHienVat != 0 || x.FDuPhong != 0 ||
                                                    x.FHangNhap != 0 || x.FHangMua != 0 || x.FPhanCap != 0) && !x.SXauNoiMa.StartsWith("8")).Select(x => x.SLns).Distinct().ToList();
            }
        }

        public IEnumerable<string> GetXauNoiMaHasSpendData(List<Guid> chungTuIds)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDtChungTuChiTiets.Where(x => x.IIdDtchungTu.HasValue && chungTuIds.Contains(x.IIdDtchungTu.Value) && (x.FTuChi != 0 || x.FHienVat != 0 || x.FDuPhong != 0 ||
                                                    x.FHangNhap != 0 || x.FHangMua != 0 || x.FPhanCap != 0) && !x.SXauNoiMa.StartsWith("8")).Select(x => x.SXauNoiMa).Distinct().ToList();
            }
        }

        public IEnumerable<string> GetXauNoiMaHasCollectData(List<Guid> chungTuIds)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TnDtChungTuChiTiets.Where(x => x.IdChungTu.HasValue && chungTuIds.Contains(x.IdChungTu.Value) && x.TuChi != 0 && x.XauNoiMa.StartsWith("8")).Select(x => x.XauNoiMa).Distinct().ToList();
            }
        }

        public IEnumerable<string> GetLnsHasCollectData(List<Guid> chungTuIds)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TnDtChungTuChiTiets.Where(x => x.IdChungTu.HasValue && chungTuIds.Contains(x.IdChungTu.Value) && x.TuChi != 0 && x.XauNoiMa.StartsWith("8")).Select(x => x.Lns).Distinct().ToList();
            }
        }

        public IEnumerable<NsDtChungTuChiTietQuery> GetDataTongHopPhanBoTheoDot(EstimationVoucherDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = $"EXECUTE sp_dt_rpt_tonghop_phanbo_theodot @VoucherIds, @LNS, @YearOfWork, @YearOfBudget, @BudgetSource, @UserName, @UnitType";
                var parameters = new[]
                {
                    new SqlParameter("@VoucherIds", searchCondition.VoucherIds),
                    new SqlParameter("@LNS", searchCondition.LNS),
                    new SqlParameter("@YearOfWork", searchCondition.YearOfWork),
                    new SqlParameter("@YearOfBudget", searchCondition.YearOfBudget),
                    new SqlParameter("@BudgetSource", searchCondition.BudgetSource),
                    new SqlParameter("@UserName", searchCondition.UserName),
                    new SqlParameter("@UnitType", searchCondition.DonViTinh)
                };
                return ctx.FromSqlRaw<NsDtChungTuChiTietQuery>(sql, parameters).ToList();
            }
        }

        public bool IsExistEstimate(Guid id, Guid estimateId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDtChungTuChiTiets.Any(c => c.IIdDtchungTu.Value == id && c.IIdCtduToanNhan.Equals(estimateId));
            }
        }

        public IEnumerable<NsDtChungTuCongKhaiQuery> GetDataBaoCaoDanhMucCongKhai02(int iNamLamViec, int iNamNganSach, int iMaNguonNganSach, int iQuarterMonths, string sIdDanhMucCongKhai, int dvt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = $"EXECUTE sp_skt_rpt_dutoandaunam_congkhai_02CKNS @iNamLamViec, @iNamNganSach, @iMaNguonNganSach, @iQuarterMonths, @sIdDanhMucCongKhai, @dvt";
                var parameters = new[]
                {
                    new SqlParameter("@iNamLamViec", iNamLamViec),
                    new SqlParameter("@iNamNganSach", iNamNganSach),
                    new SqlParameter("@iMaNguonNganSach", iMaNguonNganSach),
                    new SqlParameter("@iQuarterMonths", iQuarterMonths),
                    new SqlParameter("@sIdDanhMucCongKhai", sIdDanhMucCongKhai),
                    new SqlParameter("@dvt", dvt)
                };
                return ctx.FromSqlRaw<NsDtChungTuCongKhaiQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<NsDtChungTuCongKhaiQuery> GetDataBaoCaoDanhMucCongKhai02Clone(int iNamLamViec, int iNamNganSach, int iMaNguonNganSach, int iQuarterMonths, string sIdDanhMucCongKhai, int dvt, string sIdDotNhan)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = $"EXECUTE sp_skt_rpt_dutoandaunam_congkhai_02CKNS_Clone @iNamLamViec, @iNamNganSach, @iMaNguonNganSach, @iQuarterMonths, @sIdDanhMucCongKhai, @dvt, @sIdDotNhan";
                var parameters = new[]
                {
                    new SqlParameter("@iNamLamViec", iNamLamViec),
                    new SqlParameter("@iNamNganSach", iNamNganSach),
                    new SqlParameter("@iMaNguonNganSach", iMaNguonNganSach),
                    new SqlParameter("@iQuarterMonths", iQuarterMonths),
                    new SqlParameter("@sIdDanhMucCongKhai", sIdDanhMucCongKhai),
                    new SqlParameter("@dvt", dvt),
                    new SqlParameter("@sIdDotNhan", sIdDotNhan)
                };
                return ctx.FromSqlRaw<NsDtChungTuCongKhaiQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<NsDtPhuongAnPhanBoQuery> ExportMau01PhuLucI(string maCongKhai, int iNamLamViec, int iNamNganSach, int iMaNguonNganSach, string sTuDot, string sDenDot, int dvt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = $"EXECUTE sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl1 @MaCongKhai, @YearOfWork, @YearOfBudget, @BudgetSource, @FromDate, @ToDate, @DVT";
                var parameters = new[]
                {
                    new SqlParameter("@MaCongKhai", maCongKhai),
                    new SqlParameter("@YearOfWork", iNamLamViec),
                    new SqlParameter("@YearOfBudget", iNamNganSach),
                    new SqlParameter("@BudgetSource", iMaNguonNganSach),
                    new SqlParameter("@FromDate", sTuDot),
                    new SqlParameter("@ToDate", sDenDot),
                    new SqlParameter("@DVT", dvt),
                };
                return ctx.FromSqlRaw<NsDtPhuongAnPhanBoQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<TnDtDuToanReportQuery> ExportPhuongAnPhanBo4554(string agencies, int iNamLamViec, int iNamNganSach, int iMaNguonNganSach, string sIdChungTuDutoan, string sIdChungTuThuNop, int dvt, string voucherType)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = $"EXECUTE sp_rpt_tn_dutoan_hd4554 @agencies, @YearOfWork, @YearOfBudget, @BudgetSource, @IdChungTuDutoan, @IdChungTuThuNop, @DonViTinh, @VoucherType";
                var parameters = new[]
                {
                    new SqlParameter("@agencies", agencies),
                    new SqlParameter("@YearOfWork", iNamLamViec),
                    new SqlParameter("@YearOfBudget", iNamNganSach),
                    new SqlParameter("@BudgetSource", iMaNguonNganSach),
                    new SqlParameter("@IdChungTuDutoan", sIdChungTuDutoan),
                    new SqlParameter("@IdChungTuThuNop", sIdChungTuThuNop),
                    new SqlParameter("@DonViTinh", dvt),
                    new SqlParameter("@VoucherType", voucherType),
                };
                return ctx.FromSqlRaw<TnDtDuToanReportQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<NsDtPhuongAnPhanBoQuery> ExportMau01PhuLucII(string sLns, int iNamLamViec, int iNamNganSach, int iMaNguonNganSach, string sTuDot, string sDenDot, int dvt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = $"EXECUTE sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2 @LNS, @YearOfWork, @YearOfBudget, @BudgetSource, @FromDate, @ToDate, @DVT";
                var parameters = new[]
                {
                    new SqlParameter("@LNS", sLns),
                    new SqlParameter("@YearOfWork", iNamLamViec),
                    new SqlParameter("@YearOfBudget", iNamNganSach),
                    new SqlParameter("@BudgetSource", iMaNguonNganSach),
                    new SqlParameter("@FromDate", sTuDot),
                    new SqlParameter("@ToDate", sDenDot),
                    new SqlParameter("@DVT", dvt),
                };
                return ctx.FromSqlRaw<NsDtPhuongAnPhanBoQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<NsDtPhuongAnPhanBoQuery> ExportMau01PhuLucIIDonVi(string sLns, int iNamLamViec, int iNamNganSach, int iMaNguonNganSach, string sTuDot, string sDenDot, string maDonVi, int dvt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = $"EXECUTE sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_donvi @LNS, @YearOfWork, @YearOfBudget, @BudgetSource, @FromDate, @ToDate, @MaDonVi, @DVT";
                var parameters = new[]
                {
                    new SqlParameter("@LNS", sLns),
                    new SqlParameter("@YearOfWork", iNamLamViec),
                    new SqlParameter("@YearOfBudget", iNamNganSach),
                    new SqlParameter("@BudgetSource", iMaNguonNganSach),
                    new SqlParameter("@FromDate", sTuDot),
                    new SqlParameter("@ToDate", sDenDot),
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@DVT", dvt),
                };
                return ctx.FromSqlRaw<NsDtPhuongAnPhanBoQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<NsDtPhuongAnPhanBoQuery> ExportMau01PhuLucIIDonViExcel(string sLns, int iNamLamViec, int iNamNganSach, int iMaNguonNganSach, string sTuDot, string sDenDot, string maDonVi, int dvt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = $"EXECUTE sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_excel @LNS, @YearOfWork, @YearOfBudget, @BudgetSource, @FromDate, @ToDate, @MaDonVi, @DVT";
                var parameters = new[]
                {
                    new SqlParameter("@LNS", sLns),
                    new SqlParameter("@YearOfWork", iNamLamViec),
                    new SqlParameter("@YearOfBudget", iNamNganSach),
                    new SqlParameter("@BudgetSource", iMaNguonNganSach),
                    new SqlParameter("@FromDate", sTuDot),
                    new SqlParameter("@ToDate", sDenDot),
                    new SqlParameter("@MaDonVi", maDonVi),
                    new SqlParameter("@DVT", dvt),
                };
                return ctx.FromSqlRaw<NsDtPhuongAnPhanBoQuery>(sql, parameters).ToList();
            }
        }

        public List<string> GetReportUnitPhuLucII(string sLns, int iNamLamViec, int iNamNganSach, int iMaNguonNganSach, string sTuDot, string sDenDot)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = $"EXECUTE sp_rpt_dt_PAPBDT_m01_pl2_get_donvi @LNS, @YearOfWork, @YearOfBudget, @BudgetSource, @FromDate, @ToDate";
                var parameters = new[]
                {
                    new SqlParameter("@LNS", sLns),
                    new SqlParameter("@YearOfWork", iNamLamViec),
                    new SqlParameter("@YearOfBudget", iNamNganSach),
                    new SqlParameter("@BudgetSource", iMaNguonNganSach),
                    new SqlParameter("@FromDate", sTuDot),
                    new SqlParameter("@ToDate", sDenDot)
                };
                return ctx.FromSqlRaw<string>(sql, parameters).ToList();
            }
        }

        public List<string> GetReportSelfUnitPhuLucII(int iNamLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = $"EXECUTE sp_rpt_dt_PAPBDT_m01_pl2_get_donvi_banthan @YearOfWork";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", iNamLamViec)
                };
                return ctx.FromSqlRaw<string>(sql, parameters).ToList();
            }
        }

        public IEnumerable<NsDtPhuongAnPhanBoQuery> ExportMau02(string maCongKhai, int iNamLamViec, int iNamNganSach, int iMaNguonNganSach, string sTuDot, string sDenDot, int dvt)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = $"EXECUTE sp_rpt_dt_phuong_an_phan_bo_du_toan_m02 @MaCongKhai, @YearOfWork, @YearOfBudget, @BudgetSource, @FromDate, @ToDate, @DVT";
                var parameters = new[]
                {
                    new SqlParameter("@MaCongKhai", maCongKhai),
                    new SqlParameter("@YearOfWork", iNamLamViec),
                    new SqlParameter("@YearOfBudget", iNamNganSach),
                    new SqlParameter("@BudgetSource", iMaNguonNganSach),
                    new SqlParameter("@FromDate", sTuDot),
                    new SqlParameter("@ToDate", sDenDot),
                    new SqlParameter("@DVT", dvt),
                };
                return ctx.FromSqlRaw<NsDtPhuongAnPhanBoQuery>(sql, parameters).ToList();
            }
        }
    }
}
