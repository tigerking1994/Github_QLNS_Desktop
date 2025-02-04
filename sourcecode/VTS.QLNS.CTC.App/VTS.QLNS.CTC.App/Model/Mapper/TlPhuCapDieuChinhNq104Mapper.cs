using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlPhuCapDieuChinhNq104Mapper : Profile
    {
        public TlPhuCapDieuChinhNq104Mapper()
        {
            CreateMap<TlPhuCapDieuChinhNq104, TlPhuCapDieuChinhNq104Model>().ReverseMap();
            CreateMap<TlPhuCapDieuChinhNq104Model, TlPhuCapDieuChinhNq104Query>().ReverseMap();
        }
    }
}
