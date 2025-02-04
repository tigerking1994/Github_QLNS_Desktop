using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class CauHinhMLNSService : IService<NsMucLucNganSach>, ICauHinhMLNSService
    {
        private IMucLucNganSachRepository _mucLucNganSachRepository;

        public CauHinhMLNSService(IMucLucNganSachRepository mucLucNganSachRepository)
        {
            _mucLucNganSachRepository = mucLucNganSachRepository;
        }

        public override void AddOrUpdateRange(IEnumerable<NsMucLucNganSach> listEntities, AuthenticationInfo authenticationInfo)
        {
            var time = DateTime.Now;
            foreach (var item in listEntities)
            {
                item.NamLamViec = authenticationInfo.YearOfWork;
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
            _mucLucNganSachRepository.AddOrUpdateRange(listEntities);
        }

        public override IEnumerable<NsMucLucNganSach> FindAll(AuthenticationInfo authenticationInfo)
        {
            var listLNS = _mucLucNganSachRepository.FindByNamLamViec(authenticationInfo.YearOfWork);
            listLNS = listLNS.Where(x => string.IsNullOrEmpty(x.L)
                            && string.IsNullOrEmpty(x.K)
                            && string.IsNullOrEmpty(x.M)
                            && string.IsNullOrEmpty(x.Tm)
                            && string.IsNullOrEmpty(x.Ttm)
                            && string.IsNullOrEmpty(x.Ng)
                            && string.IsNullOrEmpty(x.Tng));
            var listId = listLNS.Select(x => x.MlnsIdParent).ToHashSet();
            listLNS = listLNS.Where(x => !listId.Contains(x.MlnsId)).OrderBy(x => x.XauNoiMa).ToList();
            return listLNS;
            //return LNS.Where(mlns => isLNS(mlns, LNS)).OrderBy(t => t.XauNoiMa);
        }

        public override IEnumerable<NsMucLucNganSach> FindAllMlnsByLnsAndNamLmaViec(List<string> lns, AuthenticationInfo authenticationInfo)
        {
            return _mucLucNganSachRepository.FindAllMlnsByLnsAndNamLamViec(lns, authenticationInfo.YearOfWork);
        }

        private bool isLNS(NsMucLucNganSach mlns, IEnumerable<NsMucLucNganSach> ListOfMlns)
        {
            // Nếu không phải lns thì ko dc thêm b quản lý
            if (string.IsNullOrEmpty(mlns.Lns))
            {
                return false;
            }
            if (!StringUtils.IsNullOrEmpty(mlns.L) ||
                !StringUtils.IsNullOrEmpty(mlns.K) ||
                !StringUtils.IsNullOrEmpty(mlns.M) ||
                !StringUtils.IsNullOrEmpty(mlns.Tm) ||
                !StringUtils.IsNullOrEmpty(mlns.Ttm) ||
                !StringUtils.IsNullOrEmpty(mlns.Ng) ||
                !StringUtils.IsNullOrEmpty(mlns.Tng))
            {
                return false;
            }
            // Nếu có dòng con là lns thì ko dc thêm
            IEnumerable<NsMucLucNganSach> children = ListOfMlns.Where(p => p.MlnsIdParent.Equals(mlns.MlnsId));
            bool hasLnsChild = children.Any(p => !string.IsNullOrEmpty(p.Lns) &&
                StringUtils.IsNullOrEmpty(p.L) &&
                StringUtils.IsNullOrEmpty(p.K) &&
                StringUtils.IsNullOrEmpty(p.M) &&
                StringUtils.IsNullOrEmpty(p.Tm) &&
                StringUtils.IsNullOrEmpty(p.Ttm) &&
                StringUtils.IsNullOrEmpty(p.Ng) &&
                StringUtils.IsNullOrEmpty(p.Tng));
            return !hasLnsChild;
        }
    }
}
