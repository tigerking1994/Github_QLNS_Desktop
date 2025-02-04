using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NsPhongBanService : IService<DmBQuanLy>, INsPhongBanService
    {
        private INsPhongBanRepository _phongBanRepository;

        public NsPhongBanService(INsPhongBanRepository phongBanRepository)
        {
            _phongBanRepository = phongBanRepository;
        }

        public override void AddOrUpdateRange(IEnumerable<DmBQuanLy> listEntities, AuthenticationInfo authenticationInfo)
        {
            var time = DateTime.Now;
            foreach (var item in listEntities)
            {
                item.INamLamViec = authenticationInfo.YearOfWork;
                if (item.IsModified)
                {
                    if (Guid.Empty.Equals(item.Id))
                    {
                        item.DNgayTao = time;
                        item.SNguoiTao = authenticationInfo.Principal;
                        item.DNgaySua = null;
                        item.SNguoiSua = null;
                    }
                    else
                    {
                        item.DNgaySua = time;
                        item.SNguoiSua = authenticationInfo.Principal;
                    }
                }
            }
            _phongBanRepository.AddOrUpdateRange(listEntities);
        }

        public int countPhongBanByNamLamViec(int namLamViec)
        {
            return _phongBanRepository.countPhongBanByNamLamViec(namLamViec);
        }

        public override IEnumerable<DmBQuanLy> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _phongBanRepository.FindAll(p => authenticationInfo.YearOfWork == p.INamLamViec);
        }

        public List<DmBQuanLy> FindByCondition(Expression<Func<DmBQuanLy, bool>> predicate)
        {
            return _phongBanRepository.FindAll(predicate).ToList();
        }
    }
}
