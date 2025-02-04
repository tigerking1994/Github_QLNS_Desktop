using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class BhThamDinhQuyetToanMapper : Profile
    {
        public BhThamDinhQuyetToanMapper()
        {
            CreateMap<BhThamDinhQuyetToanModel, BhThamDinhQuyetToan>()
                .ForMember(entity => entity.BKhoa, model => model.MapFrom(x => x.IsLocked)).ReverseMap();
            CreateMap<BhThamDinhQuyetToanQuery, BhThamDinhQuyetToanModel>()
                .ForMember(entity => entity.IsLocked, model => model.MapFrom(x => x.BKhoa)).ReverseMap();
            CreateMap<BhDmThamDinhQuyetToan, BhDmThamDinhQuyetToanModel>().ReverseMap();
            CreateMap<BhThamDinhQuyetToanChiTiet, BhThamDinhQuyetToanChiTietModel>()
                .ForMember(entity => entity.Id, model => model.MapFrom(x => x.Id)).ReverseMap();
            CreateMap<BhThamDinhQuyetToanChiTietQuery, BhThamDinhQuyetToanChiTietModel>()
                .ForMember(entity => entity.Id, model => model.MapFrom(x => x.IID_BH_TDQT_ChungTuChiTiet)).ReverseMap();
        }
    }
}
