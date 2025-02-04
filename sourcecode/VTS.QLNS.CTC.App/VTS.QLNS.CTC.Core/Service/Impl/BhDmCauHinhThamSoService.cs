using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhDmCauHinhThamSoService : IService<BhDmCauHinhThamSo>, IBhDmCauHinhThamSoService
    {
        private readonly IBhDmCauHinhThamSoRepository _repository;

        public BhDmCauHinhThamSoService(IBhDmCauHinhThamSoRepository repository)
        {
            _repository = repository;
        }

        public void Add(BhDmCauHinhThamSo entity)
        {

            _repository.Add(entity);
        }

        public void AddRange(IEnumerable<BhDmCauHinhThamSo> entities)
        {
            _repository.AddRange(entities);
        }

        public void Delete(BhDmCauHinhThamSo entity)
        {
            _repository.Delete(entity);
        }

        public IEnumerable<BhDmCauHinhThamSo> FindAll()
        {
            return _repository.FindAll();
        }

        public BhDmCauHinhThamSo FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public void Update(BhDmCauHinhThamSo entity)
        {
            _repository.Update(entity);
        }

        public void UpdateRange(IEnumerable<BhDmCauHinhThamSo> entities)
        {
            _repository.UpdateRange(entities);
        }

        public override IEnumerable<BhDmCauHinhThamSo> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _repository.FindAll().Where(x=> x.INamLamViec == authenticationInfo.YearOfWork).OrderBy(s=>s.SMa).ToList();
        }

        public override void AddOrUpdateRange(IEnumerable<BhDmCauHinhThamSo> listEntities, AuthenticationInfo authenticationInfo)
        {
            _repository.AddOrUpdateRange(listEntities, authenticationInfo);
        }

        public IEnumerable<BhDmCauHinhThamSo> FindByCondition(Expression<Func<BhDmCauHinhThamSo, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }
    }
}
