using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class QtcQBHXHChiTietGiaiThichService : IQtcQBHXHChiTietGiaiThichService
    {
        private readonly IQtcQBHXHChiTietGiaiThichRepository _repository;
        public QtcQBHXHChiTietGiaiThichService (IQtcQBHXHChiTietGiaiThichRepository repository)
        {
            _repository = repository;
        }

        public void Add(BhQtcQBHXHChiTietGiaiThich chungTuChiTietGiaiThich)
        {
            _repository.Add(chungTuChiTietGiaiThich);
        }

        public int Delete(Guid id)
        {
            return _repository.Delete(id);
        }

        public void RemoveGiaiThichBangLoiTheoChungTu(Guid id)
        {
           _repository.RemoveGiaiThichBangLoiTheoChungTu(id);
        }

        public BhQtcQBHXHChiTietGiaiThich FindByCondition(BhQtcQBHXHChiTietGiaiThichCriteria condition)
        {
            return _repository.FindByCondition(condition);
        }

        public BhQtcQBHXHChiTietGiaiThich FindById(Guid id)
        {
            return _repository.FindById(id);
        }

        public List<BhQtcQBHXHChiTietGiaiThichQuery> GetGiaiThichBangLoiTheoDonVi(int yearOfWork, string sMaDonVi, int iQuy, string sMABHXH)
        {
            return _repository.GetGiaiThichBangLoiTheoDonVi(yearOfWork, sMaDonVi, iQuy, sMABHXH);
        }

        public void Update(BhQtcQBHXHChiTietGiaiThich chungTuChiTietGiaiThich)
        {
            _repository.Update(chungTuChiTietGiaiThich);
        }
    }
}
