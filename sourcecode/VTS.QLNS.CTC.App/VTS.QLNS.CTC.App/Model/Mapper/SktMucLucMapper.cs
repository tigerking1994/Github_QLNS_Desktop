using AutoMapper;
using System;
using System.Linq;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class SktMucLucMapper : Profile
    {
        public SktMucLucMapper()
        {
            CreateMap<NsSktMucLuc, SktMucLucModel>()
                .ForMember(x => x.MLNS, y => y.MapFrom(z => string.Join("; ", z.SktMucLucMaps.Select(map => map.SNsXauNoiMa))))
                .ReverseMap();
            CreateMap<SktMucLucModel, NsSktMucLuc>()
                .ForMember(x => x.SNGCha, y => y.MapFrom(z => z.SNGCha == null ? "" : z.SNGCha))
                .ForMember(x => x.SNg, y => y.MapFrom(z => z.SNg == null ? "" : z.SNg))
                .ReverseMap();
            CreateMap<SktMucLucQuery, SktMucLucDuToanDauNamModel>()
                .ForMember(x => x.IsHangCha, y => y.MapFrom(z => z.BHangCha))
                .ReverseMap();
            CreateMap<SktMucLucDtQuery, SktMucLucDuToanDauNamModel>()
                .ForMember(x => x.IsHangCha, y => y.MapFrom(z => z.BHangCha))
                .ReverseMap();
            CreateMap<DanhMucMucLucSKTImportModel, NsSktMucLuc>()
                //.ForMember(x => x.Id, y => y.MapFrom(z => string.IsNullOrEmpty(z.Id) ? Guid.Empty : Guid.Parse(z.Id)))
                .ForMember(x => x.Id, y => Guid.NewGuid())
                .ForMember(x => x.INamLamViec, y => y.MapFrom(z => int.Parse(z.INamLamViec)));
        }
    }
}