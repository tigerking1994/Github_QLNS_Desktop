using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TnDtdnChungTuService : ITnDtdnChungTuService
    {

        private readonly ITnDtdnChungTuRepository _tnDtdnChungTuRepository;
        private readonly ITnDtdnChungTuChiTietRepository _tnDtdnChungTuChiTietRepository;

        public TnDtdnChungTuService(ITnDtdnChungTuRepository tnDtdnChungTuRepository, ITnDtdnChungTuChiTietRepository tnDtdnChungTuChiTietRepository)
        {
            _tnDtdnChungTuRepository = tnDtdnChungTuRepository;
            _tnDtdnChungTuChiTietRepository = tnDtdnChungTuChiTietRepository;
        }
        public TnDtdnChungTu Add(TnDtdnChungTu entity)
        {
            _tnDtdnChungTuRepository.Add(entity);
            return entity;
        }

        public void CreateDataReportTotalSummary(string id, int namLamViec, int namNganSach, int nguonNganSach, string idDonVi, string listChungTuTongHop, string nguoiTao)
        {
            _tnDtdnChungTuRepository.CreateDataReportTotalSummary(id, namLamViec, namNganSach, nguonNganSach, idDonVi, listChungTuTongHop, nguoiTao);
        }

        public int Delete(Guid id)
        {
            return _tnDtdnChungTuRepository.DeleteItem(id);
        }

        public TnDtdnChungTu FindAggregateVoucher(string voucherNoes)
        {
            return _tnDtdnChungTuRepository.FindAggregateVoucher(voucherNoes);
        }

        public IEnumerable<TnDtdnChungTu> FindByCondition(Expression<Func<TnDtdnChungTu, bool>> predicate)
        {
            return _tnDtdnChungTuRepository.FindAll(predicate);
        }

        public TnDtdnChungTu FindById(Guid id)
        {
            return _tnDtdnChungTuRepository.Find(id);
        }

        public int FindNextSoChungTuIndex(Expression<Func<TnDtdnChungTu, bool>> predicate)
        {
            return _tnDtdnChungTuRepository.FindNextSoChungTuIndex(predicate);
        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            return _tnDtdnChungTuRepository.LockOrUnLock(id, lockStatus);
        }

        public int Update(TnDtdnChungTu item)
        {
            return _tnDtdnChungTuRepository.Update(item);
        }

        public int UpdateRange(IEnumerable<TnDtdnChungTu> items)
        {
            return _tnDtdnChungTuRepository.UpdateRange(items);
        }
        public List<string> GetAgencyCodeByVoucherDetail(Expression<Func<TnDtdnChungTu, bool>> predicate)
        {
            return _tnDtdnChungTuRepository.GetAgencyCodeByVoucherDetail(predicate);
        }

        public IEnumerable<string> GetLnsHasData(List<Guid> chungTuIds)
        {
            return _tnDtdnChungTuRepository.GetLnsHasData(chungTuIds);
        }

        public IEnumerable<BaoCaoNhanVaQuyetToanKinhPhi> GetBaoCaoNhanVaQuyetToanKinhPhis(string sMaDonVi, int iNamLamViec, int iDonViTinh)
        {
            return _tnDtdnChungTuRepository.GetBaoCaoNhanVaQuyetToanKinhPhis(sMaDonVi, iNamLamViec, iDonViTinh);
        }
    }
}
