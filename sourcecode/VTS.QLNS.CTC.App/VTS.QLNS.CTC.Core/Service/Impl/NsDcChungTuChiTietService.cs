using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using static Dapper.SqlMapper;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NsDcChungTuChiTietService : INsDcChungTuChiTietService
    {
        private readonly INsDcChungTuChiTietRepository _chungTuChiTietRepository;
        public NsDcChungTuChiTietService(INsDcChungTuChiTietRepository chungTuChiTietRepository)
        {
            _chungTuChiTietRepository = chungTuChiTietRepository;
        }

        public void AddAggregateVoucherDetail(EstimationVoucherDetailCriteria creation)
        {
            _chungTuChiTietRepository.AddAggregateVoucherDetail(creation);
        }

        public int AddRange(IEnumerable<NsDcChungTuChiTiet> entities)
        {
            return _chungTuChiTietRepository.AddRange(entities);
        }

        public int UpdateRange(IEnumerable<NsDcChungTuChiTiet> entities)
        {
            return _chungTuChiTietRepository.UpdateRange(entities);
        }

        public IEnumerable<NsDcChungTuChiTiet> FindByChungTuID(Guid id)
        {
            return _chungTuChiTietRepository.FindAll(x => x.IIdDcchungTu == id);
        }

        public void DeleteByIdChungTu(Guid id)
        {
            _chungTuChiTietRepository.DeleteByIdChungTu(id);
        }

        public void DeleteByIds(IEnumerable<string> ids)
        {
            _chungTuChiTietRepository.DeleteByIds(ids);
        }

        public NsDcChungTuChiTiet FindByChungTuIdAndMlnsId(Guid voucherId, Guid mlnsId)
        {
            return _chungTuChiTietRepository.FindByChungTuIdAndMlnsId(voucherId, mlnsId);
        }

        public IEnumerable<NsDcChungTuChiTietQuery> FindByCondition(EstimationVoucherDetailCriteria searchCondition)
        {
            return _chungTuChiTietRepository.FindByCondition(searchCondition);
        }

        public IEnumerable<NsDcChungTuChiTietQuery> FindAllNSDCChungTuByCondition(EstimationVoucherDetailCriteria searchCondition)
        {
            return _chungTuChiTietRepository.FindAllNSDCChungTuByCondition(searchCondition);
        }

        public IEnumerable<NsDcChungTuChiTiet> FindByCondition(Expression<Func<NsDcChungTuChiTiet, bool>> predicate)
        {
            return _chungTuChiTietRepository.FindAll(predicate);
        }

        public IEnumerable<NsDcChungTuChiTietQuery> FindDuToanByCondition(EstimationVoucherDetailCriteria searchCondition)
        {
            return _chungTuChiTietRepository.FindDuToanByCondition(searchCondition);
        }

        public IEnumerable<NsDcChungTuChiTietQuery> FindByConditionTongSo(EstimationVoucherDetailCriteria searchCondition)
        {
            return _chungTuChiTietRepository.FindByConditionTongSo(searchCondition);
        }


        public IEnumerable<NsDcChungTuChiTietQuery> FindChungTuChiTietForDcDuToanTongHopReport(EstimationVoucherDetailCriteria searchCondition)
        {
            return _chungTuChiTietRepository.FindChungTuChiTietForDcDuToanTongHopReport(searchCondition);
        }

        public NsDcChungTuChiTiet FindById(Guid id)
        {
            return _chungTuChiTietRepository.Find(id);
        }

        public IEnumerable<DataDieuChinhQuery> FindDataDieuChinh(EstimationVoucherDetailCriteria searchCondition)
        {
            return _chungTuChiTietRepository.FindDataDieuChinh(searchCondition);
        }

        public int Update(NsDcChungTuChiTiet entity)
        {
            return _chungTuChiTietRepository.Update(entity);
        }

        public void BulkInsert(List<NsDcChungTuChiTiet> lstData)
        {
            _chungTuChiTietRepository.BulkInsert(lstData);
        }

        public IEnumerable<NsDcChungTuChiTietQuery> FindByVoucherID(Guid voucherID)
        {
            return _chungTuChiTietRepository.FindByVoucherID(voucherID);
        }

        public IEnumerable<NsDcChungTuChiTietQuery> FindByUnits(string maDonVi, int namLamViec, string iidChungTuNhan)
        {
            return _chungTuChiTietRepository.FindByUnits(maDonVi, namLamViec, iidChungTuNhan);
        }
    }
}
