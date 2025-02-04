using System;
using VTS.QLNS.CTC.Core.Domain;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;
using System.Linq;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhDmCheDoBhxhService : IService<BhDmCheDoBhxh>, IBhDmCheDoBhxhService
    {
        private readonly IBhDmCheDoBhxhRepository _repository;
        public BhDmCheDoBhxhService(IBhDmCheDoBhxhRepository repository)
        {
            _repository = repository;
        }
        public void Add(BhDmCheDoBhxh entity)
        {
            _repository.Add(entity);
        }

        public void AddRange(IEnumerable<BhDmCheDoBhxh> entities)
        {
            _repository.AddRange(entities);
        }

        public void Delete(BhDmCheDoBhxh entity)
        {
            _repository.Delete(entity);
        }

        public override IEnumerable<BhDmCheDoBhxh> FindAll(AuthenticationInfo authenticationInfo)
        {
            var bhDmCheDoBhxhs = _repository.FindAll();
            var result = new List<BhDmCheDoBhxh>();
            var parentList = bhDmCheDoBhxhs.Where(s => s.IIdCheDoCha == null).OrderBy(s => s.IIdMaCheDo);
            var childrenList = bhDmCheDoBhxhs.Where(s => s.IIdCheDoCha.HasValue).OrderBy(s => s.IIdMaCheDo).ToList();
            if (!parentList.Any())
            {
                result = childrenList.ToList();
            }
            else
            {
                foreach (var item in parentList)
                {
                    item.BHangCha = !item.IIdCheDoCha.HasValue;
                    result.Add(item);
                    result = GetChildren(item.Id, childrenList, result);
                }
            }
            return result;
        }

        private List<BhDmCheDoBhxh> GetChildren(Guid parentId, List<BhDmCheDoBhxh> childrenList, List<BhDmCheDoBhxh> result)
        {
            foreach (var item in childrenList.Where(x => x.IIdCheDoCha.Equals(parentId)))
            {
                item.TenCheDoCha = result.FirstOrDefault(x => x.Id.Equals(item.IIdCheDoCha))?.STenCheDo;
                result.Add(item);
                result = GetChildren(item.Id, childrenList, result);
            }
            return result;
        }

        public override void AddOrUpdateRange(IEnumerable<BhDmCheDoBhxh> listEntities, AuthenticationInfo authenticationInfo)
        {
            _repository.AddOrUpdateRange(listEntities, authenticationInfo);
        }

        public IEnumerable<BhDmCheDoBhxh> FindAll()
        {
            return _repository.FindAll();
        }

        public BhDmCheDoBhxh FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public BhDmCheDoBhxh FindByParentId(Guid id)
        {
            return _repository.FindByParentId(id);
        }

        public void Update(BhDmCheDoBhxh entity)
        {
            _repository.Update(entity);
        }

        public void UpdateRange(IEnumerable<BhDmCheDoBhxh> entities)
        {
            _repository.UpdateRange(entities);
        }
    }
}
