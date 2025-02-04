using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class DisplaySysFunctionService : SysFunctionService, IDisplaySysFunctionService
    {
        private ISysFunctionRepository _sysFunctionRepository;
        private ISysFunctionAuthorityRepository _sysFunctionAuthorityRepository;

        public DisplaySysFunctionService(ISysFunctionRepository sysFunctionRepository, ISysFunctionAuthorityRepository sysFunctionAuthorityRepository) : base(sysFunctionRepository, sysFunctionAuthorityRepository)
        {
            _sysFunctionRepository = sysFunctionRepository;
            _sysFunctionAuthorityRepository = sysFunctionAuthorityRepository;
        }

        public override void AddOrUpdateRange(IEnumerable<HtChucNang> listEntities, AuthenticationInfo authenticationInfo)
        {
            List<HtChucNang> newAddedList = new List<HtChucNang>();
            foreach (HtChucNang sysFunction in listEntities)
            {
                HtChucNang entity = _sysFunctionRepository.FindOneWithAuthorities(sysFunction.Id);
                if (entity != null)
                {
                    _sysFunctionRepository.Delete(entity);
                }
                if (!sysFunction.IsDeleted)
                {
                    // sysFunction.Id = Guid.Empty;
                    // sysFunction.IIDChucNangCha = entity.IIDChucNangCha;
                    sysFunction.SysFunctionAuthorities = sysFunction.HTQuyens.Select(t => new HtQuyenChucNang
                    {
                        IIDMaChucNang = sysFunction.IIDMaChucNang,
                        IIDMaQuyen = t.IIDMaQuyen
                    }).ToList();
                    sysFunction.ITrangThai = true;
                    newAddedList.Add(sysFunction);
                }
            }
            _sysFunctionRepository.AddRange(newAddedList);
        }
    }
}
