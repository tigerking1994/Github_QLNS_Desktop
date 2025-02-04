using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlQtChungTuChiTietGiaiThichNq104Mapper : Profile
    {
        public TlQtChungTuChiTietGiaiThichNq104Mapper()
        {
            CreateMap<TlQtChungTuChiTietGiaiThichNq104, TlQtChungTuChiTietGiaiThichNq104Model>();
            CreateMap<TlQtChungTuChiTietGiaiThichNq104Model, TlQtChungTuChiTietGiaiThichNq104>();
        }
    }
}
