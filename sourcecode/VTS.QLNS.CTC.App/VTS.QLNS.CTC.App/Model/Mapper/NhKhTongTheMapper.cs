using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhKhTongTheMapper : Profile
    {
        public NhKhTongTheMapper()
        {
            CreateMap<NhKhTongThe, NhKhTongTheModel>();
            CreateMap<NhKhTongTheModel, NhKhTongThe> ();
            CreateMap<NhKhTongTheQuery, NhKhTongTheModel>();
            CreateMap<NhKhTongTheModel, NhKhTongTheQuery>();
        }
    }
}
