using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class HTLoaiQuyenMapper : Profile
    {
        public HTLoaiQuyenMapper()
        {
            CreateMap<HtLoaiQuyen, HTLoaiQuyenModel>()
                .ForMember(x => x.HTQuyenModels, y => y.MapFrom(z => z.HtQuyens));
        }
    }
}
