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
    public class NhDmNhaThauNganHangService : INhDmNhaThauNganHangService
    {
        private readonly INhDmNhaThauNganHangRepository _repository;

        public NhDmNhaThauNganHangService(INhDmNhaThauNganHangRepository repository)
        {
            _repository = repository;
        }

        public void Add(NhDmNhaThauNganHang nhDmNhaThauNganHang)
        {
            _repository.Add(nhDmNhaThauNganHang);
        }

        public void Update(NhDmNhaThauNganHang nhDmNhaThauNganHang)
        {
            _repository.Update(nhDmNhaThauNganHang);
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public NhDmNhaThauNganHang FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public IEnumerable<NhDmNhaThauNganHang> FindByCondition(Expression<Func<NhDmNhaThauNganHang, bool>> predicate)
        {
           return _repository.FindAll(predicate);
        }

        public IEnumerable<NhDmNhaThauNganHang> FindAll()
        {
            return _repository.FindAll();
        }
    }
}
