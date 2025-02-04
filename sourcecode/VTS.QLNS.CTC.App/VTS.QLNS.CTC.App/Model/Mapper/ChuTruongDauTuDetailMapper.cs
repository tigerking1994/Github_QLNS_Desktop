using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class ChuTruongDauTuDetailMapper : Profile
    {
        public ChuTruongDauTuDetailMapper()
        {
            CreateMap<Core.Domain.Query.ChuTruongDauTuDetailQuery, ChuTruongDauTuDetailModel>();
        }
    }
}
