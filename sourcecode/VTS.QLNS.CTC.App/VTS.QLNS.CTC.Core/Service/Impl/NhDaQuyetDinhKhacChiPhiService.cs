using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhDaQuyetDinhKhacChiPhiService : IService<NhDaQuyetDinhKhacChiPhi>, INhDaQuyetDinhKhacChiPhiService
    {
        private readonly INhDaQuyetDinhKhacChiPhiRepository _repository;


        public NhDaQuyetDinhKhacChiPhiService(
            INhDaQuyetDinhKhacChiPhiRepository repository)
        {
            _repository = repository;

        }
        public void Add(NhDaQuyetDinhKhacChiPhi entity)
        {
            _repository.Add(entity);
        }

        public void AddRange(IEnumerable<NhDaQuyetDinhKhacChiPhi> data)
        {
            _repository.AddRange(data);
        }

        public void Delete(NhDaQuyetDinhKhacChiPhi entity)
        {
            _repository.Delete(entity);
        }

        public IEnumerable<NhDaQuyetDinhKhacChiPhi> FindAll()
        {
            return _repository.FindAll();
        }

        public IEnumerable<NhDaQuyetDinhKhacChiPhi> FindAll(Expression<Func<NhDaQuyetDinhKhacChiPhi, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }

        public NhDaQuyetDinhKhacChiPhi FindById(Guid id)
        {
            return _repository.Find(id);
        }
        public IEnumerable<NhDaQuyetDinhKhacChiPhi> FindByQuyetDinhKhacId(Guid id)
        {
            return _repository.FindByQuyetDinhKhacId(id);
        }

        public void Update(NhDaQuyetDinhKhacChiPhi entity)
        {
            _repository.Update(entity);
        }
        public void UpdateRange(IEnumerable<NhDaQuyetDinhKhacChiPhi> data)
        {
            _repository.UpdateRange(data);
        }
        
        public void RemoveRange(IEnumerable<NhDaQuyetDinhKhacChiPhi> data)
        {
            _repository.RemoveRange(data);
        }
        public IEnumerable<NhDaQuyetDinhKhacChiPhi> FindByCondition(Expression<Func<NhDaQuyetDinhKhacChiPhi, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }

    }
}
