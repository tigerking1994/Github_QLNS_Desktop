using AutoMapper;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class AuthenticationInfoMapper : Profile
    {
        public AuthenticationInfoMapper()
        {
            CreateMap<SessionInfo, AuthenticationInfo>();
        }
    }
}
