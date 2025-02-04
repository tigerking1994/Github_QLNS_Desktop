using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhDmMucDongBHXHService : IService<BhDmMucDongBHXH>, IBhDmMucDongBHXHService
    {
        private readonly IBhDmMucDongBHXHRepository _repository;
        public BhDmMucDongBHXHService(IBhDmMucDongBHXHRepository repository)
        {
            _repository = repository;
        }
        public void Add(BhDmMucDongBHXH entity)
        {
            _repository.Add(entity);
        }

        public override IEnumerable<BhDmMucDongBHXH> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _repository.FindAll(authenticationInfo);
        }
        public void AddRange(IEnumerable<BhDmMucDongBHXH> entities)
        {
            _repository.AddRange(entities);
        }
        public void Delete(BhDmMucDongBHXH entity)
        {
            _repository.Delete(entity);
        }

        public IEnumerable<BhDmMucDongBHXH> FindAll()
        {
            return _repository.FindAll();
        }

        public BhDmMucDongBHXH FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public BhDmMucDongBHXH FindByParentId(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(BhDmMucDongBHXH entity)
        {
            _repository.Update(entity);
        }

        public void UpdateRange(IEnumerable<BhDmMucDongBHXH> entities)
        {
            throw new NotImplementedException();
        }
        public override void AddOrUpdateRange(IEnumerable<BhDmMucDongBHXH> listEntities, AuthenticationInfo authenticationInfo)
        {
            _repository.AddOrUpdateRange(listEntities, authenticationInfo);
        }
    }
}

