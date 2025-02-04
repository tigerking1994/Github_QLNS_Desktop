using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlQtChungTuMapper : Profile
    {
        public TlQtChungTuMapper()
        {
            CreateMap<TlQtChungTuModel, TlQtChungTu>();
            CreateMap<TlQtChungTu, TlQtChungTuModel>();
        }
    }
}
