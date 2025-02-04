using AutoMapper;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhKhTongTheNhiemVuChiMapper : Profile
    {
        public NhKhTongTheNhiemVuChiMapper()
        {
            CreateMap<NhKhTongTheNhiemVuChiQuery, ComboboxItem>()
                .ForMember(x => x.DisplayItem, y => y.MapFrom(z => z.STenNhiemVuChi))
                .ForMember(x => x.HiddenValue, y => y.MapFrom(z => z.Id.ToString()))
                .ForMember(x => x.ValueItem, y => y.MapFrom(z => z.IIdNhiemVuChiId.ToString()));
            CreateMap<NhKhTongTheNhiemVuChi, NhKhTongTheNhiemVuChiModel>();
            CreateMap<NhKhTongTheNhiemVuChiModel, NhKhTongTheNhiemVuChi>();
            CreateMap<NhKhTongTheNhiemVuChiModel, NhKhTongTheNhiemVuChiQuery>();
            CreateMap<NhKhTongTheNhiemVuChiQuery, NhKhTongTheNhiemVuChiModel>();
        }
    }
}
