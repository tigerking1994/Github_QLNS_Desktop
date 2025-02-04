using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlQtChungTuChiTietMapper : Profile
    {
        public TlQtChungTuChiTietMapper()
        {
            CreateMap<TlQtChungTuChiTietModel, TlQtChungTuChiTiet>();
            CreateMap<TlQtChungTuChiTiet, TlQtChungTuChiTietModel>();
            CreateMap<TlQtChungTuChiTietModel, TlQtChungTuChiTietQuery>();
            CreateMap<TlQtChungTuChiTietQuery, TlQtChungTuChiTiet>();
            CreateMap<TlQtChungTuChiTietQuery, TlQtChungTuChiTietModel>()
                .ForMember(q => q.SoNguoi, m => m.MapFrom(i => (int?)i.SoNguoi));
            CreateMap<ReportQttxTheoCotQuery, TlQtChungTuChiTietModel>()
                .ForMember(q => q.SoNguoi, m => m.MapFrom(i => (int?)i.SoNguoi));
        }
    }
}
