using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDmTietTieuMucNganhService : IService<TlDmTietTieuMucNganh>
    {
        private readonly ITlDmTietTieuMucNganhRepository _tlDmTietTieuMucNganhRepository;

        public TlDmTietTieuMucNganhService(ITlDmTietTieuMucNganhRepository tlDmTietTieuMucNganhRepository)
        {
            _tlDmTietTieuMucNganhRepository = tlDmTietTieuMucNganhRepository;
        }

        public override void AddOrUpdateRange(IEnumerable<TlDmTietTieuMucNganh> listEntities, AuthenticationInfo authenticationInfo)
        {

            _tlDmTietTieuMucNganhRepository.AddOrUpdateRange(listEntities);
        }

        public override IEnumerable<TlDmTietTieuMucNganh> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _tlDmTietTieuMucNganhRepository.FindAll().ToList();
        }
    }
}
