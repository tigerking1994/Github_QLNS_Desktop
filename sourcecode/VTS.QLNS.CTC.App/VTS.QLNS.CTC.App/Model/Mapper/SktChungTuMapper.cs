using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class SktChungTuMapper : Profile
    {
        public SktChungTuMapper()
        {
            CreateMap<NsSktChungTu, NsSktChungTuModel>().ReverseMap();
            CreateMap<NsSktChungTuQuery, NsSktChungTuModel>().ReverseMap();
            CreateMap<NsSktChungTu, ChungTuCanCuModel>()
                .ForMember(model => model.SoChungTu, member => member.MapFrom(entity => entity.SSoChungTu))
                .ForMember(model => model.NgayChungTu, member => member.MapFrom(entity => entity.DNgayChungTu))
                .ForMember(model => model.SoQuyetDinh, member => member.MapFrom(entity => entity.SSoQuyetDinh))
                .ForMember(model => model.NgayQuyetDinh, member => member.MapFrom(entity => entity.DNgayQuyetDinh));
        }
    }
}