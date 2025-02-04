using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlQtChungTuChiTietKeHoachNq104Mapper : Profile
    {
        public TlQtChungTuChiTietKeHoachNq104Mapper()
        {
            CreateMap<TlQtChungTuChiTietKeHoachNq104, TlQtChungTuChiTietKeHoachNq104Model>();
            CreateMap<TlQtChungTuChiTietKeHoachNq104Model, TlQtChungTuChiTietKeHoachNq104>();
            CreateMap<TlQtChungTuChiTietKeHoachNq104Model, TlQtChungTuChiTietKeHoachNq104Query>();
            CreateMap<TlQtChungTuChiTietKeHoachNq104Query, TlQtChungTuChiTietKeHoachNq104Model>();
            CreateMap<TlChungTuChiTietKeHoachNq104Query, TlQtChungTuChiTietKeHoachNq104Model>();
            CreateMap<TlQtChungTuChiTietKeHoachNq104Model, TlChungTuChiTietKeHoachNq104Query>();
        }
    }
}
