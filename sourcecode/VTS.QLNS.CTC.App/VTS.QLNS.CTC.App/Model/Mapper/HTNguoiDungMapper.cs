using AutoMapper;
using System.Linq;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class HTNguoiDungMapper : Profile
    {
        public HTNguoiDungMapper()
        {
            CreateMap<HtNguoiDung, HTNguoiDungModel>()
                .ForMember(x => x.SysGroupModels, y => y.MapFrom(z => z.SysGroupUsers.Select(p => p.HTNhom)))
                .ForMember(x => x.NsNguoiDungLnsModels, y => y.MapFrom(z => z.NguoiDungLns))
                .ForMember(x => x.NsNguoiDungDonViModels, y => y.MapFrom(z => z.NsNguoiDungDonVis));
            //.ForMember(x => x.SysAuthorityModels, y => y.MapFrom(z => z.SysUserAuthorities.Select(p => p.AuthorityNameNavigation)));
            CreateMap<HTNguoiDungModel, HtNguoiDung>()
                .ForMember(x => x.SMatKhau, y => y.Ignore());
            CreateMap<HtNguoiDung, SessionInfo>()
                .ForMember(x => x.Principal, y => y.MapFrom(z => z.STaiKhoan));
                //.ForMember(x => x.Authorities, y => y.MapFrom(z => z.SysUserAuthorities.Select(t => t.AuthorityName)));
        }
    }
}
