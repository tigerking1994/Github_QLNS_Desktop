using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlDieuChinhQsKeHoachMapper : Profile
    {
        public TlDieuChinhQsKeHoachMapper()
        {
            CreateMap<TlDieuChinhQsKeHoach, TlDieuChinhQsKeHoachModel>();
            CreateMap<TlDieuChinhQsKeHoachModel, TlDieuChinhQsKeHoach>();
        }
    }
}
