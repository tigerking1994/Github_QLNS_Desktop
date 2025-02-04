using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhDmXuatXuService : INhDmXuatXuService
    {
        private readonly INhDmXuatXuRepository _nhDmXuatXuRepository;

        public NhDmXuatXuService(INhDmXuatXuRepository nhDmXuatXuRepository)
        {
            _nhDmXuatXuRepository = nhDmXuatXuRepository;
        }

        public IEnumerable<NhDmXuatXu> FindAll()
        {
            return _nhDmXuatXuRepository.FindAll();
        }

        public NhDmXuatXu FindById(Guid? id) => _nhDmXuatXuRepository.Find(id);

        public void Add(NhDmXuatXu nhDmXuatXu)
        {
            _nhDmXuatXuRepository.Add(nhDmXuatXu);
        }

        public void Update(NhDmXuatXu nhDmXuatXu)
        {
            _nhDmXuatXuRepository.Update(nhDmXuatXu);
        }
    }
}
