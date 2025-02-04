using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class DmMucLucQuyetToanMapper : Profile
    {
        public DmMucLucQuyetToanMapper()
        {
            CreateMap<NsMucLucQuyetToanNam, DmMucLucQuyetToanModel>()
                .ForMember(n => n.Stt, m => m.MapFrom(n => n.STT))
                .ForMember(n => n.IsHangCha, m => m.MapFrom(n => n.BHangCha))
                .ForMember(n => n.SMa, m => m.MapFrom(n => n.Ma))
                .ForMember(n => n.SMoTa, m => m.MapFrom(n => n.MoTa))
                .ForMember(n => n.SMaCha, m => m.MapFrom(n => n.MaCha))
                .ForMember(source => source.Mlns, dest => dest.MapFrom(c => string.Join(",", c.NsMucLucQuyetToanNamMLNS.Select(c => c.XauNoiMa))));

            CreateMap<DmMucLucQuyetToanModel, NsMucLucQuyetToanNam>()
                .ForMember(n => n.STT, m => m.MapFrom(n => n.Stt))
                .ForMember(n => n.BHangCha, m => m.MapFrom(n => n.IsHangCha))
                .ForMember(n => n.Ma, m => m.MapFrom(n => n.SMa))
                .ForMember(n => n.MoTa, m => m.MapFrom(n => n.SMoTa))
                .ForMember(n => n.MaCha, m => m.MapFrom(n => n.SMaCha));
        }
    }


  
}
