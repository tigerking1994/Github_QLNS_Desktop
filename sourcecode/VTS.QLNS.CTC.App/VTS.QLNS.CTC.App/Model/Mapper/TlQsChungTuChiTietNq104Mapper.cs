using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlQsChungTuChiTietNq104Mapper : Profile
    {
        public TlQsChungTuChiTietNq104Mapper()
        {
            CreateMap<TlQsChungTuChiTietNq104, TlQsChungTuChiTietNq104Model>();
            CreateMap<TlQsChungTuChiTietNq104Model, TlQsChungTuChiTietNq104>();
        }
    }
}
