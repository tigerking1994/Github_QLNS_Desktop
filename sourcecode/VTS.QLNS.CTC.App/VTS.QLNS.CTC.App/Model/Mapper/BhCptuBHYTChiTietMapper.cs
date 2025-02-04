using AutoMapper;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class BhCptuBHYTChiTietMapper : Profile
    {
        public BhCptuBHYTChiTietMapper()
        {
            CreateMap<BhCptuBHYTChiTietModel, BhCptuBHYTChiTiet>().ReverseMap();
            CreateMap<BhCptuBHYTChiTietQuery, BhCptuBHYTChiTietModel>()
                .ForMember(entity => entity.Id, model => model.MapFrom(item => item.IID_BH_CP_CapTamUng_KCB_BHYT_ChiTiet)).ReverseMap();

        }
    }
}
