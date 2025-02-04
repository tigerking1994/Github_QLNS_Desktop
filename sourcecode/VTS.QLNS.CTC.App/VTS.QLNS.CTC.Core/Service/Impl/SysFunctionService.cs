using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class SysFunctionService : IService<HtChucNang>, ISysFunctionService
    {
        private ISysFunctionRepository _sysFunctionRepository;
        private ISysFunctionAuthorityRepository _sysFunctionAuthorityRepository;

        public SysFunctionService(ISysFunctionRepository sysFunctionRepository, ISysFunctionAuthorityRepository sysFunctionAuthorityRepository)
        {
            _sysFunctionRepository = sysFunctionRepository;
            _sysFunctionAuthorityRepository = sysFunctionAuthorityRepository;
        }

        public override void AddOrUpdateRange(IEnumerable<HtChucNang> listEntities, AuthenticationInfo authenticationInfo)
        {
            foreach(var i in listEntities)
            {
                // remove old list function authorities
                IEnumerable<HtQuyenChucNang> sysFunctionAuthorities = _sysFunctionAuthorityRepository.FindAll(t => t.IIDMaChucNang.Equals(i.IIDMaChucNang) && t.IIDMaQuyen != null).ToList();
                _sysFunctionAuthorityRepository.RemoveSysFunctionAuthorities(sysFunctionAuthorities);
                i.SysFunctionAuthorities = i.HTQuyens.Select(t => new HtQuyenChucNang
                {
                    IIDMaChucNang = i.IIDMaChucNang,
                    IIDMaQuyen = t.IIDMaQuyen
                }).ToList();
            }
            IEnumerable<HtQuyenChucNang> addedList = listEntities.SelectMany(t => t.SysFunctionAuthorities);
            _sysFunctionAuthorityRepository.AddRange(addedList);
            _sysFunctionRepository.AddOrUpdateRange(listEntities);
        }

        public override IEnumerable<HtChucNang> FindAll(AuthenticationInfo authenticationInfo)
        {
            IEnumerable<HtChucNang> result = _sysFunctionRepository.FindAllWithAuthorties().OrderBy(t => t.SSTT).ToList();
            foreach (var i in result)
            {
                i.HTQuyens = i.SysFunctionAuthorities.Select(t => t.HTQuyen).ToList();
                i.OldFuncCode = i.IIDMaChucNang;
                i.OldName = i.STenChucNang;
            }
            return result;
        }
    }
}
