using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NsSktNganhThamDinhChiTietSktService : IService<NsSktNganhThamDinhChiTietSkt>, INsSktNganhThamDinhChiTietSktService
    {
        private readonly INsSktNganhThamDinhChiTietSktRepository _nsSktNganhThamDinhChiTietSktRepository;

        public NsSktNganhThamDinhChiTietSktService(INsSktNganhThamDinhChiTietSktRepository nsSktNganhThamDinhChiTietSktRepository)
        {
            _nsSktNganhThamDinhChiTietSktRepository = nsSktNganhThamDinhChiTietSktRepository;
        }

        public void AddOrUpdateRange(IEnumerable<NsSktNganhThamDinhChiTietSkt> listEntities)
        {
            _nsSktNganhThamDinhChiTietSktRepository.AddOrUpdateRange(listEntities);
        }

        public override IEnumerable<NsSktNganhThamDinhChiTietSkt> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _nsSktNganhThamDinhChiTietSktRepository.FindAll(t => t.INamLamViec == authenticationInfo.YearOfWork).ToList();
        }

        public IEnumerable<NsSktNganhThamDinhChiTietSkt> FindByCondition(Expression<Func<NsSktNganhThamDinhChiTietSkt, bool>> predicate)
        {
            return _nsSktNganhThamDinhChiTietSktRepository.FindAll(predicate);
        }

        public int AddRange(IEnumerable<NsSktNganhThamDinhChiTietSkt> listEntities)
        {
            return _nsSktNganhThamDinhChiTietSktRepository.AddRange(listEntities);
        }
        public int UpdateRange(IEnumerable<NsSktNganhThamDinhChiTietSkt> listEntities)
        {
            return _nsSktNganhThamDinhChiTietSktRepository.UpdateRange(listEntities);
        }

        public int Add(NsSktNganhThamDinhChiTietSkt entity)
        {
            return _nsSktNganhThamDinhChiTietSktRepository.Add(entity);
        }
        public int Update(NsSktNganhThamDinhChiTietSkt entity)
        {
            return _nsSktNganhThamDinhChiTietSktRepository.Update(entity);
        }

        public void BulkInsert(List<NsSktNganhThamDinhChiTietSkt> lstData)
        {
            _nsSktNganhThamDinhChiTietSktRepository.BulkInsert(lstData);
        }

        public void DeleteByIdChungTuSkt(Guid voucherId)
        {
            _nsSktNganhThamDinhChiTietSktRepository.DeleteByIdChungTuSkt(voucherId);
        }

        public void DeleteByYearOfWork(int namLamViec)
        {
            _nsSktNganhThamDinhChiTietSktRepository.DeleteByYearOfWork(namLamViec);
        }

        public List<JsonNsSktNganhThamDinhChiTietSktQuery> GetNsSktNganhThamDinhChiTietByChungTuId(List<Guid> iIds)
        {
            return _nsSktNganhThamDinhChiTietSktRepository.GetNsSktNganhThamDinhChiTietByChungTuId(iIds);
        }
    }
}
