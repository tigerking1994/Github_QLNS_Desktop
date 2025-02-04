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
    public class NhTtThanhToanChiTietService : IService<NhTtThanhToanChiTiet>, INhTtThanhToanChiTietService
    {
        private readonly INhTtThanhToanChiTietRepository _repository;

        public NhTtThanhToanChiTietService(INhTtThanhToanChiTietRepository repository)
        {
            _repository = repository;
        }

        public void Add(NhTtThanhToanChiTiet nhTtThanhToanChiTiet)
        {
            _repository.Add(nhTtThanhToanChiTiet);
        }

        public void Update(NhTtThanhToanChiTiet nhTtThanhToanChiTiet)
        {
            _repository.Update(nhTtThanhToanChiTiet);
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public void DeleteByDeNghiThanhToan(Guid deNghiThanhToan)
        {
            _repository.DeleteByDeNghiThanhToan(deNghiThanhToan);
        }

        public NhTtThanhToanChiTiet FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public IEnumerable<NhTtThanhToanChiTiet> FindByCondition(Expression<Func<NhTtThanhToanChiTiet, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }

        public IEnumerable<NhTtThanhToanChiTiet> FindByIdThanhToan(Guid id)
        {
            return _repository.FindAll(x => x.IIdDeNghiThanhToanId == id);
        }
    }
}
