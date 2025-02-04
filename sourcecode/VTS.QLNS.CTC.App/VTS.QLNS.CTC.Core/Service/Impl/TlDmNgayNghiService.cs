using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDmNgayNghiService : IService<TlDmNgayNghi>, ITlDmNgayNghiService
    {
        private ITlDmNgayNghiRepository _repository;
        public TlDmNgayNghiService(ITlDmNgayNghiRepository iTlDmNgayNghiRepository)
        {
            _repository = iTlDmNgayNghiRepository;
        }

        public void Add(TlDmNgayNghi nhDmTiGia)
        {
            _repository.Add(nhDmTiGia);
        }

        public int AddRange(IEnumerable<TlDmNgayNghi> holidays)
        {
            return _repository.AddRange(holidays);
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public IEnumerable<TlDmNgayNghi> FindAll()
        {
            return _repository.FindAll().ToList();
        }

        public TlDmNgayNghi FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public IEnumerable<TlDmNgayNghi> FindByYear(int year)
        {
            return _repository.FindByYear(year);
        }

        public void Update(TlDmNgayNghi nhDmTiGia)
        {
            _repository.Update(nhDmTiGia);
        }
    }
}
