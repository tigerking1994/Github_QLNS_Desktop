using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlCanBoCheDoBHXHService : ITlCanBoCheDoBHXHService
    {
        private ITlCanBoCheDoBHXHRepository _repository;
        public TlCanBoCheDoBHXHService(ITlCanBoCheDoBHXHRepository iTlCanBoCheDoBHXHRepository)
        {
            _repository = iTlCanBoCheDoBHXHRepository;
        }

        public int AddRangeCanBoCheDo(IEnumerable<TlCanBoCheDoBHXH> entities)
        {
            return _repository.AddRange(entities);
        }

        public int DeleteCanBoCheDo(Guid id)
        {
            TlCanBoCheDoBHXH entity = FindCanBoCheDo(id);
            if (entity != null)
            {
                return _repository.Delete(entity);
            }
            return 0;
        }

        public bool ExistCanBoCheDo(string maCanBo)
        {
            return _repository.ExistCanBoCheDo(maCanBo);
        }

        public TlCanBoCheDoBHXH FindCanBoCheDo(params object[] keyValues)
        {
            return _repository.Find(keyValues);
        }

        public IEnumerable<TlCanBoCheDoBHXHQuery> GetDataCheDoBHXH(string maCanBo)
        {
            return _repository.GetDataCheDoBHXH(maCanBo);
        }

        public int RemoveRange(IEnumerable<TlCanBoCheDoBHXH> canBoCheDos)
        {
            return _repository.RemoveRange(canBoCheDos);
        }

        public int UpdateCanBoCheDo(TlCanBoCheDoBHXH entity)
        {
            return _repository.Update(entity);
        }

        public IEnumerable<TlCanBoCheDoBHXH> FindByMaCanBo(string maCanBo)
        {
            var predicate = PredicateBuilder.True<TlCanBoCheDoBHXH>();
            predicate = predicate.And(x => maCanBo.Equals(x.SMaCanBo));
            return _repository.FindAll(predicate);
        }

        public IEnumerable<TlCanBoCheDoBHXH> GetSoNgayHuongBHXH(string maCanBo)
        {
            return _repository.GetSoNgayHuongBHXH(maCanBo);
        }

        public IEnumerable<TlCanBoCheDoBHXH> FindAll(Expression<Func<TlCanBoCheDoBHXH, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }

        public IEnumerable<TlCanBoCheDoBHXHQuery> ExportCanBoCheDo(int year, string months)
        {
            return _repository.ExportCanBoCheDo(year, months);
        }

        public TlCanBoCheDoBHXH FindByCondition(string maCanBo, string maCheDo)
        {
            return _repository.FindByCondition(maCanBo, maCheDo);
        }

        public IEnumerable<TlCanBoCheDoBHXHQuery> GetCanBoCheDoIndex(string maCanBo)
        {
            return _repository.GetCanBoCheDoIndex(maCanBo);
        }
    }
}
