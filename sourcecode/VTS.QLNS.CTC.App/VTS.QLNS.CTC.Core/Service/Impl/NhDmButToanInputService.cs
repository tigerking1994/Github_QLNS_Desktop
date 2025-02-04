using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhDmButToanInputService : IService<NhDmButToanInput>, INhDmButToanInputService
    {
        private readonly INhDmButToanInputRepository _repository;

        public NhDmButToanInputService(INhDmButToanInputRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<NhDmButToanInput> FindAll()
        {
            return _repository.FindAll();
        }
        public override IEnumerable<NhDmButToanInput> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _repository.FindAll(authenticationInfo);
        }

        public override void AddOrUpdateRange(IEnumerable<NhDmButToanInput> listEntities, AuthenticationInfo authenticationInfo)
        {
            var lstInsert = listEntities.Where(t => t.Id.IsNullOrEmpty() && !t.SMaButToanInput.IsEmpty() && !t.IsDeleted).ToList();
            var lstUpdate = listEntities.Where(t => !t.Id.IsNullOrEmpty() && !t.IsDeleted).ToList();
            var lstDelete = listEntities.Where(t => !t.Id.IsNullOrEmpty() && t.IsDeleted).ToList();

            var lstAll = _repository.FindAll();
            var lstNotDeleteUpdate = lstAll.Where(x => !lstDelete.Select(y => y.Id).Contains(x.Id) && !lstUpdate.Select(y => y.Id).Contains(x.Id));
            var lstCheckDuplicate = lstNotDeleteUpdate.Concat(lstInsert).Concat(lstUpdate);
            var lstMaDuplicate = lstCheckDuplicate.GroupBy(x => x.SMaButToanInput.ToUpper()).Where(g => g.Count() > 1).Select(y => y.Key).ToList();

            if (lstMaDuplicate != null && lstMaDuplicate.Count() > 0)
            {
                throw new ArgumentException("Mã bút toán input " + lstMaDuplicate.FirstOrDefault() + " bị lặp, vui lòng thử lại!");
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

        public IEnumerable<NhDmButToanInput> FindAll(Expression<Func<NhDmButToanInput, bool>> predicate)
        {
            return _repository.FindAll();
        }
    }
}
