using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TLDanhMucChucVuService : IService<TlDmChucVu>
    {
        private readonly ITLDanhMucChucVuRepository _tLDanhMucChucVuRepository;

        public TLDanhMucChucVuService(ITLDanhMucChucVuRepository tLDanhMucChucVuRepository)
        {
            _tLDanhMucChucVuRepository = tLDanhMucChucVuRepository;
        }

        public override void AddOrUpdateRange(IEnumerable<TlDmChucVu> listEntities, AuthenticationInfo authenticationInfo)
        {
            _tLDanhMucChucVuRepository.AddOrUpdateRange(listEntities);
        }

        public override IEnumerable<TlDmChucVu> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _tLDanhMucChucVuRepository.FindAll().OrderByDescending(x => x.HeSoCv).ToList();
        }
    }
}
