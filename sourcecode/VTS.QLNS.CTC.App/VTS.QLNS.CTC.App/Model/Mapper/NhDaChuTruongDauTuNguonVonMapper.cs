using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhDaChuTruongDauTuNguonVonMapper : Profile
    {
        public NhDaChuTruongDauTuNguonVonMapper()
        {
            CreateMap<NhDaDuAnNguonVon, NhDaChuTruongDauTuNguonVonModel>();
            CreateMap<NhDaChuTruongDauTuNguonVon, NhDaChuTruongDauTuNguonVonModel>();
            CreateMap<NhDaChuTruongDauTuNguonVonModel, NhDaChuTruongDauTuNguonVon>();
        }
    }
}
