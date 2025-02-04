using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class DmNhomChuKyService : IService<DanhMuc>, IDmNhomChuKyService
    {
        private readonly IDmNhomChuKyRepository _dmNhomChuKyRepository;

        public DmNhomChuKyService(IDmNhomChuKyRepository dmNhomChuKyRepository)
        {
            _dmNhomChuKyRepository = dmNhomChuKyRepository;
        }

        public override void AddOrUpdateRange(IEnumerable<DanhMuc> listEntities, AuthenticationInfo authenticationInfo)
        {
            var time = DateTime.Now;
            foreach (var item in listEntities)
            {
                item.INamLamViec = authenticationInfo.YearOfWork;
                item.SType = DanhMucChuKy.CHU_KY_GROUP;
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
            _dmNhomChuKyRepository.AddOrUpdateRange(listEntities);
        }

        public override IEnumerable<DanhMuc> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _dmNhomChuKyRepository.FindAll(d => authenticationInfo.YearOfWork == d.INamLamViec && DanhMucChuKy.CHU_KY_GROUP.Equals(d.SType)).ToList();
        }
    }
}
