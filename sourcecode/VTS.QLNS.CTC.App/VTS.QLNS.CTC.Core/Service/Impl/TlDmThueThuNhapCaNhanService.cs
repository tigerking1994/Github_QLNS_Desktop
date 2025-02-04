using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDmThueThuNhapCaNhanService : IService<TlDmThueThuNhapCaNhan>
    {
        private readonly ITlDmThueThuNhapCaNhanRepository _tTlDmThueThuNhapCaNhanRepository;

        public TlDmThueThuNhapCaNhanService(ITlDmThueThuNhapCaNhanRepository tTlDmThueThuNhapCaNhanRepository)
        {
            _tTlDmThueThuNhapCaNhanRepository = tTlDmThueThuNhapCaNhanRepository;
        }
        public override void AddOrUpdateRange(IEnumerable<TlDmThueThuNhapCaNhan> listEntities, AuthenticationInfo authenticationInfo)
        {
            _tTlDmThueThuNhapCaNhanRepository.AddOrUpdateRange(listEntities);
        }

        public override IEnumerable<TlDmThueThuNhapCaNhan> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _tTlDmThueThuNhapCaNhanRepository.FindAll().OrderBy(x => x.LoaiThue).ToList();
        }
    }
}
