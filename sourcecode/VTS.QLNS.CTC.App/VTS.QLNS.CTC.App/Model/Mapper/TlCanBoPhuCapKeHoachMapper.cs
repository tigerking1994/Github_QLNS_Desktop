using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlCanBoPhuCapKeHoachMapper : Profile
    {
        public TlCanBoPhuCapKeHoachMapper()
        {
            CreateMap<TlCanBoPhuCapKeHoach, TlCanBoPhuCapKeHoachModel>();
            CreateMap<TlCanBoPhuCapKeHoachModel, TlCanBoPhuCapKeHoach>();
            CreateMap<TlCanBoPhuCapKeHoach, TlCanBoPhuCapKeHoachQuery>().ReverseMap();
            CreateMap<TlCanBoPhuCapKeHoachModel, TlCanBoPhuCapKeHoachQuery>().ReverseMap();
        }
    }
}
