using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhDmDonViTinhService : INhDmDonViTinhService
    {
        private readonly INhDmDonViTinhRepository _nhDmDonViTinhRepository;

        public NhDmDonViTinhService(INhDmDonViTinhRepository nhDmDonViTinhRepository)
        {
            _nhDmDonViTinhRepository = nhDmDonViTinhRepository;
        }

        public IEnumerable<NhDmDonViTinh> FindAll()
        {
            return _nhDmDonViTinhRepository.FindAll();
        }

        public NhDmDonViTinh FindById(Guid? id) => _nhDmDonViTinhRepository.Find(id);

        public void Add(NhDmDonViTinh nhDmDonViTinh)
        {
            _nhDmDonViTinhRepository.Add(nhDmDonViTinh);
        }

        public void Update(NhDmDonViTinh nhDmDonViTinh)
        {
            _nhDmDonViTinhRepository.Update(nhDmDonViTinh);
        }
    }
}
