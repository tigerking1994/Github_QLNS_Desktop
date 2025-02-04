using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlQsChungTuNq104Mapper : Profile
    {
        public TlQsChungTuNq104Mapper()
        {
            CreateMap<TlQsChungTuNq104Model, TlQsChungTuNq104>();
            CreateMap<TlQsChungTuNq104, TlQsChungTuNq104Model>()
                .ForMember(entity => entity.NgayTaoString, model => model.MapFrom(x => x.NgayTao.ToString("dd/MM/yyyy")));
        }
    }
}
