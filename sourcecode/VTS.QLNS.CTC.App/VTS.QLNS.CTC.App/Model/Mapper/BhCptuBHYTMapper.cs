using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class BhCptuBHYTMapper : Profile
    {
        public BhCptuBHYTMapper()
        {
            CreateMap<BhCptuBHYTModel, BhCptuBHYT>().ReverseMap();
        }
    }
}
