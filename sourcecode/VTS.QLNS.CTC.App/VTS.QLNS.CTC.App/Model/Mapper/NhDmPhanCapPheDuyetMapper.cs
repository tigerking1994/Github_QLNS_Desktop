using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhDmPhanCapPheDuyetMapper : Profile
    {
        public NhDmPhanCapPheDuyetMapper()
        {
            CreateMap<NhDmPhanCapPheDuyet, NhDmPhanCapPheDuyetModel>();
            CreateMap<NhDmPhanCapPheDuyetModel, NhDmPhanCapPheDuyet>();
        }
    }
}
