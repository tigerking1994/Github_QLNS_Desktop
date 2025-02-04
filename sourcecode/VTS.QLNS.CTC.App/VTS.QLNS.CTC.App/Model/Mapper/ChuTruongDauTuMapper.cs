using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class ChuTruongDauTuMapper: Profile
    {
        public ChuTruongDauTuMapper()
        {
            CreateMap<Core.Domain.VdtDaChuTruongDauTu, ChuTruongDauTuModel>();
            CreateMap<Core.Domain.Query.ChuTruongDauTuQuery, ChuTruongDauTuModel>();
            CreateMap<ChuTruongDauTuModel, Core.Domain.VdtDaChuTruongDauTu>();
        }
    }
}
