using AutoMapper;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhDaHopDongMapper : Profile
    {
        public NhDaHopDongMapper()
        {
            CreateMap<NhDaHopDongQuery, ComboboxItem>()
                .ForMember(x => x.DisplayItem, y => y.MapFrom(z => $"{z.SSoHopDong} - {z.STenHopDong}"))
                .ForMember(x => x.HiddenValue, y => y.MapFrom(z => z.Id.ToString()))
                .ForMember(x => x.ValueItem, y => y.MapFrom(z => z.IIdDuAnId.ToString()));
            CreateMap<NhDaHopDong, NhDaHopDongModel>();
            CreateMap<NhDaHopDongModel, NhDaHopDong>();
            CreateMap<NhDaHopDongModel, NhDaHopDongQuery>();
            CreateMap<NhDaHopDongQuery, NhDaHopDongModel>();

            CreateMap<NhDaHopDongHangMucModel, NhDaHopDongHangMuc>();
            CreateMap<NhDaHopDongHangMuc, NhDaHopDongHangMucModel>();
            CreateMap<NhDaHopDongTrongNuocQuery, NhDaHopDongModel>();
            CreateMap<NhDaHopDongImport, NhDaHopDong>()
                .ForMember(entity => entity.Id, model => model.MapFrom(y => y.IIdHopDongID))
                //.ForMember(entity => entity.ILoai, model => model.MapFrom(y => y.ILoai))
                .ForMember(entity => entity.IThuocMenu, model => model.MapFrom(y => y.IThuocMenu))
                .ForMember(entity => entity.ILoai, model => model.MapFrom(y => y.ILoai))
                .ForMember(entity => entity.ILanDieuChinh, model => model.MapFrom(y => y.ILoai))
                .ForMember(entity => entity.SSoHopDong, model => model.MapFrom(y => y.SoHopDong))
                .ForMember(entity => entity.STenHopDong, model => model.MapFrom(y => y.TenHopDong))
                .ForMember(entity => entity.DNgayHopDong, model => model.MapFrom(y => y.DNgayHopDong));
                

        }
    }
}
