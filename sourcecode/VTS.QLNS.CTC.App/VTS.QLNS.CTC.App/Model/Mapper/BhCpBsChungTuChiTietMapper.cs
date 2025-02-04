using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;
using AutoMapper;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class BhCpBsChungTuChiTietMapper : Profile
    {
        public BhCpBsChungTuChiTietMapper()
        {
            CreateMap<BhCpBsChungTuChiTiet, BhCpBsChungTuChiTietModel>()
                .ForMember(model => model.FThuaThieuTruocBoSung, entity => entity.MapFrom(item => item.FThuaThieu))
                .ReverseMap();
            CreateMap<BhCpBsChungTuChiTietModel, BhCpBsChungTuChiTietQuery>().ReverseMap();
        }
    }
}
