using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlQtChungTuNq104Mapper : Profile
    {
        public TlQtChungTuNq104Mapper()
        {
            CreateMap<TlQtChungTuNq104Model, TlQtChungTuNq104>();
            CreateMap<TlQtChungTuNq104, TlQtChungTuNq104Model>();
        }
    }
}
