using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlQsChungTuChiTietNq104Service : ITlQsChungTuChiTietNq104Service
    {
        private ITlQsChungTuChiTietNq104Repository _tlQsChungTuChiTietRepository;

        public TlQsChungTuChiTietNq104Service(ITlQsChungTuChiTietNq104Repository tlQsChungTuChiTietRepository)
        {
            _tlQsChungTuChiTietRepository = tlQsChungTuChiTietRepository;
        }
        public int Add(IEnumerable<TlQsChungTuChiTietNq104> entites)
        {
            return _tlQsChungTuChiTietRepository.AddRange(entites);
        }

        public int DeleteByChungTuId(Guid ParentId)
        {
            return _tlQsChungTuChiTietRepository.DeleteParent(ParentId);
        }

        public int DeleteId(Guid id)
        {
            return _tlQsChungTuChiTietRepository.Delete(id);
        }

        public IEnumerable<TlQsChungTuChiTietNq104> FindAll()
        {
            return _tlQsChungTuChiTietRepository.FindAll();
        }

        public IEnumerable<TlQsChungTuChiTietNq104> FindAll(Expression<Func<TlQsChungTuChiTietNq104, bool>> predicate)
        {
            return _tlQsChungTuChiTietRepository.FindAll(predicate);
        }

        public TlQsChungTuChiTietNq104 FirstOrDefault(Expression<Func<TlQsChungTuChiTietNq104, bool>> predicate)
        {
            return _tlQsChungTuChiTietRepository.FirstOrDefault(predicate);
        }

        public int UpDate(TlQsChungTuChiTietNq104 entity)
        {
            return _tlQsChungTuChiTietRepository.Update(entity);
        }

        public int UpDateRange(IEnumerable<TlQsChungTuChiTietNq104> entites)
        {
            return _tlQsChungTuChiTietRepository.UpdateRange(entites);
        }
    }
}
