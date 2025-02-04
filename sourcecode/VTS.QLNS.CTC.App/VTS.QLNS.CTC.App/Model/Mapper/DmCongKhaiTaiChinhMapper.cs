using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class DmCongKhaiTaiChinhMapper : Profile
    {
        public DmCongKhaiTaiChinhMapper()
        {
            CreateMap<NsDanhMucCongKhai, DmCongKhaiTaiChinhModel>()
                .ForMember(n => n.Stt, m => m.MapFrom(n => n.STT))
                .ForMember(n => n.IIdDmCongKhaiCha, m => m.MapFrom(n => n.iID_DMCongKhai_Cha))
                .ForMember(n => n.IsHangCha, m => m.MapFrom(n => n.bHangCha))
                .ForMember(n => n.SMa, m => m.MapFrom(n => n.sMa))
                .ForMember(n => n.SMaCha, m => m.MapFrom(n => n.sMaCha))
                .ForMember(source => source.Mlns, dest => dest.MapFrom(c => string.Join(",", c.NsDmCongKhaiMlns.Select(c => c.sNS_XauNoiMa))));

            CreateMap<DmCongKhaiTaiChinhModel, NsDanhMucCongKhai>()
                .ForMember(source => source.NsDmCongKhaiMlns, dest => dest.MapFrom(c => c.Mlns.Split(',').Select(d => new NsDmCongKhaiMlns()
                {
                    Id = Guid.NewGuid(),
                    dNgayTao = DateTime.Now,
                    sNS_XauNoiMa = d,
                    iNamLamViec = 0,
                    iID_DMCongKhai = c.Id
                })))
                .ForMember(n => n.STT, m => m.MapFrom(n => n.Stt))
                .ForMember(n => n.iID_DMCongKhai_Cha, m => m.MapFrom(n => n.IIdDmCongKhaiCha))
                .ForMember(n => n.bHangCha, m => m.MapFrom(n => n.IsHangCha))
                .ForMember(n => n.sMa, m => m.MapFrom(n => n.SMa))
                .ForMember(n => n.sMaCha, m => m.MapFrom(n => n.SMaCha))
                .ForMember(source => source.iNamLamViec, dest => dest.MapFrom(c => 0));
        }
    }
}
