using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhDaDuToanNguonVonMapper : Profile
    {
        public NhDaDuToanNguonVonMapper()
        {
            CreateMap<Core.Domain.NhDaQdDauTuNguonVon, NhDaDuToanNguonVonModel>();
            CreateMap<NhDaQdDauTuNguonVonModel, NhDaDuToanNguonVonModel>();
            CreateMap<Core.Domain.NhDaDuToanNguonVon, NhDaDuToanNguonVonModel>();
            CreateMap<NhDaDuToanNguonVonModel, Core.Domain.NhDaDuToanNguonVon>();
        }
    }
}
