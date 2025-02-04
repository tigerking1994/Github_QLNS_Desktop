using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using System.Linq;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Core.Domain.Query;
using System;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TnDtdnChungTuChiTietRepository : Repository<TnDtdnChungTuChiTiet>, ITnDtdnChungTuChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TnDtdnChungTuChiTietRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;

        }

        public IEnumerable<TnDtdnChungTuChiTietQuery> FindByDataDetailCondition(EstimationVoucherDetailCriteria searchCondition)
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


                return ctx.FromSqlRaw<TnDtdnChungTuChiTietQuery>("EXECUTE sp_tn_dtdn_chungtu_chitiet @ChungTuId, @LNS, @YearOfWork, @YearOfBudget, @BudgetSource",
                    voucherIdParam, lnsParam, yearOfWorkParam, yearOfBudgetParam, budgetSourceParam).ToList();
            }
        }

        public IEnumerable<TnDtdnChungTuChiTietQuery> FindDataReportAgencyDetailByCondition(EstimationVoucherDetailCriteria searchCondition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter agencyParam = new SqlParameter();
                agencyParam.ParameterName = "@agencies";
                agencyParam.DbType = DbType.String;
                agencyParam.Value = searchCondition.IIDMaDonVis.ToString();
                agencyParam.Direction = ParameterDirection.Input;

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

                SqlParameter voucherTypeParam = new SqlParameter();
                voucherTypeParam.ParameterName = "@VoucherType";
                voucherTypeParam.DbType = DbType.Int32;
                voucherTypeParam.Value = searchCondition.LoaiChungTu;
                voucherTypeParam.Direction = ParameterDirection.Input;
                
                SqlParameter unitParam = new SqlParameter();
                unitParam.ParameterName = "@DonViTinh";
                unitParam.DbType = DbType.Int32;
                unitParam.Value = searchCondition.DonViTinh;
                unitParam.Direction = ParameterDirection.Input;


                return ctx.FromSqlRaw<TnDtdnChungTuChiTietQuery>("EXECUTE sp_rpt_tn_dtdn_chungtu_chitiet_by_don_vi @agencies, @LNS, @YearOfWork, @YearOfBudget, @BudgetSource, @VoucherType, @DonViTinh",
                    agencyParam, lnsParam, yearOfWorkParam, yearOfBudgetParam, budgetSourceParam, voucherTypeParam, unitParam).ToList();
            }
        }

        public IEnumerable<TnDtdnChungTuChiTiet> FindByChungTuId(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TnDtdnChungTuChiTiets.Where(x => x.IdChungTu.Equals(id)).ToList();
            }
        }
    }
}
