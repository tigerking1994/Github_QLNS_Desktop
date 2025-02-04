using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtDmChiPhiService : IService<VdtDmChiPhi>, IVdtDmChiPhiService
    {
        private readonly IVdtDmChiPhiRepository _vdtDmChiPhiRepository;

        public VdtDmChiPhiService(IVdtDmChiPhiRepository vdtDmChiPhiRepository)
        {
            _vdtDmChiPhiRepository = vdtDmChiPhiRepository;
        }

        public override void AddOrUpdateRange(IEnumerable<VdtDmChiPhi> listEntities, AuthenticationInfo authenticationInfo)
        {
            var time = DateTime.Now;
            foreach (var item in listEntities)
            {
                if (item.IsModified)
                {
                    if (Guid.Empty.Equals(item.IIdChiPhi))
                    {
                        item.DNgayTao = time;
                        item.SIdMaNguoiDungTao = authenticationInfo.Principal;
                        item.DNgaySua = null;
                        item.SIdMaNguoiDungSua = null;
                    }
                    else
                    {
                        item.DNgaySua = time;
                        item.SIdMaNguoiDungSua = authenticationInfo.Principal;
                    }
                }
            }
            _vdtDmChiPhiRepository.UpdateVdtDmChiPhi(listEntities);
        }

        public override IEnumerable<VdtDmChiPhi> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _vdtDmChiPhiRepository.FindAll(authenticationInfo);
        }

        public IEnumerable<VdtDmChiPhi> FindAll()
        {
            return _vdtDmChiPhiRepository.FindAll();
        }
    }
}
