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
    public class MucLucNganSachPopupService : IService<NsMucLucNganSach>, IMucLucNganSachPopupService
    {
        private readonly IMucLucNganSachRepository _repository;

        public MucLucNganSachPopupService(IMucLucNganSachRepository repository)
        {
            _repository = repository;
        }
        public override IEnumerable<NsMucLucNganSach> FindAll(AuthenticationInfo authenticationInfo)
        {
            var data = _repository.FindByNamLamViec(authenticationInfo.YearOfWork);

            return data;
        }
    }
}
