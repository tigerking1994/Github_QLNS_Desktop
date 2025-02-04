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
    public class MlnsChildService : IService<NsMucLucNganSach>, IMlnsChildService
    {
        private readonly IMucLucNganSachRepository _repository;

        public MlnsChildService(IMucLucNganSachRepository repository)
        {
            _repository = repository;
        }
        public override IEnumerable<NsMucLucNganSach> FindAll(AuthenticationInfo authenticationInfo)
        {
            var data = _repository.FindAllNotIn(new List<string>(),authenticationInfo.YearOfWork);

            return data;
        }

        public override IEnumerable<NsMucLucNganSach> FindAll(AuthenticationInfo authenticationInfo, bool isPopup, List<string> notIns)
        {
            var data = _repository.FindAllNotIn(notIns, authenticationInfo.YearOfWork);
            return data;
        }
    }
}
