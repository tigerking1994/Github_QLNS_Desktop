using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository.Impl;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class GroupUserService : IGroupUserService
    {
        private readonly IGroupUserRepository _groupUserRepository;

        public GroupUserService(IGroupUserRepository groupUserRepository)
        {
            _groupUserRepository = groupUserRepository;
        }

        public void Add(HtNhomNguoiDung entity)
        {
            _groupUserRepository.Add(entity);
        }
    }
}
