using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDmThueThuNhapCaNhanNq104Service : IService<TlDmThueThuNhapCaNhanNq104>
    {
        private readonly ITlDmThueThuNhapCaNhanNq104Repository _tTlDmThueThuNhapCaNhanRepository;

        public TlDmThueThuNhapCaNhanNq104Service(ITlDmThueThuNhapCaNhanNq104Repository tTlDmThueThuNhapCaNhanRepository)
        {
            _tTlDmThueThuNhapCaNhanRepository = tTlDmThueThuNhapCaNhanRepository;
        }
        public override void AddOrUpdateRange(IEnumerable<TlDmThueThuNhapCaNhanNq104> listEntities, AuthenticationInfo authenticationInfo)
        {
            _tTlDmThueThuNhapCaNhanRepository.AddOrUpdateRange(listEntities);
        }

        public override IEnumerable<TlDmThueThuNhapCaNhanNq104> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _tTlDmThueThuNhapCaNhanRepository.FindAll().OrderBy(x => x.LoaiThue).ToList();
        }
    }
}
