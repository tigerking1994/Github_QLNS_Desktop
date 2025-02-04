using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhDmNhaThauService : IService<NhDmNhaThau>, INhDmNhaThauService
    {
        private INhDmNhaThauRepository _nhDmNhaThauRepository;

        public NhDmNhaThauService(INhDmNhaThauRepository nhDmNhaThauRepository)
        {
            _nhDmNhaThauRepository = nhDmNhaThauRepository;
        }

        public int Add(NhDmNhaThau entity)
        {
            return _nhDmNhaThauRepository.Add(entity);
        }

        public int AddRange(List<NhDmNhaThau> entities)
        {
            return _nhDmNhaThauRepository.AddRange(entities);
        }

        public int Update(NhDmNhaThau entity)
        {
            return _nhDmNhaThauRepository.Update(entity);
        }

        public int Delete(Guid id)
        {
            return _nhDmNhaThauRepository.Delete(id);
        }
        public override void AddOrUpdateRange(IEnumerable<NhDmNhaThau> listEntities, AuthenticationInfo authenticationInfo)
        {
            var lstInsert = listEntities.Where(t => t.Id.IsNullOrEmpty() && !t.SMaNhaThau.IsEmpty() && !t.IsDeleted).ToList();
            var lstUpdate = listEntities.Where(t => !t.Id.IsNullOrEmpty() && !t.IsDeleted).ToList();
            var lstDelete = listEntities.Where(t => !t.Id.IsNullOrEmpty() && t.IsDeleted).ToList();

            var lstAll = _nhDmNhaThauRepository.FindAll();
            var lstNotDeleteUpdate = lstAll.Where(x => !lstDelete.Select(y => y.Id).Contains(x.Id) && !lstUpdate.Select(y => y.Id).Contains(x.Id));
            var lstCheckDuplicate = lstNotDeleteUpdate.Concat(lstInsert).Concat(lstUpdate);
            var lstMaDuplicate = lstCheckDuplicate.GroupBy(x => x.SMaNhaThau.ToUpper()).Where(g => g.Count() > 1).Select(y => y.Key).ToList();

            if (lstMaDuplicate != null && lstMaDuplicate.Count() > 0)
            {
                throw new ArgumentException("Mã nhà thầu " + lstMaDuplicate.FirstOrDefault() + " bị lặp, vui lòng thử lại!");
            }

            if (lstInsert != null && lstInsert.Count() > 0)
            {
                lstInsert.ForEach(item => { item.Id = Guid.NewGuid(); });
                _nhDmNhaThauRepository.AddRange(lstInsert);
            }
            if (lstUpdate != null && lstUpdate.Count() > 0)
            {
                _nhDmNhaThauRepository.UpdateRange(lstUpdate);
            }
            if (lstDelete != null && lstDelete.Count() > 0)
            {
                _nhDmNhaThauRepository.RemoveRange(lstDelete);
            }
        }

        public NhDmNhaThau FindById(Guid id)
        {
            return _nhDmNhaThauRepository.Find(id);
        }

        IEnumerable<NhDmNhaThau> INhDmNhaThauService.FindAll()
        {
            return _nhDmNhaThauRepository.FindAll();
        }
        public override IEnumerable<NhDmNhaThau> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _nhDmNhaThauRepository.FindAll().OrderBy(x => x.SMaNhaThau);
        }

        public object FindByCondition(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NhDmNhaThau> FindAll(Expression<Func<NhDmNhaThau, bool>> predicate)
        {
            return _nhDmNhaThauRepository.FindAll();
        }
        public List<NhDmNhaThau> FindAll() => (List<NhDmNhaThau>)_nhDmNhaThauRepository.FindAll();
    }
}
