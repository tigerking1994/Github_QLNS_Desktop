using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlQsChungTuChiTietService : ITlQsChungTuChiTietService
    {
        private ITlQsChungTuChiTietRepository _tlQsChungTuChiTietRepository;

        public TlQsChungTuChiTietService(ITlQsChungTuChiTietRepository tlQsChungTuChiTietRepository)
        {
            _tlQsChungTuChiTietRepository = tlQsChungTuChiTietRepository;
        }
        public int Add(IEnumerable<TlQsChungTuChiTiet> entites)
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

        public IEnumerable<TlQsChungTuChiTiet> FindAll()
        {
            return _tlQsChungTuChiTietRepository.FindAll();
        }

        public IEnumerable<TlQsChungTuChiTiet> FindAll(Expression<Func<TlQsChungTuChiTiet, bool>> predicate)
        {
            return _tlQsChungTuChiTietRepository.FindAll(predicate);
        }

        public TlQsChungTuChiTiet FirstOrDefault(Expression<Func<TlQsChungTuChiTiet, bool>> predicate)
        {
            return _tlQsChungTuChiTietRepository.FirstOrDefault(predicate);
        }

        public int UpDate(TlQsChungTuChiTiet entity)
        {
            return _tlQsChungTuChiTietRepository.Update(entity);
        }

        public int UpDateRange(IEnumerable<TlQsChungTuChiTiet> entites)
        {
            return _tlQsChungTuChiTietRepository.UpdateRange(entites);
        }
    }
}
