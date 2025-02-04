using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class BhDmMucLucNganSachMapper : Profile
    {
        public BhDmMucLucNganSachMapper()
        {
            CreateMap<BhDmMucLucNganSach, BhDmMucLucNganSachModel>().ReverseMap();
            CreateMap<BhDmMucLucNganSachQuery, BhDmMucLucNganSach>().ReverseMap();
            CreateMap<BhDmMucLucNganSachQuery, BhDmMucLucNganSachModel>().ReverseMap();
            CreateMap<BhDmMucLucNganSach, MLNSBHXHImportModel>().ReverseMap();
            CreateMap<BhDmMucLucNganSach, CheckBoxTreeItem>()
               .ForMember(entity => entity.DisplayItem, model => model.MapFrom(z => string.Format("{0} - {1}", z.SLNS, z.SMoTa)))
               .ForMember(entity => entity.Id, model => model.MapFrom(z => z.IIDMLNS))
               .ForMember(entity => entity.ParentId, model => model.MapFrom(z => z.IIDMLNSCha))
               .ForMember(entity => entity.ValueItem, model => model.MapFrom(z => z.SLNS));
            CreateMap<BhDmMucLucNganSach, CheckBoxItem>()
               .ForMember(entity => entity.DisplayItem, model => model.MapFrom(item => $"{item.SLNS} - {item.SMoTa}"))
               .ForMember(entity => entity.NameItem, model => model.MapFrom(item => item.SMoTa))
               .ForMember(entity => entity.ValueItem, model => model.MapFrom(item => item.SLNS))
               .ForMember(entity => entity.Id, model => model.MapFrom(item => item.IIDMLNSCha));
        }
    }
}
