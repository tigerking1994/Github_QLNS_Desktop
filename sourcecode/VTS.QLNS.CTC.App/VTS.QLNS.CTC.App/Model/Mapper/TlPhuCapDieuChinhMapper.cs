using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlPhuCapDieuChinhMapper : Profile
    {
        public TlPhuCapDieuChinhMapper()
        {
            CreateMap<TlPhuCapDieuChinh, TlPhuCapDieuChinhModel>();
            CreateMap<TlPhuCapDieuChinhModel, TlPhuCapDieuChinh>();
            CreateMap<TlPhuCapDieuChinhModel, TlPhuCapDieuChinhQuery>();
            CreateMap<TlPhuCapDieuChinhQuery, TlPhuCapDieuChinhModel>();
        }
    }
}
