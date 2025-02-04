using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public void Add(HtNhom entity)
        {
            _groupRepository.Add(entity);
        }

        public void Delete(Guid id)
        {
            _groupRepository.DeleteGroup(id);
        }

        public IEnumerable<HtNhom> FindAll(Expression<Func<HtNhom, bool>> predicate)
        {
            return _groupRepository.FindAll(predicate);
        }

        public HtNhom FindById(Guid id)
        {
            Expression<Func<HtNhom, bool>> predicate = c => c.Id.Equals(id);
            return _groupRepository.FindByPredicate(predicate);
        }

        public HtNhom FindByPredicate(Expression<Func<HtNhom, bool>> predicate)
        {
            return _groupRepository.FindByPredicate(predicate);
        }

        public IEnumerable<HtNhom> LoadEagerGroups(Expression<Func<HtNhom, bool>> predicate)
        {
            return _groupRepository.LoadEagerGroups(predicate);
        }

        public void LockOrUnLock(Guid id, bool isActivated)
        {
            _groupRepository.LockUnLockGroup(id, isActivated);
        }

        public void RemoveRange(IEnumerable<HtNhom> entities)
        {
            _groupRepository.RemoveRange(entities);
        }

        public void Update(HtNhom entity)
        {
            _groupRepository.UpdateNhom(entity);
        }

        public void UpdateRange(IEnumerable<HtNhom> entities)
        {
            _groupRepository.UpdateRange(entities);
        }
    }
}
