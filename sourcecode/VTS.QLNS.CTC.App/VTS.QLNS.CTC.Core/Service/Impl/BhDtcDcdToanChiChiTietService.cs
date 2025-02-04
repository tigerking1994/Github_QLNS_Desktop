using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhDtcDcdToanChiChiTietService : IBhDtcDcdToanChiChiTietService
    {
        private readonly IBhDtcDcdToanChiChiTietRepository _repostiory;
        public BhDtcDcdToanChiChiTietService(IBhDtcDcdToanChiChiTietRepository repostiory)
        {
            _repostiory = repostiory;
        }

        public void AddAggregate(BhDtcDcdToanChiChiTietCriteria creation)
        {
            _repostiory.AddAggregate(creation);
        }

        public int AddRange(IEnumerable<BhDtcDcdToanChiChiTiet> khcKinhphiQuanlyChiTiets)
        {
            return _repostiory.AddRange(khcKinhphiQuanlyChiTiets);
        }

        public void Delete(Guid id)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                BhDtcDcdToanChiChiTiet entity = _repostiory.Find(id);
                if (entity != null)
                {
                    // Xóa chi tiết
                    _repostiory.Delete(entity);
                }

                transactionScope.Complete();
            }
        }

        public bool ExistDTChiTiet(Guid bhxhId)
        {
            return _repostiory.ExistDTChiTiet(bhxhId);
        }

        public IEnumerable<BhDtcDcdToanChiChiTiet> FindAll(Expression<Func<BhDtcDcdToanChiChiTiet, bool>> predicate)
        {
            return _repostiory.FindAll(predicate);
        }

        public IEnumerable<BhDtcDcdToanChiChiTiet> FindByCondition(Expression<Func<BhDtcDcdToanChiChiTiet, bool>> predicate)
        {
            return _repostiory.FindAll(predicate);
        }

        public IEnumerable<BhDtcDcdToanChiChiTietQuery> FindByConditionForChildUnit(BhDtcDcdToanChiChiTietCriteria searchModel)
        {
            return _repostiory.FindByConditionForChildUnit(searchModel);
        }

        public BhDtcDcdToanChiChiTiet FindById(Guid id)
        {
            return _repostiory.Find(id);
        }

        public IEnumerable<BhDtcDcdToanChiChiTiet> FindByIdChiTiet(Guid id)
        {
            return _repostiory.FindByIdChiTiet(id);
        }

        public IEnumerable<BhDtcDcdToanChiChiTietQuery> GetDataForAgency(BhDtcDcdToanChiChiTietCriteria searchCondition)
        {
            return _repostiory.GetDataForAgency(searchCondition);
        }
        
        public IEnumerable<BhDtcDcdToanChiChiTietQuery> GetDataAggregateForAgency(BhDtcDcdToanChiChiTietCriteria searchCondition)
        {
            return _repostiory.GetDataAggregateForAgency(searchCondition);
        }

        public int RemoveRange(IEnumerable<BhDtcDcdToanChiChiTiet> bhKhcKcbChiTiets)
        {
            return _repostiory.RemoveRange(bhKhcKcbChiTiets);
        }

        public void Update(BhDtcDcdToanChiChiTiet entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repostiory.Update(entity);

                transactionScope.Complete();
            }
        }

        public IEnumerable<BhDtcDcdToanChiChiTietQuery> GetDataAggregateForAgencyKPQLKCBQYKCBTSKPK(BhDtcDcdToanChiChiTietCriteria searchCondition)
        {
            return _repostiory.GetDataAggregateForAgencyKPQLKCBQYKCBTSKPK(searchCondition);
        }

        public List<BhDtcDcdToanChiChiTietQuery> ExportDataChiTiet(BhDtcDcdToanChiChiTietCriteria searchCondition)
        {
            return _repostiory.ExportDataChiTiet(searchCondition);
        }

        public List<BhDtcDcdToanChiChiTietQuery> GetAdjustData(int namLamViec, string vouchers, string sLNS, string sMaLoaiChi, string sID_MaDonVi)
        {
            return _repostiory.GetAdjustData(namLamViec, vouchers, sLNS, sMaLoaiChi, sID_MaDonVi);
        }

        public List<BhDtcDcdToanChiChiTietQuery> FindGiaTriDieuChinhThuBHXH(string iID_MaDonVi, int? namLamViec, DateTime dNgayChungTu, int iLoaiTongHop)
        {
            return _repostiory.FindGiaTriDieuChinhThuBHXH(iID_MaDonVi, namLamViec, dNgayChungTu, iLoaiTongHop);
        }
        public IEnumerable<BhPbdtcBHXHChiTietQuery> FindPbDtChiChiTiet(int namLamViec, string idDonVi, Guid loaiDanhMucCapChi, DateTime? ngayChungTu, string sLNS)
        {
            return _repostiory.FindPbDtChiChiTiet(namLamViec, idDonVi, loaiDanhMucCapChi, ngayChungTu, sLNS);
        }
        public IEnumerable<BhDtctgBHXHChiTietQuery> FindNPBChiChiTiet(BhDtcDcdToanChiChiTietCriteria chiTietCriteria)
        {
            return _repostiory.FindNPBChiChiTiet(chiTietCriteria);
        }
        public List<BhDtcDcdToanChiChiTietQuery> FindData6ThangDauNamChiTiet(int namLamViec, string idDonVi, Guid loaiDanhMucCapChi, string sMaLoaiChi, string sLNS, DateTime? dNgayChungTu)
        {
            return _repostiory.FindData6ThangDauNamChiTiet(namLamViec, idDonVi, loaiDanhMucCapChi, sMaLoaiChi, sLNS, dNgayChungTu);
        }

        public IEnumerable<BhDtcDcdToanChiChiTietQuery> GetSettlementData(int namLamViec, string idDonVi, int quy, string sLns)
        {
            return _repostiory.GetSettlementData(namLamViec, idDonVi, quy, sLns);
        }
    }
}
