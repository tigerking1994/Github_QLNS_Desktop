using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlQtChungTuChiTietNq104Mapper : Profile
    {
        public TlQtChungTuChiTietNq104Mapper()
        {
            CreateMap<TlQtChungTuChiTietNq104Model, TlQtChungTuChiTietNq104>();
            CreateMap<TlQtChungTuChiTietNq104, TlQtChungTuChiTietNq104Model>();
            CreateMap<TlQtChungTuChiTietNq104Model, TlQtChungTuChiTietNq104Query>();
            CreateMap<TlQtChungTuChiTietNq104Query, TlQtChungTuChiTietNq104>();
            CreateMap<TlQtChungTuChiTietNq104Query, TlQtChungTuChiTietNq104Model>()
                .ForMember(q => q.SoNguoi, m => m.MapFrom(i => (int?)i.SoNguoi));
            CreateMap<ReportQttxTheoCotNq104Query, TlQtChungTuChiTietNq104Model>()
                .ForMember(q => q.SoNguoi, m => m.MapFrom(i => (int?)i.SoNguoi));
        }
    }
}
