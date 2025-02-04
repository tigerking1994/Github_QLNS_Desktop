using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlQtChungTuChiTietKeHoachMapper : Profile
    {
        public TlQtChungTuChiTietKeHoachMapper()
        {
            CreateMap<TlQtChungTuChiTietKeHoach, TlQtChungTuChiTietKeHoachModel>();
            CreateMap<TlQtChungTuChiTietKeHoachModel, TlQtChungTuChiTietKeHoach>();
            CreateMap<TlQtChungTuChiTietKeHoachModel, TlQtChungTuChiTietKeHoachQuery>();
            CreateMap<TlQtChungTuChiTietKeHoachQuery, TlQtChungTuChiTietKeHoachModel>();
            CreateMap<TlChungTuChiTietKeHoachQuery, TlQtChungTuChiTietKeHoachModel>();
            CreateMap<TlQtChungTuChiTietKeHoachModel, TlChungTuChiTietKeHoachQuery>();
        }
    }
}
