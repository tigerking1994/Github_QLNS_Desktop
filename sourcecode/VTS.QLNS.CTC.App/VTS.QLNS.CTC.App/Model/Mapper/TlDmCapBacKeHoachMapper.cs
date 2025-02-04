using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlDmCapBacKeHoachMapper : Profile
    {
        public TlDmCapBacKeHoachMapper()
        {
            CreateMap<TlDmCapBacKeHoachModel, TlDmCapBacKeHoach>();
            CreateMap<TlDmCapBacKeHoach, TlDmCapBacKeHoachModel>();
        }
    }
}
