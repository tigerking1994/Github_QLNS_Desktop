using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDmPhuCapHeThongService : IService<TlDmPhuCap>, ITlDmPhuCapHeThongService
    {
        private ITlDmPhuCapRepository _tlDmPhuCapRepository;

        public TlDmPhuCapHeThongService(ITlDmPhuCapRepository tlDmPhuCapRepository)
        {
            _tlDmPhuCapRepository = tlDmPhuCapRepository;
        }

        public override void AddOrUpdateRange(IEnumerable<TlDmPhuCap> listEntities, AuthenticationInfo authenticationInfo)
        {
            if (listEntities != null && listEntities.Any(n => n.IsDeleted))
            {
                _tlDmPhuCapRepository.UpdateCanBoPhuCapWhenChangePhuCap(authenticationInfo.Month, authenticationInfo.YearOfWork, listEntities.Where(n => n.IsDeleted).Select(n => n.Id).ToList(), true);
            }
            _tlDmPhuCapRepository.AddOrUpdateRange(listEntities);
            if (listEntities != null && listEntities.Any(n => !n.IsDeleted))
            {
                _tlDmPhuCapRepository.UpdateCanBoPhuCapWhenChangePhuCap(authenticationInfo.Month, authenticationInfo.YearOfWork, listEntities.Where(n => !n.IsDeleted).Select(n => n.Id).ToList(), false);
            }   
        }

        public override IEnumerable<TlDmPhuCap> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _tlDmPhuCapRepository.FindAll().Where(x => x.MaPhuCap == "HETHONG" || x.Parent == "HETHONG").OrderBy(x => x.XauNoiMa).ToList();
        }

        public override IEnumerable<TlDmPhuCap> FindAllPhuCapHeThong(AuthenticationInfo authenticationInfo)
        {
            return _tlDmPhuCapRepository.FindAllPhuCapHeThong();
        }
    }
}
