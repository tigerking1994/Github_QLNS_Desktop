using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhChuTruongDauTuMapper : Profile
    {
        public NhChuTruongDauTuMapper()
        {
            CreateMap<Core.Domain.NhDaChuTruongDauTu, NhDaChuTruongDauTuModel>();
            CreateMap<Core.Domain.Query.NhDaChuTruongDauTuQuery, NhDaChuTruongDauTuModel>();
            CreateMap<NhDaChuTruongDauTuModel, Core.Domain.NhDaChuTruongDauTu>();
        }
    }
}
