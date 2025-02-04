using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;
using System.Linq;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhDmCongThucOutputService : IService<NhDmCongThucOutput>, INhDmCongThucOutputService
    {
        private readonly INhDmCongThucOutputRepository _repository;

        public NhDmCongThucOutputService(INhDmCongThucOutputRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<NhDmCongThucOutput> FindAll()
        {
            return _repository.FindAll();
        }
        public override IEnumerable<NhDmCongThucOutput> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _repository.FindAll(authenticationInfo);
        }

        public override void AddOrUpdateRange(IEnumerable<NhDmCongThucOutput> listEntities, AuthenticationInfo authenticationInfo)
        {
            var lstInsert = listEntities.Where(t => t.Id.IsNullOrEmpty() && !t.SMaOutput.IsEmpty() && !t.IsDeleted).ToList();
            var lstUpdate = listEntities.Where(t => !t.Id.IsNullOrEmpty() && !t.IsDeleted).ToList();
            var lstDelete = listEntities.Where(t => !t.Id.IsNullOrEmpty() && t.IsDeleted).ToList();

            var lstAll = _repository.FindAll();
            var lstNotDeleteUpdate = lstAll.Where(x => !lstDelete.Select(y => y.Id).Contains(x.Id) && !lstUpdate.Select(y => y.Id).Contains(x.Id));
            var lstCheckDuplicate = lstNotDeleteUpdate.Concat(lstInsert).Concat(lstUpdate);
            var lstMaDuplicate = lstCheckDuplicate.GroupBy(x => x.SMaOutput.ToUpper()).Where(g => g.Count() > 1).Select(y => y.Key).ToList();

            if (lstMaDuplicate != null && lstMaDuplicate.Count() > 0)
            {
                throw new ArgumentException("Mã công thức Output " + lstMaDuplicate.FirstOrDefault() + " bị lặp, vui lòng thử lại!");
            }

            if (lstInsert != null && lstInsert.Count() > 0)
            {
                lstInsert.ForEach(item => { item.Id = Guid.NewGuid(); });
                _repository.AddRange(lstInsert);
            }
            if (lstUpdate != null && lstUpdate.Count() > 0)
            {
                _repository.UpdateRange(lstUpdate);
            }
            if (lstDelete != null && lstDelete.Count() > 0)
            {
                _repository.RemoveRange(lstDelete);
            }
        }

        public IEnumerable<NhDmCongThucOutput> FindAll(Expression<Func<NhDmCongThucOutput, bool>> predicate)
        {
            return _repository.FindAll();
        }
    }
}
