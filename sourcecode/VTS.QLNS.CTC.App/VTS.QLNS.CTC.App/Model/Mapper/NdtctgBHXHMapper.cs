using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NdtctgBHXHMapper : Profile
    {
        public NdtctgBHXHMapper()
        {
            CreateMap<BhDtctgBHXHModel, BhDtctgBHXH>().ReverseMap();
            CreateMap<BhDtctgBHXHQuery, BhDtctgBHXHModel>().ReverseMap();
            CreateMap<BhDtctgBHXHQuery, BhDtctgBHXH>().ReverseMap();
        }
    }
}
