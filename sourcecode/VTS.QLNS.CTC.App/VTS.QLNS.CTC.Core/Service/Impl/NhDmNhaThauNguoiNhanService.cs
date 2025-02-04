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
    public class NhDmNhaThauNguoiNhanService : INhDmNhaThauNguoiNhanService
    {
        private readonly INhDmNhaThauNguoiNhanRepository _repository;

        public NhDmNhaThauNguoiNhanService(INhDmNhaThauNguoiNhanRepository repository)
        {
            _repository = repository;
        }

        public void Add(NhDmNhaThauNguoiNhan nhDmNhaThauNguoiNhan)
        {
            _repository.Add(nhDmNhaThauNguoiNhan);
        }

        public void Update(NhDmNhaThauNguoiNhan nhDmNhaThauNguoiNhan)
        {
            _repository.Update(nhDmNhaThauNguoiNhan);
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public NhDmNhaThauNguoiNhan FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public IEnumerable<NhDmNhaThauNguoiNhan> FindByCondition(Expression<Func<NhDmNhaThauNguoiNhan, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }

        public IEnumerable<NhDmNhaThauNguoiNhan> FindAll()
        {
            return _repository.FindAll();
        }
    }
}
