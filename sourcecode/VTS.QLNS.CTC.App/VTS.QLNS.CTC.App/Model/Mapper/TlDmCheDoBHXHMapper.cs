using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlDmCheDoBHXHMapper : Profile
    {
        public TlDmCheDoBHXHMapper()
        {
            CreateMap<TlDmCheDoBHXH, TlDmCheDoBHXHModel>().ReverseMap();
            CreateMap<TlDmCheDoBHXH, TlDmCheDoBHXHHeThongModel>().ReverseMap();
            CreateMap<DanhMucCheDoBHXHImportModel, TlDmCheDoBHXH>()
                .ForMember(x => x.Id, y => y.MapFrom(z => Guid.NewGuid()))
                .ForMember(x => x.SMaCheDo, y => y.MapFrom(z => z.SMaCheDo))
                //.ForMember(x => x.ILoaiCheDo, y => y.MapFrom(z => int.Parse(z.ILoaiCheDo)))
                //.ForMember(x => x.IsFormula, y => y.MapFrom(z => bool.Parse(z.IsFormula)))
                .ForMember(x => x.SMaCheDoCha, y => y.MapFrom(z => z.SMaCheDoCha))
                .ForMember(x => x.SMoTa, y => y.MapFrom(z => z.SMoTa));
            CreateMap<TlDmCheDoBHXH, ComboboxItem>()
                .ForMember(x => x.DisplayItem, y => y.MapFrom(z => z.SMaCheDo))
                .ForMember(x => x.ValueItem, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.HiddenValue, y => y.MapFrom(z => z.STenCheDo));
            CreateMap<ComboboxItem, TlDmCheDoBHXH>()
                .ForMember(x => x.SMaCheDo, y => y.MapFrom(z => z.DisplayItem))
                .ForMember(x => x.Id, y => y.MapFrom(z => z.ValueItem));
        }
    }
}
