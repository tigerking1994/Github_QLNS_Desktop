using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TnQtChungTuHD4554Mapper : Profile
    {
        public TnQtChungTuHD4554Mapper()
        {
            CreateMap<TnQtChungTuHD4554, TnQtChungTuHD4554Model>();
            CreateMap<TnQtChungTuHD4554Model, TnQtChungTuHD4554>();
        }
    }
}
