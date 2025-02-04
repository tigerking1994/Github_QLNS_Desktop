using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class CauHinhCanCuService : IService<NsCauHinhCanCu>, ICauHinhCanCuService
    {
        private readonly ICauHinhCanCuRepository _cauHinhCanCuRepository;

        public CauHinhCanCuService(ICauHinhCanCuRepository cauHinhCanCuRepository)
        {
            _cauHinhCanCuRepository = cauHinhCanCuRepository;
        }

        public override void AddOrUpdateRange(IEnumerable<NsCauHinhCanCu> listEntities, AuthenticationInfo authenticationInfo)
        {
            foreach (NsCauHinhCanCu model in listEntities)
            {
                model.INamLamViec = authenticationInfo.YearOfWork;
            }
            _cauHinhCanCuRepository.AddOrUpdateRange(listEntities);
        }

        public override IEnumerable<NsCauHinhCanCu> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _cauHinhCanCuRepository.FindAll(t => t.INamLamViec == authenticationInfo.YearOfWork).ToList();
        }

        public IEnumerable<NsCauHinhCanCu> FindByCondition(Expression<Func<NsCauHinhCanCu, bool>> predicate)
        {
            return _cauHinhCanCuRepository.FindAll(predicate);
        }
    }
}
